using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class Singleton<T> : MonoBehaviour
    where T : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();
                if (instance == null)
                {
                    GameObject go = new GameObject(typeof(T).Name);
                    instance = go.AddComponent<T>();
                }
            }
            return instance;
        }
    }

    protected void Awake()
    {
        //if(instance != null)
        //{
        //    Destroy(gameObject);
        //}
        DontDestroyOnLoad(gameObject);
    }

    private void OnDestroy()
    {
        instance = null;
    }
}
