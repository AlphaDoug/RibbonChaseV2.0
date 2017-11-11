using UnityEngine;
using UnityEngine.SceneManagement;
using FlightKit;
public class GameController : MonoBehaviour
{
	public int adsAddLifeNum = 1;
    public GameObject warningText;
	public GameObject UICanvas;
    //public static int airPlaneController = 1;
    public GameObject endTheGameBox;
    public bool isGameOver;
    public GameObject []disActive;
    public GameObject[] setActive;
    public GameObject progressTracker;
    public GameObject[] levelLock;
    private int levelNum = 7;
    FlightKit.GameProgressTracker processTracker;
    float t1, t2;
    public static int airPlaneController;
    void Awake()
    {
        //PlayerPrefs.DeleteAll();
        Screen.sleepTimeout = SleepTimeout.NeverSleep; 
        Time.timeScale = 1f;
        //PlayerPrefs.DeleteAll();
        if (Application.loadedLevel == 0)
        {
            //Initail lock
            //PlayerPrefs.SetInt("level_" + 0, 1);
            for (int i = 1; i < levelNum; i++)
            {
                if (!PlayerPrefs.HasKey("level_" + i))
                {
                    PlayerPrefs.SetInt("level_" + i, 0);
                }
            }

            for (int i = 0; i < levelLock.Length; i++)
            {
                if (PlayerPrefs.HasKey("level_" + i))
                {
                    if (PlayerPrefs.GetInt("level_" + i) == 1)
                    {
                        levelLock[i].SetActive(false);
                    }

                }
            }
        }
        if (progressTracker && progressTracker.GetComponent<GameProgressTracker>())
        {
            processTracker = progressTracker.GetComponent<GameProgressTracker>();
        }
        else
        {
            Debug.Log("Can not find GameProgessTracker.");
        }

    }
    private void Start()
    {
        //预加载广告
        //TGSDK.Initialize("469604ox8m553x9LJrLa");
        //TGSDK.PreloadAd();
        ADFlyHiSDK.OnAdShowSuccess = OnAdShowSuccess;
        ADFlyHiSDK.OnAdShowFail = OnAdShowFail;

        ADFlyHiSDK.init("a100170000", "a100170001", ADFlyHiSDK.Oritation.Landscape, false);
        ADFlyHiSDK.PreloadVideo();

    }
    private void OnAdShowSuccess()
	{
		Debug.Log("广告播放成功");
		GetComponent<LifeNumCtrl> ().AdsAddLife (adsAddLifeNum);
        //if (LifeNumCtrl.lifeNum<  LifeNumCtrl.MAXLIFENUM)
		if(LifeNumCtrl.lifeNum<LifeNumCtrl.MAXLIFENUM)
		{
			GameObject warning = Instantiate(warningText) as GameObject;
			warning.GetComponent<Animator>().updateMode = AnimatorUpdateMode.UnscaledTime;
			warning.transform.SetParent(UICanvas.transform);
			warning.GetComponent<RectTransform>().localPosition = new Vector3(-23, 864.9999f, 0);
			warning.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
			warning.GetComponent<RectTransform>().localRotation = new Quaternion(0, 0, 0, 0);
		}
           
        
        
	}

	private void OnAdShowFail(string s)
	{
		Debug.Log("广告播放失败，失败描述：" + s);

	}
    public void DisActiveUI()
    {
        for (int i = 0; i < disActive.Length; i++)
        {
            disActive[i].SetActive(false);
        }
    }
    public void DisActiveButtonOnOver()
    {
        if (processTracker)
        {
            if (processTracker.GetLevelFinished())
            {

                for (int i = 0; i < disActive.Length; i++)
                {
                    disActive[i].SetActive(false);
                }
            }
        }
        else
        {
            Debug.Log("processTracker is unsigned");
        }
    }
    public void SetActiveButtonOnOver()
    {
        if (processTracker)
        {
            if (processTracker.GetLevelFinished())
            {
                for (int i = 0; i < setActive.Length; i++)
                {
                    setActive[i].SetActive(true);
                }
            }
        }
        else
        {
            Debug.Log("processTracker is unsigned");
        }

    }
    void Update()
    {
        //退出游戏提示框，并暂停游戏
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!(GameObject.Find("DramaALL") || GameObject.Find("DramaFRONT") || GameObject.Find("DramaEND")))
            {
                endTheGameBox.SetActive(true);
                for (int i = 0; i < disActive.Length; i++)
                {
                    disActive[i].SetActive(false);
                }
                if (Application.loadedLevel == 0 || GameObject.Find("TipMenu") != null)
                {
                    if (GameObject.FindObjectOfType<LevelSelectNew>() != null)
                    {
                        GameObject.FindObjectOfType<LevelSelectNew>().GetComponent<LevelSelectNew>().enabled = false;
                    }

                }
                Time.timeScale = 0;
            }
            
        }
    }

    public void EndBoxDisable()
    {
        if (GameObject.Find("PauseMenu") != null)//表示当前界面是游戏暂停界面
        {
            endTheGameBox.SetActive(false);
        }
        else
        {
            endTheGameBox.SetActive(false);
            for (int i = 0; i < disActive.Length; i++)
            {
                disActive[i].SetActive(true);
            }
            Time.timeScale = 1;
        }
        
    }
    public void StartLevel_0()
    {
        //Application.LoadLevel(0);
        SceneManager.LoadScene(0);
        //Time.timeScale = 1;
    }

    public void StartLevel_1()
    {
        //Application.LoadLevel(1);
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }

    public void StartLevel_2()
    {
        //Application.LoadLevel(2);
        SceneManager.LoadScene(2);
        Time.timeScale = 1;
    }
    public void StartLevel_3()
    {
        //Application.LoadLevel(3);
        SceneManager.LoadScene(3);
        Time.timeScale = 1;
    }

    public void StartLevel_4()
    {
       // Application.LoadLevel(4);
        SceneManager.LoadScene(4);
        Time.timeScale = 1;
    }

    public void StartLevel_5()
    {
        //Application.LoadLevel(5);
        SceneManager.LoadScene(5);
        Time.timeScale = 1;
    }

    public void StartLevel_6()
    {
        //Application.LoadLevel(5);
        SceneManager.LoadScene(6);
        Time.timeScale = 1;
    }
    public void StartLevel_7()
    {
        //Application.LoadLevel(5);
        SceneManager.LoadScene(7);
        Time.timeScale = 1;
    }
    public void StartLevel_8()
    {
        //Application.LoadLevel(5);
        SceneManager.LoadScene(8);
        Time.timeScale = 1;
    }
    public void StartLevel_9()
    {
        //Application.LoadLevel(5);
        SceneManager.LoadScene(9);
        Time.timeScale = 1;
    }
    public void StartLevel_10()
    {
        //Application.LoadLevel(5);
        SceneManager.LoadScene(10);
        Time.timeScale = 1;
    }
    public void LoadLevel(int index)
    {
        SceneManager.LoadScene(index);
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void GoOnGame()
    {
        Time.timeScale = 1;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ChoseGravity()
    {
        PlayerPrefs.SetInt("AirPlaneController", 1);
        Debug.Log("GravityController");
        airPlaneController =0;      
    }

    public void ChoseJoyStick()
    {
        PlayerPrefs.SetInt("AirPlaneController", 0);
        airPlaneController = 1;
    }

    public int ReturnController()
    {
        return PlayerPrefs.GetInt("AirPlaneController");
        //return airPlaneController;
    }
}
