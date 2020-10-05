using UnityEngine;
using System.Collections;

public class GridGenerator
{

    public static Grid<Tile> Generate(int width, int height, float cellSize, Vector3 origin)
    {
        return new Grid<Tile>(
            width,
            height,
            cellSize,
            origin,
            (int x, int y, Grid<Tile> grid) =>
            {
                var tile = new Tile(x, y, grid);
                var randomValue = Random.Range(0f, 1f);
                if (randomValue < 0.05f)
                {
                    tile.Type = TileType.Obstacle;
                }
                else if (randomValue < 0.1f)
                {
                    tile.Type = TileType.Water;
                }
                else
                {
                    tile.Type = TileType.Grass;
                }
                return tile;
            });
    }
}
