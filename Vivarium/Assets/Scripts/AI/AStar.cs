using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;

public class AStar
{
    private readonly Grid<PathNode> _grid;
    private List<TileType> _navigableTiles;
    private bool _ignoreCharacters;
    private int _waterTileCost = 0;

    private List<PathNode> _openNodes;
    private List<PathNode> _closedNodes;

    public AStar(
        List<TileType> navigableTiles,
        bool ignoreCharacters = false,
        Grid<Tile> grid = null)
    {
        var tileGrid = grid ?? TileGridController.Instance.GetGrid();
        _grid = new Grid<PathNode>(
            tileGrid.GetGrid().GetLength(0),
            tileGrid.GetGrid().GetLength(1),
            1,
            Vector3.zero,
            (int x, int y, Grid<PathNode> unused) =>
            {
                return new PathNode
                {
                    GridTile = tileGrid.GetValue(x, y),
                    GCost = float.MaxValue
                };
            });

        _navigableTiles = navigableTiles;
        _ignoreCharacters = ignoreCharacters;
    }

    public List<Tile> Execute(Tile startTile, Tile endTile)
    {
        _openNodes = new List<PathNode> { _grid.GetValue(startTile.GridX, startTile.GridY) };
        _closedNodes = new List<PathNode>();

        for (int x = 0; x < _grid.GetGrid().GetLength(0); x++)
        {
            for (int y = 0; y < _grid.GetGrid().GetLength(0); y++)
            {
                var node = _grid.GetValue(x, y);
                node.GCost = int.MaxValue;
                node.FCost = node.HCost + node.GCost;
                node.PreviousNode = null;
            }
        }

        _openNodes[0].GCost = 0;
        _openNodes[0].HCost = GetDistance(startTile, endTile);
        _openNodes[0].FCost = _openNodes[0].HCost + _openNodes[0].GCost;

        while (_openNodes.Count > 0)
        {
            var currentNode = GetLowestCostNode(_openNodes);

            if (currentNode.GridTile == endTile)
            {
                return GetPath(_grid.GetValue(endTile.GridX, endTile.GridY));
            }

            _openNodes.Remove(currentNode);
            _closedNodes.Add(currentNode);

            foreach (var neighborNode in GetNeighbors(currentNode))
            {
                if (_closedNodes.Contains(neighborNode))
                {
                    continue;
                }

                var tentativeGCost = currentNode.GCost + GetDistance(currentNode.GridTile, neighborNode.GridTile);
                if (tentativeGCost < neighborNode.GCost)
                {
                    neighborNode.PreviousNode = currentNode;
                    neighborNode.GCost = tentativeGCost;
                    if (neighborNode.GridTile.Type == TileType.Water)
                    {
                        neighborNode.GCost += _waterTileCost;
                    }

                    neighborNode.HCost = GetDistance(neighborNode.GridTile, endTile);
                    neighborNode.FCost = neighborNode.HCost + neighborNode.GCost;

                    if (!_openNodes.Contains(neighborNode))
                    {
                        _openNodes.Add(neighborNode);
                    }
                }
            }
        }

        //Could not find path.
        return null;
    }

    private float GetDistance(Tile startTile, Tile endTile)
    {
        return Vector2.Distance(
            new Vector2(startTile.GridX, startTile.GridY),
            new Vector2(endTile.GridX, endTile.GridY));
    }

    private PathNode GetLowestCostNode(List<PathNode> nodes)
    {
        var lowestCostNode = nodes.First();
        foreach (var node in nodes)
        {
            if (node.FCost < lowestCostNode.FCost)
            {
                lowestCostNode = node;
            }
        }

        return lowestCostNode;
    }

    private List<Tile> GetPath(PathNode endNode)
    {
        if (endNode == null)
        {
            return null;
        }

        var path = new List<Tile> { endNode.GridTile };
        var previousNode = endNode.PreviousNode;
        while (previousNode != null)
        {
            path.Insert(0, previousNode.GridTile);
            previousNode = previousNode.PreviousNode;
        }

        return path;
    }

    private List<PathNode> GetNeighbors(PathNode node)
    {
        var adjacentNodes = new[]{
            _grid.GetValue(node.GridTile.GridX + 1, node.GridTile.GridY),
            _grid.GetValue(node.GridTile.GridX - 1, node.GridTile.GridY),
            _grid.GetValue(node.GridTile.GridX, node.GridTile.GridY + 1),
            _grid.GetValue(node.GridTile.GridX, node.GridTile.GridY - 1)};

        var neighbors = new List<PathNode>();
        foreach (var adjacentNode in adjacentNodes)
        {
            if (adjacentNode != null &&
                _navigableTiles.Contains(adjacentNode.GridTile.Type) &&
                (_ignoreCharacters || string.IsNullOrEmpty(adjacentNode.GridTile.CharacterControllerId)))
            {
                neighbors.Add(adjacentNode);
            }
        }

        return neighbors;
    }

    public void SetNavigableTiles(List<TileType> navigableTiles)
    {
        _navigableTiles = navigableTiles;
    }

    public void SetIgnoreCharacters(bool ignoreCharacters)
    {
        _ignoreCharacters = ignoreCharacters;
    }

    public void SetWaterTileCost(int cost)
    {
        _waterTileCost = cost;
    }
}

public class PathNode
{
    public Tile GridTile { get; set; }

    public float GCost { get; set; }
    public float HCost { get; set; }
    public float FCost { get; set; }

    public PathNode PreviousNode { get; set; }

    public static PathNode FromTile(Tile tile)
    {
        return new PathNode
        {
            GridTile = tile,
            GCost = float.MaxValue
        };
    }
}