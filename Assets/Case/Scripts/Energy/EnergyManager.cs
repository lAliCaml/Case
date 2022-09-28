using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Case.Energy
{
    public class EnergyManager : MonoBehaviour
    {
        public static EnergyManager Instance;

        [SerializeField] private Image _energyImage;
        [SerializeField] private Text _energyText;

        public float energy;
        private bool _isFilling;

        void Start()
        {
            Instance = this;

            _energyText.text = ((int)energy).ToString();
            _energyImage.fillAmount = energy / 10;
            _isFilling = false;
        }


        public void SpendEnergy(float amount)
        {
            energy -= amount;

            if (!_isFilling)
            {
                StartCoroutine(FillEnergy());
            }
        }

        private IEnumerator FillEnergy()
        {
            _isFilling = true;
            while (energy <= 10)
            {
                energy += 1 * Time.deltaTime;

                _energyText.text = ((int)energy).ToString();
                _energyImage.fillAmount = energy / 10;


                yield return null;
            }

            _isFilling = false;
        }
    }


}
