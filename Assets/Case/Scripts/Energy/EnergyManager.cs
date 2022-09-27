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

        void Start()
        {
            Instance = this;

            _energyText.text = ((int)energy).ToString();
            _energyImage.fillAmount = energy / 10;
        }

        private void Update()
        {
            if (energy <= 10)
            {
                energy += 1 * Time.deltaTime;

                _energyText.text = ((int)energy).ToString();
                _energyImage.fillAmount =  energy / 10;
            }
        }

        public void SpendEnergy(float amount)
        {
            energy -= amount;
        }
    }


}
