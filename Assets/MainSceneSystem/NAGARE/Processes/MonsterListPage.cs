using UnityEngine;
using mainMenu;
using dataAccess;
using System.Collections.Generic;
using UniRx;

public class MonsterListPage : MainSceneProcess
{
    public bool loadFinished;
    
    public MonsterListPage()
    {
        Step = MainSceneStep.MonsterList;
        EelementsInherit(PreScene.target);
    }

    public override void ProcessEnter()
    {
        loadFinished = false;
        switch (Account._AccInfo.accountprogress)
        {
            case PlayerAccountProgressStep.Freedom:
                break;
            case PlayerAccountProgressStep.justCreated:
                break;
            case PlayerAccountProgressStep.Tutorial:
                MyMonsters.LoadTutorial();
                break;
        }
        
        PageTo.Go(MainSceneStep.MonsterList);
        MonsterBox.target.Open(true);
        MonsterBox.DisplayMonsterIcons(true);
        MemberDetail.target.AddHeroIconFeaturesToMonsterBox();// 该处理紧随MonsterBox.DisplayMonsterIcons之后
        // 相机的这个锁定，在所有技能展示结束后应该是按以下这两行的标准进行归位。 
        _CameraManager.Assign_SToEMode(MemberDetail.target.MemDetailWatchPos.position, MemberDetail.target.MemDetailTargetPos, 3f, 15f);
        MemberDetail.target.RefreshMemberDetailPageByFocusingChar();
        loadFinished = true;
    }
    
    public override void ProcessEnd()
    {
        MemberDetail.target.ClearHeroIconsFeatures();
        MemberDetail.target.MemberInfoT.gameObject.SetActive(false);
        MonsterBox.target.Open(false);
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
