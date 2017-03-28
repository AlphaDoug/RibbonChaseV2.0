using System;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;
using System.Collections.Generic;
using UnityStandardAssets.Vehicles.Aeroplane;
using UnityEngine.UI;

namespace FlightKit
{
    [RequireComponent(typeof(AeroplaneController))]
    public class AeroplaneAiControl : MonoBehaviour
    {
        // This script represents an AI 'pilot' capable of flying the plane towards a designated target.
        // It sends the equivalent of the inputs that a user would send to the Aeroplane controller.
        public AIDifficuty aiDifficuty;
        public GameObject randomTarget;
        public GameObject airPlaneFront;
        public GameObject airPlaneLast;
        public GameObject airPlaneRight;
        public GameObject airPlaneTop;
        [SerializeField]
        private float m_RollSensitivity = .2f;         // How sensitively the AI applies the roll controls
        [SerializeField]
        private float m_PitchSensitivity = .5f;        // How sensitively the AI applies the pitch controls
        [SerializeField]
        private float m_LateralWanderDistance = 5;     // The amount that the plane can wander by when heading for a target
        [SerializeField]
        private float m_LateralWanderSpeed = 0.11f;    // The speed at which the plane will wander laterally
        [SerializeField]
        private float m_MaxClimbAngle = 45;            // The maximum angle that the AI will attempt to make plane can climb at
        [SerializeField]
        private float m_MaxRollAngle = 45;             // The maximum angle that the AI will attempt to u
        [SerializeField]
        private float m_SpeedEffect = 0.01f;           // This increases the effect of the controls based on the plane's speed.
        [SerializeField]
        private float m_TakeoffHeight = 20;            // the AI will fly straight and only pitch upwards until reaching this height
        private int pickUpsAmount;
        private Transform m_Target;
        private Transform m_last_Target;
        private AeroplaneController m_AeroplaneController;  // The aeroplane controller that is used to move the plane
        private float m_RandomPerlin;                       // Used for generating random point on perlin noise so that the plane will wander off path slightly
        private bool m_TakenOff;                            // Has the plane taken off yet
        private bool isRandomTarget = true;
        private int pickUpsCollectedAI = 0;
        private bool isTheTime = true;
        private List<Ray> airPlane2PickUp;
        private List<RaycastHit> airPlane2PickUpHit;
        private List<GameObject> pickUp_AI_CanReach;
        private Ray distanceTest;
        private RaycastHit distanceTestHit;

        private Vector3 ahead;
        private Vector3 top;
        private Vector3 right;
        public enum AIDifficuty
        {
            easy = 0,
            normal = 1,
            hard = 2,
        };
        // setup script properties
        private void Awake()
        {
            // get the reference to the aeroplane controller, so we can send move input to it and read its current state.
            m_AeroplaneController = GetComponent<AeroplaneController>();

            // pick a random perlin starting point for lateral wandering
            m_RandomPerlin = 0f;
        }

        private void Start()
        {
            GamePickUpsAmount.aiDifficuty = aiDifficuty;
            GamePickUpsAmount.currentLevelPickUps = new List<GameObject>();
            //通过tag将场景中的所有pickups放进一个数组中
            var pickUpsSphere = GameObject.FindGameObjectsWithTag("PickUp");
            pickUpsAmount = pickUpsSphere.Length;
            GamePickUpsAmount.isCurrentLevelPickUpsCollected = new bool[pickUpsAmount];
            //将场景中的光球放进动态数组中(倒叙添加)
            for (int i = pickUpsAmount - 1; i >= 0; i--)
            {
                GamePickUpsAmount.currentLevelPickUps.Add(pickUpsSphere[i].gameObject);
            }

            // Debug.Log("球的总数是"+GamePickUpsAmount.currentLevelPickUps.Count.ToString());
            //选择一个AI要收集的光球并以此为目标
            switch (aiDifficuty)
            {
                case AIDifficuty.easy:
                    ChooseNextTarget_Easy();
                    break;
                case AIDifficuty.normal:
                    ChooseNextTarget_Normal();
                    break;
                case AIDifficuty.hard:
                    //在困难模式下，飞机向场景中所有光球发出射线并检测能到达的光球
                    ChooseNextTarget_Hard();
                    InvokeRepeating("RepeatCheckTarget", 1, 3);
                    break;
                default:
                    break;
            }

        }

