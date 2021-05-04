using UnityEngine;
using mainMenu;
using Cysharp.Threading.Tasks;

public class MonsterListPage : MainSceneProcess
{
    public bool loadFinished;
    
    public MonsterListPage()
    {
        Step = MainSceneStep.MonsterList;
        EelementsInherit(PreScene.target);
    }
    
    public void temp()
    {
        PreScene.target._SkillStonesBox_NineSlot.SkillBoxCanvas.gameObject.SetActive(false);
        PreScene.target._SkillStonesBox_Show.SkillBoxCanvas.gameObject.SetActive(false);
        // 相机的这个锁定，在所有技能展示结束后应该是按以下这两行的标准进行归位。 
        _CameraManager.Assign_SToEMode(MemberDetail.target.MemDetailWatchPos.position, MemberDetail.target.MemDetailTargetPos, 3f, 15f);
        MemberDetail.target.MemberDetailCanvas.gameObject.SetActive(true);
        //this._MonsterBox.adjustAllIconsSize(null);
        MonsterBox.target.MonsterBoxWholeT.gameObject.SetActive(true);
        MemberDetail.target.RefreshMemberDetailPageByFocusingChar();
        loadFinished = true;
    }

    public async UniTask enter()
    {
        await MonsterBox.DisplayMonsterIcons(true);
        MemberDetail.target.AddHeroIconFeaturesToMonsterBox();// 该处理紧随MonsterBox.DisplayMonsterIcons之后
    }

    public override void ProcessEnter()
    {
        loadFinished = false;
        UnityEngine.Events.UnityAction afterToDo = () =>
        {
            temp();
        };
        mainProcessRunner.RunAsQueued(enter(), afterToDo);        
    }
    
    public override void ProcessEnd()
    {
        MemberDetail.target.ClearHeroIconsFeatures();
        MemberDetail.target.MemberDetailCanvas.gameObject.SetActive(false);
        MemberDetail.target.MemberInfoT.gameObject.SetActive(false);
        MonsterBox.target.MonsterBoxWholeT.gameObject.SetActive(false);
    }
    
    readonly Vector3 screenPos = new Vector3(0.23f, 0.37f, ModelShower._nearClipPlane);
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
