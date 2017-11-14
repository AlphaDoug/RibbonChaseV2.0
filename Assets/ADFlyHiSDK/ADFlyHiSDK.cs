using System.Runtime.InteropServices;
using UnityEngine;

public class ADFlyHiSDK : MonoBehaviour {

    public static AndroidJavaObject _plugin;
    public static AndroidJavaObject _unityActivity;
    private const string GAME_OBJ_NAME = "AdFlyHiSDK";

    public enum Oritation
    {
        Landscape = 0,//横屏
        Portrait = 1,//竖屏
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    #region CallBack
    public static System.Action OnAdShowSuccess = null;
    public static System.Action<string> OnAdShowFail = null;
    #endregion

    static ADFlyHiSDK()
    {
        try
        {
            // create a new GO for our manager
            var go = new GameObject(GAME_OBJ_NAME);
            go.AddComponent<ADFlyHiSDK>();
            DontDestroyOnLoad(go);
        }
        catch (UnityException)
        {
            Debug.LogWarning("It looks like you have the AdFlyHiSDK on a GameObject in your scene. Please remove the script from your scene.");
        }

#if UNITY_ANDROID
        using (var pluginClass = new AndroidJavaClass("ADFlyHiSDKPlugin.ADFlyHiSDKPlugin"))
            _plugin = pluginClass.CallStatic<AndroidJavaObject>("instance");
#endif
    }

    /// <summary>
    /// SDK插件初始化
    /// </summary>
    /// <param name="appKey">应用ID</param>
    /// <param name="channelID">渠道ID</param>
    /// <param name="appOritation">横竖屏</param>
    /// <param name="debugMode">是否测试模式</param>
    [DllImport("__Internal")]
    internal static extern void _initSDK(string appKey, string channelID, int oritation);
    public static void init(string appKey, string channelID, Oritation appOritation, bool debugMode )
    {
        if(OnAdShowSuccess == null || OnAdShowFail == null)
        {
            throw new System.Exception("请在初始化之前定义广告播放的回调函数");
        }
        else
        {
            Debug.Log("测试回调方法");
           // OnAdShowSuccess();
           // OnAdShowFail("");
        }

#if UNITY_IPHONE
		if (debugMode)
        {
            Debug.Log("测试模式");
            _initSDK("i100010000", "i100010001", (int)appOritation);
        }
        else
        {
            _initSDK( appKey, channelID, (int)appOritation);
        }
#elif UNITY_ANDROID
        Debug.Log("初始化");

        if (debugMode)
        {
            Debug.Log("测试模式");
            _plugin.Call("init", "a100010000", "a100010000", (int)appOritation);
        }
        else
        {
            _plugin.Call("init", appKey, channelID,(int)appOritation);
        }
#endif

        PreloadVideo();
    }

    /// <summary>
    /// 检查视频广告是否已缓冲好
    /// </summary>
    /// <returns></returns>
    [DllImport("__Internal")]
    internal static extern bool _isADLoaded();
    public static bool isVideoReady()
    {
        Debug.Log("检查视频广告是否准备好");

#if UNITY_IPHONE
        return _isADLoaded();
#elif UNITY_ANDROID
        return _plugin.Call<bool>("IsPlayable");
#endif
    }

    /// <summary>
    /// 检查全屏幕广告是否已缓冲好
    /// </summary>
    /// <returns></returns>
    public static bool isFullScreenReady()
    {
        Debug.Log("检查全屏广告是否准备好");

#if UNITY_IPHONE
        //_OneWaySDKInit( iOSPId, debugMode);
        return true;
#elif UNITY_ANDROID
        return _plugin.Call<bool>("IsPlayableQP");
#endif
    }

    /// <summary>
    /// 播放视频广告
    /// </summary>
    [DllImport("__Internal")]
    internal static extern void _showVideo(string callbackName);
    public static void ShowVideo()
    {
        Debug.Log("开始播放视频广告");

#if UNITY_IPHONE
        _showVideo("OnVideoCallBack");
#elif UNITY_ANDROID
        Debug.Log("安卓");
        _plugin.Call("PlayVideo", "OnVideoCallBack"); //第二个参数是回调函数的函数名
#endif
    }

    /// <summary>
    /// 展示全屏广告
    /// </summary>
    [DllImport("__Internal")]
    internal static extern void _showFullScreen();
    public static void ShowFullScreen()
    {
        Debug.Log("开始播放全屏广告");
#if UNITY_IPHONE
        _showFullScreen();
#elif UNITY_ANDROID
        _plugin.Call("ShowQP");
#endif
    }

    public static void onResume()
    {
        _plugin.Call("onResume");
    }

    /// <summary>
    /// 预加载视频广告
    /// </summary>
    [DllImport("__Internal")]
    internal static extern void _preLoadAds();
    public static void PreloadVideo()
    {
#if UNITY_IPHONE
        _preLoadAds();
#elif UNITY_ANDROID
        _plugin.Call("PreloadVideos");
#endif
    }

    /// <summary>
    /// 预加载全屏广告
    /// </summary>
    public static void PreloadFullscreen()
    {
#if UNITY_IPHONE
#elif UNITY_ANDROID
        _plugin.Call("PreloadQP");
#endif
    }

    /// <summary>
    /// 视频广告播放回调函数
    /// </summary>
    /// <param name="result">返回信息描述</param>
    void OnVideoCallBack(string result)
    {
        Debug.Log("广告播放结束，返回信息:" + result);

#if UNITY_IPHONE
        if (result == "0")
        {
        }
        else if(result == "1")
        {
            if (OnAdShowSuccess != null)
                OnAdShowSuccess();
        }
        else
        {
            if (OnAdShowFail != null)
                OnAdShowFail(result);
        }
#elif UNITY_ANDROID
        if (result == "")
        {
            if (OnAdShowSuccess != null)
                OnAdShowSuccess();
        }
        else
        {
            if (OnAdShowFail != null)
                OnAdShowFail(result);
        }
#endif
    }
}
