using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Handles logic for character movement on the grid.
/// </summary>
public class MoveController : MonoBehaviour
{
    protected Grid<Tile> _grid;
    protected CharacterController _characterController;
    protected Dictionary<(int, int), Tile> _availableMoves = new Dictionary<(int, int), Tile>();
    protected Dictionary<(int, int), Tile> _waterInRadius = new Dictionary<(int, int), Tile>();
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

    /// <summary>
    /// Hides character move range.
    /// </summary>
    public void HideMoveRadius()
    {
        TileGridController.Instance.RemoveHighlights(GridHighlightRank.Primary);
        TileGridController.Instance.RemoveHighlights(GridHighlightRank.Quaternary);
        _availableMoves = new Dictionary<(int, int), Tile>();
        _breadthFirstSearch.Reset();
    }

    /// <summary>
    /// Shows character move range on teh grid.
    /// </summary>
    public void ShowMoveRadius()
    {
        HideMoveRadius();
        CalculateAvailableMoves();
        TileGridController.Instance.HighlightTiles(_availableMoves, GridHighlightRank.Primary);
        TileGridController.Instance.HighlightTiles(_waterInRadius, GridHighlightRank.Quaternary);
    }

    /// <summary>
    /// Calculates all tiles that this character can move to.
    /// </summary>
    /// <returns>Position-to-tile dictionary containing all the tiles the character can move to.</returns>
    public virtual Dictionary<(int, int), Tile> CalculateAvailableMoves()
    {
        var tile = _grid.GetValue(transform.position);
        if (_characterController.Character.Type == CharacterType.QueenBee)
        {
            var healthController = GetComponent<HealthController>();
            if (healthController != null && !healthController.HasTakenDamage())
            {
                return new Dictionary<(int, int), Tile> { { (tile.GridX, tile.GridY), tile } };
            }
        }

        _grid = TileGridController.Instance.GetGrid();
        _breadthFirstSearch = new BreadthFirstSearch(_grid);

        var moveRadius = StatCalculator.CalculateStat(_characterController.Character, StatType.MoveRadius);
        _breadthFirstSearch.Execute(tile, Mathf.FloorToInt(moveRadius), _characterController.Character.NavigableTiles);
        _availableMoves = _breadthFirstSearch.GetVisitedTiles();

        _waterInRadius = new Dictionary<(int, int), Tile>();
        foreach (KeyValuePair<(int, int), Tile> move in _availableMoves)
        {
            if (move.Value.Type == TileType.Water)
            {
                _waterInRadius.Add(move.Key, move.Value);
            }
        }

        foreach (KeyValuePair<(int, int), Tile> location in _waterInRadius)
        {
            _availableMoves.Remove(location.Key);
        }

        return _availableMoves;
    }

    /// <summary>
    /// Gets all the tiles the character can move to.
    /// </summary>
    /// <returns>Position-to-tile dictionary containing all the tiles the character can move to.</returns>
    public Dictionary<(int, int), Tile> GetAvailableMoves()
    {
        return _availableMoves;
    }

    /// <summary>
    /// Gets whether or not the character can move to a specific tile.
    /// </summary>
    /// <param name="tile">The target tile.</param>
    /// <returns>Whether or not the character can move to the tile</returns>
    public virtual bool IsAbleToMoveToTile(Tile tile)
    {
        return tile != null &&
            _availableMoves.ContainsKey((tile.GridX, tile.GridY));
    }

    /// <summary>
    /// Moves character to a tile.
    /// </summary>
    /// <param name="fromTile">The starting tile of the character.</param>
    /// <param name="toTile">The destination tile.</param>
    /// <param name="onMoveComplete">Callback for when the movement is complete.</param>
    /// <param name="skipMovement">Boolean flag for skipping the move animation.</param>
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

    /// <summary>
    /// Moves character along a tile path.
    /// </summary>
    /// <param name="path">The path to move along.</param>
    /// <param name="onMoveComplete">Callback for when the movement is complete.</param>
    /// <param name="skipMovement">Boolean flag for skipping the move animation.</param>
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

    /// <summary>
    /// Gets all the water tiles located inside the move range of the character.
    /// </summary>
    /// <returns>Position-to-tile dictionary of water tiles.</returns>
    public Dictionary<(int, int), Tile> GetWaterTilesInRadius()
    {
        return _waterInRadius;
    }
}
