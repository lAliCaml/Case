using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Case.Throw
{
    public interface IThrow
    {
        public void ThrowSettings(Vector3 startingPos, Transform target, int attack);
    }
}

