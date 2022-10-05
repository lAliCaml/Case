using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public AsyncOperationHandle m_SceneHandle;
    [SerializeField] private Text text_Percent;

    public AssetLabelReference[] assetLabel;

    [SerializeField]
    private Slider m_LoadingSlider;

    private bool _isLoaded = false;

    [SerializeField] private Text text_try;

    [SerializeField] private Button button_Play;
    [SerializeField] private Text text_Loading;


    private void OnEnable()
    {
        m_SceneHandle = Addressables.DownloadDependenciesAsync(assetLabel, Addressables.MergeMode.Intersection);

        m_SceneHandle.Completed += OnSceneLoaded;

        StartCoroutine(LoadingText());
    }

    private void OnDisable()
    {
        m_SceneHandle.Completed -= OnSceneLoaded;
    }


    private void OnSceneLoaded(AsyncOperationHandle obj)
    {
        if (obj.Status == AsyncOperationStatus.Succeeded)
        {
            ManagerDownload.ListDownload.names += "Assets loaded" + " \n";
            ManagerDownload.ListDownload.IsAssetDownloaded = true;
        }
    }

    public void GoToNextLevel()
    {
        if(ManagerDownload.ListDownload.IsAdsLoaded && ManagerDownload.ListDownload.IsAssetDownloaded)
        {
            Addressables.LoadSceneAsync("Level_1", LoadSceneMode.Single);

            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        if (!ManagerDownload.ListDownload.IsAssetDownloaded)
        {
            m_LoadingSlider.value = m_SceneHandle.GetDownloadStatus().Percent;
            text_Percent.text = (m_SceneHandle.GetDownloadStatus().DownloadedBytes / (1024  *1024)).ToString() + "mb"+ " / " + (m_SceneHandle.GetDownloadStatus().TotalBytes / (1024 * 1024)).ToString() + "mb";
        }
        

        text_try.text = ManagerDownload.ListDownload.names;

        if (ManagerDownload.ListDownload.IsAdsLoaded && ManagerDownload.ListDownload.IsAssetDownloaded)
        {
            m_LoadingSlider.value = 1;
            button_Play.interactable = true;
        }
    }

    IEnumerator LoadingText()
    {
        while(!ManagerDownload.ListDownload.IsAssetDownloaded)
        {
            text_Loading.text = "Loading";
            if (!ManagerDownload.ListDownload.IsAssetDownloaded)
            {
                yield return new WaitForSeconds(.5f);
            }
            
            text_Loading.text = "Loading.";
            if (!ManagerDownload.ListDownload.IsAssetDownloaded)
            {
                yield return new WaitForSeconds(.5f);
            }
            text_Loading.text = "Loading..";
            if (!ManagerDownload.ListDownload.IsAssetDownloaded)
            {
                yield return new WaitForSeconds(.5f);
            }
            text_Loading.text = "Loading...";
            if (!ManagerDownload.ListDownload.IsAssetDownloaded)
            {
                yield return new WaitForSeconds(.5f);
            }
        }

        text_Loading.text = "Play";
    }
}

