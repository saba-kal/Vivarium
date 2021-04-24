using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDecoration : MonoBehaviour
{
    public List<GameObject> decorationPrefabs;
    public GameObject holdGameObject;

    // Start is called before the first frame update
    void Start()
    {
        //SpawnRandomDecoration();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnRandomDecoration()
    {
        var randIndex = Random.Range(0, decorationPrefabs.Count);
        holdGameObject = Instantiate(decorationPrefabs[randIndex], this.transform.position, Quaternion.identity);
    }

    public void DeSpawnGameObject()
    {
        Destroy(this.holdGameObject);
    }
}
