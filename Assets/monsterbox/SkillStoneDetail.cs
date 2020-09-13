using UnityEngine;
using UnityEngine.UI;
using Api.Dto.Model;
using dataAccess;
using Skill;
using System.Collections;

namespace mainMenu
{
    public class SkillStoneDetail : MonoBehaviour
    {
        [Space(2)]
        [Header("BOXT")]
        public RectTransform _T;
        
        [Space(2)]
        [Header("图标")]
        public RectTransform IconShowT;
        
        [Space(2)]
        [Header("技能名字")]
        public Text keyname;
        public Text Showname;
        
        [Space(7)]
        [Header("EXTypes")]
        public GameObject Ex1Icon, Ex2Icon, Ex3Icon;
        
        [Space(7)]
        [Header("EXTypes")]
        public GameObject close, near, far;
        
        [Space(7)]
        [Header("当前技能等级")]
        public Text skill_level_info;
        public Text skill_level_levelup;
        
        SkillStoneOfPlayerInfoModel currentstone;
        public SkillStoneOfPlayerInfoModel GetSTTarget()
        {
            return currentstone;
        }
        
        // 额外生成一个技能石图像
        IEnumerator IconForShow(string skillID)
        {
            IEnumerator Generate = SkillStonesBox.GenerateNewStoneModel(skillID , 2);
            yield return Generate;
            SKStoneItem item = (SKStoneItem)Generate.Current;
            if (IconShowT != null)
            {
                foreach (Transform child in IconShowT) 
                {
                    Destroy(child.gameObject);
                }
                item.transform.SetParent(IconShowT);
                item.gameObject.SetActive(true);
                item.transform.localPosition = Vector3.zero;
                item.transform.localScale = Vector3.one;
            }
        }
        
        public void Clear()
        {
            keyname.text = "";
            Showname.text = "";
            ShowSkillStoneExType(-1);
            ShowSKillRanges(-10, -10);//即清空
            skill_level_levelup.text = "";
            skill_level_info.text = "";
            if (IconShowT != null)
            {
                foreach (Transform child in IconShowT)
                {
                    Destroy(child.gameObject);
                }
            }
        }
        
        public void RefreshSkillDetail(string skillStoneOfPlayerId)
        {
            if (!string.IsNullOrEmpty(skillStoneOfPlayerId))
            {
                currentstone = MySkillStonesReader.Get(skillStoneOfPlayerId);
                SkillConfig skillConfig = SkillConfigTable.GetSkillConfigByID(currentstone.skillId);
                SkillStonesBox.target.mainProcessRunner.Run(IconForShow(skillConfig.RECORD_ID));
                
                keyname.text = skillConfig.REAL_NAME;
                Showname.text = skillConfig.RECORD_ID + ":" + SkillNameTable.GetSkillName(skillConfig.RECORD_ID);
                ShowSkillStoneExType(skillConfig.SP_LEVEL);
                ShowSKillRanges(skillConfig.AI_MIN_DIS, skillConfig.AI_MAX_DIS);
                
                skill_level_levelup.text = "LV:" + currentstone.GetLevel();
                skill_level_info.text = "LV:" + currentstone.GetLevel();
            }else{
                Clear();
            }
        }
        
        public void RefreshSkillDetail(SkillConfig _ConfigOfStone)
        {
            SkillStonesBox.target.mainProcessRunner.Run(IconForShow(_ConfigOfStone.RECORD_ID));
            
            keyname.text = _ConfigOfStone.REAL_NAME;
            Showname.text = _ConfigOfStone.RECORD_ID + ":" + SkillNameTable.GetSkillName(_ConfigOfStone.RECORD_ID);
            ShowSkillStoneExType(_ConfigOfStone.SP_LEVEL);
            ShowSKillRanges(_ConfigOfStone.AI_MIN_DIS, _ConfigOfStone.AI_MAX_DIS);
            skill_level_levelup.text = "";
            skill_level_info.text = "";
        }
        
        // 技能画面展示用
        public void RefreshSkillDetail_SkillEntity(SkillEntity _SkillConfigOfSkillStone)
        {
            if (_SkillConfigOfSkillStone == null)
            {
                keyname.text = "";
                ShowSkillStoneExType(0);
                ShowSKillRanges(-10, -10);//即清空
                return;
            }
            keyname.text = _SkillConfigOfSkillStone.REAL_NAME;
            ShowSkillStoneExType(_SkillConfigOfSkillStone.SP_LEVEL);
            ShowSKillRanges(_SkillConfigOfSkillStone.AI_MIN_DIS, _SkillConfigOfSkillStone.AI_MAX_DIS);
        }
        
        void ShowSKillRanges(float dis_min, float float_max)
        {
            if (SkillConfig.RangeLimit(dis_min, float_max, true, false, false))
                close.SetActive(true);
            else
                close.SetActive(false);
                
            if (SkillConfig.RangeLimit(dis_min, float_max, false, true, false))
                near.SetActive(true);
            else
                near.SetActive(false);
                
            if (SkillConfig.RangeLimit(dis_min, float_max, false, false, true))
                far.SetActive(true);
            else
                far.SetActive(false);
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