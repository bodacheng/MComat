using UnityEngine.Animations;
using UnityEngine;
using UniRx.Toolkit;
using HittingDetection;
using System;
using System.Collections.Generic;

public class DecompositionerPool : ObjectPool<Decompositioner> {

    static GameObject Marker;
    readonly GameObject Prefab;

    public DecompositionerPool(GameObject prefab)
    {
        if (Marker == null)
        {
            Marker = new GameObject("Object Pools Container");
            UnityEngine.Object.DontDestroyOnLoad(Marker);
        }
        Prefab = prefab;
    }

    /// <summary>
    /// Get instance from pool.
    /// </summary>
    public override Decompositioner Rent()
    {
        if (isDisposed) throw new ObjectDisposedException("ObjectPool was already disposed.");

        Decompositioner instance = null;
        if (q.Count > 0)
        {
            for (int index = 0; index < q.Count; index++)
            {
                if (q[index] != null && !q[index].gameObject.activeSelf)
                {
                    instance = q[index];
                    break;
                }
                if (q[index].gameObject.activeSelf)
                {
                    Debug.Log("h変ですね" + q[index].gameObject);
                }
            }
        }
        if (instance == null)
        {
            instance = CreateInstance();
        }else{
            q.Remove(instance);
        }
        OnBeforeRent(instance);
        return instance;
    }
        
    protected override void OnBeforeReturn(Decompositioner instance)
    {
        if (FightGlobalSetting.HitBoxLogger)
        {
            if (instance._HitBox != null)
            {
                HitBoxLogger.Instance.AddLog(instance._HitBox.GeneratedByStateKey,instance._HitBox.HitBoxLifeEnding);
                instance._HitBox.GeneratedByStateKey = null;
            }
        }
        instance.Phase = 0;
        base.OnBeforeReturn(instance);
    }

    protected override void OnBeforeRent(Decompositioner instance)
    {
        base.OnBeforeRent(instance);
        instance.Local_OnEnable();
    }
    
    // オブジェクトが空のときにInstantiateする関数
    protected override Decompositioner CreateInstance()
    {
        GameObject a = UnityEngine.Object.Instantiate(Prefab);
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
        Rigidbody rigidbody = a.GetComponent<Rigidbody>();//不加刚体的话很多情况下collider的检测类物理函数检测不到
        if (rigidbody == null)
        {
            rigidbody = a.AddComponent<Rigidbody>();
            rigidbody.isKinematic = true;//这个刚体不受物理影响
        }
        decompositioner._HitBox = bO_Marker_Manager;
        decompositioner.SetPositionConstraint(positionConstraint);
        decompositioner.TrackControl = danMuTest;
        decompositioner.SetPool(this);
        return decompositioner;
    }
}
