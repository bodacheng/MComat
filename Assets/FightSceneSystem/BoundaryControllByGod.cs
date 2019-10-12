using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//这个模块的用以是把角色给限制在战斗场地内，因为我们发现在我们的游戏中边界问题十分难处理，
// 所以我们想到一个比较好的方案是在每个场地外加个“魔法环”，然而原则上不应该任何战斗地图都用这个魔法环系统，
// 所以我们单独整出这样一个模块来对这类事情进行一个统一处理

public enum BoundaryMode : int
{
    None = 0,
    Round = 1
}

public class BoundaryControllByGod : MonoBehaviour {
    
    public BoundaryMode boundaryMode;

    [Header("圆形模式参数")]
    [Space(6)]
    public GameObject BattleRing;
    public float BattleRingRadius = 20f;
    public Vector3 battleRingCenter;

    public IDictionary<Team, List<Data_Center>> AllMembers;//双方队伍人员字典，和netfightscene模块里同名变量统一。

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {      
	}

    public void RoundBattleFieldNormalControl(Vector3 battleRingCenter, float BattleRingRadius)
    {
        if (AllMembers == null)
            return;
        foreach (KeyValuePair<Team, List<Data_Center>> pair in AllMembers)
        {
            foreach (Data_Center oneBoy in pair.Value)
            {
                if (!oneBoy.IsDead.Value)
                {
                    battleRingCenter.y = oneBoy.WholeT.position.y;
                    distanceFromCharToCenter = (oneBoy.WholeT.position - battleRingCenter).magnitude;
                    if (distanceFromCharToCenter > BattleRingRadius)
                    {
                        oneBoy.onBattleGroundBundary = true;
                        oneBoy.WholeT.position =
                                  Vector3.Lerp(oneBoy.WholeT.position, battleRingCenter,
                                               Time.deltaTime);
                        oneBoy.antiWallDirection = battleRingCenter - oneBoy.WholeT.position;
                    }
                    else
                    {
                        oneBoy.onBattleGroundBundary = false;
                    }
                }
                else
                {
                    oneBoy.onBattleGroundBundary = false;
                }
            }
        }
    }

    float distanceFromCharToCenter;
    public void RoundModeGodControll(Vector3 battleRingCenter,float BattleRingRadius)
    {
        if (AllMembers == null)
            return;
        foreach (KeyValuePair<Team, List<Data_Center>> pair in AllMembers)
        {
            foreach (Data_Center oneBoy in pair.Value)
            {
                if (!oneBoy.IsDead.Value)
                {
                    battleRingCenter.y = oneBoy.WholeT.position.y;
                    distanceFromCharToCenter = (oneBoy.WholeT.position - battleRingCenter).magnitude;
                    if (distanceFromCharToCenter > BattleRingRadius)
                    {
                        oneBoy.onBattleGroundBundary = true;
                        oneBoy.WholeT.position =
                                  Vector3.Lerp(oneBoy.WholeT.position, battleRingCenter,
                                               Time.deltaTime * (distanceFromCharToCenter - BattleRingRadius) * 0.4f);
                        oneBoy.antiWallDirection = battleRingCenter - oneBoy.WholeT.position;
                    }
                    else
                    {
                        oneBoy.onBattleGroundBundary = false;
                    }
                }else{
                    oneBoy.onBattleGroundBundary = false;    
                }
            }
        }
    }

    //public void RoundField(float targetRadius)
    //{
    //    if (BattleRingRadius > targetRadius)
    //    {
    //        BattleRingRadius *= (1 / (Time.deltaTime * 0.1f + 1f));
    //        BattleRing.transform.localScale *= (1 / (Time.deltaTime * 0.1f + 1f));
    //    }
    //}

    float targetBattleGroundRingRadius = 30;
    public void SUOQUANER(int aliveMemberCount)
    {
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
        if (BattleRingRadius > targetBattleGroundRingRadius)
        {
            BattleRingRadius *= (1 / (Time.deltaTime * 0.1f + 1f));
            BattleRing.transform.localScale *= (1 / (Time.deltaTime * 0.1f + 1f));
        }
    }
}
