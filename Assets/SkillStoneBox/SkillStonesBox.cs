using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using dataAccess;
using Api.Dto.Model;

//SkillStone首先是种什么东西，以什么形式存在。。。
// 这个东西应该就和“我的拥有角色一样处理方式”
// 这个模块是针对SKillStonesBox的机能。。。它对各种...SKill石头的master table也好，T table 也好都是功能的使用者关系。

// 11.13号思考这样几个问题：
// 1.玩家的等级与CellLimit之间的制约关系怎么实现
// 2.从数据库阅读拥有技能石的函数在哪
// 3.当石头的数量超过了格子数量时候所进行的validation在哪。
// 4.有财产类的安全隐患吗。

// 18.1.6
// 这个模块缺乏这些函数：添加新技能石头(与技能石头盒子的画面配合？)
// 消耗某技能石头
namespace mainMenu
{
    public class SkillStonesBox : MonoBehaviour
    {
        [Header("画面主模块parent")]
        public RectTransform SkillBoxCanvas;
        public RectTransform BoxWholeT, BoxT, stonesTempContainer;

        [Space(7)]
        [Header("MonsterBox")]
        public MonsterBox _MonsterBox;

        [Space(7)]
        [Header("type按钮")]
        public Dropdown types;
        public Button NormalTab;
        public Button EX1Tab;
        public Button EX2Tab;
        public Button EX3Tab;

        [Space(7)]
        [Header("石头滚动视窗")]
        public ScrollRect stoneviewScrollRect;

        [Space(7)]
        [Header("type特效管理")]
        public SkillStoneBoxTabEffectsManager _SkillStoneBoxTabEffectsManager;

        [Space(7)]
        [Header("技能石头删除区域")]
        public DragAndDropCell DeleteArea;

        [Space(7)]
        [Header("攻击范围限定")]
        public Toggle closeCheckBox;
        public Toggle nearCheckBox;
        public Toggle farCheckBox;
        public Toggle outRangeCheckBox;

        [Space(7)]
        [Header("格子图标")]
        public Sprite Cell;

        [Space(7)]
        [Header("格子数量，将改为玩家账户决定")]
        public int cellsLimit;// 哪怕因为某些原因技能石头的总数量超过了背包大小，也绝对不应该去删除石头，只是做一些限制逼玩家去进行处理。

        [Space(7)]
        [Header("技能石与格子pretab")]
        public DragAndDropCell Cellprefab;

        [Space(7)]
        [Header("TheNineSlot")]
        public TheNineSlot _TheNineSlot;

        [Space(7)]
        [Header("技能石详细")]
        public skillStoneDetail _skillStoneDetail;

        [Space(7)]
        [Header("UI elements 盒子中石头详细")]
        public RectTransform usingCharacterIconPlace;
        charIcon stoneusingcharIcon;

        private IDictionary<int, DragAndDropCell> CellsDictionary = new Dictionary<int, DragAndDropCell>();//Cell这个东西我每次进入场景重新生成一次就可以。
        private string focusingtype;
        private int focusingExType = 0;
        private SkillStoneSlot DeleteSkillStoneSlot;

        public IEnumerator startUp()
        {
            MySkillStonesReader.SkillStonesBox = this;
            yield return _SkillStoneBoxTabEffectsManager.startUp();
            // 玩家可能在什么时候会把Cell的数量进行扩充？cellsLimit从哪进行读取？
            DeleteArea.cellPhase = DragAndDropCell.CellPhase.DeleteArea;
            DeleteSkillStoneSlot = new SkillStoneSlot(null, DeleteArea);
            generateCells(cellsLimit);
        }
        
        public DragAndDropCell getFirstEmptyCell()
        {
            foreach (KeyValuePair<int, DragAndDropCell> keyValuePair in CellsDictionary)
            {
                if (keyValuePair.Value.GetItem() != null)
                    continue;
                return keyValuePair.Value;
            }
            return null;
        }

        public string getFocusingType()
        {
            return focusingtype;
        }
        public void setFocusingType(string type)
        {
            this.focusingtype = type;
        }
        public int getFocusingExType()
        {
            return focusingExType;
        }

