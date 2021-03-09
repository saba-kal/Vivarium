using UnityEngine;
using System.Collections;

public class BeeHiveAIController : AIController
{

    public override void Move(
        System.Action onComplete)
    {
        onComplete?.Invoke();
    }

    public override void PerformAction(
        System.Action onComplete)
    {
        onComplete?.Invoke();
    }
}
