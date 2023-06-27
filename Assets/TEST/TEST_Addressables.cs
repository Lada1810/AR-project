using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class TEST_Addressables : MonoBehaviour
{
    public string key;

    private void Start()
    {
        StartCoroutine(LoadObject());
    }

    private IEnumerator LoadObject()
    {
        var loadingOperation = Addressables.LoadAssetAsync<GameObject>(key);
        while (!loadingOperation.IsDone)
        {
            yield return null;
        }

        var prefab = loadingOperation.Result;
        Instantiate(prefab);
    }
}
