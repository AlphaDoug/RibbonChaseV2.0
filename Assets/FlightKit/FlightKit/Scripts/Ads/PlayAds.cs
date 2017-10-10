using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Together;
public class PlayAds : MonoBehaviour {
    public GameObject gameController;
	// Use this for initialization
	void Start () {
        TGSDK.AdCloseCallback = OnAdClose;
        TGSDK.VideoAdLoadedCallback = OnVideoAdLoaded;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Ads()
    {
        if (TGSDK.CouldShowAd("FXFLPDOjRrCQSzlEl34"))
        {
            TGSDK.ShowAd("FXFLPDOjRrCQSzlEl34");
        }
    }
    public void OnAdClose(string ret)
    {
		gameController.GetComponent<LifeNumCtrl> ().AdsAddLife (5);
    }

    public void OnVideoAdLoaded(string ret)
    {

    }
}
