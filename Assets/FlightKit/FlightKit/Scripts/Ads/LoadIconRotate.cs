﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadIconRotate : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(transform.position, transform.forward, -4f);
    }
}
