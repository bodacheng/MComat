using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using Api.Dto.Model;
using dataAccess;
using System.Collections;

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
        
        [Space(7)]
        [Header("升级按钮系列")]
        public Button plusLevel; // 这个按钮的有效与否应该是取决于有没有足够的经验值币来满足升级请求。
        public Button minusLevel;
        public Button confirmLevelUp;

        SkillStoneOfPlayerInfoModel currentstone;
        string targetSkillLevel;

        IEnumerator SkillStoneLevelUp(string PlayerSkillStoneID)
        {
            IEnumerator up = MySkillStonesReader.Instance.LevelUpMySkillStone(PlayerSkillStoneID, targetSkillLevel, ApiLanguage.EnUs);
            yield return up;
            if ((bool)up.Current)
            {
                Debug.Log("升级操作成功");
            }
            else
            {
                Debug.Log("升级操作失败");
            }
        }

        public void ConfirmSkillStoneLevelUp(string PlayerSkillStoneID)
        {
            StartCoroutine(SkillStoneLevelUp(PlayerSkillStoneID));
        }

        public void RefreshSkillDetail(SkillConfig _SkillConfigOfSkillStone, string skillStoneOfPlayerId)
        {
            keyname.text = _SkillConfigOfSkillStone.REAL_NAME;
            Showname.text = _SkillConfigOfSkillStone.SHOW_NAME;
            ShowSkillStoneExType(_SkillConfigOfSkillStone.SP_LEVEL);
            ShowSKillRanges(_SkillConfigOfSkillStone.ai_trigger_ranges);
            SkillStoneOfPlayerInfoModel skillStoneOfPlayerInfoModel = MySkillStonesReader.Instance.GetSkillStoneOfPlayerInfoModelByMyStoneId(skillStoneOfPlayerId);
            skill_level_levelup.text = "LV:" + (skillStoneOfPlayerInfoModel.level ?? "1");
            skill_level_info.text = "LV:" + (skillStoneOfPlayerInfoModel.level ?? "1");
        }
        
        void ShowSKillRanges(BehaviorEnterRange[] ranges)
        {
            List<BehaviorEnterRange> behaviorEnterRanges = ranges.ToList();
            if (behaviorEnterRanges.Contains(BehaviorEnterRange.inner_range))
                close.SetActive(true);
            else
                close.SetActive(false);
                
            if (behaviorEnterRanges.Contains(BehaviorEnterRange.mid_range))
                near.SetActive(true);
            else
                near.SetActive(false);
                
            if (behaviorEnterRanges.Contains(BehaviorEnterRange.far_range))
                far.SetActive(true);
            else
                far.SetActive(false);
                
            if (behaviorEnterRanges.Contains(BehaviorEnterRange.out_of_range))
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