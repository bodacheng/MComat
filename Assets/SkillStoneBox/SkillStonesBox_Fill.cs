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
                exType = new int[1] { FocusingExType },
                close = closeCheckBox.isOn,
                near = nearCheckBox.isOn,
                far = farCheckBox.isOn
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
        }
        
        void PutSkillStonesToBox()
        {
            var targetSKs = Stones.TargetStonesFromAccount_except(form, null, null, false);
            targetSKs = Order(targetSKs);
            
            foreach (var cellPair in CellsDic)
            {
                cellPair.Value.RemoveToTemp();
            }
            
            var key = 0;
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