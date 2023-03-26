using UnityEngine;
using UnityEngine.UI;
using dataAccess;
using Skill;

namespace mainMenu
{
    public class SkillStoneDetail : MonoBehaviour
    {
        [Header("图标")]
        [SerializeField] RectTransform iconShowT;
        
        [Header("技能名字")]
        [SerializeField] Text keyName;
        [SerializeField] Text showName;

        [Header("技能类型图标")] 
        [SerializeField] GameObject atIcon;
        [SerializeField] GameObject defenceIcon;
        
        [Header("EXTypes")]
        [SerializeField] GameObject ex1Icon, ex2Icon, ex3Icon;
        
        [Header("Range")]
        [SerializeField] GameObject close, near, far;
        
        [Header("AT")]
        [SerializeField] Text AT;
        
        [Header("HP")]
        [SerializeField] Text HP;
        
        [Header("当前技能等级")]
        [SerializeField] Text stoneTargetLevel;
        
        [Header("Intro")]
        [SerializeField] Text skillIntro;
        
        [Header("tempT")]
        [SerializeField] Transform tempT;

        private void Awake()
        {
            Clear();
        }

        // 额外生成一个技能石图像
        async void IconForShow(string skillID)
        {
            var item = await Stones.GenerateStoneModel(skillID, false);
            if (iconShowT != null)
            {
                foreach (Transform child in iconShowT) 
                {
                    Destroy(child.gameObject);
                }
                item.transform.SetParent(iconShowT);
                item.gameObject.SetActive(true);
                item.transform.localPosition = Vector3.zero;
                item.transform.localScale = Vector3.one;
                item.transform.GetComponent<RectTransform>().sizeDelta = iconShowT.transform.GetComponent<RectTransform>().sizeDelta;
            }
            else
            {
                item.transform.SetParent(tempT);
            }
        }
        
        public void Clear()
        {
            keyName.text = string.Empty;
            showName.text = string.Empty;
            skillIntro.text = string.Empty;
            stoneTargetLevel.text = string.Empty;
            AT.text = string.Empty;
            HP.text = string.Empty;
            ShowSkillStoneExType(ex1Icon, ex2Icon, ex3Icon,-1);
            ShowSKillRanges(close, near, far, -10, -10); //即清空
            atIcon.SetActive(false);
            defenceIcon.SetActive(false);
            if (iconShowT != null)
            {
                foreach (Transform child in iconShowT)
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
            RefreshInfo(skillConfig);
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
            stoneTargetLevel.text = "LV:" + (currentStone.Level == PlayFabSetting._VersionMaxStoneLevel ? "MAX" : currentStone.Level.ToString());
            skillIntro.text = SkillNameTable.GetSkillIntro(skillConfig.RECORD_ID);
        }
        
        // 技能画面展示用
        public void RefreshInfo(SkillEntity skillEntity)
        {
            if (skillEntity == null)
            {
                Clear();
                return;
            }
            var skillConfig = SkillConfigTable.GetSkillConfig(skillEntity.SkillID);
            RefreshInfo(skillConfig);
            if (AT != null)
            {
                AT.text = "AT = " + skillEntity.AT;
            }
            if (HP != null)
            {
                HP.text = "HP = " + skillEntity.HP;
            }
        }
        
        public void RefreshInfo(SkillConfig config)
        {
            IconForShow(config.RECORD_ID);
            keyName.text = config.REAL_NAME;
            showName.text = SkillNameTable.GetSkillName(config.RECORD_ID);
            ShowSkillStoneExType(ex1Icon, ex2Icon, ex3Icon, config.SP_LEVEL);
            ShowSKillRanges(close, near, far, config.AIAttrs.AI_MIN_DIS, config.AIAttrs.AI_MAX_DIS);
            atIcon.SetActive(config.STATE_TYPE == BehaviorType.GI ||
                             config.STATE_TYPE == BehaviorType.GM || 
                             config.STATE_TYPE == BehaviorType.GR);
            defenceIcon.SetActive(config.STATE_TYPE == BehaviorType.CT || 
                                  config.STATE_TYPE == BehaviorType.Def);
        }
        
        public static void ShowSKillRanges(GameObject close, GameObject near, GameObject far, float disMIN, float disMAX)
        {
            close.SetActive(SkillConfig.RangeLimit(disMIN, disMAX, true, false, false));
            near.SetActive(SkillConfig.RangeLimit(disMIN, disMAX, false, true, false));
            far.SetActive(SkillConfig.RangeLimit(disMIN, disMAX, false, false, true));
        }
        
        public static void ShowSkillStoneExType(GameObject ex1Icon, GameObject ex2Icon, GameObject ex3Icon, int eX)
        {
            switch (eX)
            {
                case 0:
                    ex1Icon.SetActive(false);
                    ex2Icon.SetActive(false);
                    ex3Icon.SetActive(false);
                break;
                case 1:
                    ex1Icon.SetActive(true);
                    ex2Icon.SetActive(false);
                    ex3Icon.SetActive(false);
                break;
                case 2:
                    ex1Icon.SetActive(true);
                    ex2Icon.SetActive(true);
                    ex3Icon.SetActive(false);
                break;
                case 3:
                    ex1Icon.SetActive(true);
                    ex2Icon.SetActive(true);
                    ex3Icon.SetActive(true);
                break;
                case -1:
                    ex1Icon.SetActive(false);
                    ex2Icon.SetActive(false);
                    ex3Icon.SetActive(false);
                break;
            }
        }
    }
}