using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectLanguage : MonoBehaviour {
    public GameObject[] Chinese;
    public GameObject[] English;
	// Use this for initialization
	void Start () {
        if (PlayerPrefs.GetInt("language") == 0)
        {
            for (int i = 0; i < Chinese.Length; i++)
            {
                Chinese[i].SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < English.Length; i++)
            {
                English[i].SetActive(true);
            }
        }
	}
	
}
