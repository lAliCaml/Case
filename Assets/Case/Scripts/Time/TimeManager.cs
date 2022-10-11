using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Case.Energy;
using Case.Managers;
using Case.Energy;

namespace Case.TimeControl
{
    public class TimeManager : MonoBehaviour
    {
        [SerializeField] private Text text_Time;
        [SerializeField] private int _time;
        private int _totalTime;


        void Start()
        {
            GameManager.Instance.OnStartGame += HandleStartGame;
            _totalTime = _time;
        }

        private void HandleStartGame()
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
                    GameManager.Instance.FinishGame();
                    UIManager.Instance.ShowWinner("Enemy");
                }
                yield return new WaitForSeconds(1);
            }
        }
    }

}
