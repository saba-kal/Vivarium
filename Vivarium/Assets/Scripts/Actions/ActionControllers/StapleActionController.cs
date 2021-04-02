/// <summary>
/// Action controller for the staple knuckles weapon, which stuns characters on hit.
/// </summary>
public class StapleActionController : ActionController
{
    protected override void ExecuteActionOnCharacter(CharacterController targetCharacter)
    {
        base.ExecuteActionOnCharacter(targetCharacter);

        var stunMovementRange = 0f;
        if (targetCharacter.Character.Type == CharacterType.QueenBee)
        {
            //Don't completely stun boss character so that the fight does not become trivial.
            stunMovementRange = targetCharacter.Character.MoveRange / 2;
        }

        targetCharacter.IsStunned(stunMovementRange);
    }
}