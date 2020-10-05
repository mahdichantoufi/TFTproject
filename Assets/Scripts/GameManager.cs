using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class GameManager : MonoBehaviour 
{
    public static GameManager instance = null;
    private PlayerData playerData;

    void Awake()
    {
        //Check if instance already exists
        if (instance == null)
            //if not, set instance to this
            instance = this;
        //If instance already exists and it's not this:
        else if (instance != this)
            //Then destroy this. This enforces our singleton pattern, 
            // meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
        //Sets this to not be destroyed when reloading scene / Switching scenes
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        playerData = new PlayerData();
    }

    public PlayerData GetPlayer(){
        return this.playerData;
    }
    public void SetPlayerUsername(string username){
        this.playerData.SetUsername(username);
    }
    public void UpExperience()
    {
        if(this.playerData.GetGold() >= 4)
        {
            this.playerData.AddGold(-4);
            this.playerData.AddXp(2);
        }
        UnityEngine.Debug.Log(playerData.GetGold());
        UnityEngine.Debug.Log("          ");
    }
}
