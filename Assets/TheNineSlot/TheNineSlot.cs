using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace mainMenu
{
    public partial class TheNineSlot : MonoBehaviour
    {
        [Header("CurrentHp")]
        [SerializeField] Text _HP;
        
        [Header("Validation Warning")]
        [SerializeField] Text _ValiWarn;
        
        [Header("九格")]
        [SerializeField] StoneCell
        A1DragAndDropCell, A2DragAndDropCell, A3DragAndDropCell,
        B1DragAndDropCell, B2DragAndDropCell, B3DragAndDropCell,
        C1DragAndDropCell, C2DragAndDropCell, C3DragAndDropCell;
        
        [Header("Remove all")]
        public Button removeAllBtn;
    
        [Header("Random all")]
        public Button randomBtn;
        
        [Header("技能石编辑确认")]
        public Button ConfirmSkillChangeButton;
        
        [Header("技能石编辑确认")]
        public Button ResetButton;
        
        [Header("EXPoint+")]
        public List<GameObject> remainCharges;//固定是9个长度
        [Header("EXPoint-")]
        public List<GameObject> burdenCharges;//固定是9个长度
        [Header("over heat bar")] 
        public Slider overHeatBar;
        
        [Header("选中框")]
        public GameObject SelectedFrame;
        
        [Header("type特效管理")]
        public SkillStoneBoxTabEffectsManager _tabEffects;
        
        SkillStoneSlot A1Slot, A2Slot, A3Slot;
        SkillStoneSlot B1Slot, B2Slot, B3Slot;
        SkillStoneSlot C1Slot, C2Slot, C3Slot;
        SkillStoneSlot focusingSlot;
        public readonly List<SkillStoneSlot> allSlot = new List<SkillStoneSlot>();

        public Action<string> PrintSkillInfo;
        
        void SelectedRender(StoneCell cell)
        {
            if (cell == null)
            {
                SelectedFrame.SetActive(false);
                return;
            }
            SelectedFrame.SetActive(true);
            SelectedFrame.transform.SetParent(cell.GetComponent<RectTransform>());
            SelectedFrame.transform.localPosition = Vector3.zero;
            SelectedFrame.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
            SelectedFrame.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
            SelectedFrame.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
            SelectedFrame.gameObject.SetActive(true);
        }
        
        public SkillStoneSlot GetFocusingStoneSlot()
        {
            return focusingSlot;
        }
        
        void SlotBehaviour(SkillStoneSlot slot, Action<string> playSkill)
        {
            void buttonFeature(object sender, System.EventArgs e)
            {
                focusingSlot = slot;
                SelectedRender(focusingSlot._cell);
                slot._cell.UpdateMyItem();
                SKStoneItem _SkillStone = slot._cell.GetItem();
                if (_SkillStone != null && _SkillStone._SkillConfig != null)
                {
                    PrintSkillInfo.Invoke(_SkillStone.instanceId);
                    playSkill.Invoke(_SkillStone._SkillConfig.REAL_NAME);
                }else{
                    PrintSkillInfo.Invoke(null);
                }
            }
            
            void doubleClick(object sender, System.EventArgs e)
            {
                focusingSlot = null;
                SelectedRender(null);
            }
            
            //slot._cell.pGesture.Pressed += buttonFeature;
            //slot._DragAndDropCell.tGesture.Tapped += doubleClick;
            //slot._DragAndDropCell.lpGesture.StateChanged += GoToLevelUpPage;

            slot._cell.SetOnDropAction(((from, to) =>
            {
                StoneCell.Install(from, to);
                ValidateWarn();
            }));
        }
        
        public void StartUp(Action<string> runSkill)
        {
            SelectedRender(null);
            
            A1Slot = new SkillStoneSlot(1, A1DragAndDropCell);
            A2Slot = new SkillStoneSlot(2, A2DragAndDropCell);
            A3Slot = new SkillStoneSlot(3, A3DragAndDropCell);
            B1Slot = new SkillStoneSlot(4, B1DragAndDropCell);
            B2Slot = new SkillStoneSlot(5, B2DragAndDropCell);
            B3Slot = new SkillStoneSlot(6, B3DragAndDropCell);
            C1Slot = new SkillStoneSlot(7, C1DragAndDropCell);
            C2Slot = new SkillStoneSlot(8, C2DragAndDropCell);
            C3Slot = new SkillStoneSlot(9, C3DragAndDropCell);

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
            
            foreach (var _slot in allSlot)
            {
                SlotBehaviour(_slot, runSkill);
            }
        }
        
        // 当前技能编辑形成的各项参数更新
        public bool NineSlotsStatusRefresh()
        {
            var full = true;
            foreach (var _slot in allSlot)
            {
                _slot._cell.UpdateMyItem();
                if (_slot._cell.GetItem() == null)
                {
                    full = false;
                }
            }
            var skillIDsOnNineSlots = GetCurrentNineSlotAllSkillIds();
            var wholePoint = SkillSet.SkillBalancePoint(
                skillIDsOnNineSlots[0], skillIDsOnNineSlots[1], skillIDsOnNineSlots[2],
                skillIDsOnNineSlots[3], skillIDsOnNineSlots[4], skillIDsOnNineSlots[5],
                skillIDsOnNineSlots[6], skillIDsOnNineSlots[7], skillIDsOnNineSlots[8]
            );
            
            ShowNineSlotExSurplus(wholePoint);
            RefreshCurrentHpBasedOnNineSlots();
            RefreshNineSlotColors();
            var valR = ValidateWarn();
            ConfirmSkillChangeButton.gameObject.SetActive(valR == SkillSet.SkillEditError.Perfect);
            return full;
        }
        
         void RefreshNineSlotColors()
         {
            foreach (var _slot in allSlot)
            {
                var item = _slot._cell.GetItem();
                var worldPos = PosCal.GetWorldPos(PreScene.target.FxCamera, _slot._cell.GetComponent<RectTransform>(), 5f);
                _tabEffects.RefreshSlotEffect(_slot.num, worldPos, item != null ? item._SkillConfig.SP_LEVEL : -1);
            }
            
            return;
            foreach (SkillStoneSlot _slot in allSlot)
            {
                SKStoneItem sKStoneItem = _slot._cell.GetItem();
                if (sKStoneItem == null)
                {
                    _slot._cell.GetComponent<Image>().color = new Color(1f, 1f ,1f, 1f);
                    continue;
                }
                switch (sKStoneItem._SkillConfig.SP_LEVEL)
                {
                    case 1:
                        _slot._cell.GetComponent<Image>().color = new Color(1,0.2f,0.3f,1f);
                    break;
                    case 2:
                        _slot._cell.GetComponent<Image>().color = new Color(0f,1f,0.1f,1f);
                    break;
                    case 3:
                        _slot._cell.GetComponent<Image>().color = new Color(0f,0.1f,1f,1f);
                    break;
                    default:
                        _slot._cell.GetComponent<Image>().color = new Color(1f, 1f ,1f, 1f);
                    break;
                }
            }
         }
    }
}