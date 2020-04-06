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
        if (FightGlobalSetting.HitBoxLogger)
        {
            if (instance._HitBox != null)
            {
                HitBoxLogger.Instance.AddLog(instance._HitBox.GeneratedByStateKey,instance._HitBox.HitBoxLifeEnding);
                instance._HitBox.GeneratedByStateKey = null;
            }
        }
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
