using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// A profile used to generate random actions.
/// </summary>
[CreateAssetMenu(fileName = "New Actions Profile", menuName = "Action Profile", order = 4)]
public class ActionsGenerationProfile : ScriptableObject
{
    /// <summary>
    /// A list of possible names for the generated action.
    /// </summary>
    public List<string> PossibleNames;

    /// <summary>
    /// The minimum number of actions that can be generated.
    /// </summary>
    public int MinNumberOfActions;

    /// <summary>
    /// The maximum number of actions that can be generated.
    /// </summary>
    public int MaxNumberOfActions;

    /// <summary>
    /// A list of all possible Actions that the generated <see cref="Action"/>s will be selected from.
    /// </summary>
    public List<Action> PossibleActions;

    /// <summary>
    /// A list of <see cref="Action"/>s that will be guaranteed to be included in the list of generated Actions.
    /// </summary>
    public List<Action> GuaranteedActions;
}
