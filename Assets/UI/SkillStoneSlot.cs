using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using EZObjectPools;
using UnityEngine.UI;

// 编辑技能的两种模式，归根结底是9宫格自身的两种模式，即SkillStoneSlot的两种模式。
public class SkillStoneSlot
{
    public SkillEditMode skillEditMode;
    public SkillConfig originSkillConfig;
    public DragAndDropCell _DragAndDropCell;
    public TheNineSlot _TheNineSlot;
    public SkillStonesBox _SkillStonesBox;

    public SkillStoneSlot(SkillConfig originSkillConfig, DragAndDropCell _DragAndDropCell, TheNineSlot _TheNineSlot, SkillStonesBox _SkillStonesBox)
    {
        this.originSkillConfig = originSkillConfig;
        this._DragAndDropCell = _DragAndDropCell;
        this._DragAndDropCell._SkillStoneSlot = this;
        this._TheNineSlot = _TheNineSlot;
        this._SkillStonesBox = _SkillStonesBox;
    }

    public void clear()
    {
        DragAndDropItem[] dragAndDropItems = _DragAndDropCell.transform.GetComponentsInChildren<DragAndDropItem>();
        foreach (DragAndDropItem dragAndDropItem in dragAndDropItems)
        {
            if (dragAndDropItem != null)
            {
                if (dragAndDropItem.myskillstone_localid == -1)
                {
                    GameObject.Destroy(dragAndDropItem.gameObject);
                }
                else
                {
                    dragAndDropItem.gameObject.transform.parent = null;
                    dragAndDropItem.gameObject.transform.localScale = new Vector3(1, 1, 1);
                    dragAndDropItem.gameObject.SetActive(false);
                }
            }
        }
        _DragAndDropCell.UpdateMyItem();
    }

    public void showOrigin()
    {
        showOrigin(Color.white);
    }

    public void showOrigin(Color stoneColor)
    {
        clear();
        if (this.originSkillConfig != null)
            GenerateASkillStoneAddToCell(this.originSkillConfig, skillEditMode, stoneColor);
        
        _DragAndDropCell.GetComponent<Image>().color = new Color(1, 1, 1, 0.2f);
        _DragAndDropCell.UpdateMyItem();
    }
    //9.19 九宫和技能石头箱之间的链接可以说是一直十分棘手的地方。目前我们是完成了一个看不出什么bug的版本。
    // 从我们使用那个插件开发这个环节至今，格子bug的根源其实是开始我们没有发现GetItem函数的正确发挥作用依赖于在那之前先运行updateMyItem函数，导致GetItem结果不正确。
    // 造成了本来运行顺序就不怎么清晰一插件看起来更乱。
    //然而现在，仍然有一个潜在问题存在，那就是在某一个九宫格的cell下可能出现两个石头。
    //这个是把两个新石头拖入九宫后不停对两者进行位置移动所造成的。一旦这个现象出现就可能产生随之而来的一系列bug。
    //但这个bug我们是以showOrigin()函数内强制清空所有石头的方法解决的。
    //如果showOrigin()没给解决这个事情那那个bug还是会出现，说明这个环节某个部分还是存在些逻辑问题。

    //这个函数指的是格子自身的更新
    // 一个是在readANineAndTwo时候作用，也就是读取角色技能至九宫格的初期，
    // 一个是在SeliWholeNineAndTwo()里作用，进一步说就是每次石头的拖拽行为结束时(拖到某格子内或某空白区)
    // 这个进程的一个细节作用在于对格子颜色的更新，目前是这样：
    // 如果格子显示的是已经有的技能，则为半透明白
    // 如果是新石头，则普通技能为黄Color(1, 1, 0,1)，必杀技能为红Color(1, 0, 1, 1)

    public void GenerateASkillStoneAddToCell(SkillConfig _SkillConfig, SkillEditMode skillEditMode, Color itemColor)
    {
        if (_DragAndDropCell != null)
        {
            _DragAndDropCell.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
        }
        else
        {
            Debug.Log("严重问题");
            return;
        }

        clear();

        if (_SkillConfig != null && _SkillConfig.id >= 0)
        {
            DragAndDropItem newItem = _TheNineSlot.generateOneDragAndDropItem();
            newItem._SkillConfigOfSkillStone = _SkillConfig;
            newItem.GetComponent<Image>().sprite = skillIconsDic.Instance.getDefaultSkillIconByResource(_SkillConfig.SPLevel);
            newItem.GetComponent<Image>().color = itemColor;

            if (newItem._SkillConfigOfSkillStone.SkillPoint > 0)
            {
                _DragAndDropCell.GetComponent<Image>().color = new Color(0, 1, 1, 1f); //普通技能格子为黄 
            }else{
                _DragAndDropCell.GetComponent<Image>().color = new Color(1, 0, 0, 1f); //必杀技技能为红
            }

            if (skillEditMode == SkillEditMode.AddStoneMode)
            {

            }
            else if (skillEditMode == SkillEditMode.EditSkillMode)
            {
            }
            _DragAndDropCell.AddItem(newItem);
        }
        _DragAndDropCell.UpdateMyItem();
    }
}

//颜色管理模块
public class SlotColorManger
{
    Color editingStoneColor1 = new Color(0.2f, 0.6f, 0.3f);
    Color editingStoneColor2 = new Color(0.2f, 0.1f, 0.3f);
    Color editingStoneColor3 = new Color(0.7f, 0.6f, 0.3f);
    Color editingStoneColor4 = new Color(0.2f, 0.1f, 0.3f);
    Color editingStoneColor5 = new Color(0.5f, 0.6f, 0.7f);
    Color editingStoneColor6 = new Color(0.2f, 0.6f, 0.3f);
    Color editingStoneColor7 = new Color(0.9f, 0.6f, 0.5f);
    Color editingStoneColor8 = new Color(0.1f, 0.6f, 0.7f);
    Color editingStoneColor9 = new Color(0.9f, 0.1f, 0.4f);

    public List<Color> ColorsToDistribute = new List<Color>();
    public IDictionary<int, Color> tempStoneColorDIC;

    public void ready()
    {
        tempStoneColorDIC = new Dictionary<int, Color>();

        ColorsToDistribute.Clear();
        ColorsToDistribute.Add(editingStoneColor1);
        ColorsToDistribute.Add(editingStoneColor2);
        ColorsToDistribute.Add(editingStoneColor3);
        ColorsToDistribute.Add(editingStoneColor4);
        ColorsToDistribute.Add(editingStoneColor5);
        ColorsToDistribute.Add(editingStoneColor6);
        ColorsToDistribute.Add(editingStoneColor7);
        ColorsToDistribute.Add(editingStoneColor8);
        ColorsToDistribute.Add(editingStoneColor9);
    }

    public Color DistributeColorRandomlyToAStone(int stoneNum)
    {
        int num = UnityEngine.Random.Range(0, ColorsToDistribute.Count);
        Color toReturn = ColorsToDistribute[num];
        ColorsToDistribute.RemoveAt(num);
        tempStoneColorDIC.Add(new KeyValuePair<int, Color>(stoneNum, toReturn));

        return toReturn;
    }
}