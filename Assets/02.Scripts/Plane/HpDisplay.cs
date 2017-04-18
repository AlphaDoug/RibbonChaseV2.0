using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HpDisplay : MonoBehaviour {
    public Image hp;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<Text>().text =hp.fillAmount.ToString();
	}
}
