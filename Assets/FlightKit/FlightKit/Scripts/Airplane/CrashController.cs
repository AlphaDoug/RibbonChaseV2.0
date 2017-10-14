using UnityEngine;
using System.Collections;
using UnityStandardAssets.Utility;
using UnityStandardAssets.ImageEffects;
using System;

namespace FlightKit
{
	/// <summary>
	/// Detects when the airplane has crashed and controls the corresponding effects.
	/// </summary>
    public class CrashController : MonoBehaviour
    {
        public bool isAI = false;
		/// <summary>
		/// Collision impulste which is considered enough for a crash, leads to respawning.
		/// </summary>
        public float crashImpulse = 10f;
		/// <summary>
		/// Collision impulse which is too big to ignore, but not enough for crash. Used for effects.
		/// </summary>
        public float soundImpulse = 5f;
        public GameObject airPlaneAI;
		/// <summary>
		/// The sound of airplane crashing, which leads to being respawned.
		/// </summary>
        [Space]
        public AudioClip crash;
		/// <summary>
		/// The sound of airplane hitting something, not hard enough for a crash. Doesn't lead to respawning.
		/// </summary>
        public AudioClip hit;
        /// <summary>
        /// avoid twice crash
        /// </summary>
        public bool isCrash=true;
        private AudioSource _soundSource;

        void Start()
        {
            _soundSource = GetComponent<AudioSource>();
        }

        public void OnCollisionEnter(Collision collision)
        {
            float collisionMagnitude = collision.impulse.magnitude;

            // Do not register small collisions with the take off platform.
            if (!collision.gameObject.CompareTag(Tags.TakeOffPlatform))
            {
                if (collisionMagnitude > soundImpulse)
                {
                    RegisterHit();
                }
            }

            // If the collision is strong enough to reset the plane.
            if (collisionMagnitude > crashImpulse)
            {
                RegisterCrash();
            }
        }

        private void RegisterHit()
        {
            if (!isAI)
            {
                if (_soundSource != null && _soundSource.isActiveAndEnabled)
                {
                    _soundSource.PlayOneShot(crash);
                }
                StartCoroutine(CollisionCameraAnimation());
            }          
        }

        private void RegisterCrash()
        {
            if (!isAI)
            {
                //lifenum--
                //if (LifeCtrl.curLifeNum > 0&&isCrash)
                //{
                //    LifeCtrl.curLifeNum--;
                //    isCrash = false;
                //    Invoke("setIsCrash", 0.5f);
                //}
                if (LifeNumCtrl.lifeNum > 0 && isCrash)
                {
                    LifeNumCtrl.lifeNum--;
                    isCrash = false;
                    Invoke("setIsCrash", 0.5f);
                }
                // Play hit sound fx.
                if (_soundSource != null)
                {
                    _soundSource.PlayOneShot(hit);
                }
                GameObject.Find("DistanceTest").GetComponent<BrushPastControl>().SetPlaneStateFarAway();
                GameObject.Find("DistanceTest").GetComponent<BrushPastControl>().SetTriggerUnable();
                Invoke("SetTriggerable", 2.0f);
            }          
            if (isAI)
            {
                Invoke("ChooseNextTarget", 1.0f);
            }
         
            //Handheld.Vibrate();
            // Clear trails of the plane.
            var trails = GetComponent<AirplaneTrails>();
            trails.DeactivateTrails();
            trails.ClearTrails();
            if (PlayerPrefs.GetInt("VibrationOn") == 1)
            {
                Handheld.Vibrate();
            }
            // Reset the plane.
            var reset = GetComponent<ObjectResetter>();
            if (reset != null)
            {
                reset.DelayedReset(0.4f);
            }
        }
        private void setIsCrash()
        {
            isCrash = true;
        }
        private IEnumerator CollisionCameraAnimation()
        {
            // Enable glitch component for short time.
            var cameraFx = GameObject.FindObjectOfType<NoiseAndScratches>();
            cameraFx.enabled = true;

            yield return new WaitForSeconds(1.0f);

            cameraFx.enabled = false;
        }
        private void ChooseNextTarget()
        {
            switch (GamePickUpsAmount.aiDifficuty)
            {
                case AeroplaneAiControl.AIDifficuty.easy:
                    airPlaneAI.SendMessage("ChooseNextTarget_Easy");
                    break;
                case AeroplaneAiControl.AIDifficuty.normal:
                    airPlaneAI.SendMessage("ChooseNextTarget_Normal");
                    break;
                case AeroplaneAiControl.AIDifficuty.hard:
                    airPlaneAI.SendMessage("ChooseNextTarget_Hard");
                    break;
                default:
                    break;
            }
        }
        private void SetTriggerable()
        {
            if (GameObject.Find("DistanceTest"))
            {
                GameObject.Find("DistanceTest").GetComponent<BrushPastControl>().SetTriggerable();
            }
            
        }
    }

}