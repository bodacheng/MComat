using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

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
public class SkillStonesBox : MonoBehaviour {

    [Header("主")]
    public preparingScene _preparingScene;

    [Header("画面主模块parent")]
    public RectTransform NineAndTwoAndSkillBoxCanvas;
    public RectTransform BoxWholeT,BoxT,stonesTempContainer;

    [Space(7)]
    [Header("type按钮")]
    public Dropdown types;
    public Button NormalTab;
    public Button EX1Tab;
    public Button EX2Tab;
    public Button EX3Tab;

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
    public int cellsLimit;

    [Space(7)]
    [Header("技能石与格子pretab")]
    public DragAndDropItem SkillStonePrefab;
    public DragAndDropCell Cellprefab;

    [Space(7)]
    [Header("TheNineSlot")]
    public TheNineSlot _TheNineSlot;

    [Space(7)]
    [Header("UI elements 盒子中石头详细")]
    public Text keyname;
    public Text Showname;

    private IDictionary<int, SkillStoneCell> CellsDictionary = new Dictionary<int, SkillStoneCell>();//Cell这个东西我每次进入场景重新生成一次就可以。
    private List<DragAndDropItem> mySkillStones = new List<DragAndDropItem>();
    private string focusingtype;
    private EX focusingExType;
    private SkillStoneSlot DeleteSkillStoneSlot;

    public IEnumerator startUp()
    {
        yield return _SkillStoneBoxTabEffectsManager.startUp();
        // 玩家可能在什么时候会把Cell的数量进行扩充？cellsLimit从哪进行读取？
        DeleteArea.isDeleteArea = true;
        DeleteSkillStoneSlot = new SkillStoneSlot(null, DeleteArea, _TheNineSlot, this);
        yield return generateStones();
        generateCells(cellsLimit);
    }
        
    public string getFocusingType()
    {
        return focusingtype;
    }
    public void setFocusingType(string type)
    {
        this.focusingtype = type;
    }
    public EX getFocusingExType()
    {
        return focusingExType;
    }

    public void CellButtonBeheviour(SkillStoneCell _SkillStoneCell)
    {
        Button button = _SkillStoneCell._DragAndDropCell.GetComponent<Button>();
        if (button != null)
        {
            UnityEngine.Events.UnityAction buttonFeature = () => {
                DragAndDropItem _stoneOnCell = _SkillStoneCell._DragAndDropCell.GetItem();
                if (_stoneOnCell != null && _stoneOnCell._SkillConfigOfSkillStone != null)
                {
                    keyname.text = _stoneOnCell._SkillConfigOfSkillStone.keyName;
                    Showname.text = _stoneOnCell._SkillConfigOfSkillStone.ShowName;
                }
            };
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(buttonFeature);
        }
    }

