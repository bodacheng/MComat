using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemberDetail_edit : MainSceneProcess
{
    public IEnumerator enterProcess()
    {
        this._CameraManager.ClearDrawingLines();
        yield return SkillEditorButtonBehaviour(this._MemberDetail.focusingCharacterDataInfo);
        this._SkillStonesBox.NineAndTwoAndSkillBoxCanvas.gameObject.SetActive(true);
        this._SkillStonesBox.BoxWholeT.gameObject.SetActive(true);
        this._MemberDetail._TheNineSlot.NineSlotT.gameObject.SetActive(true);
        UnityEngine.Events.UnityAction ShowCurrentStones = () =>
        {
            this._preparingScene.triggerMainProcess(SkillEditorButtonBehaviour(this._MemberDetail.focusingCharacterDataInfo));
        };
        this._MemberDetail.SkillEditButton.onClick.AddListener(ShowCurrentStones); 
        
        this._preparingScene._CameraManager.Assign_Camera(Camera_Mode_Num.LockCamera);
        this._preparingScene._CameraManager.current_Camera_Mode.targets = new List<Transform>() { this._preparingScene._MemberDetail.MemDetailWatchPos };
        
        // 表现系
        CharacterResourceInfo _CharacterResourceInfo = CharsManager.getCharacterResourceInfo(this._MemberDetail.focusingCharacterDataInfo.resource_num);
        _SkillStonesBox._SkillStoneBoxTabEffectsManager.switchZokuseiButtons(
            _MemberDetail.ButtonEffectInFxCameraWorldSpace(_preparingScene.fxCamera,_SkillStonesBox.NormalTab.gameObject,5f),
            _MemberDetail.ButtonEffectInFxCameraWorldSpace(_preparingScene.fxCamera,_SkillStonesBox.EX1Tab.gameObject,5f),
            _MemberDetail.ButtonEffectInFxCameraWorldSpace(_preparingScene.fxCamera,_SkillStonesBox.EX2Tab.gameObject,5f),
            _MemberDetail.ButtonEffectInFxCameraWorldSpace(_preparingScene.fxCamera,_SkillStonesBox.EX3Tab.gameObject,5f),
            _CharacterResourceInfo._zokusei);
        yield break;
    }
    
    public MemberDetail_edit(preparingScene _preparingScene)
    {
        this.step = MainSceneStep.MemberDetail_edit;
        this._preparingScene = _preparingScene;
        this.EelementsInherit(_preparingScene);
    }

    public override bool canEnterOtherProcess()
    {
        return true;
    }
    
    public override void ProcessEnter()
    {
        this._preparingScene.triggerMainProcess(enterProcess());
    }
    
    public override void ProcessEnd()
    {
         this._SkillStonesBox._SkillStoneBoxTabEffectsManager.closeShowingZokuseiTagEffects();
    }

    public override void localUpdate()
    {
    }
    
    // 技能编辑画面的进入按钮按下时候的处理。有这样几个逻辑上极其极其重要的点
    // 1. 每次进入一次技能编辑画面，代表技能石头盒子进入了一个针对特定type角色的锁定状态，从而应该只生成一次相应type的石头
    // 2. 除非切换画面，否则石头应该不会再重新生成，进一步说，这次生成石头所进行的石头本地id发配环节(numinbox)也只能进行一次
    // 3. 除非切换画面，生成的石头应该是数量守恒的，如果消耗就消耗，绝不能出现逻辑错误导致的复制情况
    IEnumerator SkillEditorButtonBehaviour(CharacterDataInfo _CharacterDataInfo)
    {
        if (_CharacterDataInfo == null)
        {
            Debug.Log("到达了没道理到达的地方");
            yield break;
        }

        // 关闭整个技能展示用面板和monsterbox，同时打开九宫格画布，根据角色是谁来在九宫格画布上安排技能编辑类功能。
        _MemberDetail._TheNineSlot.readANineAndTwo(_CharacterDataInfo._NineAndTwo,SkillEditMode.AddStoneMode);

        CharacterResourceInfo _CharacterResourceInfo = CharsManager.getCharacterResourceInfo(_CharacterDataInfo.resource_num);
        
        _SkillStonesBox.setFocusingType(_CharacterResourceInfo.type);

        // 下面是EXTabsFeatureRefresh 的唯一调用处，其实确保了一点那就是“每次进入一个角色的技能edit画面时候所有石头都重新生成”
        yield return (_SkillStonesBox.EXTabsFeatureRefresh(_CharacterResourceInfo.type,false));//这一行因为牵扯到对玩家存档中技能石头的读取所以可能是(协程)
        UnityEngine.Events.UnityAction SkillEditConfirm = () =>
        {
            _preparingScene.triggerMainProcess(_TheNineSlot.UpdateEditingNineAndTwoBaseOnSlots(_CharacterDataInfo));
            _preparingScene.triggerPresentationProcess(_MemberDetail.SkillEditConfirmAnimation());
        };

        UnityEngine.Events.UnityAction SkillUpdateValidation = () =>
        {
            _preparingScene._LoadingCanvas.arrangeValiationWindow(SkillEditConfirm, "确实要进行技能更新？");
        };

        _TheNineSlot.ConfirmSkillChangeButton.onClick.RemoveAllListeners();
        _TheNineSlot.ConfirmSkillChangeButton.onClick.AddListener(SkillUpdateValidation);
    }
}
