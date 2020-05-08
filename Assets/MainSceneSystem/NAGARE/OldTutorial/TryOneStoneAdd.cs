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
        yield return AccountCharsSet.loadStoryCharsByXMLFile();
        
        IEnumerator getchar = AccountCharsSet.Load("1");
        yield return getchar;
        GetMonsterOfPlayerDetailModel myfighter = (GetMonsterOfPlayerDetailModel)getchar.Current;
        
        MemberDetail.target.focusingCharDataInfo = myfighter;
        yield return SkillEditorButtonBehaviour(MemberDetail.target.focusingCharDataInfo);//比如亚当在这个版本的角色存档里localid是1。。。
        IEnumerator loadMyStonesProcess = MySkillStonesReader.LoadAll();
        yield return (loadMyStonesProcess);

        //SkillStonesBox.Instance.SkillBoxCanvas.gameObject.SetActive(true);
        //TheNineSlot.Instance.NineSlotT.gameObject.SetActive(true);
        //SkillStonesBox.Instance.BoxWholeT.gameObject.SetActive(true);
        TheNineSlot.target.NineSlotT.gameObject.SetActive(true);
        MemberDetail.target.MemberDetailCanvas.gameObject.SetActive(false);
        TheNineSlot.target.A2DragAndDropCell.gameObject.SetActive(false);
        TheNineSlot.target.A3DragAndDropCell.gameObject.SetActive(false);
        TheNineSlot.target.B1DragAndDropCell.gameObject.SetActive(false);
        TheNineSlot.target.B2DragAndDropCell.gameObject.SetActive(false);
        TheNineSlot.target.B3DragAndDropCell.gameObject.SetActive(false);
        TheNineSlot.target.C1DragAndDropCell.gameObject.SetActive(false);
        TheNineSlot.target.C2DragAndDropCell.gameObject.SetActive(false);
        TheNineSlot.target.C3DragAndDropCell.gameObject.SetActive(false);
        TheNineSlot.target.ConfirmSkillChangeButton.gameObject.SetActive(false);

        this._CameraManager.Assign_SToEMode(MemberDetail.target.MemDetailWatchPos.position, 3f,15f);
        this._CameraManager.CurrentMode.target = MemberDetail.target.MemDetailTargetPos;
        
        // 表现系
        CharConfig _CharacterResourceInfo = MonstersConfigTable.GetCharConfig(MemberDetail.target.focusingCharDataInfo.monsterId);
        //SkillStonesBox.Instance._SkillStoneBoxTabEffectsManager.SwitchZokuseiButtons(
            //SkillStonesBox.Instance.ButtonEffectInFxCameraWorldSpace(SkillStonesBox.Instance.fxCamera,SkillStonesBox.Instance.NormalTab.gameObject,5f),
            //SkillStonesBox.Instance.ButtonEffectInFxCameraWorldSpace(SkillStonesBox.Instance.fxCamera,SkillStonesBox.Instance.EX1Tab.gameObject,5f),
            //SkillStonesBox.Instance.ButtonEffectInFxCameraWorldSpace(SkillStonesBox.Instance.fxCamera,SkillStonesBox.Instance.EX2Tab.gameObject,5f),
            //SkillStonesBox.Instance.ButtonEffectInFxCameraWorldSpace(SkillStonesBox.Instance.fxCamera,SkillStonesBox.Instance.EX3Tab.gameObject,5f),
            //_CharacterResourceInfo._zokusei);
        yield return RefreshMemberDetailGamenSystemBaseOnFocusingCharSpVersion();
        yield break;
    }
    
    public TryOneStoneAdd()
    {
        EelementsInherit(PreScene.target);
    }

    public override bool CanEnterOtherProcess()
    {
        //return int.Parse(this._MemberDetail.focusingCharacterDataInfo.a1_skill_stone_record_id) != -1;
        return true;
    }

    public override void ProcessEnter()
    {
        this.mainProcessRunner.Run(enterProcess());
    }
    
    public override void ProcessEnd()
    {
        LoadingCanvas.target.ClearHigtLight();
    }

    public override void LocalUpdate()
    {
        if (step == 1)
        {
            if (TheNineSlot.target.A1DragAndDropCell.gameObject.transform.GetComponentInChildren<SKStoneItem>())
            {
                step = 2;
                LoadingCanvas.target.HigtLightRect(TheNineSlot.target.A1DragAndDropCell.transform);
            }
        }
        if (step == 2)
        {
            if (MemberDetail.target._SkillsPrintOut.IfShowingSkill)
            {
                step = 3;
                LoadingCanvas.target.ClearHigtLight();
            }
        }
        if (step == 3)
        {
            if (!MemberDetail.target._SkillsPrintOut.IfShowingSkill)
            {
                step = 4;
                TheNineSlot.target.ConfirmSkillChangeButton.gameObject.SetActive(true);
                LoadingCanvas.target.HigtLightRect(TheNineSlot.target.ConfirmSkillChangeButton.gameObject.transform);
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
        yield return TheNineSlot.target.ReadANineAndTwo(_CharacterDataInfo);
        CharConfig _CharacterResourceInfo = MonstersConfigTable.GetCharConfig(_CharacterDataInfo.monsterId);
        //SkillStonesBox.Instance.SetFocusingType(_CharacterResourceInfo.type);
        //yield return (SkillStonesBox.Instance.EXTabsFeatureRefresh(false));
        void SkillEditConfirm()
        {
            mainProcessRunner.Run(TheNineSlot.target.UpdateMyStonesBaseOnSlots(_CharacterDataInfo));
            MemberDetail.target.presentationProcessRunner.Run(MemberDetail.target.SkillEditConfirmAnimation());
        }
        void SkillUpdateValidation()
        {
            LoadingCanvas.target.ArrangeValiationWindow(SkillEditConfirm, "编辑A1？");
        }
        TheNineSlot.target.ConfirmSkillChangeButton.onClick.RemoveAllListeners();
        TheNineSlot.target.ConfirmSkillChangeButton.onClick.AddListener(SkillUpdateValidation);
    }
    
    IEnumerator RefreshMemberDetailGamenSystemBaseOnFocusingCharSpVersion()
    {
        GetMonsterOfPlayerDetailModel focusingCharacterDataInfo = AccountCharsSet.Get("1");
        if (focusingCharacterDataInfo == null)
        {
            Debug.Log("严重错误");
            yield break;
        }
        // 下面这些都是针对技能显示这个高级功能的，按理说下面这些即便出错，上面的功能也该健全。。即，这些是表现层。
        CharDataInfo focusingData = GetMonsterOfPlayerDetailModel.GetCharDataInfo(focusingCharacterDataInfo);
        MemberDetail.target.presentationProcessRunner.Run(MemberDetail.target.SkillsPrintOutFocusingCharChangeProcess(focusingData));
    }
}
