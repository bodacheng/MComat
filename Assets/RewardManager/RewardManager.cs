using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Api.Dto.Model;
using dataAccess;

public class RewardManager : MonoBehaviour
{
     /// <summary>
     /// 给出技能石存档列表，为其增加一定量经验
     /// </summary>
     /// <param name="StoneOfPlayerIDs">技能石存档列表</param>
     /// <param name="addExp">增加的经验</param>
    public static IEnumerator ExpUpForStones(List<string> StoneOfPlayerIDs, float addExp)
    {
        for (int i = 0; i < StoneOfPlayerIDs.Count; i++)
        {
            switch (AccountSet._playerinfoReferenceMode)
            {
                case playerInfoRefMode.localTestSaveData:
                    yield return ExpUpForStones_Local(StoneOfPlayerIDs[i], addExp);
                break;
                case playerInfoRefMode.remoteTestPlayer:
                break;
                case playerInfoRefMode.formalVersion:
                break;
            }
        }
        yield break;
    }
    
    public static IEnumerator ExpUpForStones_Local(string StoneOfPlayerID, float addExp)
    {
        SkillStoneOfPlayerInfoModel stoneOfPlayer =  MySkillStonesReader.Get(StoneOfPlayerID);
        float formerExp = stoneOfPlayer.EXP;
        stoneOfPlayer.EXP = formerExp + addExp;
        yield return MySkillStonesReader.Update(StoneOfPlayerID);
    }
}