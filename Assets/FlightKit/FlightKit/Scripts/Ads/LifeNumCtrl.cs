using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class LifeNumCtrl : MonoBehaviour {
    /// <summary>
    /// 最大的生命数
    /// </summary>
    const int MAXLIFENUM = 10;
    /// <summary>
    /// 多长时间加载一条命
    /// </summary>
    const int TIMEFORALIFE = 10;
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
    private float preTime=0;
    private int startLoading = 0;
    // Use this for initialization
    void Start () {
        if (PlayerPrefs.HasKey("StartLoading")&& PlayerPrefs.HasKey("LifeNum")&& PlayerPrefs.HasKey("PreTime")  && PlayerPrefs.HasKey("Loading"))
        {
            startLoading = PlayerPrefs.GetInt("StartLoading");
            lifeNum = PlayerPrefs.GetInt("LifeNum");
            DateTime preTime = DateTime.Parse(PlayerPrefs.GetString("PreTime"));
            Debug.Log("上次时间" + preTime);
            Debug.Log("目前时间" + DateTime.Now);
            if (lifeNum < 10)
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
                loadingTimeSeconds = (int)(loading * loadingTime.TotalSeconds);
                //针对loading>1的处理在Update里
            }       
            Debug.Log("一共" + lifeNum + "条命");
            Debug.Log("loading" + loading);
        }
        else
        {
            loadingTimeSeconds = 0;
            loading = 0;
            lifeNum = MAXLIFENUM;
        }
    }
	
	// Update is called once per frame
	void Update () {
        //当生命数=0才开启loading,=10则关闭loading
        if (lifeNum == 0)
        {
            startLoading = 1;
        }
        if(lifeNum==10)
        {
            startLoading = 0;
        }
        //loading
        if (startLoading==1)
        {
            loading = (float)(loadingTimeSeconds / loadingTime.TotalSeconds);
            if (Time.time - preTime > 1)
            {
                preTime = Time.time;
                loadingTimeSeconds++;
                if (loadingTimeSeconds > loadingTime.TotalSeconds)
                {
                    loadingTimeSeconds = 0;
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
}
