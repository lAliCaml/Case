using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Case.Throw;

namespace Case.Characters
{
    public class LongDistance : Character
    {
        [SerializeField] private GameObject _arrow;
        public override void Attack()
        {
            if (_attackTarget != null)
            {

                Vector3 startingPos = transform.position + Vector3.up * 3;

                GameObject arrow = Instantiate(_arrow, startingPos, Quaternion.identity);

                string name = gameObject.tag;

                arrow.GetComponent<IThrow>().ThrowSettings(startingPos, _attackTarget, _attack, name);
            }

            base.Attack();
        }

    }

}


