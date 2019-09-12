using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using dataAccess;
using Api.Dto.Model;

//这个模块应该具备能力去读取一个角色的九宫信息。
namespace mainMenu
{
    public class TheNineSlot : MonoBehaviour
    {
        public static TheNineSlot _TheNineSlot;

        [Space(5)]
        [Header("preparingScene")]
        public preparingScene _preparingScene;

        [Space(5)]
        [Header("进程器")]
        public SingleThreadProcesser mainProcessRunner;

        [Space(5)]
        [Header("几个重要RectTransform")]
        public RectTransform NineAndTwoCanvas;
        public RectTransform NineSlotT;

        [Space(5)]
        [Header("成员详细")]
        public MemberDetail _MemberDetail;

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
        [Header("技能石编辑确认")]
        public Button ConfirmSkillChangeButton;

        [Space(7)]
        [Header("LoadingProcess")]
        public LoadingCanvas _LoadingCanvas;

        [Space(7)]
        [Header("技能石详细")]
        public skillStoneDetail _skillStoneDetail;

        [Space(7)]
        [Header("EXRemain")]
        public List<GameObject> remainCharges;//固定是9个长度

        private SkillStoneSlot A1Slot, A2Slot, A3Slot;
        private SkillStoneSlot B1Slot, B2Slot, B3Slot;
        private SkillStoneSlot C1Slot, C2Slot, C3Slot;
        private List<SkillStoneSlot> allSlot = new List<SkillStoneSlot>();

        public void SlotButtonBeheviour(SkillStoneSlot skillStoneSlot)
        {
            Button button = skillStoneSlot._DragAndDropCell.gameObject.GetComponent<Button>();
            if (button != null)
            {
                UnityEngine.Events.UnityAction buttonFeature = () =>
                {
                    skillStoneSlot._DragAndDropCell.UpdateMyItem();
                    DragAndDropItem _stoneOnCell = skillStoneSlot._DragAndDropCell.GetItem();
                    if (_stoneOnCell != null && _stoneOnCell._SkillConfigOfSkillStone != null)
                    {
                        _skillStoneDetail.keyname.text = _stoneOnCell._SkillConfigOfSkillStone.keyName;
                        _skillStoneDetail.Showname.text = _stoneOnCell._SkillConfigOfSkillStone.ShowName;
                        _skillStoneDetail.type.text = _stoneOnCell._SkillConfigOfSkillStone.type;
                        _skillStoneDetail.showSkillStoneExType(_stoneOnCell._SkillConfigOfSkillStone.SPLevel);
                        mainProcessRunner.triggerMainProcess(_SkillsPrintOut.skillShowRunWithPreparing(_stoneOnCell._SkillConfigOfSkillStone.keyName));
                    }
                };
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(buttonFeature);
            }
        }
        
        void showNineSlotExSurplus(int wholePoint)
        {
            int pointremain = wholePoint / 10;
            for (int i = 0; i < remainCharges.Count; i++)
            {
                if (i + 1 <= pointremain)
                {
                    remainCharges[i].SetActive(true);
                }else
                    remainCharges[i].SetActive(false);
            }
        }

        public List<string> getUsingStonesId()//这个id列表其实是指的盒子为玩家拥有的技能石所赋予的临时id。
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

            List<string> IDs = new List<string>();

            string A1 = A1DragAndDropCell.GetItem() != null ? A1DragAndDropCell.GetItem().localID : null;
            string A2 = A2DragAndDropCell.GetItem() != null ? A2DragAndDropCell.GetItem().localID : null;
            string A3 = A3DragAndDropCell.GetItem() != null ? A3DragAndDropCell.GetItem().localID : null;
            string B1 = B1DragAndDropCell.GetItem() != null ? B1DragAndDropCell.GetItem().localID : null;
            string B2 = B2DragAndDropCell.GetItem() != null ? B2DragAndDropCell.GetItem().localID : null;
            string B3 = B3DragAndDropCell.GetItem() != null ? B3DragAndDropCell.GetItem().localID : null;
            string C1 = C1DragAndDropCell.GetItem() != null ? C1DragAndDropCell.GetItem().localID : null;
            string C2 = C2DragAndDropCell.GetItem() != null ? C2DragAndDropCell.GetItem().localID : null;
            string C3 = C3DragAndDropCell.GetItem() != null ? C3DragAndDropCell.GetItem().localID : null;

            if (A1 != null)
                IDs.Add(A1);
            if (A2 != null)
                IDs.Add(A2);
            if (A3 != null)
                IDs.Add(A3);
            if (B1 != null)
                IDs.Add(B1);
            if (B2 != null)
                IDs.Add(B2);
            if (B3 != null)
                IDs.Add(B3);
            if (C1 != null)
                IDs.Add(C1);
            if (C2 != null)
                IDs.Add(C2);
            if (C3 != null)
                IDs.Add(C3);
            return IDs;
        }

