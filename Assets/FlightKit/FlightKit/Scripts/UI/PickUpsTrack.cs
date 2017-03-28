using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpsTrack : MonoBehaviour
{
    public GameObject[] pickUps;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Ray ray1 = Camera.main.ScreenPointToRay(pickUps[0].transform.position);
        Ray ray2 = Camera.main.ScreenPointToRay(pickUps[1].transform.position);
        Ray ray3 = Camera.main.ScreenPointToRay(pickUps[2].transform.position);
        Ray ray4 = Camera.main.ScreenPointToRay(pickUps[3].transform.position);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray1, out hitInfo))
        {
            Debug.DrawLine(ray1.origin, hitInfo.point);           
        }
        if (Physics.Raycast(ray2, out hitInfo))
        {
            Debug.DrawLine(ray2.origin, hitInfo.point);
        }
        if (Physics.Raycast(ray3, out hitInfo))
        {
            Debug.DrawLine(ray3.origin, hitInfo.point);
        }
        if (Physics.Raycast(ray4, out hitInfo))
        {
            Debug.DrawLine(ray4.origin, hitInfo.point);
        }
    }
}
