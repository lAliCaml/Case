using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Case.Energy;
using Case.Managers;
using Case.Energy;
using Photon.Bolt;

namespace Case.TimeControl
{
    public class TimeManager : GlobalEventListener
    {
        [SerializeField] private Text text_Time;
        [SerializeField] private int _time;
        private int _totalTime;


        void Start()
        {
            _totalTime = _time;
        }


        public override void OnEvent(HandleBegin evnt)
        {
            StartCoroutine(Timer());
        }



        IEnumerator Timer()
        {
            while(GameManager.Instance.isContinue)
            {
                _time--;
                text_Time.text = _time.ToString();

                if(_time == _totalTime *.5f)
                {
                    EnergyManager.Instance.EnergyRechargeRate = 2;
                }
                
                if(_time <= 0)
                {
                    var evnt = FinishGame.Create();
                    evnt.Name = "Friend";
                    evnt.Send();
                }
                yield return new WaitForSeconds(1);
            }
        }
    }

}
