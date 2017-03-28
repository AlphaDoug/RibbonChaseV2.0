using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    public float[] pageArray;   //,0.3792f,0.5054f,0.6303f,0.7558f,0.8821f{ 0.0004f, 0.1289f, 0.2537f }
    public Toggle[] toggleArray;
    public GameObject[] toggle;
    public GameObject lastBtn;
    public GameObject nextBtn;
    

    private ScrollRect scrollRect; 
    private float targetPosition;
    private float smoothing=4;
    private bool isDraging;
    private int currentPage;
   
    
    void Start()
    {
        scrollRect = GetComponent<ScrollRect>();
        targetPosition = pageArray[0];
        currentPage = 1;
        
        
    }
    void Update()
    {
        if (!isDraging)
        {
            scrollRect.horizontalNormalizedPosition = 
                Mathf.Lerp(scrollRect.horizontalNormalizedPosition, targetPosition, 0.04f * smoothing);         
        }
        if (targetPosition == pageArray[0])
        {
            lastBtn.SetActive(false);
        }
        else
        {
            lastBtn.SetActive(true);
        }
        if (targetPosition == pageArray[pageArray.Length-1])
        {
            nextBtn.SetActive(false);
        }
        else
        {
            nextBtn.SetActive(true);
        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        isDraging = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDraging = false;
        float posX = scrollRect.horizontalNormalizedPosition;
        Debug.Log("posX:" + posX);
        int index = 0;
        float offset = Mathf.Abs(posX - pageArray[0]);
        for (int i = 1; i < pageArray.Length; i++)
        {
            float offsetTemp = Mathf.Abs(posX - pageArray[i]);
            if (offsetTemp < offset)
            {
                index = i;
                offset = offsetTemp;
            }
        }
        targetPosition = pageArray[index];
        toggleArray[index].isOn = true;

    }

    #region Move2Page?
    public void Move2Page1(bool isOn)
    {
        if (isOn)
        {
            targetPosition = pageArray[0];
            currentPage = 1;
        }
    }
    public void Move2Page2(bool isOn)
    {
        if (isOn)
        {
            targetPosition = pageArray[1];
            currentPage = 2;
        }
    }
    public void Move2Page3(bool isOn)
    {
        if (isOn)
        {
            targetPosition = pageArray[2];
            currentPage = 3;
        }
    }
    public void Move2Page4(bool isOn)
    {
        if (isOn)
        {
            targetPosition = pageArray[3];
            currentPage = 4;
        }
    }
    public void Move2Page5(bool isOn)
    {
        if (isOn)
        {
            targetPosition = pageArray[4];
            currentPage = 5;
        }
    }
    public void Move2Page6(bool isOn)
    {
        if (isOn)
        {
            targetPosition = pageArray[5];
            currentPage = 6;
        }
    }
    public void Move2Page7(bool isOn)
    {
        if (isOn)
        {
            targetPosition = pageArray[6];
            currentPage = 7;
        }
    }
    public void Move2Page8(bool isOn)
    {
        if (isOn)
        {
            targetPosition = pageArray[7];
            currentPage = 8;
        }
    }
    public void Move2Page9(bool isOn)
    {
        if (isOn)
        {
            targetPosition = pageArray[8];
            currentPage = 9;
        }
    }
    public void Move2Page10(bool isOn)
    {
        if (isOn)
        {
            targetPosition = pageArray[9];
            currentPage = 10;
        }
    }
    #endregion
    public void Move2NextPage(bool isOn)
    {
        toggle[currentPage].GetComponent<Toggle>().isOn = true;
    }
    public void Move2LastPage(bool isOn)
    {
        currentPage -= 2;
        toggle[currentPage].GetComponent<Toggle>().isOn = true;
    }
}
