using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController
{
    protected PlayerData playerData;

    public bool gameOver = false;
    // public GameObject labelGameOver;
    // public Text labelScore;

    protected Transform transform;

    public GameController()
    {
        // TODO : getPlayerChampionsDetails();
        // TODO : getEnemyChampionsDetails();
        transform = GameManager.instance.transform;
        playerData = GameManager.instance.GetPlayer();
        UnityEngine.Debug.Log(playerData);
        SpawnFightingChampions();
        SpawnEnemyChampions();
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
                Spawner.spawnChampion(GameManager.instance.GetChampions()[i]);

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
                Spawner.spawnChampion(GameManager.instance.GetEnemyChampions()[i]);

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
 
}
