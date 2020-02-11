using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

namespace mainMenu
{
    public class SkillStoneDetail : MonoBehaviour
    {
        [Space(2)]
        [Header("技能信息")]
        public Text keyname;
        public Text Showname;

        [Space(7)]
        [Header("EXTypes")]
        public GameObject Ex1Icon,Ex2Icon,Ex3Icon;
        
        [Space(7)]
        [Header("EXTypes")]
        public GameObject close,near,far,outter;
        
        public void RefreshSkillDetail(SkillConfig _SkillConfigOfSkillStone)
        {
            keyname.text = _SkillConfigOfSkillStone.REAL_NAME;
            Showname.text = _SkillConfigOfSkillStone.ShowName;
            ShowSkillStoneExType(_SkillConfigOfSkillStone.SP_LEVEL);
            ShowSKillRanges(_SkillConfigOfSkillStone.ai_trigger_ranges);
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