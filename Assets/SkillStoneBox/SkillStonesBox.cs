using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// 11.13号思考这样几个问题：
// 1.玩家的等级与CellLimit之间的制约关系怎么实现
// 2.从数据库阅读拥有技能石的函数在哪
// 3.当石头的数量超过了格子数量时候所进行的validation在哪。
// 4.有财产类的安全隐患吗。
// 18.1.6
// 这个模块缺乏这些函数：添加新技能石头(与技能石头盒子的画面配合？)
// 消耗某技能石头

namespace mainMenu
{
    public partial class SkillStonesBox : MonoBehaviour
    {
        [Space(5)]
        [Header("进程器")]
        public SingleThreadProcesser mainProcessRunner;
        
        [Header("画面主模块parent")]
        public RectTransform SkillBoxCanvas;
        public RectTransform BoxWholeT, BoxT, stonesTempContainer;
        
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
        [Header("技能石头删除区域")]
        public StoneCell DeleteArea;
        
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
        
        [Header("DeleteManger")]
        public StoneDeleteManger _StoneDeleteManger;
        
        string focusingtype = "human";
        int focusingExType;
        SkillStoneSlot DeleteSkillStoneSlot;
        static RectTransform _stonesTempContainer;
        
        public static SkillStonesBox target;
        
        void Awake()
        {
            _Selected = SelectedFrame;
            _stonesTempContainer = stonesTempContainer;
        }
        
        public IEnumerator StartUp(int stoneboxsize)
        {
            DeleteArea.cellPhase = StoneCell.CellPhase.DeleteArea;
            DeleteSkillStoneSlot = new SkillStoneSlot(-1, null, DeleteArea);
            Debug.Log("技能石盒子容量为" + stoneboxsize);
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
            _SkillStoneBoxTabEffectsManager.SkillButtonExplosion(0, ButtonEffectInFxCameraWorldSpace(fxCamera,self, 3));
            focusingExType = 0;
            TheNineSlot.Instance.mainProcessRunner.Run(ArrangeSkillStonesToBox());
        }
        
        // Button feature
        public void EX1TabFeature(GameObject self)
        {
            _SkillStoneBoxTabEffectsManager.SkillButtonExplosion(1, ButtonEffectInFxCameraWorldSpace(fxCamera,self, 3));
            focusingExType = 1;
            TheNineSlot.Instance.mainProcessRunner.Run(ArrangeSkillStonesToBox());
        }
        
        // Button feature
        public void EX2TabFeature(GameObject self)
        {
            _SkillStoneBoxTabEffectsManager.SkillButtonExplosion(2, ButtonEffectInFxCameraWorldSpace(fxCamera,self, 3));
            focusingExType = 2;
            TheNineSlot.Instance.mainProcessRunner.Run(ArrangeSkillStonesToBox());
        }
        
        // Button feature
        public void EX3TabFeature(GameObject self)
        {
            _SkillStoneBoxTabEffectsManager.SkillButtonExplosion(3, ButtonEffectInFxCameraWorldSpace(fxCamera,self, 3));
            focusingExType = 3;
            TheNineSlot.Instance.mainProcessRunner.Run(ArrangeSkillStonesToBox());
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
            TheNineSlot.Instance.mainProcessRunner.Run(ArrangeSkillStonesToBox());
        }

        public void TypeDropDownBehaviour()// 直接放在type下拉按钮上的功能
        {
            string targetType = types.options[types.value].text.Clone() as string;
            TheNineSlot.Instance.mainProcessRunner.Run(EXTabsFeatureRefresh(true));
        }
        
        Vector2 buttonAnchorPosition;
        Vector2 true_buttonAnchorPosition;
        Vector3 buttonWorldPosition;
        readonly int worldSpaceConvertMode = 1;// 1: canvas screen space 2: UI元素在左下角？忘了
        public Vector3 ButtonEffectInFxCameraWorldSpace(Camera fxcamera, GameObject UI_thing, float z_offset)
        {
            switch (worldSpaceConvertMode)
            {
                case 1:
                    buttonWorldPosition = UI_thing.transform.position;
                    buttonWorldPosition = new Vector3(buttonWorldPosition.x, buttonWorldPosition.y, buttonWorldPosition.z + z_offset);
                break;
                case 2:
                    buttonAnchorPosition = UI_thing.GetComponent<RectTransform>().transform.position;
                    true_buttonAnchorPosition = new Vector2(buttonAnchorPosition.x, buttonAnchorPosition.y);
                    buttonWorldPosition = fxcamera.ScreenToWorldPoint(true_buttonAnchorPosition);
                    buttonWorldPosition = new Vector3(buttonWorldPosition.x, buttonWorldPosition.y, fxcamera.transform.position.z + z_offset);
                break;
            }
            return buttonWorldPosition;
        }
    }
}