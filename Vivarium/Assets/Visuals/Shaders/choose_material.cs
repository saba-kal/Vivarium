using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class choose_material : MonoBehaviour
{
    public Material[] materials = new Material[3];
    
    //public Material[] materialOptions = numMaterials;

    // Start is called before the first frame update
    void Start()
    {
        var randomMaterial = Random.Range(0, 3);
        this.gameObject.GetComponent<Renderer>().material = materials[randomMaterial];

        RandomRotation();
    }

    void RandomRotation()
    {
        List<float> rotations = new List<float>() { 0f, 90f, 180f, 270f};
        var randomRotation = Random.Range(0, 4);
        this.gameObject.transform.rotation *= Quaternion.Euler(0, rotations[randomRotation], 0);

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
