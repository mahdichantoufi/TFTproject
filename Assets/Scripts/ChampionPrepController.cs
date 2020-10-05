using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChampionPrepController : GameController
{
    public TextMeshProUGUI username;
    public TextMeshProUGUI gold;
    public TextMeshProUGUI xp;
    // TODO : GET CHAMPS FROM GLABAL WITH
    private PlayerData playerData;
    private void Start()
  {
    initChampions();
    initDragandDrop();
    getPlayerChampionsDetails();

    SpawnFightingChampions();
    SpawnSubstituteChampions();
  }

  private void getPlayerChampionsDetails()
  {
    playerData = GameObject.FindWithTag("GameManager").transform.GetComponent<GameManager>().GetPlayer();
    playerData.LogPrint();
    xp.text = "XP : " + playerData.GetXp().ToString();
    gold.text = "Gold : " + playerData.GetGold().ToString();
    username.text = "Hello " + playerData.GetUsername() + " :D";
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
