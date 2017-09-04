using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectNew : MonoBehaviour
{
    public GameObject adsUI;
    public GameObject allLevels;
    public GameObject levelSel;
    public RectTransform mainPosition;
    public GameObject nextPage;
    public GameObject lastPage;
    public int maxPagesAmount = 6;

    public List<GameObject> loadingScenes = new List<GameObject>();
    public List<Toggle> toggle = new List<Toggle>();
    public int screenWidth;
    public int screenHeight;
    private int currentPage = 1;
    private bool isMovingToNextPage = false;
    private bool isMovingToLastPage = false;
    private bool isMouseDown = false;
    private float mouseDownPosition_x = 0;
    private Vector3 allLevelsMouseDownPosition = new Vector3();
    private float allLevelsMove_x = 0;
    private ToggleGroup toggleGroup;
    private float speedPerFrame = 0f;
    private float a;
    // Use this for initialization
    void Start()
    {
        if (Application.loadedLevel == 0)
        {
            a = 50;
        }
        else
        {
            a = 5;
        }
        screenHeight = Screen.height;
        screenWidth = Screen.width;

        mainPosition.sizeDelta = new Vector2((screenWidth / 1920) * 1280, (screenHeight / 1080) * 1280);
    }



    // Update is called once per frame
    void Update()
    {
        MoveOnPause();

        #region 控制左右按钮可用性
        if (currentPage >= maxPagesAmount && nextPage.active)
        {
            nextPage.SetActive(false);
        }
        if (currentPage < maxPagesAmount && !nextPage.active)
        {
            nextPage.SetActive(true);
        }
        if (currentPage <= 1 && lastPage.active)
        {
            lastPage.SetActive(false);
        }
        if (currentPage > 1 && !lastPage.active)
        {
            lastPage.SetActive(true);
        }
        #endregion

        #region 控制移动到指定位置
        if (isMovingToNextPage)//正在向下一页移动
        {
            if (allLevels.GetComponent<RectTransform>().localPosition.x < -1920 * (currentPage - 1))//已经移动过头了
            {
                speedPerFrame = (-1920 * (currentPage - 1) - allLevels.GetComponent<RectTransform>().localPosition.x) / a;
                isMovingToNextPage = false;
                isMovingToLastPage = true;
            }
        }
        if (isMovingToLastPage)//正在向上一页移动
        {
            if (allLevels.GetComponent<RectTransform>().localPosition.x > -1920 * (currentPage - 1))//已经移动过头了
            {
                speedPerFrame = (-1920 * (currentPage - 1) - allLevels.GetComponent<RectTransform>().localPosition.x) / a;
                isMovingToLastPage = false;
                isMovingToNextPage = true;
            }
        }
        #endregion

        #region 控制页面随手指滑动
        if (Input.GetMouseButtonDown(0))//鼠标左键按下
        {
            if (Input.mousePosition.x > -mainPosition.sizeDelta.x / 2 + screenWidth / 2 && Input.mousePosition.x < mainPosition.sizeDelta.x / 2 + screenWidth / 2 &&
                       Input.mousePosition.y > -mainPosition.sizeDelta.y / 2 + screenHeight / 2 && Input.mousePosition.y < mainPosition.sizeDelta.y / 2 + screenHeight / 2)
            {
                isMouseDown = true;
                allLevelsMouseDownPosition = allLevels.GetComponent<RectTransform>().localPosition;
                mouseDownPosition_x = Input.mousePosition.x;
            }
        }
        if (Input.GetMouseButtonUp(0) && isMouseDown)
        {
            isMouseDown = false;
            if (loadingScenes.Count != 0)//表示存在需要点击激活的界面
            {
                if (Input.mousePosition.x - mouseDownPosition_x > 100)
                {
                    if (currentPage > 1)
                    {
                        Move2LastPage();///////////
                    }
                    if (currentPage == 1)
                    {
                        currentPage = 0;
                        Move2NextPage();//////////
                    }
                }
                else if (Input.mousePosition.x - mouseDownPosition_x < -100)
                {
                    if (currentPage < maxPagesAmount)
                    {
                        Move2NextPage();/////////////
                    }
                    if (currentPage == maxPagesAmount)
                    {
                        currentPage = maxPagesAmount + 1;
                        Move2LastPage();/////////////////////
                    }
                }
                else//点击某个界面
                {
                    #region 鼠标点击事件
                    if (Input.mousePosition.x > -mainPosition.sizeDelta.x / 2 + screenWidth / 2 && Input.mousePosition.x < mainPosition.sizeDelta.x / 2 + screenWidth / 2 &&
                        Input.mousePosition.y > -mainPosition.sizeDelta.y / 2 + screenHeight / 2 && Input.mousePosition.y < mainPosition.sizeDelta.y / 2 + screenHeight / 2)
                    {
                        //if (Application.loadedLevel == 0)//如果是mainmenu
                        //{
                        //    if (GameObject.Find("EndTheGameBox") != null)
                        //    {

                        //    }
                        //}
                        //else
                        //{
                        LoadLevel(currentPage);
                        //}

                    }

                    #endregion
                }
            }
            else//不存在点击激活的界面
            {
                if (Input.mousePosition.x - mouseDownPosition_x > 0)
                {
                    if (currentPage > 1)
                    {
                        Move2LastPage();/////////////
                    }
                    if (currentPage == 1)
                    {
                        currentPage = 0;
                        Move2NextPage();////////////
                    }
                }
                else if (Input.mousePosition.x - mouseDownPosition_x < 0)
                {
                    if (currentPage < maxPagesAmount)
                    {
                        Move2NextPage();//////////
                    }
                    if (currentPage == maxPagesAmount)
                    {
                        currentPage = maxPagesAmount + 1;
                        Move2LastPage();//////////
                    }
                }
            }
        }
        if (isMouseDown)
        {
            allLevelsMove_x = Input.mousePosition.x - mouseDownPosition_x;
            allLevels.GetComponent<RectTransform>().localPosition = allLevelsMouseDownPosition + new Vector3(allLevelsMove_x, 0, 0);
        }
        #endregion


    }

    public void Move2NextPage()
    {
        if (Application.loadedLevel == 0)
        {
            speedPerFrame = -300;
        }
        else
        {
            speedPerFrame = -500;
        }
        isMovingToNextPage = true;
        isMovingToLastPage = false;
        currentPage++;
        toggle[currentPage - 1].isOn = true;
    }

    public void Move2LastPage()
    {
        if (Application.loadedLevel == 0)
        {
            speedPerFrame = 300;
        }
        else
        {
            speedPerFrame = 500;
        }
        isMovingToLastPage = true;
        isMovingToNextPage = false;
        currentPage--;
        toggle[currentPage - 1].isOn = true;
    }

    private void LoadLevel(int index)
    {
        if (LifeNumCtrl.lifeNum <= 0)
        {
            adsUI.SetActive(true);
        }
        else
        {
            if (index < 2)
            {
                levelSel.SetActive(false);
                loadingScenes[index - 1].SetActive(true);
            }
            else if (PlayerPrefs.GetInt("level_" + (index - 2)) == 1)
            {
                levelSel.SetActive(false);
                loadingScenes[index - 1].SetActive(true);
            }
        }
    }

    private void MoveOnPause()
    {
        allLevels.GetComponent<RectTransform>().Translate(new Vector3(speedPerFrame, 0, 0));
    }
}
