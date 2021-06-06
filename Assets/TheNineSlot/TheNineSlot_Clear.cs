using UnityEngine;
using dataAccess;
using Api.Dto.Model;

namespace mainMenu
{
    public partial class TheNineSlot : MonoBehaviour
    {
        // 强制清空九宫槽 包括固有技能，用于程序处理
        void ForceClearAll()
        {
            foreach (SkillStoneSlot _slot in allSlot)
            {
                _slot._DragAndDropCell.RemoveToTemp();
            }
        }
        
        // 清空角色技能编辑
        public void ClearSkillEquip()
        {
            if (MemberDetail.target._focusing == null)
                return;

            MonsterOfPlayerInfo info = MemberDetail.target._focusing;
            StoneOfPlayerInfo originSkillInfo = Stones.GetOriginSkillOfMonster(info.InstanceId);
            foreach (SkillStoneSlot _slot in allSlot)
            {
                SKStoneItem sK = _slot._DragAndDropCell.GetItem();
                if (sK == null)
                {
                    continue;
                }
                if (originSkillInfo == null || (originSkillInfo != null && (sK.instanceId != originSkillInfo.InstanceId)))
                {
                    _slot._DragAndDropCell.RemoveToTemp();
                }
            }
            target.UpdateStonesBaseOnSlots(info);
        }
    }
}
