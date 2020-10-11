using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementData
{
    private int maximumEnemyNb;
    private int maximumFighterNb;
    private int actualFighterNb;
    private int maximumSubstitutesNb;
    private int actualSubstitutesNb;

    public class ChampInstanceData{
        public string SpawnName;
        public Vector3 SpawnPosition;
        public int PrefabIndex;
        public GameObject ChampionInstance;
        public ChampInstanceData (string spawnName, Vector3 spawnPosition, GameObject championInstance, int prefabIndex){
            this.SpawnName = spawnName;
            this.ChampionInstance = championInstance;
            this.PrefabIndex = prefabIndex;
            this.SpawnPosition = spawnPosition;
        }
        public int getChampIndex(){
            return this.PrefabIndex;
        }
        public void printPref(){
            Debug.Log("Spawner :"+this.SpawnName+" => "+this.PrefabIndex);
        }
    }
    private List<ChampInstanceData> Fighters;
    private List<ChampInstanceData> Substitutes;
    private List<ChampInstanceData> Enemies;


    public PlacementData(int NumberOfFightingSpawns, int NumberOfSubsSpawns, int NumberOfEnemySpawns)
    {
        Fighters = new List<ChampInstanceData>();
        maximumFighterNb = NumberOfFightingSpawns;
        actualFighterNb = 0;

        Substitutes = new List<ChampInstanceData>();
        maximumSubstitutesNb = NumberOfSubsSpawns;
        actualSubstitutesNb = 0;

        Enemies = new List<ChampInstanceData>();
        maximumEnemyNb = NumberOfEnemySpawns;
    }
    public bool isThereAnyFreeSpawnPoints(){
        return !(actualSubstitutesNb == maximumSubstitutesNb);
    }
    public GameObject GetSpawnActiveInstance(string spawnNameFrom){
        if(findInFightersByName(spawnNameFrom)){
            return findInFightersByNameprivate(spawnNameFrom).ChampionInstance;
        }
        else if(findInSubstitutesByName(spawnNameFrom)){
            return findInSubstitutesByNameprivate(spawnNameFrom).ChampionInstance;
        }
        else if(findInEnemiesByName(spawnNameFrom)){
            return findInEnemiesByNameprivate(spawnNameFrom).ChampionInstance;
        }
        return null;
    }

    public int GetChampionPrefabIndex(string spawnName){
        if(findInFightersByName(spawnName)){
            return findInFightersByNameprivate(spawnName).getChampIndex();
        }
        return -1;
    }
    public void addChampionInstance(
        string spawnName,  Vector3 spawnPosistion,
        GameObject championInstance,
        int prefabIndex, 
        bool fighter)
    {
        ChampInstanceData newChampInstance= new ChampInstanceData(spawnName, spawnPosistion, championInstance, prefabIndex);
        if (isThereAnyFreeSpawnPoints() && newChampInstance != null) {
            if (fighter) {
                Fighters.Add(newChampInstance);
                actualFighterNb += 1;
            } else {
                Substitutes.Add(newChampInstance);
                actualSubstitutesNb += 1;
            }
        }
    }
    //TODO : drag and drop disable for enemies
    public void addEnemyChampionInstance(
        string spawnName,  Vector3 spawnPosistion,
        GameObject championInstance, 
        int prefabIndex)
    {
        //UnityEngine.Debug.Log(spawnName + championInstance + prefabIndex);
        ChampInstanceData newChampInstance= new ChampInstanceData(spawnName, spawnPosistion, championInstance, prefabIndex);
        if (newChampInstance != null) {
            Enemies.Add(newChampInstance);
        }
    }
    public void removeAlliesHealthBar() {
           foreach (ChampInstanceData Ally in this.Fighters)
        {
            Ally.ChampionInstance.GetComponent<HealthBar>().destroyHealthBar();
        }
    }
    public void removeEnemyChampionsInstances()
    {
        foreach (ChampInstanceData Enemy in this.Enemies)
        {
            Enemy.ChampionInstance.GetComponent<HealthBar>().destroyHealthBar();
            UnityEngine.Object.Destroy(Enemy.ChampionInstance);
        }
        this.Enemies = new List<ChampInstanceData>();
    }
    public void setSpawner(string spawnNameFrom, string spawnNameTo, Vector3 spawnerPositionTo, bool ToIsFighterSpawn){
        if(ToIsFighterSpawn && findInFightersByName(spawnNameFrom)){ // From fighter spawn to fighter spawn
            ChampInstanceData champData = findInFightersByNameprivate(spawnNameFrom);
            champData.SpawnName = spawnNameTo;
            champData.ChampionInstance.transform.position = spawnerPositionTo;
        }
        else if(!ToIsFighterSpawn && findInFightersByName(spawnNameFrom)){ // From Fighter to Sub
            ChampInstanceData champData = findInFightersByNameprivate(spawnNameFrom);
            Fighters.Remove(champData);
            champData.SpawnName = spawnNameTo;
            champData.ChampionInstance.transform.position = spawnerPositionTo;
            Substitutes.Add(champData);

            actualFighterNb -= 1;
            actualSubstitutesNb += 1;
        }
        else if(ToIsFighterSpawn && findInSubstitutesByName(spawnNameFrom)){// From Sub to Fighter
            ChampInstanceData champData = findInSubstitutesByNameprivate(spawnNameFrom);
            Substitutes.Remove(champData);
            champData.SpawnName = spawnNameTo;
            champData.ChampionInstance.transform.position = spawnerPositionTo;
            Fighters.Add(champData);
            actualFighterNb += 1;
            actualSubstitutesNb -= 1;
        }
        else if(findInSubstitutesByName(spawnNameFrom)){// From Sub to sub
            ChampInstanceData champData = findInSubstitutesByNameprivate(spawnNameFrom);
            champData.SpawnName = spawnNameTo;
            champData.ChampionInstance.transform.position = spawnerPositionTo;
        }
        // this.printME();
    }
    public void switchSpawners(GameObject spawnerFrom, GameObject spawnerTo){
        bool FromIsFighterSpawn = spawnerFrom.GetComponent<ChampionSpawner>().FightingSpawner;
        bool ToIsFighterSpawn = spawnerTo.GetComponent<ChampionSpawner>().FightingSpawner;

        if(FromIsFighterSpawn && ToIsFighterSpawn){ 
            // both fighter spawns
            ChampInstanceData champDataFrom = findInFightersByNameprivate(spawnerFrom.name);
            ChampInstanceData champDataTo = findInFightersByNameprivate(spawnerTo.name);
            champDataFrom.SpawnName = spawnerTo.name;
            champDataFrom.ChampionInstance.transform.position = spawnerTo.transform.position;
            champDataTo.SpawnName = spawnerFrom.name;
            champDataTo.ChampionInstance.transform.position = spawnerFrom.transform.position;
        } 

        else if(!FromIsFighterSpawn && !ToIsFighterSpawn){ 
            // both Sub spawns
            ChampInstanceData champDataFrom = findInSubstitutesByNameprivate(spawnerFrom.name);
            ChampInstanceData champDataTo = findInSubstitutesByNameprivate(spawnerTo.name);
            champDataFrom.SpawnName = spawnerTo.name;
            champDataFrom.ChampionInstance.transform.position = spawnerTo.transform.position;
            champDataTo.SpawnName = spawnerFrom.name;
            champDataTo.ChampionInstance.transform.position = spawnerFrom.transform.position;
        }

        else if(FromIsFighterSpawn && !ToIsFighterSpawn){ 
            // From Fighter to Sub
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

        else {
            // From Sub to Fighter
            ChampInstanceData champDataFrom = findInSubstitutesByNameprivate(spawnerFrom.name);
            Substitutes.Remove(champDataFrom);
            champDataFrom.SpawnName = spawnerTo.name;
            champDataFrom.ChampionInstance.transform.position = spawnerTo.transform.position;

            ChampInstanceData champDataTo = findInFightersByNameprivate(spawnerTo.name);
            Fighters.Remove(champDataTo);
            champDataTo.SpawnName = spawnerFrom.name;
            champDataTo.ChampionInstance.transform.position = spawnerFrom.transform.position;

            Fighters.Add(champDataFrom);
            Substitutes.Add(champDataTo);
        }
        // this.printME();
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
    private ChampInstanceData findInEnemiesByNameprivate(string spawnNameFrom)
    {

        foreach (ChampInstanceData CID in Enemies)
        {
            if (CID.SpawnName == spawnNameFrom)
            {
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
    public bool findInEnemiesByName(string spawnNameFrom)
    {

        foreach (ChampInstanceData CID in Enemies)
        {
            if (CID.SpawnName == spawnNameFrom)
            {
                return true;
            }
        }
        return false;
    }
    public bool findBySpawnerName(string spawner){
        return this.findInSubstitutesByName(spawner) || this.findInFightersByName(spawner);
    }
    private void printME(){
        Debug.Log("Fighters :"+actualFighterNb+"/"+maximumFighterNb);
        Fighters.ForEach((e) => e.printPref());
        Debug.Log("Substitutes :"+actualSubstitutesNb+"/"+maximumSubstitutesNb);
        Substitutes.ForEach((e) => e.printPref());
    }
}
