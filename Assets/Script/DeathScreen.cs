using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DeathScreen : MonoBehaviour
{
    public GameObject background;
    GameObject deathTxt;
    GameObject deathGrafic;
    GameObject restartBtn;
    GameObject MainCam;
    public Sprite rinovirusTxt;
    Sprite deathTexture;
    public Sprite stafylokkerTxt;
    Main mainScript;
    // Start is called before the first frame update
    void Start()
    {   
        MainCam = GameObject.Find("Main Camera");
        mainScript = MainCam.GetComponent<Main>();
        background = GameObject.Find("DSBackground");
        deathGrafic = GameObject.Find("DeathGrafic");
        restartBtn = GameObject.Find("RestartBtn");
        deathGrafic.SetActive(true);
        restartBtn.SetActive(true);
        switch (mainScript.lastHit){
            case "Rhinovirus":
                deathTexture = rinovirusTxt;
                deathGrafic.GetComponent<RectTransform>().localPosition = new Vector3(21, -56, 0);
                print(mainScript.lastHit);
                break;
            case "Stafylokker":
                print("hi");
                deathTexture = stafylokkerTxt;
                deathGrafic.GetComponent<RectTransform>().localPosition = new Vector3(21, -41, 0);
                break;
            default:
                print("Defaul enemy death screen you probably need to add a case for this enemy");
                break;
        } 
        deathGrafic.GetComponent<Image>().sprite = deathTexture;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
