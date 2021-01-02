using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;

public class GridPointCalculator : MonoBehaviour
{
    public TextLabel TextLabelPrefab;
    public bool ShowPreview;


    private Grid<Tile> _grid;
    private Tile _objectiveTile;
    private CharacterController _currentAiCharacter;
    private Dictionary<(int, int), CharacterController> _playerCharacters;
    private Dictionary<(int, int), CharacterController> _aiCharacters;


    //Debugging data.
    private Dictionary<(int, int), TextLabel> _tilePointsLabels = new Dictionary<(int, int), TextLabel>();

    private void Update()
    {
        foreach (var textLabel in _tilePointsLabels.Values)
        {
            textLabel.gameObject.SetActive(ShowPreview);
        }
    }

    public void CalculateGridPoints(CharacterController aiCharacter)
    {
        _currentAiCharacter = aiCharacter;
        _grid = TileGridController.Instance.GetGrid();
        _playerCharacters = new Dictionary<(int, int), CharacterController>();
        _aiCharacters = new Dictionary<(int, int), CharacterController>();

        Initialize();
        CalculateGridPointsNearPlayerCharacters();
        CalculateGridPointsNearAllyCharacters();
        CalculateGridPointsForSelf();
    }

    private void Initialize()
    {
        for (var x = 0; x < _grid.GetGrid().GetLength(0); x++)
        {
            for (var y = 0; y < _grid.GetGrid().GetLength(1); y++)
            {
                var tile = _grid.GetValue(x, y);
                tile.Points = 0;

                if (tile.IsObjective)
                {
                    _objectiveTile = tile;
                }

                if (!string.IsNullOrEmpty(tile.CharacterControllerId) &&
                    _currentAiCharacter.Id != tile.CharacterControllerId)
                {
                    var characterController = TurnSystemManager.Instance.GetCharacterController(tile.CharacterControllerId);
                    if (characterController != null)
                    {
                        if (characterController.IsEnemy)
                        {
                            _aiCharacters.Add((tile.GridX, tile.GridY), characterController);
                        }
                        else
                        {
                            _playerCharacters.Add((tile.GridX, tile.GridY), characterController);
                        }
                    }
                }
            }
        }
    }

    #region Grid Points Nearby Player Characters

    private void CalculateGridPointsNearPlayerCharacters()
    {
        foreach (var playerCharacter in _playerCharacters.Values)
        {
            AddPointsForPromiximityToPlayerCharacter(playerCharacter);
            AddPointsForTilesPlayerCharacterCanMoveTo(playerCharacter);
            AddPointsForTilesPlayerCharacterCanAttack(playerCharacter);
        }
    }

    private void AddPointsForPromiximityToPlayerCharacter(CharacterController playerCharacterController)
    {
        var adjacencyPoints = _currentAiCharacter.Character.AICharacterHeuristics.OpponentHeuristics.OpponentAdjacencyPoints;
        AddPointsForPromiximityToCharacter(playerCharacterController, adjacencyPoints);
    }

    private void AddPointsForTilesPlayerCharacterCanMoveTo(CharacterController playerCharacterController)
    {
        var proximityPoints = _currentAiCharacter.Character.AICharacterHeuristics.OpponentHeuristics.OpponentProximityPoints;
        AddPointsForTilesCharacterCanMoveTo(playerCharacterController, proximityPoints);
    }

    private void AddPointsForTilesPlayerCharacterCanAttack(CharacterController playerCharacterController)
    {
        var attackPoints = _currentAiCharacter.Character.AICharacterHeuristics.OpponentHeuristics.OpponentAreaOfAttackPoints;
        AddPointsForTilesCharacterCanAttack(playerCharacterController, attackPoints);
    }

    #endregion

    #region Grid Points Nearby Ally Characters

    private void CalculateGridPointsNearAllyCharacters()
    {
        foreach (var allyCharacter in _aiCharacters.Values)
        {
            AddPointsForPromiximityToAllyCharacter(allyCharacter);
            AddPointsForTilesAllyCharacterCanMoveTo(allyCharacter);
            AddPointsForTilesAllyCharacterCanAttack(allyCharacter);
        }
    }

    private void AddPointsForPromiximityToAllyCharacter(CharacterController allyCharacterController)
    {
        var adjacencyPoints = _currentAiCharacter.Character.AICharacterHeuristics.AllyHeuristics.AllyAdjacencyPoints;
        AddPointsForPromiximityToCharacter(allyCharacterController, adjacencyPoints);
    }

    private void AddPointsForTilesAllyCharacterCanMoveTo(CharacterController allyCharacterController)
    {
        var proximityPoints = _currentAiCharacter.Character.AICharacterHeuristics.AllyHeuristics.AllyProximityPoints;
        AddPointsForTilesCharacterCanMoveTo(allyCharacterController, proximityPoints);
    }

