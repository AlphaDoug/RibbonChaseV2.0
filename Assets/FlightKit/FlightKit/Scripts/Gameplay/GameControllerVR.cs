using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using FlightKit;

public class GameControllerVR : MonoBehaviour
{

    public static int AirPlaneController = 1;
    public bool isGameOver;
    public GameObject[] disActive;
    public GameObject[] setActive;
    public GameObject progressTracker;
    FlightKit.GameProgressTracker processTracker;

    void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
    void Start()
    {
        if (progressTracker && progressTracker.GetComponent<GameProgressTracker>())
        {
            processTracker = progressTracker.GetComponent<GameProgressTracker>();
        }
        else
        {
            Debug.Log("Can not find GameProgessTracker.");
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
        AirPlaneController = 0;
    }

    public void ChoseJoyStick()
    {
        AirPlaneController = 1;
    }

    public int ReturnController()
    {
        return AirPlaneController;
    }


}
