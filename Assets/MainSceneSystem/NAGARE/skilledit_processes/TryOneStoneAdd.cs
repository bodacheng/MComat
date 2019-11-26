using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Api.Dto.Model;
using mainMenu;
using dataAccess;

// 先试着把石头添加到一个格子上。
public class TryOneStoneAdd : MainSceneProcess
{
    int step = 1;
    public IEnumerator enterProcess()
    {
        step = 1;
        // 将角色锁定为剧情人物“亚当”；亚当的信息甚至可以新建。
        yield return AccountCharsSet.Instance.loadStoryCharsByXMLFile();
        
        IEnumerator getchar = AccountCharsSet.instance.GetAccountCharacterInfo("1");
        yield return getchar;
        GetMonsterOfPlayerDetailModel myfighter = (GetMonsterOfPlayerDetailModel)getchar.Current;
        
        _MemberDetail.focusingCharacterDataInfo = myfighter;
        yield return SkillEditorButtonBehaviour(_MemberDetail.focusingCharacterDataInfo);//比如亚当在这个版本的角色存档里localid是1。。。
        IEnumerator loadMyStonesProcess = MySkillStonesReader.Instance.LoadMySkillStones();
        yield return (loadMyStonesProcess);

        SkillStonesBox.Instance.SkillBoxCanvas.gameObject.SetActive(true);
        TheNineSlot.Instance.NineSlotT.gameObject.SetActive(true);
        SkillStonesBox.Instance.BoxWholeT.gameObject.SetActive(true);
        TheNineSlot.Instance.NineSlotT.gameObject.SetActive(true);
        this._MemberDetail.MemberDetailCanvas.gameObject.SetActive(false);
        TheNineSlot.Instance.A2DragAndDropCell.gameObject.SetActive(false);
        TheNineSlot.Instance.A3DragAndDropCell.gameObject.SetActive(false);
        TheNineSlot.Instance.B1DragAndDropCell.gameObject.SetActive(false);
        TheNineSlot.Instance.B2DragAndDropCell.gameObject.SetActive(false);
        TheNineSlot.Instance.B3DragAndDropCell.gameObject.SetActive(false);
        TheNineSlot.Instance.C1DragAndDropCell.gameObject.SetActive(false);
        TheNineSlot.Instance.C2DragAndDropCell.gameObject.SetActive(false);
        TheNineSlot.Instance.C3DragAndDropCell.gameObject.SetActive(false);
        TheNineSlot.Instance.ConfirmSkillChangeButton.gameObject.SetActive(false);

        this._CameraManager.Assign_StartToEndModeCamera(this._MemberDetail.MemDetailWatchPos.position, 3f,15f);
        this._CameraManager.current_Camera_Mode.target = this._MemberDetail.MemDetailTargetPos;
        
        // 表现系
        CharacterResourceInfo _CharacterResourceInfo = monstersConfigTable.getCharacterResourceInfo(_MemberDetail.focusingCharacterDataInfo.monsterId);
        SkillStonesBox.Instance._SkillStoneBoxTabEffectsManager.SwitchZokuseiButtons(
            SkillStonesBox.Instance.ButtonEffectInFxCameraWorldSpace(SkillStonesBox.Instance.fxCamera,SkillStonesBox.Instance.NormalTab.gameObject,5f),
            SkillStonesBox.Instance.ButtonEffectInFxCameraWorldSpace(SkillStonesBox.Instance.fxCamera,SkillStonesBox.Instance.EX1Tab.gameObject,5f),
            SkillStonesBox.Instance.ButtonEffectInFxCameraWorldSpace(SkillStonesBox.Instance.fxCamera,SkillStonesBox.Instance.EX2Tab.gameObject,5f),
            SkillStonesBox.Instance.ButtonEffectInFxCameraWorldSpace(SkillStonesBox.Instance.fxCamera,SkillStonesBox.Instance.EX3Tab.gameObject,5f),
            _CharacterResourceInfo._zokusei);
        yield return RefreshMemberDetailGamenSystemBaseOnFocusingCharSpVersion();
        yield break;
    }
    
