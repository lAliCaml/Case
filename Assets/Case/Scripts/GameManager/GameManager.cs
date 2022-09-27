using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Case.Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        public bool isContinue;

        public System.Action OnStartGame;
        public System.Action OnFinishGame;

        void Start()
        {
            Instance = this;
        }

        public void StartGame()
        {
            isContinue = true;
            OnStartGame?.Invoke();
        }

        public void FinishGame()
        {
            OnFinishGame?.Invoke();
            isContinue = false;
        }
    }

}

