using System.Collections;
using UnityEngine.Animations;
using UnityEngine;
using UniRx.Toolkit;
using HittingDetection;

public class DecompositionerPool : ObjectPool<Decompositioner> {
    
    static GameObject Marker;
    readonly GameObject Prefab;

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
        instance.StopEmissions();
        if (instance._HitBox != null)
            instance._HitBox.Local_OnDisable();

        instance.gameObject.SetActive(false);
    }

    protected override void OnBeforeRent(Decompositioner instance)
    {
        instance.gameObject.SetActive(true);
        instance.Local_OnEnable();
        if (instance._HitBox != null)
        {
            instance._HitBox.Local_OnEnable();
        }
    }

    // オブジェクトが空のときにInstantiateする関数
    protected override Decompositioner CreateInstance() 
    {
        GameObject a = Object.Instantiate(Prefab);
        a.transform.SetParent(Marker.transform);
        Decompositioner decompositioner = a.GetComponent<Decompositioner>();
        BO_Marker_Manager bO_Marker_Manager = a.GetComponent<BO_Marker_Manager>();
        TrackControl danMuTest = a.GetComponent<TrackControl>();
        PositionConstraint positionConstraint = a.GetComponent<PositionConstraint>();
        if (positionConstraint == null)
        {
            positionConstraint = a.AddComponent<PositionConstraint>();
            positionConstraint.translationOffset = Vector3.zero;
            positionConstraint.weight = 1;
        }
        decompositioner._HitBox = bO_Marker_Manager;
        decompositioner.SetPositionConstraint(positionConstraint);
        decompositioner.TrackControl = danMuTest;
        decompositioner.SetPool(this);
        return decompositioner;
    }
}
