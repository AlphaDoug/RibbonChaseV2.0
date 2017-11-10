using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailsParticle : MonoBehaviour
{
    public ParticleSystem particle;
	// Use this for initialization
	void Start ()
    {
        particle.emissionRate = 0;
	}
    private void OnEnable()
    {
        particle.emissionRate = 0;
    }
    private void OnDisable()
    {
        particle.emissionRate = 0;
    }
    // Update is called once per frame
    void Update ()
    {
        if (particle.emissionRate <= 60.0f)
        {
            particle.emissionRate += 1;
        }
	}
}
