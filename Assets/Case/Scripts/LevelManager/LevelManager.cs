using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class LevelManager : MonoBehaviour
{
    private AsyncOperationHandle m_SceneHandle;

    [SerializeField]
    private Slider m_LoadingSlider;

    void OnEnable()
    {
        m_SceneHandle = Addressables.DownloadDependenciesAsync("Level_1");
        m_SceneHandle.Completed += OnSceneLoaded;
    }

    private void OnDisable()
    {
        m_SceneHandle.Completed -= OnSceneLoaded;
    }

    private void OnSceneLoaded(AsyncOperationHandle obj)
    {   
        if (obj.Status == AsyncOperationStatus.Succeeded)
        {
            Addressables.LoadSceneAsync("Level_1", UnityEngine.SceneManagement.LoadSceneMode.Single, true);
        }
    }

    private void Update()
    {
        m_LoadingSlider.value = m_SceneHandle.GetDownloadStatus().Percent;
    }
}

