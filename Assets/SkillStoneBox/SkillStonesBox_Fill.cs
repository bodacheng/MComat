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
            yield return ArrangeSkillStonesToBox(GetFocusingType(), GetFocusingExType(), closeCheckBox.isOn, nearCheckBox.isOn, farCheckBox.isOn, outRangeCheckBox.isOn, TheNineSlot.target.GetUsingStonesId());
            _StoneDeleteManger.RefreshSelectedRender();
        }
        
        // stoneviewScrollRect 应该在这个函数里扮演一个作用。
        public IEnumerator ArrangeSkillStonesToBox(string type, int exType, bool close, bool near, bool far, bool outrange, List<string> UsingStoneIDs)
        {
            foreach (KeyValuePair<int, StoneCell> cellPair in CellsDictionary)
            {
                // 下面第一行（UpdateMyItem）至关重要。技能石box往往和九宫格一起显示，readANineAndTwo函数如果和arrangeSkillStonesToBox配合运行，
                // 都是前者在前，决定好在九宫格里显示的角色装备中石头是啥，先放在那里。这个时间点上技能石背包里的格子还没有断开和那几个石头的连接。如果你不UpdateMyItem一下，
                // 它会把已经放到九宫格里的石头给拔下来扔进stonesTempContainer。
                cellPair.Value.UpdateMyItem();
                SKStoneItem dragAndDropItem = cellPair.Value.GetItem();
                if (dragAndDropItem != null)
                {
                    dragAndDropItem.transform.SetParent(stonesTempContainer);
                }
                cellPair.Value.UpdateMyItem(); // 被拔下石头的格子需要把使用中角色头像关闭。单纯的通过null化物体的parent不会让Cell组件所记录的“放置中item”撤销
            }
            
            List<String> targetSKs = MySkillStonesReader.TargetStonesFromOfAccount(type, exType, close, near, far, outrange);
            targetSKs = Order(targetSKs);
            
            if (targetSKs.Count > AccountSet.Instance._PlayerAccountInfo.Stoneboxsize)
            {
                Debug.Log("错误：待显示技能石数量超过了盒子容量。盒子长度：" + AccountSet.Instance._PlayerAccountInfo.Stoneboxsize + " AND  技能石数：" + targetSKs.Count);
                yield break;
            }
            
            int cellindex = 0;
            for (int i = 0; i < targetSKs.Count; i++)
            {
                if (UsingStoneIDs != null)
                {
                    if (!UsingStoneIDs.Contains(targetSKs[i]))
                    {
                        CellsDictionary.TryGetValue(cellindex, out StoneCell _SkillStoneCell);
                        _SkillStoneCell.AddItem(MySkillStonesReader.RenderModelDic[targetSKs[i]]);
                        _SkillStoneCell.image.color = !AccountCharsSet.CheckExist(MySkillStonesReader.Get(targetSKs[i]).inUsingMonsterOfPlayerId) ? Color.white : Color.yellow;
                        cellindex++;
                    }
                    else
                    {
                        CellsDictionary.TryGetValue(cellindex, out StoneCell _SkillStoneCell);
                        _SkillStoneCell.UpdateMyItem();
                        Debug.Log("有使用中的技能石头，直接跳过这一格");
                    }
                }
                else
                {
                    MySkillStonesReader.RenderModelDic[targetSKs[i]].GetComponent<Image>().color = Color.white;
                    CellsDictionary.TryGetValue(cellindex, out StoneCell _SkillStoneCell);
                    _SkillStoneCell.AddItem(MySkillStonesReader.RenderModelDic[targetSKs[i]]); //！！！！！这个环节会销毁被覆盖的石头。
                    _SkillStoneCell.image.color = !AccountCharsSet.CheckExist(MySkillStonesReader.Get(targetSKs[i]).inUsingMonsterOfPlayerId) ? Color.white : Color.yellow;
                    cellindex++;
                }
            }
            yield break;
        }
    }
}