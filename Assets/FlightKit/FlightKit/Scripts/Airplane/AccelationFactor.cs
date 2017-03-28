using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelationFactor : MonoBehaviour {
    private Gyroscope gyro;
	// Use this for initialization
	void Start () {
        gyro = Input.gyro;
	}
	
	// Update is called once per frame
	void OnGUI () {
        GUI.Box(new Rect(5, 5, 100, 20), String.Format("{0:0.000}", Input.acceleration.x));
        GUI.Box(new Rect(5, 30, 100, 20), String.Format("{0:0.000}", Input.acceleration.y));
        GUI.Box(new Rect(5, 55, 100, 20), String.Format("{0:0.000}", Input.acceleration.z));
     
        GUI.Box(new Rect(5, 80, 100, 20), String.Format("{0:0.000}",  gyro.attitude.x));
        GUI.Box(new Rect(5, 105, 100, 20), String.Format("{0:0.000}", gyro.attitude.y));
        GUI.Box(new Rect(5, 130, 100, 20), String.Format("{0:0.000}", gyro.attitude.z));
        GUI.Box(new Rect(5, 155, 100, 20), String.Format("{0:0.000}", gyro.attitude.w));
    }
}
