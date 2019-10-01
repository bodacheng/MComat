using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using dataAccess;
using Api.Dto.Model;

namespace mainMenu
{
    public class MemberDetail : MonoBehaviour
    {
        [Space(11)]
        [Header("modelShower")]
        public modelShower _modelShower;
        
        [Space(7)]
        [Header("美术进程处理器")]
        public SingleThreadProcesser presentationProcessRunner;

        [Space(11)]
        [Header("角色明细T，技能显示T")]
        public RectTransform MemberDetailCanvas;
        public RectTransform MemberInfoT;
        public RectTransform MemberSkillshowT;

        [Space(11)]
        [Header("Essentials")]
        public preparingScene _preparingScene;

        [Space(11)]
        [Header("LevelManager")]
        public LevelManager _LevelManager;

        [Space(7)]
        [Header("SkillStoneManager")]
        public SkillStonesBox _SkillStonesBox;

        [Space(7)]
        [Header("TheNineSlot")]
        public TheNineSlot _TheNineSlot;

        [Space(7)]
        [Header("部下详细")]
        public Text focusingCharName;
        //public UIBulletBar ExpTiao;
        public SkillsPrintOut _SkillsPrintOut;
        public RectTransform SkillShowT;
        public Button SkillShowButton, SkillEditButton;
        public Button favouriteButton;
        public Button sell;
        public InputField selfdefindtag;

        [Space(7)]
        [Header("Positions For Show")]
        public Transform MemDetailTargetPos;
        public Transform MemDetailWatchPos;

        public GetMonsterOfPlayerDetailModel focusingCharacterDataInfo = null;

        public IEnumerator refreshMemberDetailGamenSystemBaseOnFocusingChar()
        {
            if (focusingCharacterDataInfo == null || focusingCharacterDataInfo.monsterOfPlayerId == null || focusingCharacterDataInfo.monsterId == null)
            {
                SkillShowButton.onClick.RemoveAllListeners();
                sell.onClick.RemoveAllListeners();
                favouriteButton.onClick.RemoveAllListeners();
                SkillEditButton.onClick.RemoveAllListeners();
                this.MemberInfoT.gameObject.SetActive(false);
                this._LevelManager.turnOnUI(false);
                Debug.Log(" 出现了一个有残缺的GetMonsterOfPlayerDetailModel对象。这不正常，请改修逻辑 ");
                yield break;
            }

            this.MemberInfoT.gameObject.SetActive(true);
            this._LevelManager.turnOnUI(true);
            this._LevelManager.exButtonFeaturesIni(focusingCharacterDataInfo, 999999);//AccountSet.Instance._PlayerAccountInfo.Coin

            // show按钮功能加载
            SkillShowButton.onClick.RemoveAllListeners();
            UnityEngine.Events.UnityAction step2INI = () =>
            {
                _preparingScene.mainProcessRunner.triggerMainProcess(step2INIForUIRefresh(focusingCharacterDataInfo));
            };
            UnityEngine.Events.UnityAction SkillShow = () =>
            {
                _preparingScene.trySwitchToStep(MainSceneStep.MemberDetail_show, true);
            };
            SkillShowButton.onClick.AddListener(step2INI);
            SkillShowButton.onClick.AddListener(SkillShow);

            // edit按钮功能加载
            SkillEditButton.onClick.RemoveAllListeners();
            UnityEngine.Events.UnityAction SkillEdit = () =>
            {
                this._preparingScene.trySwitchToStep(MainSceneStep.MemberDetail_edit, true);
            };
            SkillEditButton.onClick.AddListener(SkillEdit);

            //UnityEngine.Events.UnityAction exPlusAction = () => { this.EXplus(_choosingAIBeheviourInfo, 100); };
            //expPlus.GetComponentInChildren<Button>().onClick.AddListener(exPlusAction);

            // 自定义tag功能加载
            //selfdefindtag.text = focusingCharacterDataInfo.userd_efined_name;
            //selfdefindtag.onValueChanged.RemoveAllListeners();
            //UnityEngine.Events.UnityAction definemycharactertag = () =>
            //{
            //    focusingCharacterDataInfo.userd_efined_name = this._preparingScene._MemberDetail.selfdefindtag.text;
            //    this._preparingScene.triggerMainProcess(AccountCharsSet.Instance.updateMyCharInfo(focusingCharacterDataInfo));
            //};
            //selfdefindtag.onValueChanged.AddListener(delegate { definemycharactertag(); });
            //UnityEngine.Events.UnityAction sellIt = () =>
            //{
            //    AccountCharsSet.Instance.sellOneChar(focusingCharacterDataInfo.monsterOfPlayerId);
            //    this._preparingScene.triggerMainProcess(AccountCharsSet.Instance.updateMyCharInfo(focusingCharacterDataInfo));
            //    _preparingScene.trySwitchToStep(MainSceneStep.MemberDetail, true);
            //};
            //UnityEngine.Events.UnityAction validation = () =>
            //{
            //    this._preparingScene._LoadingCanvas.arrangeValiationWindow(sellIt, "确实要卖？");
            //};
            //sell.onClick.RemoveAllListeners();
            //sell.onClick.AddListener(validation);

            // 下面这些都是针对技能显示这个高级功能的，按理说下面这些即便出错，上面的功能也该健全。。即，这些是表现层。
            presentationProcessRunner.triggerMainProcess(SkillsPrintOutFocusingCharChangeProcess(RemoteAccess.getCharacterDataInfo(focusingCharacterDataInfo)));
        }