        public void CellButtonBeheviour(DragAndDropCell _SkillStoneCell)
        {
            Button button = _SkillStoneCell.GetComponent<Button>();
            if (button != null)
            {
                UnityEngine.Events.UnityAction buttonFeature = () =>
                {
                    DragAndDropItem _stone = _SkillStoneCell.GetItem();
                    if (_stone != null && _stone._SkillConfigOfSkillStone != null)
                    {
                        _skillStoneDetail.keyname.text = _stone._SkillConfigOfSkillStone.keyName;
                        _skillStoneDetail.Showname.text = _stone._SkillConfigOfSkillStone.ShowName;
                        _skillStoneDetail.showSkillStoneExType(_stone._SkillConfigOfSkillStone.SPLevel);
                        switchusingmonstericon(_stone.localID);
                    }
                };
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(buttonFeature);
            }
        }
        
        private void switchusingmonstericon(string stonemonsterOfPlayerId)
        {
            SkillStoneOfPlayerInfoModel SkillStoneOfPlayerInfoModel = MySkillStonesReader.Instance.getSkillStoneOfPlayerInfoModelByMyStoneId(stonemonsterOfPlayerId);
            if (SkillStoneOfPlayerInfoModel != null)
            {
                charIcon charIcon = _MonsterBox.getCharIcon(SkillStoneOfPlayerInfoModel.inUsingMonsterOfPlayerId);
                if (charIcon != null)
                {
                    if (stoneusingcharIcon)
                        stoneusingcharIcon.gameObject.transform.SetParent(_MonsterBox.MonsterBoxContainer);
                    charIcon.gameObject.SetActive(true);
                    charIcon.gameObject.transform.SetParent(usingCharacterIconPlace);
                    charIcon.transform.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;
                    stoneusingcharIcon = charIcon;
                }
                else
                {
                    if (stoneusingcharIcon)
                        stoneusingcharIcon.gameObject.transform.SetParent(_MonsterBox.MonsterBoxContainer);
                }
            }
            else
            {
                if (stoneusingcharIcon)
                    stoneusingcharIcon.gameObject.transform.SetParent(_MonsterBox.MonsterBoxContainer);
            }
        }

        public void NormalTabFeature()
        {
            this.focusingExType = 0;
            this._TheNineSlot.triggerMainProcess(arrangeSkillStonesToBox());
        }

        public void EX1TabFeature()
        {
            this.focusingExType = 1;
            this._TheNineSlot.triggerMainProcess(arrangeSkillStonesToBox());
        }

        public void EX2TabFeature()
        {
            this.focusingExType = 2;
            this._TheNineSlot.triggerMainProcess(arrangeSkillStonesToBox());
        }

        public void EX3TabFeature()
        {
            this.focusingExType = 3;
            this._TheNineSlot.triggerMainProcess(arrangeSkillStonesToBox());
        }

        // 功能系。刷新技能石陈列界面。这里应该包括一个特殊功能，就是展示Tutorial模式下临时可用的那些石头
        public IEnumerator EXTabsFeatureRefresh(String type, bool viewingMode)
        {
            List<string> typesOfStoneIhave = new List<string>();
            foreach (KeyValuePair<string, DragAndDropItem> keyValuePair in MySkillStonesReader.mySkillStonesObjectsDic)
            {
                if (!typesOfStoneIhave.Contains(keyValuePair.Value._SkillConfigOfSkillStone.type))
                {
                    typesOfStoneIhave.Add(keyValuePair.Value._SkillConfigOfSkillStone.type);
                }
            }
            if (viewingMode)
            {
                types.gameObject.SetActive(true);
                types.ClearOptions();
                foreach (string Rname in typesOfStoneIhave)
                {
                    Dropdown.OptionData m_NewData = new Dropdown.OptionData();
                    m_NewData.text = Rname;
                    types.options.Add(m_NewData);
                }
            }
            else
            {
                types.gameObject.SetActive(false);
            }
            closeCheckBox.onValueChanged.RemoveAllListeners();
            closeCheckBox.onValueChanged.AddListener(delegate { rangeCheckBoxOnValueChanged(); });
            nearCheckBox.onValueChanged.RemoveAllListeners();
            nearCheckBox.onValueChanged.AddListener(delegate { rangeCheckBoxOnValueChanged(); });
            farCheckBox.onValueChanged.RemoveAllListeners();
            farCheckBox.onValueChanged.AddListener(delegate { rangeCheckBoxOnValueChanged(); });
            yield break;
        }

