using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace mainMenu
{
    public partial class SkillStonesBox : MonoBehaviour
    {
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
        
        string focusingtype = "human";
        int focusingExType;
        //public static SkillStonesBox target;

        void Awake()
        {
            _Selected = SelectedFrame;
            rares = new List<int> { 0, 1, 2, 3, 4, 5 };//否则其值会被inspector修改
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

        public void IniExTabs(Camera fxCamera)
        {
            void Temp(Button btn, int exLevel)
            {
                Vector3 worldPos = 
                    PosCal.GetWorldPos(PreScene.target.FxCamera, 
                    PosCal.ConvertAnchorPos(btn.GetComponent<RectTransform>().anchoredPosition, Vector2.one, Vector2.zero), 5f);
                _SkillStoneBoxTabEffectsManager.RefreshTagEffect(worldPos, exLevel);
                btn.onClick.AddListener(() =>
                {
                    //NormalTabFeature(PosCal.GetWorldPos(fxCamera, btn.GetComponent<RectTransform>(), 3));
                    _SkillStoneBoxTabEffectsManager.SkillButtonExplosion
                        (exLevel, PosCal.GetWorldPos(fxCamera, btn.GetComponent<RectTransform>(), 5f), null);//_SkillStoneBoxTabEffectsManager.transform
                    focusingExType = exLevel;
                    RestFilter();
                });
                Debug.Log(worldPos);
            }
            Temp(NormalTab,0);
            Temp(EX1Tab,1);
            Temp(EX2Tab,2);
            Temp(EX3Tab,3);
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