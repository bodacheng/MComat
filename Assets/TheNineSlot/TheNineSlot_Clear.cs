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
                _slot._DragAndDropCell.RemoveToTemp();
            }
        }
        
        // 清空角色技能编辑
        public void ClearSkillEquip()
        {
            if (MemberDetail.target._focusing == null)
                return;
                
            IEnumerator temp()
            {
                MonsterOfPlayerDetailModel info = MemberDetail.target._focusing;
                SkillStoneOfPlayerInfoModel originSkillInfo = MySkillStones.GetOriginSkillOfMonster(info.monsterOfPlayerId);
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
                yield return target.UpdateMyStonesBaseOnSlotsExecution(info);
                SkillStonesBox.target.PutSkillStonesToBox(SkillStonesBox.target.CurrentFilter());
                NineSlotsStatusRefresh();
            }
            mainProcessRunner.RunAsQueued(temp());            
        }
    }
}
