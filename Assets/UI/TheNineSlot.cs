using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using EZObjectPools;
using UnityEngine.UI;

//这个模块应该具备能力去读取一个角色的九宫信息。

public class TheNineSlot : MonoBehaviour {

    [Space(5)]
    [Header("几个重要RectTransform")]
    public RectTransform NineAndTwoAndSkillBoxCanvas;
    public RectTransform NineSlotT;

    [Space(5)]
    [Header("技能盒子要和九宫格本身联动")]
    public SkillStonesBox _skillStonesBox;

    [Space(5)]
    [Header("SKillPrintout")]
    public SkillsPrintOut _SkillsPrintOut;

    [Space(5)]
    [Header("九格")]
    public DragAndDropCell 
    A1DragAndDropCell, A2DragAndDropCell, A3DragAndDropCell,
    B1DragAndDropCell, B2DragAndDropCell, B3DragAndDropCell,
    C1DragAndDropCell, C2DragAndDropCell, C3DragAndDropCell;

    [Space(1)]
    [Header("添加技能石模式")]
    public Button AddStoneButton;
    [Space(1)]
    [Header("交换技能石位置模式")]
    public Button EditStoneButton;
    [Space(1)]
    [Header("技能石编辑确认")]
    public Button ConfirmSkillChangeButton;

    [Space(2)]
    [Header("技能信息")]
    public Text keyname;
    public Text Showname;
    public Text type;
    public Text ExType;
    public Text wholePoint;

    [Space(2)]
    [Header("主界面")]
    public preparingScene _preparingScene;

    private SkillStoneSlot A1Slot, A2Slot, A3Slot;
    private SkillStoneSlot B1Slot, B2Slot, B3Slot;
    private SkillStoneSlot C1Slot, C2Slot, C3Slot;
    private SlotColorManger _SlotColorManger;
    private List<SkillStoneSlot> allSlot = new List<SkillStoneSlot>();
    private NineAndTwo editingNineAndTwo;
    private AIStateRunner _focusingAIStateRunner;
    
    public void SlotButtonBeheviour(SkillStoneSlot skillStoneSlot)
    {
        Button button = skillStoneSlot._DragAndDropCell.gameObject.GetComponent<Button>();
        if (button != null)
        {
            UnityEngine.Events.UnityAction buttonFeature = () => {
                DragAndDropCell _DragAndDropCell = button.gameObject.GetComponent<DragAndDropCell>();

                if (_DragAndDropCell == null)
                    return;

                _DragAndDropCell.UpdateMyItem();
                DragAndDropItem _stoneOnCell = _DragAndDropCell.GetItem();
                if (_stoneOnCell != null && _stoneOnCell._SkillConfigOfSkillStone != null)
                {
                    keyname.text = _stoneOnCell._SkillConfigOfSkillStone.keyName;
                    Showname.text = _stoneOnCell._SkillConfigOfSkillStone.ShowName;
                    type.text = _stoneOnCell._SkillConfigOfSkillStone.type;
                    switch(_stoneOnCell._SkillConfigOfSkillStone.SPLevel)
                    {
                        case EX.normal:
                            ExType.text = "normal";
                            break;
                        case EX.EX1:
                            ExType.text = "Ex1";
                            break;
                        case EX.EX2:
                            ExType.text = "Ex2";
                            break;
                        case EX.EX3:
                            ExType.text = "Ex3";
                            break;
                        default:
                            ExType.text = "null?";
                            break;
                    }
                    this._preparingScene.triggerMainProcess(_SkillsPrintOut.skillShowRunWithPreparing(_stoneOnCell._SkillConfigOfSkillStone.keyName));
                }
            };
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(buttonFeature);
        }
    }

