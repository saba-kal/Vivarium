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

    private void CalculateGridPointsNearPlayerCharacters()
    {
        foreach (var playerCharacter in _playerCharacters.Values)
        {
            AddPointsForPromiximityToPlayerCharacter(playerCharacter);
        }
    }

    private void AddPointsForPromiximityToPlayerCharacter(CharacterController playerCharacterController)
    {
        var tile = _grid.GetValue(playerCharacterController.transform.position);
        var adjacencyPoints = _currentAiCharacter.Character.AICharacterHeuristics.OpponentHeuristics.OpponentAdjacencyPoints;
        var adjacentTiles = new[]{
            _grid.GetValue(tile.GridX + 1, tile.GridY),
            _grid.GetValue(tile.GridX - 1, tile.GridY),
            _grid.GetValue(tile.GridX, tile.GridY + 1),
            _grid.GetValue(tile.GridX, tile.GridY - 1)};
        foreach (var adjacentTile in adjacentTiles)
        {
            if (adjacentTile != null)
            {
                adjacentTile.Points += adjacencyPoints;
            }
        }
    }

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
