using System.Collections.Generic;
using System.Linq;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

public partial class Sensor
{
    public List<Collider> GetTargetRangeEnemyCollider(float min, float max)
    {
        if (_detectedEnemies == null || _detectedEnemies.Count == 0)
            return new List<Collider>();

        int count = _detectedEnemies.Count;

        // 准备 Native 数据
        NativeArray<Vector3> positions = new NativeArray<Vector3>(count, Allocator.TempJob);
        NativeArray<int> indices = new NativeArray<int>(count, Allocator.TempJob);
        NativeList<int> matchedIndices = new NativeList<int>(Allocator.TempJob);

        Vector3 center = Center.position;

        for (int i = 0; i < count; i++)
        {
            positions[i] = _detectedEnemies[i].transform.position;
            indices[i] = i;
        }

        float minSqr = min * min;
        float maxSqr = max * max;

        var job = new EnemyRangeFilterJob
        {
            Positions = positions,
            Indices = indices,
            MatchedIndices = matchedIndices.AsParallelWriter(),
            Center = center,
            MinSqr = minSqr,
            MaxSqr = maxSqr
        };

        JobHandle handle = job.Schedule(count, 64);
        handle.Complete();

        // 收集筛选后的 Collider
        List<Collider> result = new List<Collider>(matchedIndices.Length);
        for (int i = 0; i < matchedIndices.Length; i++)
        {
            result.Add(_detectedEnemies[matchedIndices[i]]);
        }

        // 释放资源
        positions.Dispose();
        indices.Dispose();
        matchedIndices.Dispose();

        return result;
    }
    
    public Collider GetClosestEnemyColliderInSensorRange()
    {
        return _nearestEnemyCollider;
    }
    
    public Collider GetSuddenThreatInRange(float min,float max)
    {
        var threat = GetClosestEnemyHitBoxColliderInSensorRange();
        if (threat == null)
        {
            return null;
        }
        var toMe = Vector3.Distance(Center.position, threat.transform.position);
        if (toMe >= min && toMe <= max)
        {
            return threat;
        }
        return null;
    }
    
    Collider GetClosestEnemyHitBoxColliderInSensorRange()
    {
        if (_nearestDamagingWeapon != null)
        {
            var returnValue = _nearestDamagingWeapon;
            _nearestDamagingWeapon = null;
            return returnValue;
        }
        return null;
    }
    
    public Collider[] EnemyAndTeammateBetweenMeAndEnemy()
    {
        return _jiaMateAmMate != null && _nearestEnemy != null ? (new Collider[2] { _jiaMateAmMate, _nearestEnemy }) : null;
    }
    
    List<GameObject> _enemiesByDistance = new List<GameObject>();
    public List<GameObject> GetEnemiesByDistance(bool refresh)
    {
        if (_teamConfig == null)
        {
            _enemiesByDistance.Clear();
            return _enemiesByDistance;
        }
        if (refresh)
            _enemiesByDistance = FindTargetsByDistance(this._teamConfig.myEnemies.ToArray(), SharedUnitDic);
        return _enemiesByDistance;
    }
    
    List<GameObject> _alliesByDistance = new List<GameObject>();
    List<GameObject> GetAlliesAndSelfByDistance(bool refresh)
    {
        if (_teamConfig == null)
        {
            _alliesByDistance.Clear();
            return _alliesByDistance;
        }
        if (refresh)
            _alliesByDistance = this.FindTargetsByDistance(new Team[] { this._teamConfig.myTeam }, SharedUnitDic);
        return _alliesByDistance;
    }
    
    public GameObject GetLastDeadEnemies()
    {
        var _enemiesByDistance = FindTargetsByDistance(this._teamConfig.myEnemies.ToArray(), SharedDeadUnitDic);
        return _enemiesByDistance.LastOrDefault();
    }
    
    Collider FindNearestCollider(List<Collider> colliderList)
    {
        if (colliderList == null || colliderList.Count == 0)
            return null;

        int count = colliderList.Count;

        // 准备 Position 数据
        NativeArray<Vector3> positions = new NativeArray<Vector3>(count, Allocator.TempJob);
        NativeArray<int> validFlags = new NativeArray<int>(count, Allocator.TempJob); // 标记是否为 null 的 Collider
        Vector3 center = Center.position;

        for (int i = 0; i < count; i++)
        {
            if (colliderList[i] != null)
            {
                positions[i] = colliderList[i].transform.position;
                validFlags[i] = 1;
            }
            else
            {
                positions[i] = Vector3.zero;
                validFlags[i] = 0;
            }
        }

        NativeArray<float> distances = new NativeArray<float>(count, Allocator.TempJob);
        NativeArray<int> minIndex = new NativeArray<int>(1, Allocator.TempJob);

        var distanceJob = new ComputeDistanceJob
        {
            Positions = positions,
            ValidFlags = validFlags,
            Distances = distances,
            Center = center
        };

        var findMinJob = new FindMinIndexJob
        {
            Distances = distances,
            MinIndex = minIndex
        };

        JobHandle distanceHandle = distanceJob.Schedule(count, 64);
        JobHandle minHandle = findMinJob.Schedule(distanceHandle);
        minHandle.Complete();

        int index = minIndex[0];

        positions.Dispose();
        validFlags.Dispose();
        distances.Dispose();
        minIndex.Dispose();

        return (index >= 0 && index < count) ? colliderList[index] : null;
    }
    
    [BurstCompile]
    struct ComputeDistanceJob : IJobParallelFor
    {
        [ReadOnly] public NativeArray<Vector3> Positions;
        [ReadOnly] public NativeArray<int> ValidFlags;
        public NativeArray<float> Distances;
        public Vector3 Center;

        public void Execute(int index)
        {
            if (ValidFlags[index] == 0)
            {
                Distances[index] = float.MaxValue;
                return;
            }

            float dx = Positions[index].x - Center.x;
            float dz = Positions[index].z - Center.z;
            Distances[index] = dx * dx + dz * dz;
        }
    }
    
    [BurstCompile]
    struct FindMinIndexJob : IJob
    {
        [ReadOnly] public NativeArray<float> Distances;
        public NativeArray<int> MinIndex;

        public void Execute()
        {
            float minDist = float.MaxValue;
            int best = -1;

            for (int i = 0; i < Distances.Length; i++)
            {
                if (Distances[i] < minDist)
                {
                    minDist = Distances[i];
                    best = i;
                }
            }

            MinIndex[0] = best;
        }
    }
    
    [BurstCompile]
    struct EnemyRangeFilterJob : IJobParallelFor
    {
        [ReadOnly] public NativeArray<Vector3> Positions;
        [ReadOnly] public NativeArray<int> Indices;
        public NativeList<int>.ParallelWriter MatchedIndices;
        [ReadOnly] public Vector3 Center;
        [ReadOnly] public float MinSqr;
        [ReadOnly] public float MaxSqr;

        public void Execute(int index)
        {
            Vector3 pos = Positions[index];
            float dx = pos.x - Center.x;
            float dz = pos.z - Center.z;
            float sqr = dx * dx + dz * dz;

            if (sqr >= MinSqr && sqr <= MaxSqr)
            {
                MatchedIndices.AddNoResize(Indices[index]);
            }
        }
    }
}
