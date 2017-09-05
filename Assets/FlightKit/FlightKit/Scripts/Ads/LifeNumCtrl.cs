using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class LifeNumCtrl : MonoBehaviour {
    /// <summary>
    /// 最大的生命数
    /// </summary>
    const int MAXLIFENUM = 10;
    /// <summary>
    /// 多长时间加载一条命
    /// </summary>
    const int TIMEFORALIFE = 60;
    TimeSpan loadingTime = new TimeSpan(0, 0, TIMEFORALIFE);
    /// <summary>
    /// 目前加载秒数
    /// </summary>
    private int loadingTimeSeconds = 0;
    /// <summary>
    /// 目前生命数
    /// </summary>
    public static int lifeNum;
    /// <summary>
    /// 目前加载比率=上次关闭时加载比率+目前加载秒数/多长时间加载一条命
    /// </summary>
    private float loading;
    private DateTime oriTime=new DateTime(0,0,0,0,0,0);
    private int startLoading = 0;
    public Text []lifeNumText;
    public Text LoadingTimeText;
    public GameObject gameController;
    public GameObject adsUI;
    public Image loadingUI;
    GameController controller;
    // Use this for initialization
    void Start () {
        controller = new GameController();
        controller = gameController.GetComponent<GameController>();
        if (PlayerPrefs.HasKey("StartLoading")&& PlayerPrefs.HasKey("LifeNum")&& PlayerPrefs.HasKey("PreTime")  && PlayerPrefs.HasKey("Loading"))
        {
            startLoading = PlayerPrefs.GetInt("StartLoading");
            lifeNum = PlayerPrefs.GetInt("LifeNum");
            DateTime preTime = DateTime.Parse(PlayerPrefs.GetString("PreTime"));
            Debug.Log("上次时间" + preTime);
            Debug.Log("目前时间" + DateTime.Now);
            if (lifeNum < 10 && startLoading == 1)
            {
                lifeNum += (int)((DateTime.Now - preTime).TotalSeconds / loadingTime.TotalSeconds);
                if (lifeNum >= 10)
                {
                    lifeNum = 10;
                }
            }
            if (loading <= 1&& startLoading == 1)
            {
                loading = (float)(PlayerPrefs.GetFloat("Loading") + ((DateTime.Now - preTime).TotalSeconds / loadingTime.TotalSeconds) - (int)((DateTime.Now - preTime).TotalSeconds / loadingTime.TotalSeconds));
                loadingTimeSeconds = (int)(loadingTime.TotalSeconds-(int)(loading * loadingTime.TotalSeconds));
                //针对loading>1的处理在Update里
            }    
            Debug.Log("一共" + lifeNum + "条命");
            Debug.Log("loading" + loading);
        }
        else
        {
            loadingTimeSeconds = (int)loadingTime.TotalSeconds;
            loading = 0;
            lifeNum = MAXLIFENUM;
        }
    }
	
	// Update is called once per frame
	void Update () {
        loadingUI.fillAmount = loading;
        for(int i = 0; i < lifeNumText.Length; i++)
        {
            lifeNumText[i].text = lifeNum.ToString();
        }
        if(startLoading == 1)
        {
            LoadingTimeText.enabled = true;
            LoadingTimeText.text = "NEXT     "+loadingTimeSeconds.ToString();
        }
        else
        {
            LoadingTimeText.enabled = false;
            loading = 0;
            loadingTimeSeconds = (int)(loading * loadingTime.TotalSeconds);
        }
       
        //当生命数=0才开启loading,=10则关闭loading
        if (lifeNum == 0)
        {
            if (Application.loadedLevel != 0)
            {
                NoLifeShowAdsMenu();
            }
            startLoading = 1;
        }
        if(lifeNum>=10)
        {
            startLoading = 0;
        }
        //loading
        if (startLoading==1)
        {
            loading = 1-(float)(loadingTimeSeconds / loadingTime.TotalSeconds);
            if ((DateTime.Now- oriTime).TotalSeconds > 1)
            {
                oriTime = DateTime.Now;
                loadingTimeSeconds--;
                if (loadingTimeSeconds <= 0)
                {
                    loadingTimeSeconds = (int)loadingTime.TotalSeconds;
                    loading = 0;
                    if (lifeNum < 10)
                    {
                        lifeNum++;
                    }
                }
            }
            
        }
        Debug.Log("loadingTimeSeconds" + loadingTimeSeconds);
        Debug.Log("生命数" + lifeNum);
    }
    //注意每个关卡暂停界面的重新开始都要挂上这个脚本的函数
    public void OnApplicationQuit()
    {    
        PlayerPrefs.SetString("PreTime", DateTime.Now.ToString());
        PlayerPrefs.SetInt("LifeNum", lifeNum);
        PlayerPrefs.SetFloat("Loading", (float)loading);
        PlayerPrefs.SetInt("StartLoading", startLoading);
        Debug.Log("quit");
        //PlayerPrefs.DeleteAll();
    }
    public void AdsAddLife(int addLifeNum)
    {
        lifeNum += addLifeNum;
    }
    public void NoLifeShowAdsMenu()
    {
        controller.PauseGame();
        controller.DisActiveUI();
        adsUI.SetActive(true);
    }
}
