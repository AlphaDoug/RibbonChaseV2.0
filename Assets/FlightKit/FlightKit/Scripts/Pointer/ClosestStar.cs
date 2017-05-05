using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ClosestStar : MonoBehaviour
{
    public  GameObject[] pickup;
    private GameObject airPlane;
    public  float minDistance;

    public List<GameObject> pickups = new List<GameObject>();
    public GameObject closestPickUp;
    //设置超大minDistance初始值
    public float bigNum=10000000;
    public int pickupTotalNum = 0;
    
	// Use this for initialization
	void Start () {
        //获取所有Pickup
        pickup = GameObject.FindGameObjectsWithTag("PickUp");
        pickupTotalNum = pickup.Length;
        airPlane = GameObject.Find("Airplane");
        if (airPlane == null)
        {
            throw new Exception("没有找到飞机");
        }
        minDistance = bigNum;
        for (int i = 0; i < pickup.Length; i++)
        {
            if (this.gameObject != pickup[i])
            {
                pickups.Add(pickup[i]);
            }      
        }
        UpdateClosestStar();
	}

    public void UpdateClosestStar()
    {
     
        if (airPlane.GetComponent<Pointer>().pickupCollectedNum < pickupTotalNum)
        {
            //获取离本星最近的星并放在closestPickUp中
            for (int i = 0; i < pickups.Count; i++)
            {
                if (Vector3.Distance(this.gameObject.transform.position, pickups[i].transform.position) < minDistance)
                {
                    minDistance = Vector3.Distance(this.gameObject.transform.position, pickups[i].transform.position);
                    if (pickups[i] != null)
                    {
                        closestPickUp = pickups[i];
                    }
                    else
                    {
                        Debug.LogError("No Dest for pointer");
                    }
                    
                }

            }
        }
    
    } 
}
