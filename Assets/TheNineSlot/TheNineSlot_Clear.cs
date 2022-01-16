using UnityEngine;
using dataAccess;

namespace mainMenu
{
    public partial class TheNineSlot : MonoBehaviour
    {
        // 强制清空九宫槽 包括固有技能，用于程序处理
        public void ForceClearAll()
        {
            foreach (SkillStoneSlot _slot in allSlot)
            {
                _slot._cell.RemoveToTemp();
            }
        }
        
        // 清空角色技能编辑
        public void ClearSkillEquip()
        {
            if (PreScene.target._focusing == null)
                return;

            UnitInfo info = PreScene.target._focusing;
            StoneOfPlayerInfo originSkillInfo = Stones.GetOriginSkillOfMonster(info.id);
            foreach (SkillStoneSlot _slot in allSlot)
            {
                SKStoneItem sK = _slot._cell.GetItem();
                if (sK == null)
                {
                    continue;
                }
                if (originSkillInfo == null || (originSkillInfo != null && (sK.instanceId != originSkillInfo.InstanceId)))
                {
                    _slot._cell.RemoveToTemp();
                }
            }
            UpdateStonesBaseOnSlots(info);
        }
    }
}
