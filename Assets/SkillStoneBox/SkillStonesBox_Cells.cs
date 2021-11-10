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
        
        // 当下这个函数貌似每次启动背包都运行一次也没什么大的问题，需要考虑cellsLimit发生变化瞬间的处理。
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
                    cell._SkillStoneSlot = null;
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
        
        // 加载技能石背包机能
        // -1: 技能石合成 0:强化素材添加模式  1 : showMode 2: skilledit 3 : 技能展示器
        public void CellsFeatureLoad(int mode)
        {
            for (int i = 0; i < CellsDic.Count; i++)
            {
                switch(mode)
                {
                    case -1:
                        CellButtonBeheviour_StoneMergeMode(CellsDic[i]);
                    break;
                    case 0:
                        CellButtonBeheviour_MAdd(CellsDic[i]);
                    break;
                    case 1:
                        CellButtonBeheviour_STStoneShow(CellsDic[i]);
                    break;
                    case 2:
                        CellButtonBeheviour_EditCharSkill(CellsDic[i]);
                    break;
                    case 3:
                        CellButtonBeheviour_SKillShowMode(CellsDic[i]);
                    break;
                }
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
        
        // Show Character icon using this SkillStone
        public void ShowUsingChar(SKStoneItem Item, HeroIcon targetIcon)
        {
            if (Item == null || Item.instanceId == null)
            {
                targetIcon.gameObject.SetActive(false);
                return;
            }
            StoneOfPlayerInfo SSInfo = Stones.Get(Item.instanceId);
            if (SSInfo == null || SSInfo.inUsingMonsterOfPlayerId == null)
            {
                targetIcon.gameObject.SetActive(false);
                return;
            }
            
            UnitInfo _one = MyMonsters.Get(SSInfo.inUsingMonsterOfPlayerId);
            if (_one == null)
            {
                targetIcon.gameObject.SetActive(false);
                return;
            }
            targetIcon.gameObject.SetActive(true);
            CharConfig charConfig = MonstersConfigTable.GetCharConfig(_one.r_id);
            targetIcon.ChangeIcon(charConfig == null ? null : MonsterIconDic.Get(charConfig.RECORD_ID),
            charConfig == null ? Zokusei.Null : charConfig._zokusei);
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

