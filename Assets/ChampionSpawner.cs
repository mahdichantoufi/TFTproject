using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChampionSpawner : MonoBehaviour
{
    public bool championIsActive;
    // public gameObject Champs;
    private bool championPopped = false;
    private bool containsChampion = false;
    private int ChampionID;
    private Vector3 SpawnPosition;

    private void Start() {
        SpawnPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }
    public void spawnChampion(GameObject ChampionPrefab)
    {
        if(!championPopped && containsChampion){
            GameObject Champ = Instantiate(ChampionPrefab, SpawnPosition, Quaternion.identity);//,Champs);
            containsChampion = true;
            championPopped = true;
            if(championIsActive){
                Champ.gameObject.GetComponent<Champion>().setActive();
            }
        }

    }
    public void spawnInactiveChampion(GameObject ChampionPrefab)
    {
        if(!championPopped && containsChampion){
            Instantiate(ChampionPrefab, SpawnPosition, Quaternion.identity);//,Champs);
            championPopped = true;
        }
    }
    
    public void activateSpawnPoint(){
        containsChampion = true;
    }
}
