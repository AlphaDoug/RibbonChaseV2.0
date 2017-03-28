using UnityEngine;
using System.Collections;
using UnityStandardAssets.Cameras;

namespace FlightKit
{
    public class RidParents : MonoBehaviour
    {
        public void GetRidParents()
        {
            GameObject.Find("AirplaneCameraRig").GetComponent<ProtectCameraFromWallClip>().enabled = false;
            GameObject.Find("AirplaneCameraRig").GetComponent<AutoCam>().enabled = false;
            //transform.parent.DetachChildren();
            gameObject.GetComponent<CameraMove>().enabled = true;
        }
    }
}
