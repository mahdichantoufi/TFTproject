using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Reflection;

public class ChampionPrepController : GameController
{
    private void Start()
    {
        initChampions();
        initDragandDrop();
        playerData = GameObject.FindWithTag("GameManager")
            .transform.GetComponent<GameManager>()
                .GetPlayer();
        addPurchasedChampion(Random.Range(0, 4));
        // SpawnFightingChampions();
        // SpawnSubstituteChampions();

    }
    public void addPurchasedChampion(int prefabIndex){

        ChampionSpawner SubSpawner = getFirstAvailableSubsituteSpawnPoint();
        Debug.Log(SubSpawner);
        if (SubSpawner != null){
            SubSpawner.activateSpawnPoint();
            GameObject instanceP = SubSpawner.spawnChampion(Champions[prefabIndex]);
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

    public void switchChampionsInstances(GameObject From, GameObject To){
        bool foundFrom = false;
        bool foundTo = false;

        foundFrom = playerData.GetPlacementData().findBySpawnerName(From.name);
        if(foundFrom){
            foundTo = playerData.GetPlacementData().findBySpawnerName(To.name);
            // Check if found
            // From and To : Switch the SpawnNames
            // From and !To : Change the SpawnName
            // !From : Error
            if (foundFrom && !foundTo) {
                From.GetComponent<ChampionSpawner>().desactivateSpawnPoint();
                To.GetComponent<ChampionSpawner>().activateSpawnPoint();
                playerData.GetPlacementData().setSpawner(From.name, To.name, To.transform.position);
            } else if (foundFrom && foundTo) {
                playerData.GetPlacementData().setSpawner(From.name, To.name, To.transform.position);
                playerData.GetPlacementData().setSpawner(To.name, From.name, From.transform.position);
            }
        }
        
    }
    public void UpExperience()
    {
        if (this.playerData.GetGold() >= 4)
        {
            this.playerData.AddGold(-4);
            this.playerData.AddXp(2);
        }
    }

    private void initDragandDrop(){
        transform.GetComponent<DragAndDrop>().setController(this);
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
                Spawner.spawnChampion(Champions[i]);

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
