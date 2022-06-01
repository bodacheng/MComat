using System;
using FightScene;
using UnityEngine;

public static class RewardManager
{
    public static void RequestRewards(Action success, Action fail)
    {
        switch (NetFightScene.Fight.EventType)
        {
            case FightEventType.Arena:
                CloudScript.ArenaPointUp(
                    () => {Debug.Log("胜利加分");},
                    () => {Debug.Log("没能加分成功");}
                );
                break;
            case FightEventType.Quest:
                break;
            case FightEventType.Self:
                break;
            case FightEventType.SkillTest:
                break;
        }
    }
}