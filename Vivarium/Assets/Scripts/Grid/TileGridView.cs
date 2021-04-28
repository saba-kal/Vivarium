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
    public GameObject GridObject;
    public GridVisualSettings GridSettings;

    private Grid<Tile> _grid;
    private MeshFilter _meshFilter;
    private MeshRenderer _meshRenderer;
    private MeshCollider _meshCollider;

    private Vector3 _gridOrigin;
    private List<Vector3> _vertices = new List<Vector3>();
    private List<int> _triangles = new List<int>();
    private List<Vector2> _uv = new List<Vector2>();

    /// <summary>
    /// Creates 3D representation of the grid.
    /// </summary>
    public void CreateGridMesh(Vector3 gridOrigin)
    {
        _gridOrigin = gridOrigin;

        DestroyGridMesh();
        GetGridData();
        CreateGridObject();
        GenerateMesh();
        //CreateDebugGrid();
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

        _meshCollider = gridMeshObject.AddComponent<MeshCollider>();
        _meshFilter = gridMeshObject.AddComponent<MeshFilter>();
        _meshRenderer = gridMeshObject.AddComponent<MeshRenderer>();
        _meshRenderer.material = GridSettings.GridMaterial;
        CreateTexture();
    }

    private void GenerateMesh()
    {

        var width = _grid.GetGrid().GetLength(0);
        var height = _grid.GetGrid().GetLength(1);

        _vertices = new List<Vector3>();
        _triangles = new List<int>();
        _uv = new List<Vector2>();

        var vertexIndex = 0;
        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < height; y++)
            {
                if (_grid.GetValue(x, y).Type == TileType.Water)
                {
                    CreateWaterFloorSquares(x, y, ref vertexIndex);
                }
                else
                {
                    CreateSquare(
                        ref vertexIndex,
                        new MeshTileData
                        {
                            x = x,
                            y = y,
                            height1 = 0,
                            height2 = 0,
                            height3 = 0,
                            height4 = 0
                        });
                }
            }
        }


        var mesh = new Mesh();
        mesh.vertices = _vertices.ToArray();
        mesh.triangles = _triangles.ToArray();
        mesh.uv = _uv.ToArray();

        _meshFilter.mesh = mesh;
        _meshCollider.sharedMesh = mesh;
    }

    private void CreateWaterFloorSquares(
        int x, int y,
        ref int vertexIndex)
    {
        var waterFloorHeight = -0.5f;
        var square1 = new MeshTileData
        {
            x = x,
            y = y,
            height1 = waterFloorHeight,
            height2 = waterFloorHeight,
            height3 = waterFloorHeight,
            height4 = waterFloorHeight,
            size = 0.5f
        };
        var square2 = new MeshTileData
        {
            x = x,
            y = y + 0.5f,
            height1 = waterFloorHeight,
            height2 = waterFloorHeight,
            height3 = waterFloorHeight,
            height4 = waterFloorHeight,
            size = 0.5f
        };
        var square3 = new MeshTileData
        {
            x = x + 0.5f,
            y = y + 0.5f,
            height1 = waterFloorHeight,
            height2 = waterFloorHeight,
            height3 = waterFloorHeight,
            height4 = waterFloorHeight,
            size = 0.5f
        };
        var square4 = new MeshTileData
        {
            x = x + 0.5f,
            y = y,
            height1 = waterFloorHeight,
            height2 = waterFloorHeight,
            height3 = waterFloorHeight,
            height4 = waterFloorHeight,
            size = 0.5f
        };

        var top = _grid.GetValue(x, y + 1);
        var topRight = _grid.GetValue(x + 1, y + 1);
        var right = _grid.GetValue(x + 1, y);
        var bottomRight = _grid.GetValue(x + 1, y - 1);
        var bottom = _grid.GetValue(x, y - 1);
        var bottomLeft = _grid.GetValue(x - 1, y - 1);
        var left = _grid.GetValue(x - 1, y);
        var topLeft = _grid.GetValue(x - 1, y + 1);

        //if ((right?.Type ?? TileType.Water) == TileType.Water &&
        //    (bottom?.Type ?? TileType.Water) == TileType.Water &&
        //    (left?.Type ?? TileType.Water) == TileType.Water &&
        //    (top?.Type ?? TileType.Water) == TileType.Water)
        //{
        //    CreateSquare(ref vertexIndex, square1);
        //    return;
        //}

        if (top != null && top?.Type != TileType.Water)
        {
            square2.height2 = 0;
            square2.height3 = 0;
            square3.height2 = 0;
            square3.height3 = 0;
        }
        if (topRight != null && topRight?.Type != TileType.Water)
        {
            square3.height3 = 0;
        }
        if (right != null && right?.Type != TileType.Water)
        {
            square3.height3 = 0;
            square3.height4 = 0;
            square4.height3 = 0;
            square4.height4 = 0;
        }
        if (bottomRight != null && bottomRight?.Type != TileType.Water)
        {
            square4.height4 = 0;
        }
        if (bottom != null && bottom?.Type != TileType.Water)
        {
            square1.height1 = 0;
            square1.height4 = 0;
            square4.height1 = 0;
            square4.height4 = 0;
        }
        if (bottomLeft != null && bottomLeft?.Type != TileType.Water)
        {
            square1.height1 = 0;
        }
        if (left != null && left?.Type != TileType.Water)
        {
            square1.height1 = 0;
            square1.height2 = 0;
            square2.height1 = 0;
            square2.height2 = 0;
        }
        if (topLeft != null && topLeft?.Type != TileType.Water)
        {
            square2.height2 = 0;
        }

        CreateSquare(ref vertexIndex, square1);
        CreateSquare(ref vertexIndex, square2);
        CreateSquare(ref vertexIndex, square3);
        CreateSquare(ref vertexIndex, square4);
    }

    private void CreateSquare(
        ref int vertexIndex,
        MeshTileData tileData)
    {
        var x = tileData.x;
        var y = tileData.y;
        var width = _grid.GetGrid().GetLength(0);
        var height = _grid.GetGrid().GetLength(1);

        //4 vertices define a square.
        _vertices.Add(new Vector3(x, tileData.height1, y));
        _vertices.Add(new Vector3(x, tileData.height2, y + tileData.size));
        _vertices.Add(new Vector3(x + tileData.size, tileData.height3, y + tileData.size));
        _vertices.Add(new Vector3(x + tileData.size, tileData.height4, y));

        //uv map for texturing.
        _uv.Add(new Vector2(x / width, y / height));
        _uv.Add(new Vector2(x / width, (y + tileData.size) / height));
        _uv.Add(new Vector2((x + tileData.size) / width, (y + tileData.size) / height));
        _uv.Add(new Vector2((x + tileData.size) / width, y / height));

        //triangle 1.
        _triangles.Add(vertexIndex);
        _triangles.Add(vertexIndex + 1);
        _triangles.Add(vertexIndex + 2);

        //triangle 2.
        _triangles.Add(vertexIndex);
        _triangles.Add(vertexIndex + 2);
        _triangles.Add(vertexIndex + 3);

        vertexIndex += 4;
    }


    private void CreateTexture()
    {
        var width = _grid.GetGrid().GetLength(0);
        var height = _grid.GetGrid().GetLength(1);

        var aStar = new AStar(new List<TileType> { TileType.Grass }, true, _grid);
        var pathToObjective = aStar.Execute(_grid.GetValue(0, 0), _grid.GetValue(width - 1, height - 1))?
            .ToDictionary(tile => (tile.GridX, tile.GridY));

        if (pathToObjective == null)
        {
            return;
        }

        var texture = new Texture2D(width, height, TextureFormat.RGB24, false);
        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < height; y++)
            {
                if (pathToObjective.ContainsKey((x, y)))
                {
                    texture.SetPixel(x, y, Color.black);
                }
                else
                {
                    texture.SetPixel(x, y, Color.white);
                }
            }
        }

        texture.name = "ObjectivePathMask";
        texture.Apply();

        _meshRenderer.sharedMaterial.SetTexture(texture.name, texture);
    }

    private void CreateDebugGrid()
    {
        var width = _grid.GetGrid().GetLength(0);
        var height = _grid.GetGrid().GetLength(1);
        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < height; y++)
            {
                GameObject tileObject = null;
                switch (_grid.GetValue(x, y).Type)
                {
                    case TileType.Grass:
                        tileObject = Instantiate(GridSettings.DebugGroundTile, transform);
                        break;
                    case TileType.Water:
                        tileObject = Instantiate(GridSettings.DebugWaterTile, transform);
                        break;
                    case TileType.Obstacle:
                        tileObject = Instantiate(GridSettings.DebugWallTile, transform);
                        break;
                }

                if (tileObject != null)
                {
                    tileObject.transform.position = _grid.GetWorldPositionCentered(x, y);
                }
            }
        }
    }
}

public class MeshTileData
{
    public float x, y;
    public float height1, height2, height3, height4;
    public float size = 1f;
}

[Serializable]
public class TileDisplayInfo
{
    public TileType Type;
    public GameObject TilePrefab;
}
