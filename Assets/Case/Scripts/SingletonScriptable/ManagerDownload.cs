using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SingletonScriptable/Master")]
public class ManagerDownload : SingletonScriptableObject<ManagerDownload>
{
    [SerializeField] private ListDownload _listDownload;

    public static ListDownload ListDownload
    {
        get
        {
            return Instance._listDownload;
        }
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
    private static void FirstInitialize()
    {
        ListDownload.IsAdsLoaded = false;
        ListDownload.IsAssetDownloaded = false;
    }
}
