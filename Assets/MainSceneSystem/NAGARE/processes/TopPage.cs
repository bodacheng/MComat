using System.Collections;
using UnityEngine;
using mainMenu;
using dataAccess;

public class TopPage : MainSceneProcess
{
    public TopPage()
    {
        thisProcessStep = MainSceneStep.frontPage;
        EelementsInherit(PreScene.Instance);
    }

    public override bool CanEnterOtherProcess()
    {
        return true;
    }
    
    public IEnumerator EnterProcess()
    {
        PreScene.Instance.MainMenuCanvas.gameObject.SetActive(true);
        PreScene.Instance.MainMenuBottonsT.gameObject.SetActive(true);
        PreScene.Instance._SkillStonesBox_NineSlot.SkillBoxCanvas.gameObject.SetActive(false);
        PreScene.Instance._SkillStonesBox_Show.SkillBoxCanvas.gameObject.SetActive(false);
        MonsterBox.target.MonsterBoxWholeT.gameObject.SetActive(false);

        // 相机的这个锁定，在所有技能展示结束后应该是按以下这两行的标准进行归位。 
        _CameraManager.Assign_StartToEndModeCamera(MemberDetail.target.MemDetailWatchPos.position, 3f,15f);
        _CameraManager.current_Camera_Mode.target = MemberDetail.target.MemDetailTargetPos;
        MemberDetail.target.MemberDetailCanvas.gameObject.SetActive(false);

        yield return TeamSet.LoadTeamSet(TeamSetGameMode.story);
        
        if (TeamSet.Default != null)
        {
            string focusLocalid = TeamSet.Default.GetPosMonsterOfPlayerId(0);
            if (focusLocalid != null)
            {
                 yield return MemberDetail.target.SetMemberDetailFocusingChar(focusLocalid);//确立focusing角色
                yield return _modelShower.ShowModel(focusLocalid);
            }
        }
        yield break;
    
    }
        
    public override void ProcessEnter()
    {
        this.mainProcessRunner.Run(EnterProcess());
    }
    
    public override void ProcessEnd()
    {
        PreScene.Instance.MainMenuBottonsT.gameObject.SetActive(false);
    }

    Vector3 screenPos = new Vector3(0.23f, 0.3f, 20f);
    public override void LocalUpdate()
    {
        if (!MemberDetail.target._SkillsPrintOut.IfShowingSkill)
        {
            this._modelShower.TranslateShowingCharToDefaultPos(screenPos);
        }
    }
}
