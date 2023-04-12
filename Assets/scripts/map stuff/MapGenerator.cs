using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MapGenerator : MonoBehaviour
{
    public GameObject[] gridPrefabs;
    public int rows;
    public int cols;
    public float roomWidth = 50;
    public float roomHeight = 50;
    private Room[,] grid;
    private int mapSeed;
    public int setSeed;
    public bool isMapOfTheDay;
    public bool isSetSeed;
    public bool isRandom;
    
    private 
    // Start is called before the first frame update
    void Start()
    {
        if (isMapOfTheDay)
        {
            mapSeed = DateToInt(DateTime.Now.Date);
        }
        else if (isSetSeed)
        {
            mapSeed = setSeed;
        }
        else if (isRandom)
        {
            mapSeed = DateToInt(DateTime.Now);
        }
        GenerateMap();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public GameObject RandomRoomPrefab() // Returns a random room
    {
        return gridPrefabs[UnityEngine.Random.Range(0, gridPrefabs.Length)];
    }

    public int DateToInt(DateTime dateToUse)
    {
        return dateToUse.Year + dateToUse.Month + dateToUse.Day + dateToUse.Hour + dateToUse.Minute + dateToUse.Second + dateToUse.Millisecond;
    }

    public void GenerateMap()
    {
        // clear grid
        grid = new Room[cols, rows];
        UnityEngine.Random.seed = mapSeed;

        // for each row
        for (int currentRow = 0; currentRow < rows; currentRow++)
        {
            for (int currentCol = 0; currentCol < cols; currentCol++)
            {
                // figure out location
                float xPosition = roomWidth * currentCol;
                float zPosition = roomHeight * currentRow;
                Vector3 newPosition = new Vector3(xPosition, 0.0f, zPosition);
                // create map tile there
                GameObject tempRoomObject = Instantiate(RandomRoomPrefab(), newPosition, Quaternion.identity) as GameObject;
                // set tile's parent
                tempRoomObject.transform.parent = this.transform;
                // give it a meaningful name
                tempRoomObject.name = "Room_" + currentCol + "," + currentRow;
                // get room object reference
                Room tempRoom = tempRoomObject.GetComponent<Room>();
                // add it to the grid
                grid[currentCol, currentRow] = tempRoom;

                // open doors
                // if bottom row, open north
                if (currentRow == 0)
                {
                    tempRoom.doorNorth.SetActive(false);
                }
                // if top row, set south open
                else if (currentRow == rows - 1)
                {
                    tempRoom.doorSouth.SetActive(false);
                }
                else
                {
                    tempRoom.doorNorth.SetActive(false);
                    tempRoom.doorSouth.SetActive(false);
                }
                if (currentCol == 0)
                {
                    tempRoom.doorEast.SetActive(false);
                }
                // if top row, set south open
                else if (currentCol == cols - 1)
                {
                    tempRoom.doorWest.SetActive(false);
                }
                else
                {
                    tempRoom.doorWest.SetActive(false);
                    tempRoom.doorEast.SetActive(false);
                }
            }
        }
    }
}
