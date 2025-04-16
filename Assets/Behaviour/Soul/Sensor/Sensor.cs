using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

public partial class Sensor
{
    LayerMask _meAndEnemyLayerMask;
    TeamConfig _teamConfig = TeamConfig.DefaultSet;
    static readonly IDictionary<Team, List<Data_Center>> SharedUnitDic = new Dictionary<Team, List<Data_Center>>();
    static readonly IDictionary<Team, List<Data_Center>> SharedDeadUnitDic = new Dictionary<Team, List<Data_Center>>();
    readonly List<Collider> _detectedEnemies = new List<Collider>();
    Collider _nearestEnemyCollider;
    readonly List<Collider> _damagingWeaponAround = new List<Collider>();
    Collider _nearestDamagingWeapon;    
    Data_Center _selfDataCenter;
    Collider _jiaMateAmMate, _nearestEnemy;
    public float SensorRadius
    {
        get;
        set;
    }
    
    public Transform Center
    {
        get;
        set;
    }

    public void SetDetectLayer(TeamConfig teamConfig, Data_Center self)
    {
        _teamConfig = teamConfig;
        _meAndEnemyLayerMask = teamConfig.myTeamAndMyEnemy;
        _selfDataCenter = self;
    }
    
    public static void ClearFightingMember()
    {
        SharedUnitDic.Clear();
        SharedDeadUnitDic.Clear();
    }
    
    public static void AddOrRemoveSharedUnitInfo(Data_Center member, Team team, bool add) // add:true remove: false
    {
        if (!SharedUnitDic.ContainsKey(team))
            SharedUnitDic.Add(team, new List<Data_Center>());
        var fightingUnits = SharedUnitDic[team];
        if (add)
        {
            if (!fightingUnits.Contains(member))
            {
                fightingUnits.Add(member);
            }
        }
        else
        {
            if (fightingUnits.Contains(member))
            {
                fightingUnits.Remove(member);
            }
        }
        SharedUnitDic[team] = fightingUnits;
    }
    
    public static void AddOrRemoveSharedDeadUnitInfo(Data_Center member, Team team, bool add) // add:true remove: false
    {
        if (!SharedDeadUnitDic.ContainsKey(team))
            SharedDeadUnitDic.Add(team, new List<Data_Center>());
        var fightingUnits = SharedDeadUnitDic[team];
        if (add)
        {
            if (!fightingUnits.Contains(member))
            {
                fightingUnits.Add(member);
            }
        }
        else
        {
            if (fightingUnits.Contains(member))
            {
                fightingUnits.Remove(member);
            }
        }
        SharedDeadUnitDic[team] = fightingUnits;
    }
    
    public void SensorDetectionResultClearProcess()
    {
        _detectedEnemies.Clear();
        _damagingWeaponAround.Clear();
    }
    
    List<GameObject> FindTargetsByDistance(Team[] tags, IDictionary<Team, List<Data_Center>> targetDic)
    {
        var targetList = new List<GameObject>();
        if (tags == null) return targetList;
        for (var i = 0; i < tags.Length; i++)
        {
            if (targetDic != null)
            {
                for (var y = 0; y < tags.Length; y++)
                {
                    targetDic.TryGetValue(tags[y], out var searchingMembers);
                    if (searchingMembers != null)
                    {
                        for (var k = 0; k < searchingMembers.Count; k++)
                        {
                            if (searchingMembers[k] != null)
                                targetList.Add(searchingMembers[k].WholeT.gameObject);
                            else
                                Debug.Log("检测逻辑错误");
                        }
                    }
                }
            }
        }
        
        return SortByHorizontalDistance(targetList);
    }
    
    List<GameObject> SortByHorizontalDistance(List<GameObject> targetList)
    {
        int count = targetList.Count;
        if (count == 0) return　targetList;

        // 原始位置数据
        NativeArray<Vector3> positions = new NativeArray<Vector3>(count, Allocator.TempJob);
        // 存储原始索引
        NativeArray<int> indices = new NativeArray<int>(count, Allocator.TempJob);
        Vector3 center = Center.position;

        for (int i = 0; i < count; i++)
        {
            positions[i] = targetList[i].transform.position;
            indices[i] = i;
        }

        // 调用排序 Job
        var sortJob = new SortByHorizontalDistanceJob
        {
            Positions = positions,
            Indices = indices,
            Center = center
        };

        JobHandle handle = sortJob.Schedule();
        handle.Complete();

        // 根据排好序的索引重建 List
        List<GameObject> sorted = new List<GameObject>(count);
        for (int i = 0; i < count; i++)
        {
            sorted.Add(targetList[indices[i]]);
        }

        // 替换原始 List（也可以直接用 sorted）
        targetList = sorted;

        positions.Dispose();
        indices.Dispose();
        return　targetList;
    }
    
    public void SensorDetectionResultSortProcess(Collider[] hits) //这个函数的调用必须要确保每次都在update函数之后
    {
        float sensorRadiusSqr = SensorRadius * SensorRadius;  // 预计算半径的平方
        Vector3 centerPosition = Center.position;
        foreach (Collider hit in hits)
        {
            if (hit == null || (hit.transform.position - centerPosition).sqrMagnitude > sensorRadiusSqr)
            {
                continue;
            }
            
            var hitLayer = hit.gameObject.layer;
            if (_teamConfig.enemyLayerMask == (_teamConfig.enemyLayerMask | (1 << hitLayer)) || _teamConfig.enemyShieldLayerMask == (_teamConfig.enemyShieldLayerMask | (1 << hitLayer)))
            {
                _detectedEnemies.Add(hit);
            }
            if (_teamConfig.enemyWeaponLayerMask == (_teamConfig.enemyWeaponLayerMask | (1 << hitLayer)))
            {
                _damagingWeaponAround.Add(hit);
            }
        }
        _nearestEnemyCollider = FindNearestCollider(_detectedEnemies);
        _nearestDamagingWeapon = FindNearestCollider(_damagingWeaponAround);
    }
    
    [BurstCompile]
    struct SortByHorizontalDistanceJob : IJob
    {
        public NativeArray<Vector3> Positions;
        public NativeArray<int> Indices;
        public Vector3 Center;

        public void Execute()
        {
            // 使用插入排序，对 Indices 按 Position 与 Center 的水平距离排序
            for (int i = 1; i < Indices.Length; i++)
            {
                int currentIndex = Indices[i];
                float currentDistance = HorizontalDistanceSqr(Positions[currentIndex], Center);
                int j = i - 1;

                while (j >= 0 && HorizontalDistanceSqr(Positions[Indices[j]], Center) > currentDistance)
                {
                    Indices[j + 1] = Indices[j];
                    j--;
                }
                Indices[j + 1] = currentIndex;
            }
        }

        private float HorizontalDistanceSqr(Vector3 p1, Vector3 center)
        {
            float dx = p1.x - center.x;
            float dz = p1.z - center.z;
            return dx * dx + dz * dz;
        }
    }
    
    //void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.white;
    //    Gizmos.DrawWireSphere(Center.position, sensor_radius);
    //    //Gizmos.DrawRay(transform.position,selfDataCenter.WholeT.forward * sensor_radius);
    //}
}