using UnityEngine;
using System.Collections;

public class AnimationCtrl : MonoBehaviour {
    [SerializeField] Animation animation;
    public GameObject centre;
    public float rotate_speed = 1;
	// Use this for initialization
	void Start () 
    {
        animation.Play("Take 001");
	}
	
	// Update is called once per frame
	void Update () 
    {

        transform.RotateAround(centre.transform.position, Vector3.up, rotate_speed * Time.deltaTime);
	}
}
