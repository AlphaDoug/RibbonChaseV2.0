using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpritrsAnimator : MonoBehaviour {

    private Animator _animator;
  
      void Start()
      {
         _animator = this.GetComponent<Animator>();
        for (int i = 0; i < 17; i++)
        {
            _animator.SetBool("Sprite" + (i + 1).ToString(), true);
        }
        
    }


}
