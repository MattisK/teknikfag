using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stafylokker : MonoBehaviour
{
    public float health;
    public float speed;
    public float damage;
    public string[] resistances;
    public int resistanceCount;
    public string[] weaknesses;
    public int weaknessCount;
    GameObject Level;
    levelOneScript levelOneScript;
    GameObject Main;
    public GameObject[] goal;
    public int curGoal;
    Main mainScript;
    public double distance;
    float DeathTimer;
    public Sprite DSprite1;
    public Sprite DSprite2;
    public Sprite DSprite3;
    public Sprite DSprite4;
    public GameObject SpriteHolder;

    // Start is called before the first frame update
    void Start() //does setup for the enemy right as it spawns
    {   
        distance = 9007199254000000;//defines how far the enemy is along the track
        Main = GameObject.Find("Main Camera"); //locates the main camera
        mainScript = Main.GetComponent<Main>(); //gets the main script from the main camera
        if(mainScript.gameState == "levelOne"){ //checks what level the player is on
            Level = GameObject.Find("LevelOne"); //locates the level
            levelOneScript = Level.GetComponent<levelOneScript>(); //gets the level script from the level
            for(int i = 0; i < levelOneScript.goal.Length; i++){ //copies the goal array from the level script to the enemy script
                goal[i] = levelOneScript.goal[i];
            }
            
        }
        health = 16; //how resitant is it (ish)
        speed = 2f; //incubation time
        damage = 2; // how sick do you get 
        resistances[0]= "cold.66"; //resists cold based on research by 66%
        resistances[1]= "vaccine.100"; //resists vaccines 
        resistances[2]= "immunity.80"; //resists immunity based on research by 80%
        resistances[3]= "sprit.100"; //resists sprit based on research completely
        resistances[4]= "warm.66";
        resistanceCount = 5; //how many resistances it has
        weaknesses[0]= "immuneSystem.1.5"; 
        weaknesses[1]= "antibiotica.2";
        weaknessCount = 2; //how many weaknesses it has
        curGoal=0; //which goal it is going to
       DeathTimer= -10;
    }

    // Update is called once per frame
    void Update()
    {   
       if(DeathTimer == -10){ //check if the enemy is currently dieing or not 
        if(health <= 0){ //checks if the enemy is supposed to be dieing or not
            Die(); //initiates death 
        }
        transform.position = Vector2.MoveTowards(transform.position, goal[curGoal].transform.position, speed * Time.deltaTime); //moves the enemy towards the current goal
        distance -= speed*Time.deltaTime; //counts down distance wtih the speed of the enemy
        } else if(DeathTimer > 0){ //checks what stage of death the enemy is in
            DeathTimer -= Time.deltaTime; //counts down the death timer
            if(DeathTimer > 0.28f){
                SpriteHolder.GetComponent<SpriteRenderer>().sprite = DSprite1; //changes the sprite to the appropritate death sprite
                SpriteHolder.GetComponent<Transform>().localScale = new Vector3(0.4f,0.4f,1f); //changes size to accomodate the new sprite
            }
            else if(DeathTimer > 0.23f){ //checks what stage of death the enemy is in
                SpriteHolder.GetComponent<SpriteRenderer>().sprite = DSprite2; //changes the sprite to the appropritate death sprite
            }
            else if(DeathTimer > 0.15f){
                SpriteHolder.GetComponent<SpriteRenderer>().sprite = DSprite3; //changes the sprite to the appropritate death sprite
            }
            else if(DeathTimer > 0.08f){
                SpriteHolder.GetComponent<SpriteRenderer>().sprite = DSprite4; //changes the sprite to the appropritate death sprite
            }
            else if(DeathTimer < 0){ //checks if the enemy is done dieing and destroys it when it is
                Destroy(gameObject);
            }

        }

        
    }

    public void Die(){ //function to kill the enemy properly and award the appropritate amount of money
        mainScript.enemies.Remove(gameObject); //removes the enemy from the enemies list
        DeathTimer= 0.3f;
        this.GetComponent<CircleCollider2D>().enabled = false;
        mainScript.money +=10; //gives the player money for killing the enemy
    }
}
