using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Case.Healty;

namespace Case.Characters
{
    public class Melee : Character
    {

        public override void Attack()
        {
            if (_attackTarget != null)
            {
                _attackTarget.GetComponent<IHealty>().GetDamage(_attack);
            }
            base.Attack();
        }

    }


}
