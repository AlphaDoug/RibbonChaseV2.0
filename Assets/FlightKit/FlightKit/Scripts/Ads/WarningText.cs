using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningText : MonoBehaviour {
    public GameObject en;
    public GameObject cn;
	// Use this for initialization
	void Start ()
    {
        if (PlayerPrefs.HasKey("language"))
        {
            if (PlayerPrefs.GetInt("language") == 0)
            {
                //中文
                cn.SetActive(true);
            }
            else
            {
                //英文
                en.SetActive(true);
            }
        }
       
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetDisappearAnim()
    {

        GetComponent<Animator>().SetBool("Disappear",true);
    }
	public void Destroy()
	{
		Destroy (this.gameObject);
	}
}
