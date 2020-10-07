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
        gameController = new GameController();
        championPrepController = null;
        //SceneManager.LoadScene(2);
        gameController.init();
    }
    public void EndOfBattle()
    {
        gameController = null;
        championPrepController = new ChampionPrepController();
        SceneManager.LoadScene(1);

    }
    public void Play()
    {
        this.playerData = new PlayerData(username);
        championPrepController = new ChampionPrepController();
        SceneManager.LoadScene(1);
    }

    public void BackToMenu()
    {
        UnityEngine.Debug.Log("back");
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }
    public void Pause()
    {
        UnityEngine.Debug.Log("pause");
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
    public GameObject[] GetEnemyChampions()
    {
        return this.EnemyChampions;
    }


}
