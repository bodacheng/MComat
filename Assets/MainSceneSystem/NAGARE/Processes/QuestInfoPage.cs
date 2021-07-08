using System.Collections;
using mainMenu;
using UnityEngine;

public class QuestInfoPage : MainSceneProcess
{
    FightInfo loadFight;

    // 这个进程需要有能力把加载的关卡信息记住，因为牵扯到从这个画面迁移到队伍编辑画面后再返回的问题
    public IEnumerator EnterProcess(FightInfo stage)
    {
        loadFight = stage;
        yield return ModelShower.target.ShowMyModel(null);
        PageTo.Go(MainSceneStep.QuestInfo);
        FightPreparePage.target.QuestPreparePageCanvas.gameObject.SetActive(true);
        GetReadyForQuestInfoPage();
    }
    
    public QuestInfoPage()
    {
        Step = MainSceneStep.QuestInfo;
        EelementsInherit(PreScene.target);
    }
    
    public override void ProcessEnter<T>(T t)
    {
        mainProcessRunner.RunAsQueued(EnterProcess(t as FightInfo));
    }
    
    public override void ProcessEnd()
    {
        FightPreparePage.target.QuestPreparePageCanvas.gameObject.SetActive(false);
    }

    // 这个函数目前是固定使用“默认队伍配置”
    public void GetReadyForQuestInfoPage()
    {
        FightPreparePage.target.QuestName.text = loadFight.battleNameJPG;
        switch (loadFight.eventType)
        {
            case FightEventType.Arena:
                void GoToTeamEdit_Arena()
                {
                    PreScene.target.trySwitchToStep(MainSceneStep.TeamEditFront, "arena", true);
                }
                FightPreparePage.target.EditTeamButton.onClick.RemoveAllListeners();
                FightPreparePage.target.EditTeamButton.onClick.AddListener(GoToTeamEdit_Arena);
                break;
            case FightEventType.Quest:
                void GoToTeamEdit_Arcade()
                {
                    Debug.Log("arcade");
                    PreScene.target.trySwitchToStep(MainSceneStep.TeamEditFront, "arcade", true);
                }
                FightPreparePage.target.EditTeamButton.onClick.RemoveAllListeners();
                FightPreparePage.target.EditTeamButton.onClick.AddListener(GoToTeamEdit_Arcade);
                break;
        }
        FightPreparePage.target.QuestPreparePageCanvas.gameObject.SetActive(true);
        FightPreparePage.target.StageMembersInfoShow(loadFight);
        FightPreparePage.target.BeginFight.onClick.RemoveAllListeners();
        void Go()
        {
            FightLoad.Go(loadFight, true);
        }
        FightPreparePage.target.BeginFight.onClick.AddListener(Go);
    }
}
