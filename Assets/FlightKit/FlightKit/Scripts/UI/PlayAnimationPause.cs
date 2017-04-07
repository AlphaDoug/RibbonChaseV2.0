using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimationPause : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        AnimationState _currState = GetComponent<AnimationState>();
        var animation = GetComponent<Animation>();
        bool isPlaying = true;
        float _progressTime = 0F;
        float _timeAtLastFrame = 0F;
        float _timeAtCurrentFrame = 0F;
        bool _inReversePlaying = false;
        float _deltaTime = 0F;

        animation.Play();
        _timeAtLastFrame = Time.realtimeSinceStartup;

        while (isPlaying)
        {
            _timeAtCurrentFrame = Time.realtimeSinceStartup;
            _deltaTime = _timeAtCurrentFrame - _timeAtLastFrame;
            _timeAtLastFrame = _timeAtCurrentFrame;
            _progressTime += _deltaTime;
            _currState.normalizedTime = _inReversePlaying ? 1.0f - (_progressTime / _currState.length) : _progressTime / _currState.length;
            animation.Sample();
            //…repeat or over by wrap mode 
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
