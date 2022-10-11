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
            Application.targetFrameRate = 60;
        }

        private void Update()
        {
            if (Time.frameCount % 3 == 0)
                System.GC.Collect();
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

