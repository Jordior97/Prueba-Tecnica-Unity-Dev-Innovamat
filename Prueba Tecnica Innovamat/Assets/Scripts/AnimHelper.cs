using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimHelper : MonoBehaviour
{
    public static AnimHelper Instance;

    private void Awake()
    {
        Instance = this;
    }

    // Coroutines
    #region Coroutines

    public IEnumerator AnimCoroutine(float time, Action<float> action, Action callback)
    {
        float currentTime = 0;
        while (time > currentTime)
        {
            action.Invoke(currentTime / time);
            currentTime += Time.deltaTime;
            yield return null;
        }
        action.Invoke(1);
        callback?.Invoke();
    }

    #endregion
}
