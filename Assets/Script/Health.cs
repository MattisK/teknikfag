using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Health : MonoBehaviour
{   
    TextMeshProUGUI mText;
    GameObject mainCam;
    Main mainScript;
    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.Find("Main Camera"); //locates the main camera
        mainScript = mainCam.GetComponent<Main>(); //gets the main script from the main camera
        mText = this.GetComponent<TMPro.TextMeshProUGUI>(); //gets the text component attached to the object
    }

    // Update is called once per frame
    void Update()
    {
        mText.text = ""+mainScript.playerHealth; //changes the health text to the current health of the player
    }
}
