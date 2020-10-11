using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Champion : MonoBehaviour
{
	public Animator animator;

    public string championName;
    public int price;
    public int attackDamage;
    private int health = 100;
    public int shield;
    public float speed;
    public float attackRange;
    public float attackCooldown;

    private float currentAttackDamage;
    private float currentHealth;
    private float currentShield;
    private float currentSpeed;
    private float currentAttackRange;
    private float currentAttackCooldown;

    private Image healthBar;

    private float timeSinceLastAttack;
    private bool isAlive;
    private bool isActive = false;
    

    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.localScale += new Vector3(1,1,1);
        isAlive = true;
        currentAttackDamage = attackDamage;
        currentHealth = health;
        currentShield = shield;
        currentSpeed = speed;
        currentAttackRange = attackRange + 3.0f;
        currentAttackCooldown = attackCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if(isAlive && isActive){
            bool moved = false;
            timeSinceLastAttack += Time.deltaTime;
            GameObject closestAlly = FindClosestAlly();
            GameObject closestEnemy = FindClosestEnemy();

            if(closestAlly != null){
                MoveAwayFrom (closestAlly.transform.position);
                closestAlly = null;
                closestAlly = FindClosestEnemy();
                moved = true;
            }
            if(!moved && closestEnemy != null){
                transform.LookAt (closestEnemy.transform);
                if (DistanceTo(closestEnemy.transform.position) > currentAttackRange) 
                    MoveTowards (closestEnemy.transform.position);
                else 
                    Attack(closestEnemy);
            } else if (!moved){
                if ( gameObject.tag == "Ally")
                    GameManager.instance.EndOfBattle(true);
                else
                    GameManager.instance.EndOfBattle(false);

                this.gameObject.SetActive(false);
            }
        }
    }
    public void setActive(){
        this.isActive = true;
    }
    
	GameObject FindClosestEnemy()
	{
		float distanceToClosestEnemy = Mathf.Infinity;
		GameObject closestEnemy = null;
        GameObject[] allEnemies;
        if ( gameObject.tag == "Ally")
	        allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        else
	        allEnemies = GameObject.FindGameObjectsWithTag("Ally");

		foreach (GameObject currentEnemy in allEnemies) {
            if(!currentEnemy.gameObject.GetComponent<Champion>().isDead()){
                float distanceToEnemy = DistanceTo(currentEnemy.transform.position);
                if (currentEnemy.gameObject.GetComponent<Champion>().isFighting() && distanceToEnemy < distanceToClosestEnemy) {
                    distanceToClosestEnemy = distanceToEnemy;
                    closestEnemy = currentEnemy;
                }
            }
		}
        return closestEnemy;
	}
    public bool isFighting(){
        return this.isActive;
    }
	GameObject FindClosestAlly()
	{
    	float distanceToClosestAlly = 4.0f;	
		GameObject closestAlly = null;
        GameObject[] allAllies;
        allAllies = GameObject.FindGameObjectsWithTag(gameObject.tag);
		foreach (GameObject currentAlly in allAllies) {
            if(
                !currentAlly.gameObject.GetComponent<Champion>().isDead()
            ){
                float distanceToAlly = DistanceTo(currentAlly.transform.position);
                if (distanceToAlly != 0 && distanceToAlly < distanceToClosestAlly) {
                    distanceToClosestAlly = distanceToAlly;
                    closestAlly = currentAlly;
                }
            }
		}
        return closestAlly;
	}
    float DistanceTo(Vector3 closestEnemyPos){
        return Vector3.Distance (transform.position, closestEnemyPos);
    }
    void MoveTowards(Vector3 enemyPosition){
        
        // Move our position a step closer to the target.
        float step =  currentSpeed * Time.deltaTime; // calculate distance to move
        transform.position = transform.position + (enemyPosition - transform.position) * step;
    }
    void MoveAwayFrom(Vector3 allyPosition){
        
        // Move our position a step closer to the target.
        float step =  currentSpeed * Time.deltaTime; // calculate distance to move
        transform.position = transform.position + (transform.position - allyPosition) * step;
    }
    void Attack(GameObject ClosestEnemy){
        if(timeSinceLastAttack > currentAttackCooldown){
            ClosestEnemy.gameObject.GetComponent<Champion>().takeAttaque(currentAttackDamage);
            timeSinceLastAttack = 0;
        }
    }
    void takeAttaque(float takenDamage){
        currentHealth -= takenDamage;
        transform.GetComponent<HealthBar>().SetHealth(currentHealth);
        // healthBar.fillAmount = currentHealth / health * 1.0f;
        if(currentHealth <= 0){
            die();
        }
        // Debug.Log("Taken damage = " + takenDamage.ToString());
        // Debug.Log("Current health = " + currentHealth.ToString());
    }
    public void pop(){
        isActive = false;
        isAlive = true;
        currentHealth = health;
        transform.GetComponent<HealthBar>().SetHealth(currentHealth);
        this.gameObject.SetActive(true);
    }
    void die(){
        isActive = false;
        isAlive = false;
        currentHealth = 0;
        this.gameObject.SetActive(false);
    }
    public bool isDead(){
        return !(isAlive);
    }
}
