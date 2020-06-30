using System.Collections;
using UnityEngine;
using dataAccess;
using Api.Dto.Model;
using System.Collections.Generic;

public partial class NineForShow : MonoBehaviour
{
    // 战斗结束后统计技能石升级情况时的画面显示
    public IEnumerator ShowStones_Acc(string MonsterOfPlayerID)
    {
        List<SkillStoneOfPlayerInfoModel> skillStoneOfPlayerInfoModels = MySkillStonesReader.GetEquipingStones(MonsterOfPlayerID);
        
        // 下面这些是localid。不是技能定义id
        string A1SkillID = null, A2SkillID = null, A3SkillID = null;
        string B1SkillID = null, B2SkillID = null, B3SkillID = null;
        string C1SkillID = null, C2SkillID = null, C3SkillID = null;
        
        for (int i = 0; i < skillStoneOfPlayerInfoModels.Count; i++)
        {
            switch(skillStoneOfPlayerInfoModels[i].inUsingSkillSlot)
            {
                case "1":
                    A1SkillID = skillStoneOfPlayerInfoModels[i].skillStoneOfPlayerId;
                break;
                case "2":
                    A2SkillID = skillStoneOfPlayerInfoModels[i].skillStoneOfPlayerId;
                break;
                case "3":
                    A3SkillID = skillStoneOfPlayerInfoModels[i].skillStoneOfPlayerId;
                break;
                case "4":
                    B1SkillID = skillStoneOfPlayerInfoModels[i].skillStoneOfPlayerId;
                break;
                case "5":
                    B2SkillID = skillStoneOfPlayerInfoModels[i].skillStoneOfPlayerId;
                break;
                case "6":
                    B3SkillID = skillStoneOfPlayerInfoModels[i].skillStoneOfPlayerId;
                break;
                case "7":
                    C1SkillID = skillStoneOfPlayerInfoModels[i].skillStoneOfPlayerId;
                break;
                case "8":
                    C2SkillID = skillStoneOfPlayerInfoModels[i].skillStoneOfPlayerId;
                break;
                case "9":
                    C3SkillID = skillStoneOfPlayerInfoModels[i].skillStoneOfPlayerId;
                break;
            }
        }
        
        A1S = MySkillStonesReader.GetRenderModel(A1SkillID);
        A2S = MySkillStonesReader.GetRenderModel(A2SkillID);
        A3S = MySkillStonesReader.GetRenderModel(A3SkillID);
        B1S = MySkillStonesReader.GetRenderModel(B1SkillID);
        B2S = MySkillStonesReader.GetRenderModel(B2SkillID);
        B3S = MySkillStonesReader.GetRenderModel(B3SkillID);
        C1S = MySkillStonesReader.GetRenderModel(C1SkillID);
        C2S = MySkillStonesReader.GetRenderModel(C2SkillID);
        C3S = MySkillStonesReader.GetRenderModel(C3SkillID);
        
        Parent();
        yield break;
    }
}
