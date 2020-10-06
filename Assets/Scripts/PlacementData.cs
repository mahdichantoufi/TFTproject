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
    public GameObject getSpawnActiveInstance(string spawnNameFrom){
        if(findInFightersByName(spawnNameFrom)){
            return findInFightersByNameprivate(spawnNameFrom).ChampionInstance;
        }
        else if(findInSubstitutesByName(spawnNameFrom)){
            return findInSubstitutesByNameprivate(spawnNameFrom).ChampionInstance;
        }
        return null;
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
    public void setSpawner(string spawnNameFrom, string spawnNameTo, Vector3 spawnerPositionTo, bool ToIsFighterSpawn){
        if(findInFightersByName(spawnNameFrom) && ToIsFighterSpawn){ // From fighter spawn to fighter spawn
            ChampInstanceData champData = findInFightersByNameprivate(spawnNameFrom);
            champData.SpawnName = spawnNameTo;
            champData.ChampionInstance.transform.position = spawnerPositionTo;
        }
        else if(findInFightersByName(spawnNameFrom) && !ToIsFighterSpawn){ // From Fighter to Sub
            ChampInstanceData champData = findInFightersByNameprivate(spawnNameFrom);
            Fighters.Remove(champData);
            champData.SpawnName = spawnNameTo;
            champData.ChampionInstance.transform.position = spawnerPositionTo;
            Substitutes.Add(champData);
        }
        else if(findInSubstitutesByName(spawnNameFrom)){// From Sub to Fighter
            ChampInstanceData champData = findInSubstitutesByNameprivate(spawnNameFrom);
            Substitutes.Remove(champData);
            champData.SpawnName = spawnNameTo;
            champData.ChampionInstance.transform.position = spawnerPositionTo;
            Fighters.Add(champData);
        }
    }
    public void switchSpawners(GameObject spawnerFrom, GameObject spawnerTo){
        bool FromIsFighterSpawn = spawnerFrom.GetComponent<ChampionSpawner>().FightingSpawner;
        bool ToIsFighterSpawn = spawnerTo.GetComponent<ChampionSpawner>().FightingSpawner;
        if(FromIsFighterSpawn && ToIsFighterSpawn){ // both fighter spawns
            ChampInstanceData champDataFrom = findInFightersByNameprivate(spawnerFrom.name);
            ChampInstanceData champDataTo = findInFightersByNameprivate(spawnerTo.name);
            champDataFrom.SpawnName = spawnerTo.name;
            champDataFrom.ChampionInstance.transform.position = spawnerTo.transform.position;
            champDataTo.SpawnName = spawnerFrom.name;
            champDataTo.ChampionInstance.transform.position = spawnerFrom.transform.position;

        }
        else if(!ToIsFighterSpawn){ // From Fighter to Sub
            ChampInstanceData champDataFrom = findInFightersByNameprivate(spawnerFrom.name);
            Fighters.Remove(champDataFrom);
            champDataFrom.SpawnName = spawnerTo.name;
            champDataFrom.ChampionInstance.transform.position = spawnerTo.transform.position;
            
            ChampInstanceData champDataTo = findInSubstitutesByNameprivate(spawnerTo.name);
            Substitutes.Remove(champDataTo);
            champDataTo.SpawnName = spawnerFrom.name;
            champDataTo.ChampionInstance.transform.position = spawnerFrom.transform.position;

            Substitutes.Add(champDataFrom);
            Fighters.Add(champDataTo);
        }
        else {// From Sub to Fighter
            ChampInstanceData champDataFrom = findInSubstitutesByNameprivate(spawnerFrom.name);
            Fighters.Remove(champDataFrom);
            champDataFrom.SpawnName = spawnerTo.name;
            champDataFrom.ChampionInstance.transform.position = spawnerTo.transform.position;
            
            ChampInstanceData champDataTo = findInFightersByNameprivate(spawnerTo.name);
            Substitutes.Remove(champDataTo);
            champDataTo.SpawnName = spawnerFrom.name;
            champDataTo.ChampionInstance.transform.position = spawnerFrom.transform.position;

            Substitutes.Add(champDataFrom);
            Fighters.Add(champDataTo);
        }
    }
    public int getFightingChampsNumber(){
        return this.Fighters.Count;
    }
    public void deleteChampionWithSpawnName(string spawnNameToDelete){
        if(findInFightersByName(spawnNameToDelete)){
            Fighters.Remove(findInFightersByNameprivate(spawnNameToDelete));
        }
        else if(findInSubstitutesByName(spawnNameToDelete)){
            Substitutes.Remove(findInSubstitutesByNameprivate(spawnNameToDelete));
        }

    }

    private ChampInstanceData findInFightersByNameprivate(string spawnNameFrom){

        foreach (ChampInstanceData CID in Fighters)
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

        foreach (ChampInstanceData CID in Fighters)
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
        return this.findInSubstitutesByName(spawner) || this.findInFightersByName(spawner);
    }

}
