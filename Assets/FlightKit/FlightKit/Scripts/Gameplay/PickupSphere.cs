using System;
using UnityEngine;
using UnityStandardAssets.ImageEffects;


namespace FlightKit
{
	/// <summary>
	/// Makes the object grow slowly and be picked up by the user.
	/// </summary>
	public class PickupSphere : MonoBehaviour
	{
        [SerializeField]
        public GameObject[] purpleElectricTrail;
        public GameObject[] pickUp;
        public GameObject[] powerStation;
        public delegate void OnCollectAction();
        public delegate void OnCollectActionAI();
        /// <summary>
        /// The event is fired every time the user-controlled airplane has picked up a sphere.
        /// </summary>
        public static event OnCollectAction OnCollectEvent;
        /// <summary>
        /// The event is fired every time the AI-controlled airplane has picked up a sphere.
        /// </summary>
        public static event OnCollectActionAI OnCollectEventAI;
        /// <summary>
        /// Should all PickupSpheres grow?
        /// </summary>
        public static bool growingEnabled = false;
		/// <summary>
		/// How fast does this object grow.
		/// </summary>
		public float growthSpeed = 0.1f;
		/// <summary>
		/// At what scale should growing stop.
		/// </summary>
		public float maxScale = 40f;
		/// <summary>
		/// How fast should the rings rotate?
		/// </summary>
		public float ringRotationSpeed = 150f;

		public GameObject ring1;
		public GameObject ring2;
		public GameObject sphere;

		private bool _activated;
		private BloomOptimized _bloom;
		private float _bloomInitValue;
		private bool _isTweeningOut = false;
		private bool _isDestroyed = false;
        private GameObject halo;
        private bool isAndroid = false;
        private AndroidJavaClass jc;
        private AndroidJavaObject jo;
        private GameObject airPlaneAI;

        void Start()
		{
            if (Application.platform == RuntimePlatform.Android)
            {
                jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
                isAndroid = true;
            }
            airPlaneAI = GameObject.Find("AirplaneAI");
            // Init vars.
            _bloom = GameObject.FindObjectOfType<BloomOptimized>();
            if (_bloom != null)
            {
                _bloomInitValue = _bloom.intensity;
            }
            //halo = sphere.GetComponent<Halo>();
			// Randomize initial rotation.
			transform.localRotation = UnityEngine.Random.rotation;

        }

		void Update()
		{
			// Do not update if picked up and tweened out.
			if (_isDestroyed)
			{
				return;
			}

			// Rotate rings.
			ring1.transform.Rotate(Vector3.right, ringRotationSpeed*Time.deltaTime);
			ring2.transform.Rotate(Vector3.up, ringRotationSpeed*Time.deltaTime);

			// Grow in size over time.
			if (growingEnabled && transform.localScale.x < maxScale)
			{
				transform.localScale += Vector3.one*growthSpeed*Time.deltaTime;
			}

			// Check if tweening out.
			if (_isTweeningOut)
			{
				transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, 5f*Time.deltaTime);
                if (_bloom != null)
                {
				    _bloom.intensity = Mathf.Lerp(_bloom.intensity, _bloomInitValue, 5f*Time.deltaTime);
                }
			}
		}

