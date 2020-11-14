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

        var endTile = affectedTiles.LastOrDefault();
        if (ActionReference.ProjectilePrefab != null && !endTile.Equals(default(KeyValuePair<(int, int), Tile>)))
        {
            var startPosition = _grid.GetWorldPositionCentered(startTile.GridX, startTile.GridY);
            var endPosition = _grid.GetWorldPositionCentered(endTile.Value.GridX, endTile.Value.GridY);
            StartCoroutine(AnimateProjectile(ActionReference.ProjectilePrefab, startPosition, endPosition));
        }

        ExecuteAction(affectedTiles);
    }

    private IEnumerator AnimateProjectile(GameObject projectilePrefab, Vector3 startPosition, Vector3 endPosition)
    {
        var projectile = Instantiate(projectilePrefab);
        projectile.transform.position = startPosition;

        while (projectile.transform.position != endPosition)
        {
            projectile.transform.position = Vector3.MoveTowards(projectile.transform.position, endPosition, Time.deltaTime * Constants.PROJECTILE_SPEED);
            yield return null;
        }

        Destroy(projectile);
        yield return null;
    }
}
