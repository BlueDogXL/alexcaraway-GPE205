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
    public List<AIController> enemies;
    public int numberOfEnemies;
    public GameObject[] aiPrefabs;
    // game states
    public GameObject TitleScreenStateObject;
    public GameObject MainMenuStateObject;
    public GameObject OptionsScreenStateObject;
    public GameObject CreditsScreenStateObject;
    public GameObject GameplayStateObject;
    public GameObject GameOverScreenStateObject;
    // testing keys
    public KeyCode titleKey;
    public KeyCode menuKey;
    public KeyCode optionsKey;
    public KeyCode creditsKey;
    public KeyCode gameplayKey;
    public KeyCode gameOverKey;
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
        enemies = new List<AIController>();
    }

    public void SpawnPlayer()
    {
        PawnSpawnPoint[] possiblePlayerSpawns = FindObjectsOfType<PawnSpawnPoint>();
        int randomSpawn = Random.Range(0, possiblePlayerSpawns.Length);
        GameObject newPlayerObject = Instantiate(playerControllerPrefab, Vector3.zero, Quaternion.identity);
        GameObject newPawnObject = Instantiate(playerPawnPrefab, possiblePlayerSpawns[randomSpawn].transform.position, possiblePlayerSpawns[randomSpawn].transform.rotation);
        PlayerController newController = newPlayerObject.GetComponent<PlayerController>();
        Pawn newPawn = newPawnObject.GetComponent<Pawn>();
        newController.pawn = newPawn; // pretty much use the player prefabs to make a player
        newPawn.player = newController;
        players.Add(newController);
    }
    public void SpawnEnemies()
    {
        AISpawnPoint[] possibleAISpawns = FindObjectsOfType<AISpawnPoint>();
        
        for (int i = 0; i <= numberOfEnemies; i++)
        {
            int randomSpawn = Random.Range(0, possibleAISpawns.Length);
            int randomPrefab = Random.Range(0, aiPrefabs.Length);
            GameObject newAIObject = Instantiate(aiPrefabs[randomPrefab], possibleAISpawns[randomSpawn].transform.position, possibleAISpawns[randomSpawn].transform.rotation);
            AIController newController = newAIObject.GetComponent<AIController>();
            enemies.Add(newController);
        }
    }
    
    void Start()
    {
        // SpawnPlayer(); // do all that prefab stuff
        // SpawnEnemies();
        DeactivateAllStates();
        ActivateTitleScreen();
    }

    private void Update()
    {
        if (Input.GetKeyDown(titleKey))
        {
            ActivateTitleScreen();
        }
        if (Input.GetKeyDown(menuKey))
        {
            ActivateMainMenuScreen();
        }
        if (Input.GetKeyDown(optionsKey))
        {
            ActivateOptionsScreen();
        }
        if (Input.GetKeyDown(creditsKey))
        {
            ActivateCreditsScreen();
        }
        if (Input.GetKeyDown(gameplayKey))
        {
            ActivateGameplayScreen();
        }
        if (Input.GetKeyDown(gameOverKey))
        {
            ActivateGameOverScreen();
        }
    }
    private void DeactivateAllStates()
    {
        // Deactivate all Game States
        TitleScreenStateObject.SetActive(false);
        MainMenuStateObject.SetActive(false);
        OptionsScreenStateObject.SetActive(false);
        CreditsScreenStateObject.SetActive(false);
        GameplayStateObject.SetActive(false);
        GameOverScreenStateObject.SetActive(false);
    }
    public void ActivateTitleScreen()
    {
        // shut er down
        DeactivateAllStates();
        // set em up
        TitleScreenStateObject.SetActive(true);
    }
    public void ActivateMainMenuScreen()
    {
        // shut er down
        DeactivateAllStates();
        // set em up
        MainMenuStateObject.SetActive(true);
        //insert whatever else here
    }
    public void ActivateOptionsScreen()
    {
        // shut er down
        DeactivateAllStates();
        // set em up
        OptionsScreenStateObject.SetActive(true);
        //insert whatever else here
    }
    public void ActivateCreditsScreen()
    {
        // shut er down
        DeactivateAllStates();
        // set em up
        CreditsScreenStateObject.SetActive(true);
        //insert whatever else here
    }
    public void ActivateGameplayScreen()
    {
        // shut er down
        DeactivateAllStates();
        // set em up
        GameplayStateObject.SetActive(true);
        //insert whatever else here
    }
    public void ActivateGameOverScreen()
    {
        // shut er down
        DeactivateAllStates();
        // set em up
        GameOverScreenStateObject.SetActive(true);
        //insert whatever else here
    }
}