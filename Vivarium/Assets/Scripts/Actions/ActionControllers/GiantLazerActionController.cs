using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class GiantLazerActionController : ActionController
{
    public Vector3 LazerStartPosition;
    public GameObject LazerModel;
    public GameObject CharacterModel;
    public Transform EyeBallTransform;
    public float LazerDuration = 0.5f;

    private Grid<Tile> _grid;

    public override void Execute(Tile targetTile)
    {
        if (_characterController == null)
        {
            _characterController = GetComponent<CharacterController>();
        }

        _grid = TileGridController.Instance.GetGrid();

        //Get the tiles where the laser starts and ends.
        GetStartTiles(targetTile, out var startTile1, out var startTile2, out var targetTile1, out var targetTile2);
        if (startTile1 == null || startTile2 == null || targetTile1 == null || targetTile2 == null)
        {
            return;
        }

        //Get two parallel lines of grid tiles. They will represent the tiles that were affected by the attack.
        var line1 = _grid.GetLine(
            _grid.GetWorldPosition(startTile1.GridX, startTile1.GridY),
            _grid.GetWorldPosition(targetTile1.GridX, targetTile1.GridY));
        var line2 = _grid.GetLine(
            _grid.GetWorldPosition(startTile2.GridX, startTile2.GridY),
            _grid.GetWorldPosition(targetTile2.GridX, targetTile2.GridY));

        //Remove any duplicates
        var affectedTiles = new Dictionary<(int, int), Tile>();
        foreach (var tile in line1.Concat(line2))
        {
            if (!affectedTiles.ContainsKey((tile.GridX, tile.GridY)))
            {
                affectedTiles.Add((tile.GridX, tile.GridY), tile);
            }
        }

        PlaySound();
        ExecuteAction(affectedTiles);
        AnimateAttack(targetTile1);

        return;
    }

    private void GetStartTiles(Tile targetTile, out Tile startTile1, out Tile startTile2, out Tile targetTile1, out Tile targetTile2)
    {
        startTile1 = _grid.GetValue(transform.position);
        startTile2 = _grid.GetValue(startTile1.GridX + 1, startTile1.GridY);
        targetTile1 = targetTile;
        targetTile2 = _grid.GetValue(targetTile1.GridX + 1, targetTile1.GridY);

        if (targetTile1.GridX > startTile1.GridX + 2)
        {
            startTile1 = _grid.GetValue(startTile1.GridX + 1, startTile1.GridY);
            startTile2 = _grid.GetValue(startTile1.GridX, startTile1.GridY + 1);
            targetTile2 = _grid.GetValue(targetTile1.GridX, targetTile1.GridY + 1);
        }
        else if (targetTile1.GridX >= startTile1.GridX - 1 &&
                targetTile1.GridX <= startTile1.GridX + 2 &&
                targetTile1.GridY > startTile1.GridY)
        {
            startTile1 = _grid.GetValue(startTile1.GridX, startTile1.GridY + 1);
            startTile2 = _grid.GetValue(startTile1.GridX + 1, startTile1.GridY);
        }
        else if (targetTile1.GridX < startTile1.GridX - 1)
        {
            startTile2 = _grid.GetValue(startTile1.GridX, startTile1.GridY + 1);
            targetTile2 = _grid.GetValue(targetTile1.GridX, targetTile1.GridY + 1);
        }
    }

    private void AnimateAttack(Tile targetTile)
    {

        var targetPosition = _grid.GetWorldPositionCentered(targetTile.GridX, targetTile.GridY);
        var direction = (targetPosition - CharacterModel.transform.position).normalized;
        CharacterModel.transform.rotation = Quaternion.LookRotation(direction);
        StartCoroutine(EnableLazer());
        //var scale = Vector3.Distance(transform.position, targetPosition);
        //lazer.transform.localScale = new Vector3(1, 1, scale);
        //lazer.transform.localPosition -= new Vector3(0, 0, scale);
    }

    private IEnumerator EnableLazer()
    {
        LazerModel.SetActive(true);
        yield return new WaitForSeconds(LazerDuration);
        LazerModel.SetActive(false);
    }
}
