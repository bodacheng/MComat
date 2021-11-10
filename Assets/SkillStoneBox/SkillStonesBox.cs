using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace mainMenu
{
    public partial class SkillStonesBox : MonoBehaviour
    {
        [Space(5)]
        [Header("进程器")]
        public SingleThreadProcesser mainProcessRunner;
        
        [Header("画面主模块parent")]
        public RectTransform BoxT;
        
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

        // rare 度限定
        public List<int> rares = new List<int> { 0, 1, 2, 3, 4, 5 };

        [Space(7)]
        [Header("技能石详细")]
        public SkillStoneDetail _skillStoneDetail;
        
        [Header("fxcamera")]
        public Camera fxCamera;
        
        string focusingtype = "human";
        int focusingExType;
        public static SkillStonesBox target;

        void Awake()
        {
            _Selected = SelectedFrame;
            rares = new List<int> { 0, 1, 2, 3, 4, 5 };//否则其值会被inspector修改
        }

        void Start()
        {
            target = this;
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
            _SkillStoneBoxTabEffectsManager.SkillButtonExplosion(0, ScreenPositionCal.Cal(1, fxCamera, self.GetComponent<RectTransform>(), 3), _SkillStoneBoxTabEffectsManager.transform);
            focusingExType = 0;
            RestFilter();
        }
        
        // Button feature
        public void EX1TabFeature(GameObject self)
        {
            _SkillStoneBoxTabEffectsManager.SkillButtonExplosion(1, ScreenPositionCal.Cal(1, fxCamera, self.GetComponent<RectTransform>(), 3), _SkillStoneBoxTabEffectsManager.transform);
            focusingExType = 1;
            RestFilter();
        }
        
        // Button feature
        public void EX2TabFeature(GameObject self)
        {
            _SkillStoneBoxTabEffectsManager.SkillButtonExplosion(2, ScreenPositionCal.Cal(1, fxCamera, self.GetComponent<RectTransform>(), 3), _SkillStoneBoxTabEffectsManager.transform);
            focusingExType = 2;
            RestFilter();
        }
        
        // Button feature
        public void EX3TabFeature(GameObject self)
        {
            _SkillStoneBoxTabEffectsManager.SkillButtonExplosion(3, ScreenPositionCal.Cal(1, fxCamera, self.GetComponent<RectTransform>(), 3), _SkillStoneBoxTabEffectsManager.transform);
            focusingExType = 3;
            RestFilter();
        }
        
        // 功能系。刷新技能石陈列界面。这里应该包括一个特殊功能，就是展示Tutorial模式下临时可用的那些石头
        public void EXTabsFeatureRefresh(bool viewingMode)
        {
            if (viewingMode)
            {
                types.ClearOptions();
                foreach (string Rname in MonstersConfigTable.GetTypeList())
                {
                    Dropdown.OptionData m_NewData = new Dropdown.OptionData
                    {
                        text = Rname
                    };
                    types.options.Add(m_NewData);
                }
                if (types.options.Count > 1)
                {
                    types.gameObject.SetActive(false);
                }else{
                    types.gameObject.SetActive(true);
                }
            }
            else
            {
                types.gameObject.SetActive(false);
            }
            closeCheckBox.onValueChanged.RemoveAllListeners();
            closeCheckBox.onValueChanged.AddListener(delegate { RestFilter(); });
            nearCheckBox.onValueChanged.RemoveAllListeners();
            nearCheckBox.onValueChanged.AddListener(delegate { RestFilter(); });
            farCheckBox.onValueChanged.RemoveAllListeners();
            farCheckBox.onValueChanged.AddListener(delegate { RestFilter(); });
        }

        // 直接放在type下拉按钮上的功能
        public void TypeDropDownBehaviour()
        {
            string targetType = types.options[types.value].text.Clone() as string;
            EXTabsFeatureRefresh(true);
            RestFilter();
        }
    }
}