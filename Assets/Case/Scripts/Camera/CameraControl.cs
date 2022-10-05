using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class CameraControl : MonoBehaviour
{
    public static CameraControl Instance;

    
    [SerializeField] private Transform _offsetObject;


    [SerializeField] private float _multipleXPos;
    [SerializeField] private float _multipleZPos;

    [SerializeField] private float _multipleZRot;

    private Vector3 _virtualCamStartPos;
    [SerializeField] private Transform _virtualCamPos;

    public CinemachineVirtualCamera cam;
    [SerializeField] private Transform _realTransform;
    private Vector3 _realTransformStartPos;

    private bool _isChanging;


    void Start()
    {
        Instance = this;
        _virtualCamStartPos = _virtualCamPos.position;
        _realTransformStartPos = _realTransform.position;

    }

    public IEnumerator Deneme(Transform target)
    {
        _realTransform.DOMove(target.position,1);
         yield return new WaitForSeconds(1.75f);
        _realTransform.DOMove(_realTransformStartPos, 1);
    }


    public void ChangePerspective(float x, float y)
    {
        _offsetObject.position = Vector3.right * x * _multipleXPos + Vector3.forward * y * _multipleZPos;
        _offsetObject.rotation = Quaternion.Euler(Vector3.forward * x * - _multipleZRot);
    }

    public void ChangeTarget(Transform target)
    {
        StartCoroutine(Deneme(target));
    }
}
