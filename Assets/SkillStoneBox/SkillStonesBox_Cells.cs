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
        [Header("格子图标")]
        public Sprite Cell;
        
        [Space(7)]
        [Header("格子pretab")]
        public DragAndDropCell Cellprefab;
        
        [Space(7)]
        [Header("石头滚动视窗")]
        public ScrollRect stoneviewScrollRect;
        
        // 围绕这个环节的一个问题是玩家账户中格子数量的问题。
        // 当下这个函数貌似每次启动背包都运行一次也没什么大的问题，需要考虑cellsLimit发生变化瞬间的处理。
        public void GenerateCells(int cellsLimit)
        {
            int hangshu = 1;
            Cellprefab.gameObject.GetComponent<Image>().sprite = Cell;
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
                    CellButtonBeheviour(CellsDictionary[i]);
                }
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
        public void CellButtonBeheviour(DragAndDropCell _SkillStoneCell)
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
            CharacterResourceInfo characterResourceInfo = null;
            IEnumerator getchar = AccountCharsSet.instance.GetAccountCharInfo(SkillStoneOfPlayerInfoModel.inUsingMonsterOfPlayerId);
            yield return getchar;
            GetMonsterOfPlayerDetailModel _one = (GetMonsterOfPlayerDetailModel)getchar.Current;
            if (_one == null)
            {
                targetIcon.gameObject.SetActive(false);
                yield break;
            }
            targetIcon.gameObject.SetActive(true);
            characterResourceInfo = MonstersConfigTable.GetCharacterResourceInfo(_one.monsterId);
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

