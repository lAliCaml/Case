using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI;


public class FpsManager : MonoBehaviour
{
    [SerializeField] private Text text_Fps;


    private void Start()
    {
        StartCoroutine(Writer());
    }

    IEnumerator Writer()
    {
        while(true)
        {
            text_Fps.text = ((int)(1 / Time.deltaTime)).ToString();
            yield return new WaitForSeconds(1);
        }
    }
}