    public List<int> getUsingStonesId()//这个id列表其实是指的盒子为玩家拥有的技能石所赋予的临时id。
    {
        A1DragAndDropCell.UpdateMyItem();
        A2DragAndDropCell.UpdateMyItem();
        A3DragAndDropCell.UpdateMyItem();
        B1DragAndDropCell.UpdateMyItem();
        B2DragAndDropCell.UpdateMyItem();
        B3DragAndDropCell.UpdateMyItem();
        C1DragAndDropCell.UpdateMyItem();
        C2DragAndDropCell.UpdateMyItem();
        C3DragAndDropCell.UpdateMyItem();
    
        List<int> IDs = new List<int>();

        int A1 = A1DragAndDropCell.GetItem() != null ? A1DragAndDropCell.GetItem().myskillstone_localid : -1;
        int A2 = A2DragAndDropCell.GetItem() != null ? A2DragAndDropCell.GetItem().myskillstone_localid : -1;
        int A3 = A3DragAndDropCell.GetItem() != null ? A3DragAndDropCell.GetItem().myskillstone_localid : -1;
        int B1 = B1DragAndDropCell.GetItem() != null ? B1DragAndDropCell.GetItem().myskillstone_localid : -1;
        int B2 = B2DragAndDropCell.GetItem() != null ? B2DragAndDropCell.GetItem().myskillstone_localid : -1;
        int B3 = B3DragAndDropCell.GetItem() != null ? B3DragAndDropCell.GetItem().myskillstone_localid : -1;
        int C1 = C1DragAndDropCell.GetItem() != null ? C1DragAndDropCell.GetItem().myskillstone_localid : -1;
        int C2 = C2DragAndDropCell.GetItem() != null ? C2DragAndDropCell.GetItem().myskillstone_localid : -1;
        int C3 = C3DragAndDropCell.GetItem() != null ? C3DragAndDropCell.GetItem().myskillstone_localid : -1;

        if (A1 != -1)
            IDs.Add(A1);
        if (A2 != -1)
            IDs.Add(A2);
        if (A3 != -1)
            IDs.Add(A3);
        if (B1 != -1)
            IDs.Add(B1);
        if (B2 != -1)
            IDs.Add(B2);
        if (B3 != -1)
            IDs.Add(B3);
        if (C1 != -1)
            IDs.Add(C1);
        if (C2 != -1)
            IDs.Add(C2);
        if (C3 != -1)
            IDs.Add(C3);

        return IDs;
    }
    
    public List<int> getCurrentNineSlotAllSkillIds()//注意这个函数和上面的意义完全不一样，这个返回的长度固定为9    
    {
        List<int> NineSkillIDs = new List<int>();

        int A1 = A1DragAndDropCell.GetItem() != null ? A1DragAndDropCell.GetItem()._SkillConfigOfSkillStone.id : -1;
        int A2 = A2DragAndDropCell.GetItem() != null ? A2DragAndDropCell.GetItem()._SkillConfigOfSkillStone.id : -1;
        int A3 = A3DragAndDropCell.GetItem() != null ? A3DragAndDropCell.GetItem()._SkillConfigOfSkillStone.id : -1;
        int B1 = B1DragAndDropCell.GetItem() != null ? B1DragAndDropCell.GetItem()._SkillConfigOfSkillStone.id : -1;
        int B2 = B2DragAndDropCell.GetItem() != null ? B2DragAndDropCell.GetItem()._SkillConfigOfSkillStone.id : -1;
        int B3 = B3DragAndDropCell.GetItem() != null ? B3DragAndDropCell.GetItem()._SkillConfigOfSkillStone.id : -1;
        int C1 = C1DragAndDropCell.GetItem() != null ? C1DragAndDropCell.GetItem()._SkillConfigOfSkillStone.id : -1;
        int C2 = C2DragAndDropCell.GetItem() != null ? C2DragAndDropCell.GetItem()._SkillConfigOfSkillStone.id : -1;
        int C3 = C3DragAndDropCell.GetItem() != null ? C3DragAndDropCell.GetItem()._SkillConfigOfSkillStone.id : -1;

        NineSkillIDs.Add(A1);
        NineSkillIDs.Add(A2);
        NineSkillIDs.Add(A3);
        NineSkillIDs.Add(B1);
        NineSkillIDs.Add(B2);
        NineSkillIDs.Add(B3);
        NineSkillIDs.Add(C1);
        NineSkillIDs.Add(C2);
        NineSkillIDs.Add(C3);

        return NineSkillIDs;
    }

    private GameObject SkillStonePrefab;

    public DragAndDropItem generateOneDragAndDropItem()
    {
        GameObject SkillStone = GameObject.Instantiate(SkillStonePrefab);
        SkillStone.SetActive(true);
        return SkillStone.GetComponent<DragAndDropItem>();
    }

