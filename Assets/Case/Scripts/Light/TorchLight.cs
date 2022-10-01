using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


namespace Case.LightControl
{
    public class TorchLight : MonoBehaviour
    {
        [SerializeField] private float _maxValue;

        [SerializeField] private Light _light;
        

        void Start()
        {
            _light.DOIntensity(_maxValue, Random.Range(0.3f, .5f)).SetLoops(500, LoopType.Yoyo); 
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}
