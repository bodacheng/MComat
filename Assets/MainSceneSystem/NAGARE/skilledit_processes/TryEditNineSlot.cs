using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using mainMenu;
using dataAccess;
using Api.Dto.Model;

// 先试着把石头添加到一个格子上。
public class TryEditNineSlot : MainSceneProcess
{
    public TryEditNineSlot(ProcessesRunner processesRunner)
    {
        this.thisProcessStep = MainSceneStep.Tutorial_skillEdit_sub3;
        this.nextProcessStep = MainSceneStep.none;
        
        this.subProcessesRunner = processesRunner;
        this.EelementsInherit(PreScene.Instance);
    }

    public override bool CanEnterOtherProcess()
    {
        return false;
    }
    
    public override void ProcessEnter()
    {
        mainProcessRunner.TriggerMainProcess(EnterProcess());
    }
    
    public override void ProcessEnd()
    {
    }

    public override void LocalUpdate()
    {
        if (TheNineSlot.Instance.A1DragAndDropCell.gameObject.transform.GetComponentInChildren<DragAndDropItem>() && 
                TheNineSlot.Instance.A2DragAndDropCell.gameObject.transform.GetComponentInChildren<DragAndDropItem>() && 
                    TheNineSlot.Instance.A3DragAndDropCell.gameObject.transform.GetComponentInChildren<DragAndDropItem>() &&
                        TheNineSlot.Instance.B1DragAndDropCell.gameObject.transform.GetComponentInChildren<DragAndDropItem>() && 
                            TheNineSlot.Instance.B2DragAndDropCell.gameObject.transform.GetComponentInChildren<DragAndDropItem>() && 
                                TheNineSlot.Instance.B3DragAndDropCell.gameObject.transform.GetComponentInChildren<DragAndDropItem>() &&
                                    TheNineSlot.Instance.C1DragAndDropCell.gameObject.transform.GetComponentInChildren<DragAndDropItem>() && 
                                        TheNineSlot.Instance.C2DragAndDropCell.gameObject.transform.GetComponentInChildren<DragAndDropItem>() && 
                                            TheNineSlot.Instance.C3DragAndDropCell.gameObject.transform.GetComponentInChildren<DragAndDropItem>())
        {
            TheNineSlot.Instance.ConfirmSkillChangeButton.gameObject.SetActive(true);
        }
    }
    
    public IEnumerator EnterProcess()
    {
        SkillStonesBox.Instance.SkillBoxCanvas.gameObject.SetActive(true);
        TheNineSlot.Instance.NineSlotT.gameObject.SetActive(true);
        SkillStonesBox.Instance.BoxWholeT.gameObject.SetActive(true);
        
        IEnumerator getchar = AccountCharsSet.instance.GetAccountCharInfo("1");
        yield return getchar;
        this._MemberDetail.focusingCharacterDataInfo = (GetMonsterOfPlayerDetailModel)getchar.Current;
        yield return SkillEditorButtonBehaviour(this._MemberDetail.focusingCharacterDataInfo);//比如亚当在这个版本的角色存档里localid是1。。。

        // Tutorial 模式那两按钮不需要显示
        this._MemberDetail.MemberDetailCanvas.gameObject.SetActive(false);
        this._CameraManager.Assign_StartToEndModeCamera(this._MemberDetail.MemDetailWatchPos.position, 3f,15f);
        this._CameraManager.current_Camera_Mode.target = this._MemberDetail.MemDetailTargetPos;
        
        // 表现系
        CharConfig _CharacterResourceInfo = MonstersConfigTable.GetCharacterResourceInfo(this._MemberDetail.focusingCharacterDataInfo.monsterId);
        SkillStonesBox.Instance._SkillStoneBoxTabEffectsManager.SwitchZokuseiButtons(
            SkillStonesBox.Instance.ButtonEffectInFxCameraWorldSpace(SkillStonesBox.Instance.fxCamera,SkillStonesBox.Instance.NormalTab.gameObject,5f),
            SkillStonesBox.Instance.ButtonEffectInFxCameraWorldSpace(SkillStonesBox.Instance.fxCamera,SkillStonesBox.Instance.EX1Tab.gameObject,5f),
            SkillStonesBox.Instance.ButtonEffectInFxCameraWorldSpace(SkillStonesBox.Instance.fxCamera,SkillStonesBox.Instance.EX2Tab.gameObject,5f),
            SkillStonesBox.Instance.ButtonEffectInFxCameraWorldSpace(SkillStonesBox.Instance.fxCamera,SkillStonesBox.Instance.EX3Tab.gameObject,5f),_CharacterResourceInfo._zokusei);
        yield return RefreshMemberDetailGamenSystemBaseOnFocusingCharTutorailVersion();
    }
    
