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
        
        IEnumerator getchar = AccountCharsSet.instance.getAccountCharacterInfo("1");
        yield return getchar;
        GetMonsterOfPlayerDetailModel myfighter = (GetMonsterOfPlayerDetailModel)getchar.Current;
        
        _MemberDetail.focusingCharacterDataInfo = myfighter;
        yield return SkillEditorButtonBehaviour(_MemberDetail.focusingCharacterDataInfo);//比如亚当在这个版本的角色存档里localid是1。。。
        IEnumerator loadMyStonesProcess = MySkillStonesReader.Instance.loadMySkillStones();
            yield return (loadMyStonesProcess);

        this._SkillStonesBox.SkillBoxCanvas.gameObject.SetActive(true);
        this._TheNineSlot.NineAndTwoCanvas.gameObject.SetActive(true);
        this._SkillStonesBox.BoxWholeT.gameObject.SetActive(true);
        this._TheNineSlot.NineSlotT.gameObject.SetActive(true);
        this._MemberDetail.MemberDetailCanvas.gameObject.SetActive(false);
        this._TheNineSlot.A2DragAndDropCell.gameObject.SetActive(false);
        this._TheNineSlot.A3DragAndDropCell.gameObject.SetActive(false);
        this._TheNineSlot.B1DragAndDropCell.gameObject.SetActive(false);
        this._TheNineSlot.B2DragAndDropCell.gameObject.SetActive(false);
        this._TheNineSlot.B3DragAndDropCell.gameObject.SetActive(false);
        this._TheNineSlot.C1DragAndDropCell.gameObject.SetActive(false);
        this._TheNineSlot.C2DragAndDropCell.gameObject.SetActive(false);
        this._TheNineSlot.C3DragAndDropCell.gameObject.SetActive(false);
        this._TheNineSlot.ConfirmSkillChangeButton.gameObject.SetActive(false);

        this._CameraManager.Assign_StartToEndModeCamera(this._MemberDetail.MemDetailWatchPos.position, 3f,15f);
        this._CameraManager.current_Camera_Mode.target = this._MemberDetail.MemDetailTargetPos;
        
        // 表现系
        CharacterResourceInfo _CharacterResourceInfo = MonsterConfigInfos.getCharacterResourceInfo(int.Parse(_MemberDetail.focusingCharacterDataInfo.monsterId));
        _SkillStonesBox._SkillStoneBoxTabEffectsManager.switchZokuseiButtons(
            _MemberDetail.ButtonEffectInFxCameraWorldSpace(_preparingScene.fxCamera,_SkillStonesBox.NormalTab.gameObject,5f),
            _MemberDetail.ButtonEffectInFxCameraWorldSpace(_preparingScene.fxCamera,_SkillStonesBox.EX1Tab.gameObject,5f),
            _MemberDetail.ButtonEffectInFxCameraWorldSpace(_preparingScene.fxCamera,_SkillStonesBox.EX2Tab.gameObject,5f),
            _MemberDetail.ButtonEffectInFxCameraWorldSpace(_preparingScene.fxCamera,_SkillStonesBox.EX3Tab.gameObject,5f),
            _CharacterResourceInfo._zokusei);
        yield return refreshMemberDetailGamenSystemBaseOnFocusingCharSpVersion();
        yield break;
    }
    
    public TryOneStoneAdd(preparingScene _preparingScene)
    {
        this.thisProcessStep = MainSceneStep.Tutorial_skillEdit_sub1;
        this.nextProcessStep = MainSceneStep.Tutorial_skillEdit_sub2;
        
        this._preparingScene = _preparingScene;
        this.EelementsInherit(_preparingScene);
    }

    public override bool canEnterOtherProcess()
    {
        if (int.Parse(this._MemberDetail.focusingCharacterDataInfo.a1_skill_stone_record_id) != -1)
            return true;
        return false;
    }
    
    public override void ProcessEnter()
    {
        this.mainProcessRunner.triggerMainProcess(enterProcess());
    }
    
    public override void ProcessEnd()
    {
        this._LoadingCanvas.ClearHigtLight();
    }

    public override void localUpdate()
    {
        if (step == 1)
        {
            if (this._TheNineSlot.A1DragAndDropCell.gameObject.transform.GetComponentInChildren<DragAndDropItem>())
            {
                step = 2;
                this._LoadingCanvas.HigtLightRect(this._TheNineSlot.A1DragAndDropCell.transform);
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
            if (!this._MemberDetail._SkillsPrintOut.ifShowingSkill())
            {
                step = 4;
                this._TheNineSlot.ConfirmSkillChangeButton.gameObject.SetActive(true);
                this._LoadingCanvas.HigtLightRect(this._TheNineSlot.ConfirmSkillChangeButton.gameObject.transform);
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
        yield return _TheNineSlot.readANineAndTwo(_CharacterDataInfo);
        CharacterResourceInfo _CharacterResourceInfo = MonsterConfigInfos.getCharacterResourceInfo(int.Parse(_CharacterDataInfo.monsterId));
        _SkillStonesBox.setFocusingType(_CharacterResourceInfo.type);
        yield return (_SkillStonesBox.EXTabsFeatureRefresh(_CharacterResourceInfo.type,false));
        UnityEngine.Events.UnityAction SkillEditConfirm = () =>//这里可能还有一个执行内容，就是进入到测试战斗场景。
        {
            mainProcessRunner.triggerMainProcess(_TheNineSlot.UpdateEditingNineAndTwoBaseOnSlots(_CharacterDataInfo));
            _MemberDetail.presentationProcessRunner.triggerMainProcess(_MemberDetail.SkillEditConfirmAnimation());
        };
        UnityEngine.Events.UnityAction SkillUpdateValidation = () =>
        {
            _preparingScene._LoadingCanvas.arrangeValiationWindow(SkillEditConfirm, "编辑A1？");
        };
        _TheNineSlot.ConfirmSkillChangeButton.onClick.RemoveAllListeners();
        _TheNineSlot.ConfirmSkillChangeButton.onClick.AddListener(SkillUpdateValidation);
    }
    
    IEnumerator refreshMemberDetailGamenSystemBaseOnFocusingCharSpVersion()
    {
        IEnumerator getchar = AccountCharsSet.instance.getAccountCharacterInfo("1");
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
