using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragRotate : MonoBehaviour {

    public GameObject target;
    public GameObject flag;
    private float rotateSpeed=2f;
    private float fadeSpeed = 0.05f;
    private float lastYRoll=0;
    private float lastXRoll = 0;
	// Update is called once per frame
	void Update () {
        if (Input.GetTouch(0).phase == TouchPhase.Moved)//Input.GetMouseButton(0)
        {
            //rotateSpeed = 4f;
            target.gameObject.transform.RotateAroundLocal(flag.gameObject.transform.right, Input.GetAxis("Mouse Y") * Time.deltaTime * rotateSpeed);
            target.gameObject.transform.RotateAroundLocal(flag.gameObject.transform.up,-Input.GetAxis("Mouse X") * Time.deltaTime * rotateSpeed);
            lastYRoll = Input.GetAxis("Mouse Y");
            lastXRoll = -Input.GetAxis("Mouse X");
        }
        //else
        //{
   
        //    if (rotateSpeed - fadeSpeed > 0.1f)
        //    {            
        //        target.gameObject.transform.RotateAroundLocal(flag.gameObject.transform.right, lastYRoll * Time.deltaTime * (rotateSpeed -= fadeSpeed));
        //        target.gameObject.transform.RotateAroundLocal(flag.gameObject.transform.up, lastXRoll * Time.deltaTime * (rotateSpeed - fadeSpeed));
        //    }
            
           
        //}
        
	}
}
