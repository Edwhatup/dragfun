using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuncTest : MonoBehaviour
{
    public TextAsset text;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(Application.dataPath);
        Debug.Log(Application.persistentDataPath);
    }

}
