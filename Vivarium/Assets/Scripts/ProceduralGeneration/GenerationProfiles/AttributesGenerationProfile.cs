using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// A profile used to generate random attributes.
/// </summary>
[CreateAssetMenu(fileName = "New Attribute Profile", menuName = "Attribute Profile", order = 3)]
public class AttributesGenerationProfile : ScriptableObject
{
    /// <summary>
    /// The minimum number of attributes that can be generated.
    /// </summary>
    public int MinNumberOfAttributes;

    /// <summary>
    /// The maximum number of attributes that can be generated.
    /// </summary>
    public int MaxNumberOfAttributes;

    /// <summary>
    /// A list of all possible <see cref="Attribute"/>s that the generated Attributes will be selected from.
    /// </summary>
    public List<Attribute> PossibleAttributes;

    /// <summary>
    /// A list of <see cref="Attribute"/>s that will be guaranteed to be included in the list of generated Attributes.
    /// </summary>
    public List<Attribute> GuaranteedAttributes;
}
