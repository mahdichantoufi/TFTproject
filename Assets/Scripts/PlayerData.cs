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

    public PlayerData (){
        this.username = string.Empty;
        this.earnedGold = 50;
        this.experiencePoints = 0;
        this.level = 1;
        this.health = 100;
    }
    public void SetUsername(string username) {
        this.username = username; 
    }
    public void AddGold(int gold){
        earnedGold = earnedGold + gold;
    }
    public void AddXp(int xp){
        experiencePoints = experiencePoints + xp;
        if (experiencePoints > 4) level = 2;
        else if (experiencePoints > 10) level = 3;
        else if (experiencePoints > 20) level = 3;
    }
    public void AddHealth(int health)
    {
        this.health = this.health + health;
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
        return level;
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
    }
}
