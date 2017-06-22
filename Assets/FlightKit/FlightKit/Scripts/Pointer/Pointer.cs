
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Pointer : MonoBehaviour {
    public GameObject source;
    public GameObject dest;
    public GameObject arrowUI;
    public Vector3 dir;
    private bool enable = false;
    private Transform TrSource;
    private Transform TrDest;
  
    public GameObject[] pickup;
    public List<ClosestStar> closestStar = new List<ClosestStar>();

    public int pickupCollectedNum=0;

    Quaternion q;
	// Use this for initialization
	void Start () {
        TrSource = source.transform;
        TrDest = dest.transform;
        pickup=GameObject.FindGameObjectsWithTag("PickUp");
        for (int i = 0; i < pickup.Length; i++)
        {
            closestStar.Add(pickup[i].GetComponent<ClosestStar>());
        }
	}
	
	// Update is called once per frame
	void Update () {
       
            TrSource = source.transform;
            if (dest != null)
            {
                TrDest = dest.transform;
                if (TrSource)
                    dir = (dest.transform.position - source.transform.position) * 0.009f + source.transform.position;
                if (!enable)
                {
                    arrowUI.SetActive(true);
                    enable = true;
                }
                arrowUI.transform.position = dir;
                q.SetFromToRotation(arrowUI.transform.up, (dest.transform.position - source.transform.position));
                arrowUI.transform.up = Vector3.Lerp(arrowUI.transform.up, (dest.transform.position - source.transform.position), 1f);
            }
            else
            {
                Debug.LogError("飞机没有目标");
            }                
        
	}
    //每次吃球就把光标指向离这个球最近的球,并且将除了这个球的景物全部虚化
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("PickUp"))
        {
            collider.gameObject.GetComponent<SphereCollider>().enabled = false;
            collider.gameObject.GetComponent<ClosestStar>().enabled = false;
            dest = collider.gameObject.GetComponent<ClosestStar>().closestPickUp;
            Camera.main.GetComponent<DepthOfField>().focalTransform = dest.transform;
            pickupCollectedNum++;

            for (int i = 0; i < closestStar.Count; i++)
            {
                closestStar[i].minDistance = closestStar[i].bigNum;
                closestStar[i].pickups.Remove(collider.gameObject);
                closestStar[i].UpdateClosestStar();
            }
            closestStar.Remove(collider.gameObject.GetComponent<ClosestStar>());
            //Camera.main.gameObject.GetComponent<CalculatePosition>().SetNextTarget();
        }
    }


}
