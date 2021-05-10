using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using dataAccess;
using Api.Dto.Model;
using UnityEngine.UI;

namespace mainMenu
{
    public partial class TheNineSlot : MonoBehaviour
    {
        public IEnumerator ReadANineAndTwo(MonsterOfPlayerInfo _AccCharInfo)
        {
            ForceClearAll();
            
            if (_AccCharInfo == null)
            {
                yield break;
            }
            List<StoneOfPlayerInfo> equipingstones = MySkillStones.GetEquipingStones(_AccCharInfo.InstanceId);

            for (int i = 1; i <= 9; i++)
            {
                allSlot[i - 1]._DragAndDropCell.RemoveToTemp();
            }

            for (int i = 0; i < equipingstones.Count; i++)
            {
                int usingPosInt = int.Parse(equipingstones[i].inUsingSkillSlot);
                if (equipingstones[i].InstanceId != null)
                    yield return allSlot[usingPosInt - 1].TakeASkillStoneFromBoxToSlot(equipingstones[i].InstanceId, Color.white);
                allSlot[usingPosInt - 1]._DragAndDropCell.UpdateMyItem();
                allSlot[usingPosInt - 1]._DragAndDropCell.GetComponent<Image>().color = new Color(1, 1, 1, 1f);
            }

            NineSlotsStatusRefresh();
        }
        
        // 撤销编辑
        public void ResetNineSlot()
        {
            mainProcessRunner.RunAsQueued(target.ReadANineAndTwo(MemberDetail.target._focusing));
        }
    }
}