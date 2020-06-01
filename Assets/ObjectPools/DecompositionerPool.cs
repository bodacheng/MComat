using UnityEngine.Animations;
using UnityEngine;
using UniRx.Toolkit;
using HittingDetection;
using System;
using dataAccess;
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
            for (int index = 0; index < q.Count; index++)
            {
                if (q[index] != null && q[index].Phase < 0) //&& !q[index].gameObject.activeSelf
                {
                    instance = q[index];
                    break;
                }
                if (q[index].gameObject.activeSelf)
                {
                    Debug.Log("不可解错误" + q[index].gameObject + " Phase:"+ q[index].Phase);
                }
                if (q[index].gameObject.activeSelf && q[index].Phase < 0)
                {
                    Debug.Log("超不可解错误" + q[index].gameObject + " Phase:"+ q[index].Phase);
                }
                q[index].Phase--;
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
        if (decompositioner.audioSource != null)
        {
            decompositioner.audioSource.volume = AccountSet._AccInfo.EffectsVolumn;
            decompositioner.audioSource.minDistance = 20;
            decompositioner.audioSource.maxDistance = 80;
        }
        if (BBMM != null)
        {
            BBMM.CurrentHP = BBMM.weaponHP;
            decompositioner._HitBox = BBMM;
            BBMM.SetDecompositioner(decompositioner);
        }
        
        decompositioner.SetPositionConstraint(PC);
        decompositioner.TrackControl = danMuTest;
        decompositioner.SetPool(this);
        return decompositioner;
    }
}
