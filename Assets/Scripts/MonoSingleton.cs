using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    public static T Instance;
    public void Awake() 
    {

        if (Instance != null)
            Destroy(gameObject);
        else
        {
            Instance = this as T;
        }
    }
}
