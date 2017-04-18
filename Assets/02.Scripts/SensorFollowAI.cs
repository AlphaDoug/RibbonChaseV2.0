using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorFollowAI : MonoBehaviour {
    public GameObject AiAirplane;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = AiAirplane.GetComponent<Transform>().position;
	}
}
