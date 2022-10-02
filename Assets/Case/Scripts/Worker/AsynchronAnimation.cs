using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsynchronAnimation : MonoBehaviour
{
    [SerializeField] private Animator _anim;
    void Start()
    {
        _anim.SetFloat("SpeedP", Random.Range(.85f, 1.15f));
        _anim.enabled = false;
        StartCoroutine(ActiveAnimator());
    }

    IEnumerator ActiveAnimator()
    {
        yield return new WaitForSeconds(Random.Range(0f, .35f));
        _anim.enabled = true;
    }
}
