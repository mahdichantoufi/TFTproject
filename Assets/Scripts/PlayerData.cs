using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public string username;

    public int earnedGold;
    public int experiencePoints;

    public PlayerData (){
        this.username = string.Empty;
        this.earnedGold = 0;
        this.experiencePoints = 0;
    }
    public void SetUsername(string username) {
        this.username = username; 
    }
    public void AddGold(int gold1){
        earnedGold = earnedGold + gold1;
    }
    public void AddXp(int xp1){
        experiencePoints = experiencePoints + xp1;
    }
    public string GetUsername(){
        return this.username;
    }
    public int GetGold(){
        return earnedGold;
    }
    public int GetXp(){
        return experiencePoints;
    }

    public void LogPrint(){
        Debug.Log("Object "+ this);
        Debug.Log("username "+ this.username);
        Debug.Log("earnedGold "+ this.earnedGold.ToString());
        Debug.Log("experiencePoints "+ this.experiencePoints.ToString());
    }
}
