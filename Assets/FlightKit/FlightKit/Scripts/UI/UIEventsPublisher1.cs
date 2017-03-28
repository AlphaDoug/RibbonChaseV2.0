using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlightKit
{
	/// <summary>
	/// Publishes OnPlayEvent when user starts playing a level.
	/// </summary>
	public class UIEventsPublisher1 : MonoBehaviour {

        public static event GameActions.SimpleAction OnPlayEvent;
        private GameObject UIScripts;
        private ButtonSound buttonSound;
        void Start()
        {

            //OnPlayEvent();
            UIScripts = GameObject.Find("UIScripts");
           // buttonSound = UIScripts.GetComponent<ButtonSound>();
            StartCoroutine(WaitAndStart(0.5f));

        }
        //public virtual void PublishPlay()
        //{
        //    if (OnPlayEvent != null)
        //    {
        //        OnPlayEvent();
        //    }
        //}
        IEnumerator WaitAndStart(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            OnPlayEvent();
            //buttonSound.SendMessage("PlaySound");
            UIEventsPublisher1.OnPlayEvent =null;
        }

    }
}