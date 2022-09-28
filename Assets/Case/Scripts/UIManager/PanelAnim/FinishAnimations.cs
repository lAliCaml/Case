using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Case.UIControl
{
    public class FinishAnimations : MonoBehaviour
    {
        [SerializeField] private GameObject panel_TopPlayer;
        private Vector3 pos_TopPlayer;


        [SerializeField] private GameObject panel_BottomPlayer;
        private Vector3 pos_BottomPlayer;

        [SerializeField] private GameObject button_PlayAgain;




        private void OnEnable()
        {
            pos_TopPlayer = panel_TopPlayer.transform.position;
            panel_TopPlayer.transform.position += Vector3.right * 750;


            pos_BottomPlayer = panel_BottomPlayer.transform.position;
            panel_BottomPlayer.transform.position += Vector3.left * 750;

            button_PlayAgain.SetActive(false);

            StartCoroutine(StartAnim());
         
        }

        IEnumerator StartAnim()
        {
            yield return new WaitForSeconds(.25f);
            panel_TopPlayer.transform.DOMove(PercentPos(panel_TopPlayer.transform.position, pos_TopPlayer) , .5f);
            panel_BottomPlayer.transform.DOMove(PercentPos(panel_BottomPlayer.transform.position, pos_BottomPlayer), .5f);

            yield return new WaitForSeconds(.5f);

            panel_TopPlayer.transform.DOMove(pos_TopPlayer, .5f);
            panel_BottomPlayer.transform.DOMove(pos_BottomPlayer, .5f);

            button_PlayAgain.SetActive(true);
            button_PlayAgain.transform.DOPunchScale(Vector3.one, .1f);
            button_PlayAgain.transform.DOShakeRotation(0.1f, Vector3.one * 5f, 0, 0f, false);
        }

        private Vector3 PercentPos(Vector3 currentPos, Vector3 targetPos)
        {
            Vector3 pos = currentPos + (targetPos - currentPos) * .4f;

            return pos;
        }
    }
}
