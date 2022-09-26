using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy") && gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
        else if (other.CompareTag("Player") && gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
