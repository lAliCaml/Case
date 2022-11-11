using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Case.Health;
using Photon.Bolt;

namespace Case.Characters
{
    public class Melee : Character
    {

        public override void Attack()
        {
            if (_attackTarget != null && entity.IsOwner)
            {
                BoltEntity entity = _attackTarget.gameObject.GetComponent<BoltEntity>();

                var evnt = HitEvent.Create(entity);
                evnt.Attack = _attack;
                evnt.Send();
            }
            base.Attack();
        }

    }


}