    // 功能系。刷新技能石陈列界面
    public IEnumerator EXTabsFeatureRefresh(String type,bool viewingMode)
    {
        List<string> typesOfStoneIhave = new List<string>();
        foreach (DragAndDropItem Value in mySkillStones)
        {
            if (!typesOfStoneIhave.Contains(Value._SkillConfigOfSkillStone.type))
            {
                typesOfStoneIhave.Add(Value._SkillConfigOfSkillStone.type);
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
        }else{
            types.gameObject.SetActive(false);
        }

        NormalTab.onClick.RemoveAllListeners();
        UnityEngine.Events.UnityAction NormalTabFeature = () => {
            List<int> usingStoneIDs = _TheNineSlot.getUsingStonesId();
            string targetType = type.Clone() as string;
            arrangeSkillStonesToBox(targetType, EX.normal, 
                                    closeCheckBox.isOn,
                                    nearCheckBox.isOn,
                                    farCheckBox.isOn,
                                    outRangeCheckBox.isOn,
                                    usingStoneIDs);
        };
        NormalTab.onClick.AddListener(NormalTabFeature);

        EX1Tab.onClick.RemoveAllListeners();
        UnityEngine.Events.UnityAction Ex1TabFeature = () => {
            List<int> usingStoneIDs = _TheNineSlot.getUsingStonesId();
            string targetType = type.Clone() as string;
            arrangeSkillStonesToBox(targetType, EX.EX1,
                                    closeCheckBox.isOn,
                                    nearCheckBox.isOn,
                                    farCheckBox.isOn,
                                    outRangeCheckBox.isOn,
                                    usingStoneIDs);
        };
        EX1Tab.onClick.AddListener(Ex1TabFeature);

        EX2Tab.onClick.RemoveAllListeners();
        UnityEngine.Events.UnityAction Ex2TabFeature = () => {
            List<int> usingStoneIDs = _TheNineSlot.getUsingStonesId();
            string targetType = type.Clone() as string;
            arrangeSkillStonesToBox(targetType, EX.EX2,
                                    closeCheckBox.isOn,
                                    nearCheckBox.isOn,
                                    farCheckBox.isOn,
                                    outRangeCheckBox.isOn,
                                    usingStoneIDs);
        };
        EX2Tab.onClick.AddListener(Ex2TabFeature);

        EX3Tab.onClick.RemoveAllListeners();
        UnityEngine.Events.UnityAction Ex3TabFeature = () => {
            List<int> usingStoneIDs = _TheNineSlot.getUsingStonesId();
            string targetType = type.Clone() as string;
            arrangeSkillStonesToBox(targetType, EX.EX3,
                                    closeCheckBox.isOn,
                                    nearCheckBox.isOn,
                                    farCheckBox.isOn,
                                    outRangeCheckBox.isOn,
                                    usingStoneIDs);
        };
        EX3Tab.onClick.AddListener(Ex3TabFeature);

        closeCheckBox.onValueChanged.RemoveAllListeners();
        closeCheckBox.onValueChanged.AddListener(delegate { rangeCheckBoxOnValueChanged();});
        nearCheckBox.onValueChanged.RemoveAllListeners();
        nearCheckBox.onValueChanged.AddListener(delegate { rangeCheckBoxOnValueChanged(); });
        farCheckBox.onValueChanged.RemoveAllListeners();
        farCheckBox.onValueChanged.AddListener(delegate { rangeCheckBoxOnValueChanged(); });

        yield break;
    }

    void rangeCheckBoxOnValueChanged()
    {
        arrangeSkillStonesToBox(focusingtype, focusingExType,
                                closeCheckBox.isOn,
                                nearCheckBox.isOn,
                                farCheckBox.isOn,
                                outRangeCheckBox.isOn,
                                _TheNineSlot.getUsingStonesId());
    }

    public void typeDropDownBehaviour()// 直接放在type下拉按钮上的功能
    {
        string targetType = types.options[types.value].text.Clone() as string;
        _preparingScene.triggerMainProcess(EXTabsFeatureRefresh(targetType, true));
    }

    //围绕这个环节的一个问题是玩家账户中格子数量的问题。并且可以看到这个函数的运行时点非常重要，牵扯到对其进行消灭
    public void generateCells(int cellsLimit)
    {
        //下面的环节用意不明。
        foreach (KeyValuePair<int, SkillStoneCell> _pair in CellsDictionary)
        {
            _pair.Value._DragAndDropCell.RemoveItemWithOutDestroy();
            Destroy(_pair.Value._DragAndDropCell.gameObject);
        }
        
        Cellprefab.gameObject.GetComponent<Image>().sprite = Cell;
        CellsDictionary = new Dictionary<int, SkillStoneCell>();
        for (int i = 0; i < cellsLimit; i++)
        {
            DragAndDropCell cell = Instantiate(Cellprefab);
            if (cell != null)
            {
                cell.empty = new Color(1, 1, 1, 0.6f);
                cell.full = new Color(1, 1, 1, 1);
                cell.cellType = DragAndDropCell.CellType.DragOnly;
                cell._SkillStoneSlot = null;//技能石box里用不到这个
                cell.RemoveItemWithOutDestroy();//根据之前经验，这个东西有出错的可能
                cell.gameObject.SetActive(true);
                cell.transform.SetParent(BoxT);
                cell.transform.localScale = Vector3.one;
                CellsDictionary.Add(i, new SkillStoneCell(i, cell));
                CellButtonBeheviour(CellsDictionary[i]);
            }else{
                Debug.Log("Cell Pretab有误 没有Cell组件");
            }
        }
    }

    // CellsDictionary在场景切换等情况下需要注意正确刷新
    // 注意看把技能石头放到盒子里的过程，是个依次的过程而已，因此这个函数有个把石头在格子里重新排序的功能。
    public void arrangeSkillStonesToBox(string type,EX exType,bool close,bool near,bool far,bool outrange,List<int> usingStoneIDs)
    {
        foreach (KeyValuePair<int,SkillStoneCell> cellPair in CellsDictionary)
        {
            if (cellPair.Value._DragAndDropCell.GetItem() != null)
            {
                GameObject stoneGameObject = cellPair.Value._DragAndDropCell.GetItem().gameObject;
                stoneGameObject.SetActive(false);
                //stoneGameObject.transform.localScale = Vector3.one;
                stoneGameObject.transform.SetParent(stonesTempContainer);
                cellPair.Value._DragAndDropCell.UpdateMyItem();//单纯的通过null化物体的parent不会让Cell组件所记录的“放置中item”撤销
            }
        }

        List<DragAndDropItem> SkillStonesOfTypeAndExType = new List<DragAndDropItem>();
        foreach (DragAndDropItem Value in mySkillStones)
        {
            if (Value._SkillConfigOfSkillStone.type == type // 总要带一个type。不提供全部type石头共同显示功能
                 &&
                (Value._SkillConfigOfSkillStone.SPLevel == exType || exType == EX.NULL)//EX.NULL代表全type
               &&
                Value._SkillConfigOfSkillStone.rangeLimit(close, near, far,outrange))
            {
                SkillStonesOfTypeAndExType.Add(Value);
            }else{
                if (((usingStoneIDs != null && !usingStoneIDs.Contains(Value.myskillstone_localid)) || usingStoneIDs == null))
                    Value.gameObject.SetActive(false);
                else
                    Debug.Log("使用中的石头："+ Value._SkillConfigOfSkillStone.keyName);
            }
        }

        //List<int> usingStoneIDs = new List<int>();
        int hangshu = 1;//行数
        int geshu = 0;
        for (int i = 0; i < SkillStonesOfTypeAndExType.Count; i++)
        {
            SkillStoneCell _SkillStoneCell;
            CellsDictionary.TryGetValue(i, out _SkillStoneCell);

            if (usingStoneIDs != null)
            {
                if (!usingStoneIDs.Contains(SkillStonesOfTypeAndExType[i].myskillstone_localid))
                {
                    SkillStonesOfTypeAndExType[i].GetComponent<Image>().color = Color.white;
                    _SkillStoneCell._DragAndDropCell.AddItem(SkillStonesOfTypeAndExType[i]);
                    SkillStonesOfTypeAndExType[i].inBox = true;
                    geshu += 1;
                }
                else
                    Debug.Log("有使用中的技能石头，直接跳过这一格");
            }else{
                SkillStonesOfTypeAndExType[i].GetComponent<Image>().color = Color.white;
                _SkillStoneCell._DragAndDropCell.AddItem(SkillStonesOfTypeAndExType[i]);//！！！！！这个环节会销毁被覆盖的石头。
                SkillStonesOfTypeAndExType[i].inBox = true;
                geshu += 1;
            }
        }
        hangshu = 1 + geshu / 5;
        BoxT.sizeDelta = new Vector2(BoxT.rect.width, Cellprefab.GetComponent<RectTransform>().rect.height * hangshu);
        //Cellprefab.GetComponent<RectTransform>().rect.height 应该是个固定的我们安排好了的数字？？？
    }

    public void clearAllCurrentEditingSkillStone()
    {
        if (mySkillStones != null)
        {
            foreach (DragAndDropItem _D in mySkillStones)
            {
                if (_D != null)
                    Destroy(_D.gameObject);
            }
            mySkillStones.Clear();
        }
        mySkillStones = new List<DragAndDropItem>();

        if (CellsDictionary != null)
        {
            foreach(KeyValuePair<int,SkillStoneCell> keyValuePair in CellsDictionary)
            {
                keyValuePair.Value._DragAndDropCell.UpdateMyItem();
            }
        }
    }

    // 真正删除技能石头是要通过服务器的API
    // 而本地的操作与远程的财产操作是分开的，为了效率我们不希望本地的拥有技能石列表在每次更新后都通过读取数据库重新生成，
    // 所以走了一个if requeset ok，本地直接修改索引的过程。
    public void deleteTheseStonesLocal(List<DragAndDropItem> stonesToDelete)//该函数不包含删除那个石头图标的操作。
    {
        List<int> toDeleteStonessLocalIds = new List<int>();

        List<DragAndDropItem> sureToDelete = new List<DragAndDropItem>();
        foreach (DragAndDropItem _stone in stonesToDelete)
        {
            toDeleteStonessLocalIds.Add(_stone._SkillConfigOfSkillStone.id);
            if (mySkillStones.Contains(_stone))
            {
                sureToDelete.Add(_stone);
            }
        }

        //从本模块的mySkillStones删除
        foreach (DragAndDropItem _stone in sureToDelete)
            mySkillStones.Remove(_stone);
        //从MySkillStonesReader模块的mySkillStonesDicByType删除
        MySkillStonesReader.RemoveTheseStonesFromLocalDic(toDeleteStonessLocalIds);
        arrangeSkillStonesToBox(getFocusingType(), getFocusingExType(),
                                        closeCheckBox.isOn,
                                        nearCheckBox.isOn,
                                        farCheckBox.isOn,
                                        outRangeCheckBox.isOn,
                                        null);
    }

    // 先生成技能石，然后按照需求来把石头显示在正确的地方。玩家拥有技能石头这个信息就是个int串，我们没有本地技能石头id这一说，所以技能石头我们是随用随生成
    // 玩家所拥有的所有技能石现在不存在所谓本地ID这一说，如此一来怎么对其进行索引就成了问题。
    // 字典SKillStoneDictionary是这些石头的管理索引，但这个key值只是个从0到Count的东西。
    // 从而，最大的问题是没法向其他资源循环环节那样，通过查看到底对应石头生成了没有来决定是否重新生成。
    // 因而在这个系统中我们的策略应该是每次调用一个技能编辑页面的时候都重新生成玩家应该拥有的全部石头。
    public IEnumerator generateStones()
    {
        clearAllCurrentEditingSkillStone();
        //如果玩家中途断网，下面这个环节不会有问题吗
        int myStoneLocalID = 0;
        foreach (KeyValuePair<string,List<int>> _keyValuePair in MySkillStonesReader.mySkillStonesDicByType)
        {
            if (_keyValuePair.Value != null)
            {
                for (int i = 0; i < _keyValuePair.Value.Count; i++)
                {
                    //你怎么确定的哪个石头生成了哪个没生成呢？
                    DragAndDropItem item = Instantiate(SkillStonePrefab);
                    item._SkillConfigOfSkillStone = MySkillStonesReader.getSkillConfigByID(_keyValuePair.Value[i]);
                    item.gameObject.name = "stone_" + item._SkillConfigOfSkillStone.type +"_" + item._SkillConfigOfSkillStone.keyName;
                    myStoneLocalID += 1;
                    item.myskillstone_localid = myStoneLocalID;
                    mySkillStones.Add(item);

                    IEnumerator process = null;
                    switch (defaultPools.Instance.IconLoadingMode)
                    {
                        case ResourceLoadMode.CachAB:
                            process = (skillIconsDic.Instance.findSkillIconByCach(_keyValuePair.Value[i]));
                            break;
                        case ResourceLoadMode.Resource:
                            process = (skillIconsDic.Instance.findSkillIconByResource(_keyValuePair.Value[i]));
                            break;
                        case ResourceLoadMode.StreamingAssetAB:
                            break;
                    }
                    yield return (process);
                    Sprite Icon = (Sprite)process.Current;
                    if (Icon == null)
                        Icon = skillIconsDic.Instance.getDefaultSkillIconByResource(item._SkillConfigOfSkillStone.SPLevel);
                    
                    item.GetComponent<Image>().sprite = Icon;
                    item.gameObject.transform.SetParent(stonesTempContainer);
                }
            }
        }
    }
}

public class SkillStoneCell
{
    int ID;
    public DragAndDropCell _DragAndDropCell;// .gameobject 是一个重要引用。
    public SkillStoneCell(int ID , DragAndDropCell _DragAndDropCell)
    {
        this.ID = ID;
        this._DragAndDropCell = _DragAndDropCell;
    }
}
