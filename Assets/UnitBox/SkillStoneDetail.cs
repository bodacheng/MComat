using UnityEngine;
using UnityEngine.UI;
using dataAccess;
using Skill;

namespace mainMenu
{
    public class SkillStoneDetail : MonoBehaviour
    {
        [Space(2)]
        [Header("图标")]
        [SerializeField] RectTransform IconShowT;
        
        [Space(2)]
        [Header("技能名字")]
        [SerializeField] Text keyname;
        [SerializeField] Text Showname;
        
        [Space(2)]
        [Header("EXTypes")]
        [SerializeField] GameObject Ex1Icon, Ex2Icon, Ex3Icon;
        
        [Space(2)]
        [Header("Range")]
        [SerializeField] GameObject close, near, far;
        
        [Space(2)]
        [Header("AT")]
        [SerializeField] Text AT;
        
        [Space(2)]
        [Header("HP")]
        [SerializeField] Text HP;
        
        [Space(2)]
        [Header("当前技能等级")]
        [SerializeField] Text StoneTargetLevel;

        [Space(2)]
        [SerializeField] Transform tempT;
        
        // 额外生成一个技能石图像
        async void IconForShow(string skillID)
        {
            var item = await Stones.GenerateStoneModel(skillID, false);
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
            else
            {
                item.transform.SetParent(tempT);
            }
        }
        
        public void Clear()
        {
            keyname.text = "";
            Showname.text = "";
            ShowSkillStoneExType(Ex1Icon, Ex2Icon, Ex3Icon,-1);
            ShowSKillRanges(close, near, far, -10, -10); //即清空
            StoneTargetLevel.text = "";
            if (IconShowT != null)
            {
                foreach (Transform child in IconShowT)
                {
                    Destroy(child.gameObject);
                }
            }
        }
        
        public void RefreshInfo(string instanceID)
        {
            var currentStone = Stones.Get(instanceID);
            if (currentStone == null)
            {
                Clear();
                return;
            }
            var skillConfig = SkillConfigTable.GetSkillConfig(currentStone.SkillId);
            IconForShow(skillConfig.RECORD_ID);
            keyname.text = skillConfig.REAL_NAME;
            Showname.text = skillConfig.RECORD_ID + ":" + SkillNameTable.GetSkillName(skillConfig.RECORD_ID);
            ShowSkillStoneExType(Ex1Icon, Ex2Icon, Ex3Icon, skillConfig.SP_LEVEL);
            ShowSKillRanges(close, near, far, skillConfig.AIAttrs.AI_MIN_DIS, skillConfig.AIAttrs.AI_MAX_DIS);
            var row = PowerEstimateTable.Find_RECORD_ID(skillConfig.RECORD_ID);
            float.TryParse(row.HP, out float hp);
            float.TryParse(row.EstimateDamage, out float at);
            if (AT != null)
            {
                AT.text = "AT = " + FightGlobalSetting.ATCal(at, currentStone.Level);
            }
            if (HP != null)
            {
                HP.text = "HP = " + FightGlobalSetting.StoneHpCal(hp, currentStone.Level);
            }
            StoneTargetLevel.text = "LV:" + currentStone.Level;
            transform.gameObject.SetActive(true);
        }
        
        public void RefreshInfo(SkillConfig _ConfigOfStone)
        {
            IconForShow(_ConfigOfStone.RECORD_ID);
            keyname.text = _ConfigOfStone.REAL_NAME;
            Showname.text = _ConfigOfStone.RECORD_ID + ":" + SkillNameTable.GetSkillName(_ConfigOfStone.RECORD_ID);
            ShowSkillStoneExType(Ex1Icon, Ex2Icon, Ex3Icon, _ConfigOfStone.SP_LEVEL);
            ShowSKillRanges(close, near, far, _ConfigOfStone.AIAttrs.AI_MIN_DIS, _ConfigOfStone.AIAttrs.AI_MAX_DIS);
            transform.gameObject.SetActive(true);
        }
        
        // 技能画面展示用
        public void RefreshInfo(SkillEntity _SkillEntity)
        {
            if (_SkillEntity == null)
            {
                Clear();
                return;
            }
            SkillConfig skillConfig = SkillConfigTable.GetSkillConfig(_SkillEntity.SkillID);
            keyname.text = _SkillEntity.REAL_NAME;
            Showname.text = skillConfig.SHOW_NAME;
            ShowSkillStoneExType(Ex1Icon, Ex2Icon, Ex3Icon, _SkillEntity.SP_LEVEL);
            ShowSKillRanges(close, near, far, _SkillEntity.AIAttrs.AI_MIN_DIS, _SkillEntity.AIAttrs.AI_MAX_DIS);
            PowerEstimateTable.Row row = PowerEstimateTable.Find_RECORD_ID(skillConfig.RECORD_ID);
            float.TryParse(row.HP, out float hp);
            float.TryParse(row.EstimateDamage, out float at);
            if (AT != null)
            {
                AT.text = "AT = " + _SkillEntity.AT;
            }
            if (HP != null)
            {
                HP.text = "HP = " + _SkillEntity.HP;
            }
            transform.gameObject.SetActive(true);
        }
        
        public static void ShowSKillRanges(GameObject close, GameObject near, GameObject far, float dis_min, float float_max)
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
        
        public static void ShowSkillStoneExType(GameObject Ex1Icon, GameObject Ex2Icon, GameObject Ex3Icon, int eX)
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