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

    public AssetLabelReference[] assetLabel;
    [SerializeField] private AssetReference _scene;
    [SerializeField] private List<AssetReference> _references = new List<AssetReference>();
    private AsyncOperationHandle<SceneInstance> handle;
    public GameObject uIGameObject;
    float progress;


    [SerializeField] private Text text_try;


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        StartCoroutine(DownloadScene());
    }

   
    IEnumerator DownloadScene()
    {
        var downloadScene = Addressables.LoadSceneAsync("Level_1", LoadSceneMode.Single);
        downloadScene.Completed += SceneDownloadComplete;
        Debug.Log("Starting download");

       
        while (!downloadScene.IsDone)
        {
            var status = downloadScene.GetDownloadStatus();
            progress = status.Percent;

            m_LoadingSlider.value = progress;
            text_Percent.text = (downloadScene.GetDownloadStatus().DownloadedBytes).ToString() + " / " + downloadScene.GetDownloadStatus().TotalBytes.ToString(); // "%" + ((int)(progress * 100)).ToString();
            yield return null;
        }

        text_Percent.text = "%" + (100).ToString();

        Debug.Log("Complete download");

    }

    public void Update()
    {
        text_try.text = ManagerDownload.ListDownload.names;
    }


    private void SceneDownloadComplete(AsyncOperationHandle<SceneInstance> _handle)
    {
        if (_handle.Status == AsyncOperationStatus.Succeeded)
        {
            uIGameObject.SetActive(false);
            handle = _handle;
            text_Percent.text = "Tamamlandý";
            ManagerDownload.ListDownload.names += "Level loaded" + " \n";


            Destroy(this.gameObject);
            //   StartCoroutine(UnloadScene());
        }
    }

  /*  private IEnumerator UnloadScene()
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
    }*/
}

