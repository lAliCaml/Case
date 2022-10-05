using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Case.TimeControl;

namespace Case.Energy
{
    public class EnergyManager : MonoBehaviour
    {
        public static EnergyManager Instance;

        [SerializeField] private Slider _energySlider;
        [SerializeField] private Text _energyText;

        public float energy;
        private bool _isFilling;

        public float EnergyRechargeRate;

      

        void Start()
        {
            Instance = this;

            _energyText.text = ((int)energy).ToString();
            _energySlider.value = energy / 10;
            _isFilling = false;
            EnergyRechargeRate = 1;
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
                energy += EnergyRechargeRate * Time.deltaTime * .75f;

                _energyText.text = ((int)energy).ToString();
                _energySlider.value = energy / 10;


                yield return null;
            }

            _isFilling = false;
        }
    }
}
