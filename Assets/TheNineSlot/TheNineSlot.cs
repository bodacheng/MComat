using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using dataAccess;
using Api.Dto.Model;

//这个模块应该具备能力去读取一个角色的九宫信息。
namespace mainMenu
{
    public class TheNineSlot : MonoBehaviour
    {
        public static TheNineSlot Instance;

        [Space(5)]
        [Header("preparingScene")]
        public preparingScene _preparingScene;

        [Space(5)]
        [Header("进程器")]
        public SingleThreadProcesser mainProcessRunner;

        [Space(5)]
        [Header("几个重要RectTransform")]
        public RectTransform NineSlotT;

        [Space(5)]
        [Header("成员详细")]
        public MemberDetail _MemberDetail;

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
        [Header("技能石详细")]
        public SkillStoneDetail _skillStoneDetail;

        [Space(7)]
        [Header("EXRemain")]
        public List<GameObject> remainCharges;//固定是9个长度

        SkillStoneSlot A1Slot, A2Slot, A3Slot;
        SkillStoneSlot B1Slot, B2Slot, B3Slot;
        SkillStoneSlot C1Slot, C2Slot, C3Slot;
        readonly List<SkillStoneSlot> allSlot = new List<SkillStoneSlot>();

        void Awake()
        {
            Instance = this;
        }

        public void SlotButtonBeheviour(SkillStoneSlot skillStoneSlot)
        {
            Button button = skillStoneSlot._DragAndDropCell.gameObject.GetComponent<Button>();
            if (button != null)
            {
                void buttonFeature()
                {
                    skillStoneSlot._DragAndDropCell.UpdateMyItem();
                    DragAndDropItem _SkillStone = skillStoneSlot._DragAndDropCell.GetItem();
                    if (_SkillStone != null && _SkillStone._SkillConfigOfSkillStone != null)
                    {
                        _skillStoneDetail.keyname.text = _SkillStone._SkillConfigOfSkillStone.REAL_NAME;
                        _skillStoneDetail.Showname.text = _SkillStone._SkillConfigOfSkillStone.ShowName;
                        _skillStoneDetail.type.text = _SkillStone._SkillConfigOfSkillStone.type;
                        _skillStoneDetail.ShowSkillStoneExType(_SkillStone._SkillConfigOfSkillStone.SP_LEVEL);
                        _skillStoneDetail.SwitchUsingMonsterIcon(_SkillStone.skillStoneOfPlayerId);
                        mainProcessRunner.TriggerMainProcess(_SkillsPrintOut.SkillShowRunWithPreparing(_SkillStone._SkillConfigOfSkillStone.REAL_NAME));
                    }
                }
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(buttonFeature);
            }
        }
        
        void ShowNineSlotExSurplus(int wholePoint)
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

        public List<string> GetUsingStonesId()//这个id列表其实是指的盒子为玩家拥有的技能石所赋予的临时id。
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

            string A1 = A1DragAndDropCell.GetItem()?.skillStoneOfPlayerId;
            string A2 = A2DragAndDropCell.GetItem()?.skillStoneOfPlayerId;
            string A3 = A3DragAndDropCell.GetItem()?.skillStoneOfPlayerId;
            string B1 = B1DragAndDropCell.GetItem()?.skillStoneOfPlayerId;
            string B2 = B2DragAndDropCell.GetItem()?.skillStoneOfPlayerId;
            string B3 = B3DragAndDropCell.GetItem()?.skillStoneOfPlayerId;
            string C1 = C1DragAndDropCell.GetItem()?.skillStoneOfPlayerId;
            string C2 = C2DragAndDropCell.GetItem()?.skillStoneOfPlayerId;
            string C3 = C3DragAndDropCell.GetItem()?.skillStoneOfPlayerId;

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

