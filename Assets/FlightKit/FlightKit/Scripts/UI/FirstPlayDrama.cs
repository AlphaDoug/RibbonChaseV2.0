using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// 控制剧情动画播放
/// </summary>
public class FirstPlayDrama : MonoBehaviour
{
    /// <summary>
    /// 所有剧情
    /// </summary>
    public GameObject dramaALL;
    /// <summary>
    /// 前半剧情
    /// </summary>
    public GameObject dramaFRONT;
    /// <summary>
    /// 后半剧情
    /// </summary>
    public GameObject dramaEND;
    /// <summary>
    /// 剧情显示完之后显示的界面
    /// </summary>
    public GameObject mainMenu;
    public AudioSource bgm;

    private AnimatorStateInfo animatorInfoALL;
    private AnimatorStateInfo animatorInfoFRONT;
    private AnimatorStateInfo animatorInfoEND;
    private int lastPageNum;
	// Use this for initialization
	void Start ()
    {
        //若不是主场景,那么不播放开场动画并且直接返回
        if (Application.loadedLevel != 0)
        {
            return;
        }

        if (PlayerPrefs.HasKey("IsFirstPlay"))
        {
            //不是首次玩游戏,并且没有全部通关
            if (!PlayerPrefs.HasKey("level_6") || PlayerPrefs.HasKey("level_6") && PlayerPrefs.GetInt("level_6") == 0)
            {
               
            }
            //不是首次玩游戏,但是全部通关
            if (PlayerPrefs.HasKey("level_6") && PlayerPrefs.GetInt("level_6") == 1)
            {
                
            }
        }
        else
        {
            //是首次玩游戏,那么不可能全通关,剧情动画只播放一半
            PlayerPrefs.SetString("IsFirstPlay", "False");
            mainMenu.SetActive(false);
            // bgm.gameObject.SetActive(false);
            dramaFRONT.SetActive(true);
        }
        

    }
	
	// Update is called once per frame
	void Update ()
    {

        if (dramaALL)
        {
            animatorInfoALL = dramaALL.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
            if ((animatorInfoALL.normalizedTime > 1.0f) && (animatorInfoALL.IsName("ALL")))//normalizedTime: 范围0 -- 1,  0是动作开始，1是动作结束  
            {
                dramaALL.SetActive(false);
                mainMenu.SetActive(true);
                bgm.gameObject.SetActive(true);
            }
        }
        if (dramaFRONT)
        {
            animatorInfoFRONT = dramaFRONT.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
            if ((animatorInfoFRONT.normalizedTime > 1.0f) && (animatorInfoFRONT.IsName("FRONT")))//normalizedTime: 范围0 -- 1,  0是动作开始，1是动作结束  
            {
                dramaFRONT.SetActive(false);
                mainMenu.SetActive(true);
                bgm.gameObject.SetActive(true);
            }
        }
        if (dramaEND)
        {
            animatorInfoEND = dramaEND.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
            if ((animatorInfoEND.normalizedTime > 1.0f) && (animatorInfoEND.IsName("END")))//normalizedTime: 范围0 -- 1,  0是动作开始，1是动作结束  
            {
                dramaEND.transform.parent.gameObject.SetActive(false);
                dramaEND.SetActive(false);
                mainMenu.SetActive(true);
                bgm.gameObject.SetActive(true);
            }
        }
        
    }

    public void PlayDrama()
    {

        //全部通关
        if (PlayerPrefs.GetInt("level_6") == 1)
        {
            dramaALL.SetActive(true);
			dramaFRONT.SetActive(false); 
        }
        else
        {
            dramaFRONT.SetActive(true); 
			dramaALL.SetActive(false);
        }

    }
}
