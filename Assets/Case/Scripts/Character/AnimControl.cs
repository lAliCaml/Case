using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Bolt;

namespace Case.Characters
{
    public class AnimControl : EntityBehaviour<ICharacterState>
    {
        public Animator _anim; //Because of character is using animator for equalize anim

        public override void Attached()
        {
            state.SetAnimator(_anim);

            state.AddCallback("RunP", () =>
            {
                state.Animator.SetBool("RunP", state.RunP);
            });

            state.AddCallback("AttackP", () =>
            {
                state.Animator.SetTrigger("AttackP");
            });
        }

        public void IdleMode()
        {
            state.RunP = false;
         //   state.Animator.SetBool("RunP", false);
        }

        public void RunMode()
        {
            state.RunP = true;

         //   state.Animator.ResetTrigger("AttackP");
           // state.Animator.SetBool("RunP", true);
        }

        public void AttackMode()
        {
            state.RunP = false;
            state.AttackP++;

            //state.Animator.SetBool("RunP", false);
            //state.Animator.SetTrigger("AttackP");
        }
    }

}

