using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour 
{
    public static GameManager instance = null;
    private PlayerData playerData;
    private string username;

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
        
    }

    public PlayerData GetPlayer(){
        return this.playerData;
    }
    public void SetPlayerUsername(string username){
        UnityEngine.Debug.Log(username);
        this.username = username;
    }
    public void Battle()
    {
        SceneManager.LoadScene(2);
    }
    public void Play()
    {
        UnityEngine.Debug.Log("play");
        this.playerData = new PlayerData(username);
        SceneManager.LoadScene(1);
    }

    public void BackToMenu()
    {
        UnityEngine.Debug.Log("back");
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }
    public void Pause()
    {
        UnityEngine.Debug.Log("pause");
        SceneManager.LoadScene(0);
    }


}
