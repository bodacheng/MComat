using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Api.Dto.Model;
using dataAccess;

public class RewardManager : MonoBehaviour
{
    public static IEnumerator ExpUpForStones(List<string> StoneOfPlayerIDs, float addExp)
    {
        for (int i = 0; i < StoneOfPlayerIDs.Count; i++)
        {
            switch (AccountSet._playerinfoReferenceMode)
            {
                case playerInfoRefMode.localTestSaveData:
                    yield return ExpUpForStones_Local(StoneOfPlayerIDs[i],addExp);
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
        Debug.Log("执行存档技能："+ StoneOfPlayerID + "的技能升级");
        SkillStoneOfPlayerInfoModel stoneOfPlayer =  MySkillStonesReader.Get(StoneOfPlayerID);
        float formerExp = float.Parse(stoneOfPlayer.exp);
        stoneOfPlayer.exp = (formerExp + addExp).ToString();
        yield return MySkillStonesReader.Update(StoneOfPlayerID);

        SKStoneItem sKStone = MySkillStonesReader.GetRenderModel(StoneOfPlayerID);
        sKStone.LevelUpShow(formerExp, (formerExp + addExp));
        yield break;
    }
    
    public static IEnumerator ExpUpForTeamStones(List<CharDataInfo> expUpForStones)
    {
        foreach (CharDataInfo charDataInfo in expUpForStones)
        {
            List<string> mystoneids = new List<string>();
            List<SkillStoneOfPlayerInfoModel> mystones = MySkillStonesReader.GetEquipingStones(charDataInfo.monsterOfPlayerId);
            for (int i = 0; i < mystones.Count; i++)
            {
                mystoneids.Add(mystones[i].skillStoneOfPlayerId);
            }
            yield return ExpUpForStones(mystoneids, 1000f);
        }
    }
}