using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Case.BorderControl
{
    public class BorderManager : MonoBehaviour
    {
        public static BorderManager Instance;

        [SerializeField] private GameObject[] _leftBorder;
        [SerializeField] private GameObject[] _rightBorder;

        private Transform[] transformChange;

        void Start()
        {
            Instance = this;
        }


        public void BorderCondition(bool probability)
        {
            for (int i = 0; i < _leftBorder.Length; i++)
            {
                _leftBorder[i].SetActive(probability);
                _rightBorder[i].SetActive(probability);

                _leftBorder[0].SetActive(true);
                _rightBorder[0].SetActive(true);
            }

            
        }

       
        public void ChangeBorder(string name)
        {

            StartCoroutine(ChangeBorderSettings(name));

            for (int i = 0; i < _leftBorder.Length; i++)
            {
                _leftBorder[i].SetActive(true);
                _rightBorder[i].SetActive(true);
            }

            transformChange = new Transform[2];



            if (name == "Left")
            {
                transformChange[0] = _leftBorder[0].transform;
                transformChange[1] = _leftBorder[1].transform;
            }
            else if (name == "Right")
            {
                transformChange[0] = _rightBorder[0].transform;
                transformChange[1] = _rightBorder[1].transform;
            }

            transformChange[0].DOScaleZ(20, 1);
            transformChange[0].DOMoveZ(25, 1);

            transformChange[1].DOScaleZ(48, 1);
            transformChange[1].DOMoveZ(-11, 1);
        }

        IEnumerator ChangeBorderSettings(string name)
        {
            yield return new WaitForSeconds(2);

            for (int i = 0; i < _leftBorder.Length; i++)
            {
                _leftBorder[1].SetActive(false);
                _rightBorder[1].SetActive(false);
            }
        }
    }
}

