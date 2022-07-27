using UnityEngine;
using UnityEngine.UI;
using DummyLayerSystem;
using ModelView;

namespace mainMenu
{
    public class UnitOptionLayer : UILayer
    {
        public DedicatedCameraConnector _connector;
        
        [Space(11)]
        [Header("角色明细T，技能显示T")]
        public RectTransform MemberInfoT;
                
        [Space(7)]
        [Header("mini nineslot")]
        public NineForShow _NineForShow;
        
        [Space(7)]
        [Header("部下详细")]
        public Text focusingCharName;
        public Button SkillShowButton, SkillEditButton;
        
        public static UnitOptionLayer Open()
        {
            var l = UILayerLoader.Get("UnitOptionLayer");
            UnitOptionLayer returnValue;
            if (l != null)
            {
                returnValue = l as UnitOptionLayer;
                return returnValue;
            }
            l = UILayerLoader.Load(PreScene.target.T,"UnitOptionLayer") as UnitOptionLayer;
            returnValue = l as UnitOptionLayer;
            return returnValue;
        }
        
        public static void Close()
        {
            UnitOptionLayer l = (UnitOptionLayer)UILayerLoader.Get("UnitOptionLayer");
            l._connector.Clear();
            UILayerLoader.Remove("UnitOptionLayer");
        }
        
        public void RefreshMemberDetailPageByFocusingChar()
        {
            if (PreScene.target._focusing == null || PreScene.target._focusing.id == null || PreScene.target._focusing.r_id == null)
            {
                SkillShowButton.onClick.RemoveAllListeners();
                SkillEditButton.onClick.RemoveAllListeners();
                MemberInfoT.gameObject.SetActive(false);
                return;
            }
            
            var Ref = Units.GetUnitConfig(PreScene.target._focusing.r_id);
            if (Ref == null)
            {
                Debug.Log("No this monster:" + PreScene.target._focusing.r_id);
                return;
            }
            
            // mini nineslot show
            _NineForShow.ShowStones_Acc(PreScene.target._focusing.id);
            
            MemberInfoT.gameObject.SetActive(true);
            // show按钮功能加载
            SkillShowButton.onClick.RemoveAllListeners();
            void SkillShow()
            {
                if (PreScene.target._focusing.id != null)
                    PreScene.target.trySwitchToStep(MainSceneStep.UnitSkillShow, true);
            }
            SkillShowButton.onClick.AddListener(SkillShow);
            
            // edit按钮功能加载
            SkillEditButton.onClick.RemoveAllListeners();
            void SkillEdit()
            {
                if (PreScene.target._focusing.id != null)
                    PreScene.target.trySwitchToStep(MainSceneStep.UnitSkillEdit, true);
            }
            SkillEditButton.onClick.AddListener(SkillEdit);
            
            // 下面这些都是针对技能显示这个高级功能的，按理说下面这些即便出错，上面的功能也该健全。。即，这些是表现层。
            UnitModelRender(UnitInfo.GetUnitInfo(PreScene.target._focusing));
        }
        
        void UnitModelRender(UnitInfo info)
        {
            if (info == null)
            {
                Debug.Log("角色详细信息读取错误.尝试将“对准”中的角色信息至空");
                _connector.ShowMyModel(null);
            }else
            {
                _connector.ShowMyModel(info.id);
            }
        }
    }
}