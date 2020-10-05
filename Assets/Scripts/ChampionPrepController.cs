using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Reflection;

public class ChampionPrepController : GameController
{
    // TODO : GET CHAMPS FROM GLABAL WITH
    private PlayerData playerData;
    private void Start()
  {
    initChampions();
    initDragandDrop();

    SpawnFightingChampions();
    SpawnSubstituteChampions();
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
