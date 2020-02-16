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

        [Space(5)]
        [Header("选中框")]
        public GameObject Selected;

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
        SkillStoneSlot focusingSlot;
        readonly List<SkillStoneSlot> allSlot = new List<SkillStoneSlot>();

        float last_clickTime;

        void Awake()
        {
            Instance = this;
        }

        public SkillStoneSlot GetFocusingStoneSlot()
        {
            return focusingSlot;
        }

        void SeletedRender(RectTransform T)
        {
            if (T == null)
            {
                Selected.SetActive(false);
                return;
            }
            Selected.SetActive(true);
            Selected.transform.SetParent(T);
            Selected.transform.localPosition = Vector3.zero;
            Selected.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
            Selected.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
            Selected.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
            Selected.gameObject.SetActive(true);
        }

        public void SlotButtonBeheviour(SkillStoneSlot skillStoneSlot)
        {
            Button button = skillStoneSlot._DragAndDropCell.gameObject.GetComponent<Button>();
            if (button != null)
            {
                void buttonFeature()
                {
                    if (Time.time - last_clickTime < 0.25f)
                    {
                        focusingSlot = null;
                        SeletedRender(null);
                    } else {
                        focusingSlot = skillStoneSlot;
                        SeletedRender(focusingSlot._DragAndDropCell.GetComponent<RectTransform>());
                    }
                    last_clickTime = Time.time;
                    skillStoneSlot._DragAndDropCell.UpdateMyItem();
                    DragAndDropItem _SkillStone = skillStoneSlot._DragAndDropCell.GetItem();
                    if (_SkillStone != null && _SkillStone._SkillConfigOfSkillStone != null)
                    {
                        _skillStoneDetail.RefreshSkillDetail(_SkillStone._SkillConfigOfSkillStone, _SkillStone.SkillStoneOfPlayerId);
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
                } else
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

            string A1 = A1DragAndDropCell.GetItem()?.SkillStoneOfPlayerId;
            string A2 = A2DragAndDropCell.GetItem()?.SkillStoneOfPlayerId;
            string A3 = A3DragAndDropCell.GetItem()?.SkillStoneOfPlayerId;
            string B1 = B1DragAndDropCell.GetItem()?.SkillStoneOfPlayerId;
            string B2 = B2DragAndDropCell.GetItem()?.SkillStoneOfPlayerId;
            string B3 = B3DragAndDropCell.GetItem()?.SkillStoneOfPlayerId;
            string C1 = C1DragAndDropCell.GetItem()?.SkillStoneOfPlayerId;
            string C2 = C2DragAndDropCell.GetItem()?.SkillStoneOfPlayerId;
            string C3 = C3DragAndDropCell.GetItem()?.SkillStoneOfPlayerId;

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

        public List<string> GetCurrentNineSlotAllSkillIds()//注意这个函数和上面的意义完全不一样，这个返回的是技能定义ID， 长度固定为9    
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

        public bool RefreshWholePointBasedOnCurrentNineSlots(DragAndDropItem item, DragAndDropCell replacePosition)
        {
            List<string> nineskillids = Instance.GetCurrentNineSlotAllSkillIds();

            if (item == null)
            {
                item = new DragAndDropItem();
                item._SkillConfigOfSkillStone = new SkillConfig();
            }

            if (replacePosition == Instance.A1DragAndDropCell)
            {
                nineskillids[0] = item._SkillConfigOfSkillStone.RECORD_ID;
            }
            if (replacePosition == Instance.A2DragAndDropCell)
            {
                nineskillids[1] = item._SkillConfigOfSkillStone.RECORD_ID;
            }
            if (replacePosition == Instance.A3DragAndDropCell)
            {
                nineskillids[2] = item._SkillConfigOfSkillStone.RECORD_ID;
            }
            if (replacePosition == Instance.B1DragAndDropCell)
            {
                nineskillids[3] = item._SkillConfigOfSkillStone.RECORD_ID;
            }
            if (replacePosition == Instance.B2DragAndDropCell)
            {
                nineskillids[4] = item._SkillConfigOfSkillStone.RECORD_ID;
            }
            if (replacePosition == Instance.B3DragAndDropCell)
            {
                nineskillids[5] = item._SkillConfigOfSkillStone.RECORD_ID;
            }
            if (replacePosition == Instance.C1DragAndDropCell)
            {
                nineskillids[6] = item._SkillConfigOfSkillStone.RECORD_ID;
            }
            if (replacePosition == Instance.C2DragAndDropCell)
            {
                nineskillids[7] = item._SkillConfigOfSkillStone.RECORD_ID;
            }
            if (replacePosition == Instance.C3DragAndDropCell)
            {
                nineskillids[8] = item._SkillConfigOfSkillStone.RECORD_ID;
            }

            int wholepint = MySkillStonesReader.SkillSetValidation(nineskillids[0], nineskillids[1], nineskillids[2], nineskillids[3], nineskillids[4], nineskillids[5], nineskillids[6], nineskillids[7], nineskillids[8]);
            if (wholepint < 0)
            {
                return false;
            }
            return true;
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
            SeletedRender(null);

            A1Slot = new SkillStoneSlot(1,null, A1DragAndDropCell);
            A2Slot = new SkillStoneSlot(2,null, A2DragAndDropCell);
            A3Slot = new SkillStoneSlot(3,null, A3DragAndDropCell);
            B1Slot = new SkillStoneSlot(4,null, B1DragAndDropCell);
            B2Slot = new SkillStoneSlot(5,null, B2DragAndDropCell);
            B3Slot = new SkillStoneSlot(6,null, B3DragAndDropCell);
            C1Slot = new SkillStoneSlot(7,null, C1DragAndDropCell);
            C2Slot = new SkillStoneSlot(8,null, C2DragAndDropCell);
            C3Slot = new SkillStoneSlot(9,null, C3DragAndDropCell);

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
                _slot._DragAndDropCell.cellPhase = DragAndDropCell.CellPhase.NineSlotCell_empty;
                _slot.RemoveStoneFromSlot();
            }
            if (_AccountCharacterInfo == null)
            {
                yield break;
            }
            List<SkillStoneOfPlayerInfoModel> equipingstones = MySkillStonesReader.Instance.GetMonsterEquipingStones(_AccountCharacterInfo.monsterOfPlayerId);
            for (int i = 0; i < equipingstones.Count; i++)
            {
                switch (equipingstones[i].inUsingSkillSlot)
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
            int wholePoint = MySkillStonesReader.SkillSetValidation(
                A1Slot.OnSlotStonelocalID, A2Slot.OnSlotStonelocalID, A3Slot.OnSlotStonelocalID,
                B1Slot.OnSlotStonelocalID, B2Slot.OnSlotStonelocalID, B3Slot.OnSlotStonelocalID,
                C1Slot.OnSlotStonelocalID, C2Slot.OnSlotStonelocalID, C3Slot.OnSlotStonelocalID
            );
            ShowNineSlotExSurplus(wholePoint);
            foreach (SkillStoneSlot _slot in allSlot)
            {
                yield return _slot.ShowOrigin(Color.white);
                _slot._DragAndDropCell.cellPhase = _slot._DragAndDropCell.GetItem() != null ? DragAndDropCell.CellPhase.NineSlotCell_full : DragAndDropCell.CellPhase.NineSlotCell_empty;
            }
        }

        public int GetNineSlotWholePointOfMonster(GetMonsterOfPlayerDetailModel _AccountCharacterInfo)
        {
            List<SkillStoneOfPlayerInfoModel> equipingstones = MySkillStonesReader.Instance.GetMonsterEquipingStones(_AccountCharacterInfo.monsterOfPlayerId);
            string A1=null, A2=null, A3=null, B1=null, B2=null, B3=null, C1=null, C2=null, C3=null;
            for (int i = 0; i < equipingstones.Count; i++)
            {
                switch (equipingstones[i].inUsingSkillSlot)
                {
                    case "1":
                        A1 = equipingstones[i].skillStoneOfPlayerId;
                        break;
                    case "2":
                        A2 = equipingstones[i].skillStoneOfPlayerId;
                        break;
                    case "3":
                        A3 = equipingstones[i].skillStoneOfPlayerId;
                        break;
                    case "4":
                        B1 = equipingstones[i].skillStoneOfPlayerId;
                        break;
                    case "5":
                        B2 = equipingstones[i].skillStoneOfPlayerId;
                        break;
                    case "6":
                        B3 = equipingstones[i].skillStoneOfPlayerId;
                        break;
                    case "7":
                        C1 = equipingstones[i].skillStoneOfPlayerId;
                        break;
                    case "8":
                        C2 = equipingstones[i].skillStoneOfPlayerId;
                        break;
                    case "9":
                        C3 = equipingstones[i].skillStoneOfPlayerId;
                        break;
                }
            }
            int wholePoint = MySkillStonesReader.SkillSetValidation(A1,A2,A3,B1,B2,B3,C1,C2,C3);
            return wholePoint;
        }

        public int CheckNineSlotPointsAfterOneStoneRemoved(string monsterOfPlayerId, string SkillID)
        {
            List<SkillStoneOfPlayerInfoModel> equipingstones = MySkillStonesReader.Instance.GetMonsterEquipingStones(monsterOfPlayerId);
            string A1 = null, A2 = null, A3 = null, B1 = null, B2 = null, B3 = null, C1 = null, C2 = null, C3 = null;
            for (int i = 0; i < equipingstones.Count; i++)
            {
                if (equipingstones[i].skillId == SkillID)
                    equipingstones[i].skillId = "-1";

                switch (equipingstones[i].inUsingSkillSlot)
                {
                    case "1":
                        A1 = equipingstones[i].skillId;
                        break;
                    case "2":
                        A2 = equipingstones[i].skillId;
                        break;
                    case "3":
                        A3 = equipingstones[i].skillId;
                        break;
                    case "4":
                        B1 = equipingstones[i].skillId;
                        break;
                    case "5":
                        B2 = equipingstones[i].skillId;
                        break;
                    case "6":
                        B3 = equipingstones[i].skillId;
                        break;
                    case "7":
                        C1 = equipingstones[i].skillId;
                        break;
                    case "8":
                        C2 = equipingstones[i].skillId;
                        break;
                    case "9":
                        C3 = equipingstones[i].skillId;
                        break;
                }
            }
            int wholePoint = MySkillStonesReader.SkillSetValidation(A1, A2, A3, B1, B2, B3, C1, C2, C3);
            return wholePoint;
        }

        // 这个函数应该能够被用于Tutorial模式下亚当的技能编辑。
        public IEnumerator UpdateMyStonesBaseOnSlots(GetMonsterOfPlayerDetailModel accountCharacterInfo)
        {
            List<string> usingStones = GetUsingStonesId();// 代表现在9宫格里放着的石头里的id们。与石头有没有正式装备到角色身上无关
            for (int i = 0; i < allSlot.Count; i++)
            {
                if (allSlot[i]._DragAndDropCell.GetItem() != null)
                {
                    if (allSlot[i].OnSlotStonelocalID != allSlot[i]._DragAndDropCell.GetItem().SkillStoneOfPlayerId) 
                    {
                        // 将原先九宫格对应位置的技能石卸载。即将其inUsingMonsterOfPlayerId变为null。
                        SkillStoneOfPlayerInfoModel old_skillStoneOfPlayerInfoModel = MySkillStonesReader.Instance.GetSkillStoneOfPlayerInfoModelByMyStoneId(allSlot[i].OnSlotStonelocalID);
                        if (old_skillStoneOfPlayerInfoModel != null)
                        {
                            if (!usingStones.Contains(old_skillStoneOfPlayerInfoModel.skillStoneOfPlayerId)) // 代表原来那个位置上有个技能石，但现在它在技能背包，这轮技能编辑它是要被卸载到背包里去。
                            {
                                Debug.Log("技能石头："+ old_skillStoneOfPlayerInfoModel.skillStoneOfPlayerId + "被卸下");
                                old_skillStoneOfPlayerInfoModel.inUsingMonsterOfPlayerId = null;
                                old_skillStoneOfPlayerInfoModel.inUsingSkillSlot = null;
                                yield return MySkillStonesReader.Instance.UpdateMySkillStone(old_skillStoneOfPlayerInfoModel);
                            }else{
                                // 说明这个位置上原先的技能石现在在九宫格的其他位置上，轮到所在slot的处理时自然会更新那个技能石的信息。
                            }
                        }
                        // 下面是将九宫格slot上放着的技能石正式装备到目标角色身上。
                        SkillStoneOfPlayerInfoModel new_skillStoneOfPlayerInfoModel = MySkillStonesReader.Instance.GetSkillStoneOfPlayerInfoModelByMyStoneId(allSlot[i]._DragAndDropCell.GetItem().SkillStoneOfPlayerId);
                        if (new_skillStoneOfPlayerInfoModel != null)
                        {
                            new_skillStoneOfPlayerInfoModel.inUsingMonsterOfPlayerId = accountCharacterInfo.monsterOfPlayerId;
                            new_skillStoneOfPlayerInfoModel.inUsingSkillSlot = allSlot[i].number.ToString();
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
            SeletedRender(null);
            yield break;
        }

        public void NineSlotsStatusRefresh()//这个的核心作用在于即使调整cell的phase
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
            ShowNineSlotExSurplus(wholePoint);
        }
    }
}