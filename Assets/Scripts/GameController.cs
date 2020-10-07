using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController
{
    private PlayerData playerData;

    public bool gameOver = false;
    // public GameObject labelGameOver;
    // public Text labelScore;

    private Transform transform;

    public GameController()
    {
        // TODO : getPlayerChampionsDetails();
        // TODO : getEnemyChampionsDetails();
        transform = GameManager.instance.transform;
        playerData = GameManager.instance.GetPlayer();
       
    }
    public void init()
    {
        SpawnFightingChampions();
        SpawnEnemyChampions();
    }

    public void SpawnFightingChampions(){
        ChampionSpawner Spawner;
        Transform AllySpawnPositions = transform.Find("SpawnPositions");
        Debug.Log("allies index :");
        foreach (Transform AllySP in AllySpawnPositions)
        {
            Spawner = null;
            Spawner = AllySP.gameObject.GetComponent<ChampionSpawner>();
            int index = playerData.GetPlacementData().GetChampionPrefabIndex(AllySP.gameObject.name);
            UnityEngine.Debug.Log(index);
            UnityEngine.Debug.Log(Spawner);
            UnityEngine.Debug.Log(AllySP);
            if (Spawner != null && index != -1){
                Debug.Log("+"+index);
                Spawner.spawnChampion(GameManager.instance.GetChampions()[index]);
            }
        }
    }
    public void SpawnEnemyChampions(){
        ChampionSpawner Spawner;
        Transform EnemySpawnPositions = transform.Find("EnemySpawnPositions");
        int i = 0;
        Debug.Log("Spawning enemies...");
        foreach (Transform EnemySP in EnemySpawnPositions)
        {
            Spawner = null;
            Spawner = EnemySP.gameObject.GetComponent<ChampionSpawner>();
            if (Spawner != null){
                Spawner.activateSpawnPoint();
                Spawner.spawnChampion(GameManager.instance.GetEnemyChampions()[1]);
                i++;
            }
        }
        Debug.Log("Done... EnemiesNb: "+i);
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
