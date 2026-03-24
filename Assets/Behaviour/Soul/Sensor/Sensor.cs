using System.Collections.Generic;
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
        FindTargetsByDistance(tags, targetDic, targetList);
        return targetList;
    }

    void FindTargetsByDistance(Team[] tags, IDictionary<Team, List<Data_Center>> targetDic, List<GameObject> targetList)
    {
        targetList.Clear();
        if (tags == null || targetDic == null)
        {
            return;
        }

        for (var i = 0; i < tags.Length; i++)
        {
            if (!targetDic.TryGetValue(tags[i], out var searchingMembers) || searchingMembers == null)
            {
                continue;
            }

            for (var k = 0; k < searchingMembers.Count; k++)
            {
                var member = searchingMembers[k];
                if (member != null && member.WholeT != null)
                {
                    targetList.Add(member.WholeT.gameObject);
                }
                else
                {
                    Debug.Log("检测逻辑错误");
                }
            }
        }

        SortByHorizontalDistance(targetList);
    }
    
    void SortByHorizontalDistance(List<GameObject> targetList)
    {
        if (targetList.Count < 2 || Center == null)
        {
            return;
        }

        var center = Center.position;
        for (var i = 1; i < targetList.Count; i++)
        {
            var current = targetList[i];
            var currentDistance = current != null ? HorizontalDistanceSqr(current.transform.position, center) : float.MaxValue;
            var j = i - 1;

            while (j >= 0)
            {
                var comparing = targetList[j];
                var comparingDistance = comparing != null ? HorizontalDistanceSqr(comparing.transform.position, center) : float.MaxValue;
                if (comparingDistance <= currentDistance)
                {
                    break;
                }

                targetList[j + 1] = comparing;
                j--;
            }

            targetList[j + 1] = current;
        }
    }

    static float HorizontalDistanceSqr(Vector3 position, Vector3 center)
    {
        var dx = position.x - center.x;
        var dz = position.z - center.z;
        return dx * dx + dz * dz;
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

    //void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.white;
    //    Gizmos.DrawWireSphere(Center.position, sensor_radius);
    //    //Gizmos.DrawRay(transform.position,selfDataCenter.WholeT.forward * sensor_radius);
    //}
}