        public List<string> getCurrentNineSlotAllSkillIds()//注意这个函数和上面的意义完全不一样，这个返回的长度固定为9    
        {
            List<string> NineSkillIDs = new List<string>();

            string A1 = A1DragAndDropCell.GetItem() != null ? A1DragAndDropCell.GetItem()._SkillConfigOfSkillStone.id : "-1";
            string A2 = A2DragAndDropCell.GetItem() != null ? A2DragAndDropCell.GetItem()._SkillConfigOfSkillStone.id : "-1";
            string A3 = A3DragAndDropCell.GetItem() != null ? A3DragAndDropCell.GetItem()._SkillConfigOfSkillStone.id : "-1";
            string B1 = B1DragAndDropCell.GetItem() != null ? B1DragAndDropCell.GetItem()._SkillConfigOfSkillStone.id : "-1";
            string B2 = B2DragAndDropCell.GetItem() != null ? B2DragAndDropCell.GetItem()._SkillConfigOfSkillStone.id : "-1";
            string B3 = B3DragAndDropCell.GetItem() != null ? B3DragAndDropCell.GetItem()._SkillConfigOfSkillStone.id : "-1";
            string C1 = C1DragAndDropCell.GetItem() != null ? C1DragAndDropCell.GetItem()._SkillConfigOfSkillStone.id : "-1";
            string C2 = C2DragAndDropCell.GetItem() != null ? C2DragAndDropCell.GetItem()._SkillConfigOfSkillStone.id : "-1";
            string C3 = C3DragAndDropCell.GetItem() != null ? C3DragAndDropCell.GetItem()._SkillConfigOfSkillStone.id : "-1";

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
            _TheNineSlot = this;
            SkillStonePrefab = new GameObject("SkillStone");
            SkillStonePrefab.AddComponent<DragAndDropItem>();
            SkillStonePrefab.AddComponent<Image>();
            NineSlotT.gameObject.SetActive(false);
            yield return getNineSlotReady();
            yield break;
        }

        private IEnumerator getNineSlotReady()
        {
            MySkillStonesReader.SkillStonesBox = _skillStonesBox;
            _TheNineSlot = this;

            A1Slot = new SkillStoneSlot(null, A1DragAndDropCell);
            A2Slot = new SkillStoneSlot(null, A2DragAndDropCell);
            A3Slot = new SkillStoneSlot(null, A3DragAndDropCell);
            B1Slot = new SkillStoneSlot(null, B1DragAndDropCell);
            B2Slot = new SkillStoneSlot(null, B2DragAndDropCell);
            B3Slot = new SkillStoneSlot(null, B3DragAndDropCell);
            C1Slot = new SkillStoneSlot(null, C1DragAndDropCell);
            C2Slot = new SkillStoneSlot(null, C2DragAndDropCell);
            C3Slot = new SkillStoneSlot(null, C3DragAndDropCell);

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

            foreach (SkillStoneSlot _slot in allSlot)
            {
                SlotButtonBeheviour(_slot);
            }
            yield return readANineAndTwo(null);
        }

        public IEnumerator readANineAndTwo(GetMonsterOfPlayerDetailModel _AccountCharacterInfo)
        {
            if (_AccountCharacterInfo == null)
            {
                Debug.Log("九宫格读取时机严重错误。");
                foreach (SkillStoneSlot _slot in allSlot)
                {
                    _slot.OnSlotStonelocalID = null;
                    _slot.removeStoneFromSlot();
                    _slot._DragAndDropCell.cellPhase = DragAndDropCell.CellPhase.NineSlotCell_empty;
                }
                yield break;
            }
            CharacterDataInfo characterDataInfo = RemoteAccess.getCharacterDataInfo(_AccountCharacterInfo);
            if (characterDataInfo == null)
            {
                Debug.Log("获取CharacterDataInfo信息错误：monsterlocalid" + _AccountCharacterInfo.monsterOfPlayerId);
                yield break;
            }
            NineAndTwo readingNineAndTwo = characterDataInfo._NineAndTwo;
            int wholePoint = MySkillStonesReader.skillsetValidation(
                readingNineAndTwo.A1skillid, readingNineAndTwo.A2skillid, readingNineAndTwo.A3skillid,
                readingNineAndTwo.B1skillid, readingNineAndTwo.B2skillid, readingNineAndTwo.B3skillid,
                readingNineAndTwo.C1skillid, readingNineAndTwo.C2skillid, readingNineAndTwo.C3skillid);
            showNineSlotExSurplus(wholePoint);

            A1Slot.OnSlotStonelocalID = _AccountCharacterInfo.a1_skill_stone_record_id;
            A2Slot.OnSlotStonelocalID = _AccountCharacterInfo.a2_skill_stone_record_id;
            A3Slot.OnSlotStonelocalID = _AccountCharacterInfo.a3_skill_stone_record_id;
            B1Slot.OnSlotStonelocalID = _AccountCharacterInfo.b1_skill_stone_record_id;
            B2Slot.OnSlotStonelocalID = _AccountCharacterInfo.b2_skill_stone_record_id;
            B3Slot.OnSlotStonelocalID = _AccountCharacterInfo.b3_skill_stone_record_id;
            C1Slot.OnSlotStonelocalID = _AccountCharacterInfo.c1_skill_stone_record_id;
            C2Slot.OnSlotStonelocalID = _AccountCharacterInfo.c2_skill_stone_record_id;
            C3Slot.OnSlotStonelocalID = _AccountCharacterInfo.c3_skill_stone_record_id;

            foreach (SkillStoneSlot _slot in allSlot)
            {
                yield return _slot.showOrigin(Color.white);
                if (_slot._DragAndDropCell.GetItem() != null)
                    _slot._DragAndDropCell.cellPhase = DragAndDropCell.CellPhase.NineSlotCell_full;
                else
                    _slot._DragAndDropCell.cellPhase = DragAndDropCell.CellPhase.NineSlotCell_empty;
            }
        }

