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
        _CameraManager.Assign_SToEMode(MemberDetail.target.MemDetailWatchPos.position, MemberDetail.target.MemDetailTargetPos, 3f, 15f);
        yield return TeamSet.LoadTeamSet(TeamSet.targetTeamMode);
        yield return PreScene.target.TeamEditor.INITeamPosButtons();
        yield return MonsterBox.DisplayMonsterIcons(true);
        PreScene.target.TeamEditor.AddHeroIconFeaturesToMonsterBox();// 该处理紧随MonsterBox.DisplayMonsterIcons之后
        PreScene.target.TeamEditor._nineForShow.ShowStones_Acc(MemberDetail.target._focusing.monsterOfPlayerId);
        PreScene.target.ArcadeTeamEditT.gameObject.SetActive(true);
    }
    
    public TeamEditFront()
    {
        Step = MainSceneStep.TeamEditFront;
        EelementsInherit(PreScene.target);
    }
        
    public override void ProcessEnter()
    {
        mainProcessRunner.RunAsQueued(EnterProcess());
    }
    
    public override void ProcessEnd()
    {
        PreScene.target.ArcadeTeamEditT.gameObject.SetActive(false);
        MonsterBox.target.MonsterBoxWholeT.gameObject.SetActive(false);
        mainProcessRunner.RunAsQueued(TeamSet.SaveTeamSet(TeamSet.targetTeamMode));// 退出队伍编辑画面即保存
    }
    
    readonly Vector3 screenPos = new Vector3(0.23f, 0.35f, ModelShower._nearClipPlane);
    public override void LocalUpdate()
    {
        if (!MemberDetail.target._SkillsPrintOut.IfShowingSkill)
        {
            ModelShower.target.TranslateShowingCharToDefaultPos(screenPos);
        }else{
            ModelShower.target.CFollowCharZ();
        }
    }
}
