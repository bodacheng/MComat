using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace mainMenu
{
    public partial class SkillStonesBox : MonoBehaviour
    {
        [Space(5)]
        [Header("进程器")]
        public SingleThreadProcesser mainProcessRunner;

        [Header("画面主模块parent")]
        public RectTransform SkillBoxCanvas;
        public RectTransform BoxT, stonesTempContainer;
        
        [Space(7)]
        [Header("type按钮")]
        public Dropdown types;
        public Button NormalTab;
        public Button EX1Tab;
        public Button EX2Tab;
        public Button EX3Tab;
        
        [Space(7)]
        [Header("type特效管理")]
        public SkillStoneBoxTabEffectsManager _SkillStoneBoxTabEffectsManager;

        [Space(7)]
        [Header("攻击范围限定")]
        public Toggle closeCheckBox;
        public Toggle nearCheckBox;
        public Toggle farCheckBox;
        public Toggle outRangeCheckBox;
        
        [Space(7)]
        [Header("技能石详细")]
        public SkillStoneDetail _skillStoneDetail;
        
        [Header("fxcamera")]
        public Camera fxCamera;

        string focusingtype = "human";
        int focusingExType;
        public static RectTransform _stonesTempContainer;
        
        public static SkillStonesBox target;
        
        void Awake()
        {
            _Selected = SelectedFrame;
            _stonesTempContainer = stonesTempContainer;
        }
        
        public IEnumerator StartUp(int stoneboxsize)
        {
            GenerateCells(stoneboxsize, 1);
            yield break;
        }
        
        public string GetFocusingType()
        {
            return focusingtype;
        }
        public void SetFocusingType(string type)
        {
            focusingtype = type;
        }
        public int GetFocusingExType()
        {
            return focusingExType;
        }
        
        // Button feature
        public void NormalTabFeature(GameObject self)
        {
            _SkillStoneBoxTabEffectsManager.SkillButtonExplosion(0, ScreenPositionCal.Cal(1, fxCamera, self.GetComponent<RectTransform>(), 3));
            focusingExType = 0;
            TheNineSlot.target.mainProcessRunner.Run(ArrangeSkillStonesToBox());
        }
        
        // Button feature
        public void EX1TabFeature(GameObject self)
        {
            _SkillStoneBoxTabEffectsManager.SkillButtonExplosion(1, ScreenPositionCal.Cal(1, fxCamera, self.GetComponent<RectTransform>(), 3));
            focusingExType = 1;
            TheNineSlot.target.mainProcessRunner.Run(ArrangeSkillStonesToBox());
        }
        
        // Button feature
        public void EX2TabFeature(GameObject self)
        {
            _SkillStoneBoxTabEffectsManager.SkillButtonExplosion(2, ScreenPositionCal.Cal(1, fxCamera, self.GetComponent<RectTransform>(), 3));
            focusingExType = 2;
            TheNineSlot.target.mainProcessRunner.Run(ArrangeSkillStonesToBox());
        }
        
        // Button feature
        public void EX3TabFeature(GameObject self)
        {
            _SkillStoneBoxTabEffectsManager.SkillButtonExplosion(3, ScreenPositionCal.Cal(1, fxCamera, self.GetComponent<RectTransform>(), 3));
            focusingExType = 3;
            TheNineSlot.target.mainProcessRunner.Run(ArrangeSkillStonesToBox());
        }
        
        // 功能系。刷新技能石陈列界面。这里应该包括一个特殊功能，就是展示Tutorial模式下临时可用的那些石头
        public IEnumerator EXTabsFeatureRefresh(bool viewingMode)
        {
            if (viewingMode)
            {
                types.gameObject.SetActive(true);
                types.ClearOptions();
                foreach (string Rname in MonstersConfigTable.Instance.GetTypeList())
                {
                    Dropdown.OptionData m_NewData = new Dropdown.OptionData
                    {
                        text = Rname
                    };
                    types.options.Add(m_NewData);
                }
            }
            else
            {
                types.gameObject.SetActive(false);
            }
            closeCheckBox.onValueChanged.RemoveAllListeners();
            closeCheckBox.onValueChanged.AddListener(delegate { RangeCheckBoxOnValueChanged(); });
            nearCheckBox.onValueChanged.RemoveAllListeners();
            nearCheckBox.onValueChanged.AddListener(delegate { RangeCheckBoxOnValueChanged(); });
            farCheckBox.onValueChanged.RemoveAllListeners();
            farCheckBox.onValueChanged.AddListener(delegate { RangeCheckBoxOnValueChanged(); });
            yield break;
        }
        
        void RangeCheckBoxOnValueChanged()
        {
            TheNineSlot.target.mainProcessRunner.Run(ArrangeSkillStonesToBox());
        }
        
        public void TypeDropDownBehaviour()// 直接放在type下拉按钮上的功能
        {
            string targetType = types.options[types.value].text.Clone() as string;
            TheNineSlot.target.mainProcessRunner.Run(EXTabsFeatureRefresh(true));
        }
    }
}