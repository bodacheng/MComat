using System.Collections.Generic;
using UnityEngine;
using dataAccess;
using Skill;

namespace mainMenu
{
    public partial class SkillStonesBox : MonoBehaviour
    {
        private StoneFilterForm form;
        
        public void RestFilter()
        {
            StoneFilterForm filterForm = new StoneFilterForm
            {
                type = FocusingType,
                exType = new int[1] { GetFocusingExType() },
                close = closeCheckBox.isOn,
                near = nearCheckBox.isOn,
                far = farCheckBox.isOn,
                rare = rares
            };

            form = filterForm;
            PutSkillStonesToBox();
        }

        public class StoneFilterForm
        {
            public string type;
            public BehaviorType BType = BehaviorType.NONE;
            public int[] exType = { 0, 1, 2, 3 };
            public bool close;
            public bool near;
            public bool far;
            public List<int> rare = new List<int> { 0, 1, 2, 3, 4, 5 };
        }
        
        void PutSkillStonesToBox()
        {
            List<string> targetSKs = Stones.TargetStonesFromAccount_except(form, null, null, false);
            targetSKs = Order(targetSKs);
            
            if (targetSKs.Count > PlayerAccountInfo.Me.StoneBoxSize)
            {
                Debug.Log("错误：待显示技能石数量超过了盒子容量。盒子长度：" + PlayerAccountInfo.Me.StoneBoxSize + " AND  技能石数：" + targetSKs.Count);
            }
            
            foreach (KeyValuePair<int, StoneCell> cellPair in CellsDic)
            {
                cellPair.Value.RemoveToTemp();
            }
            
            int key = 0;
            foreach (var t in targetSKs)
            {
                CellsDic.TryGetValue(key, out StoneCell _Cell);
                if (_Cell == null)
                {
                    Debug.Log("Stone box exceed："+ key);
                    Debug.Log("此时技能石头盒子的总容量：" + CellsDic.Count);
                    continue;
                }
                
                if (!Stones.GetRenderModel(t)._using)
                {
                    _Cell.AddItem(Stones.GetRenderModel(t));
                    key++;
                }
                else
                {
                    _Cell.UpdateMyItem();
                }
            }
        }
    }
}