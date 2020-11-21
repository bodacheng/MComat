using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using dataAccess;

namespace mainMenu
{
    public partial class SkillStonesBox : MonoBehaviour
    {
        public IEnumerator ArrangeSkillStonesToBox()
        {
            yield return ArrangeSkillStonesToBox(GetFocusingType(), GetFocusingExType(), closeCheckBox.isOn, nearCheckBox.isOn, farCheckBox.isOn);
            StoneDeleteManger.target.RefreshSelectedRender();
        }
        
        // stoneviewScrollRect 应该在这个函数里扮演一个作用。
        public IEnumerator ArrangeSkillStonesToBox(string type, int exType, bool close, bool near, bool far)
        {
            List<String> targetSKs = MySkillStonesReader.TargetStonesFromAccount(type, exType, close, near, far);
            targetSKs = Order(targetSKs);
            if (targetSKs.Count > AccountSet._AccInfo.Stoneboxsize)
            {
                Debug.Log("错误：待显示技能石数量超过了盒子容量。盒子长度：" + AccountSet._AccInfo.Stoneboxsize + " AND  技能石数：" + targetSKs.Count);
                yield break;
            }
            
            foreach (KeyValuePair<int, StoneCell> cellPair in CellsDictionary)
            {
                cellPair.Value.RemoveToTemp();
            }
            
            int cellindex = 0;
            for (int i = 0; i < targetSKs.Count; i++)
            {
                CellsDictionary.TryGetValue(cellindex, out StoneCell _SkillStoneCell);
                if (!MySkillStonesReader.RenderModelDic[targetSKs[i]]._using)
                {
                    _SkillStoneCell.AddItem(MySkillStonesReader.RenderModelDic[targetSKs[i]]);
                    //_SkillStoneCell.image.color = !AccountCharsSet.CheckExist(MySkillStonesReader.Get(targetSKs[i]).inUsingMonsterOfPlayerId) ? Color.white : Color.yellow;
                    cellindex++;
                }
                else
                {
                    _SkillStoneCell.UpdateMyItem();
                    Debug.Log("有使用中的技能石头，直接跳过这一格");
                }
            }
        }
    }
}