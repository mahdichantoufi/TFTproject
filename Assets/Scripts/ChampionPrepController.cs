﻿using System.Collections;
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

    }
    public void RespawnAllies(){
        Transform SpawnPositions = transform.Find("SpawnPositions");
        foreach (Transform SP in SpawnPositions)
        {
            GameObject ChampionInstance = playerData.GetPlacementData().GetSpawnActiveInstance(SP.gameObject.name);
            if (ChampionInstance != null){
                Debug.Log("Champion is ok "+ ChampionInstance.name);
                Debug.Log("SPPOs "+ SP.position);
                ChampionInstance.gameObject.SetActive(true);
                ChampionInstance.transform.position = SP.position;
            }
        }
    }
    // TODO : SpawnEnemyChampions()
    // TODO : SpawnEnemyChampions()
    public void SpawnEnemyChampions(int levelToLoad){
        Debug.Log(levelToLoad);
        List<int> IndexesToSpawn = GameManager.instance.GetLevelEnemyChampionsIndex(levelToLoad);
        int EnemiesCount = IndexesToSpawn.Count;
        int i = 0;
        ChampionSpawner Spawner;
        Transform EnemySpawnPositions = transform.Find("EnemySpawnPositions");
         foreach (Transform EnemySP in EnemySpawnPositions)
         {
            Spawner = null;
            Spawner = EnemySP.gameObject.GetComponent<ChampionSpawner>();
            if (Spawner != null && EnemiesCount > i){
             
                int prefabIndex = IndexesToSpawn[i];
                if (prefabIndex > -1){

                    GameObject instanceP = Spawner.spawnChampion(GameManager.instance.GetEnemyChampion(prefabIndex));

                    playerData.GetPlacementData().addEnemyChampionInstance(
                    Spawner.gameObject.name, Spawner.transform.position, 
                    instanceP, prefabIndex);
                    instanceP.gameObject.SetActive(true);
                }
                i++;
            }
        }
     }

    public void addPurchasedChampion(int prefabIndex){

        ChampionSpawner SubSpawner = getFirstAvailableSubsituteSpawnPoint();
        
        if (SubSpawner != null){
            GameObject instanceP = SubSpawner.spawnChampion(GameManager.instance.GetChampions()[prefabIndex]);
            playerData.GetPlacementData().addChampionInstance(
                SubSpawner.gameObject.name, SubSpawner.transform.position,
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
