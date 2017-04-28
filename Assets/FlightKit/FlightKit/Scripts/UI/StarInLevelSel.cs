using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StarInLevelSel : MonoBehaviour {
    private  Image [] stars;
	// Use this for initialization
	void Start () {
        //Debug.Log("LevelStarNum_" + Application.loadedLevel+ PlayerPrefs.GetInt("LevelStarNum_" + Application.loadedLevel));
        stars = gameObject.GetComponentsInChildren<Image>();
        if(PlayerPrefs.HasKey("LevelStarNum_" + gameObject.name.Substring(10)))
        {
            for(int i=0;i<PlayerPrefs.GetInt("LevelStarNum_" + gameObject.name.Substring(10)); i++)
            {
                stars[i].enabled = true;
            }
        }
	}
	
}
