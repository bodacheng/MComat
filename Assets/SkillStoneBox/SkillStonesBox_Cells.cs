using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using dataAccess;

namespace mainMenu
{
    public partial class SkillStonesBox : MonoBehaviour
    {
        [Header("格子")]
        [SerializeField] StoneCell cellPrefab;
        
        [Header("选中框")]
        [SerializeField] GameObject SelectedFrame;
        
        public static GameObject _Selected;

        readonly IDictionary<int, StoneCell> CellsDic = new Dictionary<int, StoneCell>();
        
        public void GenerateCells(int extraCellNum = 0)
        {
            foreach (var kv in CellsDic)
            {
                kv.Value.gameObject.SetActive(false);
            }
            
            var hang = 1;
            var cellCount = BoxLength();
            var GridLayoutGroup = BoxT.GetComponent<GridLayoutGroup>();
            cellCount += extraCellNum;
            cellCount = ((cellCount / GridLayoutGroup.constraintCount) + 1) * GridLayoutGroup.constraintCount;
            for (int i = 0; i < cellCount; i++)
            {
                if (!CellsDic.ContainsKey(i))
                {
                    var cell = Instantiate(cellPrefab);
                    cell.cellPhase = StoneCell.CellPhase.SkillStoneBoxCell;
                    CellsDic.Add(i, cell);
                }
                
                //CellsDictionary[i].RemoveItemWithOutDestroy();//根据之前经验，这个东西有出错的可能
                CellsDic[i].gameObject.SetActive(true);
                if (CellsDic[i].transform.parent != BoxT)
                {
                    CellsDic[i].transform.SetParent(BoxT);
                    CellsDic[i].transform.localPosition = Vector3.zero;
                    CellsDic[i].transform.localScale = Vector3.one;
                }
                
                CellsDic[i]._selected.SetActive(false);
            }
            
            hang = cellCount / GridLayoutGroup.constraintCount + 1;
            BoxT.sizeDelta = new Vector2(BoxT.sizeDelta.x, (GridLayoutGroup.cellSize.x + GridLayoutGroup.spacing.x) * hang);
        }
        
        public void AddFeatureToCells(Action<StoneCell> action)
        {
            foreach (var cell in CellsDic)
            {
                cell.Value.btn.ClearAllEvents();
                action.Invoke(cell.Value);
            }
        }
        
        StoneCell GetFirstEmptyCell()
        {
            foreach (KeyValuePair<int, StoneCell> keyValuePair in CellsDic)
            {
                if (keyValuePair.Value.GetItem() != null)
                    continue;
                return keyValuePair.Value;
            }
            return null;
        }

        public void ReturnStoneToBox(SKStoneItem item)
        {
            if (item._SkillConfig.SP_LEVEL == FocusingExType)
            {
                var dragAndDropCell = GetFirstEmptyCell();
                if (dragAndDropCell != null)
                {
                    dragAndDropCell.AddItem(item);
                }
                else
                {
                    Debug.Log("走到这儿的话说明已经是bug了。");
                    RemoveToTemp(item);
                }
            }
            else{
                //如果尝试归还背包的技能石必杀等级与显示中的不一致，则直接使其非显示。
                RemoveToTemp(item);
            }
        }
        
        void RemoveToTemp(SKStoneItem item)
        {
            item._using = false;
            item.gameObject.transform.SetParent(PreScene.target.stonesTempContainer);
        }

        static int BoxLength()
        {
            var returnValue = 0;
            var C_Types = Units.GetTypeList();
            for (var i = 0; i < C_Types.Count; i++)
            {
                var filterForm0 = new StoneFilterForm
                {
                    Type = C_Types[i],
                    ExType = new int[1] { 0 },
                };
                var filterForm1 = new StoneFilterForm
                {
                    Type = C_Types[i],
                    ExType = new int[1] { 1 },
                };
                var filterForm2 = new StoneFilterForm
                {
                    Type = C_Types[i],
                    ExType = new int[1] { 2 },
                };
                var filterForm3 = new StoneFilterForm
                {
                    Type = C_Types[i],
                    ExType = new int[1] { 3 },
                };
                
                var skillStonesOfTypeNormal = Stones.TargetStonesFromAccount(filterForm0);
                var skillStonesOfTypeEx1 = Stones.TargetStonesFromAccount(filterForm1);
                var skillStonesOfTypeEx2 = Stones.TargetStonesFromAccount(filterForm2);
                var skillStonesOfTypeEx3 = Stones.TargetStonesFromAccount(filterForm3);
                
                returnValue = Mathf.Max(returnValue, skillStonesOfTypeNormal.Count, skillStonesOfTypeEx1.Count, skillStonesOfTypeEx2.Count, skillStonesOfTypeEx3.Count);
            }
            return returnValue;
        }
        
        public static List<string> CheckIfExceedCellLimit()
        {
            var errorMessages = new List<string>();
            var C_Types = Units.GetTypeList();
            for (int i = 0; i < C_Types.Count; i++)
            {
                var filterForm0 = new StoneFilterForm
                {
                    Type = C_Types[i],
                    ExType = new int[1] { 0 },
                };
                var filterForm1 = new StoneFilterForm
                {
                    Type = C_Types[i],
                    ExType = new int[1] { 1 },
                };
                var filterForm2 = new StoneFilterForm
                {
                    Type = C_Types[i],
                    ExType = new int[1] { 2 },
                };
                var filterForm3 = new StoneFilterForm
                {
                    Type = C_Types[i],
                    ExType = new int[1] { 3 },
                };
                
                var SkillStonesOfType_normal = Stones.TargetStonesFromAccount(filterForm0);
                var SkillStonesOfType_EX1 = Stones.TargetStonesFromAccount(filterForm1);
                var SkillStonesOfType_EX2 = Stones.TargetStonesFromAccount(filterForm2);
                var SkillStonesOfType_EX3 = Stones.TargetStonesFromAccount(filterForm3);
                
                // if (SkillStonesOfType_normal.Count > PlayerAccountInfo.Me.StoneBoxSize)
                // {
                //     errorMessages.Add(C_Types[i]+"类角色的普通技能石数量超过限制");
                // }
                // if (SkillStonesOfType_EX1.Count > PlayerAccountInfo.Me.StoneBoxSize)
                // {
                //     errorMessages.Add(C_Types[i]+"类角色的一级必杀技能石数量超过限制");
                // }
                // if (SkillStonesOfType_EX2.Count > PlayerAccountInfo.Me.StoneBoxSize)
                // {
                //     errorMessages.Add(C_Types[i]+"类角色的二级必杀技能石数量超过限制");
                // }
                // if (SkillStonesOfType_EX3.Count > PlayerAccountInfo.Me.StoneBoxSize)
                // {
                //     errorMessages.Add(C_Types[i]+"类角色的三级必杀技能石数量超过限制");
                // }
            }
            return errorMessages;
        }
    }
}

