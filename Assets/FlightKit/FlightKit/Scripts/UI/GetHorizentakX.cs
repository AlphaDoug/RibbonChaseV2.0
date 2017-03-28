using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GetHorizentakX : MonoBehaviour {
    private ScrollRect scrollRect;
	// Use this for initialization
	void Start () {
        scrollRect = GetComponent<ScrollRect>();
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(scrollRect.horizontalNormalizedPosition);
	}
}
