using System.Collections;
using UnityEngine;
using mainMenu;

public class MemberDetailProcess : MainSceneProcess
{    
    public MemberDetailProcess(preparingScene _preparingScene)
    {
        thisProcessStep = MainSceneStep.MemberDetail;
        this._preparingScene = _preparingScene;
        EelementsInherit(_preparingScene);
    }
    
    public IEnumerator EnterProcess()
    {
        SkillStonesBox.Instance.SkillBoxCanvas.gameObject.SetActive(false);
        // 相机的这个锁定，在所有技能展示结束后应该是按以下这两行的标准进行归位。 
        _CameraManager.Assign_StartToEndModeCamera(_MemberDetail.MemDetailWatchPos.position,3f,25f);
        _CameraManager.current_Camera_Mode.target = _MemberDetail.MemDetailTargetPos;
        _MemberDetail.MemberDetailCanvas.gameObject.SetActive(true);
        yield return MonsterBox.DisplayMonsterIcons();
        //this._MonsterBox.adjustAllIconsSize(null);
        _MonsterBox.MonsterBoxWholeT.gameObject.SetActive(true);
        yield return _MemberDetail.RefreshMemberDetailGamenSystemBaseOnFocusingChar();
    }

    public override bool CanEnterOtherProcess()
    {
        return true;
    }
    
    public override void ProcessEnter()
    {
        mainProcessRunner.triggerMainProcess(EnterProcess());
    }
    
    public override void ProcessEnd()
    {
        _MemberDetail.MemberDetailCanvas.gameObject.SetActive(false);
        _MemberDetail.MemberInfoT.gameObject.SetActive(false);
        _MonsterBox.MonsterBoxWholeT.gameObject.SetActive(false);
    }

    readonly Vector3 screenPos = new Vector3(0.23f, 0.37f, 20f);
    public override void LocalUpdate()
    {
        if (!_MemberDetail._SkillsPrintOut.IfShowingSkill)
        {
            _modelShower.TranslateShowingCharToDefaultPos(screenPos);
        }
    }
}
