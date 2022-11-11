using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Case.Throw;
using Case.Health;
using Case.Managers;
using Case.BorderControl;
using DG.Tweening;
using Photon.Bolt;


namespace Case.Towers
{
    public class Tower : EntityEventListener<ITower>, IHealty
    {
        #region Variables
        [Header("CharacterFeatures")]
        private int _health;
        private int _attack;
        private Vector3 _scanArea;

        [Header("Components")]
        [SerializeField] private HealtyBar _healtBar;

        [Header("HitEffect")]
        [SerializeField] private ParticleSystem _deathEffect;



        [SerializeField] private TowerProperties _towerProperties; //ScriptableObject

        private bool _isPunchScale;

        [SerializeField] private bool _isMainTower;
        [SerializeField] private bool _isRightTower;
        [SerializeField] private bool _isLeftTower;


        [SerializeField] private GameObject Arrow;



        #endregion

        #region Define Properties

        void Start()
        {
            _health = _towerProperties.Healty;
            _attack = _towerProperties.Attack;
            _scanArea = _towerProperties.ScanArea;

            if (entity.IsOwner)
            {
                state.Health = _health;
            }
           


            if (entity.IsOwner)
            {
                StartCoroutine(FindEnemy());
            }
        }

        #endregion

        #region Server

        public override void Attached()
        {
            state.AddCallback("Health", HealthCallBack);
        }

        public override void OnEvent(HitEvent evnt) => GetDamage(evnt.Attack);

        private void HealthCallBack()
        {
            if (state.Health > 0)
            {
                float healthRate = (float)state.Health / (float)_health;
                _healtBar.HealthBarShow(healthRate);

                StartCoroutine(PunchScale());
            }
            else
            {
                Death();
            }

            Camera.main.DOShakePosition(0.1f, Vector3.one * .075f, 0, 0f, false);
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
            Collider[] hitColliders = Physics.OverlapBox(transform.position, _scanArea);


            GameObject nearestEnemy = null;
            float scanArea = 35;

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

            if (nearestEnemy != null)
            {
                Attack(nearestEnemy.transform);
            }

        }

        /* private void OnDrawGizmos() 
         {
             Gizmos.color = Color.red;
             Gizmos.DrawWireCube(transform.position, _towerProperties.ScanArea);
         }*/


        #endregion

        #region Attack

        public virtual void Attack(Transform target)
        {
            Vector3 startingPos = transform.position + Vector3.up * 3;

            GameObject arrow = BoltNetwork.Instantiate(BoltPrefabs.Arrow, transform.position, Quaternion.identity);

            string name = gameObject.tag;

            arrow.GetComponent<IThrow>().ThrowSettings(startingPos, target, _attack, name);

            if (transform.CompareTag("Friend"))
            {
                CameraControl.Instance.ChangeTarget(transform);
            }
        }


        #endregion





        #region Health

        public void GetDamage(int damage)
        {
            if(entity.IsOwner)
            {
                state.Health -= damage;
            }
           
            BoltNetwork.Instantiate(BoltPrefabs.HitEffect, transform.position, Quaternion.identity);
        }

        public void Death()
        {
            BoltNetwork.Instantiate(BoltPrefabs.DeathEffect, transform.position, Quaternion.identity);  //Instantiate(_deathEffect, transform.position, Quaternion.identity);

            if (_isMainTower)
            {
                var evnt = FinishGame.Create();
                evnt.Name = gameObject.tag;
                evnt.Send();
            }
            else if (_isLeftTower)
            {
                if (BoltNetwork.IsServer && gameObject.CompareTag("Enemy"))
                {
                    BorderManager.Instance.ChangeBorder("Left");
                }
                else if (BoltNetwork.IsClient && gameObject.CompareTag("Friend"))
                {
                    BorderManager.Instance.ChangeBorder("Left");
                }
            }
            else if (_isRightTower)
            {
                if (BoltNetwork.IsServer && gameObject.CompareTag("Enemy"))
                {
                    BorderManager.Instance.ChangeBorder("Right");
                }
                else if (BoltNetwork.IsClient && gameObject.CompareTag("Friend"))
                {
                    BorderManager.Instance.ChangeBorder("Right");
                }
            }
            BoltNetwork.Destroy(gameObject);
        }

        IEnumerator PunchScale()
        {
            if (!_isPunchScale)
            {
                _isPunchScale = true;
                transform.DOPunchScale(Vector3.one * .2f, .1f);

                yield return new WaitForSeconds(.1f);
                _isPunchScale = false;
            }

        }
        #endregion
    }

}
