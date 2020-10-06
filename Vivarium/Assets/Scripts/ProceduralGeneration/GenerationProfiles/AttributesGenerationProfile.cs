using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Attribute Profile", menuName = "Attribute Profile", order = 3)]
public class AttributesGenerationProfile : ScriptableObject
{
    public int MinNumberOfAttributes;
    public int MaxNumberOfAttributes;

    public List<Attribute> PossibleAttributes;
    public List<Attribute> GuaranteedAttributes;
}
