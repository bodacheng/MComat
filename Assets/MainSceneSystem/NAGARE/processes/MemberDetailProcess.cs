using System.Collections;
using UnityEngine;
using mainMenu;

public class MemberDetailProcess : MainSceneProcess
{
    public MemberDetailProcess()
    {
        Step = MainSceneStep.MemberDetail;
        EelementsInherit(PreScene.target);
    }
    
    public IEnumerator EnterProcess()
    {
        PreScene.target._SkillStonesBox_NineSlot.SkillBoxCanvas.gameObject.SetActive(false);
        PreScene.target._SkillStonesBox_Show.SkillBoxCanvas.gameObject.SetActive(false);
        // 相机的这个锁定，在所有技能展示结束后应该是按以下这两行的标准进行归位。 
        _CameraManager.Assign_SToEMode(MemberDetail.target.MemDetailWatchPos.position,3f,25f);
        _CameraManager.CurrentMode.target = MemberDetail.target.MemDetailTargetPos;
        MemberDetail.target.MemberDetailCanvas.gameObject.SetActive(true);
        yield return MonsterBox.DisplayMonsterIcons();
        MemberDetail.target.AddHeroIconFeaturesToMonsterBox();// 该处理紧随MonsterBox.DisplayMonsterIcons之后
        //this._MonsterBox.adjustAllIconsSize(null);
        MonsterBox.target.MonsterBoxWholeT.gameObject.SetActive(true);
        yield return MemberDetail.target.RefreshMemberDetailPageByFocusingChar();
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
        MemberDetail.target.MemberDetailCanvas.gameObject.SetActive(false);
        MemberDetail.target.MemberInfoT.gameObject.SetActive(false);
        MonsterBox.target.MonsterBoxWholeT.gameObject.SetActive(false);
    }

    readonly Vector3 screenPos = new Vector3(0.23f, 0.37f, 20f);
    public override void LocalUpdate()
    {
        if (!MemberDetail.target._SkillsPrintOut.IfShowingSkill)
        {
            ModelShower.target.TranslateShowingCharToDefaultPos(screenPos);
        }
    }
}
