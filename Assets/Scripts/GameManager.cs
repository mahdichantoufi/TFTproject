using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class GameManager : MonoBehaviour 
{
    public static GameManager instance = null;
    public string username = string.Empty;
    public int gold = 0;
    public int xp = 0;

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

    public void SetUsername(string username) {
        UnityEngine.Debug.Log(username);
        this.username = username; }
    public void AddGold(int gold1) { gold = gold + gold1; }
    public void AddXp(int xp1) { xp = xp + xp1; }
    public string GetUsername() { return this.username; }
    public int GetGold() { return gold; }
    public int GetXp() { return xp; }
}