    private void AddPointsForTilesAllyCharacterCanAttack(CharacterController allyCharacterController)
    {
        var attackPoints = _currentAiCharacter.Character.AICharacterHeuristics.AllyHeuristics.AllyAttackCoveragePoints;
        AddPointsForTilesCharacterCanAttack(allyCharacterController, attackPoints);
    }

    #endregion

    #region Grid Points for Self

    private void CalculateGridPointsForSelf()
    {
        AddPointsToTilesYouCanAttackFrom();
    }

    private void AddPointsToTilesYouCanAttackFrom()
    {
        var navigableTiles = _currentAiCharacter.CalculateAvailableMoves();
        foreach (var tile in navigableTiles.Values)
        {
            var attackPoints = _currentAiCharacter.Character.AICharacterHeuristics.SelfHeuristics.TilesCharacterCanAttackPoints;
            var affectedTiles = GetTilesAffectedByAttacks(_currentAiCharacter, tile.GridX, tile.GridY);
            foreach (var affectedTile in affectedTiles.Values)
            {
                if (!string.IsNullOrEmpty(affectedTile.CharacterControllerId))
                {
                    var characterController = TurnSystemManager.Instance.GetCharacterController(affectedTile.CharacterControllerId);
                    if (characterController != null && !characterController.IsEnemy)
                    {
                        tile.Points += attackPoints;
                    }
                }
            }
        }
    }

    #endregion

    #region Shared Heuristic Functions

    private void AddPointsForPromiximityToCharacter(CharacterController playerCharacterController, int points)
    {
        var tile = _grid.GetValue(playerCharacterController.transform.position);
        var adjacentTiles = new[]{
            _grid.GetValue(tile.GridX + 1, tile.GridY),
            _grid.GetValue(tile.GridX - 1, tile.GridY),
            _grid.GetValue(tile.GridX, tile.GridY + 1),
            _grid.GetValue(tile.GridX, tile.GridY - 1)};
        foreach (var adjacentTile in adjacentTiles)
        {
            if (adjacentTile != null)
            {
                adjacentTile.Points += points;
            }
        }
    }

    private void AddPointsForTilesCharacterCanMoveTo(CharacterController characterController, int points)
    {
        var tilesCharacterCanMoveTo = characterController.CalculateAvailableMoves();
        foreach (var tile in tilesCharacterCanMoveTo.Values)
        {
            if (tile != null)
            {
                tile.Points += points;
            }
        }
    }

    private void AddPointsForTilesCharacterCanAttack(CharacterController characterController, int points)
    {
        var characterTile = _grid.GetValue(characterController.transform.position);
        var tiles = GetTilesAffectedByAttacks(characterController, characterTile.GridX, characterTile.GridY);
        foreach (var tile in tiles.Values)
        {
            tile.Points += points;
        }
    }

    private Dictionary<(int, int), Tile> GetTilesAffectedByAttacks(
        CharacterController characterController,
        int x,
        int y)
    {
        var actions = characterController.Character.Weapon.Actions;
        var resultTiles = new Dictionary<(int, int), Tile>();

        foreach (var action in actions)
        {
            var actionController = characterController.GetActionController(action);
            if (actionController != null)
            {
                actionController.CalculateAffectedTiles(x, y);
                var tiles = actionController.GetAffectedTiles();
                foreach (var tile in tiles.Values)
                {
                    resultTiles[(tile.GridX, tile.GridY)] = tile;
                }
            }
        }

        return resultTiles;
    }

    #endregion

    #region Debugging Functions

    public void PreviewGridPoints()
    {
        foreach (var textLabel in _tilePointsLabels.Values)
        {
            Destroy(textLabel.gameObject);
        }
        _tilePointsLabels = new Dictionary<(int, int), TextLabel>();

        var grid = TileGridController.Instance.GetGrid();
        for (var x = 0; x < grid.GetGrid().GetLength(0); x++)
        {
            for (var y = 0; y < grid.GetGrid().GetLength(1); y++)
            {
                var tile = grid.GetValue(x, y);
                var textLabel = Instantiate(TextLabelPrefab, TileGridController.Instance.transform);
                textLabel.transform.position = grid.GetWorldPositionCentered(x, y);
                textLabel.SetText(tile.Points.ToString());
                var canvas = textLabel.GetComponentInChildren<Canvas>();
                if (canvas != null)
                {
                    canvas.worldCamera = Camera.main;
                }

                _tilePointsLabels.Add((x, y), textLabel);
            }
        }
    }

    public void UpdatePreview()
    {
        var grid = TileGridController.Instance.GetGrid();
        foreach (var textLabelKeyVal in _tilePointsLabels)
        {
            var tile = grid.GetValue(textLabelKeyVal.Key.Item1, textLabelKeyVal.Key.Item2);
            textLabelKeyVal.Value.SetText(tile.Points.ToString());
        }
    }

    #endregion
}
