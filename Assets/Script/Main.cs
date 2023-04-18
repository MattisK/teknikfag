using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class Main : MonoBehaviour
{
    GameObject startButton;
    GameObject quitButton;
    GameObject settingsButton;
    public GameObject splashScreen;
    public GameObject startArrow;
    public GameObject logo;
    GameObject levelOne;
    public GameObject bestiaryOpen;
    levelOneScript levelOneScript;
    GameObject levelOneSprite;
    public GameObject StartWaveBtn;
    public GameObject towersMenu;
    public GameObject TowerSpotInPref;
    public GameObject TowerSpotOutPref;
    GameObject curTowerSpot;
    public List <GameObject> enemies = new List <GameObject> ();
    int enemieCount;
    public List <GameObject> towers = new List <GameObject> ();
    public List <GameObject> towerSpots = new List <GameObject> ();
    public string gameState;
    public int ran;
    public int wave;
    public bool waveDone;
    public int endRoundAwarded;
    public int endRoundCash;
    public double timePassed;
    public GameObject EnemyOne;
    public GameObject EnemyTwo;
    GameObject curEnemy;
    public int spawn;
    public float playerHealth;
    GameObject backButton;
    public bool setRan;
    public bool mainRan;
    GameObject healthText;
    GameObject moneyText;
    GameObject roundText;
    public int holding;
    public GameObject holdingTower;
    public float money;
    public string lastHit;
    public GameObject deathScreen;
    public GameObject winScreen;
    public double deltaR3D = 1;
    protected int defScalingRhino;
    protected int defScalingStaph;
    public ButtonsScript buttonsScript;
    
   
    // Start is called before the first frame update
    void Start() //finds all the relevant objects in the scene and gets the scripts from somee of them
    { //also hides a bunch of ui elements that are not needed at the start of the game
        holding = 0;
        levelOne = GameObject.Find("LevelOne");
        levelOneSprite = GameObject.Find("LevelOneSprite");
        levelOneScript = levelOne.GetComponent<levelOneScript>();
        startButton = GameObject.Find("Start");
        quitButton = GameObject.Find("Quit");
        settingsButton = GameObject.Find("Settings");
        startArrow = GameObject.Find("StartArrow");
        logo = GameObject.Find("Logo");
        splashScreen = GameObject.Find("SplashScreen");
        backButton = GameObject.Find("Back");
        healthText = GameObject.Find("Health");
        moneyText = GameObject.Find("Money");
        roundText = GameObject.Find("Round");
        backButton.SetActive(false);
        gameState = "SplashScreen";
        setRan= false;
        healthText.SetActive(false); 
        moneyText.SetActive(false);
        roundText.SetActive(false);
        startButton.SetActive(false);
        quitButton.SetActive(false);
        settingsButton.SetActive(false);
        buttonsScript = this.GetComponent<ButtonsScript>();
    }

    // Update is called once per frame
    void Update() //main update loop that handles which scene is currently being played/run
    {   
        if(gameState == "MainMenu") // checks if the game is on the main menu and does nothing since the main menu is handled by the buttons
        {   if(mainRan == false) //checks if the main menu has been ran before
            {
                lastHit = null; //resets the last hit variable so it doesnt presist through levels
                mainRan = true; //indicates that the main menu setup has been run
                startButton.SetActive(true); //hides the start button
                quitButton.SetActive(true); //hides the quit button
                settingsButton.SetActive(true); //hides the settings button
                logo.SetActive(true); //enables the logo
                splashScreen.SetActive(true); //enables the background splash screen
                backButton.SetActive(false); //hides relevent ui elements
                healthText.SetActive(false);
                moneyText.SetActive(false);
                roundText.SetActive(false);
                
            }
        }
        if(gameState == "settings"){ //prepares the settings menu
            if(setRan == false){
                setRan = true;
                startButton.SetActive(false); //hides the start button
                quitButton.SetActive(false); //hides the quit button
                settingsButton.SetActive(false); //hides the settings button
                backButton.SetActive(true);
                healthText.SetActive(false); //redudant but better to have than not
                moneyText.SetActive(false);
            }


        }
        if(gameState == "levelOne") //checks if level one is currently being played
        {
            if(ran == 0){ //does first time setup to prepare level one for playing
                healthText.SetActive(true); //showes relevent ui elements
                moneyText.SetActive(true);
                towersMenu.SetActive(true);
                bestiaryOpen.SetActive(true);
                roundText.SetActive(true);
                playerHealth = 1; //sets the players starting health
                money=300;//sets the players starting money
                endRoundAwarded = 0; //resest wether the player has been awarded the end round bonus
                endRoundCash = 50; //sets the endround bonus to 50
                levelOneSprite.GetComponent<SpriteRenderer>().enabled = true; //enables the level one sprite
                levelOneSprite.GetComponent<SpriteRenderer>().color = new Color(0.7f, 0.7f, 0.7f, 1f); //sets the level one sprite to a dark grey
                ran = 1; //indicates that the setup has been run
                for(int i = 0; i < levelOneScript.TowerInSpots;i++){
                    curTowerSpot = Instantiate(TowerSpotInPref, levelOneScript.TowerInSpotPos[i], Quaternion.identity);//spawns the tower spots inside the body
                    towerSpots.Add(curTowerSpot);
                }
                for(int i = 0; i < levelOneScript.TowerOutSpots;i++){
                    curTowerSpot = Instantiate(TowerSpotOutPref, levelOneScript.TowerOutSpotPos[i], Quaternion.identity); //spawns the tower spots outside the body
                    towerSpots.Add(curTowerSpot);
                }
                timePassed =0; //resets the time passed for the current wave
                wave =0; //sets the wave to the first wave
                waveDone = true; //sets the wave done to false
                spawn=1; //resets the spawn counter

            }
            if(wave == 10 && IsThereEnemies() == false && waveDone == true){
                buttonsScript.Win();
            }
            if(waveDone == false){ //overarcing wave logic
                switch(wave){ //checks which wave is currently being played
                    case 1: //prepares and spawns enemies for the first wave
                        timePassed = timePassed + Time.deltaTime; //updates the time passed
                        RoundOneDefault(); //runs the wave one level one function
                        break;
                    case 2: //runs the wave two level one function

                        timePassed = timePassed + Time.deltaTime; //updates the time passed
                        RoundTwoDefault(); //runs the wave two level one function
                        break;
                    case 3: //same as above
                        timePassed = timePassed + Time.deltaTime;
                        RoundThreeDefault();
                        break;
                    case 4: //same as above
                        timePassed += Time.deltaTime;
                        RoundFourDefault();
                        break;
                    case 5://same as above
                        timePassed += Time.deltaTime;
                        RoundFiveDefault();
                        break;
                    case 6://same as above
                        timePassed += Time.deltaTime;
                        RoundSixDefault();
                        break;
                    case 7://same as above
                        timePassed += Time.deltaTime;
                        RoundSevenDefault();
                        break;
                    case 8: //same as above
                        timePassed += Time.deltaTime;
                        RoundEightDefault();
                        break;
                    case 9: //same as above
                        timePassed += Time.deltaTime;
                        RoundNineDefault();
                        break;
                    case 10:// same as above
                        timePassed += Time.deltaTime;
                        RoundTenDefault();
                        break;
                    default: //if the game runs out of waves 
                        buttonsScript.Win();
                        waveDone = true;
                        break;
                }
            } else{
                if(!IsThereEnemies()) {             //functions to test wether there are any enemies left when the "wave done" variable is set to true
                    StartWaveBtn.SetActive(true);   //enables the start wave button if there are no enemies left to prevent wave stacking
                    if(endRoundAwarded < wave){     //awards end of round cash once
                        money += endRoundCash; 
                        endRoundAwarded++;
                    }
                    
                }
            }
            if( playerHealth <= 0){ //tests if the player has died
                deathScreen.SetActive(true); //shows the death screen
                gameState = "deathScreen"; // gamestate is death screen
            }
        }

        /*if (gameState == "levelTwo") { //unused code for an unimplementet planned level two
            if(ran == 0){ //does first time setup to prepare level one for playing
                healthText.SetActive(true);
                moneyText.SetActive(true);
                towersMenu.SetActive(true);
                bestiaryOpen.SetActive(true);
                playerHealth = 1;
                //levelTwoSprite.GetComponent<SpriteRenderer>().enabled = true; //enables the level one sprite
                //levelTwoSprite.GetComponent<SpriteRenderer>().color = new Color(0.7f, 0.7f, 0.7f, 1f); //sets the level one sprite to a dark grey
                ran = 1;
                /*for(int i = 0; i < levelTwoScript.TowerInSpots;i++){
                    curTowerSpot = Instantiate(TowerSpotInPref, levelTwoScript.TowerInSpotPos[i], Quaternion.identity);//spawns the tower spots inside the body
                    towerSpots.Add(curTowerSpot);
                }
                for(int i = 0; i < levelTwoScript.TowerOutSpots;i++){
                    curTowerSpot = Instantiate(TowerSpotOutPref, levelTwoScript.TowerOutSpotPos[i], Quaternion.identity); //spawns the tower spots outside the body
                    towerSpots.Add(curTowerSpot);
                }
                timePassed =0; //resets the time passed for the current wave
                wave =0; //sets the wave to the first wave
                waveDone = true; //sets the wave done to false
                spawn=1; //resets the spawn counter
                

            }
            if(waveDone == false) {
                
            }

        }

        if (gameState == "levelThree") {
            if(ran == 0){ //does first time setup to prepare level one for playing
                healthText.SetActive(true);
                moneyText.SetActive(true);
                towersMenu.SetActive(true);
                bestiaryOpen.SetActive(true);
                playerHealth = 1;
                //levelThreeSprite.GetComponent<SpriteRenderer>().enabled = true; //enables the level one sprite
                //levelThreeSprite.GetComponent<SpriteRenderer>().color = new Color(0.7f, 0.7f, 0.7f, 1f); //sets the level one sprite to a dark grey
                ran = 1;
                /*for(int i = 0; i < levelThreeScript.TowerInSpots;i++){
                    curTowerSpot = Instantiate(TowerSpotInPref, levelThreeScript.TowerInSpotPos[i], Quaternion.identity);//spawns the tower spots inside the body
                    towerSpots.Add(curTowerSpot);
                }
                for(int i = 0; i < levelOneScript.TowerOutSpots;i++){
                    curTowerSpot = Instantiate(TowerSpotOutPref, levelThreeScript.TowerOutSpotPos[i], Quaternion.identity); //spawns the tower spots outside the body
                    towerSpots.Add(curTowerSpot);
                }
                
                timePassed =0; //resets the time passed for the current wave
                wave =0; //sets the wave to the first wave
                waveDone = true; //sets the wave done to false
                spawn=1; //resets the spawn counter

            }
        }*/
    }
    

    public void Restart(){ //restart function used the reset the level
        for(int i = 0; i < towerSpots.Count;i++){
            Destroy(towerSpots[i]); //clears tower spots
        }
        for(int i = 0; i < towers.Count;i++){
            Destroy(towers[i]); //clears towers

        }
        for(int i = 0; i < enemies.Count;i++){
            //clears enemies
            Destroy(enemies[i]);
        }
    }

    void RoundOneDefault(){
        switch (spawn) {
            case 1:
                if(timePassed > 1){ //spawns the first enemy if the time passed is greater than 1 and the enemy has not been spawned
                    
                    SpawnRhinovirus();
                    spawn++;

                }
                break;
            case 2:
                if (timePassed > 3)
                {
                    //spawns the second enemy if the time passed is greater than 2 and the enemy has not been spawned
                    SpawnRhinovirus();
                    spawn++;
                }

                break;
            case 3:
                if(timePassed > 5){ //spawns the third enemy if the time passed is greater than 3 and the enemy has not been spawned
                    SpawnRhinovirus();
                    spawn++;
                }
                break;
            case 4:
                if(timePassed > 7){ //spawns the fourth enemy if the time passed is greater than 4 and the enemy has not been spawned
                    SpawnRhinovirus();
                    spawn++;
                }
                break;
            case 5:
                if(timePassed > 9){ //spawns the fourth enemy if the time passed is greater than 4 and the enemy has not been spawned
                    SpawnRhinovirus();
                    spawn++;
                }
                break;
            
            default:
                if(timePassed > 9){ //checks if the wave is over
                    timePassed = 0;
                    waveDone = true;
                    spawn = 1;
                }
                break;
            }
    }
    void RoundTwoDefault(){
        switch (spawn) {
            
            case 1:
                if(timePassed > 1){ //spawns the first enemy if the time passed is greater than 1 and the enemy has not been spawned
                    SpawnRhinovirus();
                    spawn++;

                }
                break;
            case 2:
                if (timePassed > 3)
                {
                    //spawns the second enemy if the time passed is greater than 2 and the enemy has not been spawned
                    SpawnRhinovirus();
                    spawn++;
                }

                break;
            case 3:
                if(timePassed > 5){ //spawns the third enemy if the time passed is greater than 3 and the enemy has not been spawned
                    SpawnRhinovirus();
                    spawn++;
                }
                break;
            case 4:
                if(timePassed > 7){ //spawns the fourth enemy if the time passed is greater than 4 and the enemy has not been spawned
                    SpawnRhinovirus();
                    spawn++;
                }
                break;
            case 5:
                if(timePassed > 9){ //spawns the fourth enemy if the time passed is greater than 4 and the enemy has not been spawned
                    SpawnRhinovirus();
                    spawn++;
                    timePassed = -1.3;
                }
                break;
            
            case 6:
                if(timePassed > 0.59f){ //spawns the first enemy if the time passed is greater than 1 and the enemy has not been spawned
                    SpawnRhinovirus();
                    spawn++;

                }
                break;
            case 7:
                if (timePassed > 3) {
                    //spawns the second enemy if the time passed is greater than 2 and the enemy has not been spawned
                    SpawnRhinovirus();
                    spawn++;
                }

                break;
            case 8:
                if(timePassed > 5){ //spawns the third enemy if the time passed is greater than 3 and the enemy has not been spawned
                    SpawnRhinovirus();
                    spawn++;
                }
                break;
            case 9:
                if(timePassed > 7){ //spawns the fourth enemy if the time passed is greater than 4 and the enemy has not been spawned
                    SpawnRhinovirus();
                    spawn++;
                }
                break;
            case 10:
                if(timePassed > 9){ //spawns the fourth enemy if the time passed is greater than 4 and the enemy has not been spawned
                    SpawnRhinovirus();
                    spawn++;
                }
                break;
            
            
            default:
                if(timePassed > 10){ //checks if the wave is over
                    timePassed = 0;
                    waveDone = true;
                    spawn = 1;
                }
                break;
        }
    }

    void RoundThreeDefault() {
        
        switch (spawn) {
            
            case 1:
                if(timePassed > 1){ //spawns the first enemy if the time passed is greater than 1 and the enemy has not been spawned
                    SpawnRhinovirus();
                    spawn++;

                }
                break;
            case 2:
                if (timePassed > 1.25f)
                {
                    //spawns the second enemy if the time passed is greater than 2 and the enemy has not been spawned
                    SpawnRhinovirus();
                    spawn++;
                }

                break;
            case 3:
                if(timePassed > 1.5f){ //spawns the third enemy if the time passed is greater than 3 and the enemy has not been spawned
                    SpawnRhinovirus();
                    spawn++;
                }
                break;
            case 4:
                if(timePassed > 1.75f){ //spawns the fourth enemy if the time passed is greater than 4 and the enemy has not been spawned
                    SpawnRhinovirus();
                    spawn++;
                }
                break;
            case 5:
                if(timePassed > 2f){ //spawns the fourth enemy if the time passed is greater than 4 and the enemy has not been spawned
                    SpawnRhinovirus();
                    spawn++;
                }
                break;
            
            case 6:
                if(timePassed > 2.25f){ //spawns the first enemy if the time passed is greater than 1 and the enemy has not been spawned
                    SpawnRhinovirus();
                    spawn++;

                }
                break;
            case 7:
                if (timePassed > 2.5f)
                {
                    //spawns the second enemy if the time passed is greater than 2 and the enemy has not been spawned
                    SpawnRhinovirus();
                    spawn++;
                }

                break;
            case 8:
                if(timePassed > 2.75f){ //spawns the third enemy if the time passed is greater than 3 and the enemy has not been spawned
                    SpawnRhinovirus();
                    spawn++;
                }
                break;
            default:
                if(timePassed > 3){ //checks if the wave is over
                    timePassed = 0;
                    waveDone = true;
                    spawn = 1;
                }
                break;
        }
    }

    void RoundFourDefault() {
        switch (spawn) {
            case 1:
                if(timePassed > 0) {
                    SpawnStaf();
                    spawn++;
                }
                break;
            case 2:
                if(timePassed > 4.5f) {
                    SpawnRhinovirus();
                    spawn++;
                }
                break;
            case 3:
                if(timePassed > 4.75f) {
                    SpawnRhinovirus();
                    spawn++;
                }
                break;
            case 4:
                if(timePassed > 5) {
                    SpawnRhinovirus();
                    spawn++;
                }
                break;
            case 5:
                if(timePassed > 5.25f) {
                    SpawnRhinovirus();
                    spawn++;
                }
                break;
            case 6:
                if(timePassed > 5.5f) {
                    SpawnRhinovirus();
                    spawn++;
                }
                break;
            default:
                if(timePassed > 5.75f){ //checks if the wave is over
                    timePassed = 0;
                    waveDone = true;
                    spawn = 1;
                }
                break;

        }
        
    }
    void RoundFiveDefault() {
        //spawn 2 x 5 stafs
        switch (spawn) {
            case 1:
                if(timePassed > 0.5f) {
                    StartCoroutine(RoundFiveDefaultSubwave());
                    spawn++;
                }
                break;
            default:
                if(timePassed > 7.2f){ //checks if the wave is over
                    timePassed = 0;
                    waveDone = true;
                    spawn = 1;
                }
                break;
        }


    }
    void RoundSixDefault() {
        //spawn 18 stafs and 12 rhinovirusses
        switch (spawn) {
            //spawn 12 rhinovirus
            case 1:
                if(timePassed > 0.5f) {
                    SpawnRhinovirus();
                    spawn++;
                }
                break;
            case 2:
                if(timePassed > 1) {
                    SpawnRhinovirus();
                    spawn++;
                }
                break;
            case 3:
                if(timePassed > 1.5f) {
                    SpawnRhinovirus();
                    spawn++;
                }
                break;
            case 4:
                if(timePassed > 2) {
                    SpawnRhinovirus();
                    spawn++;
                }
                break;
            case 5:
                if(timePassed > 2.5f) {
                    SpawnRhinovirus();
                    spawn++;
                }
                break;
            case 6:
                if(timePassed > 3) {
                    SpawnRhinovirus();
                    spawn++;
                }
                break;
            case 7:
                if(timePassed > 3.5f) {
                    SpawnRhinovirus();
                    spawn++;
                }
                break;
            case 8:
                if(timePassed > 4) {
                    SpawnRhinovirus();
                    spawn++;
                }
                break;
            case 9:
                if(timePassed > 4.5f) {
                    SpawnRhinovirus();
                    spawn++;
                }
                break;
            case 10:
                if(timePassed > 5) {
                    SpawnRhinovirus();
                    spawn++;
                }
                break;
            case 11:
                if(timePassed > 5.5f) {
                    SpawnRhinovirus();
                    spawn++;
                }
                break;
            case 12:
                if(timePassed > 6) {
                    SpawnRhinovirus();
                    spawn++;
                }
                break;
            //spawn 6x3 stafs
            //spawn 3 stafs 1
            case 13:
                if(timePassed > 8) {
                    SpawnStaf();
                    spawn++;
                }
                break;
            case 14:
                if(timePassed > 10) {
                    SpawnStaf();
                    spawn++;
                }
                break;
            default:
                if(timePassed > 10){ //checks if the wave is over
                    timePassed = 0;
                    waveDone = true;
                    spawn = 1;
                }
                break;
        }

    }
    void RoundSevenDefault() {
        switch (spawn) {
            case 1:
                if(timePassed > 0.5f) {
                    StartCoroutine(RoundSevenDefaultSubwave1());
                    StartCoroutine(RoundSevenDefaultSubwave2());
                    spawn++;
                }
                break;
            default:
                if(timePassed > 11.25){ //checks if the wave is over
                    timePassed = 0;
                    waveDone = true;
                    spawn = 1;
                }
                break;
        }
    }
    void RoundEightDefault() {
        switch (spawn) {
            case 1:
                if(timePassed > 0.5f) {
                    StartCoroutine(RoundEightDefaultSubwave());
                    spawn++;
                }
                break;
            default:
                if(timePassed > 10){ //checks if the wave is over
                    timePassed = 0;
                    waveDone = true;
                    spawn = 1;
                }
                break;
        }
    }
    void RoundNineDefault() {

        switch (spawn) {
            case 1:
                if(timePassed > 0) {
                    StartCoroutine(RoundNineDefaultSubwave());
                    spawn++;
                }
                break;
            case 2:
                if(timePassed > 5.25f) {
                    StartCoroutine(RoundNineDefaultSubwave());
                    spawn++;
                }
                break;
            default:
                if(timePassed > 5.25f){ //checks if the wave is over
                    timePassed = 0;
                    waveDone = true;
                    spawn = 1;
                }
                break;  
        }
        
    }
    void RoundTenDefault() {
        switch (spawn) {
            case 1:
                if(timePassed > 0.5f){
                    StartCoroutine(RoundTenDefaultSubwave1());
                    StartCoroutine(RoundTenDefaultSubwave2());
                    spawn++;
                }
                break;
            default:
                if(timePassed > 28){ //checks if the wave is over
                    timePassed = 0;
                    waveDone = true;
                    spawn = 1;
                }
                break;
        }
    }
    void RoundScalingDefault()
    {
        defScalingStaph = (int)(Math.Pow(spawn,Math.PI/3)*Math.Cos(0.5*Math.PI*Math.Pow(spawn,Math.PI/3)));
        if(defScalingStaph < 0) {
            defScalingStaph = 0;
        }
        defScalingRhino = (int)((Math.Pow(Math.PI,1.06)/Math.E)*spawn-defScalingStaph/1.06);
        
        StartCoroutine(SpawnEnemiesOutside(defScalingRhino));
        StartCoroutine(SpawnEnemiesInside(defScalingStaph));

    }

    void RoundOutside() {
        
        StartCoroutine(SpawnEnemiesOutside((int)(Math.Exp(spawn-1))));
            
        spawn++;
        timePassed = 0;
        waveDone = true;
        
    }

    IEnumerator SpawnEnemiesOutside(int numEnemies) {
        for (int i = 0; i < numEnemies; i++) {
            SpawnRhinovirus();

            if (i < numEnemies - 1)
            {
                // Hvis det ikke er den sidste fjende, vent
                yield return new WaitForSeconds((float)(Math.Pow(1.41421356237,-0.8*spawn+3)+0.1));
            }

        }
    }
    IEnumerator RoundFiveDefaultSubwave() {
        for (int i = 0; i < 10; i++) {
            SpawnRhinovirus();

            if (i < 9)
            {
                // Hvis det ikke er den sidste fjende, vent
                yield return new WaitForSeconds(0.75f);
            }

        }
    }

    IEnumerator RoundSevenDefaultSubwave1() {
        for (int i = 0; i < 10; i++) {
            SpawnRhinovirus();

            if (i < 14)
            {
                // Hvis det ikke er den sidste fjende, vent
                yield return new WaitForSeconds(0.75f);
            }

        }
    }
    
    IEnumerator RoundSevenDefaultSubwave2() {
        for (int i = 0; i < 2; i++) {
            SpawnStaf();

            if (i < 1)
            {
                // Hvis det ikke er den sidste fjende, vent
                yield return new WaitForSeconds(3);
            }

        }
    }
    
    IEnumerator RoundEightDefaultSubwave() {
        for (int i = 0; i < 5; i++) {
            SpawnStaf();

            if (i < 4)
            {
                // Hvis det ikke er den sidste fjende, vent
                yield return new WaitForSeconds(2.5f);
            }

        }
    }
    
    IEnumerator RoundNineDefaultSubwave() {
        for (int i = 0; i < 3; i++) {
            SpawnStaf();

            if (i < 2)
            {
                // Hvis det ikke er den sidste fjende, vent
                yield return new WaitForSeconds(0.75f);
            }

        }
    }

    IEnumerator RoundTenDefaultSubwave1() {
        for (int i = 0; i < 19; i++) {
            SpawnRhinovirus();

            if (i < 18)
            {
                // Hvis det ikke er den sidste fjende, vent
                yield return new WaitForSeconds(1f);
            }

        }
    }
    
    IEnumerator RoundTenDefaultSubwave2() {
        for (int i = 0; i < 10; i++) {
            SpawnStaf();

            if (i < 9)
            {
                // Hvis det ikke er den sidste fjende, vent
                yield return new WaitForSeconds(1.75f);
            }

        }
    }
    void SpawnRhinovirus() {
        curEnemy = Instantiate(EnemyOne, levelOneScript.startPos, Quaternion.identity);
        enemies.Add(curEnemy);
    }

    void SpawnStaf()
    {
        curEnemy = Instantiate(EnemyTwo, levelOneScript.startPos, Quaternion.identity);
        enemies.Add(curEnemy);
    }

    void RoundInside() {
        
        StartCoroutine(SpawnEnemiesInside((int)(((spawn-1) / (Math.Pow(spawn-1, -0.5))) * 3+3)));
        
        spawn++;
        timePassed = 0;
        waveDone = true;
    }

    IEnumerator SpawnEnemiesInside(int numEnemies)
    {
        for(int i = 0; i < numEnemies; i++) {

            SpawnRhinovirus();

            if(i < numEnemies-1) {
                // Hvis det ikke er den sidste fjende, vent
                yield return new WaitForSeconds((float)(Math.Pow(2, -0.8 * spawn+1)+0.1));
            }
        }
    }
    
    public bool IsThereEnemies() {
        
        if(enemies.Count > 0) {
            return true;
        } else {
            return false;
        }
        return false;
    } 
}