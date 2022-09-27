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
    }

}
