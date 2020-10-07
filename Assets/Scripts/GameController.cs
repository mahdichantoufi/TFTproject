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
        // TODO : getEnemyChampionsDetails();
        transform = GameManager.instance.transform;
        playerData = GameManager.instance.GetPlayer();
       
    }
    public void init()
    {
        ActivateFightingChampions();
        // TODO : Activate Enemies
        //SpawnEnemyChampions();
    }

    public void ActivateFightingChampions(){
        Transform AllySpawnPositions = transform.Find("SpawnPositions");
        //playerData.GetPlacementData().printME();
        foreach (Transform AllySP in AllySpawnPositions)
        {
            GameObject InstanceChampion = playerData.GetPlacementData().GetSpawnActiveInstance(AllySP.gameObject.name);
            if (InstanceChampion != null){
                InstanceChampion.GetComponent<Champion>().setActive();
            }
        }
        Transform EnemySpawnPositions = transform.Find("EnemySpawnPositions");
        //playerData.GetPlacementData().printME();
        foreach (Transform EnemySP in EnemySpawnPositions)
        {
            GameObject InstanceChampion = playerData.GetPlacementData().GetSpawnActiveInstance(EnemySP.gameObject.name);
            if (InstanceChampion != null)
            {
                InstanceChampion.GetComponent<Champion>().setActive();
            }
        }
    }
   /* public void ActivateFightingChampions(){
        Transform AllySpawnPositions = transform.Find("SpawnPositions");
        //playerData.GetPlacementData().printME();
        foreach (Transform AllySP in AllySpawnPositions)
        {
            GameObject InstanceChampion = playerData.GetPlacementData().GetSpawnActiveInstance(AllySP.gameObject.name);
            if (InstanceChampion != null){
                InstanceChampion.GetComponent<Champion>().setActive();
            }
        }
    }*/
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
                Spawner.spawnChampion(GameManager.instance.GetEnemyChampion(1));
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
