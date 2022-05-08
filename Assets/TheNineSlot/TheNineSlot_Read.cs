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
            var equipments = Stones.GetEquippingStones(_AccCharInfo.id);

            for (var i = 1; i <= 9; i++)
            {
                allSlot[i - 1]._cell.RemoveToTemp();
            }

            for (var i = 0; i < equipments.Count; i++)
            {
                int usingPosInt = int.Parse(equipments[i].slot);
                if (equipments[i].InstanceId != null)
                {
                    
                    allSlot[usingPosInt - 1].TakeASkillStoneFromBoxToSlot(equipments[i].InstanceId, Color.white);
                }
                
                allSlot[usingPosInt - 1]._cell.UpdateMyItem();
                allSlot[usingPosInt - 1]._cell.GetComponent<Image>().color = new Color(1, 1, 1, 1f);
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