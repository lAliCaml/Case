using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Case.Throw;
using Photon.Bolt;

namespace Case.Characters
{
    public class LongDistance : Character
    {
        public override void Attack()
        {
            if (_attackTarget != null && entity.IsOwner)
            {

                Vector3 startingPos = transform.position + Vector3.up * 3;

                GameObject arrow = BoltNetwork.Instantiate(BoltPrefabs.Arrow, startingPos, Quaternion.identity);

                string name = gameObject.tag;

                arrow.GetComponent<IThrow>().ThrowSettings(startingPos, _attackTarget, _attack, name);
            }

            base.Attack();
        }

    }

}


