using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Case.Throw;
using Case.Health;
using Photon.Bolt;
using Case.Characters;

public class Arrow : EntityBehaviour<IThrowable>, IThrow
{
    public Transform _target;
    private int _attack;

    public void ThrowSettings(Vector3 startingPos, Transform target, int attack, string tag)
    {
        transform.position = startingPos;
        _target = target;
        _attack = attack;
        gameObject.tag = tag;
        state.Tag = tag;
        StartCoroutine(FollowTarget());
    }

    public override void Attached()
    {
        state.SetTransforms(state.Transform, transform);

        state.AddCallback("Tag", TagCallBack);
    }

    private void TagCallBack()
    {
        gameObject.tag = state.Tag;
    }


    IEnumerator FollowTarget()
    {
        while (true)
        {
            if (_target != null)
            {
                transform.LookAt(_target.position + Vector3.up);
                transform.Translate(Vector3.forward * Time.deltaTime * 30);
            }
            else
            {
                BoltNetwork.Destroy(gameObject);
                break;
            }
            yield return null;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.CompareTag("Friend") && other.CompareTag("Enemy"))
        {
            BoltEntity entity = other.gameObject.GetComponent<BoltEntity>();

            var evnt = HitEvent.Create(entity);
            evnt.Attack = _attack;
            evnt.Send();

            BoltNetwork.Destroy(gameObject);
        }
        else if (gameObject.CompareTag("Enemy") && other.CompareTag("Friend"))
        {
            BoltEntity entity = other.gameObject.GetComponent<BoltEntity>();

            var evnt = HitEvent.Create(entity);
            evnt.Attack = _attack;
            evnt.Send();

            BoltNetwork.Destroy(gameObject);
        }
    }

}
