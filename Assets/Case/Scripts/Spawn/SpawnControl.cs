using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Case.Spawn
{
    public class SpawnControl : MonoBehaviour
    {
        [SerializeField] private Transform[] _points;
        [SerializeField] private GameObject[] _enemies;

        public int TimeToSpawn;

        void Start()
        {
            StartCoroutine(Spawn());
        }

        IEnumerator Spawn()
        {
            GameObject obj;
            Vector3 pos;
            while(true)
            {
                obj = _enemies[Random.Range(0, _enemies.Length)];
                pos = _points[Random.Range(0, _points.Length)].position;
                Instantiate(obj, pos, Quaternion.Euler(Vector3.up * 180));
                yield return new WaitForSeconds(TimeToSpawn);
            }
        }
    }

}
