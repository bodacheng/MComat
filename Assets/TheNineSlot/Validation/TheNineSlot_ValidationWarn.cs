using UnityEngine;

namespace mainMenu
{
    public partial class TheNineSlot : MonoBehaviour
    {
        [SerializeField] RectTransform normalSkillIndicator;
        
        public SkillSet.SkillEditError ValidateWarn()
        {
            // 第一列技能必须有普通技能
            var valR = CheckEditBasedOnCurrent();
            ValidationWarn(valR);
            return valR;
        }
        
        public void ValidationWarn(SkillSet.SkillEditError skillEditError)
        {
            _ValiWarn.gameObject.SetActive(true);
            normalSkillIndicator.gameObject.SetActive(false);
            switch(skillEditError)
            {
                case SkillSet.SkillEditError.RepeatedSkill:
                    _ValiWarn.text = "不可装备相同技能！";
                break;
                case SkillSet.SkillEditError.UnBalanced:
                    _ValiWarn.text = "技能点数失衡";
                break;
                case SkillSet.SkillEditError.NoNormalStart:
                    normalSkillIndicator.gameObject.SetActive(true);
                    _ValiWarn.text = "第一竖列必须有一个普通技能！";// 这地方最好配合个显示特效
                    break;
                case SkillSet.SkillEditError.NotFull:
                    _ValiWarn.text = "全てのスロットを満たしましょう！";
                    break;
                case SkillSet.SkillEditError.Perfect:
                    _ValiWarn.gameObject.SetActive(false);
                    break;
            }
        }
    }
}