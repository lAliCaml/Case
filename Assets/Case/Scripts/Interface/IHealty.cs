using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Case.Healty
{
    public interface IHealty
    {
        public void GetDamage(int damage);

        public void Death();
    }

}
