using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Case.BorderControl
{
    public class BorderManager : MonoBehaviour
    {
        public static BorderManager Instance;

        [SerializeField] private GameObject _leftBorder;
        [SerializeField] private GameObject _rightBorder;

        private Transform transformChange;

        void Start()
        {
            Instance = this;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void BorderCondition(bool probability)
        {
            _leftBorder.SetActive(probability);
            _rightBorder.SetActive(probability);
        }

       
        public void ChangeBorder(string name)
        {

            StartCoroutine(ChangeBorderSettings(name));

            _leftBorder.SetActive(true);
            _rightBorder.SetActive(true);

           

            if (name == "Left")
            {
                transformChange = _leftBorder.transform;
            }
            else if (name == "Right")
            {
                transformChange = _rightBorder.transform;
            }

            transformChange.DOScaleZ(20, 1);
            transformChange.DOMoveZ(25, 1);
        }

        IEnumerator ChangeBorderSettings(string name)
        {
            yield return new WaitForSeconds(2);

            _leftBorder.SetActive(false);
            _rightBorder.SetActive(false);
        }
    }
}