        // 这个函数应该能够被用于Tutorial模式下亚当的技能编辑。
        public IEnumerator UpdateEditingNineAndTwoBaseOnSlots(GetMonsterOfPlayerDetailModel accountCharacterInfo)
        {
            foreach (SkillStoneSlot _slot in allSlot)
            {
                _slot._DragAndDropCell.UpdateMyItem();
            }
            accountCharacterInfo.a1_skill_stone_record_id = (A1Slot._DragAndDropCell.GetItem() != null ? A1Slot._DragAndDropCell.GetItem().localID : null);
            accountCharacterInfo.a2_skill_stone_record_id = (A2Slot._DragAndDropCell.GetItem() != null ? A2Slot._DragAndDropCell.GetItem().localID : null);
            accountCharacterInfo.a3_skill_stone_record_id = (A3Slot._DragAndDropCell.GetItem() != null ? A3Slot._DragAndDropCell.GetItem().localID : null);
            accountCharacterInfo.b1_skill_stone_record_id = (B1Slot._DragAndDropCell.GetItem() != null ? B1Slot._DragAndDropCell.GetItem().localID : null);
            accountCharacterInfo.b2_skill_stone_record_id = (B2Slot._DragAndDropCell.GetItem() != null ? B2Slot._DragAndDropCell.GetItem().localID : null);
            accountCharacterInfo.b3_skill_stone_record_id = (B3Slot._DragAndDropCell.GetItem() != null ? B3Slot._DragAndDropCell.GetItem().localID : null);
            accountCharacterInfo.c1_skill_stone_record_id = (C1Slot._DragAndDropCell.GetItem() != null ? C1Slot._DragAndDropCell.GetItem().localID : null);
            accountCharacterInfo.c2_skill_stone_record_id = (C2Slot._DragAndDropCell.GetItem() != null ? C2Slot._DragAndDropCell.GetItem().localID : null);
            accountCharacterInfo.c3_skill_stone_record_id = (C3Slot._DragAndDropCell.GetItem() != null ? C3Slot._DragAndDropCell.GetItem().localID : null);

            CharacterDataInfo _dCharacterDataInfo = RemoteAccess.getCharacterDataInfo(accountCharacterInfo);
            NineAndTwo nineAndTwo = _dCharacterDataInfo._NineAndTwo;

            //这里先不进行保存，进行validation
            int wholepoint = MySkillStonesReader.skillsetValidation(nineAndTwo.A1skillid, nineAndTwo.A2skillid, nineAndTwo.A3skillid,
                                                                    nineAndTwo.B1skillid, nineAndTwo.B2skillid, nineAndTwo.B3skillid,
                                                                    nineAndTwo.C1skillid, nineAndTwo.C2skillid, nineAndTwo.C3skillid);
            if (wholepoint < 0)
            {
                Debug.Log("因技能总点数为负而不予更新");
                yield break;
            }

            yield return AccountCharsSet.instance.updateMyCharInfo(accountCharacterInfo);// 缺返回判断
            yield return readANineAndTwo(accountCharacterInfo);
            yield break;
        }

        public IEnumerator SeliWholeNineAndTwo()//这个的核心作用在于即使调整cell的phase
        {
            foreach (SkillStoneSlot _slot in allSlot)
            {
                _slot._DragAndDropCell.UpdateMyItem();
                if (_slot._DragAndDropCell.GetItem() != null)
                {
                    _slot._DragAndDropCell.GetItem().GetComponent<Image>().color = getARandomColor();
                    _slot._DragAndDropCell.cellPhase = DragAndDropCell.CellPhase.NineSlotCell_full;
                }
                else
                {
                    _slot._DragAndDropCell.cellPhase = DragAndDropCell.CellPhase.NineSlotCell_empty;
                }
                SlotButtonBeheviour(_slot);
            }
            
            List<string> stonesOnNineSlots = getCurrentNineSlotAllSkillIds();
            int wholePoint = MySkillStonesReader.skillsetValidation(
                stonesOnNineSlots[0], stonesOnNineSlots[1], stonesOnNineSlots[2],
                stonesOnNineSlots[3], stonesOnNineSlots[4], stonesOnNineSlots[5],
                stonesOnNineSlots[6], stonesOnNineSlots[7], stonesOnNineSlots[8]);
            Debug.Log("积分："+ wholePoint);
            showNineSlotExSurplus(wholePoint);
            yield break;
        }
        
        private Color getARandomColor()
        {
            return new Color(Random.Range(0f,1f),Random.Range(0f,1f),Random.Range(0f,1f));
        }
    }
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