using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// <see cref="ActionViewer"/> Action viewer specifically for the Pierce Action.
///  Required to the uniqueness in tiles affected by this action.
/// </summary>
public class PierceActionViewer : ActionViewer
{
    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (_actionIsDisplayed)
        {
            MoveActionAoeOverMouse();
        }
    }

    /// <inheritdoc cref="ActionViewer.DisplayAction(float, Dictionary{(int, int), Tile})"/>
    new public void DisplayAction(float areaOfAffect, Dictionary<(int, int), Tile> activeActionTiles)
    {
        if (_characterController == null)
        {
            _characterController = GetComponent<CharacterController>();
        }

        _activeActionTiles = activeActionTiles;
        _areaOfAffect = areaOfAffect;
        _actionIsDisplayed = true;
        ShowActionAoeAroundMouse();
        ShowActionRange();
    }

    /// <inheritdoc cref="ActionViewer.HideAction"/>
    new public void HideAction()
    {
        _actionIsDisplayed = false;
        TileGridController.Instance.RemoveHighlights(AOE_COLOR);
        TileGridController.Instance.RemoveHighlights(RANGE_COLOR);
        _activeActionTiles = new Dictionary<(int, int), Tile>();
    }

    private void MoveActionAoeOverMouse()
    {
        if (_actionIsDisplayed)
        {
            TileGridController.Instance.RemoveHighlights(AOE_COLOR);
            ShowActionAoeAroundMouse();
        }
    }
    private void ShowActionAoeAroundMouse()
    {
        var mouseHoverTile = TileGridController.Instance.GetMouseHoverTile();
        var characterGridTile = _characterController.GetGridPosition();
        if (mouseHoverTile == null || !ActionIsWithinRange(mouseHoverTile))
        {
            TileGridController.Instance.RemoveHighlights(AOE_COLOR);
            return;
        }

        TileGridController.Instance.HighlightColumn(
            mouseHoverTile.GridX,
            mouseHoverTile.GridY,
            0,
            _areaOfAffect,
            AOE_COLOR,
            characterGridTile.GridX,
            characterGridTile.GridY);
    }

    /// <inheritdoc cref="ActionViewer.ActionIsWithinRange(Tile)"/>
    new public bool ActionIsWithinRange(Tile targetTile)
    {
        return _activeActionTiles.ContainsKey((targetTile.GridX, targetTile.GridY));
    }

    protected override void ShowActionRange()
    {
        TileGridController.Instance.HighlightTiles(_activeActionTiles, RANGE_COLOR);
    }

    /// <inheritdoc cref="ActionViewer.GetAffectedTiles"/>
    new public Dictionary<(int, int), Tile> GetAffectedTiles()
    {
        return _activeActionTiles;
    }
}