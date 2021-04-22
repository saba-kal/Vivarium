using UnityEngine;
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
    public GameObject GridObject;
    public Material GridMaterial;

    private Grid<Tile> _grid;
    private MeshFilter _meshFilter;
    private MeshRenderer _meshRenderer;
    private Vector3 _gridOrigin;

    /// <summary>
    /// Creates 3D representation of the grid.
    /// </summary>
    public void CreateGridMesh(Vector3 gridOrigin)
    {
        _gridOrigin = gridOrigin;

        DestroyGridMesh();
        GetGridData();
        //CreateTiles();
        CreateGridObject();
        GenerateMesh();
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


    private void CreateGridObject()
    {
        var gridMeshObject = new GameObject("GridMesh");
        gridMeshObject.transform.SetParent(transform);
        gridMeshObject.transform.position = _gridOrigin;

        _meshFilter = gridMeshObject.AddComponent<MeshFilter>();
        _meshRenderer = gridMeshObject.AddComponent<MeshRenderer>();
    }

    private void GenerateMesh()
    {
        var vertices = new List<Vector3>();
        var triangles = new List<int>();

        var width = _grid.GetGrid().GetLength(0);
        var height = _grid.GetGrid().GetLength(1);
        //width = 2;
        //height = 2;

        var vertexIndex = 0;
        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < height; y++)
            {
                if (_grid.GetValue(x, y).Type == TileType.Water)
                {
                    continue;
                }

                //4 vertices define a square.
                vertices.Add(new Vector3(x, 0, y));
                vertices.Add(new Vector3(x, 0, y + 1));
                vertices.Add(new Vector3(x + 1, 0, y + 1));
                vertices.Add(new Vector3(x + 1, 0, y));

                //triangle 1.
                triangles.Add(vertexIndex);
                triangles.Add(vertexIndex + 1);
                triangles.Add(vertexIndex + 2);

                //triangle 2.
                triangles.Add(vertexIndex);
                triangles.Add(vertexIndex + 2);
                triangles.Add(vertexIndex + 3);

                vertexIndex += 4;
            }
        }


        var mesh = new Mesh();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();

        _meshFilter.mesh = mesh;
    }
}

[Serializable]
public class TileDisplayInfo
{
    public TileType Type;
    public GameObject TilePrefab;
}
