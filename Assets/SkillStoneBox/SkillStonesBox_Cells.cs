using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using dataAccess;

namespace mainMenu
{
    public partial class SkillStonesBox : MonoBehaviour
    {
        [Space(7)]
        [Header("格子pretab")]
        public StoneCell Cellprefab;
        
        [Space(5)]
        [Header("选中框")]
        public GameObject SelectedFrame;
        public static GameObject _Selected;
        
        public IDictionary<int, StoneCell> CellsDic = new Dictionary<int, StoneCell>();
        
        public void GenerateCells()
        {
            int hangshu = 1;
            for (int i = 0; i < Account._AccInfo.Stoneboxsize; i++)
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
            hangshu = Account._AccInfo.Stoneboxsize / GridLayoutGroup.constraintCount + 1;
            BoxT.sizeDelta = new Vector2(BoxT.sizeDelta.x, (GridLayoutGroup.cellSize.x + GridLayoutGroup.spacing.x) * hangshu);
        }
        
        public void AddFeatureToCells(Action<StoneCell> action)
        {
            foreach (var cell in CellsDic)
            {
                action.Invoke(cell.Value);
            }
        }
        
        public StoneCell GetFirstEmptyCell()
        {
            foreach (KeyValuePair<int, StoneCell> keyValuePair in CellsDic)
            {
                if (keyValuePair.Value.GetItem() != null)
                    continue;
                return keyValuePair.Value;
            }
            return null;
        }
        
        public static List<string> CheckIfExceedCellLimit()
        {
            List<string> error_massegas = new List<string>();
            List<string> C_Types = MonstersConfigTable.GetTypeList();
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
                
                if (SkillStonesOfType_normal.Count > Account._AccInfo.Stoneboxsize)
                {
                    error_massegas.Add(C_Types[i]+"类角色的普通技能石数量超过限制");
                }
                if (SkillStonesOfType_EX1.Count > Account._AccInfo.Stoneboxsize)
                {
                    error_massegas.Add(C_Types[i]+"类角色的一级必杀技能石数量超过限制");
                }
                if (SkillStonesOfType_EX2.Count > Account._AccInfo.Stoneboxsize)
                {
                    error_massegas.Add(C_Types[i]+"类角色的二级必杀技能石数量超过限制");
                }
                if (SkillStonesOfType_EX3.Count > Account._AccInfo.Stoneboxsize)
                {
                    error_massegas.Add(C_Types[i]+"类角色的三级必杀技能石数量超过限制");
                }
            }
            return error_massegas;
        }
    }
}

