using System.Collections.Generic;

namespace Api.Dto.Model
{
    // 这个信息组成了一个角色与其对应的技能   
    public class MemberOfTeam
    {
        MonsterOfPlayerDetailModel MonsterInfo;
        List<SkillStoneOfPlayerInfoModel> stones;
    }
}