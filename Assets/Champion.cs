using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Champion : MonoBehaviour
{
	public Animator animator;
    public GameController gameController;
    public int attackDamage;
    public int health;
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
        if(isActive)
            Debug.Log("This is an active Item.");
        else
            Debug.Log("This is an inactive Item.");
        if(isAlive && isActive){
            Debug.Log("This is an alive/active Item.");
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
                    GameController.instance.AllEnemiesAreDead(true);
                else
                    GameController.instance.AllEnemiesAreDead(false);
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
                if (distanceToEnemy < distanceToClosestEnemy) {
                    distanceToClosestEnemy = distanceToEnemy;
                    closestEnemy = currentEnemy;
                }
            }
		}
        return closestEnemy;
	}
    // TODO : Create Path Finding to avoid Ally GameObjects
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
        Debug.Log("attacked by "+takenDamage);
        currentHealth -= takenDamage;
        if(currentHealth <= 0){
            die();
        }
        // Debug.Log("Taken damage = " + takenDamage.ToString());
        // Debug.Log("Current health = " + currentHealth.ToString());
    }
    void die(){
        isAlive = false;
        currentHealth = 0;
        Destroy(this.gameObject);
    }
    public bool isDead(){
        if (isAlive) return false;
        else return true;
    }
}
