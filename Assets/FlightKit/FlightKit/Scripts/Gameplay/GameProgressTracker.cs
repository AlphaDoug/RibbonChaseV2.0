using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

namespace FlightKit
{
	/// <summary>
	/// Keeps track of how many pickups the user has collected and registers the level completed when no pickups are left.
	/// </summary>
    public class GameProgressTracker : MonoBehaviour
    {
        public Text pickupsCurrentText;
        public Text pickupsTotalText;
        public Image pickupIconImage;

        public Text pickupsCurrentTextAI;
        public Text pickupsTotalTextAI;
        public Image pickupIconImageAI;

        public static int pickupsCollected;
        public static int pickupsCollectedAI;

        private int _numPickupsCollected = 0;
        private int _numPickupsCollectedAI = 0;
        private int _numPickupsTotal;
        private GameObject airplane;
        private RidParents ridParents;
        private bool levelFinished;
        public GameObject levelCompletedMenu;
        private GameController gameController;
        void Start()
        {
            
            gameController = GameObject.Find("GameController").GetComponent<GameController>();
            if (GameObject.Find("MainCamera").GetComponent<RidParents>())
            {
                ridParents = GameObject.Find("MainCamera").GetComponent<RidParents>();
            }
            else
            {
                Debug.Log("Can not find RidParents.");
            }

            #region If UI text holders not provided, try to find them by names.
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
            #endregion

            #region If UI_AI text holders not provided, try to find them by names.
            // If UI text holders not provided, try to find them by names.
            if (pickupsCurrentTextAI == null)
            {
                var pickupsCurrentAI = GameObject.Find("PickupsCurrentAI");
                if (pickupsCurrentAI != null)
                {
                    pickupsCurrentText = pickupsCurrentAI.GetComponent<Text>();
                }
            }

            // If UI text holders not provided, try to find them by names.
            if (pickupsTotalTextAI == null)
            {
                var pickupsTotalAI = GameObject.Find("PickupsTotalAI");
                if (pickupsTotalAI != null)
                {
                    pickupsTotalTextAI = pickupsTotalAI.GetComponent<Text>();
                }
            }

            // If UI text holders not provided, try to find them by names.
            if (pickupIconImageAI == null)
            {
                var pickupIconAI = GameObject.Find("PickupIconAI");
                if (pickupIconAI != null)
                {
                    pickupIconImage = pickupIconAI.GetComponent<Image>();
                }
            }
            #endregion
            
            var pickups = GameObject.FindObjectsOfType<PickupSphere>();
            _numPickupsTotal = pickups.Length;
            GamePickUpsAmount.pickUpsAmount = _numPickupsTotal;
            if (pickupsTotalText != null)
            {
                if (pickupsTotalText != null)
                {
                    pickupsTotalText.text = _numPickupsTotal.ToString();
                }
                if (pickupsTotalTextAI != null)
                {
                    pickupsTotalTextAI.text = _numPickupsTotal.ToString();
                }
                
            }
            
            PickupSphere.OnCollectEvent += RegisterPickup;
            PickupSphere.OnCollectEventAI += RegisterPickupAI;
        }

        public void SetLevelFinished(bool IslevelFinished)
        {
            levelFinished = IslevelFinished;
        }

        public  bool GetLevelFinished()
        {
            return levelFinished;
        }
        void OnDestroy()
        {
            PickupSphere.OnCollectEvent -= RegisterPickup;
            PickupSphere.OnCollectEventAI -= RegisterPickupAI;
        }
        /// <summary>
        /// AI收集光球时候会触发此函数
        /// </summary>
        private void RegisterPickupAI()
        {
            if (_numPickupsCollectedAI == 0)
            {
                ShowPickupCounterAI();
            }
            _numPickupsCollectedAI++;
            GamePickUpsAmount.currentPickUpsAmount--;
            pickupsCollectedAI = _numPickupsCollectedAI;
            if (pickupsCurrentTextAI != null)
            {
                pickupsCurrentTextAI.text = _numPickupsCollectedAI.ToString();
            }
            if (_numPickupsCollected + _numPickupsCollectedAI >= _numPickupsTotal)
            {
                RegisterLevelComplete();
            }
        }
        /// <summary>
        /// 玩家收集到光球的时候会触发此函数
        /// </summary>
        private void RegisterPickup()
        {
           
            if (_numPickupsCollected == 0)
            {
                //ShowPickupCounter();
            }
           // Debug.Log(_numPickupsCollected);
            _numPickupsCollected++;
            pickupsCollected = _numPickupsCollected;
            GamePickUpsAmount.currentPickUpsAmount--;
            if (pickupsCurrentText != null)
            {
                pickupsCurrentText.text = _numPickupsCollected.ToString();
            }
          //  Debug.Log(GamePickUpsAmount.currentPickUpsAmount);
            if (_numPickupsCollected + _numPickupsCollectedAI >= _numPickupsTotal)
            {
                RegisterLevelComplete();            
            }
        }

        public virtual void RegisterLevelComplete()
        {
            //TODO: Turn off collisions on the airplane.
            
            // Turn off fuel consumption.
            FuelController fc = GameObject.FindObjectOfType<FuelController>();
            if (fc != null) {
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
            gameController.DisActiveButtonOnOver();
            //gameController.SetActiveButtonOnOver();
            //levelCompletedMenu.SetActive(true);
            // Register level complete.
            //if (Application.loadedLevelName == "Gameplay_1_Redo")
            //{
            //    ShowOverMenu();
            //}
            //else
            //{
            //    Invoke("ShowOverMenu", 8f);
            //}
            levelCompletedMenu.SetActive(true);
            
            Invoke("ShowOverMenu",3f);
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

        public void ShowOverMenu()
        {           
            Time.timeScale = 0;
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
        private void ShowPickupCounterAI()
        {
            if (pickupIconImageAI != null)
            {
                pickupIconImageAI.enabled = true;
                pickupIconImageAI.canvasRenderer.SetAlpha(0.0f);
                pickupIconImageAI.CrossFadeAlpha(1.0f, 5.0f, false);
            }

            if (pickupsCurrentTextAI != null)
            {
                pickupsCurrentTextAI.enabled = true;
                pickupsCurrentTextAI.canvasRenderer.SetAlpha(0.0f);
                pickupsCurrentTextAI.CrossFadeAlpha(1.0f, 5.0f, false);
            }

            if (pickupsTotalTextAI != null)
            {
                pickupsTotalTextAI.enabled = true;
                pickupsTotalTextAI.canvasRenderer.SetAlpha(0.0f);
                pickupsTotalTextAI.CrossFadeAlpha(1.0f, 5.0f, false);
            }

        }
    }

}