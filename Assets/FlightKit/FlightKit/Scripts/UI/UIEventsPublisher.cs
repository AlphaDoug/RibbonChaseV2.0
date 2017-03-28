using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlightKit
{
	/// <summary>
	/// Publishes OnPlayEvent when user starts playing a level.
	/// </summary>
	public class UIEventsPublisher : MonoBehaviour {
        public static event GameActions.SimpleAction OnPlayEvent;
        //void Start()
        //{
        //    //OnPlayEvent();
        //    StartCoroutine(WaitAndStart(2));
        //    Debug.Log("play");
        //}
        public virtual void PublishPlay()
        {
            if (OnPlayEvent != null)
            {
                OnPlayEvent();
            }
        }
        //IEnumerator WaitAndStart(float waitTime)
        //{
        //    yield return new WaitForSeconds(waitTime);
        //    OnPlayEvent();
        //}
       
    }
}