        // reset the object to sensible values
        public void Reset()
        {
            m_TakenOff = false;
        }
        // fixed update is called in time with the physics system update
        private void Update()
        {
            if (m_Target == null)
            {
                Debug.Log("null");
                return;
            }
            #region 自动躲避障碍物
            if (m_Target.gameObject.tag == Tags.PickUp)
            {
                //向正前方发射射线
                distanceTest = new Ray(transform.position, airPlaneFront.transform.position - airPlaneLast.transform.position);
                //Debug.DrawLine(distanceTest.origin, distanceTest.origin + 1000 * distanceTest.direction);
                distanceTestHit = new RaycastHit();
                Physics.Raycast(distanceTest, out distanceTestHit, 300, 1 << LayerMask.NameToLayer("Obstacle"));
                //如果前方300单位之内有障碍物，那么选择一个最佳方向
                if (distanceTestHit.collider != null)
                {
                    m_last_Target = m_Target;
                    isRandomTarget = true;
                    ahead = Vector3.Normalize(airPlaneFront.transform.position - airPlaneLast.transform.position);
                    top = Vector3.Normalize(airPlaneTop.transform.position - transform.position);
                    right = Vector3.Normalize(airPlaneRight.transform.position - transform.position);
                    var eightDirectionTest = new Vector3[8];
                    eightDirectionTest[0] = ahead + top;
                    eightDirectionTest[1] = ahead + right + top;
                    eightDirectionTest[2] = ahead + right;
                    eightDirectionTest[3] = ahead - top + right;
                    eightDirectionTest[4] = ahead - top;
                    eightDirectionTest[5] = ahead - top - right;
                    eightDirectionTest[6] = ahead - right;
                    eightDirectionTest[7] = ahead + top - right;
                    var oneDirectionRay = new Ray();
                    var oneDirectionRayHit = new RaycastHit();
                    var eightDirectionCan = new List<Vector3>();
                    for (int i = 0; i < 8; i++)
                    {
                        oneDirectionRay = new Ray(transform.position, eightDirectionTest[i]);
                        Physics.Raycast(oneDirectionRay, out oneDirectionRayHit, 900, 1 << LayerMask.NameToLayer("Obstacle"));
                        if (oneDirectionRayHit.collider == null)
                        {
                            eightDirectionCan.Add(eightDirectionTest[i]);
                        }
                        if (eightDirectionCan.Count > 0)
                        {
                            var angleEightDirectionCan = new float[eightDirectionCan.Count];
                            float minAngle = 1000f;
                            for (int j = 0; j < eightDirectionCan.Count; j++)
                            {
                                angleEightDirectionCan[j] = Vector3.Angle(m_Target.position - transform.position, eightDirectionCan[j]);
                                if (angleEightDirectionCan[j] < minAngle)
                                {
                                    angleEightDirectionCan[j] = minAngle;
                                    randomTarget.transform.position = transform.position + eightDirectionCan[j];
                                    m_Target = randomTarget.transform;
                                    GamePickUpsAmount.aiCurrentTarget = m_Target;
                                }
                            }
                        }
                    }
                }
            }
            if (m_Target.gameObject.tag == Tags.RandomTarget)
            {
                if (isRandomTarget)
                {
                    
                    Invoke("ReSetTarget", 1.0f);
                }
                #region 
                //distanceTest = new Ray(transform.position, m_last_Target.position - transform.localPosition);
                //distanceTestHit = new RaycastHit();
                //Physics.Raycast(distanceTest, out distanceTestHit, 90000, 1 << LayerMask.NameToLayer("Obstacle"));
                //if (distanceTestHit.collider == null)
                //{
                //    m_Target = m_last_Target;
                //    GamePickUpsAmount.aiCurrentTarget = m_last_Target;
                //}
                #endregion
            }

            #endregion

            #region AI自动飞行
            if (m_Target != null)
            {
                // make the plane wander from the path, useful for making the AI seem more human, less robotic.
                Vector3 targetPos = m_Target.position +
                                    transform.right *
                                    (Mathf.PerlinNoise(Time.time * m_LateralWanderSpeed, m_RandomPerlin) * 2 - 1) *
                                    m_LateralWanderDistance;

                // adjust the yaw and pitch towards the target
                Vector3 localTarget = transform.InverseTransformPoint(targetPos);
                float targetAngleYaw = Mathf.Atan2(localTarget.x, localTarget.z);
                float targetAnglePitch = -Mathf.Atan2(localTarget.y, localTarget.z);


                // Set the target for the planes pitch, we check later that this has not passed the maximum threshold
                targetAnglePitch = Mathf.Clamp(targetAnglePitch, -m_MaxClimbAngle * Mathf.Deg2Rad,
                                               m_MaxClimbAngle * Mathf.Deg2Rad);

                // calculate the difference between current pitch and desired pitch
                float changePitch = targetAnglePitch - m_AeroplaneController.PitchAngle;

                // AI always applies gentle forward throttle
                const float throttleInput = 0.5f;

                // AI applies elevator control (pitch, rotation around x) to reach the target angle
                float pitchInput = changePitch * m_PitchSensitivity;

                // clamp the planes roll
                float desiredRoll = Mathf.Clamp(targetAngleYaw, -m_MaxRollAngle * Mathf.Deg2Rad, m_MaxRollAngle * Mathf.Deg2Rad);
                float yawInput = 0;
                float rollInput = 0;
                if (!m_TakenOff)
                {
                    // If the planes altitude is above m_TakeoffHeight we class this as taken off
                    if (m_AeroplaneController.Altitude > m_TakeoffHeight)
                    {
                        m_TakenOff = true;
                    }
                }
                else
                {
                    // now we have taken off to a safe height, we can use the rudder and ailerons to yaw and roll
                    yawInput = targetAngleYaw;
                    rollInput = -(m_AeroplaneController.RollAngle - desiredRoll) * m_RollSensitivity;
                }

                // adjust how fast the AI is changing the controls based on the speed. Faster speed = faster on the controls.
                float currentSpeedEffect = 1 + (m_AeroplaneController.ForwardSpeed * m_SpeedEffect);
                rollInput *= currentSpeedEffect;
                pitchInput *= currentSpeedEffect;
                yawInput *= currentSpeedEffect;

                // pass the current input to the plane (false = because AI never uses air brakes!)
                m_AeroplaneController.Move(rollInput, pitchInput, yawInput, throttleInput, false);
            }
            else
            {
                // no target set, send zeroed input to the planeW
                m_AeroplaneController.Move(0, 0, 0, 0, false);
            }
            #endregion
        }
        /// <summary>
        /// 在简单模式下，AI选择下一个要收集的光球的策略
        /// </summary>
        private void ChooseNextTarget_Easy()
        {
            //按照顺序选择下一个光球
            GamePickUpsAmount.currentCollectingPickUpIndex = 0;
            while (GamePickUpsAmount.isCurrentLevelPickUpsCollected[GamePickUpsAmount.currentCollectingPickUpIndex])
            {
                GamePickUpsAmount.currentCollectingPickUpIndex++;
            }
            Debug.Log("The AI next collecting sphere is: " + GamePickUpsAmount.currentCollectingPickUpIndex.ToString());
            m_Target = GamePickUpsAmount.currentLevelPickUps[GamePickUpsAmount.currentCollectingPickUpIndex].transform;
            GamePickUpsAmount.aiCurrentTarget = m_Target;
        }
        /// <summary>
        /// 在正常模式下，AI选择下一个要收集的光球的策略
        /// </summary>
        private void ChooseNextTarget_Normal()
        {
            //完全随机选择光球
            GamePickUpsAmount.currentCollectingPickUpIndex = Random.Range(0, pickUpsAmount);
            while (GamePickUpsAmount.isCurrentLevelPickUpsCollected[GamePickUpsAmount.currentCollectingPickUpIndex])
            {
                GamePickUpsAmount.currentCollectingPickUpIndex = Random.Range(0, pickUpsAmount);
            }
            Debug.Log("The AI next collecting sphere is: " + GamePickUpsAmount.currentCollectingPickUpIndex.ToString());
            m_Target = GamePickUpsAmount.currentLevelPickUps[GamePickUpsAmount.currentCollectingPickUpIndex].transform;
            GamePickUpsAmount.aiCurrentTarget = m_Target;
        }
        /// <summary>
        /// 在困难模式下，AI选择下一个要收集的光球的策略
        /// </summary>
        private void ChooseNextTarget_Hard()
        {
            airPlane2PickUp = new List<Ray>();
            airPlane2PickUpHit = new List<RaycastHit>();
            airPlane2PickUp.Clear();
            airPlane2PickUpHit.Clear();
            //GameObject.Find("IFTEXT").GetComponent<Text>().text = "";
            //for (int i = 0; i < pickUpsAmount; i++)
            //{
            //    GameObject.Find("IFTEXT").GetComponent<Text>().text = GameObject.Find("IFTEXT").GetComponent<Text>().text + 
            //        GamePickUpsAmount.isCurrentLevelPickUpsCollected[i].ToString() + "\n";

            //}
            //从飞机位置发射射线到所有未被收集的光球
            for (int i = 0; i < pickUpsAmount; i++)
            {
                if (!GamePickUpsAmount.isCurrentLevelPickUpsCollected[i])
                {
                    var ray = new Ray(transform.position, GamePickUpsAmount.currentLevelPickUps[i].transform.position - transform.position);
                    airPlane2PickUp.Add(ray);
                    var hit = new RaycastHit();
                    Physics.Raycast(ray, out hit, 900000, 1 << LayerMask.NameToLayer("PickUp")| 1 << LayerMask.NameToLayer("Obstacle"));
                    airPlane2PickUpHit.Add(hit);
                }
            }
            //计算当前帧，能够看见的光球并且计算最近的光球，以此作为收集目标，若没有能看见的光球则目标为null
            //GameObject.Find("HitTEXT").GetComponent<Text>().text = "";
            pickUp_AI_CanReach = new List<GameObject>(); ///此数组用于存放飞机能看到的光球
            for (int i = 0; i < airPlane2PickUp.Count; i++)
            {
                if (airPlane2PickUpHit[i].collider == null)
                {
                    continue;
                }
                //如果当前射线目标的物体不是光球，此时需要继续寻找下一个射线的碰撞体
                if (airPlane2PickUpHit[i].collider.gameObject.tag != Tags.PickUp)
                {
                    continue;
                }
                else  //从当前飞机位置发出的射线碰到了光球并且这个光球没有被收集
                {
                    pickUp_AI_CanReach.Add(airPlane2PickUpHit[i].collider.gameObject);//把这个光球放进一个动态数组中
                    //这个数组中存在两个或以上的元素（这样子才有可能在数组中有重复的元素）
                    if (pickUp_AI_CanReach.Count > 1)
                    {
                        //若飞机发出的射线所在直线上存在两个或者以上的光球，则将重复的光球从数组中移除
                        for (int j = 0; j < pickUp_AI_CanReach.Count - 2; j++)
                        {
                            if (airPlane2PickUpHit[i].collider.gameObject.name == pickUp_AI_CanReach[j].name)
                            {
                                pickUp_AI_CanReach.RemoveAt(pickUp_AI_CanReach.Count - 1);
                            }
                        }
                    }
                }
            }
            //GameObject.Find("AI_Can_ReachText").GetComponent<Text>().text = "能看到的：\n";
            //for (int i = 0; i < pickUp_AI_CanReach.Count; i++)
            //{
            //    GameObject.Find("AI_Can_ReachText").GetComponent<Text>().text = GameObject.Find("AI_Can_ReachText").GetComponent<Text>().text +
            //        pickUp_AI_CanReach[i].name + "\n";
            //}
            //当前没有能看见的光球，飞机保持飞行状态
            if (pickUp_AI_CanReach.Count == 0)
            {
                m_Target = null;
                GamePickUpsAmount.aiCurrentTarget = m_Target;
                Debug.Log("No Target!!!");
                return;
            }
            //当前有能看见的光球，飞机将这些光球中的距离最短的为目标
            else
            {
                var distance = new List<float>();
                //计算能看见的光球中，距离最短的光球的距离
                for (int i = 0; i < pickUp_AI_CanReach.Count; i++)
                {
                    distance.Add(Vector3.Distance(transform.position, pickUp_AI_CanReach[i].transform.position));
                }
                distance.Sort();
                for (int i = 0; i < pickUp_AI_CanReach.Count; i++)
                {
                    if (distance[0] == Vector3.Distance(transform.position, pickUp_AI_CanReach[i].transform.position))
                    {
                        m_Target = pickUp_AI_CanReach[i].transform;
                        GamePickUpsAmount.aiCurrentTarget = m_Target;
                        GamePickUpsAmount.currentCollectingPickUpIndex = i;
                        //GameObject.Find("NextTargetText").GetComponent<Text>().text = "The AI next collecting sphere is: " + m_Target.gameObject.name;
                        break;
                    }
                }
                return;
            }
        }
        /// <summary>
        ///上下左右前后八个方向随机选择一个方向作为飞行方向
        /// <summary>
        public void SetTargetRandom()
        {
            m_last_Target = m_Target;
            int direction = Random.Range(1, 7);
            switch (direction)
            {
                case 1:
                    randomTarget.transform.position = new Vector3(transform.position.x, transform.position.y + 1000, transform.position.z);
                    m_Target = randomTarget.transform;
                    GamePickUpsAmount.aiCurrentTarget = m_Target;
                    break;
                case 2:
                    randomTarget.transform.position = new Vector3(transform.position.x, transform.position.y - 1000, transform.position.z);
                    m_Target = randomTarget.transform;
                    GamePickUpsAmount.aiCurrentTarget = m_Target;
                    break;
                case 3:
                    randomTarget.transform.position = new Vector3(transform.position.x - 1000, transform.position.y, transform.position.z);
                    m_Target = randomTarget.transform;
                    GamePickUpsAmount.aiCurrentTarget = m_Target;
                    break;
                case 4:
                    randomTarget.transform.position = new Vector3(transform.position.x + 1000, transform.position.y, transform.position.z);
                    m_Target = randomTarget.transform;
                    GamePickUpsAmount.aiCurrentTarget = m_Target;
                    break;
                case 5:
                    randomTarget.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1000);
                    m_Target = randomTarget.transform;
                    GamePickUpsAmount.aiCurrentTarget = m_Target;
                    break;
                case 6:
                    randomTarget.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1000);
                    m_Target = randomTarget.transform;
                    GamePickUpsAmount.aiCurrentTarget = m_Target;
                    break;
                default:
                    break;
            }
        }
        private void RepeatCheckTarget()
        {
            if (GamePickUpsAmount.aiCurrentTarget == null)
            {
                ChooseNextTarget_Hard();
            }
        }
        private void ReSetTarget()
        {
            m_Target = m_last_Target;
            GamePickUpsAmount.aiCurrentTarget = m_last_Target;
            isRandomTarget = false;
        }
    }
}
