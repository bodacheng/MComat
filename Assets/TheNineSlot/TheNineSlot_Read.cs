using System.Collections.Generic;
using UnityEngine;
using dataAccess;
using UnityEngine.UI;

namespace mainMenu
{
    public partial class TheNineSlot : MonoBehaviour
    {
        public void ReadANineAndTwo(UnitInfo _AccCharInfo)
        {
            ForceClearAll();
            
            if (_AccCharInfo == null)
            {
                Debug.Log("严重错误");
                return;
            }
            Debug.Log("sjadh" + _AccCharInfo.id);
            List<StoneOfPlayerInfo> equipingstones = Stones.GetEquipingStones(_AccCharInfo.id);

            for (int i = 1; i <= 9; i++)
            {
                allSlot[i - 1]._DragAndDropCell.RemoveToTemp();
            }

            for (int i = 0; i < equipingstones.Count; i++)
            {
                int usingPosInt = int.Parse(equipingstones[i].inUsingSkillSlot);
                if (equipingstones[i].InstanceId != null)
                {
                    
                    allSlot[usingPosInt - 1].TakeASkillStoneFromBoxToSlot(equipingstones[i].InstanceId, Color.white);
                }

                allSlot[usingPosInt - 1]._DragAndDropCell.UpdateMyItem();
                allSlot[usingPosInt - 1]._DragAndDropCell.GetComponent<Image>().color = new Color(1, 1, 1, 1f);
            }

            NineSlotsStatusRefresh();
        }
        
        // 撤销编辑
        public void ResetNineSlot()
        {
            ReadANineAndTwo(PreScene.target._focusing);
            ValidateWarn();
        }
    }
}