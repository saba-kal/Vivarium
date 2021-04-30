using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Chooses the orientation of the weapon
public class WeaponChooser : MonoBehaviour
{
    [SerializeField]
    private GameObject BeetleVersion;
    [SerializeField]
    private GameObject BeeVersion;
    [SerializeField]
    private GameObject AntVersion;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Returns the corresponding weapon model that fits the roientation of the character type
    /// </summary>
    /// <param name="character">The character data</param>
    /// <returns></returns>
    public GameObject GetWeaponOrientation(Character character)
    {
        if (character.unitType == "bee")
        {
            return BeeVersion;
        }
        else if (character.unitType == "beetle")
        {
            return BeetleVersion;
        }
        else if (character.unitType == "ant")
        {
            return AntVersion;
        }
        else
        {
            return null;
        }
    }
}
