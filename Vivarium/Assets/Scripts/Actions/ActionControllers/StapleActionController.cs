using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;

public class StapleActionController : ActionController
{
    protected override void ExecuteActionOnCharacter(CharacterController targetCharacter)
    {
        base.ExecuteActionOnCharacter(targetCharacter);
        targetCharacter.IsStunned();
    }
}