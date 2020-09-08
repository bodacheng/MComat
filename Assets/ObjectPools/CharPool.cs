using UnityEngine;
using UniRx.Toolkit;
using System;
using System.Collections.Generic;

public class CharPool : ObjectPool<Data_Center> {

    static GameObject Marker;
    readonly Data_Center Prefab;

    public CharPool(Data_Center prefab)
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
    public override void Return(Data_Center instance)
    {
        if (isDisposed) throw new ObjectDisposedException("ObjectPool was already disposed.");
        if (instance == null) throw new ArgumentNullException(nameof(instance));
        if (q == null) q = new List<Data_Center>();       
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
    public override Data_Center Rent()
    {
        if (isDisposed) throw new ObjectDisposedException("ObjectPool was already disposed.");
        Data_Center instance = null;
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
    
    protected override void OnBeforeReturn(Data_Center instance)
    {
        base.OnBeforeReturn(instance);
    }

    protected override void OnBeforeRent(Data_Center instance)
    {
        base.OnBeforeRent(instance);
    }
    
    // オブジェクトが空のときにInstantiateする関数
    protected override Data_Center CreateInstance()
    {
        GameObject a = UnityEngine.Object.Instantiate(Prefab.WholeT.gameObject);
        OutsideDataLink _ODL = a.GetComponent<OutsideDataLink>();
        return _ODL._C;
    }
}