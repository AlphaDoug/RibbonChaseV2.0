using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveImage : MonoBehaviour
{
    public List<GameObject> image;
    private List<float> imageActiveTime = new List<float>();
    private float pauseStartTime;
    private List<bool> imageActive = new List<bool>();
    void Start ()
    {
        pauseStartTime = Time.realtimeSinceStartup;
        for (int i = 0; i < image.Count; i++)
        {
            imageActiveTime.Add(Random.Range(0, 20.0f));
            imageActive.Add(false);
            //Invoke("StartMove_" + (i + 1).ToString(), Random.Range(0, 20.0f));
        }

        


    }
    private void Update()
    {
        for (int i = 0; i < imageActiveTime.Count; i++)
        {
            if (Time.realtimeSinceStartup - pauseStartTime >= imageActiveTime[i])
            {
                StartMove(i);
            }
        }
        

    }
    private void StartMove(int index)
    {
        if (!imageActive[index])
        {
            image[index].SetActive(true);
            imageActive[index] = true;
        }
       
    }
    
}

  

