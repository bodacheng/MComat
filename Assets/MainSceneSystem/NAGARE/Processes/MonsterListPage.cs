using UnityEngine;
using mainMenu;
using Cysharp.Threading.Tasks;
using dataAccess;
using System.Collections.Generic;
using UniRx;

public class MonsterListPage : MainSceneProcess
{
    public bool loadFinished;

    ReactiveProperty<int> itemsLoadFinished = new ReactiveProperty<int>(0);
    void ItemsLoadFinished(int value)
    {
        itemsLoadFinished.Value = value;
    }

    public MonsterListPage()
    {
        Step = MainSceneStep.MonsterList;
        EelementsInherit(PreScene.target);
    }

    public async UniTask Enter()
    {
        PageTo.Go(MainSceneStep.MonsterList);
        MonsterBox.DisplayMonsterIcons(true);
        MemberDetail.target.AddHeroIconFeaturesToMonsterBox();// 该处理紧随MonsterBox.DisplayMonsterIcons之后
        // 相机的这个锁定，在所有技能展示结束后应该是按以下这两行的标准进行归位。 
        _CameraManager.Assign_SToEMode(MemberDetail.target.MemDetailWatchPos.position, MemberDetail.target.MemDetailTargetPos, 3f, 15f);
        MemberDetail.target.MemberDetailCanvas.gameObject.SetActive(true);
        MonsterBox.target.MonsterBoxWholeT.gameObject.SetActive(true);
        MemberDetail.target.RefreshMemberDetailPageByFocusingChar();
        loadFinished = true;
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

        missionWatcher = new MissionWatcher(
            new List<ReactiveProperty<int>>() {
                itemsLoadFinished
            },
            Enter(),
            () => { Debug.Log("错误，怎么办？"); }
        );
    }
    
    public override void ProcessEnd()
    {
        missionWatcher.DisposeAll();
        ItemsLoadFinished(0);
        MemberDetail.target.ClearHeroIconsFeatures();
        MemberDetail.target.MemberDetailCanvas.gameObject.SetActive(false);
        MemberDetail.target.MemberInfoT.gameObject.SetActive(false);
        MonsterBox.target.MonsterBoxWholeT.gameObject.SetActive(false);
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
