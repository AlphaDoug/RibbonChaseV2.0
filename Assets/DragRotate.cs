using UnityEngine;
using System.Collections;


public class DragRotate : MonoBehaviour
{


    public Camera m_Camera;


    public GameObject m_Cube;


    private bool isDragging = false;


    private float m_lastMouseX = float.MaxValue;


    private float m_lastSpeed = 0f;


    private bool m_isMoving = false;


    // Use this for initialization
    void Start()
    {

    }


    void FixedUpdate()
    {
        if (this.m_isMoving)
        {
            this.m_lastSpeed *= 0.9f;//递减
            Vector3 localEulerAngles = m_Cube.transform.localEulerAngles;
            m_Cube.transform.localEulerAngles = new Vector3(localEulerAngles.x, localEulerAngles.y -= m_lastSpeed * 0.5f, localEulerAngles.z);
            if (Mathf.Abs(this.m_lastSpeed) < 0.01)
            {
                this.m_isMoving = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            if (m_Camera != null)
            {
                RaycastHit hit;
                Ray m_ray = m_Camera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(m_ray, out hit, 1000f, m_Camera.cullingMask) && (hit.collider.gameObject == base.gameObject))
                {
                    //选中物体
                    Debug.Log("选中物体");
                    this.isDragging = true;
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            //抬起
            this.isDragging = false;


            //抬起后缓慢转动 
            this.m_isMoving = true;
            this.m_lastSpeed = Input.mousePosition.x - this.m_lastMouseX;


            this.m_lastMouseX = float.MaxValue;
        }
        if (this.isDragging)
        {
            float num = 0;
            if (this.m_lastMouseX != float.MaxValue)
            {
                num = Input.mousePosition.x - this.m_lastMouseX;
            }
            this.m_lastMouseX = Input.mousePosition.x;


            Vector3 cube_localEulerAngles = m_Cube.transform.localEulerAngles;
            m_Cube.transform.localEulerAngles = new Vector3(cube_localEulerAngles.x, cube_localEulerAngles.y -= num * 0.5f, cube_localEulerAngles.z);


        }
    }
}