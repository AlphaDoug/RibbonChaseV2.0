using UnityEngine;
using System.Collections;

public class FingerEvent : MonoBehaviour
{
    private bool isRotating = false;
    public float rotateSpeed = 1;
    public float toFirmAngleSpeed = 6;
    public Transform levels;
    // Use this for initialization
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        RotateView();
        //Rotate to firm angle
        if (!isRotating)
        {
            int i = (int)(levels.localRotation.eulerAngles.y / 90);
            float j = (levels.localRotation.eulerAngles.y / 90);
            int temp;
            temp = (j > i + 0.5f) ? 1 : 0;
            int targetAngle = ((int)(levels.localRotation.eulerAngles.y / 90) + temp) * 90;
            //Debug.Log("y" + levels.localRotation.eulerAngles.y + "  targetAngle" + targetAngle);
            Vector3 target = new Vector3(transform.localRotation.eulerAngles.x, targetAngle, transform.localRotation.eulerAngles.z);
            transform.rotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(target),Time.deltaTime*rotateSpeed* toFirmAngleSpeed);
        }
    }
    void RotateView()
    {
        //Input .GetAxis ("Mouse X"); 得到鼠标在水平方向的滑动
        //Input .GetAxis ("Mouse Y");得到鼠标在垂直方向的滑动
        if (Input.GetMouseButtonDown(0))
        {
            //0代表左键1代表右键2代表中键
            isRotating = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            isRotating = false;
            // transform.RotateAround(transform.position, Vector3.up, rotateSpeed * Input.GetAxis("Mouse X"));
            
        }
        if (isRotating)
        {
            //transform.RotateAround(transform.position, Vector3.up, rotateSpeed * Input.GetAxis("Mouse X"));
//#if UNITY_ANDROID
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                transform.RotateAround(transform.position, Vector3.up, rotateSpeed * -Input.GetAxis("Mouse X"));
               
            }
//#endif
#if UNITY_STANDALONE_WIN
             transform.RotateAround(transform.position, Vector3.up, rotateSpeed * -Input.GetAxis("Mouse X"));
#endif
        }
    }
}