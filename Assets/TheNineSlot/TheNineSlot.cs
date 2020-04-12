using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using dataAccess;
using Api.Dto.Model;

//这个模块应该具备能力去读取一个角色的九宫信息。
namespace mainMenu
{
    public partial class TheNineSlot : MonoBehaviour
    {
        public static TheNineSlot Instance;

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
        [Header("CurrentHp")]
        public Text _HP;

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
        
        [Space(5)]
        [Header("选中框")]
        public GameObject SelectedFrame;
        public static GameObject _Selected;

        SkillStoneSlot A1Slot, A2Slot, A3Slot;
        SkillStoneSlot B1Slot, B2Slot, B3Slot;
        SkillStoneSlot C1Slot, C2Slot, C3Slot;
        SkillStoneSlot focusingSlot;
        readonly List<SkillStoneSlot> allSlot = new List<SkillStoneSlot>();
        float last_clickTime;
        
        void Awake()
        {
            _Selected = SelectedFrame;
            Instance = this;
        }
        
        public static void SeletedRender(DragAndDropCell cell)
        {
            if (cell == null)
            {
                _Selected.SetActive(false);
                return;
            }
        
            if (cell._SelectMode == DragAndDropCell.SelectMode.single)
            {
                _Selected.SetActive(true);
                _Selected.transform.SetParent(cell.GetComponent<RectTransform>());
                _Selected.transform.localPosition = Vector3.zero;
                _Selected.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
                _Selected.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
                _Selected.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
                _Selected.gameObject.SetActive(true);
            }
            else if (cell._SelectMode == DragAndDropCell.SelectMode.multi)
            {
            
            }
        }
        
