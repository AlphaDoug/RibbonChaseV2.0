using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtRotation : MonoBehaviour {
    private Transform airplane;
	// Use this for initialization
	void Start () {
        airplane = GameObject.Find("Airplane").GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<Transform>().LookAt(airplane);
	}
}
