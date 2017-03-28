using UnityEngine;
using System.Collections;

public class ControlElectriTrail : MonoBehaviour {
    public GameObject[] ElecricTrail;
    public bool[] TrailActive;
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {

	    for(int i = 0; i < ElecricTrail.Length; i++)
        {
            if (ElecricTrail[i].activeSelf)
            {
                TrailActive[i] = true;
            }
            if (!ElecricTrail[i].activeSelf&&TrailActive[i])
            {
                ElecricTrail[i].SetActive(true);
            }
        }
	}
}
