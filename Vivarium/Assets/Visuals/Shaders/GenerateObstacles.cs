using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateObstacles : MonoBehaviour
{
    public GameObject highRiseRockPrefab;
    public GameObject obstaclePrefab;
    public GameObject underObstaclePrefab;
    public GameObject pebbleFillerPreab;

    public GameObject grassFiller;

    // Start is called before the first frame update
    void Start()
    {
        //var obstacleCoords = this.GetComponent<GetMapCoords>().GetObstacleCoords();
        //var grid = this.GetComponent<GetMapCoords>().GetGrid();
        //var gridObject = this.GetComponent<GetMapCoords>().GetGridObject();

        //for (var z = 0; z < obstacleCoords.Count; z++)
        //{
        //    var tile = grid[obstacleCoords[z][0], obstacleCoords[z][1]];
        //    Debug.Log(tile.Type);
        //    Instantiate(obstaclePrefab, gridObject.GetWorldPositionCentered(obstacleCoords[z][0], obstacleCoords[z][1]), Random.rotation);
        //}
        var obstacleCoords = this.GetComponent<GetMapCoords>().GetObstacleCoords();
        var grassCoords = this.GetComponent<GetMapCoords>().GetGrassTiles();

        generateObstacles(obstaclePrefab, 0.4f, true, false, obstacleCoords, true);
        generateObstacles(underObstaclePrefab , -0.6f, true, false, obstacleCoords, false);
        generateObstacles(pebbleFillerPreab, 0, false, true, obstacleCoords, false);

        generateObstacles(underObstaclePrefab, -1f, false, false, grassCoords, false, true);
        generateObstacles(grassFiller, -0.3f, false, true, grassCoords, false, false);

    }

    public void generateObstacles(GameObject obstacle, float additionY, bool randomRot, bool isVariedHeight, List<List<int>> Coords, bool allowHighRise, bool adjustScale = false)
    {
        // var obstacleCoords = this.GetComponent<GetMapCoords>().GetObstacleCoords();
        var obstacleCoords = Coords;
        var grid = this.GetComponent<GetMapCoords>().GetGrid();
        var gridObject = this.GetComponent<GetMapCoords>().GetGridObject();

        for (var z = 0; z < obstacleCoords.Count; z++)
        {
            var tile = grid[obstacleCoords[z][0], obstacleCoords[z][1]];
            Debug.Log(tile.Type);
            var rotation = Quaternion.identity;
            if (randomRot)
            {
                rotation = Random.rotation;
            }
            var obstacleInstance = Instantiate(obstacle, gridObject.GetWorldPositionCentered(obstacleCoords[z][0], obstacleCoords[z][1]) + new Vector3(0, additionY, 0), rotation);

            if (allowHighRise)
            {
                // highrise
                var randomoffsetX = Random.Range(-0.3f, 0.3f);
                var randomoffsetZ = Random.Range(-0.3f, 0.3f);
                var createhighRise = Random.Range(0, 3);
                if (createhighRise == 1)
                {
                    var highRiseInstance = Instantiate(highRiseRockPrefab, gridObject.GetWorldPositionCentered(obstacleCoords[z][0], obstacleCoords[z][1]) + new Vector3(0, additionY, 0) + new Vector3(randomoffsetX, 0, randomoffsetZ), Quaternion.identity);
                    var randRotX = Random.Range(-20, 20);
                    var randRotY = Random.Range(0f, 180f);
                    var randRotZ = Random.Range(-20, 20);
                    highRiseInstance.transform.rotation *= Quaternion.Euler(randRotX, randRotY, randRotZ);
                }
            }
            
            var intervalRotation = new List<float> { 90, 180, 270};
            
            if (isVariedHeight)
            {
                var randomIntervalRotation = Random.Range(0, 2);
                //var randHeight = Random.Range(1f, 1.5f);
                //obstacleInstance.transform.localScale = new Vector3(1, randHeight, 1);
                obstacleInstance.transform.rotation *= Quaternion.Euler(0, intervalRotation[randomIntervalRotation], 0);
            }

            if (adjustScale)
            {
                var scaleX = Random.Range(2f, 3f);
                var scaleZ = Random.Range(2f, 3f);
                obstacleInstance.transform.localScale = new Vector3(scaleX, 1, scaleZ);
                //obstacleInstance.transform.localScale.x = 2;
            }
        }
    }

}


