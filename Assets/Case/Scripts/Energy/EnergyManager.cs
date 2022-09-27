using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Case.Energy
{
    public class EnergyManager : MonoBehaviour
    {
        public static EnergyManager Instance;

        public int energy;

        void Start()
        {
            Instance = this;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }


}
