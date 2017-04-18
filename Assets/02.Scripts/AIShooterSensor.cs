using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FlightKit
{
    public class AIShooterSensor : MonoBehaviour
    {
        AIFireCtrl fireCtrl = new AIFireCtrl();
        public GameObject AiAirPlane;
        // Use this for initialization
        void Start()
        {
            fireCtrl = AiAirPlane.GetComponentInParent<AIFireCtrl>();
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                GamePickUpsAmount.aiCurrentTarget = other.transform;
                fireCtrl.isShoot = true;
                
            }
        }
        void OnTriggerExit(Collider other)
        {
            if (other.tag == "Player")
            {
                GamePickUpsAmount.aiCurrentTarget = null;
                fireCtrl.isShoot =  false;
            }
        }
    }

}
