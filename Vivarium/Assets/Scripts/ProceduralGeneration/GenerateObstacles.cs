using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class orderedPrefab
{
    public List<GameObject> list;
    public int Count()
    {
        return list.Count;
    }
    public GameObject GetIndex(int index)
    {
        return list[index];
    }
}

[System.Serializable]
public class orderedPrefabList
{
    public List<orderedPrefab> list;
    public int Count()
    {
        return list.Count;
    }
    public orderedPrefab GetIndex(int index)
    {
        return list[index];
    }
}

/// <summary>
/// Script to generate environment models for a level
/// </summary>
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

    public orderedPrefabList orderedPrefabs;

    public GameObject dirtFiller;

    private List<GameObject> allEnvironmentObjects = new List<GameObject>();


    private LevelGenerationProfile _levelProfile;

    /// <summary>
    /// Removes all environment models from the scene
    /// </summary>
    public void clearObjects()
    {
        for (var i = 0; i < allEnvironmentObjects.Count; i++)
        {
            DestroyImmediate(allEnvironmentObjects[i]);
        }
        allEnvironmentObjects = new List<GameObject>();
    }

    /// <summary>
    /// Generates all the environment models for the scene
    /// </summary>
    /// <param name="levelProfile">The level generation profile that dictates the position of the environment models</param>
    public void generateEnvironment(LevelGenerationProfile levelProfile)
    {
        _levelProfile = levelProfile;

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

                    var spawnLargeObject = Random.Range(0, 2);
                    if (spawnLargeObject == 1)
                    {
                        if (singleObstacleCoords.Count > 0)
                        {
                            //generate a large obstacle in every sector
                            var randSingleObstacleCoordIndex = Random.Range(0, singleObstacleCoords.Count - 1);
                            var randSingleObstacleCoord = new List<List<int>> { singleObstacleCoords[randSingleObstacleCoordIndex] };
                            var randSpecialObstaclePrefab = superSpecialObstaclePrefabs[Random.Range(0, superSpecialObstaclePrefabs.Count)];
                            singleObstacleCoords.RemoveAt(randSingleObstacleCoordIndex);
                            generateObstacles(obstaclePrefab, singleObstacleCoords, 0.4f, true, 0, false, true);
                            generateObstacles(randSpecialObstaclePrefab, randSingleObstacleCoord, 0f, false, 0, true, false);
                            generateObstacles(dirtFiller, singleObstacleCoords, 0f, false, 0, true, false);
                        }
                    }
                    else
                    {
                        if (singleObstacleCoords.Count > 0)
                        {
                            generateObstacles(obstaclePrefab, singleObstacleCoords, 0.4f, true, 0, false, true);
                            generateObstacles(dirtFiller, singleObstacleCoords, 0f, false, 0, true, false);
                        }
                    }




                }
                else if (randDirection == "vertical")
                {
                    var VerticalGroupedObstacles = this.GetComponent<GetMapCoords>().GetVerticalGroupObjects(startSection, stopSection, verticalStartSection, verticalStopSection);

                    var groupedObstacleCoords = GetGroupedFromGroupCoords(VerticalGroupedObstacles);
                    var singleObstacleCoords = GetSinglesFromGroupCoords(VerticalGroupedObstacles);

                    generateGroupObstacles(groupedObstacleCoords, "vertical");
                    generateObstacles(obstaclePrefab, singleObstacleCoords, 0.4f, true, 0, false, true);
                    generateObstacles(dirtFiller, singleObstacleCoords, 0f, false, 0, true, false);
                }



            }
        }




        generateObstacles(underObstaclePrefab, obstacleCoords, -0.6f, true, 0, false, false);
        generateObstacles(pebbleFillerPrefab, obstacleCoords, 0, false, 0, true, false);

        generateGroundWithPathToObjective();
    }

    private void generateGroundWithPathToObjective()
    {
        var aStar = new AStar(
            new List<TileType> { TileType.Grass } /*Tiles types that AStart should navigate*/,
            true /*Ignore character positions*/);

        var grid = TileGridController.Instance.GetGrid();
        var startTile = grid.GetValue(0, 0);
        var endTile = grid.GetValue(grid.GetGrid().GetLength(0) - 1, grid.GetGrid().GetLength(1) - 1);

        var pathToObjective = aStar.Execute(startTile, endTile);
        List<List<int>> pathCoords;
        if (pathToObjective == null || _levelProfile?.BossCharacter != null)
        {
            //Don't generate path if we were unable to calculate it (something is blocking the way), or
            //we are on the boss level.
            pathCoords = new List<List<int>>();
        }
        else
        {
            //Generate path tiles.
            pathCoords = this.GetComponent<GetMapCoords>().TilesToCoords(pathToObjective);
            generateObstacles(pathPrefab, pathCoords, -0.6f, false, 0, true);
        }

        //Generate grass tiles
        var grassCoords = this.GetComponent<GetMapCoords>().GetGrassTiles();
        var filteredGrassCoords = this.GetComponent<GetMapCoords>().FilterCoords(grassCoords, pathCoords);
        generateObstacles(grassBlockPrefab, filteredGrassCoords, 0, false, 0, false);
        generateMultObstacles(simpleEnvironmentPrefabs, filteredGrassCoords, 0, true, 8);
    }

    private void generateGroupObstacles(List<List<List<int>>> groupedObstacleCoords, string direction)
    {
        for (var i = 0; i < groupedObstacleCoords.Count; i++)
        {

            var separatedGroupedObstacleCoords = groupedObstacleCoords[i];


            var rotate = false;
            if (direction == "horizontal")
            {
                rotate = true;
            }

            var firstCoord = new List<List<int>>() { separatedGroupedObstacleCoords[0] };
            var lastCoord = new List<List<int>>() { separatedGroupedObstacleCoords[separatedGroupedObstacleCoords.Count - 1] };
            var middleCoords = separatedGroupedObstacleCoords.GetRange(1, separatedGroupedObstacleCoords.Count - 2);

            if (separatedGroupedObstacleCoords.Count > 2)
            {
                var RandomGameObject = orderedPrefabs.GetIndex(Random.Range(0, orderedPrefabs.Count())); //change to random
                generateObstacles(RandomGameObject.GetIndex(1), middleCoords, 0.4f, false, 0, false, false, false, rotate);
                var firstObject = RandomGameObject.GetIndex(0);
                var lastObject = RandomGameObject.GetIndex(2);
                var randOrder = Random.Range(0, 2);

                if (randOrder == 1)
                {
                    generateObstacles(firstObject, firstCoord, 0.4f, false, 0, false, false, false, rotate);
                    generateObstacles(lastObject, lastCoord, 0.4f, false, 0, false, false, false, rotate);
                }
                else
                {
                    firstObject.transform.rotation *= Quaternion.Euler(0, 180, 0);
                    lastObject.transform.rotation *= Quaternion.Euler(0, 180, 0);
                    generateObstacles(lastObject, firstCoord, 0.4f, false, 180, false, false, false, rotate);
                    generateObstacles(firstObject, lastCoord, 0.4f, false, 180, false, false, false, rotate);
                }
            }
            else
            {
                var RandomGameObject = groupedPrefabs[Random.Range(0, groupedPrefabs.Count)]; //change to random
                generateObstacles(RandomGameObject, separatedGroupedObstacleCoords, 0.4f, false, 0, false, false, false, rotate);
                generateObstacles(dirtFiller, separatedGroupedObstacleCoords, 0f, false, 0, true, false);
            }

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



    private void generateObstacles(GameObject obstacle, List<List<int>> Coords, float additionY, bool randomRot = false, int yRotation = 0, bool isRandRotateByNinety = false, bool allowHighRise = false, bool adjustScale = false, bool rotateNinety = false)
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
            obstacleInstance.transform.rotation *= Quaternion.Euler(0, yRotation, 0);
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

    private void generateMultObstacles(List<GameObject> obstacles, List<List<int>> Coords, float additionY, bool isRandRotateByNinety = false, int spawnDifficult = 0)
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


