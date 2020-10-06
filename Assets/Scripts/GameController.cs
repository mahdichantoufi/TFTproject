using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public PlayerData playerData;

    public bool gameOver = false;
    public GameObject ChampionPrefab1;
    public GameObject ChampionPrefab2;
    public GameObject ChampionPrefab3;
    public GameObject ChampionPrefab4;
    public GameObject EnemyChampionPrefab1;
    public GameObject EnemyChampionPrefab2;
    public GameObject EnemyChampionPrefab3;
    public GameObject EnemyChampionPrefab4;
    // public GameObject labelGameOver;
    // public Text labelScore;
    public GameObject[] Champions;
    private GameObject[] EnemyChampions;

    private void Awake()
    {
     
        if (instance==null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    private void Start() {
        // TODO : getPlayerChampionsDetails();
        // TODO : getEnemyChampionsDetails();
        playerData = GameManager.instance
            .transform.GetComponent<GameManager>()
                .GetPlayer();
        initChampions();
        SpawnFightingChampions();
        SpawnEnemyChampions();
    }



    public void initChampions(){
        Champions = new GameObject[4];
        Champions[0] = ChampionPrefab1;
        Champions[1] = ChampionPrefab2;
        Champions[2] = ChampionPrefab3;
        Champions[3] = ChampionPrefab4;
        foreach (GameObject C in Champions)
        {
            C.GetComponent<Champion>().gameController = this;
        }

        EnemyChampions = new GameObject[4];
        EnemyChampions[0] = EnemyChampionPrefab1;
        EnemyChampions[1] = EnemyChampionPrefab2;
        EnemyChampions[2] = EnemyChampionPrefab3;
        EnemyChampions[3] = EnemyChampionPrefab4;
        foreach (GameObject EC in EnemyChampions)
        {
            EC.GetComponent<Champion>().gameController = this;
        }
    }

    public void SpawnFightingChampions(){
        ChampionSpawner Spawner;
        Transform AllySpawnPositions = transform.Find("SpawnPositions");
        
        int i = 0;
        foreach (Transform AllySP in AllySpawnPositions)
        {
            Spawner = null;
            Spawner = AllySP.gameObject.GetComponent<ChampionSpawner>();
            if (Spawner != null){
                Spawner.activateSpawnPoint();
                Spawner.spawnChampion(Champions[i]);

                if (i==0){
                    i = 2;
                }
                else if (i==2){
                    i = 0;
                }
            }
        }
    }
    public void SpawnEnemyChampions(){
        ChampionSpawner Spawner;
        Transform EnemySpawnPositions = transform.Find("EnemySpawnPositions");
        int i = 1;
        foreach (Transform EnemySP in EnemySpawnPositions)
        {
            Spawner = null;
            Spawner = EnemySP.gameObject.GetComponent<ChampionSpawner>();
            if (Spawner != null){
                Spawner.activateSpawnPoint();
                Spawner.spawnChampion(EnemyChampions[i]);

                if (i==1){
                    i = 3;
                }
                else if (i==3){
                    i = 1;
                }
            }
        }
    }

    public void AllEnemiesAreDead(bool Win){
        gameOver = true;
        if(Win){
            Debug.Log("GAMEOVER !! YOU WIN.");
        } else {
            Debug.Log("GAMEOVER !! YOU LOSE.");
        }
        // labelGameOver.SetActive(true);

        // TODO : setNextScene();
    }
    private void getPlayerChampionsDetails(){
        //  foreach spawnpoint
        //      GET the champPrefabs and objects from global var
        //      Store them in class var
    }
    private void getEnemyChampionsDetails(){
        //  foreach Enemy spawnpoint
        //      GET the champPrefabs and objects from global var
        //      Store them in class var
    }
    private void setNextScene(){
        //  foreach spawnpoint
        //      Store the champPrefabs and objects from global var
        //      GET them in class var
        //  Launch next scene (Champ-Select & placement)
    }
    // public void BirdScore(){
    //     if(!gameOver){
    //         scoreCount++;
    //         labelScore.text = "Score : " + scoreCount.ToString();
    //     }
    // }
    public Champion GetChampion(int index)
    {
        return Champions[index].GetComponent<Champion>();

    }
}
