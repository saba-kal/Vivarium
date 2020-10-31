using UnityEngine;
using System.Collections;

public class ShieldGenerator
{
    public Shield GenerateShield(ShieldGenerationProfile shieldProfile)
    {
        if (shieldProfile == null || shieldProfile.PossibleShields == null || shieldProfile.PossibleShields.Count == 0)
        {
            return null;
        }

        return shieldProfile.PossibleShields[Random.Range(0, shieldProfile.PossibleShields.Count)];
    }
}
