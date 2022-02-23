using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using dataAccess;
using DummyLayerSystem;

namespace mainMenu
{
    public class UnitOptionLayer : UILayer
    {
        [Space(7)]
        [Header("美术进程处理器")]
        public SingleThreadProcesser presentationProcessRunner;
        
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
        
        //public static UnitOptionLayer target;
        
        void Awake()
        {
            //target = this;
        }
        
        public static UnitOptionLayer Open()
        {
            UILayer l = UILayerLoader.Get("UnitOptionLayer");
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
            
            UnitConfig Ref = Units.GetUnitConfig(PreScene.target._focusing.r_id);
            if (Ref == null)
            {
                Debug.Log("No this monster:" + PreScene.target._focusing.r_id);
                return;
            }
            BackGroundPS.target.ChangeBGByZokusei(Ref._zokusei);
            
            // mini nineslot show
            _NineForShow.ShowStones_Acc(PreScene.target._focusing.id);
            
            MemberInfoT.gameObject.SetActive(true);
            // show按钮功能加载
            SkillShowButton.onClick.RemoveAllListeners();
            void step2INI()
            {
                PreScene.target.mainProcessRunner.RunAsQueued(Step2INIForUIRefresh(PreScene.target._focusing));
            }
            void SkillShow()
            {
                if (PreScene.target._focusing.id != null)
                    PreScene.target.trySwitchToStep(MainSceneStep.UnitSkillShow, true);
            }
            SkillShowButton.onClick.AddListener(step2INI);
            SkillShowButton.onClick.AddListener(SkillShow);
            
            // edit按钮功能加载
            SkillEditButton.onClick.RemoveAllListeners();
            void SkillEdit()
            {
                if (PreScene.target._focusing.id != null)
                    PreScene.target.trySwitchToStep(MainSceneStep.UnitSkillEdit, true);
            }
            SkillEditButton.onClick.AddListener(SkillEdit);

            // 自定义tag功能加载
            //selfdefindtag.text = focusingCharacterDataInfo.userd_efined_name;
            //selfdefindtag.onValueChanged.RemoveAllListeners();
            //UnityEngine.Events.UnityAction definemycharactertag = () =>
            //{
            //    focusingCharacterDataInfo.userd_efined_name = this._preparingScene._MemberDetail.selfdefindtag.text;
            //    this._preparingScene.triggerMainProcess(AccountCharsSet.Instance.updateMyCharInfo(focusingCharacterDataInfo));
            //};
            //selfdefindtag.onValueChanged.AddListener(delegate { definemycharactertag(); });

            // 下面这些都是针对技能显示这个高级功能的，按理说下面这些即便出错，上面的功能也该健全。。即，这些是表现层。
            presentationProcessRunner.RunAsQueued(UnitModelRender(UnitInfo.GetUnitInfo(PreScene.target._focusing)));
        }
        
        IEnumerator UnitModelRender(UnitInfo info)
        {
            if (info == null)
            {
                Debug.Log("角色详细信息读取错误.尝试将“对准”中的角色信息至空");
                SkillShowSupporter.focusingC = null;
                yield return ModelShower.target.ShowMyModel(null);
            }else{
                SkillShowSupporter.focusRId = info.r_id;
                var showMyModel = ModelShower.target.ShowMyModel(info.id);
                yield return showMyModel;
                var focusingOneModel = (GameObject)showMyModel.Current;
                if (focusingOneModel == null)
                {
                    Debug.Log("模型错误");
                    SkillShowSupporter.focusingC = null;
                    yield break;
                }
                var outsideDataLink = focusingOneModel.GetComponent<OutsideDataLink>();
                if (outsideDataLink == null)
                {
                    Debug.Log("角色模型构成貌似有问题，resource：" + info.r_id);
                    yield break;
                }
                var aI_DATA_CENTER = outsideDataLink._C;
                SkillShowSupporter.focusingC = aI_DATA_CENTER;
                SkillShowSupporter.focusingC.Animation_Manger.AnimatorRef.applyRootMotion = true;
            }
        }
        
        // 里面一个非常大的重点是执行了BO_Ani_E模块的初始化
        IEnumerator Step2INIForUIRefresh(UnitInfo info)
        {
            if (info != null)
            {
                var focusingOneModel = GeneralModelPool.GetMyModel(info.id);
                yield return focusingOneModel;
                if (focusingOneModel.Current == null)
                {
                    Debug.Log("模型错误");
                    yield break;
                }
                var center = (Data_Center)focusingOneModel.Current;
                if (center == null)
                {
                    Debug.Log("角色prefab构成严重错误");
                    yield break;
                }

                var config = Units.GetUnitConfig(info.r_id);
                var unitInfo = UnitInfo.GetUnitInfo(info);
                yield return center.Step1Initialize(config.TYPE, config.BASIC_MOVEMENT_PACK, config.SPECIAL_ZOKUSEI);
                yield return center.Step2Initialize(config.TYPE, unitInfo.set, unitInfo.level, config._zokusei, config.SPECIAL_ZOKUSEI);
                
                if (center._MyBehaviorRunner != null)
                    center._MyBehaviorRunner.ChangeState("Empty");
            }
        }
        
        Vector3 tempV;
        Vector3 CaculateShowModelPosition(Vector3 screenP)//这个环节要说有什么问题的话，你那个主界面场景怎么确保总是能把射线找到地面呢。。。
        {
            tempV = CameraManager._camera.ViewportToWorldPoint(screenP);
            return tempV;
        }
    }
}