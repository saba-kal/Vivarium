using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoveCommand : ICommand
{
    private GameObject _gameObject;
    private Vector3 _toPosition;
    private List<Tile> _path;
    private float _speed;
    private Grid<Tile> _grid;

    public MoveCommand(
        GameObject gameObject,
        List<Tile> path,
        float speed)
    {
        _gameObject = gameObject;
        _path = path;
        _speed = speed;
        _grid = TileGridController.Instance.GetGrid();
    }

    public IEnumerator Execute()
    {
        if (_path == null || _path.Count == 0)
        {
            Debug.LogError("Unable to execute move command. The given path is null or empty.");
            yield break;
        }

        var pathQueue = new Queue<Tile>(_path);
        var targetTile = pathQueue.Dequeue();
        var targetPosition = _grid.GetWorldPositionCentered(targetTile.GridX, targetTile.GridY);

        while (targetTile != null)
        {
            _gameObject.transform.position = Vector3.MoveTowards(_gameObject.transform.position, targetPosition, Time.deltaTime * _speed);
            if (targetPosition == _gameObject.transform.position)
            {
                if (pathQueue.Count == 0)
                {
                    targetTile = null;
                }
                else
                {
                    targetTile = pathQueue.Dequeue();
                    targetPosition = _grid.GetWorldPositionCentered(targetTile.GridX, targetTile.GridY);
                }
            }
            yield return null;
        }
    }
}
