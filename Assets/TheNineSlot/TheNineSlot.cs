using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;
using Slider = UnityEngine.UI.Slider;

namespace mainMenu
{
    public partial class TheNineSlot : MonoBehaviour
    {
        [Header("CurrentHp")]
        [SerializeField] Text _HP;
        
        [Header("Validation Warning")]
        [SerializeField] Text validationWarn;
        
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
        [SerializeField] ConfirmBtnColorSwapper confirmBtnColorSwapper;

        [Header("Confirm Indicator")] 
        public GameObject confirmBtnIndicator;
        
        [Header("技能石编辑确认")]
        public Button ResetButton;
        
        [Header("EXPoint+")]
        [SerializeField] List<GameObject> remainCharges;//固定是9个长度
        [Header("EXPoint-")]
        [SerializeField] List<GameObject> burdenCharges;//固定是9个长度
        [Header("over heat bar")] 
        [SerializeField] Slider overHeatBar;
        
        [Header("选中框")]
        [SerializeField] GameObject selectedFrame;
        
        [Header("type特效管理")]
        public SkillStoneBoxTabEffectsManager _tabEffects;
        
        SkillStoneSlot _a1Slot, _a2Slot, _a3Slot;
        SkillStoneSlot _b1Slot, _b2Slot, _b3Slot;
        SkillStoneSlot _c1Slot, _c2Slot, _c3Slot;
        SkillStoneSlot _focusingSlot;
        public readonly List<SkillStoneSlot> AllSlot = new List<SkillStoneSlot>();

        public Action<string> PrintSkillInfo;

        void SelectedRender(StoneCell cell)
        {
            if (cell == null)
            {
                selectedFrame.SetActive(false);
                return;
            }
            selectedFrame.SetActive(true);
            selectedFrame.transform.SetParent(cell.GetComponent<RectTransform>());
            selectedFrame.transform.localPosition = Vector3.zero;
            selectedFrame.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
            selectedFrame.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
            selectedFrame.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
            selectedFrame.gameObject.SetActive(true);
        }
        
        public SkillStoneSlot GetFocusingStoneSlot()
        {
            return _focusingSlot;
        }
        
        void SlotBehaviour(SkillStoneSlot slot, Action<string, SKStoneItem> playSkill)
        {
            void ButtonFeature()
            {
                _focusingSlot = slot;
                SelectedRender(_focusingSlot._cell);
                slot._cell.UpdateMyItem();
                var stone = slot._cell.GetItem();
                if (stone != null && stone._SkillConfig != null)
                {
                    PrintSkillInfo.Invoke(stone.instanceId);
                    playSkill.Invoke(stone._SkillConfig.REAL_NAME, stone);
                }else{
                    PrintSkillInfo.Invoke(null);
                }
            }
            
            void DoubleClick()
            {
                _focusingSlot = null;
                SelectedRender(null);
            }
            
            // 前往技能石升级画面
            void GoToLevelUpPage()
            {
                if (!FightGlobalSetting.SkillStoneHasExp)
                    return;
                var stone = slot._cell.GetItem();
                if (stone != null && stone._SkillConfig != null)
                {
                    PreScene.target.trySwitchToStep(MainSceneStep.SkillStoneList, stone.instanceId, true);
                }
            }
            
            slot._cell.btn.SetListener(ButtonFeature);
            
            slot._cell.btn.ActivateHold = true;
            slot._cell.btn.ActivateDoubleClick = true;
            slot._cell.btn.onHold.AddListener(GoToLevelUpPage);
            slot._cell.btn.onDoubleClick.AddListener(DoubleClick);
            
            slot._cell.SetOnDropAction(((from, to) =>
            {
                StoneCell.Install(from, to);
                ValidateWarn();
            }));
        }
        
        public void StartUp(Action<string, SKStoneItem> runSkill)
        {
            SelectedRender(null);
            
            _a1Slot = new SkillStoneSlot(1, A1DragAndDropCell);
            _a2Slot = new SkillStoneSlot(2, A2DragAndDropCell);
            _a3Slot = new SkillStoneSlot(3, A3DragAndDropCell);
            _b1Slot = new SkillStoneSlot(4, B1DragAndDropCell);
            _b2Slot = new SkillStoneSlot(5, B2DragAndDropCell);
            _b3Slot = new SkillStoneSlot(6, B3DragAndDropCell);
            _c1Slot = new SkillStoneSlot(7, C1DragAndDropCell);
            _c2Slot = new SkillStoneSlot(8, C2DragAndDropCell);
            _c3Slot = new SkillStoneSlot(9, C3DragAndDropCell);

            AllSlot.Clear();
            AllSlot.Add(_a1Slot);
            AllSlot.Add(_a2Slot);
            AllSlot.Add(_a3Slot);
            AllSlot.Add(_b1Slot);
            AllSlot.Add(_b2Slot);
            AllSlot.Add(_b3Slot);
            AllSlot.Add(_c1Slot);
            AllSlot.Add(_c2Slot);
            AllSlot.Add(_c3Slot);
            
            foreach (var _slot in AllSlot)
            {
                SlotBehaviour(_slot, runSkill);
            }
        }
        
        // 当前技能编辑形成的各项参数更新
        public bool NineSlotsStatusRefresh()
        {
            var full = true;
            foreach (var slot in AllSlot)
            {
                slot._cell.UpdateMyItem();
                if (slot._cell.GetItem() == null)
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
            RefreshEffects();
            var valR = ValidateWarn();
            //ConfirmSkillChangeButton.gameObject.SetActive(valR == SkillSet.SkillEditError.Perfect);
            return full;
        }
        
         async void RefreshEffects()
         {
             foreach (var slot in AllSlot)
            {
                var item = slot._cell.GetItem();
                await Task.Delay(1);// wait for the UI Layer to be stable.Otherwise pos caculation will be wrong at the start
                if (slot != null && slot._cell != null)
                {
                    var worldPos = PosCal.GetWorldPos(PreScene.target.postProcessCamera, slot._cell.GetComponent<RectTransform>(), 5f);
                    _tabEffects.RefreshSlotEffect(slot.num, worldPos, item != null ? item._SkillConfig.SP_LEVEL : -1);
                }
            }
         }
    }
}