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
        public StoneCell Cellprefab;
        
        [Header("选中框")]
        public GameObject SelectedFrame;
        public static GameObject _Selected;
        
        IDictionary<int, StoneCell> CellsDic = new Dictionary<int, StoneCell>();
        
        public void GenerateCells()
        {
            var hangshu = 1;
            for (int i = 0; i < PlayerAccountInfo.Me.StoneBoxSize; i++)
            {
                if (!CellsDic.ContainsKey(i))
                {
                    StoneCell cell = Instantiate(Cellprefab);
                    cell.empty = new Color(1, 1, 1, 0.6f);
                    cell.full = new Color(1, 1, 1, 1);
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
            GridLayoutGroup GridLayoutGroup = BoxT.GetComponent<GridLayoutGroup>();
            hangshu = PlayerAccountInfo.Me.StoneBoxSize / GridLayoutGroup.constraintCount + 1;
            BoxT.sizeDelta = new Vector2(BoxT.sizeDelta.x, (GridLayoutGroup.cellSize.x + GridLayoutGroup.spacing.x) * hangshu);
        }
        
        public void AddFeatureToCells(Action<StoneCell> action)
        {
            foreach (var cell in CellsDic)
            {
                cell.Value.ClearGestureFeature();
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
            if (item._SkillConfig.SP_LEVEL == GetFocusingExType())
            {
                StoneCell dragAndDropCell = GetFirstEmptyCell();
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
        
        public static List<string> CheckIfExceedCellLimit()
        {
            List<string> errorMessages = new List<string>();
            List<string> C_Types = Units.GetTypeList();
            for (int i = 0; i < C_Types.Count; i++)
            {
                StoneFilterForm filterForm0 = new StoneFilterForm
                {
                    type = C_Types[i],
                    exType = new int[1] { 0 },
                };
                StoneFilterForm filterForm1 = new StoneFilterForm
                {
                    type = C_Types[i],
                    exType = new int[1] { 1 },
                };
                StoneFilterForm filterForm2 = new StoneFilterForm
                {
                    type = C_Types[i],
                    exType = new int[1] { 2 },
                };
                StoneFilterForm filterForm3 = new StoneFilterForm
                {
                    type = C_Types[i],
                    exType = new int[1] { 3 },
                };
                
                List<string> SkillStonesOfType_normal = Stones.TargetStonesFromAccount(filterForm0);
                List<string> SkillStonesOfType_EX1 = Stones.TargetStonesFromAccount(filterForm1);
                List<string> SkillStonesOfType_EX2 = Stones.TargetStonesFromAccount(filterForm2);
                List<string> SkillStonesOfType_EX3 = Stones.TargetStonesFromAccount(filterForm3);
                
                if (SkillStonesOfType_normal.Count > PlayerAccountInfo.Me.StoneBoxSize)
                {
                    errorMessages.Add(C_Types[i]+"类角色的普通技能石数量超过限制");
                }
                if (SkillStonesOfType_EX1.Count > PlayerAccountInfo.Me.StoneBoxSize)
                {
                    errorMessages.Add(C_Types[i]+"类角色的一级必杀技能石数量超过限制");
                }
                if (SkillStonesOfType_EX2.Count > PlayerAccountInfo.Me.StoneBoxSize)
                {
                    errorMessages.Add(C_Types[i]+"类角色的二级必杀技能石数量超过限制");
                }
                if (SkillStonesOfType_EX3.Count > PlayerAccountInfo.Me.StoneBoxSize)
                {
                    errorMessages.Add(C_Types[i]+"类角色的三级必杀技能石数量超过限制");
                }
            }
            return errorMessages;
        }
    }
}

