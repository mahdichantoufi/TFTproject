using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementData
{
    public class ChampInstanceData{
        string SpawnName;
        bool substitute;
        int PrefabIndex;
        GameObject ChampionInstance;
        public ChampInstanceData (string spawnName, GameObject championInstance, int prefabIndex, bool fighter){
            this.SpawnName = spawnName;
            this.ChampionInstance = championInstance;
            this.PrefabIndex = prefabIndex;
            this.substitute = !fighter;
        }
    }
    private int maximumFighterNb;
    private int actualFighterNb;
    private int maximumSubstitutesNb;
    private int actualSubstitutesNb;
    private ChampInstanceData[] Fighters;
    private ChampInstanceData[] Substitutes;


    public PlacementData(int NumberOfFightingSpawns, int NumberOfSubsSpawns)
    {
        maximumFighterNb = NumberOfFightingSpawns;
        actualFighterNb = 0;

        maximumSubstitutesNb = NumberOfSubsSpawns;
        actualSubstitutesNb = 0;
    }
    public void addChampion(int PrefabsIndex){

    }
    public void storeChampionInstance(GameObject ChampionInstance, int SpawnIndex){

    }
    public bool isThereAnyFreeSpawnPoints(){
        return !((actualFighterNb == maximumFighterNb) && (actualSubstitutesNb == maximumSubstitutesNb));
    }
}
