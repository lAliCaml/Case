using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Case.Healty;

namespace Case.Characters
{
    public abstract class Character : MonoBehaviour, IHealty
    {
        #region Variables
        [Header("CharacterFeatures")]
        private int _health;
        protected int _attack;
        private float _speed;
        private float _scanArea;
        private float _attackRange;

        [Header("Components")]
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private AnimControl _animControl;

        private int _currentHealth;

        [HideInInspector]
        public float HealthRate;

        private bool _isAttack;
        private bool _haveTarget;
        protected Transform _attackTarget; 



        [SerializeField] private CharacterProperties _characterProperties;
        #endregion

        #region  Define Properties
        void Start()
        {
            _health = _characterProperties.Healty;
            _attack = _characterProperties.Attack;
            _speed = _characterProperties.Speed;
            _scanArea = _characterProperties.ScanArea;
            _attackRange = _characterProperties.AttackRange;

            _currentHealth = _health;

            _agent.speed = _characterProperties.Speed;
            _isAttack = false;
            _haveTarget = false;

            StartCoroutine(RunSettings());
            StartCoroutine(FindEnemy());
        }
        #endregion



        #region Run

        IEnumerator RunSettings()
        {
            while(true)
            {
                if (!_isAttack && !_haveTarget)
                {
                    if(gameObject.CompareTag("Friend"))
                    {
                        _agent.SetDestination( transform.position + Vector3.forward * 10);
                    }
                    else if(gameObject.CompareTag("Enemy"))
                    {
                        _agent.SetDestination(transform.position - Vector3.forward * 10);
                        
                    }

                    _animControl.RunMode();
                }
                yield return new WaitForSeconds(.25f);
            }
        }

        #endregion



        #region Find Enemy

        IEnumerator FindEnemy()
        {
            while (true)
            {
                if (!_isAttack)
                {
                    ScanningEnemy();
                }
              
                yield return new WaitForSeconds(.25f);
            }
        }

        private void ScanningEnemy()
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, _scanArea);

            GameObject nearestEnemy = null;
            float scanArea = _scanArea;

            foreach (var hitObj in hitColliders)
            {
                if (gameObject.CompareTag("Friend") && hitObj.gameObject.CompareTag("Enemy"))
                {
                    if (Vector3.Distance(transform.position, hitObj.transform.position) <= scanArea)
                    {
                        nearestEnemy = hitObj.gameObject;
                        scanArea = Vector3.Distance(transform.position, hitObj.transform.position);
                    }
                }
                else if (gameObject.CompareTag("Enemy") && hitObj.gameObject.CompareTag("Friend"))
                {
                    if (Vector3.Distance(transform.position, hitObj.transform.position) <= scanArea)
                    {
                        nearestEnemy = hitObj.gameObject;
                        scanArea = Vector3.Distance(transform.position, hitObj.transform.position);
                    }
                }
            }


            if (nearestEnemy != null && Vector3.Distance(nearestEnemy.transform.position, transform.position) <= _attackRange)
            {
                BeginToAttack(nearestEnemy.transform);
            }
            else if (nearestEnemy != null && Vector3.Distance(nearestEnemy.transform.position, transform.position) >= _attackRange )
            {
                _haveTarget = true;
                RunToTheTarget(nearestEnemy.transform);
            }

        }

        private void RunToTheTarget(Transform target)
        {
            _agent.SetDestination(target.position);
        }

        #endregion



        #region Attack

        public void BeginToAttack(Transform target)
        {
            _isAttack = true;
            _agent.ResetPath();
            _animControl.AttackMode();
            _attackTarget = target;
        }

        public virtual void Attack()
        {

            _isAttack = false;
            _haveTarget = false;
        }

        #endregion




        #region Get Damage
        public void GetDamage(int damage)
        {
            _currentHealth -= damage;


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
