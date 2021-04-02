using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTransparency : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var myColor = this.GetComponent<MeshRenderer>().material.color;
        myColor.a = 0.1f;
        this.GetComponent<MeshRenderer>().material.color = myColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
