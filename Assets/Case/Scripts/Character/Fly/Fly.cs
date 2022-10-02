using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Case.Throw;


namespace Case.Characters
{
    public class Fly : Character
    {
        public override void Start()
        {
            _agent.baseOffset = 15;
            base.Start();
        }

        [SerializeField] private GameObject _dragonBall;
        public override void Attack()
        {
            if (_attackTarget != null)
            {

                Vector3 startingPos = transform.position + Vector3.up *20;

                GameObject arrow = Instantiate(_dragonBall, startingPos, Quaternion.identity);

                string name = gameObject.tag;

                _dragonBall.GetComponent<IThrow>().ThrowSettings(startingPos, _attackTarget, _attack, name);
            }

            base.Attack();
        }
    }

}
