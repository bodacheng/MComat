using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// 这个模块的用以是把角色给限制在战斗场地内，因为我们发现在我们的游戏中边界问题十分难处理，
// 所以我们想到一个比较好的方案是在每个场地外加个“魔法环”，然而原则上不应该任何战斗地图都用这个魔法环系统，
// 所以我们单独整出这样一个模块来对这类事情进行一个统一处理
// 20191103 
// 根据不同的上场人数，场地的大小应该能够有所不同，所以如果是圆形场地的话，半径应该可设置。

public enum BoundaryMode
{
    None = 0,
    Round = 1
}

public class BoundaryControllByGod : MonoBehaviour {
    
    [Header("场地控制模式")]
    [Space(6)]
    public BoundaryMode boundaryMode;

    [Header("圆形模式参数")]
    [Space(6)]
    public List<ParticleSystem> BattleRingPSs;
    ParticleSystem BattleRingPS;
    float BattleRingRadius = 20f;
    public static float _BattleRingRadius;
    
    public IDictionary<Team, List<Data_Center>> AllMembers;//双方队伍人员字典，和netfightscene模块里同名变量统一。

    void Awake()
    {
        _BattleRingRadius = BattleRingRadius;
    }
    
    void Start()
    {
        int choose = Random.Range(0,BattleRingPSs.Count);
        for (int i = 0; i < BattleRingPSs.Count;i++)
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

    float distanceFromCharToCenter;
    public void RoundBattleFieldNormalControl(Vector3 battleRingCenter)
    {
        if (AllMembers == null)
            return;
        foreach (KeyValuePair<Team, List<Data_Center>> pair in AllMembers)
        {
            foreach (Data_Center oneBoy in pair.Value)
            {
                if (!oneBoy.IsDead.Value)
                {
                    Debug.Log("fixing");
                    battleRingCenter.y = oneBoy.WholeT.position.y;
                    distanceFromCharToCenter = (oneBoy.WholeT.position - battleRingCenter).magnitude;
                    if (distanceFromCharToCenter > BattleRingRadius)
                    {
                        oneBoy._BasicPhysicSupport.hiddenMethods.onBattleGroundBundary = true;
                        oneBoy.WholeT.position = Vector3.Lerp(oneBoy.WholeT.position, battleRingCenter,Time.deltaTime); //Vector3.Lerp(oneBoy.WholeT.position, battleRingCenter,Time.deltaTime * (distanceFromCharToCenter - BattleRingRadius) * 0.4f);
                        oneBoy._BasicPhysicSupport.hiddenMethods.antiWallDirection = battleRingCenter - oneBoy.WholeT.position;
                    }
                    else
                    {
                        oneBoy._BasicPhysicSupport.hiddenMethods.onBattleGroundBundary = false;
                    }
                }
                else
                {
                    oneBoy._BasicPhysicSupport.hiddenMethods.onBattleGroundBundary = false;
                }
            }
        }
    }

    public void SUOQUANER(int aliveMemberCount)
    {
        float targetBattleGroundRingRadius = 30;
        switch (aliveMemberCount)
        {
            case 7:
                targetBattleGroundRingRadius = 20;
                break;
            case 6:
                targetBattleGroundRingRadius = 15;
                break;
            case 5:
                targetBattleGroundRingRadius = 10;
                break;
            case 4:
                targetBattleGroundRingRadius = 7;
                break;
            case 3:
                targetBattleGroundRingRadius = 7;
                break;
            case 2:
                break;
            default:
                break;
        }
        ChangeMagicRingRadius(targetBattleGroundRingRadius);
    }
}
