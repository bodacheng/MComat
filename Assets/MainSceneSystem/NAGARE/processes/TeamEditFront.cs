using System.Collections;
using UnityEngine;
using mainMenu;
using dataAccess;

public class TeamEditFront : MainSceneProcess
{
    public IEnumerator EnterProcess()
    {
        PreScene.target._SkillStonesBox_NineSlot.SkillBoxCanvas.gameObject.SetActive(false);
        PreScene.target._SkillStonesBox_Show.SkillBoxCanvas.gameObject.SetActive(false);
        MonsterBox.target.MonsterBoxWholeT.gameObject.SetActive(true);
        _CameraManager.Assign_SToEMode(MemberDetail.target.MemDetailWatchPos.position, 3f,15f);
        _CameraManager.CurrentMode.target = MemberDetail.target.MemDetailTargetPos;        
        yield return TeamSet.LoadTeamSet(TeamSet.targetTeamMode);
        yield return PreScene.target.TeamEditor.INITeamPosButtons();
        yield return MonsterBox.DisplayMonsterIcons();
        PreScene.target.TeamEditor.AddHeroIconFeaturesToMonsterBox();// 该处理紧随MonsterBox.DisplayMonsterIcons之后
        PreScene.target.ArcadeTeamEditT.gameObject.SetActive(true);
        yield break;
    }
    
    public TeamEditFront()
    {
        Step = MainSceneStep.TeamEditFront;
        EelementsInherit(PreScene.target);
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
        PreScene.target.ArcadeTeamEditT.gameObject.SetActive(false);
        MonsterBox.target.MonsterBoxWholeT.gameObject.SetActive(false);
        mainProcessRunner.Run(TeamSet.SaveTeamSet(TeamSet.targetTeamMode));// 退出队伍编辑画面即保存
    }
    
    readonly Vector3 screenPos = new Vector3(0.23f, 0.35f, 20f);
    public override void LocalUpdate()
    {
        if (!MemberDetail.target._SkillsPrintOut.IfShowingSkill)
        {
            ModelShower.target.TranslateShowingCharToDefaultPos(screenPos);
        }
    }
}
