using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AboutGameMove : MonoBehaviour
{
    public GameObject content;

    private bool isMouseDown = false;
    private Vector3 mouseDownPosition;
    private Vector3 mouseDownContentPosition;
    
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isMouseDown = true;
            mouseDownPosition = Input.mousePosition;
            mouseDownContentPosition = content.transform.localPosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            isMouseDown = false;
        }

        if (isMouseDown)//鼠标OR手指按下,停止自动滑动,跟随手指移动
        {
            if (content.transform.localPosition.y < 900 && content.transform.localPosition.y > -1500)
            {
                content.transform.localPosition = new Vector3(0, Input.mousePosition.y - mouseDownPosition.y + mouseDownContentPosition.y, 0);
            }
        }
        else//自动滑动
        {
            if (content.transform.localPosition.y < 900)
            {
                content.transform.Translate(new Vector3(0, 0.12f, 0));
            }
        }
        
	}
    private void OnDisable()
    {
        content.transform.localPosition = new Vector3(0, -1300, 0);
    }

}
