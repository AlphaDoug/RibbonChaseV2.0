using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleNoPause : MonoBehaviour {
    private ParticleSystem _particle;
    private float _deltaTime;
    private float _timeAtLastFrame;

    void Awake()
    {
        _particle = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (_particle == null) return;
        _deltaTime = Time.realtimeSinceStartup - _timeAtLastFrame;
        _timeAtLastFrame = Time.realtimeSinceStartup;
         // 如果TimeScale<le-6, 那么获取粒子刷新每一帧的时间赋值给粒子刷新函数，摆脱TimeScale控制
        if (Mathf.Abs(Time.timeScale) < 1e-6)
        {
            _particle.Simulate(_deltaTime, false, false);
            _particle.Play();
        }
      
    }
}