using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticAiming : MonoBehaviour
{
    public GameObject post;
    public Transform centerPoint;
    public List<GameObject> pickUp = new List<GameObject>();

    private List<Vector3> pickUpOnScrennVector3 = new List<Vector3>();
    private List<float> distance = new List<float>();
    private CanvasRenderer postCanvasRenderer;
    private bool isOnTarget = false;
    private bool isBigger = true;
	// Use this for initialization
	void Start ()
    {
        postCanvasRenderer = post.GetComponent<CanvasRenderer>();
        for (int i = 0; i < pickUp.Count; i++)
        {
            pickUpOnScrennVector3.Add(Camera.main.WorldToScreenPoint(pickUp[i].transform.position));
            distance.Add(Vector2.Distance(pickUpOnScrennVector3[i], transform.position));
        }     
	}
	
	// Update is called once per frame
	void Update ()
    {
        #region 控制准星的旋转和缩放
        if (isOnTarget)
        {
            post.transform.Rotate(new Vector3(0, 0, 5));
            if (isBigger)
            {
                post.transform.localScale += new Vector3(0.02f, 0.02f, 0);
                if (post.transform.localScale.x > 1.5f && post.transform.localScale.y > 1.5f)
                {
                    isBigger = false;
                }
            }
            else
            {
                post.transform.localScale -= new Vector3(0.02f, 0.02f, 0);
                if (post.transform.localScale.x < 1.0f && post.transform.localScale.y < 1.0f)
                {
                    isBigger = true;
                }
            }
            
        }
        else
        {
            post.transform.localRotation = new Quaternion(0, 0, 0, 0);
            post.transform.localScale = new Vector3(1, 1, 1);
        }
        #endregion


        distance.Clear();
        pickUpOnScrennVector3.Clear();
        for (int i = 0; i < pickUp.Count; i++)
        {
            pickUpOnScrennVector3.Add(Camera.main.WorldToScreenPoint(pickUp[i].transform.position));
            distance.Add(Vector2.Distance(pickUpOnScrennVector3[i], transform.position));
        }
        distance.Sort();
        if (distance.Count > 0 && distance[0] < 400)//存在收集品并且离光点距离小于300
        {
            for (int i = 0; i < pickUp.Count; i++)
            {
                if (distance[0] == Vector2.Distance(pickUpOnScrennVector3[i], transform.position) && pickUpOnScrennVector3[i].z > 300 && pickUpOnScrennVector3[i].z < 1500)
                {
                    //准星移动到光球上
                    //post.SetActive(true);
                    postCanvasRenderer.SetAlpha(1);
                    isOnTarget = true;
                    post.transform.position = new Vector3(pickUpOnScrennVector3[i].x, pickUpOnScrennVector3[i].y);
                    break;
                }
                //在指定范围内不存在光球，则准星移到中心
                //post.SetActive(false);
                isOnTarget = false;
                post.transform.position = centerPoint.transform.position;
                postCanvasRenderer.SetAlpha(0.3f);
            }
        }
        //不存在光球或者离中心光点距离大于300，则准星移动中心
        else
        {
            // post.SetActive(false);
            isOnTarget = false;
            post.transform.position = centerPoint.transform.position;
            postCanvasRenderer.SetAlpha(0.3f);
        }
    }
}