    public IEnumerator startUp()
    {
        SkillStonePrefab = new GameObject("SkillStone");
        SkillStonePrefab.AddComponent<DragAndDropItem>();
        SkillStonePrefab.AddComponent<Image>();
        NineSlotT.gameObject.SetActive(false);
        getNineSlotReady();
        yield break;
    }

    private void getNineSlotReady()
    {
        _SlotColorManger = new SlotColorManger();
        _SlotColorManger.ready();

        A1Slot = new SkillStoneSlot(null, A1DragAndDropCell, this, _skillStonesBox);
        A2Slot = new SkillStoneSlot(null, A2DragAndDropCell, this, _skillStonesBox);
        A3Slot = new SkillStoneSlot(null, A3DragAndDropCell, this, _skillStonesBox);
        B1Slot = new SkillStoneSlot(null, B1DragAndDropCell, this, _skillStonesBox);
        B2Slot = new SkillStoneSlot(null, B2DragAndDropCell, this, _skillStonesBox);
        B3Slot = new SkillStoneSlot(null, B3DragAndDropCell, this, _skillStonesBox);
        C1Slot = new SkillStoneSlot(null, C1DragAndDropCell, this, _skillStonesBox);
        C2Slot = new SkillStoneSlot(null, C2DragAndDropCell, this, _skillStonesBox);
        C3Slot = new SkillStoneSlot(null, C3DragAndDropCell, this, _skillStonesBox);

        allSlot.Clear();
        allSlot.Add(A1Slot);
        allSlot.Add(A2Slot);
        allSlot.Add(A3Slot);
        allSlot.Add(B1Slot);
        allSlot.Add(B2Slot);
        allSlot.Add(B3Slot);
        allSlot.Add(C1Slot);
        allSlot.Add(C2Slot);
        allSlot.Add(C3Slot);

        A1DragAndDropCell.isDeleteArea = false;
        A2DragAndDropCell.isDeleteArea = false;
        A3DragAndDropCell.isDeleteArea = false;
        B1DragAndDropCell.isDeleteArea = false;
        B2DragAndDropCell.isDeleteArea = false;
        B3DragAndDropCell.isDeleteArea = false;
        C1DragAndDropCell.isDeleteArea = false;
        C2DragAndDropCell.isDeleteArea = false;
        C3DragAndDropCell.isDeleteArea = false;

        foreach (SkillStoneSlot _slot in allSlot)
        {
            SlotButtonBeheviour(_slot);
        }

        UnityEngine.Events.UnityAction AddStoneMode;
        AddStoneButton.onClick.RemoveAllListeners();
        AddStoneMode = () => {
            _skillStonesBox.BoxWholeT.gameObject.SetActive(true);
            readANineAndTwo(this.editingNineAndTwo,SkillEditMode.AddStoneMode);
            List<int> nineAndTwoUsingIDs = getUsingStonesId();
            _skillStonesBox.arrangeSkillStonesToBox(_skillStonesBox.getFocusingType(), _skillStonesBox.getFocusingExType(),
                                                    _skillStonesBox.closeCheckBox.isOn,
                                                    _skillStonesBox.nearCheckBox.isOn,
                                                    _skillStonesBox.farCheckBox.isOn,
                                                    _skillStonesBox.outRangeCheckBox.isOn,
                                                    nineAndTwoUsingIDs);
        };
        AddStoneButton.onClick.AddListener(AddStoneMode);

        UnityEngine.Events.UnityAction EditStoneMode;
        EditStoneButton.onClick.RemoveAllListeners();
        EditStoneMode = () => {
            _skillStonesBox.arrangeSkillStonesToBox(_skillStonesBox.getFocusingType(), _skillStonesBox.getFocusingExType(),
                                                    _skillStonesBox.closeCheckBox.isOn,
                                                    _skillStonesBox.nearCheckBox.isOn,
                                                    _skillStonesBox.farCheckBox.isOn,
                                                    _skillStonesBox.outRangeCheckBox.isOn,
                                                    null);//其实这里不需要nineAndTwoUsingIDs
            _skillStonesBox.BoxWholeT.gameObject.SetActive(false);
            readANineAndTwo(this.editingNineAndTwo, SkillEditMode.EditSkillMode);
        };
        EditStoneButton.onClick.AddListener(EditStoneMode);

        //一上来应该是addstone模式。
        readANineAndTwo(this.editingNineAndTwo, SkillEditMode.AddStoneMode);
    }

