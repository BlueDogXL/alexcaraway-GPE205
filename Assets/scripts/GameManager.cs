using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // prefabs
    public GameObject playerControllerPrefab;
    public GameObject playerPawnPrefab;
    public Transform playerSpawnTransform;
    public List<PlayerController> players;
    void Awake() // game manager used extreme speed and does this before start()
    {
        if (instance == null) // if there is no game manager already
        {
            instance = this; // we are the game manager now, don't destroy me
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); // there can only be one
        }

        players = new List<PlayerController>(); // list of players for player controllers to add themselves to
    }

    public void SpawnPlayer()
    {
        GameObject newPlayerObject = Instantiate(playerControllerPrefab, Vector3.zero, Quaternion.identity);
        GameObject newPawnObject = Instantiate(playerPawnPrefab, playerSpawnTransform.position, playerSpawnTransform.rotation);
        Controller newController = newPlayerObject.GetComponent<Controller>();
        Pawn newPawn = newPawnObject.GetComponent<Pawn>();
        newController.pawn = newPawn; // pretty much use the player prefabs to make a player
    }
    
    void Start()
    {
        SpawnPlayer(); // do all that prefab stuff
    }
}