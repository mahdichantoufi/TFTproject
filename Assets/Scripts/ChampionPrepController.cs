using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Reflection;

public class ChampionPrepController : GameController
{
    // TODO : GET CHAMPS FROM GLABAL WITH
    private void Start()
    {
        initChampions();
        initDragandDrop();
        playerData = GameObject.FindWithTag("GameManager")
            .transform.GetComponent<GameManager>()
                .GetPlayer();
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
    public bool switchChampionsInstances(GameObject From, GameObject To){
        bool foundFrom = false;
        bool foundTo = false;
        ChampionSpawner FromScript = From.GetComponent<ChampionSpawner>();
        ChampionSpawner ToScript = To.GetComponent<ChampionSpawner>();
        Debug.Log("can Move ? " + playerData.canMoveBasedOnLevel(FromScript.FightingSpawner, ToScript.FightingSpawner, ToScript.spawnPointIsActive()));

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
                playerData.GetPlacementData().switchSpawners(From, To);
            }
            return true;
        }
        return false;
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
