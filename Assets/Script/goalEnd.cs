using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goalEnd : MonoBehaviour
{   
    Main mainScript;
    GameObject mainCam;
    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.Find("Main Camera"); //locates the main camera
        mainScript = mainCam.GetComponent<Main>(); //gets the main script from the main camera
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collider){ //collision detection that damages the player when an enemy reaches the end of the path
        if(collider.gameObject.tag == "rhinovirus"){ //detects wether the collidier is an enemy/what enemy it is
            collider.gameObject.GetComponent<RinovirusScript>().health=0; //destroys the enemy
            mainScript.playerHealth= mainScript.playerHealth-collider.gameObject.GetComponent<RinovirusScript>().damage; //damages the player
            mainScript.lastHit = "Rhinovirus"; //sets the last hit to the enemy that hit the player
        } else if(collider.gameObject.tag == "stafylokker"){
            print("hi");
            collider.gameObject.GetComponent<Stafylokker>().curGoal++;
            mainScript.playerHealth= mainScript.playerHealth-collider.gameObject.GetComponent<Stafylokker>().damage; //damages the player
            mainScript.lastHit = "Stafylokker"; //sets the last hit to the enemy that hit the player
            collider.gameObject.GetComponent<Stafylokker>().health=0; //destroys the enemy
        }
    }
}
