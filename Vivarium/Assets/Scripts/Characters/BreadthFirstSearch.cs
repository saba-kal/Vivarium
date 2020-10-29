using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

public class BreadthFirstSearch
{
    private Tile[,] _grid;
    private BFSTile[,] _bfsGrid;
    private Dictionary<(int, int), Tile> _visitedTiles;
    private readonly bool _ignoreCharacters = false;

    public BreadthFirstSearch(Grid<Tile> grid = null, bool ignoreCharacters = false)
    {
        Reset(grid);
        _ignoreCharacters = ignoreCharacters;
    }

    public void Execute(Tile startingTile, int maxSteps, List<TileType> navigableTiles)
    {
        var queue = new Queue<Tile>();

        //Visit the starting tile.
        _bfsGrid[startingTile.GridX, startingTile.GridY].Visited = true;
        _bfsGrid[startingTile.GridX, startingTile.GridY].Path.Add(startingTile);
        queue.Enqueue(startingTile);

        while (queue.Count > 0)
        {
            var tile = queue.Dequeue();

            var adjacentPositions = new List<(int, int)> {
                (tile.GridX + 1, tile.GridY), //Right
                (tile.GridX - 1, tile.GridY), //Left
                (tile.GridX, tile.GridY + 1), //Up
                (tile.GridX, tile.GridY - 1), //Down
            };

            //Iterate through adjacent tiles and check if they can be visited.
            foreach (var position in adjacentPositions)
            {
                if (TileCanBeVisited(position.Item1, position.Item2, navigableTiles))
                {
                    var tileWasVisited = VisitTile(position.Item1, position.Item2, queue, tile, _grid[position.Item1, position.Item2], maxSteps);
                    if (tileWasVisited)
                    {
                        //TODO: Figure out why this if statement is here and maybe make use of it.
                    }
                }
            }
        }
    }

    private bool TileCanBeVisited(int x, int y, List<TileType> navigableTiles)
    {
        //x and y must be within the grid range.
        //The tile must not have been visited previously.
        //The tile terrain type must be navigable.
        //Another character must not be on the tile, unless we are ignoring characters.
        return x >= 0 && y >= 0 &&
            x < _bfsGrid.GetLength(0) && y < _bfsGrid.GetLength(1) &&
            !_bfsGrid[x, y].Visited &&
            navigableTiles.Contains(_grid[x, y].Type) &&
            (_ignoreCharacters || string.IsNullOrEmpty(_grid[x, y].CharacterControllerId));
    }

    private bool VisitTile(int x, int y, Queue<Tile> queue, Tile fromTile, Tile toTile, int maxSteps)
    {
        //Calculate the number of steps it takes to get to the tile.
        _bfsGrid[x, y].Steps = _bfsGrid[fromTile.GridX, fromTile.GridY].Steps + 1;

        //Number of steps must be less than the character's move range.
        if (_bfsGrid[x, y].Steps < maxSteps)
        {
            _bfsGrid[x, y].Visited = true;
            //Store the path it takes to get to the tile. This will later be used to move the character object.
            _bfsGrid[x, y].Path = _bfsGrid[fromTile.GridX, fromTile.GridY].Path.Concat(new List<Tile> { toTile }).ToList();
            queue.Enqueue(toTile);
            _visitedTiles.Add((x, y), toTile);
            return true;
        }

        return false;
    }

    public List<Tile> GetPathToTile(Tile toTile)
    {
        return _bfsGrid[toTile.GridX, toTile.GridY].Path;
    }

    public Dictionary<(int, int), Tile> GetVisitedTiles()
    {
        return _visitedTiles;
    }

    public void Reset(Grid<Tile> grid = null)
    {
        if (grid != null)
        {
            _grid = grid.GetGrid();
        }
        else
        {
            _grid = TileGridController.Instance.GetGrid().GetGrid();
        }
        _bfsGrid = new BFSTile[_grid.GetLength(0), _grid.GetLength(1)];
        for (var x = 0; x < _grid.GetLength(0); x++)
        {
            for (var y = 0; y < _grid.GetLength(1); y++)
            {
                _bfsGrid[x, y] = new BFSTile();
            }
        }
        _visitedTiles = new Dictionary<(int, int), Tile>();
    }
}

public class BFSTile
{
    public bool Visited = false;
    public int Steps = 0;
    public List<Tile> Path = new List<Tile>();
}
