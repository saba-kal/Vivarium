using System;

[Serializable]
public class Tile
{
    private string _characterControllerId;
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
    public TileType Type
    {
        get { return _type; }
        set
        {
            _type = value;
            _grid.TriggerGridCellChange(GridX, GridY);
        }
    }

    public int GridX { get; private set; }
    public int GridY { get; private set; }

    private bool _isObjective;
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
    public TileSpawnType SpawnType
    {
        get { return _spawnType; }
        set
        {
            _spawnType = value;
            _grid.TriggerGridCellChange(GridX, GridY);
        }
    }

    private Grid<Tile> _grid;

    public Tile(int x, int y, Grid<Tile> grid)
    {
        GridX = x;
        GridY = y;
        _grid = grid;
    }
}
