using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProjectileActionViewer : ActionViewer
{

    protected override void ShowActionRange()
    {
        var gridController = TileGridController.Instance;
        var grid = gridController.GetGrid();

        var startTile = grid.GetValue(transform.position);
        var tilesInRange = gridController.GetTilesInRadius(startTile.GridX, startTile.GridY, _minRange, _maxRange);

        var tilesProjectileCanHit = new Dictionary<(int, int), Tile>();

        foreach (var tile in tilesInRange.Values)
        {
            var line = grid.GetLine(
                grid.GetWorldPosition(startTile.GridX, startTile.GridY),
                grid.GetWorldPosition(tile.GridX, tile.GridY));

            foreach (var lineTile in line)
            {
                var projectileIsBlocked = (lineTile.Type == TileType.Obstacle ||
                    !string.IsNullOrEmpty(lineTile.CharacterControllerId)) &&
                    !lineTile.Equals(startTile);

                var projectileCanHitTile =
                    !tilesProjectileCanHit.ContainsKey((lineTile.GridX, lineTile.GridY)) &&
                    tilesInRange.ContainsKey((lineTile.GridX, lineTile.GridY));

                if (projectileIsBlocked)
                {
                    if (!string.IsNullOrEmpty(lineTile.CharacterControllerId) && projectileCanHitTile)
                    {
                        tilesProjectileCanHit.Add((lineTile.GridX, lineTile.GridY), lineTile);
                    }

                    break;
                }

                if (!lineTile.Equals(startTile) && projectileCanHitTile)
                {
                    tilesProjectileCanHit.Add((lineTile.GridX, lineTile.GridY), lineTile);
                }
            }
        }

        _activeActionTiles = tilesProjectileCanHit;
        gridController.HighlightTiles(tilesProjectileCanHit, RANGE_COLOR);
    }
}
