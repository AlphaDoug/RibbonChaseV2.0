using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class CheckUpdate : MonoBehaviour
{
    public GameObject updateWindow;
    public GameObject findNewVersion;
    public GameObject isNoWifi;
    private bool isAndroid;
    private AndroidJavaClass jc;
    private AndroidJavaObject jo;
    // Use this for initialization
    void Start()
    {
        Debug.Log(GlobalVariable.runTimeCount);
        if (GlobalVariable.runTimeCount != 1)
        {
            return;
        }
        if (Application.platform == RuntimePlatform.Android)
        {
            jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
            isAndroid = true;
        }
        if (isAndroid)
        {
            if (!IsNetworkAvailable())
            {
                jo.Call("showDialog", "Watch", "Network Unavailable");
                return;
            }
            if (IsHaveUpdate())
            {
                updateWindow.SetActive(true);
                findNewVersion.SetActive(true);
            }
            if (!IsHaveUpdate())
            {
                //jo.Call("showDialog", "Watch", "There is no update.");
            }
        }
        GlobalVariable.runTimeCount++;
    }
    private bool IsNetworkAvailable()
    {
        if (isAndroid)
        {
            return jo.Call<bool>("isNetworkAvailable");
        }
        return false;
    }
    private bool IsHaveUpdate()
    {
        var url = "www.ribbonchase.top";
        var content = jo.Call<string>("getHTML", url);
        string pattern = @"<!--Version=(.*?)-->";
        string matchString = null;
        foreach (Match match in Regex.Matches(content, pattern))
            matchString = match.Value;
        var lastestVersionName = matchString.Substring(13, 7);
        var localVersionName = jo.Call<string>("getVersion");
        if (lastestVersionName != localVersionName)
        {
            if (localVersionName == "Cannot find version name")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            return false;
        }
    }
    private void OpenExternBrowser()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            var url = "www.ribbonchase.top";
            AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
            var content = jo.Call<string>("getHTML", url);
            string pattern = @"<!--Version=(.*?)-->";
            string matchString = null;
            foreach (Match match in Regex.Matches(content, pattern))
                matchString = match.Value;
            var versionName = matchString.Substring(13, 7);

            AndroidJavaClass jc1 = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject jo1 = jc1.GetStatic<AndroidJavaObject>("currentActivity");
            jo1.Call("DownloadAPK", "http://104.236.215.170/RibbonChaseV" + versionName + ".apk");
        }
    }
    public void OpenIsWifi()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            AndroidJavaClass jc1 = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject jo1 = jc1.GetStatic<AndroidJavaObject>("currentActivity");
            if (jo1.Call<bool>("isWifi"))
            {
                OpenExternBrowser();
            }
            else
            {
                isNoWifi.SetActive(true);
            }
        }
    }
    public void NoWifi_YES()
    {
        OpenExternBrowser();
    }


}
