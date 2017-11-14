using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour {

    public enum BTN_STYLE
    {
        ShowVideo = 1,
        ShowFullScreen = 2
    }

    public BTN_STYLE style;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnCLick()
    {
        bool ready = false;
        switch (style)
        {
            case BTN_STYLE.ShowVideo:
                ready = ADFlyHiSDK.isVideoReady();
                Debug.Log(ready);
                if(ready)
                {
                    ADFlyHiSDK.ShowVideo();
                }
                break;
            case BTN_STYLE.ShowFullScreen:
                ready = ADFlyHiSDK.isFullScreenReady();
                Debug.Log(ready);
                if (ready)
                {
                    ADFlyHiSDK.ShowFullScreen();
                }
                break;
        }
    }
}
