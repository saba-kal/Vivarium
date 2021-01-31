using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;

public class SkewerActionController : ActionController
{
    //public Vector3 ProjectileStartPosition;
    //public Transform ProjectileTransform;

    protected override void ExecuteActionOnCharacter(CharacterController targetCharacter)
    {
        if (_characterController.IsAbleToMove())
        {
            base.ExecuteActionOnCharacter(targetCharacter);
            _characterController.SetHasMoved(true);
            UIController.Instance.DisableMoveForCharacter(_characterController.Id);
        }
    }
}
