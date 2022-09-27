using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Case.Healty;
using Case.Throw;
using Case.Health;
using DG.Tweening;
using Case.Managers;


namespace Case.Towers
{
    public class Tower : MonoBehaviour, IHealty
    {
        #region Variables
        [Header("CharacterFeatures")]
        private int _health;
        private int _attack;
        private float _scanArea;

        [Header("Components")]
        [SerializeField] private HealtyBar _healtBar;

        [Header("HitEffect")]
        [SerializeField] private ParticleSystem _hitEffect;
        [SerializeField] private ParticleSystem _deathEffect;



        [SerializeField] private TowerProperties _towerProperties; //ScriptableObject

        private int _currentHealth;

        [SerializeField] private bool _isMainTower;

        [SerializeField] private GameObject Arrow;

        #endregion

        #region Define Properties

        void Start()
        {
            _health = _towerProperties.Healty;
            _attack = _towerProperties.Attack;
            _scanArea = _towerProperties.ScanArea;

            _currentHealth = _health;

            StartCoroutine(FindEnemy());
        }

        #endregion



        #region FindEnemy

        IEnumerator FindEnemy()
        {
            while (true)
            {
                ScanningEnemy();
                yield return new WaitForSeconds(1);
            }
        }

        private void ScanningEnemy()
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, _scanArea);

            GameObject nearestEnemy = null;
            float scanArea = _scanArea;

            foreach (var hitObj in hitColliders)
            {
                if (hitObj.gameObject.CompareTag("Enemy"))
                {
                    if(Vector3.Distance(transform.position , hitObj.transform.position) <= scanArea)
                    {
                        nearestEnemy = hitObj.gameObject;
                        scanArea = Vector3.Distance(transform.position, hitObj.transform.position);
                    }
                }
            }

            if (nearestEnemy != null)
            {
                Attack(nearestEnemy.transform);
            }
            
        }


        #endregion

        #region Attack

        public virtual void Attack(Transform target)
        {
            Vector3 startingPos = transform.position + Vector3.up * 3;

            GameObject arrow = Instantiate(Arrow, transform.position, Quaternion.identity);

            string name = gameObject.tag;

            arrow.GetComponent<IThrow>().ThrowSettings(startingPos, target, _attack, name);

        }


        #endregion





        #region Health

        public void GetDamage(int damage)
        {
            _currentHealth -= damage;
            Instantiate(_hitEffect, transform.position, Quaternion.identity);

            if (_currentHealth > 0)
            {
                float healthRate = (float)_currentHealth / (float)_health;
                _healtBar.HealthBarShow(healthRate);

                transform.DOPunchScale(Vector3.one * .1f, .1f);
            }
            else
            {
                Death();
            }

            Camera.main.DOShakePosition(0.1f, Vector3.one * .075f, 0, 0f, false);
        }

        public void Death()
        {
            Instantiate(_deathEffect, transform.position, Quaternion.identity);

            if(_isMainTower)
            {
                GameManager.Instance.FinishGame();
            }

            Destroy(gameObject);
        }
        #endregion
    }

}