    public void readANineAndTwo(AIStateRunner _runner, NineAndTwo toRead, SkillEditMode skillEditMode)
    {
        this._focusingAIStateRunner = _runner;
        this.readANineAndTwo(toRead, skillEditMode);
    }

    public void readANineAndTwo(NineAndTwo toRead,SkillEditMode skillEditMode)
    {
        _SlotColorManger.ready();

        foreach (SkillStoneSlot _slot in allSlot)
        {
            _slot.skillEditMode = skillEditMode;
        }

        if (toRead == null)
            return;
        this.editingNineAndTwo = toRead.DeepCopy();
        this.editingNineAndTwo.sortNineAndTwo();

        wholePoint.text = MySkillStonesReader.skillsetValidation(
            editingNineAndTwo.A1skillid,editingNineAndTwo.A2skillid,editingNineAndTwo.A3skillid,
            editingNineAndTwo.B1skillid,editingNineAndTwo.B2skillid,editingNineAndTwo.B3skillid,
            editingNineAndTwo.C1skillid,editingNineAndTwo.C2skillid,editingNineAndTwo.C3skillid
        ).ToString();

        A1Slot.originSkillConfig = editingNineAndTwo.getA1Config();
        A2Slot.originSkillConfig = editingNineAndTwo.getA2Config();
        A3Slot.originSkillConfig = editingNineAndTwo.getA3Config();
        B1Slot.originSkillConfig = editingNineAndTwo.getB1Config();
        B2Slot.originSkillConfig = editingNineAndTwo.getB2Config();
        B3Slot.originSkillConfig = editingNineAndTwo.getB3Config();
        C1Slot.originSkillConfig = editingNineAndTwo.getC1Config();
        C2Slot.originSkillConfig = editingNineAndTwo.getC2Config();
        C3Slot.originSkillConfig = editingNineAndTwo.getC3Config();
        
        if (skillEditMode == SkillEditMode.EditSkillMode)
        {
            _SlotColorManger.ready();

            foreach (SkillStoneSlot _slot in allSlot)
            {
                Color _colo = Color.white;
                if (_slot._DragAndDropCell.GetItem() != null)
                {
                    if (!_SlotColorManger.tempStoneColorDIC.ContainsKey(_slot._DragAndDropCell.GetItem().GetInstanceID()))
                    {
                        _colo = _SlotColorManger.DistributeColorRandomlyToAStone(_slot._DragAndDropCell.GetItem().GetInstanceID());
                    }
                }
                _slot.showOrigin(_colo);
                _slot._DragAndDropCell.cellType = DragAndDropCell.CellType.Swap;
            }
        }
        if (skillEditMode == SkillEditMode.AddStoneMode)
        {
            foreach (SkillStoneSlot _slot in allSlot)
            {
                _slot.showOrigin();
                _slot._DragAndDropCell.cellType = DragAndDropCell.CellType.DropOnly;
            }
        }
    }

