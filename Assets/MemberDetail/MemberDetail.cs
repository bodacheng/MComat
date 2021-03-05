using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using dataAccess;
using Api.Dto.Model;
using System.Collections.Generic;
using System.Linq;

namespace mainMenu
{
    public class MemberDetail : MonoBehaviour
    {        
        [Space(7)]
        [Header("美术进程处理器")]
        public SingleThreadProcesser presentationProcessRunner;
        
        [Space(11)]
        [Header("角色明细T，技能显示T")]
        public RectTransform MemberDetailCanvas;
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
        
        public MonsterOfPlayerDetailModel _focusing;
        
        public static MemberDetail target;
        
        void Awake()
        {
            target = this;
        }
        
        #region 快捷切换角色
        public void SwitchChar()
        {
            List<string> keys = MonsterBox.mainMenuIcons.Keys.ToList();
            int index = keys.IndexOf(_focusing.monsterOfPlayerId);
            index--;
            if (index == -1)
                index = keys.Count - 1;
            string targetMonsterOfPlayerID = keys[index];
            IEnumerator MonsterIconButton()
            {
                yield return target.SetMemberDetailFocusingChar(targetMonsterOfPlayerID);//确立focusing角色
                yield return target.RefreshMemberDetailPageByFocusingChar();
                yield return MemberDetail_edit.SkillShowSpEnterProcess();
            }
            PreScene.target.mainProcessRunner.RunAsQueued(MonsterIconButton());
        }
        #endregion
        
        #region MonsterBoxIconFeature 必须在monsterbox生成所有角色头像之后执行
        public void AddHeroIconFeaturesToMonsterBox()
        {
            foreach (KeyValuePair<string, HeroIcon> keyValuePair in MonsterBox.mainMenuIcons)
            {
                AddHeroIconFeatureToMonsterBox(keyValuePair.Key,keyValuePair.Value.iconButton);
            }
        }
        
        void AddHeroIconFeatureToMonsterBox(string CharLocalId, Button targetButton)
        {
            IEnumerator MonsterIconButton()
            {
                MonsterBox.target.Select(CharLocalId);
                yield return target.SetMemberDetailFocusingChar(CharLocalId);//确立focusing角色
                yield return target.RefreshMemberDetailPageByFocusingChar();
            }
            void Trigger()
            {
                PreScene.target.mainProcessRunner.RunAsQueued(MonsterIconButton());
            }
            targetButton.onClick.AddListener(Trigger);
        }
        
        public void ClearHeroIconsFeatures()
        {
            foreach (KeyValuePair<string, HeroIcon> keyValuePair in MonsterBox.mainMenuIcons)
            {
                keyValuePair.Value.iconButton.onClick.RemoveAllListeners();
            }
        }
        #endregion
        
