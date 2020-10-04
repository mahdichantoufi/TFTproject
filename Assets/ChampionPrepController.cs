using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChampionPrepController : GameController
{
    private void Start() {
        initChampions();
        SpawnFightingChampions();
    }
    public override void SpawnFightingChampions(){

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

        Transform AllySubsSpawnPositions = transform.Find("SubsSpawnPositions");
        // AllySpawnPositions.Find("Spawn1").gameObject.GetComponent<ChampionSpawner>().activateSpawnPoint();
        // AllySpawnPositions.Find("Spawn2").gameObject.GetComponent<ChampionSpawner>().activateSpawnPoint();
        // AllySpawnPositions.Find("Spawn3").gameObject.GetComponent<ChampionSpawner>().activateSpawnPoint();
        // AllySpawnPositions.Find("Spawn4").gameObject.GetComponent<ChampionSpawner>().activateSpawnPoint();
        i = 0;
        foreach (Transform AllySP in AllySubsSpawnPositions)
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
    }
}
