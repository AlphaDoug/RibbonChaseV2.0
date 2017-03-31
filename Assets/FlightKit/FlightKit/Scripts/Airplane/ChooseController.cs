using UnityEngine;
using System.Collections;
namespace FlightKit
{
    public class ChooseController : MonoBehaviour
    {
        public GameObject airPlane;
        public GameObject joyStick;
        private AirplaneUserControl gravityController;
        private AirPlaneJoystickController joyController;
        void Awake()
        {
            if (airPlane == null)
            {
                Debug.LogError("No Plane Found!");
            }
            else if (airPlane.GetComponent<AirplaneUserControl>() && airPlane.GetComponent<AirPlaneJoystickController>())
            {
                gravityController = airPlane.GetComponent<AirplaneUserControl>();
                joyController = airPlane.GetComponent<AirPlaneJoystickController>();
            }
            if (PlayerPrefs.GetInt("AirPlaneController") == null)
            {
                PlayerPrefs.SetInt("AirPlaneController",0);
            }
            if (PlayerPrefs.GetInt("AirPlaneController") == 1)
            {
               
                gravityController.enabled = true;
                joyController.enabled = false;
                //if (GameControllerVR.AirPlaneController == 0)
                //{                                   
                //    if (gravityController)
                //    {
                //        gravityController.enabled = true;
                //    }


                //    if (joyController != null)
                //    {
                //        joyController.enabled = false;
                //    }

                //    joyStick.SetActive(false);
                //}
                //else
                //{
                //    gravityBorder.SetActive(false);
                //    joyBorder.SetActive(true);

                //    //if (gravityIntro && joyStickIntro)
                //    //{
                //      //  gravityIntro.SetActive(false);
                //        //joyStickIntro.SetActive(true);
                //    //}
                //    if (gravityBorder)
                //    {
                //        gravityBorder.SetActive(false);
                //    }
                //    if (joyBorder)
                //    {
                //        joyBorder.SetActive(true);
                //    }

                //    if (gravityController != null)
                //    {
                //        gravityController.enabled = false;
                //    }
                //    if (joyController != null)
                //    {
                //        joyController.enabled = true;
                //    }
                //    if (joyStick != null)
                //    {
                //        joyStick.SetActive(true);
                //    }

            }
            else
            {
                gravityController.enabled = false;
                joyController.enabled=true;
                joyStick.SetActive(true);
            }         
        }

    }
}
