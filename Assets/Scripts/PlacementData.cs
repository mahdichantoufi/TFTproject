using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementData
{
    private int maximumFighterNb;
    private int actualFighterNb;
    private int maximumSubstitutesNb;
    private int actualSubstitutesNb;

    public class ChampInstanceData{
        public string SpawnName;
        public int PrefabIndex;
        public GameObject ChampionInstance;
        public ChampInstanceData (string spawnName, GameObject championInstance, int prefabIndex){
            this.SpawnName = spawnName;
            this.ChampionInstance = championInstance;
            this.PrefabIndex = prefabIndex;
        }
    }
    private List<ChampInstanceData> Fighters;
    private List<ChampInstanceData> Substitutes;


    public PlacementData(int NumberOfFightingSpawns, int NumberOfSubsSpawns)
    {
        Fighters = new List<ChampInstanceData>();
        maximumFighterNb = NumberOfFightingSpawns;
        actualFighterNb = 0;

        Substitutes = new List<ChampInstanceData>();
        maximumSubstitutesNb = NumberOfSubsSpawns;
        actualSubstitutesNb = 0;
    }
    public bool isThereAnyFreeSpawnPoints(){
        return !((actualFighterNb == maximumFighterNb) && (actualSubstitutesNb == maximumSubstitutesNb));
    }
    
    public void addChampionInstance(
        string spawnName, 
        GameObject championInstance, 
        int prefabIndex, 
        bool fighter)
    {
        ChampInstanceData newChampInstance= new ChampInstanceData(spawnName, championInstance, prefabIndex);
        if (isThereAnyFreeSpawnPoints() && newChampInstance != null) {
            if (fighter) {
                Fighters.Add(newChampInstance);
            } else {
                Substitutes.Add(newChampInstance);
            }
        }
    }
    public void setSpawner(string spawnNameFrom, string spawnNameTo, Vector3 spawnerPositionTo){
        Debug.Log("moving ... from "+spawnNameFrom+" to "+spawnNameTo+" in pos "+spawnerPositionTo);
        if(findInFightersByName(spawnNameFrom)){
            Debug.Log("fighter");
            ChampInstanceData champData = findInFightersByNameprivate(spawnNameFrom);
            champData.SpawnName = spawnNameTo;
            champData.ChampionInstance.transform.position = spawnerPositionTo;
        }
        else if(findInSubstitutesByName(spawnNameFrom)){
            Debug.Log("sub");
            ChampInstanceData champData = findInSubstitutesByNameprivate(spawnNameFrom);
            champData.SpawnName = spawnNameTo;
            champData.ChampionInstance.transform.position = spawnerPositionTo;
        }
    }
    public void deleteChampionWithSpawnName(string SpawnerName){

    }
    private ChampInstanceData findInFightersByNameprivate(string spawnNameFrom){

        foreach (ChampInstanceData CID in Substitutes)
        {
            if (CID.SpawnName == spawnNameFrom){
                return CID;
            }
        }
        return null;
    }
    private ChampInstanceData findInSubstitutesByNameprivate(string spawnNameFrom){

        foreach (ChampInstanceData CID in Substitutes)
        {
            if (CID.SpawnName == spawnNameFrom){
                return CID;
            }
        }
        return null;
    }

    public bool findInFightersByName(string spawnNameFrom){

        foreach (ChampInstanceData CID in Substitutes)
        {
            if (CID.SpawnName == spawnNameFrom){
                return true;
            }
        }
        return false;
    }
    public bool findInSubstitutesByName(string spawnNameFrom){

        foreach (ChampInstanceData CID in Substitutes)
        {
            if (CID.SpawnName == spawnNameFrom){
                return true;
            }
        }
        return false;
    }
    public bool findBySpawnerName(string spawner){
        return this.findInSubstitutesByName(spawner) && this.findInFightersByName(spawner);
    }

}
