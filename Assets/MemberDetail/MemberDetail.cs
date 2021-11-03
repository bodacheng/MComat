using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using dataAccess;

namespace mainMenu
{
    public class MemberDetail : MonoBehaviour
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
        public SkillsPrintOut _SkillsPrintOut;
        public Button SkillShowButton, SkillEditButton;
        
        [Space(7)]
        [Header("Positions For Show")]
        public Transform MemDetailTargetPos;
        public Transform MemDetailWatchPos;
        
        public UnitInfo _focusing;
        
        public static MemberDetail target;
        
        void Awake()
        {
            target = this;
        }
        
        public void RefreshMemberDetailPageByFocusingChar()
        {
            if (_focusing == null || _focusing.id == null || _focusing.r_id == null)
            {
                SkillShowButton.onClick.RemoveAllListeners();
                SkillEditButton.onClick.RemoveAllListeners();
                MemberInfoT.gameObject.SetActive(false);
                return;
            }
            
            CharConfig Ref = MonstersConfigTable.GetCharConfig(_focusing.r_id);
            if (Ref == null)
            {
                Debug.Log("No this monster:" + _focusing.r_id);
                return;
            }
            BackGroundPS.target.ChangeBGByZokusei(Ref._zokusei);
            
            // mini nineslot show
            _NineForShow.ShowStones_Acc(_focusing.id);
            
            MemberInfoT.gameObject.SetActive(true);
            // show按钮功能加载
            SkillShowButton.onClick.RemoveAllListeners();
            void step2INI()
            {
                PreScene.target.mainProcessRunner.RunAsQueued(Step2INIForUIRefresh(_focusing));
            }
            void SkillShow()
            {
                if (target._focusing.id != null)
                    PreScene.target.trySwitchToStep(MainSceneStep.UnitSkillShow, true);
            }
            SkillShowButton.onClick.AddListener(step2INI);
            SkillShowButton.onClick.AddListener(SkillShow);
            
            // edit按钮功能加载
            SkillEditButton.onClick.RemoveAllListeners();
            void SkillEdit()
            {
                if (target._focusing.id != null)
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
            presentationProcessRunner.RunAsQueued(CharModelRender(UnitInfo.GetCharDataInfo(_focusing)));
        }
        
        public IEnumerator CharModelRender(UnitInfo info)
        {
            if (info == null)
            {
                Debug.Log("角色详细信息读取错误.尝试将“对准”中的角色信息至空");
                _SkillsPrintOut.focusingC = null;
                IEnumerator readshowmodel = ModelShower.target.ShowMyModel(null);
                yield return readshowmodel;
            }else{
                _SkillsPrintOut.focusCharConfigID = info.r_id;
                IEnumerator readshowmodel = ModelShower.target.ShowMyModel(info.id);
                yield return readshowmodel;
                GameObject focusingOneModel = (GameObject)readshowmodel.Current;
                if (focusingOneModel == null)
                {
                    Debug.Log("模型错误");
                    _SkillsPrintOut.focusingC = null;
                    yield break;
                }
                OutsideDataLink outsideDataLink = focusingOneModel.GetComponent<OutsideDataLink>();
                if (outsideDataLink == null)
                {
                    Debug.Log("角色模型构成貌似有问题，monsterid：" + info.r_id);
                    yield break;
                }
                Data_Center aI_DATA_CENTER = outsideDataLink._C;
                _SkillsPrintOut.focusingC = aI_DATA_CENTER;
                _SkillsPrintOut.focusingC.Animation_Manger.AnimatorRef.applyRootMotion = true;
            }
        }

        // 纯表现系
        public void SkillEditConfirmAnimation()
        {
            CharConfig characterResourceInfo = MonstersConfigTable.GetCharConfig(_focusing.r_id);
            string personalEffectsPath = FightGlobalSetting.EffectPathDefine(characterResourceInfo._zokusei);
            EffectsManager.GenerateEffect("skillEditConfirmEffect", personalEffectsPath, CaculateShowModelPosition(new Vector3(0.2f, 0.4f, 8)), Quaternion.identity, null);
        }

        // 里面一个非常大的重点是执行了BO_Ani_E模块的初始化
        public IEnumerator Step2INIForUIRefresh(UnitInfo accCharInfo)
        {
            if (accCharInfo != null)
            {
                IEnumerator focusingOneModel = GeneralModelPool.GetMyModel(accCharInfo.id);
                yield return focusingOneModel;
                if (focusingOneModel.Current == null)
                {
                    Debug.Log("模型错误");
                    yield break;
                }
                Data_Center aI_DATA_CENTER = (Data_Center)focusingOneModel.Current;
                if (aI_DATA_CENTER == null)
                {
                    Debug.Log("角色pretab构成严重错误");
                    yield break;
                }

                CharConfig characterResourceInfo = MonstersConfigTable.GetCharConfig(accCharInfo.r_id);
                UnitInfo characterDataInfo = UnitInfo.GetCharDataInfo(accCharInfo);
                yield return aI_DATA_CENTER.Step1Initialize(characterResourceInfo.TYPE, characterResourceInfo.BASIC_MOVEMENT_PACK, characterResourceInfo.SPECIAL_ZOKUSEI);
                yield return aI_DATA_CENTER.Step2Initialize(characterResourceInfo.TYPE, characterDataInfo.set, characterResourceInfo._zokusei, characterResourceInfo.SPECIAL_ZOKUSEI);
                
                if (aI_DATA_CENTER._MyBehaviorRunner != null)
                    aI_DATA_CENTER._MyBehaviorRunner.ChangeState("Empty");
            }
            else
                yield break;
        }

        //下面这个函数总是建立在monsterbox函数运行在前，而monsterbox会部署好所有展示用模
        public void SetMemberDetailFocusingChar(string localID)
        {
            _focusing = MyMonsters.Get(localID);
        }

        Vector3 tempV;
        Vector3 CaculateShowModelPosition(Vector3 screenP)//这个环节要说有什么问题的话，你那个主界面场景怎么确保总是能把射线找到地面呢。。。
        {
            tempV = CameraManager._camera.ViewportToWorldPoint(screenP);
            return tempV;
        }        
    }
}