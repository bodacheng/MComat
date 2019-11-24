using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using mainMenu;
using dataAccess;
using Api.Dto.Model;

// 先试着把石头添加到一个格子上。
public class TryEditNineSlot : MainSceneProcess
{
    public TryEditNineSlot(preparingScene _preparingScene,ProcessesRunner processesRunner)
    {
        this.thisProcessStep = MainSceneStep.Tutorial_skillEdit_sub3;
        this.nextProcessStep = MainSceneStep.none;
        
        this.processesRunner = processesRunner;
        this._preparingScene = _preparingScene;
        this.EelementsInherit(_preparingScene);
    }

    public override bool CanEnterOtherProcess()
    {
        return false;
    }
    
    public override void ProcessEnter()
    {
        this.mainProcessRunner.triggerMainProcess(enterProcess());
    }
    
    public override void ProcessEnd()
    {
    }

    public override void LocalUpdate()
    {
        if (this._TheNineSlot.A1DragAndDropCell.gameObject.transform.GetComponentInChildren<DragAndDropItem>() && 
                this._TheNineSlot.A2DragAndDropCell.gameObject.transform.GetComponentInChildren<DragAndDropItem>() && 
                    this._TheNineSlot.A3DragAndDropCell.gameObject.transform.GetComponentInChildren<DragAndDropItem>() &&
                        this._TheNineSlot.B1DragAndDropCell.gameObject.transform.GetComponentInChildren<DragAndDropItem>() && 
                            this._TheNineSlot.B2DragAndDropCell.gameObject.transform.GetComponentInChildren<DragAndDropItem>() && 
                                this._TheNineSlot.B3DragAndDropCell.gameObject.transform.GetComponentInChildren<DragAndDropItem>() &&
                                    this._TheNineSlot.C1DragAndDropCell.gameObject.transform.GetComponentInChildren<DragAndDropItem>() && 
                                        this._TheNineSlot.C2DragAndDropCell.gameObject.transform.GetComponentInChildren<DragAndDropItem>() && 
                                            this._TheNineSlot.C3DragAndDropCell.gameObject.transform.GetComponentInChildren<DragAndDropItem>())
        {
            this._TheNineSlot.ConfirmSkillChangeButton.gameObject.SetActive(true);
        }
    }
    
    public IEnumerator enterProcess()
    {
        this._SkillStonesBox.SkillBoxCanvas.gameObject.SetActive(true);
        this._TheNineSlot.NineSlotT.gameObject.SetActive(true);
        this._SkillStonesBox.BoxWholeT.gameObject.SetActive(true);
        
        IEnumerator getchar = AccountCharsSet.instance.GetAccountCharacterInfo("1");
        yield return getchar;
        this._MemberDetail.focusingCharacterDataInfo = (GetMonsterOfPlayerDetailModel)getchar.Current;
        yield return SkillEditorButtonBehaviour(this._MemberDetail.focusingCharacterDataInfo);//比如亚当在这个版本的角色存档里localid是1。。。

        // Tutorial 模式那两按钮不需要显示
        this._MemberDetail.MemberDetailCanvas.gameObject.SetActive(false);
        this._CameraManager.Assign_StartToEndModeCamera(this._MemberDetail.MemDetailWatchPos.position, 3f,15f);
        this._CameraManager.current_Camera_Mode.target = this._MemberDetail.MemDetailTargetPos;
        
        // 表现系
        CharacterResourceInfo _CharacterResourceInfo = monstersConfigTable.getCharacterResourceInfo(this._MemberDetail.focusingCharacterDataInfo.monsterId);
        _SkillStonesBox._SkillStoneBoxTabEffectsManager.SwitchZokuseiButtons(
            this._SkillStonesBox.ButtonEffectInFxCameraWorldSpace(_SkillStonesBox.fxCamera,_SkillStonesBox.NormalTab.gameObject,5f),
            this._SkillStonesBox.ButtonEffectInFxCameraWorldSpace(_SkillStonesBox.fxCamera,_SkillStonesBox.EX1Tab.gameObject,5f),
            this._SkillStonesBox.ButtonEffectInFxCameraWorldSpace(_SkillStonesBox.fxCamera,_SkillStonesBox.EX2Tab.gameObject,5f),
            this._SkillStonesBox.ButtonEffectInFxCameraWorldSpace(_SkillStonesBox.fxCamera,_SkillStonesBox.EX3Tab.gameObject,5f),_CharacterResourceInfo._zokusei);
        yield return refreshMemberDetailGamenSystemBaseOnFocusingCharTutorailVersion();
    }
    
