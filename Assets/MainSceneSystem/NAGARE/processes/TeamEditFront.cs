using System.Collections;
using UnityEngine;
using mainMenu;

public class TeamEditFront : MainSceneProcess
{
    public IEnumerator EnterProcess()
    {
        PreScene.Instance._SkillStonesBox_NineSlot.SkillBoxCanvas.gameObject.SetActive(false);
        PreScene.Instance._SkillStonesBox_Show.SkillBoxCanvas.gameObject.SetActive(false);
        MonsterBox.target.MonsterBoxWholeT.gameObject.SetActive(true);
        _CameraManager.Assign_StartToEndModeCamera(MemberDetail.target.MemDetailWatchPos.position, 3f,15f);
        _CameraManager.current_Camera_Mode.target = MemberDetail.target.MemDetailTargetPos;
        yield return PreScene.Instance.arcadeTeamManager.INITeamPosButtons();
        yield return MonsterBox.DisplayMonsterIcons();
        PreScene.Instance.arcadeTeamManager.AddHeroIconFeaturesToMonsterBox();// 该处理紧随MonsterBox.DisplayMonsterIcons之后
        PreScene.Instance.ArcadeTeamEditT.gameObject.SetActive(true);
        yield break;
    }
    
    public TeamEditFront()
    {
        thisProcessStep = MainSceneStep.TeamEditFront;
        EelementsInherit(PreScene.Instance);
    }

    public override bool CanEnterOtherProcess()
    {
        return true;
    }
    
    public override void ProcessEnter()
    {
        mainProcessRunner.Run(EnterProcess());
    }
    
    public override void ProcessEnd()
    {
        PreScene.Instance.ArcadeTeamEditT.gameObject.SetActive(false);
    }
    
    Vector3 screenPos = new Vector3(0.23f, 0.35f, 20f);
    public override void LocalUpdate()
    {
        if (!MemberDetail.target._SkillsPrintOut.IfShowingSkill)
        {
            _modelShower.TranslateShowingCharToDefaultPos(screenPos);
        }
    }
}
