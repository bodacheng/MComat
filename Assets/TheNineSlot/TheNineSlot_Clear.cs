using UnityEngine;
using dataAccess;
using Api.Dto.Model;
using System.Collections;

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
                _slot._DragAndDropCell.RemoveToTemp();
            }
        }
        
        // 清空角色技能编辑
        public void ClearSkillEquip()
        {
            if (MemberDetail.target._focusing == null)
                return;
            MonsterOfPlayerDetailModel info = MemberDetail.target._focusing;
            SkillStoneOfPlayerInfoModel originSkillInfo = MySkillStonesReader.GetOriginSkillOfMonster(info.monsterOfPlayerId);
            
            foreach (SkillStoneSlot _slot in allSlot)
            {
                SKStoneItem sK = _slot._DragAndDropCell.GetItem();
                if (sK == null)
                {
                    continue;
                }
                if (originSkillInfo == null || (originSkillInfo != null && (sK.SkillStoneOfPlayerId != originSkillInfo.skillStoneOfPlayerId)))
                {
                    _slot._DragAndDropCell.RemoveToTemp();
                }
            }
            
            IEnumerator temp()
            {
                yield return target.UpdateMyStonesBaseOnSlotsExecution(info);
                yield return SkillStonesBox.target.PutSkillStonesToBox(SkillStonesBox.target.CurrentFilter());
                NineSlotsStatusRefresh();
            }
            
            mainProcessRunner.Run(temp());            
        }
    }
}
