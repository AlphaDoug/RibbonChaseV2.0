using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectLanguage : MonoBehaviour
{
    public List<GameObject> englishUI;
    public List<GameObject> chineseUI;
	// Use this for initialization
	void Start ()
    {
        if (PlayerPrefs.GetInt("language") == 0)
        {
            //中文
            for (int i = 0; i < englishUI.Count; i++)
            {
                englishUI[i].SetActive(false);
            }
            for (int i = 0; i < chineseUI.Count; i++)
            {
                chineseUI[i].SetActive(true);
            }
        }
        else
        {
            //英文
            for (int i = 0; i < englishUI.Count; i++)
            {
                englishUI[i].SetActive(true);
            }
            for (int i = 0; i < chineseUI.Count; i++)
            {
                chineseUI[i].SetActive(false);
            }
        }
    }
	
}
