using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CloseOrHome : MonoBehaviour {
    [SerializeField]
    public Button homeButton;
    [SerializeField]
    public Button closeButton;
	
	// Update is called once per frame
	void Update () {
        if (LifeNumCtrl.lifeNum == 0)
        {
            homeButton.gameObject.SetActive(true);
            closeButton.gameObject.SetActive(false);
        }
        else
        {
            homeButton.gameObject.SetActive(false);
            closeButton.gameObject.SetActive(true);
        }
	}
}
