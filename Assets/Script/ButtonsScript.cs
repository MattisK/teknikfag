using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

public class ButtonsScript : MonoBehaviour
{
    public int spritCost;
    public int immunoCost;
    GameObject mainCam;
    Main mainScript;
    public GameObject spritTower;
    public GameObject immunoTower;
    public GameObject CancelButton;
    bool bestRan;
    public GameObject bestiaryClose;
    public GameObject bestiaryOpen;
    public GameObject bestiary;
    public GameObject GulStafPage;
    public GameObject RhinovirusPage;
    public GameObject FjenderBtns;
    public GameObject SpritTårnPage;
    public GameObject ImmunTårnPage;
    public GameObject TårneBtns;
    public GameObject startButton; 
    public GameObject quitButton;
    public GameObject settingsButton;
    public GameObject DeathScreen;
    public GameObject WinScreen;
    public GameObject startWaveBtn;
    public GameObject BestiaryStart;
    public GameObject FjenderPage;
    public GameObject SplashScreen;
    public GameObject StartArrow;
    public GameObject Logo;
    public double animateStartScreen;
    public double animateStartScreenFinish;
    public GameObject TårnePage;
    // Start is called before the first frame update
    void Start() //prepares the button script and sets the prices of the towers on game start
    {
        immunoCost = 250; //sets price of immuno tower
        spritCost = 200; //sets price of sprit tower
        mainCam = GameObject.Find("Main Camera");
        mainScript = mainCam.GetComponent<Main>(); //locates the main script for future use
        animateStartScreenFinish = 1; //indicates that the start menu animation hasnt run yet

    }

    void Update()
    {
        if(bestRan == false){ //runs the bestiary setup whenever bestRan is set to false
        bestiaryClose.SetActive(false);
        bestiaryOpen.SetActive(false);
        bestRan = true;
        }
        if(animateStartScreen > 0){
            animateStartScreen = animateStartScreen - Time.deltaTime;
            StartArrow.transform.position = new Vector3(StartArrow.transform.position.x, StartArrow.transform.position.y-400*Time.deltaTime*(Screen.height/1080f), StartArrow.transform.position.z);
            Logo.transform.position = new Vector3(Logo.transform.position.x-20*Time.deltaTime*(Screen.width/1920f), Logo.transform.position.y+300*Time.deltaTime*(Screen.height/1080f), Logo.transform.position.z);
            Logo.transform.localScale = new Vector3(Logo.transform.localScale.x-2f*Time.deltaTime, Logo.transform.localScale.y-2f*Time.deltaTime, Logo.transform.localScale.z);
        } else if(animateStartScreenFinish ==0){
            animateStartScreenFinish = 1;
            StartArrow.SetActive(false);
            mainScript.gameState = "MainMenu";

        }
    }
    

    public void StartArrowBtn(){ //the first button on the start menu that runs the startup animation
        animateStartScreen = 1;//starts the animation over and runs it over one second
        animateStartScreenFinish= 0; //indicates that the animation is perpared to run
        StartArrow.GetComponent<Button>().enabled= false; //disables the start button so it cant be pressed again
    }

    // Update is called once per frame

    public void SpritSummon()//logic for the buy sprit tower button
    {
        if(mainScript.money >= spritCost && mainScript.holding == 0)  //checks if the player is holding a tower and if they have enough money
        {
            mainScript.money = mainScript.money - spritCost; //removes the cost of the tower from the players money
            mainScript.holding = 1; //sets the holding variable to 1 to indicate that the player is holding a tower
            mainScript.holdingTower = Instantiate(spritTower, new Vector3(0,0,0), Quaternion.identity); //creates a sprit tower that can then be placed by the player
            CancelButton.SetActive(true); //enables the cancel buy button

        }
    }

    public void ImmunoSummon(){ //logic for the buy immuno tower button
        if(mainScript.money >= immunoCost&& mainScript.holding==0) //same as above but for the immuno tower
        {
            mainScript.money = mainScript.money - immunoCost;//same as above but for the immuno tower
            mainScript.holding = 2; //indicates that the player is holding an immuno tower
            mainScript.holdingTower = Instantiate(immunoTower, new Vector3(0,0,0), Quaternion.identity); //creates an immuno tower like above
            CancelButton.SetActive(true); //enables the cancel buy button

        }
    }
    public void Back(){ //goes back to the main menu from the settings menu
        mainScript.gameState = "MainMenu"; //changes game state to main menu
        mainScript.mainRan=false; //indicates that main menu setup needs to be run
        mainScript.setRan=false; //indicates that the settingsmenu is not open
    }
    public void OpenBestiary(){ //opens the bestiary
        bestiary.SetActive(true); //enables the bestiary
        bestiaryClose.SetActive(true); //enables the close bestiary button
    }
    public void CloseBestiary(){ //closes the bestiary from the close bestiary button
        bestiary.SetActive(false);//disables the bestiary
    }

