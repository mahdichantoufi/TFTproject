using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance;

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
        if(instance==null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    private void Start() {
        initChampions();
        SpawnFightingChampions();
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
    public virtual void SpawnFightingChampions(){

        ChampionSpawner Spawner;

        Transform AllySpawnPositions = transform.Find("SpawnPositions");
        // AllySpawnPositions.Find("Spawn1").gameObject.GetComponent<ChampionSpawner>().activateSpawnPoint();
        // AllySpawnPositions.Find("Spawn2").gameObject.GetComponent<ChampionSpawner>().activateSpawnPoint();
        // AllySpawnPositions.Find("Spawn3").gameObject.GetComponent<ChampionSpawner>().activateSpawnPoint();
        // AllySpawnPositions.Find("Spawn4").gameObject.GetComponent<ChampionSpawner>().activateSpawnPoint();
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

                // i++;
                // if (i==4){
                //     i = 0;
                // }
            }
        }

        Transform EnemySpawnPositions = transform.Find("EnemySpawnPositions");
        // EnemySpawnPositions.Find("Spawn1").gameObject.GetComponent<ChampionSpawner>().activateSpawnPoint();
        // EnemySpawnPositions.Find("Spawn2").gameObject.GetComponent<ChampionSpawner>().activateSpawnPoint();
        // EnemySpawnPositions.Find("Spawn3").gameObject.GetComponent<ChampionSpawner>().activateSpawnPoint();
        // EnemySpawnPositions.Find("Spawn4").gameObject.GetComponent<ChampionSpawner>().activateSpawnPoint();
        i = 1;
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

                // i++;
                // if (i==4){
                //     i = 0;
                // }
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
    }
    // public void BirdScore(){
    //     if(!gameOver){
    //         scoreCount++;
    //         labelScore.text = "Score : " + scoreCount.ToString();
    //     }
    // }
}
