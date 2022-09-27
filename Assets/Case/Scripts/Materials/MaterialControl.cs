using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialControl : MonoBehaviour
{
    [SerializeField] private Material mat;
    private Vector2 _offset;
    void Start()
    {
        _offset = Vector2.one;
    }

    // Update is called once per frame
    void Update()
    {
        _offset += (Vector2.right * Random.Range(0f, .1f) + Vector2.up * Random.Range(0f, .1f)) * Time.deltaTime * 1;
        mat.SetTextureOffset("_MainTex", _offset);
    }
}
