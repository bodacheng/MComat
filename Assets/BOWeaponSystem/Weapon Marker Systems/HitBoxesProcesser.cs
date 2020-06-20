using System.Collections.Generic;
using UnityEngine;
using HittingDetection;

public class HitBoxesProcesser : MonoBehaviour
{
    public static HitBoxesProcesser Instance;
    public static Dictionary<Collider, BO_Marker_Manager> ColliderHitBox = new Dictionary<Collider, BO_Marker_Manager>();
    public List<Decompositioner> processingDecompositioners = new List<Decompositioner>();
    
    void Awake()
    {
        Instance = this;
    }

    public static void AddToDecompositionerProcesserList(Decompositioner _poolObject)
    {
        if (Instance != null)
            Instance.AddToHitBoxesProcesserList(_poolObject);
    }
    
    // 用于靠collider索引对应的BO_Marker_Manager，与update内功能无关。
    public static void AddToColliderHitBoxDic(Collider collider, BO_Marker_Manager bo_hitbox)
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
                processingDecompositioners[i].Step1();
            }
            for (int i = 0; i < processingDecompositioners.Count; i++)
            {
                processingDecompositioners[i].Step2();
            }
            for (int i = 0; i < processingDecompositioners.Count; i++)
            {
                processingDecompositioners[i].Life();
            }
            processingDecompositioners.Clear();
        }
    }

    void AddToHitBoxesProcesserList(Decompositioner _poolObject)
    {
        if (!processingDecompositioners.Contains(_poolObject))
            processingDecompositioners.Add(_poolObject);
    }
}
