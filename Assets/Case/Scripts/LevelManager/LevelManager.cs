using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    [SerializeField] private Slider m_LoadingSlider;
    [SerializeField] private Text text_Percent;
    [SerializeField] private Text text_Info;

    [SerializeField] private AssetReference _scene;
    [SerializeField] private List<AssetReference> _references = new List<AssetReference>();
    private AsyncOperationHandle<SceneInstance> handle;
    public GameObject uIGameObject;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        StartCoroutine(DownloadScene());
    }

    float progress;
    IEnumerator DownloadScene()
    {
        var downloadScene = Addressables.LoadSceneAsync(_scene, LoadSceneMode.Additive);
        //downloadScene.Completed += SceneDownloadComplete;
        text_Info.text = "Starting download";
        Debug.Log("Starting download");

       
        while (!downloadScene.IsDone)
        {
            var status = downloadScene.GetDownloadStatus();
            progress = status.Percent;

            m_LoadingSlider.value = progress;
            text_Percent.text = "%" + ((int)(progress * 100)).ToString();
            Debug.Log((int)(progress * 100));
            yield return null;
        }

        text_Percent.text = "%" + ((int)(progress * 100)).ToString();

        text_Info.text = "Complete download";
        Debug.Log("Complete download");

    }


  /*  private void SceneDownloadComplete(AsyncOperationHandle<SceneInstance> _handle)
    {
        if (_handle.Status == AsyncOperationStatus.Succeeded)
        {
            uIGameObject.SetActive(false);
            handle = _handle;
            text_Info.text = "";
            text_Percent.text = "";

            //   StartCoroutine(UnloadScene());
        }
    }*/

    private IEnumerator UnloadScene()
    {
        yield return new WaitForSeconds(2);
        Debug.Log("Unload Scene start");
        Debug.Log(handle.Result.Scene.name);

        Addressables.UnloadSceneAsync(handle, true).Completed += op =>
        {
            if (op.Status == AsyncOperationStatus.Succeeded)
            {
                uIGameObject.SetActive(true);
                Debug.Log("Unload Scene");
            }
        };
    }
}

