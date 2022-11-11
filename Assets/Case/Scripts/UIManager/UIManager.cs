using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Bolt;
using System;
using Photon.Bolt.Matchmaking;
using Photon.Bolt.Tokens;
using Photon.Bolt.Utils;

namespace Case.Managers
{
    public class UIManager : GlobalEventListener
    {
        public static UIManager Instance;


        [Header("Panels")]
        [SerializeField] private GameObject panel_Game;
        [SerializeField] private GameObject panel_End;

        [Header("Winner-Loser screen")]
        [SerializeField] private Transform _textWinner;
        [SerializeField] private Transform _purpleTeam;
        [SerializeField] private Transform _blueTeam;

        void Start()
        {
            Instance = this;

            GameManager.Instance.OnFinishGame += HandleFinishGame;
        }



        private void HandleFinishGame()
        {
            BoltNetwork.Shutdown();
            BoltLauncher.Shutdown();
            panel_Game.SetActive(false);
            panel_End.SetActive(true);
        }

        #region Buttons

        public void PlayAgain()
        {
            if(BoltNetwork.IsServer)
            {
                BoltNetwork.UpdateSessionList(BoltNetwork.SessionList);
            }
            SceneManager.LoadScene(0);
        }



        #endregion

        public override void OnEvent(FinishGame evnt)
        {
            if (evnt.Name == "Enemy")
            {
                _textWinner.parent = _blueTeam;
            }
            else if (evnt.Name == "Friend")
            {
                _textWinner.parent = _purpleTeam;
            }
            _textWinner.localPosition = Vector3.zero;
        }

        public override void OnEvent(HandleBegin evnt)
        {
            panel_Game.SetActive(true);
            panel_End.SetActive(false);
        }

    }

}
