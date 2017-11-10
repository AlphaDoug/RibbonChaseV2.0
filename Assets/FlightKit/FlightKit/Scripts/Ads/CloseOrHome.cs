using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CloseOrHome : MonoBehaviour {
    public GameObject homeButton;
	public GameObject closeButton;
	// Update is called once per frame
	void Update () {
        if (LifeNumCtrl.lifeNum == 0)
        {
            homeButton.SetActive(true);
            closeButton.SetActive(false);
        }
        else
        {
            homeButton.SetActive(false);
            closeButton.SetActive(true);
        }
	}
}
