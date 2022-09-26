using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{

    [SerializeField] private Transform[] _points;

    [SerializeField] private GameObject[] _soldiers;
    



    void Start()
    {
        StartCoroutine(RespawnEnemy());
    }


    IEnumerator RespawnEnemy()
    {
        GameObject soldier;
        Vector3 pos;

        while(true)
        {
            soldier = _soldiers[Random.Range(0, _soldiers.Length)];
            pos = _points[Random.Range(0, _points.Length)].position;


            Instantiate(soldier, pos, Quaternion.identity);


            yield return new WaitForSeconds(1);
        }
    }
}
