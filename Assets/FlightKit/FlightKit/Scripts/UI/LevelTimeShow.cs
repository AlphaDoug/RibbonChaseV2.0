using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LevelTimeShow : MonoBehaviour {
    public Text timeText;
    private string currentTime_Seconds;
    private string currentTime_Minutes;
    // Use this for initialization
    void Start ()
    {
        currentTime_Minutes = "00";
        currentTime_Seconds = "00";
        timeText.text = currentTime_Minutes + ":" + currentTime_Seconds;
        FlightKit.GamePickUpsAmount.time = "00:00";
	}
	
	// Update is called once per frame
	void Update ()
    {
        currentTime_Minutes = ((int)(Time.timeSinceLevelLoad / 60)).ToString();
        currentTime_Seconds = ((int)(Time.timeSinceLevelLoad % 60)).ToString();
        if (int.Parse(currentTime_Minutes) < 10)
        {
            currentTime_Minutes = "0" + currentTime_Minutes;
        }
        if (int.Parse(currentTime_Minutes) == 0)
        {
            currentTime_Minutes = "00";
        }
        if (int.Parse(currentTime_Seconds) < 10)
        {
            currentTime_Seconds = "0" + currentTime_Seconds;
        }
        if (int.Parse(currentTime_Seconds) == 0)
        {
            currentTime_Seconds = "00";
        }       
        timeText.text = currentTime_Minutes + ":" + currentTime_Seconds;
        FlightKit.GamePickUpsAmount.time = currentTime_Minutes + ":" + currentTime_Seconds;
    }
}
