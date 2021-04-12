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
    private Animator _animator;
    private string _moveAnimationId;

    private bool _rotationEnabled = true;
    private bool _isRotating = false;
    private bool _skipMovement = false;


    /// <summary>
    /// Command to move a gameObject
    /// </summary>
    /// <param name="gameObject">The game object to move</param>
    /// <param name="path">The list of tiles that the game object will move to</param>
    /// <param name="speed">The speed in which the game obejct will move</param>
    /// <param name="onMoveComplete">an action that will occur when the movement is complete</param>
    /// <param name="roatationEnabled">a boolean indicating if the game object will rotate when moving</param>
    /// <param name="skipMovement">a boolean indicating if moving the object is to be shown</param>
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
        _animator = _gameObject.GetComponentInChildren<Animator>();
        _moveAnimationId = System.Enum.GetName(typeof(AnimationType), AnimationType.move);
    }


    /// <summary>
    /// Moves the gameobject
    /// </summary>
    /// <returns></returns>
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
        SetWalkAnimationTrigger(true);

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
        SetWalkAnimationTrigger(false);
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

            if (Quaternion.Angle(characterController.Model.transform.rotation, targetRotation) <= 0.01f)
            {
                _isRotating = false;
            }
        }
        else
        {
            _isRotating = false;
        }
    }

    private void SetWalkAnimationTrigger(bool isWalking)
    {
        if (Utils.HasParameter(_moveAnimationId, _animator))
        {
            _animator.SetBool(_moveAnimationId, isWalking);
        }
    }
}
