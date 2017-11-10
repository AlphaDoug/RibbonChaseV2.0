using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Example : MonoBehaviour {
	public Text text1;
	public Text text2;
	// Use this for initialization
	void Start () {
        ADFlyHiSDK.OnAdShowSuccess = OnAdShowSuccess;
        ADFlyHiSDK.OnAdShowFail = OnAdShowFail;

		ADFlyHiSDK.init("a100170000", "a100170001", ADFlyHiSDK.Oritation.Landscape, true);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnAdShowSuccess()
    {
        Debug.Log("广告播放成功");
		text1.text="广告播放成功";
    }

    private void OnAdShowFail(string s)
    {
        Debug.Log("广告播放失败，失败描述：" + s);
		text2.text="广告播放失败，失败描述：" + s;
    }
}
