using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[CreateAssetMenu(fileName = "New Actions Profile", menuName = "Action Profile", order = 4)]
public class ActionsGenerationProfile : ScriptableObject
{
    public List<string> PossibleNames;

    public int MinNumberOfActions;
    public int MaxNumberOfActions;

    public List<Action> PossibleActions;
    public List<Action> GuaranteedActions;
}
