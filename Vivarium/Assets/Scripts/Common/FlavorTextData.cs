using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Scriptable object version of <see cref="FlavorText"/>.
/// </summary>
[CreateAssetMenu(fileName = "New Flavor Text", menuName = "Flavor Text", order = 20)]
public class FlavorTextData : ScriptableObject
{
    /// <summary>
    /// List of possible names that a game entity can have.
    /// One will be chosen at random when creating <see cref="FlavorText"/>.
    /// </summary>
    public List<string> PossibleNames;

    /// <summary>
    /// Gets the first name in the possible names list.
    /// </summary>
    public string Name { get => PossibleNames.FirstOrDefault() ?? "Missing Name"; }

    /// <summary>
    /// The description of the game entity.
    /// </summary>
    [TextArea(5, 20)]
    public string Description;
}
