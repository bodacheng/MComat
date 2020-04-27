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
        [Header("SKillPrintout")]
        public SkillsPrintOut _SkillsPrintOut;

        [Space(5)]
        [Header("CurrentHp")]
        public Text _HP;

        [Space(5)]
        [Header("九格")]
        public StoneCell
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
        
        public static void SeletedRender(StoneCell cell)
        {
            if (cell == null)
            {
                _Selected.SetActive(false);
                return;
            }
        
            if (cell._SelectMode == StoneCell.SelectMode.single)
            {
                _Selected.SetActive(true);
                _Selected.transform.SetParent(cell.GetComponent<RectTransform>());
                _Selected.transform.localPosition = Vector3.zero;
                _Selected.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
                _Selected.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
                _Selected.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
                _Selected.gameObject.SetActive(true);
            }
            else if (cell._SelectMode == StoneCell.SelectMode.multi)
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
                    SKStoneItem _SkillStone = skillStoneSlot._DragAndDropCell.GetItem();
                    if (_SkillStone != null && _SkillStone._SkillConfig != null)
                    {
                        _skillStoneDetail.RefreshSkillDetail(_SkillStone._SkillConfig, _SkillStone.SkillStoneOfPlayerId);
                        mainProcessRunner.Run(_SkillsPrintOut.SkillShowRunWithPrepare(_SkillStone._SkillConfig.REAL_NAME));
                    }
                }
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(buttonFeature);
            }
        }
        
        GameObject SkillStonePrefab;
        public SKStoneItem GenerateOneDragAndDropItem()
        {
            GameObject SkillStone = Instantiate(SkillStonePrefab);
            SkillStone.SetActive(true);
            return SkillStone.GetComponent<SKStoneItem>();
        }

        public IEnumerator StartUp()
        {
            SkillStonePrefab = new GameObject("SkillStone");
            SkillStonePrefab.AddComponent<SKStoneItem>();
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
                _slot.OnSlotStoneID = null;
                _slot._DragAndDropCell.cellPhase = StoneCell.CellPhase.NineSlotCell_empty;
                _slot.RemoveStoneFromSlot();
            }
            if (_AccountCharacterInfo == null)
            {
                yield break;
            }
            List<SkillStoneOfPlayerInfoModel> equipingstones = MySkillStonesReader.GetMonsterEquipingStones(_AccountCharacterInfo.monsterOfPlayerId);
            for (int i = 0; i < equipingstones.Count; i++)
            {
                switch (equipingstones[i].inUsingSkillSlot)
                {
                    case "1":
                        A1Slot.OnSlotStoneID = equipingstones[i].skillStoneOfPlayerId;
                        break;
                    case "2":
                        A2Slot.OnSlotStoneID = equipingstones[i].skillStoneOfPlayerId;
                        break;
                    case "3":
                        A3Slot.OnSlotStoneID = equipingstones[i].skillStoneOfPlayerId;
                        break;
                    case "4":
                        B1Slot.OnSlotStoneID = equipingstones[i].skillStoneOfPlayerId;
                        break;
                    case "5":
                        B2Slot.OnSlotStoneID = equipingstones[i].skillStoneOfPlayerId;
                        break;
                    case "6":
                        B3Slot.OnSlotStoneID = equipingstones[i].skillStoneOfPlayerId;
                        break;
                    case "7":
                        C1Slot.OnSlotStoneID = equipingstones[i].skillStoneOfPlayerId;
                        break;
                    case "8":
                        C2Slot.OnSlotStoneID = equipingstones[i].skillStoneOfPlayerId;
                        break;
                    case "9":
                        C3Slot.OnSlotStoneID = equipingstones[i].skillStoneOfPlayerId;
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
            List<SkillStoneOfPlayerInfoModel> equipingstones = MySkillStonesReader.GetMonsterEquipingStones(monsterOfPlayerId);
            string A1 = null, A2 = null, A3 = null, B1 = null, B2 = null, B3 = null, C1 = null, C2 = null, C3 = null;
            for (int i = 0; i < equipingstones.Count; i++)
            {
                switch (equipingstones[i].inUsingSkillSlot)
                {
                    case "1":
                        A1 = (equipingstones[i].skillId != SkillID) ? equipingstones[i].skillId : "-1";
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
        
        public void NineSlotsStatusRefresh()//这个的核心作用在于即使调整cell的phase
        {
            foreach (SkillStoneSlot _slot in allSlot)
            {
                _slot._DragAndDropCell.UpdateMyItem();
                _slot._DragAndDropCell.cellPhase = _slot._DragAndDropCell.GetItem() != null ? StoneCell.CellPhase.NineSlotCell_full : StoneCell.CellPhase.NineSlotCell_empty;
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