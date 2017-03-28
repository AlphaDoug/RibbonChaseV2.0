using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpFingers : MonoBehaviour {
    public GameObject finger1;
    public GameObject finger2;
    private int count;
    // Use this for initialization
    void Start ()
    {
        count = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
        count++; 
        if (count % 16 <= 8)
        {
            finger1.SetActive(true);
            finger2.SetActive(false);
        }
        else
        {
            finger1.SetActive(false);
            finger2.SetActive(true);
        }
	}
}
