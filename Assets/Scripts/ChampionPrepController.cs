using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Reflection;
using System.Security.Cryptography;

public class ChampionPrepController
{
    protected PlayerData playerData;
    protected Transform transform;
    public ChampionPrepController()
    {
        transform = GameManager.instance.transform;
        playerData = GameManager.instance.GetPlayer();
        //SpawnEnemyChampions();
    }

    // TODO : SpawnEnemyChampions()
    // TODO : SpawnEnemyChampions()
    public void SpawnEnemyChampions(){
        int prefabIndex = 1;

        ChampionSpawner Spawner;
        Transform EnemySpawnPositions = transform.Find("EnemySpawnPositions");
        //Spawner = EnemySpawnPositions.GetChild(0).GetComponent<ChampionSpawner>();
        //Debug.Log(Spawner);
        //GameObject instanceP = Spawner.spawnChampion(GameManager.instance.GetEnemyChampion(prefabIndex));
        //UnityEngine.Debug.Log(EnemyChampions[1].gameObject.GetComponent<Champion>().championName);
         
         foreach (Transform EnemySP in EnemySpawnPositions)
         {
             Spawner = null;
             Spawner = EnemySP.gameObject.GetComponent<ChampionSpawner>();
             if (Spawner != null){
                GameObject instanceP = Spawner.spawnChampion(GameManager.instance.GetEnemyChampion(prefabIndex));
                UnityEngine.Debug.Log(instanceP.gameObject.GetComponent<Champion>().championName);
                playerData.GetPlacementData().addEnemyChampionInstance(
                Spawner.gameObject.name, 
                instanceP, prefabIndex);
                instanceP.gameObject.SetActive(true);
             }
        }
         Debug.Log("Done... EnemiesNb: ");
     }

    // TODO : SpawnFightingChampions
    // public void SpawnFightingChampions(){
    //     ChampionSpawner Spawner;
    //     Transform AllySpawnPositions = transform.Find("FightSpawnPositions");
    //     //playerData.GetPlacementData().printME();
    //     Debug.Log("allies index :");
    //     foreach (Transform AllySP in AllySpawnPositions)
    //     {
    //         Spawner = null;
    //         Spawner = AllySP.gameObject.GetComponent<ChampionSpawner>();
    //         int index = playerData.GetPlacementData().GetChampionPrefabIndex(AllySP.gameObject.name);
    //         UnityEngine.Debug.Log(index);
    //         UnityEngine.Debug.Log(Spawner);
    //         UnityEngine.Debug.Log(AllySP);
    //         if (Spawner != null && index != -1){
    //             Debug.Log("+"+index);
    //             Spawner.spawnChampion(GameManager.instance.GetChampions()[index+1]);
    //         }
    //     }
    // }


    public void addPurchasedChampion(int prefabIndex){

        ChampionSpawner SubSpawner = getFirstAvailableSubsituteSpawnPoint();
        
        if (SubSpawner != null){
            GameObject instanceP = SubSpawner.spawnChampion(GameManager.instance.GetChampions()[prefabIndex]);
            playerData.GetPlacementData().addChampionInstance(
                SubSpawner.gameObject.name, 
                instanceP, prefabIndex, false);
        }
    }
    public ChampionSpawner getFirstAvailableSubsituteSpawnPoint(){
        ChampionSpawner Spawner;
        Transform AllySubsSpawnPositions = transform.Find("SubsSpawnPositions");
        foreach (Transform AllySP in AllySubsSpawnPositions)
        {
            Spawner = null;
            Spawner = AllySP.gameObject.GetComponent<ChampionSpawner>();
            if (Spawner != null && !Spawner.spawnPointIsActive()){
                return Spawner;
            }
        }
        return null;
    }
    public bool switchChampionsInstances(GameObject From, GameObject To){
        bool foundFrom = false;
        bool foundTo = false;
        ChampionSpawner FromScript = From.GetComponent<ChampionSpawner>();
        ChampionSpawner ToScript = To.GetComponent<ChampionSpawner>();
        
        foundFrom = playerData.GetPlacementData().findBySpawnerName(From.name);
        if(foundFrom){
            // SpawnFrom contient un champion
            if (!playerData.canMoveBasedOnLevel(FromScript.FightingSpawner, ToScript.FightingSpawner, ToScript.spawnPointIsActive()))
                return false;
                // Le player a placé autant de champions sur le terrain qu'autorisés par son level

            foundTo = playerData.GetPlacementData().findBySpawnerName(To.name);
            if (!foundTo) {
                // SpawnFrom contient un champion mais pas SpawnTo
                FromScript.desactivateSpawnPoint();
                ToScript.activateSpawnPoint();
                playerData.GetPlacementData().setSpawner(From.name, To.name, To.transform.position, ToScript.FightingSpawner);
            }
            else {
                // SpawnFrom Et SpawnTo contiennent un champion
                // Check if found
                // From and To : Switch the SpawnNames
                // From and !To : Change the SpawnName
                // !From : Error
                if (From.name == To.name){
                    return false;
                }
                playerData.GetPlacementData().switchSpawners(From, To);
            }
            return true;
        } else 
            Debug.Log("SpawnFrom ne contient pas de champion");
        return false;
    }
    public void SpawnSubstituteChampions(){
        ChampionSpawner Spawner;
        Transform AllySubsSpawnPositions = transform.Find("SubsSpawnPositions");

        int i = 1;
        foreach (Transform AllySP in AllySubsSpawnPositions)
        {
            Spawner = null;
            Spawner = AllySP.gameObject.GetComponent<ChampionSpawner>();
            if (Spawner != null){
                Spawner.activateSpawnPoint();
                Spawner.spawnChampion(GameManager.instance.GetChampions()[i]);

                if (i==1){
                    i = 3;
                }
                else if (i==3){
                    i = 1;
                }
            }
        }
    }

}
