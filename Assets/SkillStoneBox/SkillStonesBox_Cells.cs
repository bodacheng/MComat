using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using dataAccess;
using Api.Dto.Model;

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
        
        [Space(7)]
        [Header("石头滚动视窗")]
        public ScrollRect stoneviewScrollRect;
        
        public static IDictionary<int, StoneCell> CellsDictionary = new Dictionary<int, StoneCell>();
        
        public static void PreventCellsFromDestroy()
        {
            foreach (KeyValuePair<int, StoneCell> keyValuePair in CellsDictionary)
            {
                keyValuePair.Value.transform.SetParent( ResourceKeeper.dontDestroyOnLoadParent);
            }
        }
        
        // 当下这个函数貌似每次启动背包都运行一次也没什么大的问题，需要考虑cellsLimit发生变化瞬间的处理。
        public void GenerateCells()
        {
            int hangshu = 1;
            for (int i = 0; i < AccountSet._AccInfo.Stoneboxsize; i++)
            {
                if (!CellsDictionary.ContainsKey(i))
                {
                    StoneCell cell = Instantiate(Cellprefab);
                    cell.empty = new Color(1, 1, 1, 0.6f);
                    cell.full = new Color(1, 1, 1, 1);
                    cell.cellPhase = StoneCell.CellPhase.SkillStoneBoxCell;
                    cell._SkillStoneSlot = null;
                    CellsDictionary.Add(i, cell);
                }
                
                //CellsDictionary[i].RemoveItemWithOutDestroy();//根据之前经验，这个东西有出错的可能
                CellsDictionary[i].gameObject.SetActive(true);
                if (CellsDictionary[i].transform.parent != BoxT)
                {
                    CellsDictionary[i].transform.SetParent(BoxT);
                    CellsDictionary[i].transform.localPosition = Vector3.zero;
                    CellsDictionary[i].transform.localScale = Vector3.one;
                }
                
                CellsDictionary[i]._selected.SetActive(false);
            }
            GridLayoutGroup GridLayoutGroup = BoxT.GetComponent<GridLayoutGroup>();
            hangshu = AccountSet._AccInfo.Stoneboxsize / GridLayoutGroup.constraintCount + 1;
            BoxT.sizeDelta = new Vector2(BoxT.sizeDelta.x, (GridLayoutGroup.cellSize.x + GridLayoutGroup.spacing.x) * hangshu);
        }
        
        // 加载技能石背包机能
        // -1: 技能石合成 0:强化素材添加模式  1 : showMode 2: skilledit 3 : 技能展示器
        public void CellsFeatureLoad(int mode)
        {
            for (int i = 0; i < CellsDictionary.Count; i++)
            {
                switch(mode)
                {
                    case -1:
                        CellButtonBeheviour_StoneMergeMode(CellsDictionary[i]);
                    break;
                    case 0:
                        CellButtonBeheviour_MAdd(CellsDictionary[i]);
                    break;
                    case 1:
                        CellButtonBeheviour_STStoneShow(CellsDictionary[i]);
                    break;
                    case 2:
                        CellButtonBeheviour_EditCharSkill(CellsDictionary[i]);
                    break;
                    case 3:
                        CellButtonBeheviour_SKillShowMode(CellsDictionary[i]);
                    break;
                }
            }
        }
        
        public StoneCell GetFirstEmptyCell()
        {
            foreach (KeyValuePair<int, StoneCell> keyValuePair in CellsDictionary)
            {
                if (keyValuePair.Value.GetItem() != null)
                    continue;
                return keyValuePair.Value;
            }
            return null;
        }
        
        // Show Character icon using this SkillStone
        public IEnumerator ShowUsingChar(SKStoneItem Item, HeroIcon targetIcon)
        {
            if (Item == null || Item.SkillStoneOfPlayerId == null)
            {
                targetIcon.gameObject.SetActive(false);
                yield break;
            }
            StoneOfPlayerInfo SSOfPlayerInfo = MySkillStones.Get(Item.SkillStoneOfPlayerId);
            if (SSOfPlayerInfo == null)
            {
                Debug.Log("逻辑错误. SkillStoneOfPlayerId:"+ Item.SkillStoneOfPlayerId);
                yield break;
            }
            
            if (SSOfPlayerInfo.inUsingMonsterOfPlayerId == null)
            {
                targetIcon.gameObject.SetActive(false);
                yield break;
            }
            MonsterOfPlayerInfo _one = MyMonsters.Get(SSOfPlayerInfo.inUsingMonsterOfPlayerId);
            if (_one == null)
            {
                targetIcon.gameObject.SetActive(false);
                yield break;
            }
            targetIcon.gameObject.SetActive(true);
            CharConfig charConfig = MonstersConfigTable.GetCharConfig(_one.monsterId);
            targetIcon.ChangeIcon(charConfig == null ? null : MonsterIconDic.GetMonsterIconSyn(charConfig.RECORD_ID),
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
                    close = false,
                    near = false,
                    far = false
                };
                StoneFilterForm filterForm1 = new StoneFilterForm
                {
                    type = C_Types[i],
                    exType = new int[1] { 1 },
                    close = false,
                    near = false,
                    far = false
                };
                StoneFilterForm filterForm2 = new StoneFilterForm
                {
                    type = C_Types[i],
                    exType = new int[1] { 2 },
                    close = false,
                    near = false,
                    far = false
                };
                StoneFilterForm filterForm3 = new StoneFilterForm
                {
                    type = C_Types[i],
                    exType = new int[1] { 3 },
                    close = false,
                    near = false,
                    far = false
                };
                
                List<string> SkillStonesOfType_normal = MySkillStones.TargetStonesFromAccount(filterForm0);
                List<string> SkillStonesOfType_EX1 = MySkillStones.TargetStonesFromAccount(filterForm1);
                List<string> SkillStonesOfType_EX2 = MySkillStones.TargetStonesFromAccount(filterForm2);
                List<string> SkillStonesOfType_EX3 = MySkillStones.TargetStonesFromAccount(filterForm3);
                
                if (SkillStonesOfType_normal.Count > AccountSet._AccInfo.Stoneboxsize)
                {
                    error_massegas.Add(C_Types[i]+"类角色的普通技能石数量超过限制");
                }
                if (SkillStonesOfType_EX1.Count > AccountSet._AccInfo.Stoneboxsize)
                {
                    error_massegas.Add(C_Types[i]+"类角色的一级必杀技能石数量超过限制");
                }
                if (SkillStonesOfType_EX2.Count > AccountSet._AccInfo.Stoneboxsize)
                {
                    error_massegas.Add(C_Types[i]+"类角色的二级必杀技能石数量超过限制");
                }
                if (SkillStonesOfType_EX3.Count > AccountSet._AccInfo.Stoneboxsize)
                {
                    error_massegas.Add(C_Types[i]+"类角色的三级必杀技能石数量超过限制");
                }
            }
            return error_massegas;
        }
    }
}

