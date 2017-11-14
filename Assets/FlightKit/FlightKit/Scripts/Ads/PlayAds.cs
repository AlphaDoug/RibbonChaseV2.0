using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayAds : MonoBehaviour {
    public GameObject gameController;

	public Text text1;
	public Text text2;
	public Text text3;
    //public bool isa;

    private int t = 0;
	// Use this for initialization
	void Start () {
        // TGSDK.AdCloseCallback = OnAdClose;
        // TGSDK.VideoAdLoadedCallback = OnVideoAdLoaded;
        //ADFlyHiSDK.OnAdShowSuccess = OnAdShowSuccess;
        //ADFlyHiSDK.OnAdShowFail = OnAdShowFail;

        //ADFlyHiSDK.init("a100170000","a100170001", ADFlyHiSDK.Oritation.Landscape, true);
        //ADFlyHiSDK.PreloadVideo ();
        
    }

    void Update()
    {
        t++;
        if (t > 30)
        {
            t = 0;
            if (ADFlyHiSDK.isVideoReady())
            {
                GetComponent<Image>().color = new Color(1, 1, 1);
                //    if (PlayerPrefs.HasKey("language"))
                //    {
                if (PlayerPrefs.GetInt("language") == 0)
                {
                    //中文
                    text1.text = "填充完毕";
                }
                else
                {
                    //英文
                    text1.text = "Ready";
                }
                //    }             

            }
            else
            {
                GetComponent<Image>().color = new Color(0.6f, 0.6f, 0.6f);
                //  if (PlayerPrefs.HasKey("language"))
                //  {
                if (PlayerPrefs.GetInt("language") == 0)
                {
                    //中文
                    text1.text = "正在填充...";
                }
                else
                {
                    //英文
                    text1.text = "loading...";
                }
                //  }

            }
        }
    }


    public void Ads()
    {
        if (ADFlyHiSDK.isVideoReady())
        {
           // text1.text = "播放成功";
            ADFlyHiSDK.ShowVideo();
           
        }
        else
        {
                if (PlayerPrefs.GetInt("language") == 0)
                {
                    //中文
                    text1.text = "正在填充...";
                }
                else
                {
                    //英文
                    text1.text = "loading...";
                }
            
        }

    }
    /*public void OnAdClose(string ret)
    {
		gameController.GetComponent<LifeNumCtrl> ().AdsAddLife (5);
		GameObject warning = Instantiate (warningText)as GameObject;
		warning.transform.SetParent (UICanvas.transform);
		warning.GetComponent<RectTransform>().localPosition = new Vector3 (-23, 864.9999f, 0);
		warning.GetComponent<RectTransform>().localScale = new Vector3 (1, 1, 1);
		warning.GetComponent<RectTransform>().localRotation = new Quaternion (0, 0, 0,0);

    }
*/
    public void OnVideoAdLoaded(string ret)
    {

    }

}