        public SkillStoneSlot GetFocusingStoneSlot()
        {
            return focusingSlot;
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
                        SeletedRender(focusingSlot._DragAndDropCell);
                    }
                    last_clickTime = Time.time;
                    skillStoneSlot._DragAndDropCell.UpdateMyItem();
                    DragAndDropItem _SkillStone = skillStoneSlot._DragAndDropCell.GetItem();
                    if (_SkillStone != null && _SkillStone._SkillConfigOfSkillStone != null)
                    {
                        _skillStoneDetail.RefreshSkillDetail(_SkillStone._SkillConfigOfSkillStone, _SkillStone.SkillStoneOfPlayerId);
                        mainProcessRunner.Run(_SkillsPrintOut.SkillShowRunWithPreparing(_SkillStone._SkillConfigOfSkillStone.REAL_NAME));
                    }
                }
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(buttonFeature);
            }
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
            TheNineSlot.SeletedRender(null);
            
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

            foreach (SkillStoneSlot _slot in allSlot)
            {
                yield return _slot.ShowOrigin(Color.white);
            }
            NineSlotsStatusRefresh();
        }

        public int CheckNineSlotPointsAfterOneStoneRemoved(string monsterOfPlayerId, string SkillID)
        {
            List<SkillStoneOfPlayerInfoModel> equipingstones = MySkillStonesReader.Instance.GetMonsterEquipingStones(monsterOfPlayerId);
            string A1 = null, A2 = null, A3 = null, B1 = null, B2 = null, B3 = null, C1 = null, C2 = null, C3 = null;
            for (int i = 0; i < equipingstones.Count; i++)
            {
                switch (equipingstones[i].inUsingSkillSlot)
                {
                    case "1":
                        A1 = (equipingstones[i].skillId != SkillID)? equipingstones[i].skillId : "-1";
                        break;
                    case "2":
                        A2 = (equipingstones[i].skillId != SkillID) ? equipingstones[i].skillId : "-1";
                        break;
                    case "3":
                        A3 = (equipingstones[i].skillId != SkillID) ? equipingstones[i].skillId : "-1";
                        break;
                    case "4":
                        B1 = (equipingstones[i].skillId != SkillID) ? equipingstones[i].skillId : "-1";
                        break;
                    case "5":
                        B2 = (equipingstones[i].skillId != SkillID) ? equipingstones[i].skillId : "-1";
                        break;
                    case "6":
                        B3 = (equipingstones[i].skillId != SkillID) ? equipingstones[i].skillId : "-1";
                        break;
                    case "7":
                        C1 = (equipingstones[i].skillId != SkillID) ? equipingstones[i].skillId : "-1";
                        break;
                    case "8":
                        C2 = (equipingstones[i].skillId != SkillID) ? equipingstones[i].skillId : "-1";
                        break;
                    case "9":
                        C3 = (equipingstones[i].skillId != SkillID) ? equipingstones[i].skillId : "-1";
                        break;
                }
            }
            int wholePoint = MySkillStonesReader.SkillBalancePoint(A1, A2, A3, B1, B2, B3, C1, C2, C3);
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
                        SkillStoneOfPlayerInfoModel old_skillStoneOfPlayerInfoModel = MySkillStonesReader.Instance.GetStoneOfPlayerInfoModelByMyStoneId(allSlot[i].OnSlotStonelocalID);
                        if (old_skillStoneOfPlayerInfoModel != null)
                        {
                            if (!usingStones.Contains(old_skillStoneOfPlayerInfoModel.skillStoneOfPlayerId)) // 代表原来那个位置上有个技能石，但现在它在技能背包，这轮技能编辑它是要被卸载到背包里去。
                            {
                                Debug.Log("技能石头："+ old_skillStoneOfPlayerInfoModel.skillStoneOfPlayerId + "被卸下");
                                old_skillStoneOfPlayerInfoModel.inUsingMonsterOfPlayerId = null;
                                old_skillStoneOfPlayerInfoModel.inUsingSkillSlot = null;
                                yield return MySkillStonesReader.Instance.UpdateMySkillStone();
                            }else{
                                // 说明这个位置上原先的技能石现在在九宫格的其他位置上，轮到所在slot的处理时自然会更新那个技能石的信息。
                            }
                        }
                        // 下面是将九宫格slot上放着的技能石正式装备到目标角色身上。
                        SkillStoneOfPlayerInfoModel new_skillStoneOfPlayerInfoModel = MySkillStonesReader.Instance.GetStoneOfPlayerInfoModelByMyStoneId(allSlot[i]._DragAndDropCell.GetItem().SkillStoneOfPlayerId);
                        if (new_skillStoneOfPlayerInfoModel != null)
                        {
                            new_skillStoneOfPlayerInfoModel.inUsingMonsterOfPlayerId = accountCharacterInfo.monsterOfPlayerId;
                            new_skillStoneOfPlayerInfoModel.inUsingSkillSlot = allSlot[i].number.ToString();
                            yield return MySkillStonesReader.Instance.UpdateMySkillStone();
                        }
                    }
                }else{
                    SkillStoneOfPlayerInfoModel old_skillStoneOfPlayerInfoModel = MySkillStonesReader.Instance.GetStoneOfPlayerInfoModelByMyStoneId(allSlot[i].OnSlotStonelocalID);
                    if (old_skillStoneOfPlayerInfoModel != null)// 旧技能石被卸下
                    {
                        if (!usingStones.Contains(old_skillStoneOfPlayerInfoModel.skillStoneOfPlayerId))// 代表卸载的技能石头
                        {
                            Debug.Log("技能石头："+ old_skillStoneOfPlayerInfoModel.skillStoneOfPlayerId + "被卸下");
                            old_skillStoneOfPlayerInfoModel.inUsingMonsterOfPlayerId = null;
                            old_skillStoneOfPlayerInfoModel.inUsingSkillSlot = null;
                            yield return MySkillStonesReader.Instance.UpdateMySkillStone();
                        }else{
                            // 说明这个位置上原先的技能石现在在九宫格的其他位置上，轮到所在slot的处理时自然会更新那个技能石的信息。
                        }
                    }
                }
            }
            yield return ReadANineAndTwo(accountCharacterInfo);
            TheNineSlot.SeletedRender(null);
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
            List<string> skillIDsOnNineSlots = GetCurrentNineSlotAllSkillIds();
            int wholePoint = MySkillStonesReader.SkillBalancePoint(
            skillIDsOnNineSlots[0], skillIDsOnNineSlots[1], skillIDsOnNineSlots[2],
            skillIDsOnNineSlots[3], skillIDsOnNineSlots[4], skillIDsOnNineSlots[5],
            skillIDsOnNineSlots[6], skillIDsOnNineSlots[7], skillIDsOnNineSlots[8]);
            ShowNineSlotExSurplus(wholePoint);
            RefreshCurrentHpBasedOnNineSlots();
        }
    }
}