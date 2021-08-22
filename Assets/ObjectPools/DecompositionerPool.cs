using UnityEngine.Animations;
using UnityEngine;
using UniRx.Toolkit;
using HittingDetection;
using System;
using dataAccess;
using System.Collections.Generic;
using Log;

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
    /// Return instance to pool.
    /// </summary>
    public override void Return(Decompositioner instance)
    {
        if (isDisposed) throw new ObjectDisposedException("ObjectPool was already disposed.");
        if (instance == null) throw new ArgumentNullException("instance");
        if (q == null) q = new List<Decompositioner>();       
        if ((q.Count + 1) == MaxPoolCount)
        {
            throw new InvalidOperationException("Reached Max PoolSize");
        }
        OnBeforeReturn(instance);
        if (!q.Contains(instance))
            q.Add(instance);
        else{
            Debug.Log(" 邪门了："+ instance);
        }
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
            instance = q[0];
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
            if (instance.IsWeapon)
            {
                HitBoxLogger.Instance.AddLog(instance._HitBox.GeneratedByStateKey, instance._HitBox.HitBoxLifeEnding);
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
        HitBoxManager BBMM = a.GetComponent<HitBoxManager>();
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
        if (decompositioner.audioSource != null)
        {
            decompositioner.audioSource.volume = AppSetting.value.EffectsVolumn;
            decompositioner.audioSource.minDistance = 20;
            decompositioner.audioSource.maxDistance = 80;
        }
        
        if (BBMM != null)
        {
            BBMM.CurrentHP = BBMM.weaponHP;
            decompositioner._HitBox = BBMM;
            BBMM.SetDecompositioner(decompositioner);
        }
        decompositioner.IsWeapon = decompositioner._HitBox != null;
        decompositioner.SetPositionConstraint(PC);
        decompositioner.TrackControl = danMuTest;
        decompositioner.SetPool(this);
        return decompositioner;
    }
}
