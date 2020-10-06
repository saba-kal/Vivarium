using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActionsGenerationProfile
{
    public List<string> PossibleNames;

    public int MinNumberOfActions;
    public int MaxNumberOfActions;

    public List<Action> PossibleActions;
    public List<Action> GuaranteedActions;
}
