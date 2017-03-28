using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Vehicles.Aeroplane;

namespace FlightKit
{
    [RequireComponent(typeof(AeroplaneController))]
    public class AirPlaneAIControll : MonoBehaviour
    {
        public GameObject pickUp1;
        private Transform pickUp1Transform;
        private AeroplaneController _airplane;
        
        // Use this for initialization
        void Start()
        {
            pickUp1Transform = pickUp1.GetComponent<Transform>();
        }

        private void Awake()
        {
            // Set up the reference to the aeroplane controller.
            _airplane = GetComponent<AeroplaneController>();
        }
        // Update is called once per frame
        void FixedUpdate()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log(transform.position.y.ToString() + "  " +pickUp1Transform.position.y.ToString());
            }
            if (transform.position.y > pickUp1Transform.position.y)
            {
                _airplane.Move(0, -(transform.position.y - pickUp1Transform.position.y) / 10000f, 0, 0, false);
            }
            if (transform.position.y < pickUp1Transform.position.y)
            {
                _airplane.Move(0, (transform.position.y - pickUp1Transform.position.y) / 10000f, 0, 0, false);
            }
            
        }
    }

}
