using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour {

    public float sensor_radius = 15;//这个范围我们也就看作是普攻的冲击检测范围。
    public IDictionary<Team, List<Data_Center>> TeamMembers;
    
    LayerMask _layers;
    LayerMask meAndEnemyLayermask;
    Collider[] _hits; //What was hit in this frame?
    RaycastHit[] _spherecastHits;
    TeamConfig _TeamConfig = TeamConfig.defaultSet;
    
    int DetectionInterval;
    bool DetectionLoopStarted;
    int DetectionResultLastFrame;
    bool continuousDetection;
    
    List<Collider> detectedEnemies = new List<Collider>();
    Collider nearestEnemyCollider;
    
    List<Collider> DamagingWeaponAround = new List<Collider>();
    Collider NearestDamagingWeapon;
    
    Data_Center SelfDataCenter;

    public bool IFContinuousDetectionStarted()
    {
        return continuousDetection;
    }

    public void SetDectectLayer(TeamConfig teamConfig,Data_Center _self)
    {
        _TeamConfig = teamConfig;
        if (_TeamConfig != null)
        {
            _layers = teamConfig.mySensorAndWeaponTargetLayerMask;
            meAndEnemyLayermask = teamConfig.myTeamAndMyEnemy;
        }
        SelfDataCenter = _self;
    }

    public Collider GetTargetRangeEnemyCollider(float min, float max)
    {
        for (int i = 0; i < detectedEnemies.Count; i++)
        {
            float to_me = Vector3.Distance(transform.position, detectedEnemies[i].transform.position);
            if (to_me >= min && to_me <= max)
            {
                return detectedEnemies[i];
            }
        }
        return null;
    }
        
    public Collider GetClosestEnemyColliderInSensorRange()
    {
        return nearestEnemyCollider;
    }
    
    public Collider GetSuddenThreatInRange(float min,float max)
    {
        Collider threat = GetClosestEnemyHitBoxColliderInSensorRange();
        if (threat == null)
        {
            return null;
        }
        float to_me = Vector3.Distance(transform.position, threat.transform.position);
        if (to_me >= min && to_me <= max)
        {
            return threat;
        }
        return null;
    }
    
    Collider GetClosestEnemyHitBoxColliderInSensorRange()
    {
        return NearestDamagingWeapon;
    }

    public void SensorFixedUpdate()
    {
        if (DetectionLoopStarted)
        {
            if (DetectionInterval == 0)
            {
                SensorDetectionResultClearProcess();
                SensorDetectProcess();//检测
                SensorDetectionResultSortProcess();//整理
                SphereCastSortProcess();
            }
            if (DetectionInterval > DetectionResultLastFrame)
            {
                DetectionInterval = 0;
                if (!continuousDetection)
                {
                    DetectionLoopStarted = false;
                    SensorDetectionResultClearProcess();
                }
                return;//否则下面的DetectionInterval++会导致其值立刻从0变到1，无法进入上面的if (DetectionInterval == 0)部分。
            }
            DetectionInterval++;
        }
	}
    
    // continuousDetectionStart(0) 的情况下。
    // round 0: (一次检测) this.DetectionResultLastFrame == 0, DetectionInterval = 1 
    // round 1: DetectionInterval = 0; (上次检测结果未被清空)
    // round 2: 一次检测，DetectionInterval++; (上次检测未被清空)
    // round 3: DetectionInterval == 1, DetectionInterval = 0,(上次检测未被清空)
    // round 4: 一次检测，DetectionInterval++; (上次检测未被清空)
    //。。。。循环
    // continuousDetectionStart(-1) 的情况下。
    // round 0:  (一次检测) this.DetectionResultLastFrame == -1, DetectionInterval = 1
    // round 1: DetectionInterval = 0; (上次检测结果未被清空)
    // round 2: 一次检测 由于0 > -1, DetectionInterval = 0,
    // round 3: 一次检测 由于0 > -1, DetectionInterval = 0,
    // ... 循环
    // 结论： continuousDetectionStart(0) 让检测器隔一帧检测一次，continuousDetectionStart(-1)(任何负)，让检测器每帧检测一次

    public void ContinuousDetectionStart(int _DetectionResultLastFrame)
    {
        SensorDetectionResultClearProcess();
        SensorDetectProcess();//检测
        SensorDetectionResultSortProcess();//整理
        SphereCastSortProcess();

        continuousDetection = true;
        DetectionResultLastFrame = _DetectionResultLastFrame;
        DetectionLoopStarted = true;
        DetectionInterval = 1;//这个设置是为了在启动本函数瞬间进行检测活动，但不会在update里立刻再进行一次，形成间隔
    }

    public void OneRoundDetectionStart(int _DetectionResultLastFrame)
    {
        SensorDetectionResultClearProcess();
        SensorDetectProcess();//检测
        SensorDetectionResultSortProcess();//整理
        SphereCastSortProcess();

        continuousDetection = false;
        DetectionResultLastFrame = _DetectionResultLastFrame;
        DetectionLoopStarted = false;
        DetectionInterval = 1;
    }

    public void Stop()
    {
        SensorDetectionResultClearProcess();
        DetectionLoopStarted = false;
        continuousDetection = false;
    }

    Collider jiamateammate; Collider nearestenemy;
    public Collider[] EnemyAndTeammateBetweenMeAndEnemy()
    {
        return jiamateammate != null && nearestenemy != null ? (new Collider[2] { jiamateammate, nearestenemy }) : null;
    }

    public void SensorDetectProcess()
    {
        _hits = Physics.OverlapSphere(transform.position, sensor_radius, _layers);//这个东西消耗太大，起码可以考虑减少运行次数 // FIXUPDATE
        _spherecastHits = Physics.SphereCastAll(transform.position, 1f, SelfDataCenter.WholeT.forward, sensor_radius, meAndEnemyLayermask, QueryTriggerInteraction.Collide);
    }

    public void SensorDetectionResultClearProcess()
    {
        detectedEnemies.Clear();
        DamagingWeaponAround.Clear();
    }

    List<GameObject> EnemiesByDistance = new List<GameObject>();
    public List<GameObject> GetEnemiesByDistance(bool refresh)
    {
        if (_TeamConfig == null)
        {
            EnemiesByDistance.Clear();
            return EnemiesByDistance;
        }
        if (refresh)
            EnemiesByDistance = FindTargetsByDistance(this._TeamConfig.myEnemies.ToArray());
        return EnemiesByDistance;
    }

    List<GameObject> AlliesByDistance = new List<GameObject>();
    public List<GameObject> GetAlliesAndSelfByDistance(bool refresh)
    {
        if (_TeamConfig == null)
        {
            AlliesByDistance.Clear();
            return AlliesByDistance;
        }
        if (refresh)
            AlliesByDistance = this.FindTargetsByDistance(new Team[] { this._TeamConfig.myTeam });
        return AlliesByDistance;
    }

    List<Data_Center> searchingMembers;
    public List<GameObject> FindTargetsByDistance(Team[] tags) // 根据提供得目标标签获取一个以离自身距离为基准的gameobjects列表。有了这个显然FindClosestEnemy这个函数就很落后了
    {
        List<GameObject> target_list = new List<GameObject>();//貌似换成clear的话会减少GC,然而如果那样做，将产生一个极其严重的bug。你很可能在不知不觉中让两个使用了这个函数求列表的函数指向了同一地址。
        if (tags != null)
        {
            for (int i = 0; i < tags.Length; i++)
            {
                if (TeamMembers != null)
                {
                    for (int y = 0; y < tags.Length; y++)
                    {
                        TeamMembers.TryGetValue(tags[y], out searchingMembers);
                        if (searchingMembers != null)
                        {
                            for (int k = 0; k < searchingMembers.Count; k++)
                            {
                                if (searchingMembers[k]._MyBehaviorRunner.GetNowState().StateKey != "Death" && searchingMembers[k]._MyBehaviorRunner.GetNowState().StateKey != "Empty")
                                    target_list.Add(searchingMembers[k].WholeT.gameObject);
                            }
                        }
                        else
                        {
                            searchingMembers = null;
                        }
                    }
                }
            }
            if (target_list.Count > 1)
            {
                target_list.Sort((a, b) => HorizontalDistanceCompare(a.transform.position, b.transform.position));
                return target_list;
            }
            else
            {
                return target_list;
            }
        }
        return target_list;
    }

    public void SphereCastSortProcess()
    {
        if (_spherecastHits == null)
            return;

        float matetome = sensor_radius, enemytome = sensor_radius;
        foreach (RaycastHit raycastHit in _spherecastHits)
        {
            if (FightAttriCalReference.AllMeatColliders.Contains(raycastHit.collider))
            {
                if (_TeamConfig.myTeamLayerMask == (_TeamConfig.myTeamLayerMask | (1 << raycastHit.collider.gameObject.layer)))
                {
                    if (!SelfDataCenter.FightDataRef.IfMyBody(raycastHit.collider))
                    {
                        float to_me = Vector3.Distance(transform.position, raycastHit.collider.transform.position);
                        if (to_me < matetome)
                        {
                            jiamateammate = raycastHit.collider;
                            matetome = to_me;
                        }
                    }
                }
                if (_TeamConfig.enemyLayerMask == (_TeamConfig.enemyLayerMask | (1 << raycastHit.collider.gameObject.layer)))
                {
                    float to_me = Vector3.Distance(transform.position, raycastHit.collider.transform.position);
                    if (to_me < enemytome)
                    {
                        nearestenemy = raycastHit.collider;
                        enemytome = to_me;
                    }
                }
            }
        }

        if (jiamateammate != null && nearestenemy != null)
        {
            if (Vector3.Distance(transform.position, jiamateammate.transform.position) < Vector3.Distance(transform.position, nearestenemy.transform.position))
                return;//意思就是说让jiamateammate和nearestenemy不为空
        }
        jiamateammate = null;
        nearestenemy = null;
    }

    public void SensorDetectionResultSortProcess() //这个函数的调用必须要确保每次都在update函数之后
    {
        if (_hits == null)
        {
            return;
        }
        foreach (Collider hit in _hits)
        {
            if (hit != null)
            {
                if (_TeamConfig.enemyLayerMask == (_TeamConfig.enemyLayerMask | (1 << hit.gameObject.layer)) || _TeamConfig.enemyShieldLayerMask == (_TeamConfig.enemyShieldLayerMask | (1 << hit.gameObject.layer)))
                {
                    detectedEnemies.Add(hit);
                }
                if (_TeamConfig.enemyWeaponLayerMask == (_TeamConfig.enemyWeaponLayerMask | (1 << hit.gameObject.layer)))
                {
                    DamagingWeaponAround.Add(hit);
                }
            }
        }
        nearestEnemyCollider = FindNearestCollider(detectedEnemies);
        NearestDamagingWeapon = FindNearestCollider(DamagingWeaponAround);
    }

    float p1_to_me, p2_to_me;
    int HorizontalDistanceCompare(Vector3 p1, Vector3 p2)
    {
        p1.y = gameObject.transform.position.y;
        p1_to_me = (p1 - gameObject.transform.position).magnitude;

        p2.y = gameObject.transform.position.y;
        p2_to_me = (p2 - gameObject.transform.position).magnitude;

        return p1_to_me > p2_to_me ? 1 : p1_to_me < p2_to_me ? -1 : 0;
    }

    Collider target;
    Collider FindNearestCollider(List<Collider> list)
    {
        if (list == null || list.Count == 0)
        {
            return null;
        }
        if (list[0] == null)
        {
            return null;
        }
        if (list.Count == 1)
            return list[0];

        target = list[0];
        for (int i = 1; i < list.Count; i++)
        {
            if (list[i] == null)
                continue;
            if (HorizontalDistanceCompare(target.transform.position, list[i].transform.position) == 1)
            {
                target = list[i];
            }
        }
        return target;
    }

    public bool AllyBetweenSelfAndEnemy(float judgmentRange)
    {
        GetEnemiesByDistance(true);
        GetAlliesAndSelfByDistance(true);
        if (EnemiesByDistance.Count > 0 && AlliesByDistance.Count > 1)
        {
            float disToNearestEnemy2j, disToNearestAlly2j;
            disToNearestEnemy2j = HorizontalDistanceCompare(EnemiesByDistance[0].transform.position, gameObject.transform.position);
            disToNearestAlly2j = HorizontalDistanceCompare(AlliesByDistance[1].transform.position, gameObject.transform.position);
            return disToNearestEnemy2j >= disToNearestAlly2j && disToNearestEnemy2j < Mathf.Pow(judgmentRange, 2) && 
            Vector3.Angle((EnemiesByDistance[0].transform.position - gameObject.transform.position), (AlliesByDistance[1].transform.position - gameObject.transform.position)) < 40;
        }
        return false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, sensor_radius);
        //Gizmos.DrawRay(transform.position,selfDataCenter.WholeT.forward * sensor_radius);
    }
}

