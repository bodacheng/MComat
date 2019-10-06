using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx.Toolkit;
using HittingDetection;

public class DecompositionerPool : ObjectPool<Decompositioner> {
    
    static GameObject Marker;
    private readonly GameObject Prefab;
        
    public DecompositionerPool(GameObject prefab)
    {
        if (Marker == null)
        {
            Marker = new GameObject("Object Pools Container");
            Object.DontDestroyOnLoad(Marker);
        }
        Prefab = prefab;
    }
    
    protected override void OnBeforeReturn(Decompositioner instance)
    {
        instance.gameObject.SetActive(false);
        instance.transform.SetParent(Marker.transform);
    }

    // オブジェクトが空のときにInstantiateする関数
    protected override Decompositioner CreateInstance() 
    {
        GameObject a = Object.Instantiate(Prefab);
        a.transform.SetParent(Marker.transform);
        Decompositioner decompositioner = a.GetComponent<Decompositioner>();
        BO_Marker_Manager bO_Marker_Manager = a.GetComponent<BO_Marker_Manager>();
        Rigidbody rigidbody = a.GetComponent<Rigidbody>();
        decompositioner.rigidbody = rigidbody;
        decompositioner._HitBox = bO_Marker_Manager;
        decompositioner.setPool(this);
        return decompositioner;
    }
}
