using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Case.Characters
{
    [CreateAssetMenu(fileName = ("new Character"), menuName = ("Character"))]
    public class CharacterProperties : ScriptableObject
    {
        public int Healty;
        public int Attack;
        public float Speed;

        [Header("Attack Values")]
        public Vector3 ScanOffset;
        public Vector3 ScanArea; //Because of detect enemy around 
        public float AttackRange; //Because of attack enemy

        [Header("Create Character")]
        public GameObject Character;
        public float Energy;


    }
}