    public TryOneStoneAdd(preparingScene _preparingScene)
    {
        this.thisProcessStep = MainSceneStep.Tutorial_skillEdit_sub1;
        this.nextProcessStep = MainSceneStep.Tutorial_skillEdit_sub2;
        
        this._preparingScene = _preparingScene;
        this.EelementsInherit(_preparingScene);
    }

    public override bool CanEnterOtherProcess()
    {
        return int.Parse(this._MemberDetail.focusingCharacterDataInfo.a1_skill_stone_record_id) != -1;
    }

    public override void ProcessEnter()
    {
        this.mainProcessRunner.triggerMainProcess(enterProcess());
    }
    
    public override void ProcessEnd()
    {
        this._LoadingCanvas.ClearHigtLight();
    }

    public override void LocalUpdate()
    {
        if (step == 1)
        {
            if (TheNineSlot.Instance.A1DragAndDropCell.gameObject.transform.GetComponentInChildren<DragAndDropItem>())
            {
                step = 2;
                this._LoadingCanvas.HigtLightRect(TheNineSlot.Instance.A1DragAndDropCell.transform);
            }
        }
        if (step == 2)
        {
            if (this._MemberDetail._SkillsPrintOut.ifShowingSkill())
            {
                step = 3;
                this._LoadingCanvas.ClearHigtLight();
            }
        }
        if (step == 3)
        {
            if (!_MemberDetail._SkillsPrintOut.ifShowingSkill())
            {
                step = 4;
                TheNineSlot.Instance.ConfirmSkillChangeButton.gameObject.SetActive(true);
                _LoadingCanvas.HigtLightRect(TheNineSlot.Instance.ConfirmSkillChangeButton.gameObject.transform);
            }
        }
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
        CharacterResourceInfo _CharacterResourceInfo = monstersConfigTable.getCharacterResourceInfo(_CharacterDataInfo.monsterId);
        SkillStonesBox.Instance.SetFocusingType(_CharacterResourceInfo.type);
        yield return (SkillStonesBox.Instance.EXTabsFeatureRefresh(false));
        void SkillEditConfirm()
        {
            mainProcessRunner.triggerMainProcess(TheNineSlot.Instance.UpdateEditingNineAndTwoBaseOnSlots(_CharacterDataInfo));
            _MemberDetail.presentationProcessRunner.triggerMainProcess(_MemberDetail.SkillEditConfirmAnimation());
        }
        void SkillUpdateValidation()
        {
            _preparingScene._LoadingCanvas.arrangeValiationWindow(SkillEditConfirm, "编辑A1？");
        }
        TheNineSlot.Instance.ConfirmSkillChangeButton.onClick.RemoveAllListeners();
        TheNineSlot.Instance.ConfirmSkillChangeButton.onClick.AddListener(SkillUpdateValidation);
    }
    
    IEnumerator RefreshMemberDetailGamenSystemBaseOnFocusingCharSpVersion()
    {
        IEnumerator getchar = AccountCharsSet.instance.GetAccountCharacterInfo("1");
        yield return getchar;
        GetMonsterOfPlayerDetailModel focusingCharacterDataInfo = (GetMonsterOfPlayerDetailModel)getchar.Current;
        if (focusingCharacterDataInfo == null)
        {
            Debug.Log("严重错误");
            yield break;
        }
        // 下面这些都是针对技能显示这个高级功能的，按理说下面这些即便出错，上面的功能也该健全。。即，这些是表现层。
        CharacterDataInfo focusingData = RemoteAccess.getCharacterDataInfo(focusingCharacterDataInfo);
        _MemberDetail.presentationProcessRunner.triggerMainProcess(this._MemberDetail.SkillsPrintOutFocusingCharChangeProcess(focusingData));
    }
}
