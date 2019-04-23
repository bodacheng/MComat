using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MemberDetail : MonoBehaviour {

    [Space(11)]
    [Header("Essentials")]
    public preparingScene _preparingScene;

    [Space(7)]
    [Header("MonsterBox")]
    public MonsterBox _MonsterBox;

    [Space(7)]
    [Header("SkillStoneManager")]
    public SkillStonesBox _SkillStonesBox;

    [Space(7)]
    [Header("TheNineSlot")]
    public TheNineSlot _TheNineSlot;

	[Space(7)]
    [Header("部下详细")]
    public Text focusingCharName;
    public Button expPlus;
    //public UIBulletBar ExpTiao;
    public Text choosingCharLevel;
    public SkillsPrintOut _SkillsPrintOut;
    public RectTransform SkillShowT;
    public Button SkillShowButton, SkillEditButton;
    public Button favouriteButton;
    public Button sell;
    public InputField selfdefindtag;
    
    [Space(7)]
    [Header("Positions For Show")]
    public Transform MemDetailWatchPos;

    [Space(7)]
    [Header("focusingCharacterDataInfo 平常是null，无视它")]
    public CharacterDataInfo focusingCharacterDataInfo;   
    public GameObject showingChar;    
    
    public IEnumerator showThisCharacterModel(int localID)
    {
        GameObject _char = myModelPool.Instance.getMyModel(localID);
        if (_char == null)
        {
            yield return (this._preparingScene._CharSetManager.buildShowModel(AccountCharsSet.getTheCharacterOfMine(localID)));
            _char = myModelPool.Instance.getMyModel(localID);
        }
        if (showingChar != _char)
        {
            if (showingChar != null)
                showingChar.SetActive(false);
            this.showingChar = _char;
            if (this.showingChar != null)
            {
                this.showingChar.SetActive(true);
                this.showingChar.transform.parent = null;
                this.showingChar.transform.position = this.caculateShowModelPosition(new Vector3(0.2f, 0.4f, 8));//右
                this.showingChar.transform.localRotation = Quaternion.identity;
            }else{
                Debug.Log("展示用模型加载严重错误");
            }
        }
        yield return _char;
    }
    
    public IEnumerator refreshMemberDetailGamenSystemBaseOnFocusingChar()
    {
        if (focusingCharacterDataInfo == null)
        {
            SkillShowButton.onClick.RemoveAllListeners();
            //UnityEngine.Events.UnityAction exPlusAction = () => { this.EXplus(_choosingAIBeheviourInfo, 100); };
            //expPlus.GetComponentInChildren<Button>().onClick.AddListener(exPlusAction);
            sell.onClick.RemoveAllListeners();
            favouriteButton.onClick.RemoveAllListeners();
            SkillEditButton.onClick.RemoveAllListeners();
            yield break;
        }

        // show按钮功能加载
        SkillShowButton.onClick.RemoveAllListeners();
        UnityEngine.Events.UnityAction step2INI = () => 
        {
            StartCoroutine(step2INIForUIRefresh()); 
        };
        UnityEngine.Events.UnityAction SkillShow = () =>
        {
            _preparingScene.trySwitchToStep(MainSceneStep.MemberDetail_show,true);
        };
        SkillShowButton.onClick.AddListener(step2INI);       
        SkillShowButton.onClick.AddListener(SkillShow);
        
        // edit按钮功能加载
        SkillEditButton.onClick.RemoveAllListeners();
        UnityEngine.Events.UnityAction SkillEdit = () =>
        {
            this._preparingScene.trySwitchToStep(MainSceneStep.MemberDetail_edit,true);
        };
        SkillEditButton.onClick.AddListener(SkillEdit); 
        
        //UnityEngine.Events.UnityAction exPlusAction = () => { this.EXplus(_choosingAIBeheviourInfo, 100); };
        //expPlus.GetComponentInChildren<Button>().onClick.AddListener(exPlusAction);

        // 自定义tag功能加载
        selfdefindtag.text = focusingCharacterDataInfo.userd_efined_name;
        selfdefindtag.onValueChanged.RemoveAllListeners();
        UnityEngine.Events.UnityAction definemycharactertag = () =>
        {
            focusingCharacterDataInfo.userd_efined_name = this._preparingScene._MemberDetail.selfdefindtag.text;
            this._preparingScene.StartCoroutine(AccountCharsSet.Instance.overrideMyCharsInfo());
        };
        selfdefindtag.onValueChanged.AddListener(delegate { definemycharactertag();});
        
        // 卖角色按钮功能加载
        if (!focusingCharacterDataInfo.favorite)
        {
            UnityEngine.Events.UnityAction sellIt = () => 
            {
                AccountCharsSet.Instance.sellOneChar(focusingCharacterDataInfo.localID);
                StartCoroutine(AccountCharsSet.Instance.overrideMyCharsInfo());
                _preparingScene.trySwitchToStep(MainSceneStep.MemberDetail,true);
            };
            UnityEngine.Events.UnityAction validation = () =>
            {
                this._preparingScene._LoadingCanvas.arrangeValiationWindow(sellIt, "确实要卖？");
            };
            sell.onClick.RemoveAllListeners();
            sell.onClick.AddListener(validation);
        }
        
        // like按钮功能加载
        favouriteButton.onClick.RemoveAllListeners();
        Color tochange = favouriteButton.GetComponent<Image>().color;
        if (focusingCharacterDataInfo.favorite)
            favouriteButton.GetComponent<Image>().color = new Color(tochange.r, tochange.g, tochange.b, 1f);
        else
            favouriteButton.GetComponent<Image>().color = new Color(tochange.r, tochange.g, tochange.b, 0.5f);

        UnityEngine.Events.UnityAction setAsFavourite = () =>
        {
            focusingCharacterDataInfo.favorite = !focusingCharacterDataInfo.favorite;
            Color _C = favouriteButton.GetComponent<Image>().color;
            if (focusingCharacterDataInfo.favorite)
                favouriteButton.GetComponent<Image>().color = new Color(_C.r, _C.g, _C.b, 1f);
            else
                favouriteButton.GetComponent<Image>().color = new Color(_C.r, _C.g, _C.b, 0.5f);
            
            StartCoroutine(AccountCharsSet.Instance.overrideMyCharsInfo());
        };
        favouriteButton.onClick.AddListener(setAsFavourite);

        // 下面这些都是针对技能显示这个高级功能的，按理说下面这些即便出错，上面的功能也该健全
        this._SkillsPrintOut.focusingResourceNum = focusingCharacterDataInfo.resource_num;
        IEnumerator readshowmodel = showThisCharacterModel(focusingCharacterDataInfo.localID);
        yield return readshowmodel;
        GameObject focusingOneModel = (GameObject)readshowmodel.Current;
        if (focusingOneModel == null)
        {
            Debug.Log("模型错误");
            this._SkillsPrintOut._FocusingAnimationManger = null;
            yield break;
        }
        AI_DATA_CENTER aI_DATA_CENTER = focusingOneModel.GetComponent<AI_DATA_CENTER>();
        this._SkillsPrintOut._FocusingAnimationManger = aI_DATA_CENTER.Animation_Manger;
        this._SkillsPrintOut._FocusingAnimationManger.Animator.applyRootMotion = true;
    }

    void Update()
    {
        if (showingChar != null && _SkillsPrintOut._CameraManager.current_Camera_Mode_Num == Camera_Mode_Num.LockCamera)
        {
            showingChar.transform.position = caculateShowModelPosition(new Vector3(0.2f, 0.4f, 8));//右
        }
    }
    
    Vector2 buttonAnchorPosition;
    Vector2 true_buttonAnchorPosition;
    Vector3 buttonWorldPosition;
    public Vector3 ButtonEffectInFxCameraWorldSpace(Camera fxcamera,GameObject UI_thing,float z_offset)//这个函数是以攻击钮与防御，闪避钮在右下角为前提写的。
    {
        buttonAnchorPosition = UI_thing.GetComponent<RectTransform>().anchoredPosition;
        true_buttonAnchorPosition = new Vector2(Screen.width + buttonAnchorPosition.x,buttonAnchorPosition.y);
        buttonWorldPosition = fxcamera.ScreenToWorldPoint(true_buttonAnchorPosition);
        buttonWorldPosition = new Vector3(buttonWorldPosition.x,buttonWorldPosition.y,fxcamera.transform.position.z + z_offset);
        return buttonWorldPosition;
    }

    // 纯表现系
    public IEnumerator SkillEditConfirmAnimation()
    {
        _TheNineSlot.NineAndTwoAndSkillBoxCanvas.gameObject.SetActive(false);

        CharacterResourceInfo characterResourceInfo = CharsManager.getCharacterResourceInfo(focusingCharacterDataInfo.resource_num);
        
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
        defaultPools.Instance.GenerateEffect("skillEditConfirmEffect", personalEffectsPath, caculateShowModelPosition(new Vector3(0.2f, 0.4f, 8)), Quaternion.identity,null);
        yield return new WaitForSeconds(0.1f);
        _TheNineSlot.NineAndTwoAndSkillBoxCanvas.gameObject.SetActive(true);
    }

    // 里面一个非常大的重点是执行了BO_Ani_E模块的初始化
    public IEnumerator step2INIForUIRefresh()
    {
        if (this.focusingCharacterDataInfo != null && focusingCharacterDataInfo._NineAndTwo != null)
        {
            GameObject focusingOneModel = myModelPool.Instance.getMyModel(focusingCharacterDataInfo.localID);
            if (focusingOneModel == null)
            {
                Debug.Log("模型错误");
                yield break;
            }

            AI_DATA_CENTER aI_DATA_CENTER = focusingOneModel.GetComponent<AI_DATA_CENTER>();
            if (aI_DATA_CENTER == null)
            {
                Debug.Log("角色pretab构成严重错误");
                yield break;
            }
            
            CharacterResourceInfo characterResourceInfo = CharsManager.getCharacterResourceInfo(focusingCharacterDataInfo.resource_num);

            yield return (aI_DATA_CENTER.step1Initialize(characterResourceInfo.type, characterResourceInfo.BasicMoveSetName,characterResourceInfo.personalMagicPack));
            yield return (
                aI_DATA_CENTER.step2Initialize(
                    characterResourceInfo.type, focusingCharacterDataInfo._NineAndTwo, 
                    characterResourceInfo.getPassiveSkillConfigs(),
                    focusingCharacterDataInfo._NineAndTwo.level, characterResourceInfo._zokusei, characterResourceInfo.personalMagicPack)
            );

            if (aI_DATA_CENTER.getRunner() != null)
                aI_DATA_CENTER.getRunner().changeState("Empty");
        }
        else
            yield break;
    }

    void ExpTiaoRefresh(CharacterDataInfo _CharacterDataInfo)
    {
        this.choosingCharLevel.text = _CharacterDataInfo.level.ToString();
        // ExpTiao.fillAmount =  还不知道怎么算，先不管。
    }

    //public bool EXplus(AIBeheviourInfo _info, int plus)
    //{
    //    if (TeamSet.Instance.localCustomerInfo.IntelliCoin >= plus)
    //    {
    //        TeamSet.Instance.localCustomerInfo.plusIntelliCoin(-plus);
    //        _info.ExpPlus(plus);
    //        ExpTiaoRefresh(_info);
    //        return true;
    //    }
    //    else
    //    {
    //        Debug.Log("账户里智慧果实不足");
    //        return false;
    //    }
    //}

    //下面这个函数总是建立在monsterbox函数运行在前，而monsterbox会部署好所有展示用模
    public void SetMemberDetailSystemFocusingCharacter(int localID)
    {
        focusingCharacterDataInfo = AccountCharsSet.getTheCharacterOfMine(localID);
    }

    Vector3 tempV;
    public Vector3 caculateShowModelPosition(Vector3 screenP)//这个环节要说有什么问题的话，你那个主界面场景怎么确保总是能把射线找到地面呢。。。
    {
        tempV = CameraManager._camera.ViewportToWorldPoint(screenP);
        return tempV;
    }
}
