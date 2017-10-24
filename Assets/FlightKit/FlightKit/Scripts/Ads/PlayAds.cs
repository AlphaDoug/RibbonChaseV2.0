using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Together;
public class PlayAds : MonoBehaviour {
    public GameObject gameController;
	public GameObject warningText;
	public GameObject UICanvas;
	// Use this for initialization
	void Start () {
        TGSDK.AdCloseCallback = OnAdClose;
        TGSDK.VideoAdLoadedCallback = OnVideoAdLoaded;
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
		GameObject warning = Instantiate (warningText)as GameObject;
		warning.transform.SetParent (UICanvas.transform);
		warning.GetComponent<RectTransform>().localPosition = new Vector3 (-23, 864.9999f, 0);
		warning.GetComponent<RectTransform>().localScale = new Vector3 (1, 1, 1);
		warning.GetComponent<RectTransform>().localRotation = new Quaternion (0, 0, 0,0);

    }

    public void OnVideoAdLoaded(string ret)
    {

    }
}
