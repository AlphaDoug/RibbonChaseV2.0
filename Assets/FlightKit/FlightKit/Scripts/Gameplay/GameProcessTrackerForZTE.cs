﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

namespace FlightKit
{
    /// <summary>
    /// Keeps track of how many pickups the user has collected and registers the level completed when no pickups are left.
    /// </summary>
    public class GameProcessTrackerForZTE : MonoBehaviour
    {
        public Text pickupsCurrentText;
        public Text pickupsTotalText;
        public Image pickupIconImage;

        private int _numPickupsCollected = 0;
        private int _numPickupsTotal;

        private RidParents ridParents;
        private bool levelFinished;
        public GameObject levelCompletedMenu;
        private GameControllerVR gameController;
        void Start()
        {
            gameController = GameObject.Find("GameController").GetComponent<GameControllerVR>();
            ridParents = GameObject.Find("MainCamera").GetComponent<RidParents>();
            // If UI text holders not provided, try to find them by names.
            if (pickupsCurrentText == null)
            {
                var pickupsCurrent = GameObject.Find("PickupsCurrent");
                if (pickupsCurrent != null)
                {
                    pickupsCurrentText = pickupsCurrent.GetComponent<Text>();
                }
            }

            // If UI text holders not provided, try to find them by names.
            if (pickupsTotalText == null)
            {
                var pickupsTotal = GameObject.Find("PickupsTotal");
                if (pickupsTotal != null)
                {
                    pickupsTotalText = pickupsTotal.GetComponent<Text>();
                }
            }

            // If UI text holders not provided, try to find them by names.
            if (pickupIconImage == null)
            {
                var pickupIcon = GameObject.Find("PickupIcon");
                if (pickupIcon != null)
                {
                    pickupIconImage = pickupIcon.GetComponent<Image>();
                }
            }

            var pickups = GameObject.FindObjectsOfType<PickUpStar>();
            _numPickupsTotal = pickups.Length;
            if (pickupsTotalText != null)
            {
                pickupsTotalText.text = _numPickupsTotal.ToString();
            }

            PickUpStar.OnCollectEvent += RegisterPickup;
        }

        public void SetLevelFinished(bool IslevelFinished)
        {
            levelFinished = IslevelFinished;
        }

        public bool GetLevelFinished()
        {
            return levelFinished;
        }
        void OnDestroy()
        {
            PickupSphere.OnCollectEvent -= RegisterPickup;
        }

        private void RegisterPickup()
        {
            if (_numPickupsCollected == 0)
            {
                ShowPickupCounter();
            }

            _numPickupsCollected++;

            if (pickupsCurrentText != null)
            {
                pickupsCurrentText.text = _numPickupsCollected.ToString();
            }

            if (_numPickupsCollected >= _numPickupsTotal)
            {
                RegisterLevelComplete();
                Debug.Log("Completed");
            }
        }

        public virtual void RegisterLevelComplete()
        {
            //TODO: Turn off collisions on the airplane.

            // Turn off fuel consumption.
            FuelController fc = GameObject.FindObjectOfType<FuelController>();
            if (fc != null)
            {
                fc.enabled = false;
            }

            StartCoroutine(FadeOutCoroutine());
        }

        private IEnumerator FadeOutCoroutine()
        {
            var bloom = GameObject.FindObjectOfType<BloomOptimized>();
            float targetIntensity = 2.3f;
            float targetThreshold = 0.4f;

            var musicController = GameObject.FindObjectOfType<MusicController>();
            bool tweenMusic = musicController != null && musicController.gameplay != null;

            var wait = new WaitForEndOfFrame();
            float tween = 1f;
            float tweenSpeed = 0.5f;
            float startTime = Time.realtimeSinceStartup;
            float lastTime = startTime;
            float deltaTime = 0;
            float fixedDeltaTime = Time.fixedDeltaTime;

            while (tween > 0.1)
            {
                deltaTime = Time.realtimeSinceStartup - lastTime;
                lastTime = Time.realtimeSinceStartup;

                if (bloom != null)
                {
                    bloom.intensity = Mathf.Lerp(bloom.intensity, targetIntensity, tweenSpeed * deltaTime);
                    bloom.threshold = Mathf.Lerp(bloom.threshold, targetThreshold, tweenSpeed * deltaTime);
                }

                if (tweenMusic)
                {
                    musicController.gameplay.volume = Mathf.Lerp(musicController.gameplay.volume,
                            0f, tweenSpeed * deltaTime);
                }

                // Slow down.
                Time.timeScale = Mathf.Lerp(Time.timeScale, 0f, tweenSpeed * deltaTime);
                Time.fixedDeltaTime = fixedDeltaTime * Time.timeScale;

                tween = Mathf.Lerp(tween, 0f, tweenSpeed * deltaTime);

                yield return wait;
            }

            // Speed up again.
            Time.timeScale = 1f;
            Time.fixedDeltaTime = fixedDeltaTime;

            SetLevelFinished(true);
            ridParents.GetRidParents();
            gameController.DisActiveButtonOnOver();
            //gameController.SetActiveButtonOnOver();
            //levelCompletedMenu.SetActive(true);
            // Register level complete.

            //LevelCompleted();
        }

        public void LevelCompleted()
        {
            // Register level complete.
            LevelCompleteController lcc = GameObject.FindObjectOfType<LevelCompleteController>();
            if (lcc != null)
            {

                lcc.HandleLevelComplete();
            }
        }
        private void ShowPickupCounter()
        {
            if (pickupIconImage != null)
            {
                pickupIconImage.enabled = true;
                pickupIconImage.canvasRenderer.SetAlpha(0.0f);
                pickupIconImage.CrossFadeAlpha(1.0f, 5.0f, false);
            }

            if (pickupsCurrentText != null)
            {
                pickupsCurrentText.enabled = true;
                pickupsCurrentText.canvasRenderer.SetAlpha(0.0f);
                pickupsCurrentText.CrossFadeAlpha(1.0f, 5.0f, false);
            }

            if (pickupsTotalText != null)
            {
                pickupsTotalText.enabled = true;
                pickupsTotalText.canvasRenderer.SetAlpha(0.0f);
                pickupsTotalText.CrossFadeAlpha(1.0f, 5.0f, false);
            }
        }
    }

}