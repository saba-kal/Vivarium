using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles UI icons that appear near the health bar.
/// </summary>
public class HealthBarIcon : MonoBehaviour
{
    public bool isActionIcon;
    public Image Icon;

    private CharacterController _characterController;

    void Update()
    {
        bool move = CheckOnMove();
        bool action = CheckOnAction();
        if (isActionIcon)
            Icon.enabled = action;
        else
            Icon.enabled = move;
    }

    /// <summary>
    /// Sets the proper character controller to the generated character attached to their health bar. Used by CharacterGenerator.
    /// <param name="characterController">The character controller being set.</param>
    /// </summary>
    public void SetCharacterController(CharacterController characterController)
    {
        _characterController = characterController;
    }

    private bool CheckOnAction()
    {
        return _characterController.IsAbleToAttack();
    }

    private bool CheckOnMove()
    {
        return _characterController.IsAbleToMove();
    }
}