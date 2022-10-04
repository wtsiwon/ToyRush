using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour
    where T : MonoBehaviour
{
    public static T instance;

    public static T Instance { get; private set; } = null;

    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = GetComponent<T>();
        }
    }

    private void OnDestroy()
    {
        instance = null;
    }

}
