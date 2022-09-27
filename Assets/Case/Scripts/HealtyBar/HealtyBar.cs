using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Case.Characters;
using UnityEngine.UI;

namespace Case.Health
{
    public class HealtyBar : MonoBehaviour
    {
        private Character _character;
       

        [SerializeField] private Vector3 _offset;

        [SerializeField] private Image _healthImage;
        [SerializeField] private Transform _canvas;

        private void Start()
        {
            _canvas.transform.LookAt(Camera.main.transform.position);


            if (gameObject.CompareTag("Friend"))
            {
                _healthImage.color = Color.red;
            }
            else if (gameObject.CompareTag("Enemy"))
            {
                _healthImage.color = Color.magenta;
            }
        }

        public void HealthBarShow(float rate)
        {
            _healthImage.fillAmount = rate;
        }
    }
}

