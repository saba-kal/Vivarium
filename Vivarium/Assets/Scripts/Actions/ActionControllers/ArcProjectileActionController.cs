using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ArcProjectileActionController : ActionController
{
    public bool SkipCommandQueue = false;
    public override void Execute(Tile targetTile, System.Action onActionComplete = null)
    {
        if (_characterController == null)
        {
            _characterController = GetComponent<CharacterController>();
        }

        _delay = 0;
        var areaOfAffect = StatCalculator.CalculateStat(ActionReference, StatType.AttackAOE);
        var affectedTiles = TileGridController.Instance.GetTilesInRadius(targetTile.GridX, targetTile.GridY, 0, areaOfAffect);

        CommandController.Instance.ExecuteCommand(
            new MakeCharacterFaceTileCommand(
                _characterController,
                targetTile,
                true));

        PlaySound();

        var endPosition = TileGridController.Instance.GetGrid().GetWorldPositionCentered(targetTile.GridX, targetTile.GridY);

        this.PerformAnimation();

        if (SkipCommandQueue)
        {
            StartCoroutine(AnimateProjectile(ActionReference.ProjectilePrefab, transform.position, endPosition, () =>
            {
                this.ExecuteAction(affectedTiles);
            }));
        }
        else
        {
            CommandController.Instance.ExecuteCoroutine(AnimateProjectile(ActionReference.ProjectilePrefab, transform.position, endPosition, () =>
            {
                this.ExecuteAction(affectedTiles);
            }));
        }

        onActionComplete?.Invoke();
    }

    protected override void ExecuteAction(Dictionary<(int, int), Tile> affectedTiles)
    {
        var targetCharacterIds = new List<string>();

        foreach (var tile in affectedTiles)
        {
            if (!string.IsNullOrEmpty(tile.Value.CharacterControllerId))
            {
                targetCharacterIds.Add(tile.Value.CharacterControllerId);
            }
        }

        GenerateParticlesOnTiles(affectedTiles);

        var targetCharacters = TurnSystemManager.Instance.GetCharacterWithIds(targetCharacterIds, GetTargetType());
        CommandController.Instance.ExecuteCoroutine(ExecuteAction(targetCharacters, affectedTiles));
    }

    private IEnumerator AnimateProjectile(GameObject projectilePrefab, Vector3 startPosition, Vector3 endPosition, System.Action onComplete)
    {
        yield return new WaitForSeconds(ActionReference.ActionTriggerDelay);

        var projectile = Instantiate(projectilePrefab);
        projectile.transform.position = startPosition;

        if (startPosition == endPosition)
        {
            endPosition.x += 0.1f;
            endPosition.z += 0.1f;
        }

        var distance = Vector3.Distance(startPosition, endPosition);
        var height = Constants.PROJECTILE_HEIGHT * distance;
        var time = 0f;
        var timeMultiplier = 0.5f / distance * Constants.PROJECTILE_SPEED;

        while (projectile.transform.position.y >= endPosition.y - 0.1f)
        {
            projectile.transform.position = Parabola(startPosition, endPosition, height, time * timeMultiplier);
            time += Time.deltaTime;
            yield return null;
        }

        Destroy(projectile);
        onComplete();
        yield return null;
    }

    /// <summary>
    /// Source: https://gist.github.com/ditzel/68be36987d8e7c83d48f497294c66e08
    /// </summary>
    private Vector3 Parabola(Vector3 start, Vector3 end, float height, float t)
    {
        System.Func<float, float> f = x => -4 * height * x * x + 4 * height * x;

        var mid = Vector3.Lerp(start, end, t);

        return new Vector3(mid.x, f(t) + Mathf.Lerp(start.y, end.y, t), mid.z);
    }
}
