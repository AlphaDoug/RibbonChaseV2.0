using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControlsMenuController : MonoBehaviour
{
    public GameObject soundToggle;
    public GameObject modelToggle;
    public GameObject reviseToggle;
    public GameObject vibrationToggle;
    public GameObject sensitivityToggle;
    public GameObject gameController;
    public GameObject sliderBar;

    private Vector3 mouseDownPosition;
    private Vector3 mouseDownContentPosition;
    private bool isMouseDown = false;
    private Slider soundToggleSlider;
    private Slider modelToggleSlider;
    private Slider reviseToggleSlider;
    private Slider vibrationToggleSlider;
    private Slider sensitivityToggleSlider;
    private Dropdown dropDown;
    public int language = 0;
    //语言选择
    public enum Language
    {
        Chinese = 0,
        English = 1,
    };

    // Use this for initialization
    void Start()
    {
        //dropDown = GameObject.Find("LanguageChoose").GetComponent<Dropdown>();
        //musicToggleSlider = musicToggle.GetComponent<Slider>();
        soundToggleSlider = soundToggle.GetComponent<Slider>();
        modelToggleSlider = modelToggle.GetComponent<Slider>();
        reviseToggleSlider = reviseToggle.GetComponent<Slider>();
        vibrationToggleSlider = vibrationToggle.GetComponent<Slider>();
        sensitivityToggleSlider = sensitivityToggle.GetComponent<Slider>();
        language = (int)Language.English;

        //初始化语言
        if (PlayerPrefs.GetInt("language") == null)
        {
            PlayerPrefs.SetInt("language", 1);
        }
        //初始化模式控制
        if (PlayerPrefs.GetInt("AirPlaneController") == 0|| PlayerPrefs.GetInt("AirPlaneController") == null)
        {
            modelToggleSlider.value= 0;
        }
        else
        {
            modelToggleSlider.value = 1;
        }
        //初始化音乐开关
        if (PlayerPrefs.HasKey("MusicOn"))
        {
            soundToggleSlider.value = PlayerPrefs.GetInt("MusicOn");
            AudioListener.volume = PlayerPrefs.GetInt("MusicOn");
        }
        else
        {
            PlayerPrefs.SetInt("MusicOn", 1);
            soundToggleSlider.value = PlayerPrefs.GetInt("MusicOn");
            AudioListener.volume = PlayerPrefs.GetInt("MusicOn");
        }
        //初始化震动设置
        if (PlayerPrefs.HasKey("VibrationOn"))
        {
            vibrationToggleSlider.value = PlayerPrefs.GetInt("VibrationOn");

        }
        else
        {
            PlayerPrefs.SetInt("VibrationOn", 0);
            vibrationToggleSlider.value = PlayerPrefs.GetInt("VibrationOn");

        }
        //初始化灵敏度,范围是1-5
        if (PlayerPrefs.HasKey("SensitivityOn"))
        {
            sensitivityToggleSlider.value = (PlayerPrefs.GetFloat("SensitivityOn") - 1) / 4;

        }
        else
        {
            PlayerPrefs.SetFloat("SensitivityOn", 3.0f);
            sensitivityToggleSlider.value = (PlayerPrefs.GetFloat("SensitivityOn") - 1) / 4;

        }
        //初始化方向控制
        if (PlayerPrefs.GetInt("ReviseDirection") == 0)
        {
            reviseToggleSlider.value = 1;
        }
        else
        {
            reviseToggleSlider.value = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isMouseDown = true;
            mouseDownPosition = Input.mousePosition;
            mouseDownContentPosition = sliderBar.transform.localPosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            isMouseDown = false;
        }

        if (isMouseDown)//鼠标OR手指按下,停止自动滑动,跟随手指移动
        {
            sliderBar.transform.localPosition = new Vector3(
                0,
                Mathf.Clamp(Input.mousePosition.y - mouseDownPosition.y + mouseDownContentPosition.y, -140, 400),
                0);
        }
        //Debug.Log(PlayerPrefs.GetInt("language") + "language");
        if (Input.GetMouseButtonUp(0))
        {
            
            if (soundToggleSlider.value < 0.5f)
            {
                soundToggleSlider.value = 0;
            }
            else
            {
                soundToggleSlider.value = 1;
            }


            if (vibrationToggleSlider.value < 0.5f)
            {
                vibrationToggleSlider.value = 0;
                PlayerPrefs.SetInt("VibrationOn", 0);
            }
            else
            {
                vibrationToggleSlider.value = 1;
                PlayerPrefs.SetInt("VibrationOn", 1);
            }

            if (reviseToggleSlider.value < 0.5f)
            {
                reviseToggleSlider.value = 0;
                ReviseDirection();
            }
            else
            {
                reviseToggleSlider.value = 1;
                NormalDirection();
            }

            if (modelToggleSlider.value < 0.5f)
            {
                modelToggleSlider.value = 0;
            }
            else
            {
                modelToggleSlider.value = 1;
            }
            if (soundToggleSlider.value == 0)
            {
                AudioListener.volume = 0;
                PlayerPrefs.SetInt("MusicOn", 0);
            }
            else
            {
                AudioListener.volume = 1;
                PlayerPrefs.SetInt("MusicOn", 1);
            }
         
            if (modelToggleSlider.value==0)
            {
                gameController.SendMessage("ChoseJoyStick");
            }
            else if(modelToggleSlider.value==1)
            {
                gameController.SendMessage("ChoseGravity");
            }
            ////切换语言，如果和之前存的值不同才改变
            //if (GameObject.FindGameObjectWithTag("Language").GetComponent<Text>().text == "English")
            //{
            //    if (PlayerPrefs.GetInt("language") != 1)
            //    {

            //        language = (int)Language.English;
            //        PlayerPrefs.SetInt("language", language);
            //        Application.LoadLevel(0);        
            //    }              
            //}         
            //else
            //{
            //    if (PlayerPrefs.GetInt("language") != 0)
            //    {
            //        language = (int)Language.Chinese;
            //        PlayerPrefs.SetInt("language", language);
            //        Application.LoadLevel(0);    
            //    }         
            //}
        }
        
    }
    public void SetChinese()
    {
        language = (int)Language.Chinese;
        PlayerPrefs.SetInt("language", language);
        SceneManager.LoadScene(0);
    }
    public void SetEnglish()
    {
        language = (int)Language.English;
        PlayerPrefs.SetInt("language", language);
        SceneManager.LoadScene(0);
    }
    public void NormalDirection()
    {
        PlayerPrefs.SetInt("ReviseDirection", 0);
    }
    public void ReviseDirection()
    {
        PlayerPrefs.SetInt("ReviseDirection", 1);
    }
    public void OnSensitivityValueChanged()
    {
        PlayerPrefs.SetFloat("SensitivityOn", sensitivityToggleSlider.value * 4 + 1);
    }
}