        public List<string> GetCurrentNineSlotAllSkillIds()//注意这个函数和上面的意义完全不一样，这个返回的长度固定为9    
        {
            List<string> NineSkillIDs = new List<string>();
            string A1 = A1DragAndDropCell.GetItem() != null ? A1DragAndDropCell.GetItem()._SkillConfigOfSkillStone.RECORD_ID : "-1";
            string A2 = A2DragAndDropCell.GetItem() != null ? A2DragAndDropCell.GetItem()._SkillConfigOfSkillStone.RECORD_ID : "-1";
            string A3 = A3DragAndDropCell.GetItem() != null ? A3DragAndDropCell.GetItem()._SkillConfigOfSkillStone.RECORD_ID : "-1";
            string B1 = B1DragAndDropCell.GetItem() != null ? B1DragAndDropCell.GetItem()._SkillConfigOfSkillStone.RECORD_ID : "-1";
            string B2 = B2DragAndDropCell.GetItem() != null ? B2DragAndDropCell.GetItem()._SkillConfigOfSkillStone.RECORD_ID : "-1";
            string B3 = B3DragAndDropCell.GetItem() != null ? B3DragAndDropCell.GetItem()._SkillConfigOfSkillStone.RECORD_ID : "-1";
            string C1 = C1DragAndDropCell.GetItem() != null ? C1DragAndDropCell.GetItem()._SkillConfigOfSkillStone.RECORD_ID : "-1";
            string C2 = C2DragAndDropCell.GetItem() != null ? C2DragAndDropCell.GetItem()._SkillConfigOfSkillStone.RECORD_ID : "-1";
            string C3 = C3DragAndDropCell.GetItem() != null ? C3DragAndDropCell.GetItem()._SkillConfigOfSkillStone.RECORD_ID : "-1";
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

        GameObject SkillStonePrefab;
        public DragAndDropItem GenerateOneDragAndDropItem()
        {
            GameObject SkillStone = Instantiate(SkillStonePrefab);
            SkillStone.SetActive(true);
            return SkillStone.GetComponent<DragAndDropItem>();
        }

        public IEnumerator StartUp()
        {
            SkillStonePrefab = new GameObject("SkillStone");
            SkillStonePrefab.AddComponent<DragAndDropItem>();
            SkillStonePrefab.AddComponent<Image>();
            NineSlotT.gameObject.SetActive(false);
            yield return GetNineSlotReady();
            yield break;
        }

        IEnumerator GetNineSlotReady()
        {
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
            yield return ReadANineAndTwo(null);
        }

        public IEnumerator ReadANineAndTwo(GetMonsterOfPlayerDetailModel _AccountCharacterInfo)
        {
            foreach (SkillStoneSlot _slot in allSlot)
            {
                _slot.OnSlotStonelocalID = null;
                _slot.RemoveStoneFromSlot();
                _slot._DragAndDropCell.cellPhase = DragAndDropCell.CellPhase.NineSlotCell_empty;
            }
            if (_AccountCharacterInfo == null)
            {
                yield break;
            }
            CharacterDataInfo characterDataInfo = RemoteAccess.GetCharacterDataInfo(_AccountCharacterInfo);
            if (characterDataInfo == null)
            {
                Debug.Log("获取CharacterDataInfo信息错误：monsterlocalid" + _AccountCharacterInfo.monsterOfPlayerId);
                yield break;
            }
            NineAndTwo readingNineAndTwo = characterDataInfo._NineAndTwo;
            int wholePoint = MySkillStonesReader.SkillSetValidation(
                readingNineAndTwo.A1skillid, readingNineAndTwo.A2skillid, readingNineAndTwo.A3skillid,
                readingNineAndTwo.B1skillid, readingNineAndTwo.B2skillid, readingNineAndTwo.B3skillid,
                readingNineAndTwo.C1skillid, readingNineAndTwo.C2skillid, readingNineAndTwo.C3skillid
            );
            ShowNineSlotExSurplus(wholePoint);

            List<SkillStoneOfPlayerInfoModel> equipingstones = MySkillStonesReader.Instance.GetMonsterEquipingStones(_AccountCharacterInfo.monsterOfPlayerId);
            for (int i = 0; i < equipingstones.Count; i++)
            {
                switch(equipingstones[i].inUsingSkillSlot)
                {
                    case "1":
                    A1Slot.OnSlotStonelocalID = equipingstones[i].skillStoneOfPlayerId;
                    break;
                    case "2":
                    A2Slot.OnSlotStonelocalID = equipingstones[i].skillStoneOfPlayerId;
                    break;
                    case "3":
                    A3Slot.OnSlotStonelocalID = equipingstones[i].skillStoneOfPlayerId;
                    break;
                    case "4":
                    B1Slot.OnSlotStonelocalID = equipingstones[i].skillStoneOfPlayerId;
                    break;
                    case "5":
                    B2Slot.OnSlotStonelocalID = equipingstones[i].skillStoneOfPlayerId;
                    break;
                    case "6":
                    B3Slot.OnSlotStonelocalID = equipingstones[i].skillStoneOfPlayerId;
                    break;
                    case "7":
                    C1Slot.OnSlotStonelocalID = equipingstones[i].skillStoneOfPlayerId;
                    break;
                    case "8":
                    C2Slot.OnSlotStonelocalID = equipingstones[i].skillStoneOfPlayerId;
                    break;
                    case "9":
                    C3Slot.OnSlotStonelocalID = equipingstones[i].skillStoneOfPlayerId;
                    break;
                }
            }

            foreach (SkillStoneSlot _slot in allSlot)
            {
                yield return _slot.ShowOrigin(Color.white);
                _slot._DragAndDropCell.cellPhase = _slot._DragAndDropCell.GetItem() != null? DragAndDropCell.CellPhase.NineSlotCell_full : DragAndDropCell.CellPhase.NineSlotCell_empty;
            }
        }
        
        int SlotNum(SkillStoneSlot stoneSlot)
        {
            if (stoneSlot == A1Slot)
            return 1;
            if (stoneSlot == A2Slot)
            return 2;
            if (stoneSlot == A3Slot)
            return 3;
            if (stoneSlot == B1Slot)
            return 4;
            if (stoneSlot == B2Slot)
            return 5;
            if (stoneSlot == B3Slot)
            return 6;
            if (stoneSlot == C1Slot)
            return 7;
            if (stoneSlot == C2Slot)
            return 8;
            if (stoneSlot == C3Slot)
            return 9;
            return -1;
        }

        // 这个函数应该能够被用于Tutorial模式下亚当的技能编辑。
        public IEnumerator UpdateMyStonesBaseOnSlots(GetMonsterOfPlayerDetailModel accountCharacterInfo)
        {
            List<string> usingStones = GetUsingStonesId();
            for (int i = 0; i < allSlot.Count; i++)
            {
                if (allSlot[i]._DragAndDropCell.GetItem() != null)
                {
                    if (allSlot[i].OnSlotStonelocalID != allSlot[i]._DragAndDropCell.GetItem().skillStoneOfPlayerId) 
                    {
                        SkillStoneOfPlayerInfoModel old_skillStoneOfPlayerInfoModel = MySkillStonesReader.Instance.GetSkillStoneOfPlayerInfoModelByMyStoneId(allSlot[i].OnSlotStonelocalID);
                        if (old_skillStoneOfPlayerInfoModel != null)
                        {
                            if (!usingStones.Contains(old_skillStoneOfPlayerInfoModel.skillStoneOfPlayerId))// 代表卸载的技能石头
                            {
                                Debug.Log("技能石头："+ old_skillStoneOfPlayerInfoModel.skillStoneOfPlayerId + "被卸下");
                                old_skillStoneOfPlayerInfoModel.inUsingMonsterOfPlayerId = null;
                                old_skillStoneOfPlayerInfoModel.inUsingSkillSlot = null;
                                yield return MySkillStonesReader.Instance.UpdateMySkillStone(old_skillStoneOfPlayerInfoModel);
                            }else{
                                // 说明这个位置上原先的技能石现在在九宫格的其他位置上，轮到所在slot的处理时自然会更新那个技能石的信息。
                            }
                        }
                        SkillStoneOfPlayerInfoModel new_skillStoneOfPlayerInfoModel = MySkillStonesReader.Instance.GetSkillStoneOfPlayerInfoModelByMyStoneId(allSlot[i]._DragAndDropCell.GetItem().skillStoneOfPlayerId);
                        if (new_skillStoneOfPlayerInfoModel != null)
                        {
                            new_skillStoneOfPlayerInfoModel.inUsingMonsterOfPlayerId = accountCharacterInfo.monsterOfPlayerId;
                            new_skillStoneOfPlayerInfoModel.inUsingSkillSlot = SlotNum(allSlot[i]).ToString();
                            yield return MySkillStonesReader.Instance.UpdateMySkillStone(new_skillStoneOfPlayerInfoModel);
                        }
                    }
                }else{
                    SkillStoneOfPlayerInfoModel old_skillStoneOfPlayerInfoModel = MySkillStonesReader.Instance.GetSkillStoneOfPlayerInfoModelByMyStoneId(allSlot[i].OnSlotStonelocalID);
                    if (old_skillStoneOfPlayerInfoModel != null)// 旧技能石被卸下
                    {
                        if (!usingStones.Contains(old_skillStoneOfPlayerInfoModel.skillStoneOfPlayerId))// 代表卸载的技能石头
                        {
                            Debug.Log("技能石头："+ old_skillStoneOfPlayerInfoModel.skillStoneOfPlayerId + "被卸下");
                            old_skillStoneOfPlayerInfoModel.inUsingMonsterOfPlayerId = null;
                            old_skillStoneOfPlayerInfoModel.inUsingSkillSlot = null;
                            yield return MySkillStonesReader.Instance.UpdateMySkillStone(old_skillStoneOfPlayerInfoModel);
                        }else{
                            // 说明这个位置上原先的技能石现在在九宫格的其他位置上，轮到所在slot的处理时自然会更新那个技能石的信息。
                        }
                    }
                }
            }
            yield return ReadANineAndTwo(accountCharacterInfo);
            yield break;
        }

        public IEnumerator SeliAllNineSlots()//这个的核心作用在于即使调整cell的phase
        {
            foreach (SkillStoneSlot _slot in allSlot)
            {
                _slot._DragAndDropCell.UpdateMyItem();
                _slot._DragAndDropCell.cellPhase = _slot._DragAndDropCell.GetItem() != null ? DragAndDropCell.CellPhase.NineSlotCell_full : DragAndDropCell.CellPhase.NineSlotCell_empty;
                SlotButtonBeheviour(_slot);
            }
            List<string> stonesOnNineSlots = GetCurrentNineSlotAllSkillIds();
            int wholePoint = MySkillStonesReader.SkillSetValidation(
                stonesOnNineSlots[0], stonesOnNineSlots[1], stonesOnNineSlots[2],
                stonesOnNineSlots[3], stonesOnNineSlots[4], stonesOnNineSlots[5],
                stonesOnNineSlots[6], stonesOnNineSlots[7], stonesOnNineSlots[8]);
            Debug.Log("积分："+ wholePoint);
            ShowNineSlotExSurplus(wholePoint);
            yield break;
        }
    }
}