        void rangeCheckBoxOnValueChanged()
        {
            _TheNineSlot.triggerMainProcess(arrangeSkillStonesToBox());
        }

        public void typeDropDownBehaviour()// 直接放在type下拉按钮上的功能
        {
            string targetType = types.options[types.value].text.Clone() as string;
            _TheNineSlot.triggerMainProcess(EXTabsFeatureRefresh(targetType, true));
        }

        // 围绕这个环节的一个问题是玩家账户中格子数量的问题。
        // 当下这个函数貌似每次启动背包都运行一次也没什么大的问题，需要考虑cellsLimit发生变化瞬间的处理。
        public void generateCells(int cellsLimit)
        {
            int hangshu = 1;
            Cellprefab.gameObject.GetComponent<Image>().sprite = Cell;
            for (int i = 0; i < cellsLimit; i++)
            {
                if (CellsDictionary.ContainsKey(i))//我姑且认为该字典里每个key值对应的SkillStoneCell对象不会凭空消失
                {
                }
                else
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
            hangshu = cellsLimit / 5;
            BoxT.sizeDelta = new Vector2(BoxT.sizeDelta.x, (100f + 7f) * hangshu - stoneviewScrollRect.gameObject.GetComponent<RectTransform>().sizeDelta.y);
        }

        public IEnumerator arrangeSkillStonesToBox()
        {
            yield return arrangeSkillStonesToBox(getFocusingType(), getFocusingExType(), closeCheckBox.isOn, nearCheckBox.isOn, farCheckBox.isOn, outRangeCheckBox.isOn, _TheNineSlot.getUsingStonesId());
        }

        // stoneviewScrollRect 应该在这个函数里扮演一个作用。
        public IEnumerator arrangeSkillStonesToBox(string type, int exType, bool close, bool near, bool far, bool outrange, List<String> usingStoneIDs)
        {
            foreach (KeyValuePair<int, DragAndDropCell> cellPair in CellsDictionary)
            {
                // 下面第一行（UpdateMyItem）至关重要。技能石box往往和九宫格一起显示，readANineAndTwo函数如果和arrangeSkillStonesToBox配合运行，
                // 都是前者在前，决定好在九宫格里显示的角色装备中石头是啥，先放在那里。这个时间点上技能石背包里的格子还没有断开和那几个石头的连接。如果你不UpdateMyItem一下，
                //它会把已经放到九宫格里的石头给拔下来扔进stonesTempContainer。
                cellPair.Value.UpdateMyItem();
                DragAndDropItem dragAndDropItem = cellPair.Value.GetItem();
                if (dragAndDropItem != null)
                {
                    dragAndDropItem.transform.SetParent(stonesTempContainer);
                    cellPair.Value.UpdateMyItem();//单纯的通过null化物体的parent不会让Cell组件所记录的“放置中item”撤销
                }
            }
            List<String> SkillStonesOfTypeAndExType = new List<String>();//localid
            foreach (KeyValuePair<String, SkillStoneOfPlayerInfoModel> keyValuePair in MySkillStonesReader.mySkillStonesDataDic)
            {
                SkillConfig _SkillConfigOfSkillStone = SkillsConfigInfos.getSkillConfigByID(keyValuePair.Value.skillId);
                if (_SkillConfigOfSkillStone.type == type && (_SkillConfigOfSkillStone.SPLevel == exType || exType == -1) && _SkillConfigOfSkillStone.rangeLimit(close, near, far, outrange))
                    SkillStonesOfTypeAndExType.Add(keyValuePair.Value.skillStoneOfPlayerId);
            }

            int cellindex = 0;
            for (int i = 0; i < SkillStonesOfTypeAndExType.Count; i++)
            {
                if (usingStoneIDs != null)
                {
                    if (!usingStoneIDs.Contains(SkillStonesOfTypeAndExType[i]))
                    {
                        DragAndDropCell _SkillStoneCell;
                        CellsDictionary.TryGetValue(cellindex, out _SkillStoneCell);
                        cellindex++;
                        _SkillStoneCell.AddItem(MySkillStonesReader.mySkillStonesObjectsDic[SkillStonesOfTypeAndExType[i]]);
                        if (!AccountCharsSet.checkifContainsAccountCharsSetKey(MySkillStonesReader.Instance.getSkillStoneOfPlayerInfoModelByMyStoneId(SkillStonesOfTypeAndExType[i]).inUsingMonsterOfPlayerId))
                            _SkillStoneCell.image.color = Color.white;
                        else
                            _SkillStoneCell.image.color = Color.yellow;
                    }
                    else
                        Debug.Log("有使用中的技能石头，直接跳过这一格");
                }
                else
                {
                    MySkillStonesReader.mySkillStonesObjectsDic[SkillStonesOfTypeAndExType[i]].GetComponent<Image>().color = Color.white;
                    DragAndDropCell _SkillStoneCell;
                    CellsDictionary.TryGetValue(cellindex, out _SkillStoneCell);
                    cellindex++;
                    _SkillStoneCell.AddItem(MySkillStonesReader.mySkillStonesObjectsDic[SkillStonesOfTypeAndExType[i]]);//！！！！！这个环节会销毁被覆盖的石头。
                    if (!AccountCharsSet.checkifContainsAccountCharsSetKey(MySkillStonesReader.Instance.getSkillStoneOfPlayerInfoModelByMyStoneId(SkillStonesOfTypeAndExType[i]).inUsingMonsterOfPlayerId))
                        _SkillStoneCell.image.color = Color.white;
                    else
                        _SkillStoneCell.image.color = Color.yellow;
                }
            }
            yield break;
        }

        public IEnumerator generateOneStone(string stonelocalid)
        {
            if (MySkillStonesReader.mySkillStonesObjectsDic.ContainsKey(stonelocalid))
            {
                if (MySkillStonesReader.mySkillStonesObjectsDic[stonelocalid] != null)
                    yield break;
            }
            SkillStoneOfPlayerInfoModel skillStoneOfPlayerInfoModel = MySkillStonesReader.Instance.getSkillStoneOfPlayerInfoModelByMyStoneId(stonelocalid);
            SkillConfig skillConfig = SkillsConfigInfos.getSkillConfigByID(skillStoneOfPlayerInfoModel.skillId);
            
            IEnumerator process = null;
            switch (ResourceLoadingSetting.Instance.IconLoadingMode)
            {
                case ResourceLoadMode.CachAB:
                    process = (skillIconsDic.Instance.findSkillIconByCach(MySkillStonesReader.mySkillStonesDataDic[stonelocalid].skillId));
                    break;
                case ResourceLoadMode.Resource:
                    process = (skillIconsDic.Instance.findSkillIconByResource(MySkillStonesReader.mySkillStonesDataDic[stonelocalid].skillId));
                    break;
                case ResourceLoadMode.StreamingAssetAB:
                    break;
            }
            yield return (process);
            GameObject Icon = (GameObject)process.Current;
            if (Icon == null)
                Icon = Instantiate(skillIconsDic.Instance.getDefaultSkillIconByResource(skillConfig.SPLevel));
            DragAndDropItem item = Icon.GetComponent<DragAndDropItem>();
            if (item == null)
                item = Icon.AddComponent<DragAndDropItem>();

            if (!MySkillStonesReader.mySkillStonesObjectsDic.ContainsKey(stonelocalid))
                MySkillStonesReader.mySkillStonesObjectsDic.Add(stonelocalid, item);
            else
                 MySkillStonesReader.mySkillStonesObjectsDic[stonelocalid] = item;

            item._SkillConfigOfSkillStone = SkillsConfigInfos.getSkillConfigByID(MySkillStonesReader.mySkillStonesDataDic[stonelocalid].skillId);
            item.gameObject.name = "stone_" + item._SkillConfigOfSkillStone.type + "_" + item._SkillConfigOfSkillStone.keyName;
            item.localID = stonelocalid;
            item.gameObject.transform.SetParent(stonesTempContainer);           
        }
    }
}