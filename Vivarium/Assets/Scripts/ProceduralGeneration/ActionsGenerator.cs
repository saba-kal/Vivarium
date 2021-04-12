using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

/// <summary>
/// Generates a list of actions based on a given ActionsGenerationProfile.
/// </summary>
public class ActionsGenerator
{
    /// <summary>
    /// Generates a list of actions based on a given ActionsGenerationProfile.
    /// </summary>
    /// <param name="actionsProfile">An <see cref="ActionsGenerationProfile"/> containing the information used to generate the actions.</param>
    /// <returns>A list of randomly generated <see cref="Action"/>.</returns>
    public List<Action> GenerateActions(ActionsGenerationProfile actionsProfile)
    {
        if (actionsProfile.MinNumberOfActions < 0 ||
            actionsProfile.MaxNumberOfActions > actionsProfile.PossibleActions.Count)
        {
            Debug.LogError($"The minimum and maximum number of actions is out of range of the possible actions.");
            return new List<Action>();
        }

        var numberOfActions = UnityEngine.Random.Range(actionsProfile.MinNumberOfActions, actionsProfile.MaxNumberOfActions + 1);
        var actions = actionsProfile.PossibleActions.OrderBy(x => UnityEngine.Random.Range(0, 100)).Take(numberOfActions);

        if (actionsProfile.GuaranteedActions != null &&
            actionsProfile.GuaranteedActions.Count > 0)
        {
            actions = actions.Concat(actionsProfile.GuaranteedActions);
        }

        foreach (var action in actions)
        {
            action.Id = Guid.NewGuid().ToString();
        }

        return actions.ToList();
    }
}
