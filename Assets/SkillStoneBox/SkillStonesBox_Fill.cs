using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using dataAccess;
using Skill;

namespace mainMenu
{
    public partial class SkillStonesBox : MonoBehaviour
    {
        public StoneFilterForm CurrentFilter()
        {
            StoneFilterForm filterForm = new StoneFilterForm
            {
                type = GetFocusingType(),
                exType = new int[1] { GetFocusingExType() },
                close = closeCheckBox.isOn,
                near = nearCheckBox.isOn,
                far = farCheckBox.isOn,
                rare = rares
            };
            return filterForm;
        }
        
        public void PutSkillStonesToBox(StoneFilterForm filterForm)
        {
            PutSkillStonesToBox(filterForm, null, null);
        }
        
        // stoneviewScrollRect 应该在这个函数里扮演一个作用。
        public void PutSkillStonesToBox(StoneFilterForm filterForm, List<string> exceptSkIDs, List<string> extraList)
        {
            List<String> targetSKs = Stones.TargetStonesFromAccount_except(filterForm, exceptSkIDs, extraList, false);
            targetSKs = Order(targetSKs);
            if (targetSKs.Count > Account._AccInfo.Stoneboxsize)
            {
                Debug.Log("错误：待显示技能石数量超过了盒子容量。盒子长度：" + Account._AccInfo.Stoneboxsize + " AND  技能石数：" + targetSKs.Count);
            }
            
            foreach (KeyValuePair<int, StoneCell> cellPair in CellsDictionary)
            {
                cellPair.Value.RemoveToTemp();
            }

            int cellindex = 0;
            for (int i = 0; i < targetSKs.Count; i++)
            {
                CellsDictionary.TryGetValue(cellindex, out StoneCell _SkillStoneCell);
                if (_SkillStoneCell == null)
                {
                    Debug.Log("Stone box exceed");
                    continue;
                }

                if (!Stones.GetRenderModel(targetSKs[i])._using)
                {
                    _SkillStoneCell.AddItem(Stones.GetRenderModel(targetSKs[i]));
                    //_SkillStoneCell.image.color = !AccountCharsSet.CheckExist(MySkillStonesReader.Get(targetSKs[i]).inUsingMonsterOfPlayerId) ? Color.white : Color.yellow;
                    cellindex++;
                }
                else
                {
                    _SkillStoneCell.UpdateMyItem();
                    //Debug.Log("有使用中的技能石头，直接跳过这一格");
                }
            }
            StoneDeleteManger.target.RefreshSelectedRender();
        }

        public class StoneFilterForm
        {
            public string type;
            public BehaviorType BType = BehaviorType.NONE;
            public int[] exType = { 0,1,2,3 };
            public bool close = false;
            public bool near = false;
            public bool far = false;
            public List<int> rare = new List<int> { 0,1,2,3,4,5};
        }
    }
}