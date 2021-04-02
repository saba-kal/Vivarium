using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateObstacles : MonoBehaviour
{
    public GameObject highRiseRockPrefab;
    public GameObject obstaclePrefab;
    public GameObject underObstaclePrefab;
    public GameObject pebbleFillerPrefab;
    public GameObject coastalGravelPrefab;
    public GameObject pathPrefab;
    public GameObject grassBlockPrefab;

    public List<GameObject> groupedPrefabs;
    public List<GameObject> simpleEnvironmentPrefabs;
    public List<GameObject> superSpecialObstaclePrefabs;


    //public GameObject logPrefab;

    public GameObject dirtFiller;

    private List<GameObject> allEnvironmentObjects = new List<GameObject>();


    public void clearObjects()
    {
        for (var i = 0; i < allEnvironmentObjects.Count; i++)
        {
            DestroyImmediate(allEnvironmentObjects[i]);
        }
        allEnvironmentObjects = new List<GameObject>();
    }

    public void generateEnvironment()
    {
        var height = this.GetComponent<GetMapCoords>().GetHeight();
        var splitSections = heightSplitter(height, 5);


        var obstacleCoords = this.GetComponent<GetMapCoords>().GetObstacleCoords();
        var grassCoords = this.GetComponent<GetMapCoords>().GetGrassTiles();
        var allCoords = this.GetComponent<GetMapCoords>().GetAllCoords();
        //var borderCoords = this.GetComponent<GetMapCoords>().GetOuterBorderCoords();
        var costalCoords = this.GetComponent<GetMapCoords>().GetCoastalCoords();


        var startSection = 0;
        var stopSection = 0;

        var verticalStartSection = 0;
        var verticalStopSection = 0;


        var randDirection = "horizontal";

        for (var x = 0; x < splitSections.Count; x++)
        {
            verticalStartSection = verticalStopSection;
            verticalStopSection = verticalStartSection + splitSections[x];
            startSection = 0;
            stopSection = 0;
            for (var i = 0; i < splitSections.Count; i++)
            {
                if (randDirection == "horizontal")
                {
                    randDirection = "vertical";
                }
                else
                {
                    randDirection = "horizontal";
                }

                startSection = stopSection;
                stopSection = startSection + splitSections[i];

                if (randDirection == "horizontal")
                {
                    var HorizontalGroupedObstacles = this.GetComponent<GetMapCoords>().GetHorizontalGroupObjects(startSection, stopSection, verticalStartSection, verticalStopSection);


                    var groupedObstacleCoords = GetGroupedFromGroupCoords(HorizontalGroupedObstacles);
                    var singleObstacleCoords = GetSinglesFromGroupCoords(HorizontalGroupedObstacles);

                    generateGroupObstacles(groupedObstacleCoords, "horizontal");
                    generateObstacles(obstaclePrefab, singleObstacleCoords, 0.4f, true, false, true);
                    generateObstacles(dirtFiller, singleObstacleCoords, 0f, false, true, false);
                    generateMultObstacles(superSpecialObstaclePrefabs, singleObstacleCoords, 0, true, 30);




                }
                else if (randDirection == "vertical")
                {
                    var VerticalGroupedObstacles = this.GetComponent<GetMapCoords>().GetVerticalGroupObjects(startSection, stopSection, verticalStartSection, verticalStopSection);

                    var groupedObstacleCoords = GetGroupedFromGroupCoords(VerticalGroupedObstacles);
                    var singleObstacleCoords = GetSinglesFromGroupCoords(VerticalGroupedObstacles);

                    generateGroupObstacles(groupedObstacleCoords, "vertical");
                    generateObstacles(obstaclePrefab, singleObstacleCoords, 0.4f, true, false, true);
                    generateObstacles(dirtFiller, singleObstacleCoords, 0f, false, true, false);
                    generateMultObstacles(superSpecialObstaclePrefabs, singleObstacleCoords, 0, true, 30);
                }



            }
        }



        generateMultObstacles(simpleEnvironmentPrefabs, grassCoords, 0, true, 8);
        generateObstacles(underObstaclePrefab, obstacleCoords, -0.6f, true, false, false);
        generateObstacles(pebbleFillerPrefab, obstacleCoords, 0, false, true, false);

        var gridGenerator = new GridGenerator();
        var pathTiles = gridGenerator.GetPathToObjective(this.GetComponent<GetMapCoords>().GetGridObject());
        var pathCoords = this.GetComponent<GetMapCoords>().TilesToCoords(pathTiles);
        generateObstacles(pathPrefab, pathCoords, -0.6f, false, true);

        var filteredGrassCoords = this.GetComponent<GetMapCoords>().FilterCoords(grassCoords, pathCoords);
        generateObstacles(grassBlockPrefab, filteredGrassCoords, 0, false, false);
    }


    private void generateGroupObstacles(List<List<List<int>>> groupedObstacleCoords, string direction)
    {
        for (var i = 0; i < groupedObstacleCoords.Count; i++)
        {
            var RandomGameObject = groupedPrefabs[Random.Range(0, groupedPrefabs.Count)]; //change to random
            var separatedGroupedObstacleCoords = groupedObstacleCoords[i];

            var rotate = false;
            if (direction == "horizontal")
            {
                rotate = true;
            }
            generateObstacles(RandomGameObject, separatedGroupedObstacleCoords, 0.4f, false, false, false, false, rotate);
            generateObstacles(dirtFiller, separatedGroupedObstacleCoords, 0f, false, true, false);

        }
    }

    private List<int> heightSplitter(int height, int sections)
    {
        var returnList = new List<int>();


        var leftover = height % sections;
        var divisble = height - leftover;
        var evenSections = divisble / sections;

        for (var i = 0; i < sections; i++)
        {
            returnList.Add(evenSections);
        }

        for (var i = 0; i < returnList.Count; i++)
        {
            if (leftover == 0)
            {
                break;
            }
            returnList[i] += 1;
            leftover -= 1;
        }


        return returnList;
    }

    private List<List<List<int>>> GetGroupedFromGroupCoords(List<List<List<int>>> groupedObstacles)
    {
        var groupedObstacleCoords = new List<List<List<int>>>();

        for (var x = 0; x < groupedObstacles.Count; x++)
        {
            if (groupedObstacles[x].Count > 2)
            {
                groupedObstacleCoords.Add(groupedObstacles[x]);
            }
        }
        return groupedObstacleCoords;
    }


    private List<List<int>> GetSinglesFromGroupCoords(List<List<List<int>>> groupedObstacles)
    {
        var normalObstacleCoords = new List<List<int>>();

        for (var x = 0; x < groupedObstacles.Count; x++)
        {
            if (groupedObstacles[x].Count <= 2)
            {
                normalObstacleCoords.AddRange(groupedObstacles[x]);
            }
        }
        return normalObstacleCoords;
    }




    public void generateObstacles(GameObject obstacle, List<List<int>> Coords, float additionY, bool randomRot = false, bool isRandRotateByNinety = false, bool allowHighRise = false, bool adjustScale = false, bool rotateNinety = false)
    {
        // var obstacleCoords = this.GetComponent<GetMapCoords>().GetObstacleCoords();
        var obstacleCoords = Coords;
        var grid = this.GetComponent<GetMapCoords>().GetGrid();
        var gridObject = this.GetComponent<GetMapCoords>().GetGridObject();

        for (var z = 0; z < obstacleCoords.Count; z++)
        {
            var tile = grid[obstacleCoords[z][0], obstacleCoords[z][1]];
            var rotation = Quaternion.identity;
            if (randomRot)
            {
                rotation = Random.rotation;
            }
            var obstacleInstance = Instantiate(obstacle, gridObject.GetWorldPositionCentered(obstacleCoords[z][0], obstacleCoords[z][1]) + new Vector3(0, additionY, 0), rotation);
            //var myColor = obstacleInstance.GetComponent<MeshRenderer>().material.color;
            //myColor.a = 0.1f;
            //obstacleInstance.GetComponent<MeshRenderer>().material.color = myColor;


            obstacleInstance.transform.SetParent(TileGridController.Instance.transform);

            allEnvironmentObjects.Add(obstacleInstance);
            if (allowHighRise)
            {
                // highrise
                var randomoffsetX = Random.Range(-0.3f, 0.3f);
                var randomoffsetZ = Random.Range(-0.3f, 0.3f);
                var createhighRise = Random.Range(0, 3);
                if (createhighRise == 1)
                {
                    var highRiseInstance = Instantiate(highRiseRockPrefab, gridObject.GetWorldPositionCentered(obstacleCoords[z][0], obstacleCoords[z][1]) + new Vector3(0, additionY, 0) + new Vector3(randomoffsetX, 0, randomoffsetZ), Quaternion.identity);
                    highRiseInstance.transform.SetParent(TileGridController.Instance.transform);

                    allEnvironmentObjects.Add(highRiseInstance);
                    var randRotX = Random.Range(-20, 20);
                    var randRotY = Random.Range(0f, 180f);
                    var randRotZ = Random.Range(-20, 20);
                    highRiseInstance.transform.rotation *= Quaternion.Euler(randRotX, randRotY, randRotZ);
                }
            }

            var intervalRotation = new List<float> { 90, 180, 270 };

            if (isRandRotateByNinety)
            {
                var randomIntervalRotation = Random.Range(0, 3);
                obstacleInstance.transform.rotation *= Quaternion.Euler(0, intervalRotation[randomIntervalRotation], 0);
            }

            if (adjustScale)
            {
                var scaleX = Random.Range(2f, 3f);
                var scaleZ = Random.Range(2f, 3f);
                obstacleInstance.transform.localScale = new Vector3(scaleX, 1, scaleZ);
            }

            if (rotateNinety)
            {
                obstacleInstance.transform.rotation *= Quaternion.Euler(0, 90, 0);
            }
        }
    }

    public void generateMultObstacles(List<GameObject> obstacles, List<List<int>> Coords, float additionY, bool isRandRotateByNinety = false, int spawnDifficult = 0)
    {

        // var obstacleCoords = this.GetComponent<GetMapCoords>().GetObstacleCoords();
        var obstacleCoords = Coords;
        var grid = this.GetComponent<GetMapCoords>().GetGrid();
        var gridObject = this.GetComponent<GetMapCoords>().GetGridObject();

        for (var z = 0; z < obstacleCoords.Count; z++)
        {
            var randSpawn = Random.Range(0, spawnDifficult);

            if (randSpawn == 1)
            {
                var tile = grid[obstacleCoords[z][0], obstacleCoords[z][1]];
                var rotation = Quaternion.identity;


                var randPrefab = obstacles[Random.Range(0, obstacles.Count)];
                var obstacleInstance = Instantiate(randPrefab, gridObject.GetWorldPositionCentered(obstacleCoords[z][0], obstacleCoords[z][1]) + new Vector3(0, additionY, 0), rotation);
                obstacleInstance.transform.SetParent(TileGridController.Instance.transform);

                allEnvironmentObjects.Add(obstacleInstance);

                var intervalRotation = new List<float> { 90, 180, 270 };

                if (isRandRotateByNinety)
                {
                    var randomIntervalRotation = Random.Range(0, 3);
                    obstacleInstance.transform.rotation *= Quaternion.Euler(0, intervalRotation[randomIntervalRotation], 0);
                }
            }
        }
    }
}


