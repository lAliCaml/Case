using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Case.Healty
{
    public interface IHealty
    {
        public void GetDamage(int damage);

        public int CurrentHealty(int healty, int damage);

        public void Death();
    }

}
