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
        _offset += (Vector2.right * 2 + Vector2.up) * Time.deltaTime * .05f;
        mat.SetTextureOffset("_MainTex", _offset);
    }
}
