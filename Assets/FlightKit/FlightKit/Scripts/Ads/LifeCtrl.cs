using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class LifeCtrl : MonoBehaviour {
    /// <summary>
    /// 最大的生命数
    /// </summary>
    const int MAXLIFENUM = 10;
    /// <summary>
    /// 现存的生命数
    /// </summary>
    public static int curLifeNum= 0;
    public static int loadingTime =5;
    /// <summary>
    /// 是否是第一次触发
    /// </summary>
    public bool isfirst=false;
    /// <summary>
    /// 多长时间增加一条生命
    /// </summary>
    public int timeForALife = 120;
    /// <summary>
    /// 系统触发增加生命的初始时间
    /// </summary>
    public float preTime =0;
    // Use this for initialization
    private void Awake()
    {

        // PlayerPrefs.DeleteAll();

        if ((PlayerPrefs.HasKey("CurY") && DateTime.Now.Year > PlayerPrefs.GetInt("CurY"))
            || (PlayerPrefs.HasKey("CurMouth") && DateTime.Now.Month > PlayerPrefs.GetInt("CurMouth"))           
            )
        {
            curLifeNum = MAXLIFENUM;
        }

        if (PlayerPrefs.HasKey("loadingTime"))
        {
            loadingTime = PlayerPrefs.GetInt("loadingTime");
        }
        if (PlayerPrefs.HasKey("CurLifeNum") &&PlayerPrefs.HasKey("CurD")&& PlayerPrefs.HasKey("CurH") && PlayerPrefs.HasKey("CurM") && PlayerPrefs.HasKey("CurS"))
        {
            long temp = PlayerPrefs.GetInt("CurLifeNum")
                + (long)(((DateTime.Now.Day-PlayerPrefs.GetInt("CurD"))*86400+(DateTime.Now.Hour-PlayerPrefs.GetInt("CurH"))*3600+(DateTime.Now.Minute - PlayerPrefs.GetInt("CurM")) * 60
                + DateTime.Now.Second - PlayerPrefs.GetInt("CurS")) / 5);
            if (temp > MAXLIFENUM)
            {
                curLifeNum = MAXLIFENUM;
            }
            else if (temp > 0 && temp < MAXLIFENUM)
            {
                curLifeNum =(int) temp;
            }
            
        }


    }
	
	// Update is called once per frame
	void Update () {
        
        if(Time.time - preTime > 1)
        {
            loadingTime--;
            preTime = Time.time;
            Debug.Log("loadingTime"+ loadingTime);
        }
        if (loadingTime == 0)
        {
            //Debug.Log("curLifeNum" + curLifeNum);
            Debug.Log("sssssssssssssssss" );
            if (curLifeNum < MAXLIFENUM)
            {
                curLifeNum++;
                
            }
            loadingTime = 5;
        }
        Debug.Log("curLifeNum" + curLifeNum);
    }

    void LoadLife()
    {

    }
    private void OnEnable()
    {
        Debug.Log("OnEnable");
    }
    private void OnDisable()
    {
        Debug.Log("OnDisable");
    }
    void OnApplicationQuit()
    {
        //PlayerPrefs.SetInt("CurY", DateTime.Now.Year);
        //PlayerPrefs.SetInt("CurMouth", DateTime.Now.Month);
        
        PlayerPrefs.SetInt("loadingTime", loadingTime);
        PlayerPrefs.SetInt("CurLifeNum", curLifeNum);
        PlayerPrefs.SetInt("CurD", DateTime.Now.Day);
        PlayerPrefs.SetInt("CurH", DateTime.Now.Hour);
        PlayerPrefs.SetInt("CurM", DateTime.Now.Minute);
        PlayerPrefs.SetInt("CurS", DateTime.Now.Second);
        Debug.Log("quit");
    }
}
