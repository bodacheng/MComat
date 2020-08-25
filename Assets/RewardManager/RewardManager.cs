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
    public static IEnumerator ExpUpForStones(List<string> StoneOfPlayerIDs, int addExp)
    {
        for (int i = 0; i < StoneOfPlayerIDs.Count; i++)
        {
            switch (AccountSet.ReferenceMode)
            {
                case PlayerInfoRefMode.localTestSaveData:
                    yield return ExpUpForStones_Local(StoneOfPlayerIDs[i], addExp);
                break;
                case PlayerInfoRefMode.remoteTestPlayer:
                break;
                case PlayerInfoRefMode.formalVersion:
                break;
            }
        }
        yield break;
    }
    
    public static IEnumerator ExpUpForStones_Local(string StoneOfPlayerID, int addExp)
    {
        SkillStoneOfPlayerInfoModel stoneOfPlayer =  MySkillStonesReader.Get(StoneOfPlayerID);
        int formerExp = stoneOfPlayer.EXP;
        stoneOfPlayer.EXP = formerExp + addExp;
        yield return MySkillStonesReader.Update(StoneOfPlayerID);
    }
}