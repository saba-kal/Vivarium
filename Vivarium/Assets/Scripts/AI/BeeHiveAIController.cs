using UnityEngine;
using System.Collections;

/// <summary>
/// <see cref="AIController"/> for the bee hive character. Since bee hives cannot move or perform actions,
/// this class simply overrides the base <see cref="AIController"/> and removes all logic.
/// </summary>
public class BeeHiveAIController : AIController
{
    /// <inheritdoc cref="AIController.Move(System.Action)"/>
    public override void Move(
        System.Action onComplete)
    {
        onComplete?.Invoke();
    }

    /// <inheritdoc cref="AIController.PerformAction(System.Action)"/>
    public override void PerformAction(
        System.Action onComplete)
    {
        onComplete?.Invoke();
    }
}
