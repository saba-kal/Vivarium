using UnityEngine;
using System.Collections;

/// <summary>
/// Represents the name and description of various game entities.
/// </summary>
public class FlavorText
{
    /// <summary>
    /// The name of a game entity.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// The description of the game entity.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Creates a flavor text using <see cref="FlavorTextData"/>.
    /// </summary>
    /// <param name="flavorTextData">The flavor text data used to create the flavor text.</param>
    /// <returns><see cref="FlavorText"></returns>
    public static FlavorText FromFlavorTextData(FlavorTextData flavorTextData)
    {
        var name = "Missing Name";
        if (flavorTextData != null && flavorTextData.PossibleNames.Count > 0)
        {
            name = flavorTextData.PossibleNames[Random.Range(0, flavorTextData.PossibleNames.Count)];
        }

        var description = "Missing Description";
        if (!string.IsNullOrWhiteSpace(flavorTextData?.Description))
        {
            description = flavorTextData.Description;
        }

        return new FlavorText
        {
            Name = name,
            Description = description
        };
    }
}