        public IEnumerator SkillsPrintOutFocusingCharChangeProcess(CharacterDataInfo _focusingCharacterDataInfo)
        {
            if (_focusingCharacterDataInfo == null)
            {
                Debug.Log("角色详细信息读取错误.尝试将“对准”中的角色信息至空");
                this._SkillsPrintOut.focusingCharacterData = null;
                IEnumerator readshowmodel = _modelShower.showThisCharacterModel(null);
                yield return readshowmodel;
                yield break;
            }else{
                this._SkillsPrintOut.focusingResourceNum = _focusingCharacterDataInfo.monsterId;
                IEnumerator readshowmodel = _modelShower.showThisCharacterModel(_focusingCharacterDataInfo.monsterOfPlayerId);
                yield return readshowmodel;
                GameObject focusingOneModel = (GameObject)readshowmodel.Current;
                if (focusingOneModel == null)
                {
                    Debug.Log("模型错误");
                    this._SkillsPrintOut.focusingCharacterData = null;
                    yield break;
                }
                OutsideDataLink outsideDataLink = focusingOneModel.GetComponent<OutsideDataLink>();
                if (outsideDataLink == null)
                {
                    Debug.Log("角色模型构成貌似有问题，monsterid：" + _focusingCharacterDataInfo.monsterId);
                    yield break;
                }
                Data_Center aI_DATA_CENTER = outsideDataLink._C;
                this._SkillsPrintOut.focusingCharacterData = aI_DATA_CENTER;
                this._SkillsPrintOut.focusingCharacterData.Animation_Manger.Animator.applyRootMotion = true;
            }
            yield break;
        }

        // 纯表现系
        public IEnumerator SkillEditConfirmAnimation()
        {
            this._SkillStonesBox.SkillBoxCanvas.gameObject.SetActive(false);
            CharacterResourceInfo characterResourceInfo = monstersConfigTable.getCharacterResourceInfo(focusingCharacterDataInfo.monsterId);
            string personalEffectsPath;
            switch (characterResourceInfo._zokusei)
            {
                case zokusei.darkMagic:
                    personalEffectsPath = "darkMagic";
                    break;
                case zokusei.blueMagic:
                    personalEffectsPath = "blueMagic";
                    break;
                case zokusei.greenMagic:
                    personalEffectsPath = "greenMagic";
                    break;
                case zokusei.lightMagic:
                    personalEffectsPath = "lightMagic";
                    break;
                case zokusei.redMagic:
                    personalEffectsPath = "redMagic";
                    break;
                default:
                    personalEffectsPath = "defaultEffects";
                    break;
            }
            EffectAndHurtObjectLoading.Instance.GenerateEffect("skillEditConfirmEffect", personalEffectsPath, caculateShowModelPosition(new Vector3(0.2f, 0.4f, 8)), Quaternion.identity, null);
            yield return new WaitForSeconds(0.1f);
            this._SkillStonesBox.SkillBoxCanvas.gameObject.SetActive(true);
        }

        // 里面一个非常大的重点是执行了BO_Ani_E模块的初始化
        public IEnumerator step2INIForUIRefresh(GetMonsterOfPlayerDetailModel accountCharacterInfo)
        {
            if (accountCharacterInfo != null)
            {
                GameObject focusingOneModel = myModelPool.Instance.getMyModel(accountCharacterInfo.monsterOfPlayerId);
                if (focusingOneModel == null)
                {
                    Debug.Log("模型错误");
                    yield break;
                }
                OutsideDataLink outsideDataLink = focusingOneModel.GetComponent<OutsideDataLink>();
                Data_Center aI_DATA_CENTER = outsideDataLink._C;
                if (aI_DATA_CENTER == null)
                {
                    Debug.Log("角色pretab构成严重错误");
                    yield break;
                }

                CharacterResourceInfo characterResourceInfo = monstersConfigTable.getCharacterResourceInfo(accountCharacterInfo.monsterId);
                CharacterDataInfo characterDataInfo = RemoteAccess.getCharacterDataInfo(accountCharacterInfo);
                yield return (aI_DATA_CENTER.step1Initialize(characterResourceInfo.type, characterResourceInfo.BASIC_MOVEMENT_PACK, characterResourceInfo.SPECIAL_ZOKUSEI));
                yield return (
                    aI_DATA_CENTER.step2Initialize(
                        characterResourceInfo.type, characterDataInfo._NineAndTwo,
                        characterDataInfo._NineAndTwo.level, characterResourceInfo._zokusei, characterResourceInfo.SPECIAL_ZOKUSEI)
                );

                if (aI_DATA_CENTER.AIStateRunner != null)
                    aI_DATA_CENTER.AIStateRunner.changeState("Empty");
            }
            else
                yield break;
        }

        //下面这个函数总是建立在monsterbox函数运行在前，而monsterbox会部署好所有展示用模
        public IEnumerator SetMemberDetailSystemFocusingCharacter(string localID)
        {
            IEnumerator getchar = AccountCharsSet.instance.getAccountCharacterInfo(localID);
            yield return getchar;
            focusingCharacterDataInfo = (GetMonsterOfPlayerDetailModel)getchar.Current;
            yield break;
        }

        Vector3 tempV;
        public Vector3 caculateShowModelPosition(Vector3 screenP)//这个环节要说有什么问题的话，你那个主界面场景怎么确保总是能把射线找到地面呢。。。
        {
            tempV = CameraManager._camera.ViewportToWorldPoint(screenP);
            return tempV;
        }
    }
}