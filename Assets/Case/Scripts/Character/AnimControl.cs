using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Case.Characters
{
    public class AnimControl : MonoBehaviour
    {
        [SerializeField] private Animator _anim;

        public void IdleMode()
        {
            _anim.SetBool("RunP", false);
        }

        public void RunMode()
        {
            _anim.ResetTrigger("AttackP");
            _anim.SetBool("RunP", true);
        }

        public void AttackMode()
        {
            _anim.SetBool("RunP", false);
            _anim.SetTrigger("AttackP");
        }
    }

}

