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
        public DragAndDropCell Cellprefab;
        
        [Space(5)]
        [Header("选中框")]
        public GameObject SelectedFrame;
        public static GameObject _Selected;

        [Space(7)]
        [Header("石头滚动视窗")]
        public ScrollRect stoneviewScrollRect;
        
        public IDictionary<int, DragAndDropCell> CellsDictionary = new Dictionary<int, DragAndDropCell>();//Cell这个东西我每次进入场景重新生成一次就可以。
        
        public static void SeletedRender(DragAndDropCell cell)
        {
            if (cell == null)
            {
                _Selected.SetActive(false);
                return;
            }
            
            if (cell._SelectMode == DragAndDropCell.SelectMode.single)
            {
                _Selected.SetActive(true);
                _Selected.transform.SetParent(cell.GetComponent<RectTransform>());
                _Selected.transform.localPosition = Vector3.zero;
                _Selected.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
                _Selected.GetComponent<RectTransform>().localScale = Vector3.one;
                _Selected.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
                _Selected.gameObject.SetActive(true);
            }
            else if (cell._SelectMode == DragAndDropCell.SelectMode.multi)
            {
            }
        }
        
        // 当下这个函数貌似每次启动背包都运行一次也没什么大的问题，需要考虑cellsLimit发生变化瞬间的处理。
        public void GenerateCells(int cellsLimit, int mode)// 1 : showMode 2: skilledit
        {
            int hangshu = 1;
            for (int i = 0; i < cellsLimit; i++)
            {
                if (!CellsDictionary.ContainsKey(i))//我姑且认为该字典里每个key值对应的SkillStoneCell对象不会凭空消失
                {
                    DragAndDropCell cell = Instantiate(Cellprefab);
                    cell.empty = new Color(1, 1, 1, 0.6f);
                    cell.full = new Color(1, 1, 1, 1);
                    cell.cellPhase = DragAndDropCell.CellPhase.SkillStoneBoxCell;
                    cell._SkillStoneSlot = null;//技能石box里用不到这个
                    cell.RemoveItemWithOutDestroy();//根据之前经验，这个东西有出错的可能
                    cell.gameObject.SetActive(true);
                    cell.transform.SetParent(BoxT);
                    cell.transform.localScale = Vector3.one;
                    CellsDictionary.Add(i, cell);                    
                }
                if (mode == 1)
                {
                    CellButtonBeheviour_STStoneShow(CellsDictionary[i]);
                }
                if (mode == 2)
                {
                    CellButtonBeheviour_EditCharSkill(CellsDictionary[i]);
                }
                CellsDictionary[i]._selected.SetActive(false);
                CellsDictionary[i]._SelectMode = DragAndDropCell.SelectMode.single;
            }
            GridLayoutGroup gridLayoutGroup = BoxT.GetComponent<GridLayoutGroup>();
            hangshu = cellsLimit / gridLayoutGroup.constraintCount + 1;
            BoxT.sizeDelta = new Vector2(BoxT.sizeDelta.x, (gridLayoutGroup.cellSize.x + gridLayoutGroup.spacing.x) * hangshu);
        }
                
        public DragAndDropCell GetFirstEmptyCell()
        {
            foreach (KeyValuePair<int, DragAndDropCell> keyValuePair in CellsDictionary)
            {
                if (keyValuePair.Value.GetItem() != null)
                    continue;
                return keyValuePair.Value;
            }
            return null;
        }
        
        float lastclicktime;
        public void CellButtonBeheviour_EditCharSkill(DragAndDropCell _SkillStoneCell)
        {
            Button button = _SkillStoneCell.GetComponent<Button>();
            if (button != null)
            {
                void buttonFeature()
                {
                    if (Time.time - lastclicktime < 0.25f) // double click
                    {
                        if (TheNineSlot.Instance.GetFocusingStoneSlot() != null)
                        {
                            _SkillStoneCell.DragStoneFromSKillStoneBoxToNineSlot(_SkillStoneCell,TheNineSlot.Instance.GetFocusingStoneSlot());
                        }
                    }
                    lastclicktime = Time.time;
                    DragAndDropItem _stone = _SkillStoneCell.GetItem();
                    if (_stone != null && _stone._SkillConfigOfSkillStone != null)
                    {
                        _skillStoneDetail.RefreshSkillDetail(_stone._SkillConfigOfSkillStone, _stone.SkillStoneOfPlayerId);
                    }
                }
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(buttonFeature);
                button.onClick.AddListener(delegate { SeletedRender(_SkillStoneCell); });
            }
        }
        
        public void CellButtonBeheviour_STStoneShow(DragAndDropCell _SkillStoneCell)
        {
            Button button = _SkillStoneCell.GetComponent<Button>();
            if (button != null)
            {
                void buttonFeature()
                {
                    DragAndDropItem _stone = _SkillStoneCell.GetItem();
                    if (_stone != null && _stone._SkillConfigOfSkillStone != null)
                    {
                        _skillStoneDetail.RefreshSkillDetail(_stone._SkillConfigOfSkillStone, _stone.SkillStoneOfPlayerId);
                    }
                }
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(buttonFeature);
                button.onClick.AddListener(delegate { SeletedRender(_SkillStoneCell); });
            }
        }
        
        public IEnumerator ShowUsingChar(DragAndDropItem dragAndDropItem, HeroIcon targetIcon)
        {
            if (dragAndDropItem == null || dragAndDropItem.SkillStoneOfPlayerId == null)
            {
                targetIcon.gameObject.SetActive(false);
                yield break;
            }
            SkillStoneOfPlayerInfoModel SkillStoneOfPlayerInfoModel = MySkillStonesReader.Instance.GetStoneOfPlayerInfoModelByMyStoneId(dragAndDropItem.SkillStoneOfPlayerId);
            if (SkillStoneOfPlayerInfoModel.inUsingMonsterOfPlayerId == null)
            {
                targetIcon.gameObject.SetActive(false);
                yield break;
            }
            CharConfig characterResourceInfo = null;
            IEnumerator getchar = AccountCharsSet.instance.GetAccountCharInfo(SkillStoneOfPlayerInfoModel.inUsingMonsterOfPlayerId);
            yield return getchar;
            GetMonsterOfPlayerDetailModel _one = (GetMonsterOfPlayerDetailModel)getchar.Current;
            if (_one == null)
            {
                targetIcon.gameObject.SetActive(false);
                yield break;
            }
            targetIcon.gameObject.SetActive(true);
            characterResourceInfo = MonstersConfigTable.GetCharConfig(_one.monsterId);
            targetIcon.ChangeIcon(characterResourceInfo == null ? null : MonsterIconDic.Instance.GetMonsterIconSyn(characterResourceInfo.RECORD_ID),
            characterResourceInfo == null ? Zokusei.Null : characterResourceInfo._zokusei);
            yield break;
        }
                
        public List<string> CheckIfExceedCellLimit()
        {
            List<string> error_massegas = new List<string>();
            List<string> characterTypes = MonstersConfigTable.Instance.GetTypeList();
            for (int i = 0; i < characterTypes.Count; i++)
            {
                List<string> SkillStonesOfType_normal = MySkillStonesReader.TargetStonesFromOfAccount(characterTypes[i], 0, true, true, true, true);
                List<string> SkillStonesOfType_EX1 = MySkillStonesReader.TargetStonesFromOfAccount(characterTypes[i], 1, true, true, true, true);
                List<string> SkillStonesOfType_EX2 = MySkillStonesReader.TargetStonesFromOfAccount(characterTypes[i], 2, true, true, true, true);
                List<string> SkillStonesOfType_EX3 = MySkillStonesReader.TargetStonesFromOfAccount(characterTypes[i], 3, true, true, true, true);
                
                if (SkillStonesOfType_normal.Count > AccountSet.Instance._PlayerAccountInfo.Stoneboxsize)
                {
                    error_massegas.Add(characterTypes[i]+"类角色的普通技能石数量超过限制");
                }
                if (SkillStonesOfType_EX1.Count > AccountSet.Instance._PlayerAccountInfo.Stoneboxsize)
                {
                    error_massegas.Add(characterTypes[i]+"类角色的一级必杀技能石数量超过限制");
                }
                if (SkillStonesOfType_EX2.Count > AccountSet.Instance._PlayerAccountInfo.Stoneboxsize)
                {
                    error_massegas.Add(characterTypes[i]+"类角色的二级必杀技能石数量超过限制");
                }
                if (SkillStonesOfType_EX3.Count > AccountSet.Instance._PlayerAccountInfo.Stoneboxsize)
                {
                    error_massegas.Add(characterTypes[i]+"类角色的三级必杀技能石数量超过限制");
                }
            }
            return error_massegas;
        }
    }
}

