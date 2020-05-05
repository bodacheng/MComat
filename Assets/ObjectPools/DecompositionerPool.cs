using UnityEngine.Animations;
using UnityEngine;
using UniRx.Toolkit;
using HittingDetection;
using System;

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
        BO_Marker_Manager BBMM = a.GetComponent<BO_Marker_Manager>();
        TrackControl danMuTest = a.GetComponent<TrackControl>();
        PositionConstraint PC = a.GetComponent<PositionConstraint>();
        if (PC == null)
        {
            PC = a.AddComponent<PositionConstraint>();
            PC.translationOffset = Vector3.zero;
            PC.weight = 1;
        }
        
        Rigidbody RG = a.GetComponent<Rigidbody>();//不加刚体的话很多情况下collider的检测类物理函数检测不到
        if (RG == null)
        {
            RG = a.AddComponent<Rigidbody>();
        }
        RG.isKinematic = true;//这个刚体不受物理影响
        
        if (decompositioner.audioSource == null)
        {
            decompositioner.audioSource = decompositioner.transform.GetComponent<AudioSource>();
        }
        
        decompositioner._HitBox = BBMM;
        decompositioner.SetPositionConstraint(PC);
        decompositioner.TrackControl = danMuTest;
        decompositioner.SetPool(this);
        return decompositioner;
    }
}
