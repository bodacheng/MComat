using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using dataAccess;
using Api.Dto.Model;
using Michsky.UI.Shift;

namespace mainMenu
{
    public partial class TheNineSlot : MonoBehaviour
    {
        public static TheNineSlot target;
        
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
        [Header("Validation Warning")]
        public Text _ValiWarn;
        
        [Space(5)]
        [Header("九格")]
        public StoneCell
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
        public static GameObject _Selected;
        
        [Space(5)]
        [Header("UIManager 就是有个UI插件自带的一套东西")]
        public UIManager uIManager;// 实时控制确认按钮颜色
        
        SkillStoneSlot A1Slot, A2Slot, A3Slot;
        SkillStoneSlot B1Slot, B2Slot, B3Slot;
        SkillStoneSlot C1Slot, C2Slot, C3Slot;
        public SkillStoneSlot focusingSlot;
        readonly List<SkillStoneSlot> allSlot = new List<SkillStoneSlot>();
        float last_clickTime;
        
        void Awake()
        {
            _Selected = SelectedFrame;
            target = this;
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
                        _skillStoneDetail.RefreshSkillDetail(_SkillStone.SkillStoneOfPlayerId);
                        mainProcessRunner.Run(_SkillsPrintOut.SkillShowRunWithPrepare(_SkillStone._SkillConfig.REAL_NAME));
                    }else{
                        _skillStoneDetail.Clear();
                    }
                }
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(buttonFeature);
            }
        }
        
        public IEnumerator StartUp()
        {
            NineSlotT.gameObject.SetActive(false);
            yield return GetNineSlotReady();
        }
        
        IEnumerator GetNineSlotReady()
        {
            SeletedRender(null);
            
            A1Slot = new SkillStoneSlot(1, null, A1DragAndDropCell);
            A2Slot = new SkillStoneSlot(2, null, A2DragAndDropCell);
            A3Slot = new SkillStoneSlot(3, null, A3DragAndDropCell);
            B1Slot = new SkillStoneSlot(4, null, B1DragAndDropCell);
            B2Slot = new SkillStoneSlot(5, null, B2DragAndDropCell);
            B3Slot = new SkillStoneSlot(6, null, B3DragAndDropCell);
            C1Slot = new SkillStoneSlot(7, null, C1DragAndDropCell);
            C2Slot = new SkillStoneSlot(8, null, C2DragAndDropCell);
            C3Slot = new SkillStoneSlot(9, null, C3DragAndDropCell);
            
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
        
        // 当前技能编辑形成的各项参数更新
        public void NineSlotsStatusRefresh()
        {
            foreach (SkillStoneSlot _slot in allSlot)
            {
                _slot._DragAndDropCell.UpdateMyItem();
            }
            List<string> skillIDsOnNineSlots = GetCurrentNineSlotAllSkillIds();
            int wholePoint = NineAndTwo.SkillBalancePoint(
                skillIDsOnNineSlots[0], skillIDsOnNineSlots[1], skillIDsOnNineSlots[2],
                skillIDsOnNineSlots[3], skillIDsOnNineSlots[4], skillIDsOnNineSlots[5],
                skillIDsOnNineSlots[6], skillIDsOnNineSlots[7], skillIDsOnNineSlots[8]
            );
            
            ShowNineSlotExSurplus(wholePoint);
            RefreshCurrentHpBasedOnNineSlots();
            RefreshNineSlotColors();
            
            NineAndTwo.SkillEditError valR = target.CheckEditBasedOnCurrent();
            if (valR != NineAndTwo.SkillEditError.Perfect)
            {
                // confirm 按钮颜色变化
                uIManager.primaryColor = new Color(1,0,0,1);
            }else{
                uIManager.primaryColor = new Color(0,1,0,1);
            }
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