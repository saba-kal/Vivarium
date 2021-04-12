﻿using UnityEngine;
using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;

/// <summary>
/// Creates the visual game objects that represent the grid.
/// </summary>
public class TileGridView : MonoBehaviour
{
    public TileGridController GridController;
    public List<TileDisplayInfo> TileInfos;
    public GameObject LevelObjectivePrefab;

    private Grid<Tile> _grid;

    /// <summary>
    /// Creates 3D representation of the grid.
    /// </summary>
    public void CreateGridMesh()
    {
        DestroyGridMesh();
        GetGridData();
        CreateTiles();
    }

    private void GetGridData()
    {
        if (GridController != null && GridController.GridIsGenerated())
        {
            _grid = GridController.GetGrid();
        }
        else
        {
            Debug.LogError("The tile grid view is missing a reference to the grid controller or the grid is not yet generated.");
        }
    }

    private void CreateTiles()
    {
        if (TileInfos.Count == 0)
        {
            Debug.LogError($"There are no prefabs that can be used to generate the grids. Ensure that the TileInfos field is not empty.");
            return;
        }

        var gridArray = _grid.GetGrid();
        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                var tileInfo = TileInfos.FirstOrDefault(t => t.Type == gridArray[x, y].Type);
                if (tileInfo == null)
                {
                    Debug.LogError($"Unable to find tile of type {gridArray[x, y].Type}");
                    return;
                }

                var tileObject = Instantiate(tileInfo.TilePrefab, transform);
                tileObject.transform.position = _grid.GetWorldPositionCentered(x, y);

                if (_grid.GetValue(x, y).IsObjective)
                {
                    if (LevelObjectivePrefab != null)
                    {
                        var objectiveWorldPosition = _grid.GetWorldPositionCentered(x, y);
                        var objective = Instantiate(LevelObjectivePrefab, transform);
                        objective.transform.position = objectiveWorldPosition;
                    }
                    else
                    {
                        Debug.LogError("Unable to spawn objective model because the prefab is null.");
                    }
                }
            }
        }
    }

    /// <summary>
    /// Destroys all the game objects created by this class.
    /// </summary>
    public void DestroyGridMesh()
    {
        var tiles = GameObject.FindGameObjectsWithTag(Constants.TILE_GRID_TAG);
        foreach (var tile in tiles)
        {
            DestroyImmediate(tile);
        }
    }
}

[Serializable]
public class TileDisplayInfo
{
    public TileType Type;
    public GameObject TilePrefab;
}
