using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 控制右边擦肩而过文字的显示
/// </summary>
public class BrushPastControl : MonoBehaviour
{
    public enum PlaneState
    {
        near = 0,
        farAway = 1
    }
    public enum MoveDirtection
    {
        none = -1,
        right = 0,
        left = 1
    }
    public GameObject brushPastText;
    private PlaneState planeState = PlaneState.farAway;
    private MoveDirtection moveDirection = MoveDirtection.none;
    private float moveSpeed = 0;
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            moveSpeed = 250;
            moveDirection = MoveDirtection.left;
            Debug.Log(brushPastText.GetComponent<RectTransform>().localPosition.x);
            Invoke("SetBack", 2.0f);
        }
         brushPastText.GetComponent<RectTransform>().Translate(new Vector3(-moveSpeed, 0, 0));

        if (brushPastText.GetComponent<RectTransform>().localPosition.x <= 610 && moveDirection == MoveDirtection.left)//移动过头了
        {
            moveSpeed = -(610 - brushPastText.GetComponent<RectTransform>().localPosition.x) ;
        }
        if(brushPastText.GetComponent<RectTransform>().localPosition.x >= 1400 && moveDirection == MoveDirtection.right)
        {
            moveSpeed = -(1400 - brushPastText.GetComponent<RectTransform>().localPosition.x);
        }
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Obstacle" && planeState == PlaneState.farAway)
        {
            planeState = PlaneState.near;  
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Obstacle" && planeState == PlaneState.near)
        {
            planeState = PlaneState.farAway;
            moveSpeed = 250;
            moveDirection = MoveDirtection.left;
            Invoke("SetBack", 2.0f);
        }
    }
    private void SetBack()
    {
        moveSpeed = -250;
        moveDirection = MoveDirtection.right;
    }
    public void SetPlaneStateFarAway()
    {
        planeState = PlaneState.farAway;
    }
    public void SetPlaneStateNear()
    {
        planeState = PlaneState.near;
    }
    public void SetTriggerUnable()
    {
        GetComponent<MeshCollider>().enabled = false;
    }
    public void SetTriggerable()
    {
        GetComponent<MeshCollider>().enabled = true;
    }
}
