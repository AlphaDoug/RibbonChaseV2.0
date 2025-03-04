﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/// <summary>
/// 控制关卡结束之后通关界面
/// </summary>
public class LevelCompletedControl : MonoBehaviour
{

    public GameObject theFirstStar;
    public GameObject theSecondStar;
    public GameObject theThirdStar;
    public GameObject successWord;
    public GameObject returnButton;
    public GameObject retryButton;
    public GameObject nextButton;
    public GameObject airPlane;
	public GameObject aboutGame;

    private CanvasRenderer successCanvasRenderer;
    private CanvasRenderer theFirstStarCanvasRenderer;
    private CanvasRenderer theSecondStarCanvasRenderer;
    private CanvasRenderer theThirdStarCanvasRenderer;
    private CanvasRenderer returnButtonCanvasRenderer;
    private CanvasRenderer retryButtonCanvasRenderer;
    private CanvasRenderer nextButtonCanvasRenderer;
    private bool isShowReturn = false;
    private bool isShowRetry = false;
    private bool isShowNext = false;
    private float successAlpha = 0.01f;
    private float buttonAlpha1 = 0.01f;
    private float buttonAlpha2 = 0.01f;
    private float buttonAlpha3 = 0.01f;
    private int loadedLevel;

    private Text successTime;
    private float realTime;
    public float oneStarTime;
    public float twoStarTime;
    public float threeStarTime;

    private int starNum=0;
    // Use this for initialization
    void Start()
    {
        successTime = GameObject.Find("RealTime").GetComponent<Text>();
        if (successTime == null)
        {
            Debug.LogError("没有找到RealTime");
        }
       
        loadedLevel = Application.loadedLevel;
        airPlane.SetActive(false);

        successCanvasRenderer = successWord.GetComponent<CanvasRenderer>();

        theFirstStarCanvasRenderer = theFirstStar.GetComponent<CanvasRenderer>();
        theSecondStarCanvasRenderer = theSecondStar.GetComponent<CanvasRenderer>();
        theThirdStarCanvasRenderer = theThirdStar.GetComponent<CanvasRenderer>();

        returnButtonCanvasRenderer = returnButton.GetComponent<CanvasRenderer>();
        retryButtonCanvasRenderer = retryButton.GetComponent<CanvasRenderer>();

        successTime.text = FlightKit.GamePickUpsAmount.time;
        realTime = Time.timeSinceLevelLoad;

        successCanvasRenderer.SetAlpha(0);
        theFirstStarCanvasRenderer.SetAlpha(0);
        theSecondStarCanvasRenderer.SetAlpha(0);
        theThirdStarCanvasRenderer.SetAlpha(0);

        //returnButtonCanvasRenderer.SetAlpha(0);
        //if (loadedLevel!=6)
        //{
        //    nextButtonCanvasRenderer = nextButton.GetComponent<CanvasRenderer>();
        //    nextButtonCanvasRenderer.SetAlpha(0);
        //}
        //retryButtonCanvasRenderer.SetAlpha(0);
        
        //GameObject.Find("RealTime").guiText = successTime;
        if (realTime < threeStarTime)
        {
            Invoke("ShowFirstStar", 1f);
            Invoke("ShowSecondStar", 1.5f);
            Invoke("ShowThirdStar", 2.2f);
            starNum = 3;      
        }
        else if (realTime < twoStarTime && realTime>threeStarTime)
        {
            Invoke("ShowFirstStar", 1f);
            Invoke("ShowSecondStar", 1.5f);
            starNum = 2;
        }
        else
        {
            Invoke("ShowFirstStar", 1f);
            starNum = 1;
        }

        if (PlayerPrefs.HasKey("LevelStarNum_" + Application.loadedLevel))
        {
            if(starNum>PlayerPrefs.GetInt("LevelStarNum_" +  Application.loadedLevel))
            {
                PlayerPrefs.SetInt("LevelStarNum_" + Application.loadedLevel, starNum);
            }
        }
        else
        {
            PlayerPrefs.SetInt("LevelStarNum_" + Application.loadedLevel, starNum);
        }
        Invoke("ShowReturnButton", 2.5f);
        Invoke("ShowRetryButton", 2.7f);
        Invoke("ShowNextButton", 2.9f);
		//Invoke("ShowAboutGame", 2.7f);

    }

    // Update is called once per frame
    void Update()
    {
        if (successCanvasRenderer.GetAlpha() <= 1)
        {
            successAlpha = successAlpha * 1.5f;
            successCanvasRenderer.SetAlpha(successAlpha);
        }
        if (isShowRetry)
        {
            //buttonAlpha1 = buttonAlpha1 + 0.02f;
            //retryButtonCanvasRenderer.SetAlpha(buttonAlpha1);
            retryButton.SetActive(true);
        }
        if (isShowNext && loadedLevel != 7)
        {
            //buttonAlpha2 = buttonAlpha2 + 0.02f;
            //if (nextButtonCanvasRenderer != null)
            //{
            //    nextButtonCanvasRenderer.SetAlpha(buttonAlpha2);
            //}
            nextButton.SetActive(true);
        }
        if (isShowReturn)
        {
            //buttonAlpha3 = buttonAlpha3 + 0.02f;
            //returnButtonCanvasRenderer.SetAlpha(buttonAlpha3);
            returnButton.SetActive(true);
        }

    }
	private void ShowAboutGame()
	{
		if (Application.loadedLevel == (Application.levelCount-1))
		{
			aboutGame.SetActive (true);

		}
	}
    private void ShowFirstStar()
    {
        theFirstStar.GetComponent<AudioSource>().Play();
        float theFirstStarAlpha = 0.01f;
        while (theFirstStarCanvasRenderer.GetAlpha() <= 1)
        {
            theFirstStarAlpha = theFirstStarAlpha * 4f;
            theFirstStarCanvasRenderer.SetAlpha(successAlpha);
        }
    }
    private void ShowSecondStar()
    {
        theFirstStar.GetComponent<AudioSource>().Play();
        float theSecondStarAlpha = 0.01f;
        while (theSecondStarCanvasRenderer.GetAlpha() <= 1)
        {
            theSecondStarAlpha = successAlpha * 4f;
            theSecondStarCanvasRenderer.SetAlpha(successAlpha);
        }
    }
    private void ShowThirdStar()
    {
        theThirdStar.GetComponent<AudioSource>().Play();
        float theThirdStarAlpha = 0.01f;
        while (theThirdStarCanvasRenderer.GetAlpha() <= 1)
        {
            theThirdStarAlpha = successAlpha * 4f;
            theThirdStarCanvasRenderer.SetAlpha(successAlpha);
        }
    }
    private void ShowReturnButton()
    {
        isShowReturn = true;
    }
    private void ShowRetryButton()
    {
        isShowRetry = true;
    }
    private void ShowNextButton()
    {
        isShowNext = true;
    }
}
