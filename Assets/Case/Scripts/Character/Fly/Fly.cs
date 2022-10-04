using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Case.Throw;


namespace Case.Characters
{
    public class Fly : Character
    {
        [SerializeField] private GameObject _dragonBall;

        public override void Start()
        {
            base.Start();
            _agent.baseOffset = 6;
            
        }

        
        public override void Attack()
        {
            if (_attackTarget != null)
            {
                Vector3 startingPos = transform.position  + Vector3.forward * 3 + Vector3.up * 3;

                GameObject arrow = Instantiate(_dragonBall, startingPos, Quaternion.identity);

                string name = gameObject.tag;

                arrow.GetComponent<IThrow>().ThrowSettings(startingPos, _attackTarget, _attack, name);
            }

            base.Attack();
        }
    }

}
