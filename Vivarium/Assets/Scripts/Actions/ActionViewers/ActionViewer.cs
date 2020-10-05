using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActionViewer : MonoBehaviour
{
    public Action ActionReference;

    private CharacterController _characterController;

    private bool _actionIsDisplayed = false;
    private float _areaOfAffect;
    private float _range;
    private const GridHighlightRank AOE_COLOR = GridHighlightRank.Secondary;
    private const GridHighlightRank RANGE_COLOR = GridHighlightRank.Tertiary;

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

    public void DisplayAction(float areaOfAffect, float ActionRange)
    {
        _areaOfAffect = areaOfAffect;
        _range = ActionRange;
        _actionIsDisplayed = true;
        ShowActionAoeAroundMouse();
        ShowActionRange();
    }

    public void HideAction()
    {
        _actionIsDisplayed = false;
        TileGridController.Instance.RemoveHighlights(AOE_COLOR);
        TileGridController.Instance.RemoveHighlights(RANGE_COLOR);
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
            _areaOfAffect,
            AOE_COLOR);
    }

    private bool ActionIsWithinRange(Tile targetTile)
    {
        var targetPosition = TileGridController.Instance.GetGrid().GetWorldPositionCentered(targetTile.GridX, targetTile.GridY);
        return Vector3.Distance(_characterController.transform.position, targetPosition) < _range + 0.01f;
    }

    private void ShowActionRange()
    {
        TileGridController.Instance.GetGrid().GetGridCoordinates(_characterController.transform.position, out var x, out var y);
        TileGridController.Instance.HighlightRadius(x, y, _range, RANGE_COLOR);
    }
}
