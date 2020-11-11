using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using dataAccess;
using Api.Dto.Model;

namespace mainMenu
{
    public partial class TheNineSlot : MonoBehaviour
    {
        public IEnumerator ReadANineAndTwo(MonsterOfPlayerDetailModel _AccCharInfo)
        {
            ForceClearAll();
            
            if (_AccCharInfo == null)
            {
                yield break;
            }
            List<SkillStoneOfPlayerInfoModel> equipingstones = MySkillStonesReader.GetEquipingStones(_AccCharInfo.monsterOfPlayerId);
            for (int i = 0; i < equipingstones.Count; i++)
            {
                switch (equipingstones[i].inUsingSkillSlot)
                {
                    case "1":
                        A1Slot.OnSlotStoneID = equipingstones[i].skillStoneOfPlayerId;
                        break;
                    case "2":
                        A2Slot.OnSlotStoneID = equipingstones[i].skillStoneOfPlayerId;
                        break;
                    case "3":
                        A3Slot.OnSlotStoneID = equipingstones[i].skillStoneOfPlayerId;
                        break;
                    case "4":
                        B1Slot.OnSlotStoneID = equipingstones[i].skillStoneOfPlayerId;
                        break;
                    case "5":
                        B2Slot.OnSlotStoneID = equipingstones[i].skillStoneOfPlayerId;
                        break;
                    case "6":
                        B3Slot.OnSlotStoneID = equipingstones[i].skillStoneOfPlayerId;
                        break;
                    case "7":
                        C1Slot.OnSlotStoneID = equipingstones[i].skillStoneOfPlayerId;
                        break;
                    case "8":
                        C2Slot.OnSlotStoneID = equipingstones[i].skillStoneOfPlayerId;
                        break;
                    case "9":
                        C3Slot.OnSlotStoneID = equipingstones[i].skillStoneOfPlayerId;
                        break;
                }
            }
            
            foreach (SkillStoneSlot _slot in allSlot)
            {
                yield return _slot.ShowOrigin(Color.white);
            }
            NineSlotsStatusRefresh();
        }
        
        // 撤销编辑
        public void ResetNineSlot()
        {
            mainProcessRunner.Run(target.ReadANineAndTwo(MemberDetail.target._focusing));
        }
    }
}