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
                _slot.OnSlotStoneID = null;
                _slot.RemoveStoneFromSlot();
            }
        }
        
        // 清空角色技能编辑
        public void ClearSkillEquip()
        {
            if (MemberDetail.target._focusing == null)
                return;
            MonsterOfPlayerDetailModel info = MemberDetail.target._focusing;
            SkillStoneOfPlayerInfoModel originSkillInfo = MySkillStonesReader.GetOriginSkillOfMonster(info.monsterOfPlayerId);
            
            NineSlotsStatusRefresh();
            foreach (SkillStoneSlot _slot in allSlot)
            {
                SKStoneItem sK = _slot._DragAndDropCell.GetItem();
                if (sK == null)
                    continue;
                if (sK.Inherent && MySkillStonesReader.Get(sK.SkillStoneOfPlayerId) == originSkillInfo)
                {
                
                }else{
                    _slot._DragAndDropCell.cellPhase = StoneCell.CellPhase.NineSlotCell;
                    _slot.RemoveStoneFromSlot();
                }
            }
            mainProcessRunner.Run(TheNineSlot.target.UpdateMyStonesBaseOnSlotsExecution(info));
        }
    }
}
