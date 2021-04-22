using UnityEngine;
using System.Collections;

/// <summary>
/// Generates a Shield based on a given ShieldGenerationProfile.
/// </summary>
public class ShieldGenerator
{
    /// <summary>
    /// Generates a Shield based on a given ShieldGenerationProfile.
    /// </summary>
    /// <param name="shieldProfile"><see cref="ShieldGenerationProfile"/> containing the information used to generate the shield.</param>
    /// <returns>A randomly generated <see cref="Shield"/>.</returns>
    public Shield GenerateShield(ShieldGenerationProfile shieldProfile)
    {
        if (shieldProfile == null || shieldProfile.PossibleShields == null || shieldProfile.PossibleShields.Count == 0)
        {
            return null;
        }

        return shieldProfile.PossibleShields[Random.Range(0, shieldProfile.PossibleShields.Count)];
    }
}
