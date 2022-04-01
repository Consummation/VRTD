using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRTD
{
    public class GunAnimator : MonoBehaviour,IGunAnimator
    {
        [SerializeField] private Animator animator;

        private int _fireHash;

        private void Awake()
        {
            _fireHash = Animator.StringToHash("Fire");
        }

        public void PlayFire()
        {
            animator.Play(_fireHash,0,0);
        }
    }
}
