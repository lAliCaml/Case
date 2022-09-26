using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Case.Towers
{
    [CreateAssetMenu(fileName = ("new Tower"), menuName = ("Tower"))]
    public class TowerProperties : ScriptableObject
    {
        public int Healty;
        public int Attack;
        public float ScanArea;
    }

}
