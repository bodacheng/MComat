using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.UI;
using dataAccess;
using Api.Dto.Model;

// SkillStone首先是种什么东西，以什么形式存在。。。
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
        public static SkillStonesBox Instance;

        [Header("画面主模块parent")]
        public RectTransform SkillBoxCanvas;
        public RectTransform BoxWholeT, BoxT, stonesTempContainer;

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
        [Header("技能石详细")]
        public SkillStoneDetail _skillStoneDetail;
       
        [Header("fxcamera")]
        public Camera fxCamera;

        IDictionary<int, DragAndDropCell> CellsDictionary = new Dictionary<int, DragAndDropCell>();//Cell这个东西我每次进入场景重新生成一次就可以。
        string focusingtype = "human";
        int focusingExType;
        SkillStoneSlot DeleteSkillStoneSlot;

        void Awake()
        {
            Instance = this;
        }

        public IEnumerator StartUp()
        {
            yield return _SkillStoneBoxTabEffectsManager.StartUp();
            // 玩家可能在什么时候会把Cell的数量进行扩充？cellsLimit从哪进行读取？
            DeleteArea.cellPhase = DragAndDropCell.CellPhase.DeleteArea;
            DeleteSkillStoneSlot = new SkillStoneSlot(null, DeleteArea);
            GenerateCells(cellsLimit);
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

        public string GetFocusingType()
        {
            return focusingtype;
        }
        public void SetFocusingType(string type)
        {
            focusingtype = type;
        }
        public int GetFocusingExType()
        {
            return focusingExType;
        }

        public void CellButtonBeheviour(DragAndDropCell _SkillStoneCell)
        {
            Button button = _SkillStoneCell.GetComponent<Button>();
            if (button != null)
            {
                void buttonFeature()
                {
                    DragAndDropItem _stone = _SkillStoneCell.GetItem();
                    if (_stone != null && _stone._SkillConfigOfSkillStone != null)
                    {
                        _skillStoneDetail.keyname.text = _stone._SkillConfigOfSkillStone.REAL_NAME;
                        _skillStoneDetail.Showname.text = _stone._SkillConfigOfSkillStone.ShowName;
                        _skillStoneDetail.ShowSkillStoneExType(_stone._SkillConfigOfSkillStone.SP_LEVEL);
                        _skillStoneDetail.SwitchUsingMonsterIcon(_stone.skillStoneOfPlayerId);
                    }
                }
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(buttonFeature);
            }
        }
        
        public void NormalTabFeature(GameObject self)
        {
            _SkillStoneBoxTabEffectsManager.Skillbuttonexplosion(ButtonEffectInFxCameraWorldSpace(fxCamera,self, 3));
            focusingExType = 0;
            TheNineSlot.Instance.mainProcessRunner.TriggerMainProcess(ArrangeSkillStonesToBox());
        }

        public void EX1TabFeature(GameObject self)
        {
            _SkillStoneBoxTabEffectsManager.Skillbuttonexplosion(ButtonEffectInFxCameraWorldSpace(fxCamera,self, 3));
            focusingExType = 1;
            TheNineSlot.Instance.mainProcessRunner.TriggerMainProcess(ArrangeSkillStonesToBox());
        }

        public void EX2TabFeature(GameObject self)
        {
            _SkillStoneBoxTabEffectsManager.Skillbuttonexplosion(ButtonEffectInFxCameraWorldSpace(fxCamera,self, 3));
            focusingExType = 2;
            TheNineSlot.Instance.mainProcessRunner.TriggerMainProcess(ArrangeSkillStonesToBox());
        }

        public void EX3TabFeature(GameObject self)
        {
            _SkillStoneBoxTabEffectsManager.Skillbuttonexplosion(ButtonEffectInFxCameraWorldSpace(fxCamera,self, 3));
            focusingExType = 3;
            TheNineSlot.Instance.mainProcessRunner.TriggerMainProcess(ArrangeSkillStonesToBox());
        }
        
        // 功能系。刷新技能石陈列界面。这里应该包括一个特殊功能，就是展示Tutorial模式下临时可用的那些石头
        public IEnumerator EXTabsFeatureRefresh(bool viewingMode)
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
                    Dropdown.OptionData m_NewData = new Dropdown.OptionData
                    {
                        text = Rname
                    };
                    types.options.Add(m_NewData);
                }
            }
            else
            {
                types.gameObject.SetActive(false);
            }
            closeCheckBox.onValueChanged.RemoveAllListeners();
            closeCheckBox.onValueChanged.AddListener(delegate { RangeCheckBoxOnValueChanged(); });
            nearCheckBox.onValueChanged.RemoveAllListeners();
            nearCheckBox.onValueChanged.AddListener(delegate { RangeCheckBoxOnValueChanged(); });
            farCheckBox.onValueChanged.RemoveAllListeners();
            farCheckBox.onValueChanged.AddListener(delegate { RangeCheckBoxOnValueChanged(); });
            yield break;
        }

        void RangeCheckBoxOnValueChanged()
        {
            TheNineSlot.Instance.mainProcessRunner.TriggerMainProcess(ArrangeSkillStonesToBox());
        }

        public void typeDropDownBehaviour()// 直接放在type下拉按钮上的功能
        {
            string targetType = types.options[types.value].text.Clone() as string;
            TheNineSlot.Instance.mainProcessRunner.TriggerMainProcess(EXTabsFeatureRefresh(true));
        }

        // 围绕这个环节的一个问题是玩家账户中格子数量的问题。
        // 当下这个函数貌似每次启动背包都运行一次也没什么大的问题，需要考虑cellsLimit发生变化瞬间的处理。
        public void GenerateCells(int cellsLimit)
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

        public IEnumerator ArrangeSkillStonesToBox()
        {
            yield return ArrangeSkillStonesToBox(GetFocusingType(), GetFocusingExType(), closeCheckBox.isOn, nearCheckBox.isOn, farCheckBox.isOn, outRangeCheckBox.isOn, TheNineSlot.Instance.GetUsingStonesId());
        }

        // stoneviewScrollRect 应该在这个函数里扮演一个作用。
        public IEnumerator ArrangeSkillStonesToBox(string type, int exType, bool close, bool near, bool far, bool outrange, List<String> usingStoneIDs)
        {
            foreach (KeyValuePair<int, DragAndDropCell> cellPair in CellsDictionary)
            {
                // 下面第一行（UpdateMyItem）至关重要。技能石box往往和九宫格一起显示，readANineAndTwo函数如果和arrangeSkillStonesToBox配合运行，
                // 都是前者在前，决定好在九宫格里显示的角色装备中石头是啥，先放在那里。这个时间点上技能石背包里的格子还没有断开和那几个石头的连接。如果你不UpdateMyItem一下，
                // 它会把已经放到九宫格里的石头给拔下来扔进stonesTempContainer。
                cellPair.Value.UpdateMyItem();
                DragAndDropItem dragAndDropItem = cellPair.Value.GetItem();
                if (dragAndDropItem != null)
                {
                    dragAndDropItem.transform.SetParent(stonesTempContainer);
                    cellPair.Value.UpdateMyItem(); //单纯的通过null化物体的parent不会让Cell组件所记录的“放置中item”撤销
                }
            }
            List<String> SkillStonesOfTypeAndExType = new List<String>(); //技能石本地id
            foreach (KeyValuePair<String, SkillStoneOfPlayerInfoModel> keyValuePair in MySkillStonesReader.mySkillStonesDataDic)
            {
                SkillConfig _SkillConfigOfSkillStone = SkillConfigTable.GetSkillConfigByID(keyValuePair.Value.skillId);
                if (_SkillConfigOfSkillStone.type == type && (_SkillConfigOfSkillStone.SP_LEVEL == exType || exType == -1) && SkillConfigTable.RangeLimit(_SkillConfigOfSkillStone.ai_trigger_ranges.ToList(),close, near, far, outrange))
                    SkillStonesOfTypeAndExType.Add(keyValuePair.Value.skillStoneOfPlayerId);
            }

            int cellindex = 0;
            for (int i = 0; i < SkillStonesOfTypeAndExType.Count; i++)
            {
                if (usingStoneIDs != null)
                {
                    if (!usingStoneIDs.Contains(SkillStonesOfTypeAndExType[i]))
                    {
                        CellsDictionary.TryGetValue(cellindex, out DragAndDropCell _SkillStoneCell);
                        cellindex++;
                        _SkillStoneCell.AddItem(MySkillStonesReader.mySkillStonesObjectsDic[SkillStonesOfTypeAndExType[i]]);
                        _SkillStoneCell.image.color = !AccountCharsSet.CheckIfContainsAccountCharsSetKey(MySkillStonesReader.Instance.GetSkillStoneOfPlayerInfoModelByMyStoneId(SkillStonesOfTypeAndExType[i]).inUsingMonsterOfPlayerId)
                            ? Color.white
                            : Color.yellow;
                    }
                    else
                    {
                        Debug.Log("有使用中的技能石头，直接跳过这一格");
                    }
                }
                else
                {
                    MySkillStonesReader.mySkillStonesObjectsDic[SkillStonesOfTypeAndExType[i]].GetComponent<Image>().color = Color.white;
                    CellsDictionary.TryGetValue(cellindex, out DragAndDropCell _SkillStoneCell);
                    cellindex++;
                    _SkillStoneCell.AddItem(MySkillStonesReader.mySkillStonesObjectsDic[SkillStonesOfTypeAndExType[i]]); //！！！！！这个环节会销毁被覆盖的石头。
                    _SkillStoneCell.image.color = !AccountCharsSet.CheckIfContainsAccountCharsSetKey(MySkillStonesReader.Instance.GetSkillStoneOfPlayerInfoModelByMyStoneId(SkillStonesOfTypeAndExType[i]).inUsingMonsterOfPlayerId)
                        ? Color.white
                        : Color.yellow;
                }
            }
            yield break;
        }
        
        public IEnumerator GenerateOneStone(SkillStoneOfPlayerInfoModel one)
        {
            SkillConfig _SkillConfig = SkillConfigTable.GetSkillConfigByID(one.skillId);
            if (_SkillConfig == null)
            {
                Debug.Log("巨大问题,技能id似乎未定义："+one.skillId);
                yield break;
            }
            if (MySkillStonesReader.mySkillStonesDataDic.ContainsKey(one.skillStoneOfPlayerId))
            {
                MySkillStonesReader.mySkillStonesDataDic[one.skillStoneOfPlayerId] = one;
            }else{
                MySkillStonesReader.mySkillStonesDataDic.Add(one.skillStoneOfPlayerId, one);
            }
            yield return GenerateOneStoneModel(one.skillStoneOfPlayerId);
        }

        public IEnumerator GenerateOneStoneModel(string skillStoneOfPlayerId)
        {
            if (MySkillStonesReader.mySkillStonesObjectsDic.ContainsKey(skillStoneOfPlayerId))
            {
                if (MySkillStonesReader.mySkillStonesObjectsDic[skillStoneOfPlayerId] != null)
                    yield break;
            }
            SkillStoneOfPlayerInfoModel skillStoneOfPlayerInfoModel = MySkillStonesReader.Instance.GetSkillStoneOfPlayerInfoModelByMyStoneId(skillStoneOfPlayerId);
            SkillConfig skillConfig = SkillConfigTable.GetSkillConfigByID(skillStoneOfPlayerInfoModel.skillId);
            
            IEnumerator process = null;
            switch (ResourceLoadingSetting.Instance.IconLoadingMode)
            {
                case ResourceLoadMode.CachAB:
                    process = (skillIconsDic.Instance.findSkillIconByCach(MySkillStonesReader.mySkillStonesDataDic[skillStoneOfPlayerId].skillId));
                    break;
                case ResourceLoadMode.Resource:
                    process = (skillIconsDic.Instance.findSkillIconByResource(MySkillStonesReader.mySkillStonesDataDic[skillStoneOfPlayerId].skillId));
                    break;
                case ResourceLoadMode.StreamingAssetAB:
                    break;
            }
            yield return (process);
            GameObject Icon = (GameObject)process.Current;
            if (Icon == null)
                Icon = Instantiate(skillIconsDic.Instance.getDefaultSkillIconByResource(skillConfig.SP_LEVEL));
            DragAndDropItem item = Icon.GetComponent<DragAndDropItem>();
            if (item == null)
                item = Icon.AddComponent<DragAndDropItem>();

            if (!MySkillStonesReader.mySkillStonesObjectsDic.ContainsKey(skillStoneOfPlayerId))
                MySkillStonesReader.mySkillStonesObjectsDic.Add(skillStoneOfPlayerId, item);
            else
                 MySkillStonesReader.mySkillStonesObjectsDic[skillStoneOfPlayerId] = item;

            item._SkillConfigOfSkillStone = SkillConfigTable.GetSkillConfigByID(MySkillStonesReader.mySkillStonesDataDic[skillStoneOfPlayerId].skillId);
            item.gameObject.name = "stone_" + item._SkillConfigOfSkillStone.type + "_" + item._SkillConfigOfSkillStone.REAL_NAME;
            item.skillStoneOfPlayerId = skillStoneOfPlayerId;
            item.gameObject.transform.SetParent(stonesTempContainer);           
        }
        
        Vector2 buttonAnchorPosition;
        Vector2 true_buttonAnchorPosition;
        Vector3 buttonWorldPosition;
        readonly int worldSpaceConvertMode = 1;// 1: canvas screen space 2: UI元素在左下角？忘了
        public Vector3 ButtonEffectInFxCameraWorldSpace(Camera fxcamera, GameObject UI_thing, float z_offset)
        {
            switch (worldSpaceConvertMode)
            {
                case 1:
                    buttonWorldPosition = UI_thing.transform.position;
                    buttonWorldPosition = new Vector3(buttonWorldPosition.x, buttonWorldPosition.y, buttonWorldPosition.z + z_offset);
                break;
                case 2:
                    buttonAnchorPosition = UI_thing.GetComponent<RectTransform>().transform.position;
                    true_buttonAnchorPosition = new Vector2(buttonAnchorPosition.x, buttonAnchorPosition.y);
                    buttonWorldPosition = fxcamera.ScreenToWorldPoint(true_buttonAnchorPosition);
                    buttonWorldPosition = new Vector3(buttonWorldPosition.x, buttonWorldPosition.y, fxcamera.transform.position.z + z_offset);
                break;
            }
            return buttonWorldPosition;
        }
    }
}