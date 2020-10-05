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
    public GameObject spawnChampion(GameObject ChampionPrefab)
    {
        if(!championPopped && containsChampion){
            GameObject championInstance = Instantiate(ChampionPrefab, SpawnPosition, Quaternion.identity);//,Champs);
            containsChampion = true;
            championPopped = true;
            if(championIsActive){
                championInstance.gameObject.GetComponent<Champion>().setActive();
            }
            return championInstance;
        }
        return null;
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
