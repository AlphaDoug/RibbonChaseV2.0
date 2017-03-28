using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculatePosition : MonoBehaviour
{
    public Transform image;
    public Transform targetTransform;
    public GameObject pickUps;
    public float edgeDistance = 20.0f;
    public float angle;
    public Vector3 targetCamarePo;

    private Vector3 selfPosition;
    private Vector3 targetPosition;
    private Vector3 selfForwardNormalized;
    private Vector3 self2targetNormalized;
    private Vector3 footOfPerpendicular;
    private float screenWidth;
    private float screenHeight;
    private int indexTarget = 0;
    // Use this for initialization
    void Start()
    {
        selfPosition = transform.position;
        targetPosition = targetTransform.position;
        selfForwardNormalized = transform.forward.normalized;
        self2targetNormalized = (selfPosition - targetPosition).normalized;
        screenWidth = Screen.width;
        screenHeight = Screen.height;
    }

    // Update is called once per frame
    void Update()
    {
        selfPosition = transform.position;
        targetPosition = targetTransform.position;
        selfForwardNormalized = transform.forward.normalized;
        self2targetNormalized = (targetPosition - selfPosition).normalized;
        var k = Vector3.Dot((selfPosition - targetPosition), selfForwardNormalized);
        footOfPerpendicular = k * selfForwardNormalized + targetPosition;
        angle = Vector3.Angle(transform.up, footOfPerpendicular - selfPosition);
        targetCamarePo = Camera.main.WorldToViewportPoint(targetPosition);
        if (targetCamarePo.z > 0)//摄像机平面前方
        {
            if (((targetCamarePo.x < 1 || targetCamarePo.x > 0) && (targetCamarePo.y > 0 || targetCamarePo.y < 1)))//表示在屏幕可视范围内
            {
                //image.position = new Vector3(targetCamarePo.x * screenWidth, targetCamarePo.y * screenHeight, 0);
                image.gameObject.GetComponent<CanvasRenderer>().SetAlpha(0);
            }            
            if (targetCamarePo.x <= 0 || targetCamarePo.y > 1 && targetCamarePo.x > 0 && targetCamarePo.x < 0.5f || targetCamarePo.y < 0 && targetCamarePo.x > 0 && targetCamarePo.x < 0.5f)//在屏幕左半边
            {
                image.gameObject.GetComponent<CanvasRenderer>().SetAlpha(1);
                image.transform.eulerAngles = new Vector3(0, 0, angle - 360.0f);
                if ((screenHeight / 2) - Mathf.Tan(Mathf.Deg2Rad * (angle - 90)) * screenWidth / 2 > 0 && (screenHeight / 2) - Mathf.Tan(Mathf.Deg2Rad * (angle - 90)) * screenWidth / 2 < screenHeight)
                {
                    image.position = new Vector3(edgeDistance, (screenHeight / 2) - Mathf.Tan(Mathf.Deg2Rad * (angle - 90)) * screenWidth / 2);
                }
                if (screenHeight / 2 - Mathf.Tan(Mathf.Deg2Rad * (angle - 90)) * screenWidth / 2 < 0)
                {
                    image.position = new Vector3((Mathf.Tan(Mathf.Deg2Rad * (angle - 90)) * (screenWidth / 2) - (screenHeight / 2)) / Mathf.Tan(Mathf.Deg2Rad * (angle - 90)), edgeDistance);
                }
                if (screenHeight / 2 - Mathf.Tan(Mathf.Deg2Rad * (angle - 90)) * screenWidth / 2 > screenHeight)
                {
                    image.position = new Vector3((Mathf.Tan(Mathf.Deg2Rad * (angle - 90)) * screenWidth / 2 + screenHeight / 2) / Mathf.Tan(Mathf.Deg2Rad * (angle - 90)), screenHeight - edgeDistance);
                }
            }
            if (targetCamarePo.x >= 1 || targetCamarePo.y > 1 && targetCamarePo.x < 1 && targetCamarePo.x > 0.5f || targetCamarePo.y < 0 && targetCamarePo.x > 0.5f && targetCamarePo.x < 1)//在屏幕右半边
            {
                //image.gameObject.GetComponent<CanvasRenderer>().SetAlpha(1);
                image.transform.eulerAngles = new Vector3(0, 0, -angle);
                if (screenHeight / 2 - Mathf.Tan(Mathf.Deg2Rad * (angle - 90)) * screenWidth / 2 > 0 && screenHeight / 2 - Mathf.Tan(Mathf.Deg2Rad * (angle - 90)) * screenWidth / 2 < screenHeight)
                {
                    image.position = new Vector3(screenWidth - edgeDistance, screenHeight / 2 - Mathf.Tan(Mathf.Deg2Rad * (angle - 90)) * screenWidth / 2);
                }
                if (screenHeight / 2 - Mathf.Tan(Mathf.Deg2Rad * (angle - 90)) * screenWidth / 2 < 0)
                {
                    image.position = new Vector3(screenWidth - (Mathf.Tan(Mathf.Deg2Rad * (angle - 90)) * screenWidth / 2 - screenHeight / 2) / Mathf.Tan(Mathf.Deg2Rad * (angle - 90)), edgeDistance);
                }
                if (screenHeight / 2 - Mathf.Tan(Mathf.Deg2Rad * (angle - 90)) * screenWidth / 2 > screenHeight)
                {
                    image.position = new Vector3(screenWidth - (Mathf.Tan(Mathf.Deg2Rad * (angle - 90)) * screenWidth / 2 + screenHeight / 2) / Mathf.Tan(Mathf.Deg2Rad * (angle - 90)), screenHeight - edgeDistance);
                }
            }
        }
        else//摄像机平面后方
        {
            image.gameObject.GetComponent<CanvasRenderer>().SetAlpha(1);
            if (targetCamarePo.x < 0.5f)//右边
            {
                image.transform.eulerAngles = new Vector3(0, 0, -angle);
                if (Mathf.Tan(Mathf.Deg2Rad * (angle - 90)) < screenHeight / screenWidth && Mathf.Tan(Mathf.Deg2Rad * (angle - 90)) > -screenHeight / screenWidth)
                {
                    image.position = new Vector3(screenWidth - edgeDistance, (screenHeight / 2) - Mathf.Tan(Mathf.Deg2Rad * (angle - 90)) * screenWidth / 2);
                }
                if (Mathf.Tan(Mathf.Deg2Rad * (angle - 90)) > screenHeight / screenWidth)
                {
                    image.position = new Vector3(screenWidth - (Mathf.Tan(Mathf.Deg2Rad * (angle - 90)) * (screenWidth / 2) - (screenHeight / 2)) / Mathf.Tan(Mathf.Deg2Rad * (angle - 90)), edgeDistance);
                }
                if (Mathf.Tan(Mathf.Deg2Rad * (angle - 90)) < -screenHeight / screenWidth)
                {
                    image.position = new Vector3(screenWidth - (Mathf.Tan(Mathf.Deg2Rad * (angle - 90)) * screenWidth / 2 + screenHeight / 2) / Mathf.Tan(Mathf.Deg2Rad * (angle - 90)), screenHeight - edgeDistance);
                }
            }
            else//左边
            {
                image.transform.eulerAngles = new Vector3(0, 0, angle - 360.0f);
                if (Mathf.Tan(Mathf.Deg2Rad * (angle - 90)) < screenHeight / screenWidth && Mathf.Tan(Mathf.Deg2Rad * (angle - 90)) > -screenHeight / screenWidth)
                {
                    image.position = new Vector3(edgeDistance, (screenHeight / 2) - Mathf.Tan(Mathf.Deg2Rad * (angle - 90)) * screenWidth / 2);
                }
                if (Mathf.Tan(Mathf.Deg2Rad * (angle - 90)) > screenHeight / screenWidth)
                {
                    image.position = new Vector3((Mathf.Tan(Mathf.Deg2Rad * (angle - 90)) * (screenWidth / 2) - (screenHeight / 2)) / Mathf.Tan(Mathf.Deg2Rad * (angle - 90)), edgeDistance);
                }
                if (Mathf.Tan(Mathf.Deg2Rad * (angle - 90)) < -screenHeight / screenWidth)
                {
                    image.position = new Vector3((Mathf.Tan(Mathf.Deg2Rad * (angle - 90)) * screenWidth / 2 + screenHeight / 2) / Mathf.Tan(Mathf.Deg2Rad * (angle - 90)), screenHeight - edgeDistance);
                }
            }
        }
    }
    public void SetNextTarget()
    {
        targetTransform = targetTransform.gameObject.GetComponent<ClosestStar>().closestPickUp.transform;
    }
}