    //里面的因数，是剧情人物“亚当”的角色信息。
    IEnumerator SkillEditorButtonBehaviour(GetMonsterOfPlayerDetailModel _CharacterDataInfo)
    {
        if (_CharacterDataInfo == null)
        {
            Debug.Log("没有找到亚当的信息？程序严重错乱");
            yield break;
        }
        yield return _TheNineSlot.readANineAndTwo(_CharacterDataInfo);
        this._TheNineSlot.ConfirmSkillChangeButton.gameObject.SetActive(false);
        this._TheNineSlot.A1DragAndDropCell.gameObject.SetActive(true);
        this._TheNineSlot.A2DragAndDropCell.gameObject.SetActive(true);
        this._TheNineSlot.A3DragAndDropCell.gameObject.SetActive(true);
        this._TheNineSlot.B1DragAndDropCell.gameObject.SetActive(true);
        this._TheNineSlot.B2DragAndDropCell.gameObject.SetActive(true);
        this._TheNineSlot.B3DragAndDropCell.gameObject.SetActive(true);
        this._TheNineSlot.C1DragAndDropCell.gameObject.SetActive(true);
        this._TheNineSlot.C2DragAndDropCell.gameObject.SetActive(true);
        this._TheNineSlot.C3DragAndDropCell.gameObject.SetActive(true);
        
        CharacterResourceInfo _CharacterResourceInfo = monstersConfigTable.getCharacterResourceInfo(_CharacterDataInfo.monsterId);
        _SkillStonesBox.SetFocusingType(_CharacterResourceInfo.type);
        yield return (_SkillStonesBox.EXTabsFeatureRefresh(_CharacterResourceInfo.type,false));
        UnityEngine.Events.UnityAction SkillEditConfirm = () =>//这里可能还有一个执行内容，就是进入到测试战斗场景。
        {
            mainProcessRunner.triggerMainProcess(_TheNineSlot.UpdateEditingNineAndTwoBaseOnSlots(_CharacterDataInfo));
            _MemberDetail.presentationProcessRunner.triggerMainProcess(_MemberDetail.SkillEditConfirmAnimation());
            this.processesRunner.changeProcess(MainSceneStep.Tutorial_skillEdit_sub4);
        };

        UnityEngine.Events.UnityAction SkillUpdateValidation = () =>
        {
            _preparingScene._LoadingCanvas.arrangeValiationWindow(SkillEditConfirm, "确实要进行技能更新？");
        };
        _TheNineSlot.ConfirmSkillChangeButton.onClick.RemoveAllListeners();
        _TheNineSlot.ConfirmSkillChangeButton.onClick.AddListener(SkillUpdateValidation);               
    }
    
    public IEnumerator refreshMemberDetailGamenSystemBaseOnFocusingCharTutorailVersion()
    {        
        // 下面这些都是针对技能显示这个高级功能的，按理说下面这些即便出错，上面的功能也该健全。。即，这些是表现层。
        IEnumerator getchar = AccountCharsSet.instance.GetAccountCharacterInfo("1");
        yield return getchar;
        GetMonsterOfPlayerDetailModel focusingCharacterDataInfo = (GetMonsterOfPlayerDetailModel)getchar.Current;
        CharacterDataInfo characterDataInfo = RemoteAccess.getCharacterDataInfo(focusingCharacterDataInfo);
        _MemberDetail.presentationProcessRunner.triggerMainProcess(this._MemberDetail.SkillsPrintOutFocusingCharChangeProcess(characterDataInfo));
        yield break;
    }
}
