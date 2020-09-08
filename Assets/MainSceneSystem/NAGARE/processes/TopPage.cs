using System.Collections;
using UnityEngine;
using mainMenu;
using dataAccess;

public class TopPage : MainSceneProcess
{
    public TopPage()
    {
        Step = MainSceneStep.FrontPage;
        EelementsInherit(PreScene.target);
    }
    
    public IEnumerator EnterProcess()
    {
        PreScene.target.MainMenuCanvas.gameObject.SetActive(true);
        PreScene.target.MainMenuBottonsT.gameObject.SetActive(true);
        PreScene.target._SkillStonesBox_NineSlot.SkillBoxCanvas.gameObject.SetActive(false);
        PreScene.target._SkillStonesBox_Show.SkillBoxCanvas.gameObject.SetActive(false);
        MonsterBox.target.MonsterBoxWholeT.gameObject.SetActive(false);
        
        // 相机的这个锁定，在所有技能展示结束后应该是按以下这两行的标准进行归位。 
        _CameraManager.Assign_SToEMode(MemberDetail.target.MemDetailWatchPos.position, MemberDetail.target.MemDetailTargetPos, 3f, 15f);
        MemberDetail.target.MemberDetailCanvas.gameObject.SetActive(false);

        yield return TeamSet.LoadTeamSet(TeamSetGameMode.story);
        
        if (TeamSet.Default != null)
        {
            string focusLocalid = TeamSet.Default.GetMonsterOfPlayerIdOnPos(0);
            if (focusLocalid != null)
            {
                yield return MemberDetail.target.SetMemberDetailFocusingChar(focusLocalid);//确立focusing角色
                yield return ModelShower.target.ShowMyModel(focusLocalid);
            }
        }
        yield break;
    
    }
        
    public override void ProcessEnter()
    {
        mainProcessRunner.Run(EnterProcess());
    }
    
    public override void ProcessEnd()
    {
        PreScene.target.MainMenuBottonsT.gameObject.SetActive(false);
    }

    readonly Vector3 screenPos = new Vector3(0.23f, 0.3f, ModelShower._nearClipPlane);
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
