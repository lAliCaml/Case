using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Case.Health;
using Case.Managers;
using DG.Tweening;
using Photon.Bolt;

namespace Case.Characters
{
    public abstract class Character : EntityEventListener<ICharacterState>, IHealty
    {
        #region Variables
        [Header("CharacterFeatures")]
        private int _health;
        protected int _attack;
        private float _speed;
        private Vector3 _scanOffset;
        private Vector3 _scanArea;
        private float _attackRange;

        [Header("Components")]
        [SerializeField] protected NavMeshAgent _agent;
        [SerializeField] private AnimControl _animControl;
        [SerializeField] private HealtyBar _healtBar;


        private int _currentHealth;

        private bool _isAttack;            //When character reach to the enemey
        private bool _haveTarget;         //Because run towards enemy
        protected Transform _attackTarget;



        [SerializeField] private CharacterProperties _characterProperties;  //ScriptableObject
        #endregion

        #region  Define Properties
        public virtual void Start()
        {
            _health = _characterProperties.Healty;
            _attack = _characterProperties.Attack;
            _speed = _characterProperties.Speed;
            _scanOffset = _characterProperties.ScanOffset;
            _scanArea = _characterProperties.ScanArea;
            _attackRange = _characterProperties.AttackRange;

            _currentHealth = _health;

            if(entity.IsOwner)
            {
                state.Health = _currentHealth;
            }
         

            _agent.speed = _speed;
            _isAttack = false;
            _haveTarget = false;

            //Scale
            transform.localScale = Vector3.zero;
            transform.DOScale(Vector3.one, .15f);
        }

        public void Initialize(string tag)
        {
            state.Tag = tag;
            gameObject.tag = tag;

            if (tag == "Enemy")
            {
                transform.rotation = Quaternion.Euler(Vector3.up * 180);
            }


            CameraControl.Instance.ChangeTarget(transform);

            if (entity.IsOwner)
            {
                StartCoroutine(RunSettings());
                StartCoroutine(FindEnemy());
            }

        }
        #endregion


        #region Server
        public override void Attached()
        {
            state.SetTransforms(state.Transform, transform);

            state.AddCallback("Tag", TagCallBack);
            state.AddCallback("Health", HealthCallBack);
        }

        public override void OnEvent(HitEvent evnt) => GetDamage(evnt.Attack);

        private void HealthCallBack()
        {
            _currentHealth = state.Health;

            if (_currentHealth > 0)
            {
                float healthRate = (float)state.Health / (float)_health;
                _healtBar.HealthBarShow(healthRate);
            }
            else
            {
                BoltNetwork.Destroy(gameObject);
            }
        }

        private void TagCallBack()
        {
            gameObject.tag = state.Tag;
            _healtBar.Initialize();
        }

        #endregion


        #region Run

        /*
         * If there are no enemy around the current character 
         */
        IEnumerator RunSettings()
        {
            while (GameManager.Instance.isContinue)
            {
                if (!_isAttack && !_haveTarget)
                {
                    if (gameObject.CompareTag("Friend"))
                    {
                        _agent.enabled = true;
                        _agent.SetDestination(transform.position + Vector3.forward * 25 - Vector3.up * transform.position.y);
                        _animControl.RunMode();
                    }
                    else if (gameObject.CompareTag("Enemy"))
                    {
                        _agent.enabled = true;
                        _agent.SetDestination(transform.position - Vector3.forward * 25 - Vector3.up * transform.position.y);
                        _animControl.RunMode();
                    }
                }
                yield return new WaitForSeconds(.25f);
            }

            if (_agent.isActiveAndEnabled)
            {
                _agent.ResetPath();
            }

            _animControl.IdleMode();
        }

        private void RunToTheTarget(Transform target)
        {
            _agent.enabled = true;
            _agent.SetDestination(target.position);
            _animControl.RunMode();
        }

        #endregion



        #region Find Enemy

        IEnumerator FindEnemy()
        {
            yield return new WaitForSeconds(.1f);
            while (!_isAttack)
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
            Collider[] hitColliders = Physics.OverlapBox(transform.position + _scanOffset, _scanArea / 2); //Create sphere collider for the hit with enemies collider

            GameObject nearestEnemy = null;
            //  float scanArea = _scanArea;
            float scanArea = 100;

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
            else if (nearestEnemy != null && Vector3.Distance(nearestEnemy.transform.position, transform.position) >= _attackRange)
            {
                _haveTarget = true;
                RunToTheTarget(nearestEnemy.transform);
            }
            else
            {
                _haveTarget = false;
            }

        }

        /* private void OnDrawGizmos() 
         {
             Gizmos.color = Color.red;
             Gizmos.DrawWireCube(transform.position + _scanOffset, _scanArea);
         }*/

        #endregion



        #region Attack

        public void BeginToAttack(Transform target)
        {

            if (_agent.enabled)
            {
                _agent.ResetPath();
            }
            _agent.enabled = false;


            _isAttack = true;
            _animControl.AttackMode();
            _attackTarget = target;

            CameraControl.Instance.ChangeTarget(transform);
        }

        public void Rotate()
        {
            if (_attackTarget != null)
            {
                Quaternion targetRotate = Quaternion.LookRotation(Vector3.right * (_attackTarget.position.x - transform.position.x) + Vector3.forward * (_attackTarget.position.z - transform.position.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotate, Time.deltaTime * 200);
            }
        }


        /*
         * Attack moments 
         * this funtions interact with AttackStateTime 
         */
        public virtual void Attack()
        {
            if (entity.IsOwner)
            {
                _agent.enabled = true;
                _isAttack = false;
                _haveTarget = false;

                StartCoroutine(FindEnemy());
            }
        }

        #endregion




        #region Get Damage
        public void GetDamage(int damage)
        {
            if (entity.IsOwner)
            {
                state.Health -= damage;
            }
           
        }
        #endregion
    }

}
