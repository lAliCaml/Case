using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Case.Healty;
using Case.Health;

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
        [SerializeField] private HealtyBar _healtBar;

        
        private int _currentHealth;

        private bool _isAttack;            //When character reach to the enemey
        private bool _haveTarget;         //Because run towards enemy
        protected Transform _attackTarget; 



        [SerializeField] private CharacterProperties _characterProperties;  //ScriptableObject
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

            _agent.speed = _speed;
            _isAttack = false;
            _haveTarget = false;

            StartCoroutine(RunSettings());
            StartCoroutine(FindEnemy());
        }

        public void Initialize(string tag)
        {
            gameObject.tag = tag;

        }
        #endregion



        #region Run

        /*
         * If there are no enemy around the current character 
         */
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

        private void RunToTheTarget(Transform target)
        {
            _agent.SetDestination(target.position);
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
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, _scanArea); //Create sphere collider for the hit with enemies collider

            GameObject nearestEnemy = null;
            float scanArea = _scanArea;

            /*
             * Calculate hit colliders distance and find which one is closest
             */
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


            /*
             * If enemy is not too far away character will be attack
             * else character will run towards enemey
             */
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

        

        #endregion



        #region Attack

        public void BeginToAttack(Transform target)
        {
            _isAttack = true;
            _agent.ResetPath();
            _animControl.AttackMode();
            _attackTarget = target;
        }

        public void Rotate()
        {
            if(_attackTarget!= null)
            {
                Quaternion targetRotate = Quaternion.LookRotation(_attackTarget.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotate, Time.deltaTime * 200);
            }
        }


        /*
         * Attack moments 
         * this funtions interact with AttackStateTime 
         */
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
                float healthRate = (float)_currentHealth / (float)_health;
                _healtBar.HealthBarShow(healthRate);
            }
            else
            {
                Death();
            }
        }

        public void Death()
        {
            Destroy(gameObject);
        }

        #endregion
    }

}
