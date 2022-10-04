using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour
    where T : MonoBehaviour
{
    public T instance;

    public T Instance
    {
        get
        {
            return instance = GetComponent<T>();
        }
    }
}