        public IEnumerator RefreshMemberDetailPageByFocusingChar()
        {
            if (_focusing == null || _focusing.monsterOfPlayerId == null || _focusing.monsterId == null)
            {
                SkillShowButton.onClick.RemoveAllListeners();
                SkillEditButton.onClick.RemoveAllListeners();
                MemberInfoT.gameObject.SetActive(false);
                yield break;
            }
            
            CharConfig Ref = MonstersConfigTable.GetCharConfig(_focusing.monsterId);
            BackGroundPS.target.ChangeBGByZokusei(Ref._zokusei);
            
            // mini nineslot show
            _NineForShow.ShowStones_Acc(_focusing.monsterOfPlayerId);
            
            MemberInfoT.gameObject.SetActive(true);
            // show按钮功能加载
            SkillShowButton.onClick.RemoveAllListeners();
            void step2INI()
            {
                PreScene.target.mainProcessRunner.RunAsQueued(Step2INIForUIRefresh(_focusing));
            }
            void SkillShow()
            {
                if (target._focusing.monsterOfPlayerId != null)
                    PreScene.target.trySwitchToStep(MainSceneStep.MemberDetail_show, true);
            }
            SkillShowButton.onClick.AddListener(step2INI);
            SkillShowButton.onClick.AddListener(SkillShow);
            
            // edit按钮功能加载
            SkillEditButton.onClick.RemoveAllListeners();
            void SkillEdit()
            {
                if (target._focusing.monsterOfPlayerId != null)
                    PreScene.target.trySwitchToStep(MainSceneStep.MemberDetail_edit, true);
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
            presentationProcessRunner.RunAsQueued(CharModelAndSkillRenderProcess(MonsterOfPlayerDetailModel.GetCharDataInfo(_focusing)));
        }
        
        public IEnumerator CharModelAndSkillRenderProcess(CharDataInfo _CharDataInfo)
        {
            if (_CharDataInfo == null)
            {
                Debug.Log("角色详细信息读取错误.尝试将“对准”中的角色信息至空");
                _SkillsPrintOut.focusingC = null;
                IEnumerator readshowmodel = ModelShower.target.ShowMyModel(null);
                yield return readshowmodel;
            }else{
                _SkillsPrintOut.focusCharConfigID = _CharDataInfo.ResourceID;
                IEnumerator readshowmodel = ModelShower.target.ShowMyModel(_CharDataInfo.monsterOfPlayerId);
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
                    Debug.Log("角色模型构成貌似有问题，monsterid：" + _CharDataInfo.ResourceID);
                    yield break;
                }
                Data_Center aI_DATA_CENTER = outsideDataLink._C;
                _SkillsPrintOut.focusingC = aI_DATA_CENTER;
                _SkillsPrintOut.focusingC.Animation_Manger.Animator.applyRootMotion = true;
            }
        }

        // 纯表现系
        public IEnumerator SkillEditConfirmAnimation()
        {
            SkillStonesBox.target.SkillBoxCanvas.gameObject.SetActive(false);
            CharConfig characterResourceInfo = MonstersConfigTable.GetCharConfig(_focusing.monsterId);
            string personalEffectsPath = FightGlobalSetting.EffectPathDefine(characterResourceInfo._zokusei);
            EffectsManager.GenerateEffect("skillEditConfirmEffect", personalEffectsPath, CaculateShowModelPosition(new Vector3(0.2f, 0.4f, 8)), Quaternion.identity, null);
            yield return new WaitForSeconds(0.1f);
            SkillStonesBox.target.SkillBoxCanvas.gameObject.SetActive(true);
        }

        // 里面一个非常大的重点是执行了BO_Ani_E模块的初始化
        public IEnumerator Step2INIForUIRefresh(MonsterOfPlayerDetailModel accountCharacterInfo)
        {
            if (accountCharacterInfo != null)
            {
                IEnumerator focusingOneModel = GeneralModelPool.GetMyModel(accountCharacterInfo.monsterOfPlayerId);
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

                CharConfig characterResourceInfo = MonstersConfigTable.GetCharConfig(accountCharacterInfo.monsterId);
                CharDataInfo characterDataInfo = MonsterOfPlayerDetailModel.GetCharDataInfo(accountCharacterInfo);
                yield return aI_DATA_CENTER.Step1Initialize(characterResourceInfo.TYPE, characterResourceInfo.BASIC_MOVEMENT_PACK, characterResourceInfo.SPECIAL_ZOKUSEI);
                yield return aI_DATA_CENTER.Step2Initialize(characterResourceInfo.TYPE, characterDataInfo._NineAndTwo, characterResourceInfo._zokusei, characterResourceInfo.SPECIAL_ZOKUSEI);
                
                if (aI_DATA_CENTER._MyBehaviorRunner != null)
                    aI_DATA_CENTER._MyBehaviorRunner.ChangeState("Empty");
            }
            else
                yield break;
        }

        //下面这个函数总是建立在monsterbox函数运行在前，而monsterbox会部署好所有展示用模
        public IEnumerator SetMemberDetailFocusingChar(string localID)
        {
            _focusing = AccountCharsSet.Get(localID);
            yield break;
        }

        Vector3 tempV;
        Vector3 CaculateShowModelPosition(Vector3 screenP)//这个环节要说有什么问题的话，你那个主界面场景怎么确保总是能把射线找到地面呢。。。
        {
            tempV = CameraManager._camera.ViewportToWorldPoint(screenP);
            return tempV;
        }        
    }
}