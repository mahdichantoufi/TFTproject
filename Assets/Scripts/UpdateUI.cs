using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Diagnostics;

public class UpdateUI : MonoBehaviour
{
    public TextMeshProUGUI username;
    public TextMeshProUGUI gold;
    public TextMeshProUGUI level;
    public Image healthBar;
    public Image xpBar;
    private PlayerData playerData;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playerData == null)
        {
            playerData = GameObject.FindWithTag("GameManager").transform.GetComponent<GameManager>().GetPlayer();
            username.text = playerData.GetUsername();
        }

        healthBar.fillAmount = 1.0f * playerData.GetHealth() / 100.0f;
        xpBar.fillAmount = playerData.GetXp();
        level.text = playerData.GetLevel().ToString();
        gold.text = playerData.GetGold().ToString();
        //UnityEngine.Debug.Log(playerData.GetGold().ToString());
        //UnityEngine.Debug.Log(playerData.GetXp().ToString());
    }
}