    public void startWave(){ //starts the next wave on button press
        if(mainScript.waveDone == true){ //checks if the current wave is done
            mainScript.wave++; //increments the wave counter
            mainScript.waveDone = false; //indicates that the new wave isnt done yet
            startWaveBtn.SetActive(false); //disalbes itself
        }

        
    }

    public void ClickStart() //starts the game
    { 
        startButton.SetActive(false); //hides the start button
        quitButton.SetActive(false); //hides the quit button
        settingsButton.SetActive(false); //hides the settings button
        Logo.SetActive(false);
        SplashScreen.SetActive(false);
        mainScript.gameState = "levelOne"; //changes the game state to level one
        mainScript.ran = 0; //resets wether the level has been ran before

    }
    public void FjenderTab(){ //bestiary logic for the "fjender" tab
        FjenderPage.SetActive(true); //enables the fjender page
        FjenderBtns.SetActive(true); //enables the fjender buttons
        GulStafPage.SetActive(false); //disables the gul staf page to clear previously opened pages
        RhinovirusPage.SetActive(false); //disables the rhinovirus page to clear previously opened pages
        BestiaryStart.SetActive(false); //disables the bestiary start page to clear the start text
        TårnePage.SetActive(false); //disables the tårne page to clear previously opened pages

    }

    public void GulStafBtn(){ //bestiary logic for the "gul staf" button
        FjenderBtns.SetActive(false); //disables the fjender buttons to clear previously opened pages
        GulStafPage.SetActive(true); //enables the gul staf page
    }

    public void RhinovirusBtn(){ //same but for rhinovirus
        FjenderBtns.SetActive(false);
        RhinovirusPage.SetActive(true);
    }

     public void TårneTab(){ //same as fjender tab but for tårne
        TårnePage.SetActive(true);
        TårneBtns.SetActive(true);
        SpritTårnPage.SetActive(false);
        ImmunTårnPage.SetActive(false);
        BestiaryStart.SetActive(false);
        FjenderPage.SetActive(false);

    }

    public void SpritTårnBtn(){ //same as gulstaf but for the sprit tower page	
        TårneBtns.SetActive(false);
        SpritTårnPage.SetActive(true);
    }

    public void ImmunTårnBtn(){ //same as above but for the immun tower page
        TårneBtns.SetActive(false);
        ImmunTårnPage.SetActive(true);
    }
    public void QuitGame(){ //quits the game (hopefully)
        Application.Quit();
    }
    public void Settings(){ //opens the settings menu
        mainScript.gameState = "settings";
    }
    public void Restart(){
        mainScript.Restart(); //resets the game
        mainScript.gameState = "levelOne"; //changes the game state to level one
        mainScript.ran = 0; //resets wether the level has been ran before
        DeathScreen.SetActive(false); //hides the death screen
        WinScreen.SetActive(false); //hides the win screen
        Destroy(mainScript.holdingTower); //destroys the tower the player is holding
        mainScript.holding = 0; //sets the holding variable to 0
        CancelButton.gameObject.SetActive(false); //disables the cancel button
    }
    public void Win(){ //enables the win screen
        WinScreen.SetActive(true); //enables the win screen
    }
    
    public void BackFromLevel(){ //back button logic for the end of level screens (win and death screen)
        mainScript.gameState = "MainMenu"; //changes the game state to main menu
        mainScript.mainRan=false; //indicates that main menu setup needs to be run
        mainScript.setRan=false; //indicates that the settingsmenu is not open
        WinScreen.SetActive(false); //hides the win screen
        DeathScreen.SetActive(false); //hides the death screen
        mainScript.Restart(); //resets the game
        bestiaryOpen.SetActive(false);
        mainScript.ran = 0; //resets wether the level has been ran before
    }
    public void CancelPlacement(){ //cancels the placement of a tower
        mainScript.holding = 0; //sets the holding variable to 0
        if(mainScript.holdingTower.tag == "ImmunoTower"){ //refunds the player money based on the tower they were holding
            mainScript.money += 250;
        } else if(mainScript.holdingTower.tag == "SpritTower"){ 
            mainScript.money += 200;
        }
        Destroy(mainScript.holdingTower); //destroys the tower the player is holding
        CancelButton.gameObject.SetActive(false); //disables the cancel button
    }
}
