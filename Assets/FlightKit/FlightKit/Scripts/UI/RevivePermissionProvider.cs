﻿using System.Collections;
using UnityEngine;

namespace FlightKit
{
    /// <summary>
    /// This class encapsulates the actions needed to perform for user to revive. For example, show video ads.
    /// </summary>
    public class RevivePermissionProvider : MonoBehaviour {
		/// <summary>
		/// This event is fired once user presses revive button.
		/// </summary>
		public static event GameActions.SimpleAction OnReviveRequested;

		/// <summary>
		/// This event is fired once user views ads/pays for reviving.
		/// </summary>
		public static event GameActions.SimpleAction OnReviveGranted;
        void OnEnable()
        {
            OnReviveRequested += HandleReviveRequested;
        }

        void OnDisable()
        {
            OnReviveRequested -= HandleReviveRequested;
        }
        
        /// <summary>
        /// Call this if user pressed revive button, this will fire OnReviveRequested.
        /// </summary>
        public virtual void RequestRevive()
        {
            if (OnReviveRequested != null)
            {
                OnReviveRequested();
            }
        }
        
        /// <summary>
        /// Call this once user is allowed to revive.
        /// </summary>
        public virtual void GrantRevive()
        {
            if (OnReviveGranted != null)
            {
                OnReviveGranted();
            }
        }
        
        protected virtual void HandleReviveRequested()
        {
                StartCoroutine(ReviveNextFrame());
          
        }

        //protected void HandleShowResult(AdShowResult result)
        //{
        //    switch (result) {
        //        case AdShowResult.Finished:
        //            Debug.Log("The ad was successfully shown.");
        //            GrantRevive();
        //            break;
        //        case AdShowResult.Skipped:
        //            Debug.Log("The ad was skipped before reaching the end.");
        //            break;
        //        case AdShowResult.Failed:
        //            Debug.LogError("The ad failed to be shown.");
        //            break;
        //    }
        //}
        
        /// <summary>
        /// Waits until next frame and grants revival.
        /// </summary>
        private IEnumerator ReviveNextFrame()
        {
            yield return new WaitForEndOfFrame();
            GrantRevive();
        }
        
    }
}
