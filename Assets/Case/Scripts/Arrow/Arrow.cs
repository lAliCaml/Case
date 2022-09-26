using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Case.Throw;
using Case.Healty;

public class Arrow : MonoBehaviour, IThrow
{
    private Transform _target;
    private int _attack;



    public void ThrowSettings(Vector3 startingPos, Transform target, int attack)
    {
        transform.position = startingPos;
        _target = target;
        _attack = attack;
    }

    // Update is called once per frame
    void Update()
    {
        if(_target != null)
        {
            transform.LookAt(_target.position + Vector3.up);
            transform.Translate(Vector3.forward * Time.deltaTime * 40);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            other.GetComponent<IHealty>().GetDamage(_attack);
            Destroy(gameObject);
        }
    }
}
