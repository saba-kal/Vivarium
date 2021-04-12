using UnityEngine;
using System.Collections;

public class MakeCharacterFaceTileCommand : ICommand
{
    private CharacterController _characterController;
    private Tile _targetTile;
    private bool _isInstant;

    /// <summary>
    /// Command to make a character face a tile
    /// </summary>
    /// <param name="characterController">Character controller </param>
    /// <param name="targetTile">Tile the cahracter faces</param>
    /// <param name="isInstant"></param>
    public MakeCharacterFaceTileCommand(
        CharacterController characterController,
        Tile targetTile,
        bool isInstant)
    {
        _characterController = characterController;
        _targetTile = targetTile;
        _isInstant = isInstant;
    }

    /// <summary>
    /// Makes the character face a tile
    /// </summary>
    /// <returns></returns>
    public IEnumerator Execute()
    {
        var grid = TileGridController.Instance.GetGrid();
        if (_characterController?.Model == null || _targetTile == null || grid == null)
        {
            yield return null;
        }

        var direction =
            (_characterController.transform.position - grid.GetWorldPositionCentered(_targetTile.GridX, _targetTile.GridY))
            .normalized;
        var targetRotation = Quaternion.LookRotation(direction);

        if (_isInstant)
        {
            _characterController.Model.transform.rotation = targetRotation;
            yield return null;
        }

        while (_characterController.Model.transform.rotation != targetRotation)
        {
            _characterController.Model.transform.rotation = Quaternion.RotateTowards(
                _characterController.Model.transform.rotation,
                targetRotation,
                Time.deltaTime * Constants.CHAR_ROTATION_SPEED
            );

            yield return null;
        }
    }
}
