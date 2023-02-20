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
            confirmBtnColorSwapper.ChangeColor(skillEditError == SkillSet.SkillEditError.Perfect ? Color.green : Color.white);
            validationWarn.gameObject.SetActive(true);
            normalSkillIndicator.gameObject.SetActive(false);
            switch(skillEditError)
            {
                case SkillSet.SkillEditError.RepeatedSkill:
                    validationWarn.text = Translate.Get("CantEquipSameSkill");
                break;
                case SkillSet.SkillEditError.UnBalanced:
                    validationWarn.text = Translate.Get("UnBalanced");
                break;
                case SkillSet.SkillEditError.NoNormalStart:
                    normalSkillIndicator.gameObject.SetActive(true);
                    validationWarn.text = Translate.Get("AColumnNeedNormal");
                    break;
                case SkillSet.SkillEditError.NotFull:
                    validationWarn.text = Translate.Get("NotFull");//"全てのスロットを満たしましょう！";
                    break;
                case SkillSet.SkillEditError.Perfect:
                    validationWarn.gameObject.SetActive(false);
                    break;
            }
        }
    }
}