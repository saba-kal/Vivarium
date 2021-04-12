using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Allows game object to choose a material randomly from an array
/// </summary>
public class choose_material : MonoBehaviour
{
    public Material[] materials = new Material[3];

    void Start()
    {
        var randomMaterial = Random.Range(0, 3);
        this.gameObject.GetComponent<Renderer>().material = materials[randomMaterial];

    }
}
