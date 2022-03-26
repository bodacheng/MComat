using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UniRx;

namespace mainMenu
{
    public partial class SkillStonesBox : MonoBehaviour
    {
        [Header("画面主模块parent")]
        [SerializeField] RectTransform BoxT;
        
        [Header("type按钮")]
        [SerializeField] Dropdown types;
        [SerializeField] Button NormalTab;
        [SerializeField] Button EX1Tab;
        [SerializeField] Button EX2Tab;
        [SerializeField] Button EX3Tab;

        [Header("order")] 
        [SerializeField] Button orderBtn;
        
        [Header("type特效管理")]
        public SkillStoneBoxTabEffectsManager _tabEffects;
        
        [Header("攻击范围限定")]
        [SerializeField] Toggle closeCheckBox;
        [SerializeField] Toggle nearCheckBox;
        [SerializeField] Toggle farCheckBox;

        // rare 度限定
        List<int> rares = new List<int> { 0, 1, 2, 3, 4, 5 };
        int focusingExType;
        
        void Awake()
        {
            _Selected = SelectedFrame;
            rares = new List<int> { 0, 1, 2, 3, 4, 5 };//否则其值会被inspector修改
            FocusingType = "human";
            orderBtn.onClick.AddListener(SwitchOrder);
        }

        public string FocusingType
        {
            get;
            set;
        }
        
        int GetFocusingExType()
        {
            return focusingExType;
        }

        public async void IniExTabs(Camera fxCamera)
        {
            void Temp(Button btn, int exLevel)
            {
                var worldPos = PosCal.GetWorldPos(fxCamera, 
                    PosCal.ConvertAnchorPos(btn.GetComponent<RectTransform>().anchoredPosition, Vector2.one, Vector2.zero),
                    5f);
                _tabEffects.RefreshTagEffect(worldPos, exLevel);
                btn.onClick.AddListener(() =>
                {
                    //NormalTabFeature(PosCal.GetWorldPos(fxCamera, btn.GetComponent<RectTransform>(), 3));
                    _tabEffects.SkillButtonExplosion
                        (exLevel, PosCal.GetWorldPos(fxCamera, btn.GetComponent<RectTransform>(), 5f), _tabEffects.transform);
                    focusingExType = exLevel;
                    RestFilter();
                });
            }
            await Observable.TimerFrame(5);
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
                foreach (string Rname in Units.GetTypeList())
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