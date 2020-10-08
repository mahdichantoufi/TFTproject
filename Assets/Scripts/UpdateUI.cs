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
            EnableUI();
            fighting = false;
            gameManager.GetChampionPrepController().SpawnEnemyChampions(gameManager.GameLevel);
            gameManager.GetChampionPrepController().RespawnAllies();
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
