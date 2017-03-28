using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ControlsMenuController : MonoBehaviour
{
    public GameObject musicToggle;
    public GameObject soundToggle;
    public GameObject modelToggle;
    public GameObject gameController;

    private Slider musicToggleSlider;
    private Slider soundToggleSlider;
    private Slider modelToggleSlider;
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
        dropDown = GameObject.Find("LanguageChoose").GetComponent<Dropdown>();
        musicToggleSlider = musicToggle.GetComponent<Slider>();
        soundToggleSlider = soundToggle.GetComponent<Slider>();
        modelToggleSlider = modelToggle.GetComponent<Slider>();
        language = (int)Language.Chinese;

        //初始化时如果有之前存的值就读取之前存的值来初始化
        if (PlayerPrefs.GetInt("language") == null)
        {
            PlayerPrefs.SetInt("language", 0);
        }
        if (PlayerPrefs.GetInt("language") == 0)
        {
            dropDown.ClearOptions();
            Dropdown.OptionData chineseData = new Dropdown.OptionData();
            chineseData.text = "中文";
            dropDown.options.Add(chineseData);

            Dropdown.OptionData englishData = new Dropdown.OptionData();
            englishData.text = "English";
            dropDown.options.Add(englishData);

            dropDown.captionText.text = "中文";

            //GameObject.Find("LanguageChoose").GetComponent<Dropdown>().captionText.text = "中文";
            //GameObject.Find("LanguageChoose").GetComponent<Dropdown>().options[1].text = "中文";
        }
        if (PlayerPrefs.GetInt("language") == 1)
        {
            dropDown.ClearOptions();
            
            Dropdown.OptionData englishData = new Dropdown.OptionData();
            englishData.text = "English";
            dropDown.options.Add(englishData);

            Dropdown.OptionData chineseData = new Dropdown.OptionData();
            chineseData.text = "中文";
            dropDown.options.Add(chineseData);

            dropDown.captionText.text = "English";
            //GameObject.Find("LanguageChoose").GetComponent<Dropdown>().captionText.text = "English";
            //GameObject.Find("LanguageChoose").GetComponent<Dropdown>().options[1].text = "English";
        }
        if (PlayerPrefs.GetInt("AirPlaneController") == 0|| PlayerPrefs.GetInt("AirPlaneController") ==null)
        {
            modelToggleSlider.value= 0;
        }
        else
        {
            modelToggleSlider.value = 1;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (musicToggleSlider.value < 0.5f)
            {
                musicToggleSlider.value = 0;
            }
            else
            {
                musicToggleSlider.value = 1;
            }
            if (soundToggleSlider.value < 0.5f)
            {
                soundToggleSlider.value = 0;
            }
            else
            {
                soundToggleSlider.value = 1;
            }
            if (modelToggleSlider.value < 0.5f)
            {
                modelToggleSlider.value = 0;
            }
            else
            {
                modelToggleSlider.value = 1;
            }
            if (musicToggleSlider.value == 0)
            {
                AudioListener.volume = 0;
            }
            else
            {
                AudioListener.volume = 1;
            }
            if (modelToggleSlider.value==0)
            {
                gameController.SendMessage("ChoseJoyStick");
            }
            else if(modelToggleSlider.value==1)
            {
                gameController.SendMessage("ChoseGravity");
            }
            //切换语言，如果和之前存的值不同才改变
            if (GameObject.FindGameObjectWithTag("Language").GetComponent<Text>().text == "English")
            {
                if (PlayerPrefs.GetInt("language") != 1)
                {

                    language = (int)Language.English;
                    PlayerPrefs.SetInt("language", language);
                    Application.LoadLevel(0);        
                }              
            }         
            else
            {
                if (PlayerPrefs.GetInt("language") != 0)
                {
                    language = (int)Language.Chinese;
                    PlayerPrefs.SetInt("language", language);
                    Application.LoadLevel(0);    
                }         
            }
        }
        
    }
}
