using UnityEngine;
using System.Collections;

public class Pillars_rotate : MonoBehaviour {
    public GameObject Pillar1;
    public GameObject Pillar2;
    public GameObject Pillar3;
	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        Pillar1.transform.Rotate(0, 0.3f, 0);
        Pillar2.transform.Rotate(0.3f, 0, 0);
        Pillar3.transform.Rotate(0.3f, 0, 0);
	}
}
