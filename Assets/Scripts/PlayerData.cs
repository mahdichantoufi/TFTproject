using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public string username;
    public int earnedGold;
    public float experiencePoints;
    public int level;
    public int health;
    public bool uptodate;
    private PlacementData placementData;
    private int NumberOfFightingSpawns = 9;
    private int NumberOfSubsSpawns = 10;
    public PlayerData (string username){
        this.username = username;
        this.earnedGold = 5000;
        this.experiencePoints = 0;
        this.level = 1;
        this.health = 100;
        this.uptodate = false;
        this.placementData = new PlacementData(NumberOfFightingSpawns,NumberOfSubsSpawns);
    }
    public void AddGold(int gold){
        earnedGold = earnedGold + gold;
        this.uptodate = false;
    }
    public void AddXp(int xp){
        experiencePoints = experiencePoints + xp;
        if (experiencePoints >= 4) this.level = 2;
        if (experiencePoints >= 10) this.level = 3;
        if (experiencePoints >= 20) this.level = 4;
        this.uptodate = false;
    }
    public void AddHealth(int health)
    {
        this.health = this.health + health;
        this.uptodate = false;
    }
    public string GetUsername(){
        return this.username;
    }
    public int GetGold(){
        return earnedGold;
    }
    public float GetXp()
    {
        if (experiencePoints <= 4) return experiencePoints / 4.0f;
        else if (experiencePoints <= 10) return experiencePoints / 10.0f;
        else if (experiencePoints <= 20) return experiencePoints / 20.0f;
        else return 0.0f;
    }
    public int GetLevel(){
        return this.level;
    }
    public int GetHealth()
    {
        return this.health;
    }

    public void LogPrint(){
        Debug.Log("Object "+ this);
        Debug.Log("username "+ this.username);
        Debug.Log("earnedGold "+ this.earnedGold.ToString());
        Debug.Log("experiencePoints "+ this.experiencePoints.ToString());
        Debug.Log("level " + this.level.ToString());
    }
    public PlacementData GetPlacementData(){
        return this.placementData;
    }
    public bool canMoveBasedOnLevel(bool FromIsFighter, bool ToIsFighter, bool ToContainsChampion){
        Debug.Log("ToIsFighter " + ToIsFighter);
        if (!ToIsFighter)
            return true;
        Debug.Log("FromIsFighter " + FromIsFighter);
        if (FromIsFighter)
            return true;

        // Case : From Sub to Fighter
        
        int FightersNb = this.GetPlacementData().getFightingChampsNumber();
        int level = this.GetLevel();
        if (FightersNb < level || ToContainsChampion){
            Debug.Log(FightersNb+"<"+level);
            return true;
        }
        return false;
    }
}
