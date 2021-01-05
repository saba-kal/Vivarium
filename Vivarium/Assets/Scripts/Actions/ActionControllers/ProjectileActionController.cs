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
        if (_characterController == null)
        {
            _characterController = GetComponent<CharacterController>();
        }

        var grid = TileGridController.Instance.GetGrid();
        Tile startTile = grid.GetValue(transform.position);
        if (startTile == null || targetTile == null)
        {
            return;
        }

        CommandController.Instance.ExecuteCommand(
            new MakeCharacterFaceTileCommand(
                _characterController,
                targetTile,
                true));

        var line = grid.GetLine(
            grid.GetWorldPosition(startTile.GridX, startTile.GridY),
            grid.GetWorldPosition(targetTile.GridX, targetTile.GridY));

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

        if (!affectedTiles.ContainsKey((targetTile.GridX, targetTile.GridY)))
        {
            return;
        }

        var endTile = affectedTiles.LastOrDefault();
        if (ActionReference.ProjectilePrefab != null && !endTile.Equals(default(KeyValuePair<(int, int), Tile>)))
        {
            var startPosition = grid.GetWorldPositionCentered(startTile.GridX, startTile.GridY);
            var endPosition = grid.GetWorldPositionCentered(endTile.Value.GridX, endTile.Value.GridY);
            StartCoroutine(AnimateProjectile(ActionReference.ProjectilePrefab, startPosition, endPosition));
        }

        PlaySound();
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
