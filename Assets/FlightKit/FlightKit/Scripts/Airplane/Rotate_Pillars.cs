using UnityEngine;
using System.Collections;

public class Rotate_Pillars : MonoBehaviour {

	// Use this for initialization
    public bool isX = false;
    public bool isY = false;
    public bool isZ = false;
    public GameObject Propeller_1;
    public GameObject Propeller_2;
    public GameObject Propeller_3;
    public GameObject Propeller_4;
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        //以模型Y轴旋转，单位为2.
        if (isX)
        {
            Propeller_1.transform.Rotate(40, 0, 0);
            Propeller_2.transform.Rotate(40, 0, 0);
            Propeller_3.transform.Rotate(40, 0, 0);
            Propeller_4.transform.Rotate(40, 0, 0);
        }
        if (isY)
        {
            Propeller_1.transform.Rotate(0, 40, 0);
            Propeller_2.transform.Rotate(0, 40, 0);
            Propeller_3.transform.Rotate(0, 40, 0);
            Propeller_4.transform.Rotate(0, 40, 0);
        }
        if (isZ)
        {
            Propeller_1.transform.Rotate(0, 0, 40);
            Propeller_2.transform.Rotate(0, 0, 40);
            Propeller_3.transform.Rotate(0, 0, 40);
            Propeller_4.transform.Rotate(0, 0, 40);
        }
	}
}
