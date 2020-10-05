using System;

[Serializable]
public class Tile
{
    private int? _characterControllerId;
    public int? CharacterControllerId
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


    private Grid<Tile> _grid;

    public Tile(int x, int y, Grid<Tile> grid)
    {
        GridX = x;
        GridY = y;
        _grid = grid;
    }
}
