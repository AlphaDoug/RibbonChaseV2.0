using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDir : MonoBehaviour {
    [SerializeField]
    public RectTransform LevelP;
    public RectTransform[] levels;
    //public Transform camera;
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
		for(int i = 0; i < levels.Length; i++)
        {
            //levels[i].rotation = new Quaternion(LevelP.rotation.x, LevelP.rotation.y, LevelP.rotation.z, 0f);
            //Face Camera
            levels[i].transform.LookAt(Camera.main.transform);
            //Scale with distance to camera
            float dis = ((-Vector3.Distance(levels[i].position, Camera.main.transform.position) + 3500f) / 650);
            levels[i].localScale = new Vector3(
                (dis * 2 > 0.5f) ? dis * 2 : 0.5f,
                (dis * 2 > 0.5f) ? dis * 2 : 0.5f,
                (dis * 2 > 0.5f) ? dis * 2 : 0.5f
                );
        }
      //  Debug.Log("levels[0]" + levels[0].localRotation.eulerAngles.y);
        
	}
}
