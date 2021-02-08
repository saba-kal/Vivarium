using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateFocusPlane : MonoBehaviour
{
    public GameObject FocusPlane;
    // Start is called before the first frame update
    void Start()
    {
        var allCoords = this.GetComponent<GetMapCoords>().GetAllCoords();
        this.GetComponent<GenerateObstacles>().generateObstacles(FocusPlane, allCoords, -2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
