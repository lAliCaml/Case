using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(menuName = "SingletonScriptable/List")]
public class ListDownload : ScriptableObject
{
    public string names;

    public bool IsAdsLoaded;
    public bool IsAssetDownloaded;
}

