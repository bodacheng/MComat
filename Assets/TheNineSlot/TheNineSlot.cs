using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

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

        [Header("Confirm Indicator")] 
        public GameObject confirmBtnIndicator;
        
        [Header("技能石编辑确认")]
        public Button ResetButton;
        
        [Header("EXPoint+")]
        public List<GameObject> remainCharges;//固定是9个长度
        [Header("EXPoint-")]
        public List<GameObject> burdenCharges;//固定是9个长度
        [Header("over heat bar")] 
        public Slider overHeatBar;
        
        [Header("选中框")]
        [SerializeField] GameObject SelectedFrame;
        
        [Header("type特效管理")]
        public SkillStoneBoxTabEffectsManager _tabEffects;
        
        SkillStoneSlot _a1Slot, _a2Slot, _a3Slot;
        SkillStoneSlot _b1Slot, _b2Slot, _b3Slot;
        SkillStoneSlot _c1Slot, _c2Slot, _c3Slot;
        SkillStoneSlot focusingSlot;
        public readonly List<SkillStoneSlot> allSlot = new();

        public Action<string> PrintSkillInfo;
        public Action plsTryNormalSkill;
        
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
            void ButtonFeature()
            {
                focusingSlot = slot;
                SelectedRender(focusingSlot._cell);
                slot._cell.UpdateMyItem();
                var skillStone = slot._cell.GetItem();
                if (skillStone != null && skillStone._SkillConfig != null)
                {
                    PrintSkillInfo.Invoke(skillStone.instanceId);
                    playSkill.Invoke(skillStone._SkillConfig.REAL_NAME);
                }else{
                    PrintSkillInfo.Invoke(null);
                }
            }
            
            void DoubleClick()
            {
                focusingSlot = null;
                SelectedRender(null);
            }
            
            // 前往技能石升级画面
            void GoToLevelUpPage()
            {
                if (!FightGlobalSetting._skillStoneHasExp)
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
        
        public void StartUp(Action<string> runSkill)
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

            allSlot.Clear();
            allSlot.Add(_a1Slot);
            allSlot.Add(_a2Slot);
            allSlot.Add(_a3Slot);
            allSlot.Add(_b1Slot);
            allSlot.Add(_b2Slot);
            allSlot.Add(_b3Slot);
            allSlot.Add(_c1Slot);
            allSlot.Add(_c2Slot);
            allSlot.Add(_c3Slot);
            
            foreach (var _slot in allSlot)
            {
                SlotBehaviour(_slot, runSkill);
            }
        }
        
        // 当前技能编辑形成的各项参数更新
        public bool NineSlotsStatusRefresh()
        {
            var full = true;
            foreach (var slot in allSlot)
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
             foreach (var slot in allSlot)
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