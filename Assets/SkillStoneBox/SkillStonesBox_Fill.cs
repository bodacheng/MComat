using UnityEngine;
using dataAccess;
using Skill;

namespace mainMenu
{
    public partial class SkillStonesBox : MonoBehaviour
    {
        private StoneFilterForm _form;
        
        public void RestFilter()
        {
            var filterForm = new StoneFilterForm
            {
                Type = FocusingType,
                ExType = new[] { FocusingExType },
                Close = closeCheckBox.isOn,
                Near = nearCheckBox.isOn,
                Far = farCheckBox.isOn
            };

            _form = filterForm;
            PutSkillStonesToBox();
        }
        
        public class StoneFilterForm
        {
            public string Type;
            public BehaviorType BType = BehaviorType.NONE;
            public int[] ExType = { 0, 1, 2, 3 };
            public bool Close;
            public bool Near;
            public bool Far;
        }
        
        void PutSkillStonesToBox()
        {
            var targetSKs = Stones.TargetStonesFromAccount_except(_form, null, null, false);
            targetSKs = Order(targetSKs);
            
            foreach (var cellPair in CellsDic)
            {
                cellPair.Value.RemoveToTemp();
            }
            
            var key = 0;
            foreach (var instanceId in targetSKs)
            {
                CellsDic.TryGetValue(key, out var cell);
                if (cell == null)
                {
                    Debug.Log("Stone box exceed："+ key);
                    Debug.Log("此时技能石头盒子的总容量：" + CellsDic.Count);
                    continue;
                }
                
                if (!Stones.GetRenderModel(instanceId)._using)
                {
                    cell.AddItem(Stones.GetRenderModel(instanceId));
                    key++;
                }
                else
                {
                    cell.UpdateMyItem();
                }
            }
        }
    }
}