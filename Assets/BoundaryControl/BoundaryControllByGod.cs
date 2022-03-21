using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BoundaryControllByGod : MonoBehaviour {

    public List<GameObject> BackGrounds = new List<GameObject>();

    public List<ParticleSystem> BattleRingPSs;
    ParticleSystem BattleRingPS;
    float BattleRingRadius = 20f;
    public static float _BattleRingRadius;
    public static BoundaryControllByGod target;
    
    void Awake()
    {
        target = this;
        _BattleRingRadius = BattleRingRadius;
    }
    
    void Start()
    {
        int choose = Random.Range(0, BattleRingPSs.Count);
        for (int i = 0; i < BattleRingPSs.Count; i++)
        {
            if (i == choose)
            {
                BattleRingPS = BattleRingPSs[i];
                BattleRingPS.gameObject.SetActive(true);
            }else{
                BattleRingPSs[i].gameObject.SetActive(false);
            }
        }
    }

    public void ChangeBackGround(int Number)
    {
        if (Number >= BackGrounds.Count)
        {
            Debug.Log("战斗场景编号错误："+ Number);
            ChangeBackGround(0);
            return;
        }
        for (int i = 0; i < BackGrounds.Count; i++)
        {
            if (Number == i)
            {
                BackGrounds[i].SetActive(true);
            }
            else
                BackGrounds[i].SetActive(false);
        }
    }
    
    public void ChangeMagicRingRadius(float targetradius)
    {
        BattleRingRadius = targetradius;
        _BattleRingRadius = BattleRingRadius;
        var sh = BattleRingPS.shape;
        void changeRadius(float x)
        {
            sh.radius = x;
        }
        DOTween.To(() => BattleRingRadius, x => BattleRingRadius = x, targetradius, 1).OnUpdate(() => changeRadius(BattleRingRadius));
    }
    
    public static void LimitTargetToRange(Data_Center dataCenter)
    {
        var temp = dataCenter.WholeT.position;
        temp.y = 0;
        var dis_from_center = temp.magnitude;
        if (dis_from_center > _BattleRingRadius)
        {
            temp = temp.normalized * _BattleRingRadius;
            temp.y = dataCenter.WholeT.position.y;
            dataCenter.WholeT.position = temp;
            dataCenter._BasicPhysicSupport.AtRing = true;
        }
        else
        {
            dataCenter._BasicPhysicSupport.AtRing = false;
        }
        
        temp = dataCenter.WholeT.position;
        if (temp.y < 0)
        {
            temp.y = 0f;
            dataCenter.WholeT.position = temp;
        }
    }
}

//public void SUOQUANER(int aliveMemberCount)
//{
//    float targetBattleGroundRingRadius = 30;
//    switch (aliveMemberCount)
//    {
//        case 7:
//            targetBattleGroundRingRadius = 20;
//            break;
//        case 6:
//            targetBattleGroundRingRadius = 15;
//            break;
//        case 5:
//            targetBattleGroundRingRadius = 10;
//            break;
//        case 4:
//            targetBattleGroundRingRadius = 7;
//            break;
//        case 3:
//            targetBattleGroundRingRadius = 7;
//            break;
//        case 2:
//            break;
//        default:
//            break;
//    }
//    ChangeMagicRingRadius(targetBattleGroundRingRadius);
//}

//public IDictionary<Team, List<Data_Center>> AllMembers;//双方队伍人员字典，和netfightscene模块里同名变量统一。
//float distanceFromCharToCenter;
//public void RoundBattleFieldNormalControl(Vector3 battleRingCenter)
//{
//    if (AllMembers == null)
//        return;
//    foreach (KeyValuePair<Team, List<Data_Center>> pair in AllMembers)
//    {
//        foreach (Data_Center oneBoy in pair.Value)
//        {
//            if (!oneBoy.IsDead.Value)
//            {
//                battleRingCenter.y = oneBoy.WholeT.position.y;
//                distanceFromCharToCenter = (oneBoy.WholeT.position - battleRingCenter).magnitude;
//                if (distanceFromCharToCenter > BattleRingRadius)
//                {
//                    oneBoy._BasicPhysicSupport.hiddenMethods.onBattleGroundBundary = true;
//                    oneBoy.WholeT.position = Vector3.Lerp(oneBoy.WholeT.position, battleRingCenter,Time.deltaTime); //Vector3.Lerp(oneBoy.WholeT.position, battleRingCenter,Time.deltaTime * (distanceFromCharToCenter - BattleRingRadius) * 0.4f);
//                    oneBoy._BasicPhysicSupport.hiddenMethods.antiWallDirection = battleRingCenter - oneBoy.WholeT.position;
//                }
//                else
//                {
//                    oneBoy._BasicPhysicSupport.hiddenMethods.onBattleGroundBundary = false;
//                }
//            }
//            else
//            {
//                oneBoy._BasicPhysicSupport.hiddenMethods.onBattleGroundBundary = false;
//            }
//        }
//    }
//}
