using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToStart : MonoBehaviour {
    public GameObject clickToStart;
    public GameObject mainMenu;

	// Use this for initialization
	void Start ()
    {
        if (GlobalVariable.runTimeCount == 0)
        {
            clickToStart.SetActive(true);
            mainMenu.SetActive(false);
            GlobalVariable.runTimeCount++;
        }
        else
        {
            clickToStart.SetActive(false);
            mainMenu.SetActive(true);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
