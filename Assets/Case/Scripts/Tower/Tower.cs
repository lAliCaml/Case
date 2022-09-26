using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Case.Healty;
using Case.Throw;


namespace Case.Towers
{
    public class Tower : MonoBehaviour, IHealty
    {
        #region Variables
        [Header("CharacterFeatures")]
        private int _health;
        private int _attack;
        private float _scanArea;

        private int _currentHealth;
        [SerializeField] private TowerProperties _towerProperties;

        [HideInInspector]
        public float HealthRate;

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
            arrow.GetComponent<IThrow>().ThrowSettings(startingPos, target, _attack);

        }


        #endregion





        #region Health

        public void GetDamage(int damage)
        {
            _currentHealth -= damage;

            Debug.Log(gameObject.name + " ," + _currentHealth.ToString());


            if (_currentHealth > 0)
            {
                HealthRate = _currentHealth / _health;
            }
            else
            {
                Death();
            }
        }

        public void Death()
        {
            Debug.Log("Die");
            Destroy(gameObject);
        }
        #endregion
    }

}
