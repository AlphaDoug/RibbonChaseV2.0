using UnityEngine;
using System.Collections;
using FlightKit;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingRotate : MonoBehaviour {
    public GameObject levelScripts;
    //public GameObject ButtonDown;
    public GameObject GameController;
    /// <summary>
    /// The scene index which will be loaded next time.
    /// </summary>
    public int loadingSceneIndex;
    private GameProgressTracker gameProgressTracker;
    //private ButtonDown buttonDown;
    private GameControllerVR gameController;
    private int degree=0;
    private AsyncOperation async;
    private string loadingSceneString= "GamePlay_";
  
    // Use this for initialization
    void Start ()
    {
       
        gameProgressTracker = levelScripts.GetComponent<GameProgressTracker>();
       
        gameController = GameController.GetComponent<GameControllerVR>();
        if (loadingSceneIndex!=0)
        {
            loadingSceneString = loadingSceneString + loadingSceneIndex.ToString();
        }
        else
        {
            loadingSceneString = "MainMenu";
        }
        //在这里开启一个异步任务，
        //进入loadScene方法。
        StartCoroutine(loadScene8());
	}
	
	// Update is called once per frame
	void Update ()
    {           
        transform.RotateAround(transform.position,transform.forward,-4f);
    }
    void NextLevel()
    {
        gameProgressTracker.LevelCompleted();

    }
    void RandomSeed()
    {
        if (degree == 0)
        {
            degree = Random.Range(2, 100);
        }
       
    }
    IEnumerator loadScene8()
    {
        //异步读取场景。
       
        async = SceneManager.LoadSceneAsync(loadingSceneIndex);
      
        //读取完毕后返回， 系统会自动进入C场景
        yield return async;
    }
}
