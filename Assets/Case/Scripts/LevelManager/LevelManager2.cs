using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;


public class LevelManager2 : MonoBehaviour
{
    //public AssetReference[] _operation;
    public AsyncOperationHandle m_SceneHandle;
    [SerializeField] private Text text_Percent;

    public AssetLabelReference[] assetLabel;
    [SerializeField] private List<AssetReference> _references = new List<AssetReference>();

    [SerializeField]
    private Slider m_LoadingSlider;

    private bool _isLoaded = false;

    private string language;

    void OnEnable()
    {
        m_SceneHandle = Addressables.DownloadDependenciesAsync(assetLabel, Addressables.MergeMode.Intersection);


        //Addressables.LoadAssetsAsync<GameObject>(assetLabel, LoadCallback).Completed += LoadingManager_Completed;

        //m_SceneHandle = Addressables.DownloadDependenciesAsync(_references);
           m_SceneHandle.Completed += OnSceneLoaded;
    }

    private void OnDisable()
    {
        m_SceneHandle.Completed -= OnSceneLoaded;
    }

   /* private void LoadingManager_Completed(AsyncOperationHandle obj)
    {
        // We show the UI button once the scene is successfully downloaded
        if (obj.Status == AsyncOperationStatus.Succeeded)
        {
            Debug.Log("Yuklendi");
        }
    }*/

    private void OnSceneLoaded(AsyncOperationHandle obj)
    {
        // We show the UI button once the scene is successfully downloaded
        if (obj.Status == AsyncOperationStatus.Succeeded)
        {
          
        }
    }

    // Function to handle which level is loaded next
    public void GoToNextLevel()
    {
          //  Addressables.LoadSceneAsync("Level_0" + GameManager.s_CurrentLevel, UnityEngine.SceneManagement.LoadSceneMode.Single, true);
    }

    private void Update()
    {
        // We don't need to check for this value every single frame, and certainly not after the scene has been loaded
        m_LoadingSlider.value = m_SceneHandle.GetDownloadStatus().Percent;
        text_Percent.text = (m_SceneHandle.GetDownloadStatus().DownloadedBytes).ToString()+ " / " + m_SceneHandle.GetDownloadStatus().TotalBytes.ToString();

        if (m_SceneHandle.GetDownloadStatus().Percent >= 1 && !_isLoaded)
        {
            _isLoaded = true;
            Addressables.LoadSceneAsync("Level_1");
        }
    }
}
