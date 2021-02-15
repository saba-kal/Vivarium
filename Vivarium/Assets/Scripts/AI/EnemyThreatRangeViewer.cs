using UnityEngine;
using System.Collections.Generic;
using UnityEditor.Rendering.Universal.ShaderGUI;
using System.Linq;
using System.Collections;

public class EnemyThreatRangeViewer : MonoBehaviour
{
    public GameObject TileHighlightPrefab;
    public float TileHeightOffset = 0.01f;
    public float OpacityChange = 0.1f;

    private TileGridController _gridController;
    private List<CharacterController> _enemyCharacters;
    private Dictionary<(int, int), ThreatRangeTile> _tileHighlights =
        new Dictionary<(int, int), ThreatRangeTile>();
    private bool _isEnabled = false;
    private bool _isInitialCalculation = true;

    private void OnEnable()
    {
        ThreatRangeToggle.OnToggleChange += ToggleThreatRange;
        LevelGenerator.OnLevelGenerationComplete += CalculateThreatRange;
        PlayerController.OnPlayerAttack += RecalculateThreatRange;
        TurnSystemManager.OnTurnStart += RecalculateThreatRange;
        CharacterController.OnDeath += RecalculateThreatRange;
        CharacterController.OnMove += TestMethod;
    }

    private void OnDisable()
    {
        ThreatRangeToggle.OnToggleChange -= ToggleThreatRange;
        LevelGenerator.OnLevelGenerationComplete -= CalculateThreatRange;
        PlayerController.OnPlayerAttack -= RecalculateThreatRange;
        TurnSystemManager.OnTurnStart -= RecalculateThreatRange;
        TurnSystemManager.OnTurnStart -= RecalculateThreatRange;
        CharacterController.OnDeath -= RecalculateThreatRange;
        CharacterController.OnMove -= TestMethod;
    }

    private void ToggleThreatRange(bool showThreatRange)
    {
        Initialize();
        _isEnabled = showThreatRange;
        foreach (var tileHighlight in _tileHighlights.Values)
        {
            if (tileHighlight.TileObject == null)
            {
                continue; //Tile object was destroyed.
            }

            tileHighlight.TileObject.SetActive(_isEnabled);
        }
    }

    public void CalculateThreatRange()
    {
        _isInitialCalculation = true;
        RecalculateThreatRange();
    }

    private void RecalculateThreatRange(CharacterController characterController)
    {
        RecalculateThreatRange();
    }

    private void TestMethod(CharacterController characterController)
    {
        Initialize();
        ClearHighlights();
        RecalculateThreatRange();
    }

    private void RecalculateThreatRange()
    {
        StartCoroutine(StartCalculationProcess());
        //if (_isInitialCalculation)
        //{
        //    _isInitialCalculation = false;
        //    return;
        //}

        //Initialize();
        //ClearHighlights();

        //foreach (var characterController in _enemyCharacters)
        //{
        //    if (characterController?.Character?.Weapon?.Actions == null)
        //    {
        //        continue;
        //    }

        //    CalculateCharacterThreatRange(characterController);
        //}
    }

    private IEnumerator StartCalculationProcess()
    {
        if (_isInitialCalculation)
        {
            yield return new WaitForSeconds(2f);
            _isInitialCalculation = false;
        }

        yield return new WaitForSeconds(0.5f);

        Initialize();
        ClearHighlights();

        foreach (var characterController in _enemyCharacters)
        {
            if (characterController?.Character?.Weapon?.Actions == null)
            {
                continue;
            }

            CalculateCharacterThreatRange(characterController);
        }
    }

    private void CalculateCharacterThreatRange(CharacterController characterController)
    {

        var visitedCharacterTiles = new HashSet<(int, int)>();

        var availableMoves = characterController.CalculateAvailableMoves();
        Debug.Log("Threat range moves:" + availableMoves.Count);
        foreach (var navigableTile in availableMoves.Values)
        {
            foreach (var action in characterController.Character.Weapon.Actions)
            {
                var affectedTiles = CalculateActionThreatRange(characterController, action, navigableTile);
                foreach (var tile in affectedTiles)
                {
                    if (tile.Type == TileType.Water ||
                        visitedCharacterTiles.Contains((tile.GridX, tile.GridY)))
                    {
                        continue;
                    }
                    else if (_tileHighlights.TryGetValue((tile.GridX, tile.GridY), out var threatRangeTile) &&
                        threatRangeTile.CharacterControllerId != characterController.Id)
                    {
                        IncrementTileOpacity(threatRangeTile.TileObject);
                    }
                    else
                    {
                        _tileHighlights.Add((tile.GridX, tile.GridY), new ThreatRangeTile
                        {
                            Tile = tile,
                            TileObject = CreateThreatRangeHighlight(tile),
                            CharacterControllerId = characterController.Id
                        });
                    }

                    if (!visitedCharacterTiles.Contains((tile.GridX, tile.GridY)))
                    {
                        visitedCharacterTiles.Add((tile.GridX, tile.GridY));
                    }
                }
            }
        }
    }

    private List<Tile> CalculateActionThreatRange(
        CharacterController characterController,
        Action action,
        Tile navigableTile)
    {
        var actionController = characterController.GetActionController(action);
        if (actionController != null)
        {
            actionController.CalculateAffectedTiles(navigableTile.GridX, navigableTile.GridY);
            return actionController.GetAffectedTiles().Values.ToList();
        }

        return new List<Tile>();
    }

    private GameObject CreateThreatRangeHighlight(Tile tile)
    {
        var tilePrefab = Instantiate(TileHighlightPrefab, _gridController.transform);
        tilePrefab.transform.position = _gridController.GetGrid()
            .GetWorldPositionCentered(tile.GridX, tile.GridY);
        tilePrefab.transform.Translate(new Vector3(0, TileHeightOffset, 0));
        tilePrefab.SetActive(_isEnabled);
        return tilePrefab;
    }

    private void Initialize()
    {
        _gridController = TileGridController.Instance;
        _enemyCharacters = TurnSystemManager.Instance.AIManager.AICharacters;
    }

    private void ClearHighlights()
    {
        foreach (var tileHighlight in _tileHighlights.Values)
        {
            Destroy(tileHighlight.TileObject);
        }

        _tileHighlights = new Dictionary<(int, int), ThreatRangeTile>();
    }

    private void IncrementTileOpacity(GameObject tileObject)
    {
        var renderer = tileObject.GetComponentInChildren<Renderer>();
        if (renderer != null)
        {
            var newMaterial = Instantiate(renderer.material);
            newMaterial.color = new Color(
                newMaterial.color.r,
                newMaterial.color.g,
                newMaterial.color.b,
                newMaterial.color.a + OpacityChange);
            renderer.material = newMaterial;
        }
    }
}

public class ThreatRangeTile
{
    public Tile Tile { get; set; }
    public string CharacterControllerId { get; set; }
    public GameObject TileObject { get; set; }
}
