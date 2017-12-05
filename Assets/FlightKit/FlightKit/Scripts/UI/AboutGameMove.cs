using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 控制制作人员界面的运动
/// </summary>
public class AboutGameMove : MonoBehaviour
{
    public GameObject contentCN;
	public GameObject contentEN;

    private bool isMouseDown = false;
    private Vector3 mouseDownPosition;
    private Vector3 mouseDownContentPosition;
    
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void LateUpdate ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isMouseDown = true;
            mouseDownPosition = Input.mousePosition;
			if (contentCN.activeSelf) 
			{
				mouseDownContentPosition = contentCN.transform.localPosition;
			} 
			else
			{
				mouseDownContentPosition = contentEN.transform.localPosition;
			}

        }
        if (Input.GetMouseButtonUp(0))
        {
            isMouseDown = false;
        }

        if (isMouseDown)//鼠标OR手指按下,停止自动滑动,跟随手指移动
        {
			if (contentCN.transform.localPosition.y < 900 && contentCN.transform.localPosition.y > -1500)
            {
				contentCN.transform.localPosition = new Vector3(0, Input.mousePosition.y - mouseDownPosition.y + mouseDownContentPosition.y, 0);
            }
			if (contentEN.transform.localPosition.y < 900 && contentEN.transform.localPosition.y > -1500)
			{
				contentEN.transform.localPosition = new Vector3(0, Input.mousePosition.y - mouseDownPosition.y + mouseDownContentPosition.y, 0);
			}
        }
        else//自动滑动
        {
			if (contentCN.transform.localPosition.y < 900)
            {
				//!!!!!!!!!!!!!!!!!!!!!!增关卡必改!!!!!!!!!!!!!!
				if (Application.loadedLevel == 7)
				{
                    //最后一关的剧情移动
					contentCN.transform.Translate (new Vector3 (0, 1f, 0));
				} 
				else
				{
                    //首界面的剧情移动
					contentCN.transform.Translate(new Vector3(0, 0.12f, 0));
				}
                
            }
			if (contentEN.transform.localPosition.y < 900)
			{
				//!!!!!!!!!!!!!!!!!11增关卡必改!!!!!!!!!!!!!!!!!!!
				if (Application.loadedLevel == 7)
				{
                    //最后一关的剧情移动
                    contentEN.transform.Translate (new Vector3 (0, 1f, 0));
				} 
				else
				{
                    //首界面的剧情移动
                    contentEN.transform.Translate(new Vector3(0, 0.12f, 0));
				}

			}


        }
        
	}
    /// <summary>
    /// 界面不可用时重置位置
    /// </summary>
    private void OnDisable()
    {
		contentCN.transform.localPosition = new Vector3(0, -1300, 0);
		contentEN.transform.localPosition = new Vector3(0, -1300, 0);
        isMouseDown = false;
    }

}
