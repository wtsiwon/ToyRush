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
            GameObject go;
            if (instance == null)
            {
                go = GameObject.Find(typeof(T).Name);
                if(go == null)
                {
                    go = new GameObject(typeof(T).Name);
                    instance = go.AddComponent<T>();
                }
                else
                {
                    instance = go.GetComponent<T>();
                }
            }
            return instance;
        }
    }

    protected void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void OnDestroy()
    {
        instance = null;
    }

}