    public IEnumerator UpdateEditingNineAndTwoBaseOnSlots(CharacterDataInfo _CharacterDataInfo)
    {
        List<DragAndDropItem> toExaustMyStones = new List<DragAndDropItem>();
        foreach (SkillStoneSlot _slot in allSlot)
        {
            _slot._DragAndDropCell.UpdateMyItem();
            if (_slot._DragAndDropCell.GetItem() != null)
            {
                if (_slot._DragAndDropCell.GetItem().myskillstone_localid >= 0)
                    toExaustMyStones.Add(_slot._DragAndDropCell.GetItem());
            }
        }

        editingNineAndTwo.A1skillid = A1Slot._DragAndDropCell.GetItem() != null ? A1Slot._DragAndDropCell.GetItem()._SkillConfigOfSkillStone.id : -1;
        editingNineAndTwo.A2skillid = A2Slot._DragAndDropCell.GetItem() != null ? A2Slot._DragAndDropCell.GetItem()._SkillConfigOfSkillStone.id : -1;
        editingNineAndTwo.A3skillid = A3Slot._DragAndDropCell.GetItem() != null ? A3Slot._DragAndDropCell.GetItem()._SkillConfigOfSkillStone.id : -1;
        editingNineAndTwo.B1skillid = B1Slot._DragAndDropCell.GetItem() != null ? B1Slot._DragAndDropCell.GetItem()._SkillConfigOfSkillStone.id : -1;
        editingNineAndTwo.B2skillid = B2Slot._DragAndDropCell.GetItem() != null ? B2Slot._DragAndDropCell.GetItem()._SkillConfigOfSkillStone.id : -1;
        editingNineAndTwo.B3skillid = B3Slot._DragAndDropCell.GetItem() != null ? B3Slot._DragAndDropCell.GetItem()._SkillConfigOfSkillStone.id : -1;
        editingNineAndTwo.C1skillid = C1Slot._DragAndDropCell.GetItem() != null ? C1Slot._DragAndDropCell.GetItem()._SkillConfigOfSkillStone.id : -1;
        editingNineAndTwo.C2skillid = C2Slot._DragAndDropCell.GetItem() != null ? C2Slot._DragAndDropCell.GetItem()._SkillConfigOfSkillStone.id : -1;
        editingNineAndTwo.C3skillid = C3Slot._DragAndDropCell.GetItem() != null ? C3Slot._DragAndDropCell.GetItem()._SkillConfigOfSkillStone.id : -1;

        //这里先不进行保存，进行validation
        int wholepoint = MySkillStonesReader.skillsetValidation(editingNineAndTwo.A1skillid,editingNineAndTwo.A2skillid,editingNineAndTwo.A3skillid,
                                                                editingNineAndTwo.B1skillid,editingNineAndTwo.B2skillid,editingNineAndTwo.B3skillid,
                                                                editingNineAndTwo.C1skillid,editingNineAndTwo.C2skillid,editingNineAndTwo.C3skillid);
        if (wholepoint < 0)
        {
            Debug.Log("因技能总点数为负而不予更新");
            yield break;
        }

        editingNineAndTwo.sortNineAndTwo();
        _CharacterDataInfo._NineAndTwo = editingNineAndTwo;
        AccountCharsSet.updateMyCharInfo(_CharacterDataInfo.localID, _CharacterDataInfo);
        yield return AccountCharsSet.Instance.overrideMyCharsInfo();
        // 缺返回判断

        foreach (SkillStoneSlot _slot in allSlot)
        {
            _slot._DragAndDropCell.RemoveItem();
        }
        _skillStonesBox.deleteTheseStonesLocal(toExaustMyStones);//从本地删除这些石头，针对“我的石头”，与下面单纯removeitem的操作有区别

        if (_CharacterDataInfo._NineAndTwo != null)
            readANineAndTwo(_CharacterDataInfo._NineAndTwo, SkillEditMode.AddStoneMode);
        yield break;
    }

    public void SeliWholeNineAndTwo()//这个才算是9宫格子真正版本的刷新？另外那个refresh只是说一上来的
    {
        List<int> InNineTwoNewStoneIDs = new List<int>();
        foreach (SkillStoneSlot _slot in allSlot)
        {
            if (_slot.skillEditMode == SkillEditMode.AddStoneMode)
            {
                if (_slot._TheNineSlot != null && _slot._SkillStonesBox != null)
                {
                    _slot._DragAndDropCell.UpdateMyItem();
                    if (_slot._DragAndDropCell.GetItem() != null)
                    {
                        if (_slot._DragAndDropCell.GetItem().myskillstone_localid != -1)
                        {
                            _slot._DragAndDropCell.GetItem().inBox = false;
                            InNineTwoNewStoneIDs.Add(_slot._DragAndDropCell.GetItem().myskillstone_localid);
                            if (!_SlotColorManger.tempStoneColorDIC.ContainsKey(_slot._DragAndDropCell.GetItem().myskillstone_localid))
                            {
                                _SlotColorManger.DistributeColorRandomlyToAStone(_slot._DragAndDropCell.GetItem().myskillstone_localid);
                                _slot._DragAndDropCell.GetItem().GetComponent<Image>().color =
                                    _SlotColorManger.tempStoneColorDIC[_slot._DragAndDropCell.GetItem().myskillstone_localid];
                            }

                            if (_slot._DragAndDropCell.GetItem()._SkillConfigOfSkillStone.SkillPoint > 0)
                            {
                                _slot._DragAndDropCell.GetComponent<Image>().color = new Color(1, 1, 0, 1);
                            }
                            else
                            {
                                _slot._DragAndDropCell.GetComponent<Image>().color = new Color(1, 0, 1, 1);
                            }
                            _slot._DragAndDropCell.cellType = DragAndDropCell.CellType.Swap;//指的是九宫格内部石头的交换位置，是新拖的石头之间的交换
                        }
                        else{
                            _slot._DragAndDropCell.cellType = DragAndDropCell.CellType.DropOnly;//没有覆盖住的技能石头那么就不能去拖动它。是说你原来的石头在那，add模式你不可以随便换位置。
                            _slot.showOrigin();
                        }
                    }
                    else{
                        _slot._DragAndDropCell.GetComponent<Image>().color = new Color(1, 1, 1, 0.2f);
                        _slot.showOrigin();
                    }
                }
            }
            SlotButtonBeheviour(_slot);
        }

        List<int> toRemove = new List<int>();
        foreach(KeyValuePair<int,Color> _pair in _SlotColorManger.tempStoneColorDIC)
        {
            if (!InNineTwoNewStoneIDs.Contains(_pair.Key))
            {
                _SlotColorManger.ColorsToDistribute.Add(_pair.Value);
                toRemove.Add(_pair.Key);
            }
        }
        foreach(int id in toRemove)
        {
            _SlotColorManger.tempStoneColorDIC.Remove(id);
        }

        wholePoint.text = MySkillStonesReader.skillsetValidation(
            editingNineAndTwo.A1skillid,editingNineAndTwo.A2skillid,editingNineAndTwo.A3skillid,
            editingNineAndTwo.B1skillid,editingNineAndTwo.B2skillid,editingNineAndTwo.B3skillid,
            editingNineAndTwo.C1skillid,editingNineAndTwo.C2skillid,editingNineAndTwo.C3skillid
        ).ToString();
    }

