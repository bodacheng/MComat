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
        [Header("AT")]
        public Text AT;
        [Space(7)]
        [Header("HP")]
        public Text HP;
        
        [Space(7)]
        [Header("当前技能等级")]
        public Slider expValue;
        public Text StoneTargetLevel;
        
        // 额外生成一个技能石图像
        void IconForShow(string skillID)
        {
            SKStoneItem item = MySkillStonesReader.GenerateNewStoneModel_New(skillID, false);
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
                item.transform.GetComponent<RectTransform>().sizeDelta = IconShowT.transform.GetComponent<RectTransform>().sizeDelta;
            }
        }
        
        public void Clear()
        {
            _T.gameObject.SetActive(false);
            keyname.text = "";
            Showname.text = "";
            ShowSkillStoneExType(-1);
            ShowSKillRanges(-10, -10); //即清空
            StoneTargetLevel.text = "";
            if (expValue != null)
            {
                expValue.value = 0;
            }
            if (IconShowT != null)
            {
                foreach (Transform child in IconShowT)
                {
                    Destroy(child.gameObject);
                }
            }
        }
        
        public void RefreshInfo(string skillStoneOfPlayerId)
        {
            SkillStoneOfPlayerInfoModel currentstone = MySkillStonesReader.Get(skillStoneOfPlayerId);
            if (currentstone == null)
            {
                Clear();
                return;
            }
            SkillConfig skillConfig = SkillConfigTable.GetSkillConfigByID(currentstone.skillId);
            IconForShow(skillConfig.RECORD_ID);

            keyname.text = skillConfig.REAL_NAME;
            Showname.text = skillConfig.RECORD_ID + ":" + SkillNameTable.GetSkillName(skillConfig.RECORD_ID);
            ShowSkillStoneExType(skillConfig.SP_LEVEL);
            ShowSKillRanges(skillConfig.AIAttrs.AI_MIN_DIS, skillConfig.AIAttrs.AI_MAX_DIS);
            PowerEstimateTable.Row row = PowerEstimateTable.Find_RECORD_ID(skillConfig.RECORD_ID);
            float.TryParse(row.HP, out float hp);
            float.TryParse(row.EstimateDamage, out float at);
            if (AT != null)
            {
                AT.text = "MaxDamage = " + SkillEntity.ATCal(at, currentstone.GetLevel());
            }
            if (HP != null)
            {
                HP.text = "MaxHp = " + SkillEntity.StoneHpCal(hp, currentstone.GetLevel());
            }
            LevelExpConfig.Current current = LevelExpConfig.GetCurrentInfo(currentstone.EXP);
            StoneTargetLevel.text = "Level:" + current.currentLevel.ToString() + "/100";
            if (expValue != null)
            {
                expValue.value = (float)current.expRemain / (current.expRemain + current.expToNextLevel);
            }
            _T.gameObject.SetActive(true);
        }
        
        public void RefreshInfo(SkillConfig _ConfigOfStone)
        {
            IconForShow(_ConfigOfStone.RECORD_ID);
            keyname.text = _ConfigOfStone.REAL_NAME;
            Showname.text = _ConfigOfStone.RECORD_ID + ":" + SkillNameTable.GetSkillName(_ConfigOfStone.RECORD_ID);
            ShowSkillStoneExType(_ConfigOfStone.SP_LEVEL);
            ShowSKillRanges(_ConfigOfStone.AIAttrs.AI_MIN_DIS, _ConfigOfStone.AIAttrs.AI_MAX_DIS);
            _T.gameObject.SetActive(true);
        }
        
        // 技能画面展示用
        public void RefreshInfo(SkillEntity _SkillEntity)
        {
            if (_SkillEntity == null)
            {
                Clear();
                return;
            }
            SkillConfig skillConfig = SkillConfigTable.GetSkillConfigByID(_SkillEntity.SkillID);
            keyname.text = _SkillEntity.REAL_NAME;
            Showname.text = skillConfig.SHOW_NAME;
            ShowSkillStoneExType(_SkillEntity.SP_LEVEL);
            ShowSKillRanges(_SkillEntity.AIAttrs.AI_MIN_DIS, _SkillEntity.AIAttrs.AI_MAX_DIS);
            PowerEstimateTable.Row row = PowerEstimateTable.Find_RECORD_ID(skillConfig.RECORD_ID);
            float.TryParse(row.HP, out float hp);
            float.TryParse(row.EstimateDamage, out float at);
            if (AT != null)
            {
                AT.text = "MaxDamage = " + _SkillEntity.AT;
            }
            if (HP != null)
            {
                HP.text = "MaxHp = " + _SkillEntity.HP;
            }
            _T.gameObject.SetActive(true);
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