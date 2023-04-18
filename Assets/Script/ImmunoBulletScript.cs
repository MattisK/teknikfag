using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmunoBulletScript : MonoBehaviour
{
    public GameObject motherObject; 
    ImmunoTowerScript motherScript;
    Main mainScript;
    Quaternion rotation;
    public float speed;
    public float lifeTime;
    public float dmg;
    public string type = "immuno";
    bool ran;
    float timeCasing;
    Vector3 curTarget;
    Vector3 mPos;

    // Start is called before the first frame update
    void Start()
    {
        timeCasing= 1;
        speed = 2f;
        lifeTime = 5f;
        ran = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(motherObject == null) //check to delete the projetile if the mother is destroyed at any point during the projectiles life
        {
            Destroy(this.gameObject);
        }
        mPos = new Vector3(motherObject.transform.position[0],motherObject.transform.position[1],motherObject.transform.position[2]); //gets the position of the tower
        curTarget = new Vector3((mPos[0]+1*Mathf.Sin(lifeTime)), mPos[1]+1*Mathf.Cos(lifeTime), 0); //defines the resting position of the bullet in a circle around the tower
        transform.rotation= Quaternion.Euler(0,0,15*lifeTime); //rotates the bullet over time to make it more lively
        if(ran ==false){ //gets the scripts from the mother object once since it doesnt function properly in start()
            motherScript = motherObject.GetComponent<ImmunoTowerScript>(); 
            mainScript = motherScript.mainScript;
            ran = true;
        }
        if(motherScript.curEnemy > 0){ //checks if there is an enemy in range
            transform.position = Vector2.MoveTowards(transform.position, motherScript.target, speed * Time.deltaTime*timeCasing); //moves towards the enemy depending on time chasing
            timeCasing += Time.deltaTime*3; //accelerates the bullet to make it catch up to enemies over time
        }
        else{
            transform.position = Vector2.MoveTowards(transform.position, curTarget, speed * Time.deltaTime); //otherwise move around the tower in a circle
            timeCasing = 1; // reset time chasing
            
        }
        lifeTime -= Time.deltaTime; // count down life time of the projectile
        this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1,1,1,lifeTime); //changes the opacity of the projectile when close to death
        if(lifeTime <= 0 || motherScript.StartWaveBtn.activeSelf == true) //if the projectile is dead or the wave is over destroy it
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other) //collision detection
    {
        if(other.gameObject.tag == "rhinovirus") //checks if collider is a rhinovirus
        {
            if(other.gameObject.GetComponent<RinovirusScript>().health > 0) //deals damage to the virus if it still has health
            {
                other.gameObject.GetComponent<RinovirusScript>().health -= dmg*2;
                Destroy(this.gameObject); //destroys the projectile after it succesfully does damage to the enemy
            }
           
            if(other.gameObject.GetComponent<RinovirusScript>().health <= 0) // destroys the virus if it has no health left
            {
                other.gameObject.GetComponent<RinovirusScript>().Die(); //runs the death function of the virus to award the player the correct amount of money
            }
            
        } else if(other.gameObject.tag == "stafylokker"){ //checks if collider is a stafylokker
            if(other.gameObject.GetComponent<Stafylokker>().health > 0) //same as rhinovirus but with a different damage multiplier
            {
                other.gameObject.GetComponent<Stafylokker>().health -= dmg*1.5f;
                Destroy(this.gameObject); //destroys the projectile after it succesfully does damage to the enemy
            }
           
            if(other.gameObject.GetComponent<Stafylokker>().health <= 0) //same as above but with the respective "Die()" function
            {
                other.gameObject.GetComponent<Stafylokker>().Die();
            }
        }

    }
}
