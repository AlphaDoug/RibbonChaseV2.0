﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotate : MonoBehaviour {
    private Vector3[]randomVector;
    private Vector3 vector;
	// Use this for initialization
	void Start () {
        randomVector = new Vector3[4];
        randomVector[0] = Vector3.forward;
        randomVector[1] = Vector3.left;
        randomVector[2] = Vector3.right;
        randomVector[3] = Vector3.back;
        vector = randomVector[(int)Random.RandomRange(0, 3)];

    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void FixedUpdate()
    {
        this.gameObject.transform.RotateAround(this.transform.position, vector, 0.3f);
    }
}
