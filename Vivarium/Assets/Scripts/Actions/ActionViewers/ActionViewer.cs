using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Makes the effect of a selected action visible to the player through tile highlights.
/// </summary>
public class ActionViewer : MonoBehaviour
{
    public Action ActionReference;

    protected CharacterController _characterController;

    protected bool _actionIsDisplayed = false;
    protected float _areaOfAffect;
    protected const GridHighlightRank AOE_COLOR = GridHighlightRank.Secondary;
    protected const GridHighlightRank RANGE_COLOR = GridHighlightRank.Tertiary;
    protected Dictionary<(int, int), Tile> _activeActionTiles = new Dictionary<(int, int), Tile>();

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

    /// <summary>
    /// Applies tile highlights to the tiles possibly effected by the selected action.
    /// </summary>
    /// <param name="areaOfAffect">Radius of tiles around the target tile that would also be effected. Determined by the action.</param>
    /// <param name="activeActionTiles">Dictionary containing all tiles within range of the action, along with their coordinates.</param>
    public void DisplayAction(float areaOfAffect, Dictionary<(int, int), Tile> activeActionTiles)
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

    /// <summary>
    /// Removes the tile highlights related to an action when it is deselected
    /// </summary>
    public void HideAction()
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
        if (mouseHoverTile == null || !ActionIsWithinRange(mouseHoverTile))
        {
            TileGridController.Instance.RemoveHighlights(AOE_COLOR);
            return;
        }

        TileGridController.Instance.HighlightRadius(
            mouseHoverTile.GridX,
            mouseHoverTile.GridY,
            0,
            _areaOfAffect,
            AOE_COLOR);
    }

    /// <summary>
    /// Checks if a tile is within range of the selected action.
    /// </summary>
    /// <param name="targetTile">The tile that is being checked.</param>
    /// <returns>Returns true if tile is in range, otherwise returns false.</returns>
    public bool ActionIsWithinRange(Tile targetTile)
    {
        return _activeActionTiles.ContainsKey((targetTile.GridX, targetTile.GridY));
    }

    protected virtual void ShowActionRange()
    {
        TileGridController.Instance.HighlightTiles(_activeActionTiles, RANGE_COLOR);
    }

    /// <summary>
    /// Gets the tiles that are within range of the currently selected action.
    /// </summary>
    /// <returns>Dictionary of these tiles, along with their coordinates.</returns>
    public Dictionary<(int, int), Tile> GetAffectedTiles()
    {
        return _activeActionTiles;
    }
}
