using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Case.Characters;
using UnityEngine.UI;
using DG.Tweening;
using Case.Managers;

namespace Case.Health
{
    public class HealtyBar : MonoBehaviour
    {
        private Character _character;
       

        [SerializeField] private Vector3 _offset;

        [SerializeField] private Image _healthImage;
        [SerializeField] private Transform _canvas;

        public void Initialize()
        {
            if (gameObject.CompareTag("Friend"))
            {
                _healthImage.color = Color.red;
            }
            else if (gameObject.CompareTag("Enemy"))
            {
                _healthImage.color = Color.magenta;
            }
            _canvas.transform.DORotate(Vector3.right * 60, .1f);// = Quaternion.Euler(Vector3.right * 60); //DOLocalRotate(Vector3.right * 60, 1);
            _canvas.LookAt(Camera.main.transform.position + Vector3.up * 1000);
        }

        public void Update()
        {
            if(GameManager.Instance.isContinue)
            {
                _canvas.transform.DORotate(Vector3.right * 60, .1f);// = Quaternion.Euler(Vector3.right * 60); //DOLocalRotate(Vector3.right * 60, 1);
                _canvas.LookAt(Camera.main.transform.position + Vector3.up * 1000);
            }
            
        }

        public void HealthBarShow(float rate)
        {
            _healthImage.fillAmount = rate;
        }
    }
}

