using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Diagnostics;
using System;

public class UpdateUI : MonoBehaviour
{
    public TextMeshProUGUI username;
    public TextMeshProUGUI gold;
    public TextMeshProUGUI level;
    public TextMeshProUGUI result;
    public Image healthBar;
    public Image xpBar;

    private ChampionPrepController championPrepController;
    private GameManager gameManager;

    private PlayerData playerData;
    private int[] championsIndex;
    Champion[] champions;
    private bool fighting;


    // Start is called before the first frame update
    void Start()
    {
        fighting = false;
        gameManager = GameManager.instance;
        championPrepController = gameManager.GetChampionPrepController();
        champions = new Champion[4];
        championsIndex = new int[4];
        playerData = GameManager.instance.GetPlayer();
        username.text = playerData.GetUsername();
        championPrepController.SpawnEnemyChampions(gameManager.GameLevel);
        result.text = "Stage : " + gameManager.GameLevel.ToString();
        refreshStore();
    }

    // Update is called once per frame
    void Update()
    {
        if(fighting == false && gameManager.fighting == true) {
            DisableUI();
            fighting = true;
        }
        else if ( fighting == true && gameManager.fighting == false) {
            if(playerData.GetHealth() <= 0) {
                GameOver(false);
            }
            else {
                EnableUI();
                refreshStore();
                UnityEngine.Debug.Log("++ lvl " + gameManager.GameLevel + "sur " + gameManager.GetLevelNumbers());
                if(gameManager.GameLevel < gameManager.GetLevelNumbers()) {
                    championPrepController.RespawnAllies();
                    championPrepController.SpawnEnemyChampions(gameManager.GameLevel);
                    //update UI with bool win
                    if(gameManager.PlayerWonTheFight) {
                        result.text = "Victory ! Stage : " + gameManager.GameLevel.ToString();
                    }
                    else {
                        result.text = "Defeat ! Stage : " + gameManager.GameLevel.ToString();
                    }
            }
            else {
               GameOver(true);
            }
            fighting = false;
            }    
        }
        if(playerData != null)
        {
            if(playerData.uptodate == false)
            {
                //update de l'ui
                healthBar.fillAmount = 1.0f * playerData.GetHealth() / 100.0f;
                xpBar.fillAmount = playerData.GetXp();
                if (xpBar.fillAmount == 1.0f) xpBar.fillAmount = 0.0f;
                level.text = playerData.GetLevel().ToString();
                gold.text = playerData.GetGold().ToString();
                playerData.uptodate = true;
            }
        }
    }

    private void GameOver(bool win) {
        DisableUI();
        transform.GetComponent<DragAndDrop>().enabled = false;
        playerData.GetPlacementData().removeAlliesHealthBar();
        if(win) result.text = "Congratulations ! You won ! ";
        else result.text = "GameOver ! You lose !";
    }

    public void refreshStore()
    {        
        int i;
        //get scripts associated to indexes in gamecontroller data
        for (i = 0; i < championsIndex.Length; i++)
        {
            //get 4 random indexes
            championsIndex[i] = (int)Math.Round(UnityEngine.Random.Range(0.0f, 0.3f) * 10.0f);
            //get champions scripts from the right prefab
            champions[i] = GameManager.instance.GetChampion(championsIndex[i]);   
        }
        //get every box 
        Transform panelStore = transform.GetChild(1).GetChild(0);
        i = 0;
        while (i < panelStore.childCount)
        {
            //for each box set text 
            //name
            TextMeshProUGUI text = panelStore.GetChild(i).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
            text.text = champions[i].championName;
            //attack
            text = panelStore.GetChild(i).GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
            text.text = champions[i].attackDamage.ToString();
            //defense
            text = panelStore.GetChild(i).GetChild(2).gameObject.GetComponent<TextMeshProUGUI>();
            text.text = champions[i].shield.ToString();
            //price
            text = panelStore.GetChild(i).GetChild(3).gameObject.GetComponent<TextMeshProUGUI>();
            text.text = champions[i].price.ToString();
            //UnityEngine.Debug.Log(text);
            i++;
        }
    }
    public void BuyChampion(int index)
    {

        if(playerData.GetGold() >= champions[championsIndex[index]].price && championPrepController.getFirstAvailableSubsituteSpawnPoint() != null)
        {
            playerData.AddGold(-champions[championsIndex[index]].price);
            championPrepController.addPurchasedChampion(championsIndex[index]);
        }
    }
    public void UpExperience()
    {
        if (this.playerData.GetGold() >= 4)
        {
            this.playerData.AddGold(-4);
            this.playerData.AddXp(2);
        }
    }
    public void Refresh()
    {
        if(playerData.GetGold() >= 2)
        {
            playerData.AddGold(-2);
            refreshStore();
        }
    }
    private void DisableUI() {
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(3).gameObject.SetActive(false);
    }
    private void EnableUI() {
        transform.GetChild(1).gameObject.SetActive(true);
        transform.GetChild(3).gameObject.SetActive(true);
    }
    public void Battle()
    {
        gameManager.Battle();
    }
    public void Quit()
    {
        gameManager.Pause();
    }

}
