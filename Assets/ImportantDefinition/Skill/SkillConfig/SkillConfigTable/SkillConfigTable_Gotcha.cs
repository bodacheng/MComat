using System.Collections.Generic;
using Skill;
using Api.Dto.Model;
using UnityEngine;
using dataAccess;

public partial class SkillConfigTable
{
    public static List<SkillStoneOfPlayerInfoModel> TenTimesGotcha(string type)
    {
        List<SkillStoneOfPlayerInfoModel> Geted = new List<SkillStoneOfPlayerInfoModel>();
        
        List<SkillConfig> skillConfigs = GetSkillConfigsOfType(type);
        for (int i = 0; i < 10; i++)
        {
            int random_index = Random.Range(0,skillConfigs.Count);
            SkillConfig skillConfig = skillConfigs[random_index];
            SkillStoneOfPlayerInfoModel stoneInfo = new SkillStoneOfPlayerInfoModel
            {
                skillStoneOfPlayerId = MySkillStonesReader.GetNonRepeatID_LocalSave(),
                skillId = skillConfig.REAL_NAME,
                exp = "0",
                Inherent = "false",
                inUsingMonsterOfPlayerId = i.ToString(),
                inUsingSkillSlot = null
            };
            Geted.Add(stoneInfo);
        }
        return Geted;
    }
}
