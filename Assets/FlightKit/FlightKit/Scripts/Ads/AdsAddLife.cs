using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Together;

public class AdsAddLife : MonoBehaviour
{
    void Awake()
    {
        TGSDK.SetDebugModel(true);
#if UNITY_IOS && !UNITY_EDITOR
		        TGSDK.Initialize ("hP7287256x5z1572E5n7");
#elif UNITY_ANDROID && !UNITY_EDITOR
		        //TGSDK.Initialize ("59t5rJH783hEQ3Jd7Zqr");
                //a4AIA3319q2AdoW2M014
                TGSDK.Initialize ("a4AIA3319q2AdoW2M014");
#endif
    }

    // Use this for initialization
    void Start()
    {
        TGSDK.PreloadAd();
        TGSDK.AdShowSuccessCallback = OnAdShowSuccess;
        TGSDK.AdShowFailedCallback = OnAdShowFailed;
        TGSDK.AdCompleteCallback = OnAdComplete;
        TGSDK.AdCloseCallback = OnAdClose;
        TGSDK.AdClickCallback = OnAdClick;
        TGSDK.AdRewardSuccessCallback = OnAdRewardSuccess;
        TGSDK.AdRewardFailedCallback = OnAdRewardFailed;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ShowAds()
    {
        if (TGSDK.CouldShowAd("Ig8T0upxRDjV3JznCgo"))
        {
            DebugConsole.Log("广告准备好了");
            TGSDK.ShowAd("Ig8T0upxRDjV3JznCgo");
        }
        else
        {
            DebugConsole.Log("广告未准备好");
        }
    }
    public void OnAdShowSuccess(string ret)
    {
        DebugConsole.Log("广告播放成功" + ret);
    }

    public void OnAdShowFailed(string error)
    {
        DebugConsole.Log("广告播放失败" + error);
    }

    public void OnAdComplete(string ret)
    {
        DebugConsole.Log("广告播放结束" + ret);
    }

    public void OnAdClose(string ret)
    {
        DebugConsole.Log("广告被关闭" + ret);
    }

    public void OnAdClick(string ret)
    {
        DebugConsole.Log("广告被点击" + ret);
    }

    public void OnAdRewardSuccess(string ret)
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<LifeNumCtrl>().AdsAddLife(5);
        DebugConsole.Log("奖励类广告达成领奖条件" + ret);
    }

    public void OnAdRewardFailed(string error)
    {
        DebugConsole.Log("奖励类广告未达成领奖条件" + error);
    }
}
