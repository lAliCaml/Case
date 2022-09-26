using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Case.Healty;

namespace Case.Characters
{
    public class Character : MonoBehaviour, IHealty
    {
        [Header("CharacterFeatures")]
        private int _health;
        private int _attack;
        private float _speed;
        private float _range;

        private int _currentHealth;
        public float HealthRate;



        [SerializeField] private CharacterProperties _characterProperties;

        void Start()
        {
            _health = _characterProperties.Healty;
            _attack = _characterProperties.Attack;
            _speed = _characterProperties.Speed;
            _range = _characterProperties.Range;

            _currentHealth = _health;
        }

        public virtual void Attack()
        {

        }







        public void GetDamage(int damage)
        {
            HealthRate = CurrentHealty(_currentHealth, damage);
        }

        public int CurrentHealty(int currentHealth, int damage)
        {
            int value = currentHealth - damage;
            int rate = value / _health;

            if (value > 0)
            {
                return rate;
            }
            else
            {
                Death();
            }
            return value;
        }

        public void Death()
        {
            Debug.Log("Die");
            Destroy(gameObject);
        }
    }

}
