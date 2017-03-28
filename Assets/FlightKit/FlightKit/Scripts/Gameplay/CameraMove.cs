using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {
    public Vector3 destination;
    public Quaternion targetRotation;
    public GameControllerVR gameController;
    public GameObject levelFinishedUI;
    // Use this for initialization
    void Start () {
        if (Application.loadedLevelName == "ZTE_1_Star")
        {
            Debug.Log(Application.loadedLevelName);
            destination = new Vector3(900, 1300, -1008);
            targetRotation = Quaternion.Euler(33, 287, 2);

           // targetRotation = Quaternion.Euler(33, -20, 2);
        }
        if (Application.loadedLevelName == "ZTE_2_ZTE")
        {
            Debug.Log(Application.loadedLevelName);
            //destination = new Vector3(900, 1300, -1008);
            //targetRotation = Quaternion.Euler(33, 20, 2);
            destination = new Vector3(2170, 1689, -721);
            targetRotation = Quaternion.Euler(33, 315, 349);
        }


        gameController = GameObject.Find("GameController").GetComponent<GameControllerVR>();
    }
	
	// Update is called once per frame
	void Update () {
        //transform.parent.DetachChildren();
        transform.position = Vector3.MoveTowards(gameObject.transform.position, destination, Time.deltaTime * 500);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime);
        if (Vector3.Distance(transform.position, destination) < 100f)
        {
            gameController.SetActiveButtonOnOver();
            Invoke("SuccessedUI", 4f);
        }
        
	}
    public void SuccessedUI()
    {
        levelFinishedUI.SetActive(true);
    }
}
