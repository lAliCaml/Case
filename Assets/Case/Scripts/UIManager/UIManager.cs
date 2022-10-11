using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Case.Managers
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance;

       

        [Header("Panels")]
        [SerializeField] private GameObject panel_Menu;
        [SerializeField] private GameObject panel_Game;
        [SerializeField] private GameObject panel_End;
        
        [Header("Winner-Loser screen")]
        [SerializeField] private Transform _textWinner;
        [SerializeField] private Transform _purpleTeam;
        [SerializeField] private Transform _blueTeam;

        void Start()
        {
            Instance = this;

            GameManager.Instance.OnStartGame += HandleStartGame;
            GameManager.Instance.OnFinishGame += HandleFinishGame;
        }


        private void HandleStartGame()
        {
            panel_Menu.SetActive(false);
            panel_Game.SetActive(true);
            panel_End.SetActive(false);
        }


        private void HandleFinishGame()
        {
            panel_Menu.SetActive(false);
            panel_Game.SetActive(false);
            panel_End.SetActive(true);
        }

        #region Buttons
        public void StartButton()
        {
            GameManager.Instance.StartGame();

        }

        public void PlayAgain()
        {
            SceneManager.LoadScene(0);
        }

        #endregion

        public void ShowWinner(string name)
        {
            if(name == "Friend")
            {
                _textWinner.parent = _purpleTeam;
            }
            else if(name == "Enemy")
            {
                _textWinner.parent = _blueTeam;
            }
            _textWinner.localPosition = Vector3.zero;
            
        }
    }

}
