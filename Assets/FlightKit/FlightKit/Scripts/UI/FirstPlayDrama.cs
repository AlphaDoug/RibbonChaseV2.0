using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPlayDrama : MonoBehaviour
{
    public GameObject drama;
    public GameObject mainMenu;
    public List<GameObject> pages = new List<GameObject>();
	// Use this for initialization
	void Start ()
    {
        if (PlayerPrefs.HasKey("IsFirstPlay"))
        {
            //不是首次玩游戏

        }
        else
        {
            //是首次玩游戏
            PlayerPrefs.SetString("IsFirstPlay", "False");
            drama.SetActive(true);
            mainMenu.SetActive(false);

        }
        StartLastPageActiveMonitor();

    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
       
	}

    public void StartLastPageActiveMonitor()
    {
        StartCoroutine(LastPageActiveMonitor());
    }
    IEnumerator LastPageActiveMonitor()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            if (!pages[pages.Count - 1].activeSelf)
            {
                drama.SetActive(false);
                for (int i = 0; i < pages.Count; i++)
                {
                    pages[i].SetActive(true);
                    foreach (Transform child in pages[i].transform)
                    {
                        child.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                    }
                    pages[i].GetComponent<Animator>().Play("New State");
                }
                break;
            }
        }
    }
}
