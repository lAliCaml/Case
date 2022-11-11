using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;
using Photon.Bolt;

public class CameraControl : MonoBehaviour
{
    public static CameraControl Instance;


    [SerializeField] private Transform _offsetObject;


    [SerializeField] private float _multipleXPos;
    [SerializeField] private float _multipleZPos;

    [SerializeField] private float _multipleZRot;


    [SerializeField] private Transform _realTransform;
    private Vector3 _realTransformStartPos;

    private bool _isChanging;

    [SerializeField] private GameObject _camServer;
    [SerializeField] private GameObject _camClient;



    void Start()
    {

        Instance = this;

        if(BoltNetwork.IsServer)
        {
            _camServer.SetActive(true);
            _camClient.SetActive(false);
        }
        else if(BoltNetwork.IsClient)
        {
            _camServer.SetActive(false);
            _camClient.SetActive(true);
        }




        _realTransformStartPos = _realTransform.position;

    }

    public void ChangeTarget(Transform target)
    {
        if (!_isChanging)
        {
            StartCoroutine(Deneme(target));
        }

    }


    public IEnumerator Deneme(Transform target)
    {
        _isChanging = true;
        _realTransform.DOMove(target.position * .5f, 1);
        yield return new WaitForSeconds(1.75f);
        _realTransform.DOMove(_realTransformStartPos, 1);
        _isChanging = false;
    }


    public void ChangePerspective(float x, float y)
    {
        _offsetObject.position = Vector3.right * x * _multipleXPos + Vector3.forward * y * _multipleZPos;
        _offsetObject.rotation = Quaternion.Euler(Vector3.forward * x * -_multipleZRot);
    }


}