    Data_Center _Data_Center;
    void addShowSkillInfoFeature(Button _button, State_Transition_Set _state_Transition_Set)
    {
        _button.onClick.RemoveAllListeners();
        UnityEngine.Events.UnityAction showSkillInfo = () =>
        {
            foreach (behaviorEnterRange _range in _state_Transition_Set.ai_trigger_ranges)
            {
                switch (_range)
                {
                    case behaviorEnterRange.inner_range:
                        break;
                    case behaviorEnterRange.mid_range:
                        break;
                    case behaviorEnterRange.far_range:
                        break;
                    case behaviorEnterRange.out_of_range:
                        break;
                }
            }

            ////////超级功能////////
            if (this._focusingAIStateRunner != null)
            {
                _Data_Center = this._focusingAIStateRunner.gameObject.GetComponent<Data_Center>();
                //_CameraManager.Assign_Camera(Camera_Mode_Num.WatchOverCamera, new List<Transform>() { _focusingAIStateRunner.gameObject.transform });
                this._focusingAIStateRunner.changeState(_state_Transition_Set.StateKey);
            }
            else
            {
                Debug.Log(" 没能锁定状态机？ ");
            }
        };
        _button.onClick.AddListener(showSkillInfo);
    }

    //public static void refreshSlot(SkillStoneSlot _Cell)
    //{
    //    if (_Cell._DragAndDropCell.GetItem() != null && _Cell._DragAndDropCell.GetItem()._SkillConfigOfSkillStone != null)
    //    {
    //        switch (_Cell._DragAndDropCell.GetItem()._SkillConfigOfSkillStone.SPLevel)
    //        {
    //            case EX.normal:
    //                _Cell._DragAndDropCell.GetItem().gameObject.GetComponent<Image>().sprite = _Cell._SkillStonesBox._normalSkillStoneIcon;
    //                break;
    //            case EX.EX1:
    //                _Cell._DragAndDropCell.GetItem().gameObject.GetComponent<Image>().sprite = _Cell._SkillStonesBox._EX1SkillStoneIcon;
    //                break;
    //            case EX.EX2:
    //                _Cell._DragAndDropCell.GetItem().gameObject.GetComponent<Image>().sprite = _Cell._SkillStonesBox._EX2SkillStoneIcon;
    //                break;
    //            case EX.EX3:
    //                _Cell._DragAndDropCell.GetItem().gameObject.GetComponent<Image>().sprite = _Cell._SkillStonesBox._EX3SkillStoneIcon;
    //                break;
    //        }
    //    }
    //}
}

public enum SkillEditMode:int
{
    AddStoneMode = 1,
    EditSkillMode = 2
}
