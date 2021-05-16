using UnityEngine;
using System.Collections;

public class UndoMoveController : MonoBehaviour
{
    public static UndoMoveController Instance { get; private set; }
    public Vector3 recordedPosition;
    public CharacterController recordedCharacter;
    public bool IsUndoTrue = false;

    void Start()
    {
        Instance = this;
    }

    void OnEnable()
    {
        CharacterController.OnMove += RecordMove;
        UIController.OnUndoClick += UndoMove;
        UIController.OnEndTurnClick += DisableUndo;
        PlayerController.OnPlayerAttack += DisableUndo;
        TradeUIController.OnTradeComplete += DisableUndo;
    }

    void OnDisable()
    {
        CharacterController.OnMove -= RecordMove;
        UIController.OnUndoClick -= UndoMove;
        UIController.OnEndTurnClick -= DisableUndo;
        PlayerController.OnPlayerAttack -= DisableUndo;
        TradeUIController.OnTradeComplete -= DisableUndo;
    }

    public void RecordMove(CharacterController characterController, Vector3 oldPosition)
    {
        if (characterController.IsEnemy)
        {
            return;
        }
        recordedPosition = oldPosition;
        recordedCharacter = characterController;
        UIController.Instance.UndoButton.interactable = true;
        IsUndoTrue = true;
    }

    public void UndoMove()
    {
        recordedCharacter.GetGridPosition().CharacterControllerId = null;
        recordedCharacter.transform.position = recordedPosition;
        recordedCharacter.GetGridPosition().CharacterControllerId = recordedCharacter.Id;
        recordedCharacter.Deselect();
        recordedCharacter.HideMoveRadius();
        recordedCharacter.SetHasMoved(false);
        UnitInspectionController.Instance.EnableMoveForCharacter(recordedCharacter.Id);
        DisableUndo();
    }

    public void DisableUndo()
    {
        recordedCharacter = null;
        recordedPosition = Vector3.zero;
        UIController.Instance.UndoButton.interactable = false;
        IsUndoTrue = false;
    }

    private void DisableUndo(CharacterController unused)
    {
        recordedCharacter = null;
        recordedPosition = Vector3.zero;
        UIController.Instance.UndoButton.interactable = false;
        IsUndoTrue = false;
    }
}