//public List<Collider> getInnerRangeWallColliders() //这个函数的调用必须要确保每次都在update函数之后
//{
//    wallTs.Clear();
//    if (_hits == null)
//    {
//        return wallTs;
//    }
//    foreach (Collider hit in this._hits)
//    {
//        if (hit != null)
//        {
//            if (hit.gameObject.layer == 13)
//            {

//                //_ClosestPointOnBounds = hit.ClosestPointOnBounds(transform.position);
//                if ((hit.transform.position - transform.position).magnitude < innerSensorRadius)
//                {
//                    wallTs.Add(hit);
//                    break;
//                }

//            }
//        }
//    }
//    return wallTs;
//}

//public List<Collider> getMyTeammatesNearby()
//{
//    return teammatesC;
//}

//public void MyteamDetectionResultSortProcess()
//{
//    teammatesC = teamMatesHIts.ToList();
//    if (teammatesC.Count > 1)
//    {
//        //OutterDamagingWeapon.Sort((a, b) => horizontalDistanceCompare(a.transform.position, b.transform.position));
//        tempCForNearest = FindNearestCollider(teammatesC);
//        if (tempCForNearest != null)
//        {
//            teammatesC.Remove(tempCForNearest);
//            teammatesC.Insert(0, tempCForNearest);
//        }
//    }
//}

// What kinds of info we need from all the hits we get ?
// 1.Other characters
// 2.Weapons on damaging mode
// 3.Working shield

// 在这个函数中我们使用了大量getComponent函数，但实际上在新的分层机制下，这些东西可以回避掉。
// 我们整个AI系统，策略上的一些判定靠的是DATAcente里那些，而这个地方的判定更多的来说是针对敌人近身情况下的一些应急性动作。
// 也就是说，其实对getNearbyEnemyHealthBody这个函数的利用基本只局限于和近身敌人的距离判定一类。。。
// 既然层的目的本来就是针对打击判定系统自身，那如果角色层上的collider的确不用来做伤害hitbox，何不直接在这种情况下把角色给设置成other层？
//List<Collider> FocousingNearbyEnemyColliders;
//public List<Collider> getNearbyEnemyColliders()
//{
//    FocousingNearbyEnemyColliders = new List<Collider>();
//    foreach (Collider hit in this._hits)
//    {
//        if (hit != null)
//        {
//            if (enemyMeatLayers == (enemyMeatLayers | (1 << hit.gameObject.layer)))
//            {
//                FocousingNearbyEnemyColliders.Add(hit);
//            }
//        }
//    }
//    return FocousingNearbyEnemyColliders;
//}