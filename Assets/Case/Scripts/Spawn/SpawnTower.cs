using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Bolt;

public class SpawnTower : MonoBehaviour
{
    [SerializeField] private Vector3 t_l_enemy;
    [SerializeField] private Vector3 t_r_enemy;
    [SerializeField] private Vector3 t_m_enemy;

    [SerializeField] private Vector3 t_l_friend;
    [SerializeField] private Vector3 t_r_friend;
    [SerializeField] private Vector3 t_m_friend;
    void Start()
    {
        if (BoltNetwork.IsServer)
        {
            BoltNetwork.Instantiate(BoltPrefabs.Tower_Friend_Left, t_l_friend, Quaternion.identity);
            BoltNetwork.Instantiate(BoltPrefabs.Tower_Friend_Main, t_m_friend, Quaternion.identity);
            BoltNetwork.Instantiate(BoltPrefabs.Tower_Friend_Right, t_r_friend, Quaternion.identity);
        }

        if (BoltNetwork.IsClient)
        {
            BoltNetwork.Instantiate(BoltPrefabs.Tower_Enemy_Left, t_l_enemy, Quaternion.identity);
            BoltNetwork.Instantiate(BoltPrefabs.Tower_Enemy_Main, t_m_enemy, Quaternion.identity);
            BoltNetwork.Instantiate(BoltPrefabs.Tower_Enemy_Right, t_r_enemy, Quaternion.identity);
        }
    }


}
