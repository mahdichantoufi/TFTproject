using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
    public GameObject HealthBarPref;
    Transform target;
    GameObject ChampionHealth;
    private int maxHealth = 100;
 
 
 
    // Use this for initialization
    void Start () {
        ChampionHealth = Instantiate(HealthBarPref);
        UnityEngine.Debug.Log("health "+ ChampionHealth);
        ChampionHealth.transform.SetParent(GameObject.Find("Canvas").transform, false);
    }
 
    private void Awake() {
        target = gameObject.transform;
    }
 
    // Update is called once per frame
    void Update () {
        ChampionHealth.transform.position  = Camera.main.WorldToScreenPoint(target.position + new Vector3(0,6,0));
    }
    public void destroyHealthBar() {
        Destroy(ChampionHealth);
    }
    public void SetHealth(float health) {
        ChampionHealth.gameObject.GetComponent<Image>().fillAmount = health / maxHealth * 1.0f;
    }
}