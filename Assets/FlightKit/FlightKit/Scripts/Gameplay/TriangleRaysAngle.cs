using UnityEngine;
using System.Collections;

public class TriangleRaysAngle : MonoBehaviour {
    public ParticleSystem particleSystem;
    int particleCount=0;
    ParticleSystem.Particle[] particles;
    public GameObject airplane;
    private Vector3 speed;
    // Use this for initialization
    void Start () {
        particleSystem=GetComponent<ParticleSystem>();
        particles = new ParticleSystem.Particle[1000];
        
    }
	
	// Update is called once per frame
	void LateUpdate() {
        speed = -(airplane.GetComponent<Transform>().forward) * 20;
        particleCount = particleSystem.GetParticles(particles);
        //Debug.Log("speed" + speed);
        for (int i = 0; i < particleCount; i++)
        {
            particles[i].velocity = speed;
        }

        particleSystem.SetParticles(particles, particleCount);
    }
}
