using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerInSpot : MonoBehaviour
{   
    GameObject Level;
    GameObject LevelSprite;
    GameObject Main;
    Main mainScript;
    // Start is called before the first frame update
    void Start() //checks what level the player is on and locates the level and level sprite
    {   
        Main = GameObject.Find("Main Camera");
        mainScript = Main.GetComponent<Main>();
        if(mainScript.gameState == "levelOne"){
            Level = GameObject.Find("LevelOne");
            LevelSprite = GameObject.Find("LevelOneSprite");
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
