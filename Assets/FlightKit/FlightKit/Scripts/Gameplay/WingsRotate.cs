using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingsRotate : MonoBehaviour {
    public GameObject center;
	// Use this for initialization
	void Start () {
		
	}

    void FixedUpdate()
    {
        this.gameObject.transform.RotateAround(center.transform.position, Vector3.forward, 0.3f);
    }
}
