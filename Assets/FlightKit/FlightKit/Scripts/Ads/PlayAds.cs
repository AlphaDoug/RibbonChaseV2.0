using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Together;
public class PlayAds : MonoBehaviour {
    public GameObject gameController;
    public GameObject addAnim;
    private AnimatorStateInfo animatorInfo;

    // Use this for initialization
    void Start () {
        TGSDK.AdCloseCallback = OnAdClose;
        TGSDK.VideoAdLoadedCallback = OnVideoAdLoaded;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (addAnim.activeSelf)
        {
            addAnim.GetComponent<Animator>().updateMode = AnimatorUpdateMode.UnscaledTime;
            animatorInfo = addAnim.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
            if ((animatorInfo.normalizedTime > 1.0f) && (animatorInfo.IsName("AddAnim")))//normalizedTime: 范围0 -- 1,  0是动作开始，1是动作结束  
            {
                addAnim.SetActive(false);
                gameController.GetComponent<LifeNumCtrl>().AdsAddLife(5);
            }
        }
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
        
        addAnim.SetActive(true);	
    }

    public void OnVideoAdLoaded(string ret)
    {

    }
}
