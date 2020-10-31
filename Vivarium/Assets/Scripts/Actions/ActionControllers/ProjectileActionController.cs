using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;

public class ProjectileActionController : ActionController
{
    public Vector3 ProjectileStartPosition;
    public Transform ProjectileTransform;

    public override void Execute(Tile targetTile)
    {
        Tile startTile = _grid.GetValue(transform.position);
        if (startTile == null || targetTile == null)
        {
            return;
        }

        var line = _grid.GetLine(
            _grid.GetWorldPosition(startTile.GridX, startTile.GridY),
            _grid.GetWorldPosition(targetTile.GridX, targetTile.GridY));

        var affectedTiles = new Dictionary<(int, int), Tile>();
        foreach (var tile in line)
        {
            //UnityEngine.Debug.Log(tile.CharacterControllerId);
            if (!tile.Equals(startTile))
            {
                affectedTiles.Add((tile.GridX, tile.GridY), tile);
                if (tile.Type == TileType.Obstacle || !string.IsNullOrEmpty(tile.CharacterControllerId))
                {
                    break;
                }
            }
        }

        ExecuteAction(affectedTiles);
    }
}
