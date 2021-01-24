using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoveCommand : ICommand
{
    private GameObject _gameObject;
    private List<Tile> _path;
    private float _speed;
    private Grid<Tile> _grid;
    private System.Action _onMoveComplete;
    private SoundManager _soundManager;

    private bool _rotationEnabled = true;
    private bool _isRotating = false;
    private bool _skipMovement = false;

    public MoveCommand(
        GameObject gameObject,
        List<Tile> path,
        float speed,
        System.Action onMoveComplete = null,
        bool roatationEnabled = true,
        bool skipMovement = false)
    {
        _gameObject = gameObject;
        _path = path;
        _speed = speed;
        _grid = TileGridController.Instance.GetGrid();
        _onMoveComplete = onMoveComplete;
        _rotationEnabled = roatationEnabled;
        _skipMovement = skipMovement;
        _soundManager = SoundManager.GetInstance();
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
        _isRotating = true;

        _soundManager?.Play(Constants.WALK_SOUND);
        while (targetTile != null && _gameObject != null)
        {
            if (_isRotating && _rotationEnabled)
            {
                FaceMovementDirection(_gameObject.transform.position, targetPosition);
                _soundManager?.Pause(Constants.WALK_SOUND);
            }
            else
            {
                if (_skipMovement)
                {
                    _gameObject.transform.position = targetPosition;
                }
                else
                {
                    _soundManager?.Resume(Constants.WALK_SOUND);
                    _gameObject.transform.position = Vector3.MoveTowards(_gameObject.transform.position, targetPosition, Time.deltaTime * _speed);
                }



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
                        _isRotating = true;
                    }
                }
            }

            yield return null;
        }

        _soundManager?.Stop(Constants.WALK_SOUND);
        _onMoveComplete?.Invoke();
    }

    private void FaceMovementDirection(Vector3 fromPosition, Vector3 toPosition)
    {
        var characterController = _gameObject.GetComponent<CharacterController>();

        if (characterController?.Model != null && fromPosition != toPosition)
        {
            var targetRotation = Quaternion.LookRotation((fromPosition - toPosition).normalized);

            characterController.Model.transform.rotation = Quaternion.RotateTowards(
                characterController.Model.transform.rotation,
                targetRotation,
                Time.deltaTime * Constants.CHAR_ROTATION_SPEED
            );

            if (targetRotation == characterController.Model.transform.rotation)
            {
                _isRotating = false;
            }
        }
        else
        {
            _isRotating = false;
        }
    }
}
