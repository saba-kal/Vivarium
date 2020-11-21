﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActionViewer : MonoBehaviour
{
    public Action ActionReference;

    protected CharacterController _characterController;

    protected bool _actionIsDisplayed = false;
    protected float _areaOfAffect;
    protected float _minRange;
    protected float _maxRange;
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

    public void DisplayAction(float areaOfAffect, float ActionMinRange, float ActionMaxRange)
    {
        if (_characterController == null)
        {
            _characterController = GetComponent<CharacterController>();
        }

        _areaOfAffect = areaOfAffect;
        _minRange = ActionMinRange;
        _maxRange = ActionMaxRange;
        _actionIsDisplayed = true;
        ShowActionAoeAroundMouse();
        ShowActionRange();
    }

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

    public bool ActionIsWithinRange(Tile targetTile)
    {
        return _activeActionTiles.ContainsKey((targetTile.GridX, targetTile.GridY));
    }

    protected virtual void ShowActionRange()
    {
        TileGridController.Instance.GetGrid().GetGridCoordinates(_characterController.transform.position, out var x, out var y);
        _activeActionTiles = TileGridController.Instance.HighlightRadius(x, y, _minRange, _maxRange, RANGE_COLOR);
    }
}
