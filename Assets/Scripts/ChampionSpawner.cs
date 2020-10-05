using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChampionSpawner : MonoBehaviour
{
    public bool championIsActive;
    
    private bool championPopped = false;
    private bool containsChampion = false;
    private Vector3 SpawnPosition;
    private GameObject ChampionPrefab;

    private void Start() {
        SpawnPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }
    public void spawnChampion(GameObject ChampionPrefab)
    {
        if(!championPopped && containsChampion){
            ChampionPrefab = Instantiate(ChampionPrefab, SpawnPosition, Quaternion.identity);//,Champs);
            containsChampion = true;
            championPopped = true;
            if(championIsActive){
                ChampionPrefab.gameObject.GetComponent<Champion>().setActive();
            }
        }

    }
    
    public void activateSpawnPoint(){
        containsChampion = true;
    }
    public void desactivateSpawnPoint(){
        Destroy(ChampionPrefab.gameObject);
        ChampionPrefab = null;
        championPopped = false;
        containsChampion = false;
    }
    public bool spawnPointIsActive(){
        return containsChampion;
    }
    public bool spawnedChampionIsDraggable(){
        return (containsChampion && !championIsActive);
    }
}
