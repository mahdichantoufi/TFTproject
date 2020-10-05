using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementData
{
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
    private int maximumFighterNb;
    private int actualFighterNb;
    private int maximumSubstitutesNb;
    private int actualSubstitutesNb;
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
    public void addChampionInstance(string spawnName, GameObject championInstance, int prefabIndex, bool fighter){
        ChampInstanceData newChampInstance= new ChampInstanceData(spawnName, championInstance, prefabIndex);
        if (isThereAnyFreeSpawnPoints() && newChampInstance != null) {
            if (fighter) {
                Fighters.Add(newChampInstance);
            } else {
                Substitutes.Add(newChampInstance);
            }
        }
    }
    public void switchChampionsInstances(string spawnNameFrom, string spawnNameTo){
        ChampInstanceData spawnDataFrom = null;
        ChampInstanceData spawnDataTo = null;
        bool foundFrom = false;
        bool foundTo = false;

        Debug.Log("button downe on " + spawnNameFrom);
        Debug.Log("button up on " + spawnNameTo);
        // TODO : do the same for substitutes
        // foreach (ChampInstanceData CID in Fighters)
        // {
        //     if (!foundFrom && CID.SpawnName == spawnNameFrom){
        //         spawnDataFrom = CID;
        //         // TODO :
        //         foundFrom = true;
        //     } else if (!foundTo && CID.SpawnName == spawnNameTo){
        //         spawnDataTo = CID;
        //         // TODO :
        //         foundTo = true;
        //     }
        // }

        // Check if found
        // From and To : Switch the SpawnNames
        // From and !To : Change the SpawnName
        // !From : Switch the SpawnNames
    }

}
