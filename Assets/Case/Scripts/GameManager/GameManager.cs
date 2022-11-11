using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Photon.Bolt;

namespace Case.Managers
{
    public class GameManager : GlobalEventListener
    {
        public static GameManager Instance;

        public bool isContinue;

        public Action OnFinishGame;

        void Start()
        {
            if(Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }

            if(BoltNetwork.IsClient)
            {
                var evnt = HandleBegin.Create();
                evnt.Send();
            }
        }


        public override void OnEvent(HandleBegin evnt)
        {
            isContinue = true;
        }



        public override void OnEvent(FinishGame evnt)
        {
            OnFinishGame?.Invoke();
            isContinue = false;
        }
    }

}

