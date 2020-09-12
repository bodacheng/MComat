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
        
        public static IDictionary<int, StoneCell> CellsDictionary = new Dictionary<int, StoneCell>();//Cell这个东西我每次进入场景重新生成一次就可以。
        
        public static void PreventCellsFromDestroy()
        {
            foreach (KeyValuePair<int, StoneCell> keyValuePair in CellsDictionary)
            {
                keyValuePair.Value.transform.SetParent( ResourceKeeper.dontDestroyOnLoadParent);
            }
        }
        
        // 当下这个函数貌似每次启动背包都运行一次也没什么大的问题，需要考虑cellsLimit发生变化瞬间的处理。
        public void GenerateCells(int cellsLimit, int mode)
        {
            int hangshu = 1;
            for (int i = 0; i < cellsLimit; i++)
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
            hangshu = cellsLimit / GridLayoutGroup.constraintCount + 1;
            BoxT.sizeDelta = new Vector2(BoxT.sizeDelta.x, (GridLayoutGroup.cellSize.x + GridLayoutGroup.spacing.x) * hangshu);
            CellsFeatureLoad(cellsLimit, mode);
        }
        
        public void CellsFeatureLoad(int cellsLimit, int mode) // 0:强化素材添加模式  1 : showMode 2: skilledit 3 : 技能展示器
        {
            for (int i = 0; i < cellsLimit; i++)
            {
                switch(mode)
                {
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
                CellsDictionary[i]._SelectMode = StoneCell.SelectMode.single;
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
            SkillStoneOfPlayerInfoModel SSOfPlayerInfo = MySkillStonesReader.Get(Item.SkillStoneOfPlayerId);
            if (SSOfPlayerInfo.inUsingMonsterOfPlayerId == null)
            {
                targetIcon.gameObject.SetActive(false);
                yield break;
            }
            GetMonsterOfPlayerDetailModel _one = AccountCharsSet.Get(SSOfPlayerInfo.inUsingMonsterOfPlayerId);
            if (_one == null)
            {
                Debug.Log("逻辑错误。inUsingMonsterOfPlayerId："+ SSOfPlayerInfo.inUsingMonsterOfPlayerId);
                targetIcon.gameObject.SetActive(false);
                yield break;
            }
            targetIcon.gameObject.SetActive(true);
            CharConfig charConfig = MonstersConfigTable.GetCharConfig(_one.monsterId);
            targetIcon.ChangeIcon(charConfig == null ? null : MonsterIconDic.Instance.GetMonsterIconSyn(charConfig.RECORD_ID),
            charConfig == null ? Zokusei.Null : charConfig._zokusei);
        }
        
        public static List<string> CheckIfExceedCellLimit()
        {
            List<string> error_massegas = new List<string>();
            List<string> C_Types = MonstersConfigTable.Instance.GetTypeList();
            for (int i = 0; i < C_Types.Count; i++)
            {
                List<string> SkillStonesOfType_normal = MySkillStonesReader.TargetStonesFromAccount(C_Types[i], 0, true, true, true);
                List<string> SkillStonesOfType_EX1 = MySkillStonesReader.TargetStonesFromAccount(C_Types[i], 1, true, true, true);
                List<string> SkillStonesOfType_EX2 = MySkillStonesReader.TargetStonesFromAccount(C_Types[i], 2, true, true, true);
                List<string> SkillStonesOfType_EX3 = MySkillStonesReader.TargetStonesFromAccount(C_Types[i], 3, true, true, true);
                
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

