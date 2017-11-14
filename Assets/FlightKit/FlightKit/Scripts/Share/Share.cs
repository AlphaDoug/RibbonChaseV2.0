using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using cn.sharesdk.unity3d;
using UnityEngine.UI;
using System.IO;

public class Share : MonoBehaviour {
	public int shareAddLifeNum=3;
	public GameObject warningText;
	public GameObject UICanvas;
	public GUISkin demoSkin;
	public ShareSDK ssdk;
	private GameObject gameController;

	// Use this for initialization
	void Start ()
	{	
		ssdk = gameObject.GetComponent<ShareSDK>();
		ssdk.shareHandler = OnShareResultHandler;
		gameController = GameObject.Find ("GameController");
	}

    String GetImagePath()
    {     
        string filepath = "jar:file://" + Application.dataPath + "!/assets/Icon.png" ;
        return filepath;
    }

    public void OnShareButtonDown()
	{
        Application.CaptureScreenshot("Icon.png");
        ShareContent content = new ShareContent();

		content.SetText("随风疾行，触摸天空，飞跃高山大海，身后已是星辰——触摸天空下载链接https://www.taptap.com/app/64704");
        //content.SetImageUrl("http://ww3.sinaimg.cn/mw690/be159dedgw1evgxdt9h3fj218g0xctod.jpg");
        content.SetImagePath(Application.persistentDataPath + "/" + "Icon.png");
		content.SetImageUrl ("https://www.taptap.com/app/64704");
        content.SetTitle("触摸天空");
		content.SetTitleUrl("https://www.taptap.com/app/64704");
		content.SetSite("触摸天空");
		content.SetSiteUrl("https://www.taptap.com/app/64704");
		content.SetUrl("https://www.taptap.com/app/64704");
        //content.SetComment("test description"); 
        //content.SetMusicUrl("http://mp3.mwap8.com/destdir/Music/2009/20090601/ZuiXuanMinZuFeng20090601119.mp3");
        content.SetShareType(ContentType.Image);
        
        ssdk.ShowPlatformList(null, content, 100, 100);
    }

	void OnShareResultHandler (int reqID, ResponseState state, PlatformType type, Hashtable result)
	{
		

        if (state == ResponseState.Success)
		{
			if(gameController!=null)
			{
				gameController.GetComponent<LifeNumCtrl> ().AdsAddLife (shareAddLifeNum);
			}
			else
			{
				Debug.LogError ("找不到GameController");
			}

            print ("share successfully - share result :");
			print (MiniJSON.jsonEncode(result));
			if(LifeNumCtrl.lifeNum<LifeNumCtrl.MAXLIFENUM)
			{
				GameObject warnText = Instantiate (warningText)as GameObject;
				warnText.transform.SetParent (UICanvas.transform);
				warnText.GetComponent<RectTransform>().localPosition = new Vector3 (-23, 864.9999f, 0);
				warnText.GetComponent<RectTransform>().localScale = new Vector3 (1, 1, 1);
				warnText.GetComponent<RectTransform>().localRotation = new Quaternion (0, 0, 0,0);
				warnText.GetComponent<Animator> ().updateMode = AnimatorUpdateMode.UnscaledTime;
			}

		}
		else if (state == ResponseState.Fail)
		{
#if UNITY_ANDROID
            print ("fail! throwable stack = " + result["stack"] + "; error msg = " + result["msg"]);
			#elif UNITY_IPHONE
			print ("fail! error code = " + result["error_code"] + "; error msg = " + result["error_msg"]);
			#endif
		}
		else if (state == ResponseState.Cancel) 
		{
      
           print ("cancel !");
		}
	}
  
   
}
