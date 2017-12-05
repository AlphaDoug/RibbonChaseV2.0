using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 教学控制脚本
/// </summary>
public class GameTeaching : MonoBehaviour
{
    public GameObject controllTeaching_Joystick;
    public GameObject controllTeaching_Gravity;
    public GameObject speedUpTeaching;
    public GameObject pickUps;
    public GameObject good;
    // Use this for initialization
    void Start ()
    {
        //五秒之后显示教学
        Invoke("ControllTeachingAble", 5.0f);
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Time.timeSinceLevelLoad;
	}
    private void ControllTeachingAble()
    {
        //Time.timeScale = 0;
        if (PlayerPrefs.GetInt("AirPlaneController") == 0)
        {
            controllTeaching_Joystick.SetActive(true);
            Invoke("ControllTeachingDisable_Joystick", 3.0f);
        }
        if (PlayerPrefs.GetInt("AirPlaneController") == 1)
        {
            controllTeaching_Gravity.SetActive(true);
            Invoke("ControllTeachingDisable_Gravity", 3.0f);
        }
    }
    private void ControllTeachingDisable_Joystick()
    {
        // Time.timeScale = 1;
        controllTeaching_Joystick.SetActive(false);
        Invoke("SpeedUpTeachingAble", 10.0f);
    }
    private void ControllTeachingDisable_Gravity()
    {
        // Time.timeScale = 1;
        controllTeaching_Gravity.SetActive(false);
        Invoke("SpeedUpTeachingAble", 10.0f);
    }
    private void SpeedUpTeachingAble()
    {
        speedUpTeaching.SetActive(true);
        Invoke("SpeedUpTeachingDisable", 3.0f);
    }
    private void SpeedUpTeachingDisable()
    {
        speedUpTeaching.SetActive(false);
        Invoke("GoodAble", 3.0f);
    }
    private void GoodAble()
    {
        good.SetActive(true);
        Invoke("GoodDisable", 3.0f);
    }
    private void GoodDisable()
    {
        good.SetActive(false);
    }
}
