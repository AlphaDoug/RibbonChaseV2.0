using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardCanvas : MonoBehaviour {
    private Transform tr;
    private Transform mainCameraTr;
	// Use this for initialization
	void Start () {
        tr = GetComponent<Transform>();
        //获取场景中主摄像机的Transform组件
        mainCameraTr = Camera.main.transform;
	}
	
	// Update is called once per frame
	void Update () {
        tr.LookAt(mainCameraTr);
	}
}
