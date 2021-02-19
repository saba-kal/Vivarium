using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MoveController : MonoBehaviour
{
    protected Grid<Tile> _grid;
    protected CharacterController _characterController;
    protected Dictionary<(int, int), Tile> _availableMoves = new Dictionary<(int, int), Tile>();
    protected BreadthFirstSearch _breadthFirstSearch;

    void Start()
    {
        VirtualStart();
    }

    protected virtual void VirtualStart()
    {
        _breadthFirstSearch = new BreadthFirstSearch();
        _grid = TileGridController.Instance.GetGrid();
        _characterController = GetComponent<CharacterController>();
    }

    public void HideMoveRadius()
    {
        TileGridController.Instance.RemoveHighlights(GridHighlightRank.Primary);
        _availableMoves = new Dictionary<(int, int), Tile>();
        _breadthFirstSearch.Reset();
    }

    public void ShowMoveRadius()
    {
        HideMoveRadius();
        CalculateAvailableMoves();
        TileGridController.Instance.HighlightTiles(_availableMoves, GridHighlightRank.Primary);
    }

    public virtual Dictionary<(int, int), Tile> CalculateAvailableMoves()
    {
        _grid = TileGridController.Instance.GetGrid();
        _breadthFirstSearch = new BreadthFirstSearch(_grid);

        var moveRadius = StatCalculator.CalculateStat(_characterController.Character, StatType.MoveRadius);
        var tile = _grid.GetValue(transform.position);
        _breadthFirstSearch.Execute(tile, Mathf.FloorToInt(moveRadius), _characterController.Character.NavigableTiles);
        _availableMoves = _breadthFirstSearch.GetVisitedTiles();

        List<(int, int)> waterLocations = new List<(int, int)>();
        foreach (KeyValuePair<(int, int), Tile> move in _availableMoves)
        {
            if (move.Value.Type == TileType.Water)
            {
                waterLocations.Add(move.Key);
            }
        }

        foreach ((int, int) location in waterLocations)
        {
            _availableMoves.Remove(location);
        }

        return _availableMoves;
    }

    public Dictionary<(int, int), Tile> GetAvailableMoves()
    {
        return _availableMoves;
    }

    public virtual bool IsAbleToMoveToTile(Tile tile)
    {
        return tile != null &&
            _availableMoves.ContainsKey((tile.GridX, tile.GridY));
    }

    public virtual void MoveToTile(Tile fromTile, Tile toTile, System.Action onMoveComplete = null, bool skipMovement = false)
    {
        if (fromTile == null || toTile == null)
        {
            return;
        }

        fromTile.CharacterControllerId = null;
        CommandController.Instance.ExecuteCommand(
            new MoveCommand(
                gameObject,
                _breadthFirstSearch.GetPathToTile(toTile),
                Constants.CHAR_MOVE_SPEED,
                onMoveComplete,
                true,
                skipMovement));
        toTile.CharacterControllerId = _characterController.Id;
    }

    public void MoveAlongPath(List<Tile> path, System.Action onMoveComplete = null, bool skipMovement = false)
    {
        if (path == null || path.Count == 0)
        {
            Debug.LogWarning($"Character \"{gameObject.name}\": Unable to move because the path list is empty.");
            return;
        }

        path[0].CharacterControllerId = null;
        CommandController.Instance.ExecuteCommand(
            new MoveCommand(
                gameObject,
                path,
                Constants.CHAR_MOVE_SPEED,
                onMoveComplete,
                true,
                skipMovement));
        path.Last().CharacterControllerId = _characterController.Id;
    }
}
