using UnityEngine;
using System.Collections;

public class DebugForward : MonoBehaviour {
	// Update is called once per frame
	void Update () {
        Debug.Log(gameObject.GetComponent<Transform>().forward);
	}
}
