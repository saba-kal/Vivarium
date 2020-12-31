using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
        var moveRadius = StatCalculator.CalculateStat(_characterController.Character, StatType.MoveRadius);
        var tile = _grid.GetValue(transform.position);
        _breadthFirstSearch.Execute(tile, Mathf.FloorToInt(moveRadius), _characterController.Character.NavigableTiles);
        _availableMoves = _breadthFirstSearch.GetVisitedTiles();
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
        //CommandController.Instance.ExecuteCommand(
        //    new MoveCommand(
        //        gameObject,
        //        _breadthFirstSearch.GetPathToTile(toTile),
        //        Constants.CHAR_MOVE_SPEED,
        //        onMoveComplete));
        //toTile.CharacterControllerId = _characterController.Id;

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

    public virtual void MoveToTile(Tile fromTile, Tile toTile, float newSpeed = Constants.CHAR_MOVE_SPEED, System.Action onMoveComplete = null)
    {
        if (fromTile == null || toTile == null)
        {
            return;
        }

        fromTile.CharacterControllerId = null;
        //CommandController.Instance.ExecuteCommand(
        //    new MoveCommand(
        //        gameObject,
        //        _breadthFirstSearch.GetPathToTile(toTile),
        //        Constants.CHAR_MOVE_SPEED,
        //        onMoveComplete));
        //toTile.CharacterControllerId = _characterController.Id;

        CommandController.Instance.ExecuteCommand(
            new MoveCommand(
                gameObject,
                _breadthFirstSearch.GetPathToTile(toTile),
                newSpeed,
                onMoveComplete));
        toTile.CharacterControllerId = _characterController.Id;
    }
}