		void OnTriggerEnter(Collider collider)
		{
			if (_activated)
			{
				return;
			}

            if (collider.gameObject.CompareTag(Tags.PlayerAI))
            {

                _activated = true;
                if (OnCollectEventAI != null)
                {
                    OnCollectEventAI();
                }
                // Tween out the sphere
                _isTweeningOut = true;
                
                // Disperse boids
                var boids = GetComponentInParent<BoidMaster>();
                if (boids != null)
                {
                    boids.neighborDistance = 250f;
                }
                //从全局数组中移除被收集的光球
                RemoveCollectedPickup();// Self destroy after delay (can't destroy this GO completely, since that would break boids)
                DestroyNow();
                //按照困难度选择选择下一个目标光球
                
                Invoke("ChooseNextTarget", 0.5f);
                //CheckPreNext();
            }
			if (collider.gameObject.CompareTag(Tags.Player))
			{
				_activated = true;
                Debug.Log("玩家吃球");

                if (GameObject.Find("AirplaneAI") != null)

                if (airPlaneAI != null)

                {
                    //从全局数组中移除被收集的光球
                    RemoveCollectedPickup();
                }
                
                if (OnCollectEvent != null)
                {
                    OnCollectEvent();
                }
                if (isAndroid)
                {
                    jo.Call("StartShock", new long[] { 100, 100 }, -1);
                }

				var sound = GetComponentInParent<AudioSource>();
                if (sound != null) 
                {
				    sound.Play();
                }
                // Tween out the sphere
                _isTweeningOut = true;

				// Flash screen
                if (_bloom != null)
                {
				    _bloom.intensity = 0.5f;
                }

                // Disperse boids
                var boids = GetComponentInParent<BoidMaster>();
				if (boids != null)
				{
					boids.neighborDistance = 250f;
				}
                // Self destroy after delay (can't destroy this GO completely, since that would break boids)
                Invoke("DestroyNow", 1f);
                //CheckPreNext();
                if (airPlaneAI != null)
                {
                    Invoke("ChooseNextTarget", 0.5f);
                }
              

            }
		}

        public void CheckPreNext()
        {
            string indexTemp = gameObject.name.ToString().Substring(7, 1);
            int sphereIndex = Convert.ToInt16(indexTemp);
            Debug.Log("spherIndex" + sphereIndex);
            switch ((sphereIndex-1))
            {
                case 0:
                    if (pickUp[1].GetComponent<PickupSphere>().ring1==null)
                    {
                        purpleElectricTrail[0].SetActive(true);
                    }
                    if (pickUp[4].GetComponent<PickupSphere>().ring1 == null)
                    {
                        purpleElectricTrail[4].SetActive(true);
                    }
                    break;
                case 1:
                    if (pickUp[0].GetComponent<PickupSphere>().ring1 == null)
                    {
                        purpleElectricTrail[0].SetActive(true);
                    }
                    if (pickUp[2].GetComponent<PickupSphere>().ring1 == null)
                    {
                        purpleElectricTrail[1].SetActive(true);
                    }
                    break;
                case 2:
                    if (pickUp[1].GetComponent<PickupSphere>().ring1 == null)
                    {
                        purpleElectricTrail[1].SetActive(true);
                    }
                    if (pickUp[3].GetComponent<PickupSphere>().ring1 == null)
                    {
                        purpleElectricTrail[2].SetActive(true);
                    }
                    break;
                case 3:
                    if (pickUp[2].GetComponent<PickupSphere>().ring1 == null)
                    {
                        purpleElectricTrail[2].SetActive(true);
                    }
                    if (pickUp[4].GetComponent<PickupSphere>().ring1 == null)
                    {
                        purpleElectricTrail[3].SetActive(true);
                    }
                    break;
                case 4:
                    if (pickUp[0].GetComponent<PickupSphere>().ring1 == null)
                    {
                        purpleElectricTrail[4].SetActive(true);
                    }
                    if (pickUp[3].GetComponent<PickupSphere>().ring1 == null)
                    {
                        purpleElectricTrail[3].SetActive(true);
                    }
                    break;
                default:

                    break;

            }
                
        }

		public void TweenBloom(float value)
		{
			_bloom.intensity = value;
		}

		private void DestroyNow()
		{
			_bloom.intensity = _bloomInitValue;
            GetComponent<SphereCollider>().enabled = false;
            DestroyObject(sphere);
			DestroyObject(ring1);
			DestroyObject(ring2);
           
			_isDestroyed = true;
		}

        private void RemoveCollectedPickup()
        {
            string indexTemp = gameObject.name.ToString().Substring(8, 2);
            int sphereIndex = Convert.ToInt16(indexTemp);
            GamePickUpsAmount.isCurrentLevelPickUpsCollected[sphereIndex] = true;//角标为sphereIndex的光球将会为true 代表已收集
            //当玩家收集到一个光球的时候，如果AI目标也是这个光球，则AI立即选择下一个光球、
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
                    //while (GamePickUpsAmount.aiCurrentTarget == null)
                    //{
                    //    airPlaneAI.SendMessage("ChooseNextTarget_Hard");
                    //    break;
                    //}
                    break;
                default:
                    break;
            }
        }
    }
}