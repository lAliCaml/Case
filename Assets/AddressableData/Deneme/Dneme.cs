using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dneme : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] hitColliders = Physics.OverlapBox(transform.position, Vector3.one*20 / 2); //Create sphere collider for the hit with enemies collider

        Debug.Log(hitColliders.Length);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, Vector3.one * 20);
    }
}
