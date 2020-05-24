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
        List<SkillStoneOfPlayerInfoModel> skillStoneOfPlayerInfoModels = MySkillStonesReader.GetMonsterEquipingStones(MonsterOfPlayerID);
        string A1SkillID = null, A2SkillID = null, A3SkillID = null;
        string B1SkillID = null, B2SkillID = null, B3SkillID = null;
        string C1SkillID = null, C2SkillID = null, C3SkillID = null;
        
        for (int i = 0; i < skillStoneOfPlayerInfoModels.Count; i++)
        {
            switch(skillStoneOfPlayerInfoModels[i].inUsingSkillSlot)
            {
                case "1":
                    A1SkillID = skillStoneOfPlayerInfoModels[i].skillId;
                break;
                case "2":
                    A2SkillID = skillStoneOfPlayerInfoModels[i].skillId;
                break;
                case "3":
                    A3SkillID = skillStoneOfPlayerInfoModels[i].skillId;
                break;
                case "4":
                    B1SkillID = skillStoneOfPlayerInfoModels[i].skillId;
                break;
                case "5":
                    B2SkillID = skillStoneOfPlayerInfoModels[i].skillId;
                break;
                case "6":
                    B3SkillID = skillStoneOfPlayerInfoModels[i].skillId;
                break;
                case "7":
                    C1SkillID = skillStoneOfPlayerInfoModels[i].skillId;
                break;
                case "8":
                    C2SkillID = skillStoneOfPlayerInfoModels[i].skillId;
                break;
                case "9":
                    C3SkillID = skillStoneOfPlayerInfoModels[i].skillId;
                break;
            }
        }
        
        SKStoneItem A1S = MySkillStonesReader.GetRenderModel(A1SkillID);
        SKStoneItem A2S = MySkillStonesReader.GetRenderModel(A2SkillID);
        SKStoneItem A3S = MySkillStonesReader.GetRenderModel(A3SkillID);
        SKStoneItem B1S = MySkillStonesReader.GetRenderModel(B1SkillID);
        SKStoneItem B2S = MySkillStonesReader.GetRenderModel(B2SkillID);
        SKStoneItem B3S = MySkillStonesReader.GetRenderModel(B3SkillID);
        SKStoneItem C1S = MySkillStonesReader.GetRenderModel(C1SkillID);
        SKStoneItem C2S = MySkillStonesReader.GetRenderModel(C2SkillID);
        SKStoneItem C3S = MySkillStonesReader.GetRenderModel(C3SkillID);
        
        Parent(A1S,A2S,A3S,B1S,B2S,B3S,C1S,C2S,C3S);
        yield break;
    }
}
