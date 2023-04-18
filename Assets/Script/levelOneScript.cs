using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelOneScript : MonoBehaviour
{
    public int TowerInSpots;
    public Vector3[] TowerInSpotPos;
    public int TowerOutSpots;
    public Vector3[] TowerOutSpotPos;
    public int WaveCount;
    public int[] EnemieOnes;
    public int[] EnemieTwos;
    public int[] EnemieThrees;
    public Vector3 startPos;
    public GameObject[] goal;
    // Start is called before the first frame update
    void Start()
    {   
        startPos = new Vector3(-10,-5f,1); //stores the start position of level one
        TowerInSpots = 3; //determines how many spots there are for towers to be placed outside of the body 
        TowerInSpotPos[0] = new Vector3(2.8f,0.7f,0); //sets the location of the first tower spot
        TowerInSpotPos[1] = new Vector3(5f,0.5f,0); //sets the location of the second tower spot
        TowerInSpotPos[2] = new Vector3(6.9f,2.3f,0); //sets the location of the third tower spot

        TowerOutSpots = 3;//determines how many spots there are for towers to be placed inside of the body
        TowerOutSpotPos[0] = new Vector3(-5.7f,1.3f,0); //sets the location of the first tower spot
        TowerOutSpotPos[1] = new Vector3(-3.8f,0f,0); //sets the location of the second tower spot
        TowerOutSpotPos[2] = new Vector3(-1.6f,0.4f,0); //sets the location of the third tower spot
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
