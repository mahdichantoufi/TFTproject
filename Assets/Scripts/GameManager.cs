using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour 
{
    public static GameManager instance = null;
    private GameController gameController;
    private ChampionPrepController championPrepController;
    private PlayerData playerData;
    private string username;
    public bool fighting;


    public int GameLevel = 0;
    private int LevelNumbers = 2;

    public GameObject ChampionPrefab1;
    public GameObject ChampionPrefab2;
    public GameObject ChampionPrefab3;
    public GameObject ChampionPrefab4;
    public GameObject EnemyChampionPrefab1;
    public GameObject EnemyChampionPrefab2;
    public GameObject EnemyChampionPrefab3;
    public GameObject EnemyChampionPrefab4;

    private GameObject[] Champions;
    private GameObject[] EnemyChampions;
    private int[,] EnemyChampionsPerLevel;
    public bool PlayerWonTheFight = true;

    void Awake()
    {
        //Check if instance already exists
        if (instance == null)
            //if not, set instance to this
            instance = this;
        //If instance already exists and it's not this:
        else if (instance != this)
            //Then destroy this. This enforces our singleton pattern, 
            // meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
        //Sets this to not be destroyed when reloading scene / Switching scenes
        DontDestroyOnLoad(gameObject);
    }
    public void printOk(){}
    void Start()
    {
        Champions = new GameObject[4];
        EnemyChampions = new GameObject[4];
        Champions[0] = ChampionPrefab1;
        Champions[1] = ChampionPrefab2;
        Champions[2] = ChampionPrefab3;
        Champions[3] = ChampionPrefab4;

        EnemyChampions[0] = EnemyChampionPrefab1;
        EnemyChampions[1] = EnemyChampionPrefab2;
        EnemyChampions[2] = EnemyChampionPrefab3;
        EnemyChampions[3] = EnemyChampionPrefab4;

        EnemyChampionsPerLevel = new int[LevelNumbers, 9];
        for (int LevelIndex = 0; LevelIndex < LevelNumbers; LevelIndex++)
        {
            for (int championIndex = 0; championIndex < 9; championIndex++)
            {
                EnemyChampionsPerLevel[LevelIndex, championIndex] = -1;
            }
            
        }
        EnemyChampionsPerLevel[0, 0] = 1;
        EnemyChampionsPerLevel[1, 0] = 0;
        EnemyChampionsPerLevel[1, 5] = 2;
        EnemyChampionsPerLevel[1, 3] = 3;
    }
    public int GetLevelNumbers() {
        return this.LevelNumbers;
    }
    public GameController GetGameController()
    {
        return this.gameController;
    }
    public ChampionPrepController GetChampionPrepController()
    {
        return this.championPrepController;
    }

    public PlayerData GetPlayer(){
        return this.playerData;
    }
    public void SetPlayerUsername(string username){
        this.username = username;
    }
    public void Battle()
    {
        fighting = true;
        championPrepController = null;
        gameController = new GameController();    
    }
    public void EndOfBattle(bool PlayerWon)
    {
        UnityEngine.Debug.Log("end of battle");
        if(fighting == true) {
            this.PlayerWonTheFight = PlayerWon;
            fighting = false;
            gameController = null;
            championPrepController = new ChampionPrepController();
            playerData.GetPlacementData().removeEnemyChampionsInstances();
            this.desactivateSpawners("EnemySpawnPositions");
            if(this.PlayerWonTheFight) this.PlayerWon();
            else this.PlayerLost();
            UnityEngine.Debug.Log(this.GameLevel);
            UnityEngine.Debug.Log(PlayerWon);
        }
    }
    private void PlayerWon() {
        this.GameLevel++;
        playerData.AddGold(10);
        playerData.AddXp(2);
    }
    private void PlayerLost() {
        playerData.AddHealth(-10);
        playerData.AddXp(2);
    }
    public void Play()
    {
        fighting = false;
        this.playerData = new PlayerData(username);
        SceneManager.LoadScene(1);
        championPrepController = new ChampionPrepController();
        championPrepController.RespawnAllies();
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void Pause()
    {
        UnityEngine.Debug.Log("pause");
        //reset
        desactivateSpawners("SpawnPositions");
        desactivateSpawners("EnemySpawnPositions");
        SceneManager.LoadScene(0);
    }
    public Champion GetChampion(int index)
    {
        return Champions[index].GetComponent<Champion>();

    }
    public GameObject[] GetChampions()
    {
        return this.Champions;
    }
    public GameObject GetEnemyChampion(int i)
    {
        Debug.Log("index to spanw : "+ i);
        return this.EnemyChampions[i];
    }
    public List<int> GetLevelEnemyChampionsIndex(int levelToLoad){
        List<int> IndexList= new List<int>();
        UnityEngine.Debug.Log("spawning enemies on : " + this.EnemyChampionsPerLevel.GetLength(1) + "array index");
        for (int i = 0; i < this.EnemyChampionsPerLevel.GetLength(1); i++)
        {
            IndexList.Add(this.EnemyChampionsPerLevel[levelToLoad, i]);
        }
        return IndexList;
    }
    public void desactivateSpawners( string toreset){
        ChampionSpawner Spawner;
        Transform SpawnPositions = transform.Find(toreset);
        foreach (Transform SP in SpawnPositions)
        {
             Spawner = null;
             Spawner = SP.gameObject.GetComponent<ChampionSpawner>();
             if (Spawner != null){
                 Spawner.desactivateSpawnPoint();
             }
        }
    }
}
