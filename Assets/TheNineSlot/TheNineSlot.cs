using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Michsky.UI.Shift;
using UniRx;

namespace mainMenu
{
    public partial class TheNineSlot : MonoBehaviour
    {
        [Space(5)]
        [Header("进程器")]
        [SerializeField] SingleThreadProcesser mainProcessRunner;
        
        [Space(5)]
        [Header("CurrentHp")]
        [SerializeField] Text _HP;
        
        [Space(5)]
        [Header("Validation Warning")]
        [SerializeField] Text _ValiWarn;
        
        [Space(5)]
        [Header("九格")]
        [SerializeField] StoneCell
        A1DragAndDropCell, A2DragAndDropCell, A3DragAndDropCell,
        B1DragAndDropCell, B2DragAndDropCell, B3DragAndDropCell,
        C1DragAndDropCell, C2DragAndDropCell, C3DragAndDropCell;
        
        [Space(1)]
        [Header("技能石编辑确认")]
        public Button ConfirmSkillChangeButton;
        
        [Space(1)]
        [Header("技能石编辑确认")]
        public Button ResetButton;
        
        [Space(7)]
        [Header("技能石详细")]
        public SkillStoneDetail _skillStoneDetail;
        
        [Space(7)]
        [Header("EXPoint+")]
        public List<GameObject> remainCharges;//固定是9个长度
        [Space(7)]
        [Header("EXPoint-")]
        public List<GameObject> burdenCharges;//固定是9个长度
        
        [Space(5)]
        [Header("选中框")]
        public GameObject SelectedFrame;

        [Space(5)]
        [Header("UIManager 就是有个UI插件自带的一套东西")]
        public UIManager uIManager;// 实时控制确认按钮颜色
        
        SkillStoneSlot A1Slot, A2Slot, A3Slot;
        SkillStoneSlot B1Slot, B2Slot, B3Slot;
        SkillStoneSlot C1Slot, C2Slot, C3Slot;
        public SkillStoneSlot focusingSlot;
        readonly List<SkillStoneSlot> allSlot = new List<SkillStoneSlot>();
        float last_clickTime;
        bool pressStart;
        float pressingSeconds;
        SingleAssignmentDisposable pressCount;
        
        public void SeletedRender(StoneCell cell)
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
                        _skillStoneDetail.RefreshInfo(_SkillStone.instanceId);
                        mainProcessRunner.RunAsQueued(SkillShowSupporter.SkillShowRunWithPrepare(_SkillStone._SkillConfig.REAL_NAME));
                    }else{
                        _skillStoneDetail.Clear();
                    }
                }
                
                EventTrigger trigger = button.GetComponent<EventTrigger>();
                EventTrigger.Entry enter = new EventTrigger.Entry
                {
                    eventID = EventTriggerType.PointerDown
                };
                EventTrigger.Entry up = new EventTrigger.Entry
                {
                    eventID = EventTriggerType.PointerUp
                };
                enter.callback.AddListener((eventData) => {
                    if (!pressStart)
                    {
                        pressStart = true;
                        PressGoToLevelUpPage(skillStoneSlot._DragAndDropCell);
                        StoneCell.SeletedRender(skillStoneSlot._DragAndDropCell, SelectedFrame);
                        buttonFeature();
                    }

                } );
                up.callback.AddListener( (eventData) => { pressStart = false; } );
                
                trigger.triggers.Clear();
                trigger.triggers.Add(enter);
                trigger.triggers.Add(up);
            }
        }
        
        // 前往技能石升级画面
        public void PressGoToLevelUpPage(StoneCell _SkillStoneCell)
        {
            pressCount = new SingleAssignmentDisposable
            {
                Disposable = Observable.EveryUpdate().Subscribe(_ =>
                    {
                        if (pressStart)
                        {
                            pressingSeconds += Time.deltaTime;
                            if (pressingSeconds > 1f)
                            {
                                pressingSeconds = 0;
                                pressStart = false;
                                SKStoneItem _stone = _SkillStoneCell.GetItem();
                                if (_stone != null && _stone._SkillConfig != null)
                                {
                                    if (FightGlobalSetting._skillStoneHasExp)
                                        PreScene.target.trySwitchToStep(MainSceneStep.SkillStoneList, _stone.instanceId, true);
                                }
                            }
                        }
                        if (!pressStart)
                        {
                            pressingSeconds = 0;
                            if (!pressCount.IsDisposed)
                            {
                                pressCount.Dispose();
                            }
                        }
                    }
                )
            };
        }
        
        public void StartUp()
        {
            SeletedRender(null);
            
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

            foreach (SkillStoneSlot _slot in allSlot)
            {
                SlotButtonBeheviour(_slot);
            }
        }
        
        // 当前技能编辑形成的各项参数更新
        public bool NineSlotsStatusRefresh()
        {
            bool full = true;
            foreach (SkillStoneSlot _slot in allSlot)
            {
                _slot._DragAndDropCell.UpdateMyItem();
                if (_slot._DragAndDropCell.GetItem() == null)
                {
                    full = false;
                }
            }
            List<string> skillIDsOnNineSlots = GetCurrentNineSlotAllSkillIds();
            int wholePoint = SkillSet.SkillBalancePoint(
                skillIDsOnNineSlots[0], skillIDsOnNineSlots[1], skillIDsOnNineSlots[2],
                skillIDsOnNineSlots[3], skillIDsOnNineSlots[4], skillIDsOnNineSlots[5],
                skillIDsOnNineSlots[6], skillIDsOnNineSlots[7], skillIDsOnNineSlots[8]
            );
            
            ShowNineSlotExSurplus(wholePoint);
            RefreshCurrentHpBasedOnNineSlots();
            RefreshNineSlotColors();
            
            SkillSet.SkillEditError valR = CheckEditBasedOnCurrent();
            if (valR != SkillSet.SkillEditError.Perfect)
            {
                // confirm 按钮颜色变化
                uIManager.primaryColor = new Color(1,0,0,1);
            }else{
                uIManager.primaryColor = new Color(0,1,0,1);
            }
            return full;
        }
        
         void RefreshNineSlotColors()
         {
            foreach (SkillStoneSlot _slot in allSlot)
            {
                SKStoneItem sKStoneItem = _slot._DragAndDropCell.GetItem();
                if (sKStoneItem == null)
                {
                    _slot._DragAndDropCell.GetComponent<Image>().color = new Color(1f, 1f ,1f, 1f);
                    continue;
                }
                switch (sKStoneItem._SkillConfig.SP_LEVEL)
                {
                    case 1:
                        _slot._DragAndDropCell.GetComponent<Image>().color = new Color(1,0.2f,0.3f,1f);
                    break;
                    case 2:
                        _slot._DragAndDropCell.GetComponent<Image>().color = new Color(0f,1f,0.1f,1f);
                    break;
                    case 3:
                        _slot._DragAndDropCell.GetComponent<Image>().color = new Color(0f,0.1f,1f,1f);
                    break;
                    default:
                        _slot._DragAndDropCell.GetComponent<Image>().color = new Color(1f, 1f ,1f, 1f);
                    break;
                }
            }
         }
    }
}