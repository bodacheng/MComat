using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceKeeper : MonoBehaviour
{
    public Transform _dontDestroyOnLoadParent;
    public static Transform dontDestroyOnLoadParent;
    
    void Awake()
    {
        if (dontDestroyOnLoadParent == null)
            dontDestroyOnLoadParent = _dontDestroyOnLoadParent;
        else
            Debug.Log("已经找到非销毁对象parent");
        DontDestroyOnLoad(dontDestroyOnLoadParent);
    }
}