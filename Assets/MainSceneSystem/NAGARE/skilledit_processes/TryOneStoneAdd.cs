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
        
        IEnumerator getchar = AccountCharsSet.instance.GetAccountCharInfo("1");
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
        CharacterResourceInfo _CharacterResourceInfo = MonstersConfigTable.GetCharacterResourceInfo(_MemberDetail.focusingCharacterDataInfo.monsterId);
        SkillStonesBox.Instance._SkillStoneBoxTabEffectsManager.SwitchZokuseiButtons(
            SkillStonesBox.Instance.ButtonEffectInFxCameraWorldSpace(SkillStonesBox.Instance.fxCamera,SkillStonesBox.Instance.NormalTab.gameObject,5f),
            SkillStonesBox.Instance.ButtonEffectInFxCameraWorldSpace(SkillStonesBox.Instance.fxCamera,SkillStonesBox.Instance.EX1Tab.gameObject,5f),
            SkillStonesBox.Instance.ButtonEffectInFxCameraWorldSpace(SkillStonesBox.Instance.fxCamera,SkillStonesBox.Instance.EX2Tab.gameObject,5f),
            SkillStonesBox.Instance.ButtonEffectInFxCameraWorldSpace(SkillStonesBox.Instance.fxCamera,SkillStonesBox.Instance.EX3Tab.gameObject,5f),
            _CharacterResourceInfo._zokusei);
        yield return RefreshMemberDetailGamenSystemBaseOnFocusingCharSpVersion();
        yield break;
    }
    
    public TryOneStoneAdd()
    {
        this.thisProcessStep = MainSceneStep.Tutorial_skillEdit_sub1;
        this.nextProcessStep = MainSceneStep.Tutorial_skillEdit_sub2;
        
        this.EelementsInherit(PreScene.Instance);
    }

    public override bool CanEnterOtherProcess()
    {
        //return int.Parse(this._MemberDetail.focusingCharacterDataInfo.a1_skill_stone_record_id) != -1;
        return true;
    }

    public override void ProcessEnter()
    {
        this.mainProcessRunner.TriggerMainProcess(enterProcess());
    }
    
    public override void ProcessEnd()
    {
        LoadingCanvas.target.ClearHigtLight();
    }

    public override void LocalUpdate()
    {
        if (step == 1)
        {
            if (TheNineSlot.Instance.A1DragAndDropCell.gameObject.transform.GetComponentInChildren<DragAndDropItem>())
            {
                step = 2;
                LoadingCanvas.target.HigtLightRect(TheNineSlot.Instance.A1DragAndDropCell.transform);
            }
        }
        if (step == 2)
        {
            if (this._MemberDetail._SkillsPrintOut.IfShowingSkill)
            {
                step = 3;
                LoadingCanvas.target.ClearHigtLight();
            }
        }
        if (step == 3)
        {
            if (!_MemberDetail._SkillsPrintOut.IfShowingSkill)
            {
                step = 4;
                TheNineSlot.Instance.ConfirmSkillChangeButton.gameObject.SetActive(true);
                LoadingCanvas.target.HigtLightRect(TheNineSlot.Instance.ConfirmSkillChangeButton.gameObject.transform);
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
        CharacterResourceInfo _CharacterResourceInfo = MonstersConfigTable.GetCharacterResourceInfo(_CharacterDataInfo.monsterId);
        SkillStonesBox.Instance.SetFocusingType(_CharacterResourceInfo.type);
        yield return (SkillStonesBox.Instance.EXTabsFeatureRefresh(false));
        void SkillEditConfirm()
        {
            mainProcessRunner.TriggerMainProcess(TheNineSlot.Instance.UpdateMyStonesBaseOnSlots(_CharacterDataInfo));
            _MemberDetail.presentationProcessRunner.TriggerMainProcess(_MemberDetail.SkillEditConfirmAnimation());
        }
        void SkillUpdateValidation()
        {
            LoadingCanvas.target.ArrangeValiationWindow(SkillEditConfirm, "编辑A1？");
        }
        TheNineSlot.Instance.ConfirmSkillChangeButton.onClick.RemoveAllListeners();
        TheNineSlot.Instance.ConfirmSkillChangeButton.onClick.AddListener(SkillUpdateValidation);
    }
    
    IEnumerator RefreshMemberDetailGamenSystemBaseOnFocusingCharSpVersion()
    {
        IEnumerator getchar = AccountCharsSet.instance.GetAccountCharInfo("1");
        yield return getchar;
        GetMonsterOfPlayerDetailModel focusingCharacterDataInfo = (GetMonsterOfPlayerDetailModel)getchar.Current;
        if (focusingCharacterDataInfo == null)
        {
            Debug.Log("严重错误");
            yield break;
        }
        // 下面这些都是针对技能显示这个高级功能的，按理说下面这些即便出错，上面的功能也该健全。。即，这些是表现层。
        CharDataInfo focusingData = RemoteAccess.GetCharacterDataInfo(focusingCharacterDataInfo);
        _MemberDetail.presentationProcessRunner.TriggerMainProcess(this._MemberDetail.SkillsPrintOutFocusingCharChangeProcess(focusingData));
    }
}