    //里面的因数，是剧情人物“亚当”的角色信息。
    IEnumerator SkillEditorButtonBehaviour(GetMonsterOfPlayerDetailModel _CharacterDataInfo)
    {
        if (_CharacterDataInfo == null)
        {
            Debug.Log("没有找到亚当的信息？程序严重错乱");
            yield break;
        }
        yield return TheNineSlot.Instance.ReadANineAndTwo(_CharacterDataInfo);
        TheNineSlot.Instance.ConfirmSkillChangeButton.gameObject.SetActive(false);
        TheNineSlot.Instance.A1DragAndDropCell.gameObject.SetActive(true);
        TheNineSlot.Instance.A2DragAndDropCell.gameObject.SetActive(true);
        TheNineSlot.Instance.A3DragAndDropCell.gameObject.SetActive(true);
        TheNineSlot.Instance.B1DragAndDropCell.gameObject.SetActive(true);
        TheNineSlot.Instance.B2DragAndDropCell.gameObject.SetActive(true);
        TheNineSlot.Instance.B3DragAndDropCell.gameObject.SetActive(true);
        TheNineSlot.Instance.C1DragAndDropCell.gameObject.SetActive(true);
        TheNineSlot.Instance.C2DragAndDropCell.gameObject.SetActive(true);
        TheNineSlot.Instance.C3DragAndDropCell.gameObject.SetActive(true);
        
        CharConfig _CharacterResourceInfo = MonstersConfigTable.GetCharacterResourceInfo(_CharacterDataInfo.monsterId);
        SkillStonesBox.Instance.SetFocusingType(_CharacterResourceInfo.type);
        yield return SkillStonesBox.Instance.EXTabsFeatureRefresh(false);
        void SkillEditConfirm()
        {
            mainProcessRunner.TriggerMainProcess(TheNineSlot.Instance.UpdateMyStonesBaseOnSlots(_CharacterDataInfo));
            _MemberDetail.presentationProcessRunner.TriggerMainProcess(_MemberDetail.SkillEditConfirmAnimation());
            this.subProcessesRunner.ChangeProcess(MainSceneStep.Tutorial_skillEdit_sub4);
        }

        void SkillUpdateValidation()
        {
            LoadingCanvas.target.ArrangeValiationWindow(SkillEditConfirm, "确实要进行技能更新？");
        }
        TheNineSlot.Instance.ConfirmSkillChangeButton.onClick.RemoveAllListeners();
        TheNineSlot.Instance.ConfirmSkillChangeButton.onClick.AddListener(SkillUpdateValidation);               
    }
    
    public IEnumerator RefreshMemberDetailGamenSystemBaseOnFocusingCharTutorailVersion()
    {        
        // 下面这些都是针对技能显示这个高级功能的，按理说下面这些即便出错，上面的功能也该健全。。即，这些是表现层。
        IEnumerator getchar = AccountCharsSet.instance.GetAccountCharInfo("1");
        yield return getchar;
        GetMonsterOfPlayerDetailModel focusingCharacterDataInfo = (GetMonsterOfPlayerDetailModel)getchar.Current;
        CharDataInfo characterDataInfo = RemoteAccess.GetCharacterDataInfo(focusingCharacterDataInfo);
        _MemberDetail.presentationProcessRunner.TriggerMainProcess(this._MemberDetail.SkillsPrintOutFocusingCharChangeProcess(characterDataInfo));
        yield break;
    }
}
