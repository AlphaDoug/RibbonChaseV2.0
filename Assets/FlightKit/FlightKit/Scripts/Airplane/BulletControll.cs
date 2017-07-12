using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControll : MonoBehaviour {
    public Transform targetPosition;
    public Transform airPlane;
    public float maxSpeed = 10.0f;
    public float acceleratedSpeed = 1.0f;
    private Rigidbody bulletRigidbody;
    private bool isFire = false;
    private float currentSpeed = 0.0f;
	// Use this for initialization
	void Start ()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (isFire)
        {
            if (currentSpeed < maxSpeed)
            {
                transform.Translate((targetPosition.position - transform.position).normalized * currentSpeed);
                currentSpeed += acceleratedSpeed;
            }
            else
            {
                transform.Translate((targetPosition.position - transform.position).normalized * maxSpeed);
            }
        }
        
    }
    public void Fire()
    {
        transform.position = airPlane.transform.position;
        isFire = true;
    }
}
