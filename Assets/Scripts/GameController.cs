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

    private Transform transform;

    public GameController()
    {
        // TODO : getEnemyChampionsDetails();
        transform = GameManager.instance.transform;
        playerData = GameManager.instance.GetPlayer();
        ActivateFightingChampions("SpawnPositions");
        ActivateFightingChampions("EnemySpawnPositions");
       
    }

    public void ActivateFightingChampions(string SpanersParentName){
        Transform SpawnPositions = transform.Find(SpanersParentName);
        foreach (Transform SP in SpawnPositions)
        {
            GameObject InstanceChampion = playerData.GetPlacementData().GetSpawnActiveInstance(SP.gameObject.name);
            if (InstanceChampion != null){
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

}
