using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
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
    public GameObject mapGeneratorPrefab;
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
    public KeyCode gameOverLoseKey;
    public KeyCode gameOverWinKey;
    // text i guess
    public TextMeshProUGUI playerOneScore;
    public TextMeshProUGUI playerOneLives;
    public TextMeshProUGUI playerTwoScore;
    public TextMeshProUGUI playerTwoLives;
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
    public void SpawnMap()
    {
        GameObject newMapGen = Instantiate(mapGeneratorPrefab, Vector3.zero, Quaternion.identity);
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
        newPawn.controller = newController;
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
            TankPawn newPawn = newAIObject.GetComponent<TankPawn>();
            newController.pawn = newPawn;
            newPawn.controller = newController;
            enemies.Add(newController);
        }
    }

    public void RespawnPlayer(Controller oldController)
    {
        PawnSpawnPoint[] possiblePlayerSpawns = FindObjectsOfType<PawnSpawnPoint>();
        int randomSpawn = Random.Range(0, possiblePlayerSpawns.Length);
        GameObject newPawnObject = Instantiate(playerPawnPrefab, possiblePlayerSpawns[randomSpawn].transform.position, possiblePlayerSpawns[randomSpawn].transform.rotation);
        Pawn newPawn = newPawnObject.GetComponent<Pawn>();
        oldController.pawn = newPawn; // pretty much use the player pawn prefabs to remake a player while keeping the same controller
        newPawn.controller = oldController;
    }
    void Start()
    {
        DeactivateAllStates();
        ActivateTitleScreen();
        SetPlayerOneScore(0);
        SetPlayerOneLives(players[0].lives);
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
        if (Input.GetKeyDown(gameOverLoseKey))
        {
            ActivateGameOverScreen(false);
        }
        if (Input.GetKeyDown(gameOverWinKey))
        {
            ActivateGameOverScreen(true);
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
        players.Clear();
        enemies.Clear();
        MapGenerator[] existingMap = FindObjectsOfType<MapGenerator>();
        for (int i = 0; i < existingMap.Length;) 
        {
            Destroy(existingMap[i]);
        }
        SpawnMap();
        SpawnPlayer(); // do all that prefab stuff
        SpawnEnemies();

    }
    public void TryGameOver()
    {
        bool isGameOver = true;
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].lives > 0)
            {
                isGameOver = false;
            }
        }
        if (isGameOver == true)
        {
            ActivateGameOverScreen(false);
        }
        isGameOver = true;
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i].lives > 0)
            {
                isGameOver = false;
            }
        }
        if (isGameOver == true)
        {
            ActivateGameOverScreen(true);
        }
    }
    public void ActivateGameOverScreen(bool victory)
    {
        // shut er down
        DeactivateAllStates();
        // set em up
        GameOverScreenStateObject.SetActive(true);
        //insert whatever else here
    }
    public void SetPlayerOneScore(int score)
    {
        playerOneScore.text = "SCORE: " + score;
    }
    public void SetPlayerOneLives(int lives)
    {
        playerOneLives.text = "LIVES: " + lives;
    }
    public void SetPlayerTwoScore(int score)
    {
        playerTwoScore.text = "SCORE: " + score;
    }
    public void SetPlayerTwoLives(int lives)
    {
        playerTwoLives.text = "LIVES: " + lives;
    }
}