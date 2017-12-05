using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 语言切换控制
/// </summary>
public class SelectLanguage : MonoBehaviour
{
    public List<GameObject> englishUI;
    public List<GameObject> chineseUI;
    // Use this for initialization
     void Awake()
    {
        //PlayerPrefs.DeleteAll();
    }
    void Start ()
    {
        if (PlayerPrefs.HasKey("language"))
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
        else
        {
            if (Application.systemLanguage == SystemLanguage.ChineseSimplified)
            {
            
                PlayerPrefs.SetInt("language", 0);
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
            
                PlayerPrefs.SetInt("language", 1);
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
	
}
