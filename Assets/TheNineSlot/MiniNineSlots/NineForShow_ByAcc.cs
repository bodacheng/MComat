using UnityEngine;
using dataAccess;
using Api.Dto.Model;
using System.Collections.Generic;

public partial class NineForShow : MonoBehaviour
{
    public void ShowStones_Acc(string MonsterOfPlayerID)
    {
        List<SkillStoneOfPlayerInfoModel> skillStoneOfPlayerInfoModels = MySkillStonesReader.GetEquipingStones(MonsterOfPlayerID);
        
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

        ShowStones(
            A1SkillID, A2SkillID, A3SkillID,
            B1SkillID, B2SkillID, B3SkillID,
            C1SkillID, C2SkillID, C3SkillID
        );
    }
}
