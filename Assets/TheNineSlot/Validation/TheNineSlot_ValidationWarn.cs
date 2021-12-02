using UnityEngine;

namespace mainMenu
{
    public partial class TheNineSlot : MonoBehaviour
    {
        void ValidateWarn()
        {
            // 第一列技能必须有普通技能
            SkillSet.SkillEditError valR = CheckEditBasedOnCurrent();
            if (valR != SkillSet.SkillEditError.Perfect)
            {
                ValidationWarn(valR, PreScene.target._focusing.id);
            }
        }
        
        public void ValidationWarn(SkillSet.SkillEditError skillEditError, string unit_instanceID)
        {
            switch(skillEditError)
            {
                case SkillSet.SkillEditError.RepeatedSkill:
                    _ValiWarn.text = "不可装备相同技能";
                break;
                case SkillSet.SkillEditError.UnBalanced:
                    _ValiWarn.text = "角色："+ unit_instanceID + "技能点数失衡";
                break;
                case SkillSet.SkillEditError.NoNormalStart:
                    _ValiWarn.text = "第一竖列必须有一个非必杀技";
                    break;
                case SkillSet.SkillEditError.UnableToFinish:
                    _ValiWarn.text = "没法补全当前九宫格";
                    break;
            }
        }
    }
}