using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FlightKit;
public class ReviveControl : MonoBehaviour
{
    private FuelController _fuelControoler;
    // Use this for initialization
    void Start ()
    {
        _fuelControoler = GetComponent<FuelController>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
    public void Revive()
    {
        Time.timeScale = 0;
       // _fuelControoler.fuelAmount = 0.5f;
    }
}
