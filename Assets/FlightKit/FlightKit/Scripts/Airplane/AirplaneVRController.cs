using System.Collections;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Vehicles.Aeroplane;

namespace FlightKit
{
    /// <summary>
    /// Reads the user input from standalone or mobile controls and feeds it to the user-controlled AeroplaneController.
    /// </summary>
    [RequireComponent(typeof(AeroplaneController))]
    public class AirplaneVRController : MonoBehaviour
    {
        public GameObject leftRotateButton;
        /// <summary>
		/// Maximum allowed roll angle on mobile.
		/// </summary>
        public float maxRollAngle = 80;
        /// <summary>
        /// Maximum allowed roll angle on mobile.
        /// </summary>
        public float maxPitchAngle = 80;

        [Range(0, 1)]
        private float rightRotateClamp = 0;
        [Range(-1, 0)]
        private float leftRotateClamp = 0;
        [Range(-1, 1)]
        private float rotateClamp = 0;
        /// <summary>
        /// Reference to the aeroplane that we're controlling
        /// </summary>
        private AeroplaneController _airplane;

        /// <summary>
        /// euler angle
        /// </summary>
        private Vector3 euler;
        private bool isAccelation = true;
        private string lastButtonName;
        private float sysTime;
        private float lastButtonPressedTime;
        private int buttonPressedCount = 0;
        private void Awake()
        {
            // Set up the reference to the aeroplane controller.
            _airplane = GetComponent<AeroplaneController>();
        }

        IEnumerator Start()
        {
            // Disable aerodynamic effect at start to prevent the plane from jitter on ground.
            float aerodynamicEffect = _airplane.AerodynamicEffect;
            _airplane.AerodynamicEffect = 0f;

            yield return new WaitForSeconds(3);

            // Enable aerodynamic effect.
            _airplane.AerodynamicEffect = aerodynamicEffect;
        }


        private void FixedUpdate()
        {

            if (!isAccelation)
            {
                Mathf.Clamp(_airplane.MaxSpeed -= 1f, 200, 100);
                if (_airplane.MaxSpeed < 100)
                {
                    _airplane.MaxSpeed = 100;
                    isAccelation = true;
                }
            }
#if UNITY_STANDALONE_WIN
             //Read input for the pitch, yaw, roll and throttle of the aeroplane.

             float mousePitch = ControlsPrefs.IsMouseEnabled ? CrossPlatformInputManager.GetAxis("Mouse Y") : 0;
             float mouseRoll = ControlsPrefs.IsMouseEnabled ? CrossPlatformInputManager.GetAxis("Mouse X") : 0;
             
             float roll = ControlsPrefs.IsRollEnabled? CrossPlatformInputManager.GetAxis("Roll") + mouseRoll : 0;
             //Read inputs. They are clamped in AeroplaneController, so can go out of [-1, 1] here.

             float pitch = (ControlsPrefs.IsInversePitch ? -1f : 1f) * CrossPlatformInputManager.GetAxis("Pitch") ;
             float yaw = CrossPlatformInputManager.GetAxis("Yaw");
#endif

#if UNITY_ANDROID
            rotateClamp = rightRotateClamp != 0 ? rightRotateClamp : leftRotateClamp;
            float roll = ControlsPrefs.IsRollEnabled ? rotateClamp : 0;
            //float yaw = Input.acceleration.x;
            //float pitch = (ControlsPrefs.IsInversePitch ? -1f : 1f) * Input.acceleration.y;
            float yaw = Input.acceleration.x;
            float pitch = (ControlsPrefs.IsInversePitch ? -1f : 1f) * -Input.acceleration.z;
#endif
            bool airBrakes = CrossPlatformInputManager.GetButton("Brakes");
            // auto throttle up, or down if braking.
            float throttle = airBrakes ? -1 : 1;

            // Pass the input to the aeroplane
            _airplane.Move(roll, pitch, yaw, throttle, airBrakes);
        }

        private void AdjustInputForMobileControls(ref float roll, ref float pitch, ref float throttle)
        {
            float intendedRollAngle = roll * maxRollAngle * Mathf.Deg2Rad;
            float intendedPitchAngle = pitch * maxPitchAngle * Mathf.Deg2Rad;
            roll = Mathf.Clamp((intendedRollAngle - _airplane.RollAngle), -1, 1);
            pitch = Mathf.Clamp((intendedPitchAngle - _airplane.PitchAngle), -1, 1);
            float intendedThrottle = throttle * 0.5f + 0.5f;
            throttle = Mathf.Clamp(intendedThrottle - _airplane.Throttle, -1, 1);
        }

        public void RightRotate()
        {
            Mathf.Clamp(rightRotateClamp += 0.1f, 0, 1);
            if (rightRotateClamp > 1)
            {
                rightRotateClamp = 1;
            }

        }

        public void LeftRotate()
        {
            Mathf.Clamp(leftRotateClamp -= 0.1f, -1, 0);
            if (leftRotateClamp < -1)
            {
                leftRotateClamp = -1;
            }


        }
        public void ClearLRR()
        {
            rightRotateClamp = 0;
            leftRotateClamp = 0;
        }

        public void Accelation()
        {
            Mathf.Clamp(_airplane.MaxSpeed += 10f, 100, 200);
            if (_airplane.MaxSpeed > 200)
            {
                _airplane.MaxSpeed = 200;
            }
        }
        public void NormalSpeed()
        {
            isAccelation = false;
            //_airplane.MaxSpeed = 100;

        }
        void On_ButtonDown(string buttonName)
        {

            if (sysTime - lastButtonPressedTime < 0.5f)
            {

                if (lastButtonName == buttonName)
                {

                    NormalSpeed();
                }
                else
                {

                    Accelation();
                }
            }
            lastButtonPressedTime = Time.time;
            lastButtonName = buttonName;
        }
    }

}
