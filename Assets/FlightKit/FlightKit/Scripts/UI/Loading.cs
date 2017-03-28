using UnityEngine;
using System.Collections;

public class Loading : MonoBehaviour
{
    private string loadingScene;
    private float fps = 10.0f;
    private float time;
    private int nowFram;
    //异步对象
    AsyncOperation async;

    //读取场景的进度，它的取值范围在0 - 1 之间。
    int progress = 0;

    void Start()
    {
        //在这里开启一个异步任务，
        //进入loadScene方法。
        StartCoroutine(loadScene());
    }

    //注意这里返回值一定是 IEnumerator
    IEnumerator loadScene()
    {
        //异步读取场景。
        
        async = Application.LoadLevelAsync("Gameplay_8");

        //读取完毕后返回， 系统会自动进入C场景
        yield return async;
    }
    void Update()
    {

        
        progress = (int)(async.progress * 100);
        //有了读取进度的数值，大家可以自行制作进度条啦。
        Debug.Log("xuanyusong" + progress);
    }
   
   
}