using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;

public class ProjectileActionController : ActionController
{
    public Vector3 ProjectileStartPosition;
    public Transform ProjectileTransform;

    private bool _skipAnimation = false;

    public override void Execute(Tile targetTile, System.Action onActionComplete = null)
    {
        if (_characterController == null)
        {
            _characterController = GetComponent<CharacterController>();
        }

        var grid = TileGridController.Instance.GetGrid();
        Tile startTile = grid.GetValue(transform.position);
        if (startTile == null || targetTile == null)
        {
            onActionComplete?.Invoke();
            return;
        }

        CommandController.Instance.ExecuteCommand(
            new MakeCharacterFaceTileCommand(
                _characterController,
                targetTile,
                true));

        var areaOfAffect = StatCalculator.CalculateStat(ActionReference, StatType.AttackAOE);
        var affectedTiles = TileGridController.Instance.GetTilesInRadius(targetTile.GridX, targetTile.GridY, 0, areaOfAffect);

        if (ActionReference.ProjectilePrefab != null)
        {
            PerformAnimation();
            _skipAnimation = true;

            var startPosition = grid.GetWorldPositionCentered(startTile.GridX, startTile.GridY);
            var endPosition = grid.GetWorldPositionCentered(targetTile.GridX, targetTile.GridY);
            CommandController.Instance.ExecuteCoroutine(AnimateProjectile(ActionReference.ProjectilePrefab, startPosition, endPosition, () =>
            {
                ExecuteAction(affectedTiles);
                _skipAnimation = false;

                onActionComplete?.Invoke();
            }));
        }

        PlaySound();
    }

    private IEnumerator AnimateProjectile(GameObject projectilePrefab, Vector3 startPosition, Vector3 endPosition, System.Action onActionComplete = null)
    {
        yield return new WaitForSeconds(ActionReference.ActionTriggerDelay);

        var projectile = Instantiate(projectilePrefab);
        projectile.transform.position = startPosition;

        while (projectile.transform.position != endPosition)
        {
            projectile.transform.position = Vector3.MoveTowards(projectile.transform.position, endPosition, Time.deltaTime * Constants.PROJECTILE_SPEED);
            yield return null;
        }

        Destroy(projectile);
        onActionComplete?.Invoke();
        yield return null;
    }

    public override void CalculateAffectedTiles(int x, int y)
    {
        var gridController = TileGridController.Instance;
        var grid = gridController.GetGrid();

        var startTile = grid.GetValue(x, y);
        var minRange = StatCalculator.CalculateStat(ActionReference, StatType.AttackMinRange);
        var maxRange = StatCalculator.CalculateStat(ActionReference, StatType.AttackMaxRange);
        var tilesInRange = gridController.GetTilesInRadius(startTile.GridX, startTile.GridY, minRange, maxRange);

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

        _tilesActionCanAffect = tilesProjectileCanHit;
    }

    protected override void PerformAnimation()
    {
        if (_skipAnimation)
        {
            return;
        }

        base.PerformAnimation();
    }
}
