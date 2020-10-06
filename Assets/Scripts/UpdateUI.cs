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

    public ChampionPrepController gameController;

    private PlayerData playerData;
    private int[] championsIndex;
    Champion[] champions;


    // Start is called before the first frame update
    void Start()
    {
        champions = new Champion[4];
        championsIndex = new int[4];
        playerData = GameObject.FindWithTag("GameManager").transform.GetComponent<GameManager>().GetPlayer();
        username.text = playerData.GetUsername();
        refreshStore();
    }

    // Update is called once per frame
    void Update()
    {
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
            champions[i] = GameController.instance.GetChampion(championsIndex[i]);   
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

        if(playerData.GetGold() >= champions[championsIndex[index]].price && gameController.getFirstAvailableSubsituteSpawnPoint() != null)
        {
            playerData.AddGold(-champions[championsIndex[index]].price);
            gameController.GetComponent<ChampionPrepController>().addPurchasedChampion(championsIndex[index]);
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

}
