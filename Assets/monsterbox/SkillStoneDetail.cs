using UnityEngine;
using UnityEngine.UI;
using Api.Dto.Model;
using dataAccess;
using Skill;

namespace mainMenu
{
    public class SkillStoneDetail : MonoBehaviour
    {
        [Space(2)]
        [Header("技能名字")]
        public Text keyname;
        public Text Showname;

        [Space(7)]
        [Header("EXTypes")]
        public GameObject Ex1Icon,Ex2Icon,Ex3Icon;
        
        [Space(7)]
        [Header("EXTypes")]
        public GameObject close,near,far,outter;

        [Space(7)]
        [Header("当前技能等级")]
        public Text skill_level_info;
        public Text skill_level_levelup;
        
        SkillStoneOfPlayerInfoModel currentstone;
        public SkillStoneOfPlayerInfoModel GetSTTarget()
        {
            return currentstone;
        }
               
        public void RefreshSkillDetail(SkillConfig _SkillConfigOfSkillStone, string skillStoneOfPlayerId)
        {
            keyname.text = _SkillConfigOfSkillStone.REAL_NAME;
            Showname.text = _SkillConfigOfSkillStone.RECORD_ID;
            ShowSkillStoneExType(_SkillConfigOfSkillStone.SP_LEVEL);
            ShowSKillRanges(_SkillConfigOfSkillStone.AI_MIN_DIS,_SkillConfigOfSkillStone.AI_MAX_DIS);
            currentstone = MySkillStonesReader.Instance.GetStoneOfPlayerInfoModelByMyStoneId(skillStoneOfPlayerId);
            skill_level_levelup.text = "LV:" + (currentstone.level ?? "1");
            skill_level_info.text = "LV:" + (currentstone.level ?? "1");
        }
        
        void ShowSKillRanges(float dis_min, float float_max)
        {
            if (SkillConfigTable.RangeLimit(dis_min,float_max,true, false, false, false))
                close.SetActive(true);
            else
                close.SetActive(false);
                
            if (SkillConfigTable.RangeLimit(dis_min,float_max,false, true, false, false))
                near.SetActive(true);
            else
                near.SetActive(false);
                
            if (SkillConfigTable.RangeLimit(dis_min,float_max,false, false, true, false))
                far.SetActive(true);
            else
                far.SetActive(false);
                
            if (SkillConfigTable.RangeLimit(dis_min,float_max,false, false, false, true))
                outter.SetActive(true);
            else
                outter.SetActive(false);
        }
        
        void ShowSkillStoneExType(int eX)
        {
            switch (eX)
            {
                case 0:
                    Ex1Icon.SetActive(false);
                    Ex2Icon.SetActive(false);
                    Ex3Icon.SetActive(false);
                break;
                case 1:
                    Ex1Icon.SetActive(true);
                    Ex2Icon.SetActive(false);
                    Ex3Icon.SetActive(false);
                break;
                case 2:
                    Ex1Icon.SetActive(true);
                    Ex2Icon.SetActive(true);
                    Ex3Icon.SetActive(false);
                break;
                case 3:
                    Ex1Icon.SetActive(true);
                    Ex2Icon.SetActive(true);
                    Ex3Icon.SetActive(true);
                break;
                case -1:
                    Ex1Icon.SetActive(false);
                    Ex2Icon.SetActive(false);
                    Ex3Icon.SetActive(false);
                break;
            }
        }        
    }
}