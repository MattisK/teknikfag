using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goal : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collider){ //collision detector that changes the goal of the current enemy thats coliding to the next goal 
        if(collider.gameObject.tag == "rhinovirus"){ //detects wether the collider is an enemy/what enemy it is
            collider.gameObject.GetComponent<RinovirusScript>().curGoal++; //changes the goal of the enemy to the next point 
        } else if(collider.gameObject.tag == "stafylokker"){
            collider.gameObject.GetComponent<Stafylokker>().curGoal++;
        }
    }
}
