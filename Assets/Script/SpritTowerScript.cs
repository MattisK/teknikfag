using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpritTowerScript : MonoBehaviour
{
    public float dmg;
    public float range;
    public float fireRate; 
    public bool placed;
    GameObject placedOn;
    public GameObject mainCam;
    ButtonsScript buttonsScript;
    public GameObject buyBtn;
    GameObject CancelButton;
    Camera cam;
    Main mainScript;
    Vector3 mousePos;
    List <GameObject> enemies = new List <GameObject> ();
    List <double> distances = new List <double> ();
    int curEnemy;
    Vector3 target;
    public bool setup;
    double frameCount;
    float timePassed;
    public GameObject bullet;
    GameObject tempBullet;
    GameObject curClosePlace;
    bool placeable;

    // Start is called before the first frame update
    void Start()
    {   
        placeable = false;
        mainCam = GameObject.Find("Main Camera"); //locates the main camera
        buttonsScript=mainCam.GetComponent<ButtonsScript>();
        buyBtn = GameObject.Find("spritButton");
        CancelButton = buttonsScript.CancelButton;
        cam = Camera.main;
        mousePos=Input.mousePosition;
        dmg = 3.4f;
        range = 2.5f;
        fireRate = 0.96f;
        placed = false;
        
        this.transform.position = cam.ScreenToWorldPoint(new Vector3(mousePos[0], mousePos[1], 10));
        mainScript = mainCam.GetComponent<Main>(); //gets the main script from the main camera
    }

    // Update is called once per frame
    void Update()
    {
        if(placed == false)
        {
            if(Input.GetMouseButtonUp(0)){
                placeable = true;//makes sure the player doesnt accidently place the tower
            }
            mousePos=Input.mousePosition;
            this.transform.position = cam.ScreenToWorldPoint(new Vector3(mousePos[0], mousePos[1], 10)); //puts the tower on the cursors position 
            if((Input.GetMouseButton(0) && placeable == true && curClosePlace != null) || (Input.GetMouseButtonUp(0) && curClosePlace != null)) //checks if the player clicks 
            {
                this.transform.position = curClosePlace.transform.position; //places the tower on the spot
                this.transform.position = new Vector3(this.transform.position[0]-0.15f, this.transform.position[1]+0.4f, 0); //offsets the tower so it lines up properly
                placed = true; //changes to towers state to placed to it begins shooting
                placedOn = curClosePlace; //sets the towers placement spot to the one it was placed on
                curClosePlace.gameObject.SetActive(false); //disables the placement spot so another tower cant be placed on it again
                mainScript.towers.Add(this.gameObject); //adds the tower to the list of towers in the main script
                CancelButton.SetActive(false); //disables the cancel placement button
                mainScript.holding = 0;
                
            }
        } else {
            
            if(setup==false){
                this.GetComponent<CircleCollider2D>().radius = range; //changes the towers hit box to represent its range
                setup=true; //stops the setup from running again
            }
            if(curEnemy > 0) //checks if there are any enemies in range before doing targetting calculations
            {
                for(int i=0;i<curEnemy;i++){ //loops through all the enemies in range
                    if(enemies[i].tag == "rhinovirus"){
                        distances.Add(enemies[i].GetComponent<RinovirusScript>().distance); //puts all the enemies distances into a list
                    } else if(enemies[i].tag == "stafylokker"){
                        distances.Add(enemies[i].GetComponent<Stafylokker>().distance); //puts all the enemies distances into a list
                    }
                }
                target = enemies[GetIndexOfLowestValue(distances)].transform.position; //changes the towers target to the enemy thats furthest along the track
                transform.rotation= Quaternion.Euler(0,0,RadsToDegs(Mathf.Atan2(target[1]-this.transform.position[1], target[0]-this.transform.position[0]))); //rotates the tower to face the target
            }
            distances.Clear(); //resets the list of distances for next frame
            timePassed += Time.deltaTime; //adds the time passed since last frame to the time passed variable
            if(timePassed >= fireRate) //checks if enough time has passed to fire again
            {
                if(curEnemy > 0) //checkes if theres any enemies in range to shoot at
                {
                    timePassed =0; //resets the shooting timer
                    GameObject tempBullet = Instantiate(bullet); //shoots a bullet
                    tempBullet.transform.rotation = this.transform.rotation; //makes it go the right direction
                    tempBullet.transform.position = this.transform.position+transform.right * 1f; //makes sure it appears in front of the tower
                    tempBullet.GetComponent<SpritBulletScript>().dmg = dmg; //sets the damage of the bullet
                }
            }

        }   
        
    }

    void OnTriggerStay2D(Collider2D collider) //placement logic
    {
        

        if(collider.gameObject.tag == "towerOut"&& placed == false) //checks if the tower is over a valid placement spot
        {
            curClosePlace = collider.gameObject; //sets the current closest placement spot to the one its over
        }
    }
    void OnTriggerEnter2D(Collider2D collider) //tageting logic
    {
        if(collider.gameObject.tag == "rhinovirus"&& placed == true) //checks if a Rhinovirus enters the towers range
        {   
            enemies.Add(collider.gameObject); //adds it to list of enemies in range
            curEnemy++; //counts up the amount of enemies in range
        }else if(collider.gameObject.tag == "stafylokker"){
            enemies.Add(collider.gameObject); //remove it from list of enemies in range
            curEnemy++; //counts down the total amount of enemies in range
        }
    }
    void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "rhinovirus"&& placed == true) //checks if a Rhinovirus exits the towers range (or dies)
        {   
            enemies.Remove(collider.gameObject); //remove it from list of enemies in range
            curEnemy--; //counts down the total amount of enemies in range
        }else if(collider.gameObject.tag == "stafylokker"){
            enemies.Remove(collider.gameObject); //remove it from list of enemies in range
            curEnemy--; //counts down the total amount of enemies in range
        } else if(collider.gameObject.tag == "towerOut"&& placed == false) //checks if the tower is over a valid placement spot
        {
                curClosePlace = null; //sets the current closest placement spot to the one its over
        }
        
        
    }

    private int GetIndexOfLowestValue(List<double> arr) //function to find the lowest value in a list
    {
        double value = double.MaxValue; //sets a value to compare to 
        int index = -1; //sets the curent index value to -1 to prevent errors
        for(int i = 0; i < curEnemy; i++) //loops through the list to find the lowest value
        {
            if(arr[i] < value) //compares the current indexs value to the current lowest
            {
                index = i; // sets the index to the current index
                value = arr[i]; //sets the value to the current indexs value
            }
        }
        return index; //return the index of the lost value
    }
    private float RadsToDegs(float radians) //function to convert radians to degrees
    {
        float degrees = (180 / 3.141f) * radians; //math
        return (degrees); //return the value in degrees
    }

}
