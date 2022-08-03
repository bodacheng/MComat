using System.Collections.Generic;
using UnityEngine;
using HittingDetection;

public class HitBoxesProcesser : MonoBehaviour
{
    public static HitBoxesProcesser Instance;
    
    private static Dictionary<Collider, HitBoxManager> ColliderHitBox = new Dictionary<Collider, HitBoxManager>();
    private List<Decomposition> processingDecompositioners = new List<Decomposition>();
    
    void Awake()
    {
        Instance = this;
    }

    public void Clear()
    {
        processingDecompositioners.Clear();
    }

    public HitBoxManager GetHitBox(Collider c)
    {
        ColliderHitBox.TryGetValue(c, out HitBoxManager hit_hitbox);
        return hit_hitbox;
    }
    
    public static void AddToDecompositionerProcesserList(Decomposition _poolObject)
    {
        if (Instance != null)
            Instance.AddToHitBoxesProcesserList(_poolObject);
    }
    
    // 用于靠collider索引对应的BO_Marker_Manager，与update内功能无关。
    public static void AddToColliderHitBoxDic(Collider collider, HitBoxManager bo_hitbox)
    {
        if (!ColliderHitBox.ContainsKey(collider))
        {
            ColliderHitBox.Add(collider, bo_hitbox);
        }
    }

    void Update()
    {
        if (processingDecompositioners.Count > 0)
        {
            for (int i = 0; i < processingDecompositioners.Count; i++)
            {
                if (processingDecompositioners[i] == null)
                {
                    Debug.Log("队列错误");
                    processingDecompositioners.Clear();
                    return;
                }
                processingDecompositioners[i].Step1();
            }
            for (int i = 0; i < processingDecompositioners.Count; i++)
            {
                if (processingDecompositioners[i] == null)
                {
                    Debug.Log("队列错误");
                    processingDecompositioners.Clear();
                    return;
                }
                processingDecompositioners[i].Step2();
            }
            for (int i = 0; i < processingDecompositioners.Count; i++)
            {
                if (processingDecompositioners[i] == null)
                {
                    Debug.Log("队列错误");
                    processingDecompositioners.Clear();
                    return;
                }
                processingDecompositioners[i].Life();
            }
            processingDecompositioners.Clear();
        }
    }

    void AddToHitBoxesProcesserList(Decomposition _poolObject)
    {
        if (!processingDecompositioners.Contains(_poolObject))
            processingDecompositioners.Add(_poolObject);
    }
}
