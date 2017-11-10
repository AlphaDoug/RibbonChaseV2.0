using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DaramPage : MonoBehaviour
{
    public GameObject nextPage;
    public bool isChangeAlpha = false;
    public bool isFirstPage = false;
    public float alphaSpeed = 0.02f;

    public List<SpriteRenderer> childSprite = new List<SpriteRenderer>();
	// Use this for initialization
	void Start ()
    {
        if (isFirstPage)
        {
            isChangeAlpha = true;
        }
    }
	
	void Update ()
    {
        if (childSprite[0].color.a == 1)
        {
            //this.GetComponent<CanvasRenderer>().SetAlpha(GetComponent<CanvasRenderer>().GetAlpha() - alphaSpeed);
            if (isChangeAlpha)
            {
                GetComponent<Animator>().updateMode = AnimatorUpdateMode.UnscaledTime;
                GetComponent<Animator>().Play(gameObject.name);
                StartCoroutine(ChangeAlpha());
                isChangeAlpha = false;
            }
           
        }
        if (childSprite[0].color.a <= 0)
        {
            //透明度变为0之后开始下一张图的动态变化
            if (nextPage == null)
            {

            }
            else
            {
                nextPage.GetComponent<DaramPage>().isChangeAlpha = true;
            }        
            StopAllCoroutines();
            Debug.Log("开始下一张图片改变");
            gameObject.SetActive(false);
        }
	}


    IEnumerator ChangeAlpha()
    {
        yield return new WaitForSeconds(6);

        while (childSprite[0].color.a >= 0)
        {
            for (int i = 0; i < childSprite.Count; i++)
            {
                childSprite[i].color = new Color(1, 1, 1, childSprite[i].color.a - alphaSpeed);
            }
            yield return new WaitForSeconds(0.05f);
        }
    }

    private void OnEnable()
    {
        if (isFirstPage)
        {
            isChangeAlpha = true;
        }
    }
}
