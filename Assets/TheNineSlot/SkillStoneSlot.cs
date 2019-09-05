using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using dataAccess;
using Api.Dto.Model;

// 编辑技能的两种模式，归根结底是9宫格自身的两种模式，即SkillStoneSlot的两种模式。
// 从我们使用那个插件开发这个环节至今，格子bug的根源其实是开始我们没有发现GetItem函数的正确发挥作用依赖于在那之前先运行updateMyItem函数，导致GetItem结果不正确。
// 造成了本来运行顺序就不怎么清晰一插件看起来更乱。
//然而现在，仍然有一个潜在问题存在，那就是在某一个九宫格的cell下可能出现两个石头。
//这个是把两个新石头拖入九宫后不停对两者进行位置移动所造成的。一旦这个现象出现就可能产生随之而来的一系列bug。
//但这个bug我们是以showOrigin()函数内强制清空所有石头的方法解决的。
//如果showOrigin()没给解决这个事情那那个bug还是会出现，说明这个环节某个部分还是存在些逻辑问题。
public class SkillStoneSlot
{
    public string OnSlotStonelocalID;
    public DragAndDropCell _DragAndDropCell;
    
    public SkillStoneSlot(string OnSlotStonelocalid, DragAndDropCell _DragAndDropCell)
    {
        this.OnSlotStonelocalID = OnSlotStonelocalid;
        this._DragAndDropCell = _DragAndDropCell;
        this._DragAndDropCell._SkillStoneSlot = this;
    }
    
    public void removeStoneFromSlot()
    {
        _DragAndDropCell.UpdateMyItem();
        DragAndDropItem _DragAndDropItem = _DragAndDropCell.GetItem();
        if (_DragAndDropItem)
            _DragAndDropItem.gameObject.transform.SetParent(MySkillStonesReader.SkillStonesBox.stonesTempContainer);
        _DragAndDropCell.UpdateMyItem();
    }
    
    public void returnStoneToBox()
    {
        _DragAndDropCell.UpdateMyItem();
        DragAndDropItem _DragAndDropItem = _DragAndDropCell.GetItem();
        if (_DragAndDropItem)
        {
            DragAndDropCell dragAndDropCell = MySkillStonesReader.SkillStonesBox.getFirstEmptyCell();
            if (dragAndDropCell != null)
                dragAndDropCell.AddItem(_DragAndDropItem);
            else
                _DragAndDropItem.gameObject.transform.SetParent(MySkillStonesReader.SkillStonesBox.stonesTempContainer);
        }
        _DragAndDropCell.UpdateMyItem();
    }

    //这个函数指的是格子自身的更新
    // 一个是在readANineAndTwo时候作用，也就是读取角色技能至九宫格的初期，
    // 一个是在SeliWholeNineAndTwo()里作用，进一步说就是每次石头的拖拽行为结束时(拖到某格子内或某空白区)
    // 这个进程的一个细节作用在于对格子颜色的更新，目前是这样：
    // 如果格子显示的是已经有的技能，则为半透明白
    // 如果是新石头，则普通技能为黄Color(1, 1, 0,1)，必杀技能为红Color(1, 0, 1, 1)
    public IEnumerator showOrigin(Color stoneColor)
    {
        removeStoneFromSlot();
        if (this.OnSlotStonelocalID != null)
        {
            yield return takeASkillStoneFromBoxToSlot(this.OnSlotStonelocalID,stoneColor);
        }
        this._DragAndDropCell.UpdateMyItem();
        _DragAndDropCell.GetComponent<Image>().color = new Color(1, 1, 1, 0.2f);
        yield break;
    }

    public IEnumerator takeASkillStoneFromBoxToSlot(string OnSlotStonelocalID, Color itemColor)
    {
        SkillStoneOfPlayerInfoModel SkillStoneOfPlayerInfoModel = MySkillStonesReader.Instance.getSkillStoneOfPlayerInfoModelByMyStoneId(OnSlotStonelocalID);
        DragAndDropItem _DragAndDropItem = MySkillStonesReader.Instance.getOneStoneModel(OnSlotStonelocalID);
        if (_DragAndDropItem == null)
            yield break;
        _DragAndDropItem.GetComponent<Image>().color = itemColor;
        if (_DragAndDropItem._SkillConfigOfSkillStone.SPLevel == 0)
        {
            _DragAndDropCell.GetComponent<Image>().color = new Color(0, 1, 1, 1f); //普通技能格子为黄 
        }else{
            _DragAndDropCell.GetComponent<Image>().color = new Color(1, 0, 0, 1f); //必杀技技能为红
        }
        _DragAndDropCell.AddItem(_DragAndDropItem);
        _DragAndDropCell.UpdateMyItem();
        yield break;
    }
}