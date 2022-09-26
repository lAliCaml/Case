using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Case.Healty;


namespace Case.Towers
{
    public class Tower : MonoBehaviour, IHealty
    {
        #region Variables
        [Header("CharacterFeatures")]
        private int _health;
        private int _attack;
        private float _speed;
        private float _scanArea;

        private int _currentHealth;
        [SerializeField] private TowerProperties _towerProperties;

        [HideInInspector]
        public float HealthRate;

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
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 10);

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
            Debug.Log(target.name);
        }


        #endregion





        #region Health

        public void GetDamage(int damage)
        {
            HealthRate = CurrentHealty(_currentHealth, damage);
        }

        public int CurrentHealty(int currentHealth, int damage)
        {
            int value = currentHealth - damage;
            int rate = value / _health;

            if (value > 0)
            {
                return rate;
            }
            else
            {
                Death();
            }
            return value;
        }

        public void Death()
        {
            Debug.Log("Die");
            Destroy(gameObject);
        }
        #endregion
    }

}
