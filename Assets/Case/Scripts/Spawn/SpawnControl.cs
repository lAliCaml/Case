using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Case.Managers;
using Photon.Bolt;

namespace Case.Spawn
{
    public class SpawnControl : GlobalEventListener
    {

        [SerializeField] private Transform[] _points;

        private List<GameObject> _enemies = new List<GameObject>();

        public int TimeToSpawn;


        public override void OnEvent(HandleBegin evnt)
        {
            if (BoltNetwork.IsServer)
            {
                StartCoroutine(Spawn());
            }
        }


        IEnumerator Spawn()
        {
            GameObject obj;
            Vector3 pos;
            while(GameManager.Instance.isContinue)
            {
               // obj = _enemies[Random.Range(0, 1)];
                pos = _points[Random.Range(0, _points.Length)].position;
                BoltNetwork.Instantiate(BoltPrefabs.Archer, pos, Quaternion.Euler(Vector3.up * 180));
                yield return new WaitForSeconds(TimeToSpawn);
            }
        }
    }

}
