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
        this.nextProcessStep = MainSceneStep.None;
        
        this.SubProcessesRunner = processesRunner;
        this.EelementsInherit(PreScene.target);
    }

    public override bool CanEnterOtherProcess()
    {
        return false;
    }
    
    public override void ProcessEnter()
    {
        mainProcessRunner.Run(EnterProcess());
    }
    
    public override void ProcessEnd()
    {
    }

    public override void LocalUpdate()
    {
        if (TheNineSlot.target.A1DragAndDropCell.gameObject.transform.GetComponentInChildren<SKStoneItem>() && 
                TheNineSlot.target.A2DragAndDropCell.gameObject.transform.GetComponentInChildren<SKStoneItem>() && 
                    TheNineSlot.target.A3DragAndDropCell.gameObject.transform.GetComponentInChildren<SKStoneItem>() &&
                        TheNineSlot.target.B1DragAndDropCell.gameObject.transform.GetComponentInChildren<SKStoneItem>() && 
                            TheNineSlot.target.B2DragAndDropCell.gameObject.transform.GetComponentInChildren<SKStoneItem>() && 
                                TheNineSlot.target.B3DragAndDropCell.gameObject.transform.GetComponentInChildren<SKStoneItem>() &&
                                    TheNineSlot.target.C1DragAndDropCell.gameObject.transform.GetComponentInChildren<SKStoneItem>() && 
                                        TheNineSlot.target.C2DragAndDropCell.gameObject.transform.GetComponentInChildren<SKStoneItem>() && 
                                            TheNineSlot.target.C3DragAndDropCell.gameObject.transform.GetComponentInChildren<SKStoneItem>())
        {
            TheNineSlot.target.ConfirmSkillChangeButton.gameObject.SetActive(true);
        }
    }
    
    public IEnumerator EnterProcess()
    {
        //SkillStonesBox.Instance.SkillBoxCanvas.gameObject.SetActive(true);
        //TheNineSlot.Instance.NineSlotT.gameObject.SetActive(true);
        //SkillStonesBox.Instance.BoxWholeT.gameObject.SetActive(true);
        
        IEnumerator getchar = AccountCharsSet.Load("1");
        yield return getchar;
        MemberDetail.target.focusingCharDataInfo = (GetMonsterOfPlayerDetailModel)getchar.Current;
        yield return SkillEditorButtonBehaviour(MemberDetail.target.focusingCharDataInfo);//比如亚当在这个版本的角色存档里localid是1。。。

        // Tutorial 模式那两按钮不需要显示
        MemberDetail.target.MemberDetailCanvas.gameObject.SetActive(false);
        this._CameraManager.Assign_SToEMode(MemberDetail.target.MemDetailWatchPos.position, 3f,15f);
        this._CameraManager.CurrentMode.target = MemberDetail.target.MemDetailTargetPos;
        
        // 表现系
        CharConfig _CharacterResourceInfo = MonstersConfigTable.GetCharConfig(MemberDetail.target.focusingCharDataInfo.monsterId);
        //SkillStonesBox.Instance._SkillStoneBoxTabEffectsManager.SwitchZokuseiButtons(
            //SkillStonesBox.Instance.ButtonEffectInFxCameraWorldSpace(SkillStonesBox.Instance.fxCamera,SkillStonesBox.Instance.NormalTab.gameObject,5f),
            //SkillStonesBox.Instance.ButtonEffectInFxCameraWorldSpace(SkillStonesBox.Instance.fxCamera,SkillStonesBox.Instance.EX1Tab.gameObject,5f),
            //SkillStonesBox.Instance.ButtonEffectInFxCameraWorldSpace(SkillStonesBox.Instance.fxCamera,SkillStonesBox.Instance.EX2Tab.gameObject,5f),
            //SkillStonesBox.Instance.ButtonEffectInFxCameraWorldSpace(SkillStonesBox.Instance.fxCamera,SkillStonesBox.Instance.EX3Tab.gameObject,5f),_CharacterResourceInfo._zokusei);
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
        yield return TheNineSlot.target.ReadANineAndTwo(_CharacterDataInfo);
        TheNineSlot.target.ConfirmSkillChangeButton.gameObject.SetActive(false);
        TheNineSlot.target.A1DragAndDropCell.gameObject.SetActive(true);
        TheNineSlot.target.A2DragAndDropCell.gameObject.SetActive(true);
        TheNineSlot.target.A3DragAndDropCell.gameObject.SetActive(true);
        TheNineSlot.target.B1DragAndDropCell.gameObject.SetActive(true);
        TheNineSlot.target.B2DragAndDropCell.gameObject.SetActive(true);
        TheNineSlot.target.B3DragAndDropCell.gameObject.SetActive(true);
        TheNineSlot.target.C1DragAndDropCell.gameObject.SetActive(true);
        TheNineSlot.target.C2DragAndDropCell.gameObject.SetActive(true);
        TheNineSlot.target.C3DragAndDropCell.gameObject.SetActive(true);
        
        CharConfig _CharacterResourceInfo = MonstersConfigTable.GetCharConfig(_CharacterDataInfo.monsterId);
        //SkillStonesBox.Instance.SetFocusingType(_CharacterResourceInfo.type);
        //yield return SkillStonesBox.Instance.EXTabsFeatureRefresh(false);
        void SkillEditConfirm()
        {
            mainProcessRunner.Run(TheNineSlot.target.UpdateMyStonesBaseOnSlots(_CharacterDataInfo));
            MemberDetail.target.presentationProcessRunner.Run(MemberDetail.target.SkillEditConfirmAnimation());
        }

        void SkillUpdateValidation()
        {
            LoadingCanvas.target.ArrangeValiationWindow(SkillEditConfirm, "确实要进行技能更新？");
        }
        TheNineSlot.target.ConfirmSkillChangeButton.onClick.RemoveAllListeners();
        TheNineSlot.target.ConfirmSkillChangeButton.onClick.AddListener(SkillUpdateValidation);               
    }
    
    public IEnumerator RefreshMemberDetailGamenSystemBaseOnFocusingCharTutorailVersion()
    {
        // 下面这些都是针对技能显示这个高级功能的，按理说下面这些即便出错，上面的功能也该健全。。即，这些是表现层。
        IEnumerator getchar = AccountCharsSet.Load("1");
        yield return getchar;
        GetMonsterOfPlayerDetailModel focusingCharacterDataInfo = (GetMonsterOfPlayerDetailModel)getchar.Current;
        CharDataInfo characterDataInfo = GetMonsterOfPlayerDetailModel.GetCharDataInfo(focusingCharacterDataInfo);
        MemberDetail.target.presentationProcessRunner.Run(MemberDetail.target.SkillsPrintOutFocusingCharChangeProcess(characterDataInfo));
        yield break;
    }
}
