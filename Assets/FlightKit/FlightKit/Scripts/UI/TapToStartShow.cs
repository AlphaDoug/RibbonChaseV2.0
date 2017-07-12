using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TapToStartShow : MonoBehaviour
{
    private Image image;
    public float changeSpeed = 0.02f;
    public float minAlpha = 0;
    public float maxAlpha = 1;
    private bool isChangeUp = false;
	// Use this for initialization
	void Start ()
    {
        image = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (image.color.a >= maxAlpha)
        {
            isChangeUp = false;
        }
        if (image.color.a <= minAlpha)
        {
            isChangeUp = true;           
        }
        if (isChangeUp)
        {
            image.color = new Color(255, 255, 255, image.color.a + changeSpeed);
        }
        else
            image.color = new Color(255, 255, 255, image.color.a - changeSpeed);
    }
}
