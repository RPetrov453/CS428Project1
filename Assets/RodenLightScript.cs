using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;


public class RodenLightScript : MonoBehaviour
{

    public GameObject Roden;
    public GameObject RodenLight;

    //change to hex 904C5B
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("CheckOrientation", 2f,2f);
    }

    bool UpsideDown = false;
    bool specialLight = false;
    bool justTurnedOff = false;


    // 0 is normal,
    // 1 is upside down
    // previous state is 0;
    // Upon going to 1, then back to 0, change lighting.
    // Upon staying at 0, change nothing.
    //Upon changing to 1 again, if lighting is changed
    // Upon going back to 0, nothing changes, prev is 1.
    //Upon switching back to 1, orientation


    // Update is called once per frame

    void CheckOrientation()
    {

        if (Roden.transform.up.y < 0f) //Are upsidedown.
        {
            //Debug.Log("We just went upside down.");

            UpsideDown = true;
            if (specialLight == true)
            { //If light was on, toggle off.
                specialLight = false;
                RodenLight.GetComponent<Light>().color = Color.white;
                RodenLight.GetComponent<Light>().intensity = 0.5f;
                justTurnedOff = true;
            }


        }
        else if (Roden.transform.up.y > 0f) //Upright 
        {
            //Debug.Log("Went upright.");
            if (UpsideDown == true && specialLight == false && justTurnedOff == false) //Activated the toggle, turn light on.
            {
                //Debug.Log("Went upright after the toggle, with light off. Turning Light on.");
                RodenLight.GetComponent<Light>().color = new Color(.56f, .30f, .35f);
                RodenLight.GetComponent<Light>().intensity = 1.2f;
                UpsideDown = false;
                specialLight = true;
            }
            else if (justTurnedOff == true)
            {
                UpsideDown = false;
                justTurnedOff = false;
            }

        }








    }

}
