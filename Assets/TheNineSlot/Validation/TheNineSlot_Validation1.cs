using System.Collections.Generic;
using UnityEngine;

namespace mainMenu
{
    public partial class TheNineSlot : MonoBehaviour
    {
        // 基于当前九宫格对技能编辑进行合法判断
        public SkillEditError CheckEditBasedOnCurrent()
        {
            List<string> nineskillids = target.GetCurrentNineSlotAllSkillIds();
            return CheckEditBasedOnCurrent(nineskillids);
        }
        
        // 基于当前九宫格对技能编辑进行合法判断 包括首发技能检测
        public SkillEditError CheckEditBasedOnCurrent_WithStartSkillCheck()
        {
            List<string> nineskillids = target.GetCurrentNineSlotAllSkillIds();
            if (CheckStartSKills(nineskillids[0], nineskillids[3], nineskillids[6]) == SkillEditError.NoNormalStart)
            {
                return SkillEditError.NoNormalStart;
            }
            return CheckEditBasedOnCurrent(nineskillids);
        }
        
        // 基于当前九宫格对技能编辑进行合法判断
        public SkillEditError CheckEditBasedOnCurrent(SKStoneItem item, StoneCell replacePosition)
        {
            List<string> nineskillids = target.GetCurrentNineSlotAllSkillIds();
            if (item != null)
            {
                for (int i = 0; i < allSlot.Count; i++)
                {
                    if (replacePosition == allSlot[i]._DragAndDropCell)
                    {
                        nineskillids[i] = item._SkillConfig.RECORD_ID;
                    }
                }
            }
            return CheckEditBasedOnCurrent(nineskillids);
        }
    }
}