using System;

/// <summary>
/// Represents data on each cell of the grid.
/// </summary>
[Serializable]
public class Tile
{
    private string _characterControllerId;
    /// <summary>
    /// The ID of the character standing on the tile. Null/empty if no character is on tile.
    /// </summary>
    public string CharacterControllerId
    {
        get { return _characterControllerId; }
        set
        {
            _characterControllerId = value;
            _grid.TriggerGridCellChange(GridX, GridY);
        }
    }

    private string _name;
    /// <summary>
    /// Name of the grid tile.
    /// </summary>
    public string Name
    {
        get { return _name; }
        set
        {
            _name = value;
            _grid.TriggerGridCellChange(GridX, GridY);
        }
    }

    private TileType _type;
    /// <summary>
    /// The terrain type of the tile.
    /// </summary>
    public TileType Type
    {
        get { return _type; }
        set
        {
            _type = value;
            _grid.TriggerGridCellChange(GridX, GridY);
        }
    }

    /// <summary>
    /// The x coordinate of the tile.
    /// </summary>
    public int GridX { get; private set; }

    /// <summary>
    /// The y coordinate of the tile.
    /// </summary>
    public int GridY { get; private set; }

    private bool _isObjective;
    /// <summary>
    /// True if this tile is the objective that players must reach.
    /// </summary>
    public bool IsObjective
    {
        get { return _isObjective; }
        set
        {
            _isObjective = value;
            _grid.TriggerGridCellChange(GridX, GridY);
        }
    }


    private TileSpawnType _spawnType;
    /// <summary>
    /// Type of entity that this grid cell can spawn.
    /// </summary>
    public TileSpawnType SpawnType
    {
        get { return _spawnType; }
        set
        {
            _spawnType = value;
            _grid.TriggerGridCellChange(GridX, GridY);
        }
    }

    private int _points;
    /// <summary>
    /// How much AI values this tile.
    /// </summary>
    public int Points
    {
        get { return _points; }
        set
        {
            _points = value;
            _grid.TriggerGridCellChange(GridX, GridY);
        }
    }

    private Grid<Tile> _grid;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="x">X coordinate of the tile.</param>
    /// <param name="y">Y coordinate of the tile.</param>
    /// <param name="grid">The grid that the tile belongs to.</param>
    public Tile(int x, int y, Grid<Tile> grid)
    {
        GridX = x;
        GridY = y;
        _grid = grid;
    }
}
