using UnityEngine;
using System.Collections;
using UnityStandardAssets.Vehicles.Aeroplane;
using UnityStandardAssets.ImageEffects;

public class MotionBlurController : MonoBehaviour {
    public GameObject airPlane;
    public GameObject camera;   
    public GameObject TriangleRays;
    public GameObject speedUp;

    private AeroplaneController aeroPlaneController;
    private MotionBlur motionBlur;
    private AudioSource speedupsound;
    // Use this for initialization
    void Start () {
        aeroPlaneController = airPlane.GetComponent<AeroplaneController>();
        motionBlur = camera.GetComponent<MotionBlur>();
        speedupsound = speedUp.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        motionBlur.blurAmount = (aeroPlaneController.MaxSpeed - 100) * 0.007f;
        if (motionBlur == null)
        {
            Debug.LogError("no plane");
        }
        if (aeroPlaneController.MaxSpeed > 110)
        {
            Accelation();
        }
        else
        {
            NotAccelation();
        }
	}

    void Accelation()
    {
        TriangleRays.SetActive(true);
        speedUp.SetActive(true);
        
    }

    void NotAccelation()
    {
        TriangleRays.SetActive(false);
        speedUp.SetActive(false);
    }
}
