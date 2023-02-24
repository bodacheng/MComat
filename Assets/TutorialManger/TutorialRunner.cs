using System.Collections.Generic;
using mainMenu;

public class TutorialRunner
{
    static TutorialRunner _instance;
    public static TutorialRunner Main
    {
        get
        {
            if (_instance == null)
            {
                _instance = new TutorialRunner();
            }
            return _instance;
        }
    }
    
    TutorialProcess currentProcess;
    readonly List<TutorialProcess> _tutorialProcesses = new ();
    
    void GenerateStep1Tutorial()
    {
        var goToUnitList = new GoTo("unit");
        var openSkillEdit = new OpenSkillEdit("3");
        var skillEditTry = new SkillEditTry("openInstruction1");
        var forceBack = new ForceBack(
            () => ProcessesRunner.Main.currentProcess.Step == MainSceneStep.FrontPage
        );
        
        _tutorialProcesses.Clear();
        _tutorialProcesses.Add(goToUnitList);
        _tutorialProcesses.Add(openSkillEdit);
        _tutorialProcesses.Add(skillEditTry);
        _tutorialProcesses.Add(forceBack);
    }
    
    void GenerateStep2Tutorial()
    {
        var goTo = new GoTo("arcade");
        var teamEdit1 = new TeamEdit("teamEdit1");
        var goToStageOne = new GoToStageOne();
        
        bool StageOneFinished()
        {
            return PlayerAccountInfo.Me.tutorialProgress == "StageOneFinished";
        }
        
        var waitFighting = new WaitProcess(StageOneFinished);
        
        _tutorialProcesses.Clear();
        _tutorialProcesses.Add(goTo);
        _tutorialProcesses.Add(teamEdit1);
        _tutorialProcesses.Add(goToStageOne);
        _tutorialProcesses.Add(waitFighting);
    }

    void GenerateStep3Tutorial()
    {
        var goTo = new GoTo("gotcha");
        var tryGotcha = new TryGotcha();
        var forceBack = new ForceBack(
            () => ProcessesRunner.Main.currentProcess.Step == MainSceneStep.FrontPage
        );
        
        _tutorialProcesses.Clear();
        _tutorialProcesses.Add(goTo);
        _tutorialProcesses.Add(tryGotcha);
        _tutorialProcesses.Add(forceBack);
    }

    void GenerateStep4Tutorial()
    {
        var goTo = new GoTo("unit");
        var openSkillEdit = new OpenSkillEdit("2");
        var skillEditTry = new SkillEditTry("openInstruction2");
        var forceBack = new ForceBack(
            () => ProcessesRunner.Main.currentProcess.Step == MainSceneStep.FrontPage
        );
        _tutorialProcesses.Clear();
        _tutorialProcesses.Add(goTo);
        _tutorialProcesses.Add(openSkillEdit);
        _tutorialProcesses.Add(skillEditTry);
        _tutorialProcesses.Add(forceBack);
    }
    
    void GenerateStep5Tutorial()
    {
        var goTo = new GoTo("arcade");
        var teamEdit2 = new TeamEdit("teamEdit2");
        var goToStageOne = new GoToStageOne();
        
        _tutorialProcesses.Clear();
        _tutorialProcesses.Add(goTo);
        _tutorialProcesses.Add(teamEdit2);
        _tutorialProcesses.Add(goToStageOne);// 这个环节已经可有可无。如果玩家在队伍编辑后直接退出游戏重开，将获得自由
    }
    
    public void Process()
    {
        if (currentProcess != null)
        {
            currentProcess.LocalUpdate();
            if (currentProcess.CanEnterOtherProcess()) // && currentProcess.nextProcessStep != MainSceneStep.None
            {
                MoveToNext();
            }
        }
    }

    void StartToMove()
    {
        ChangeProcess(_tutorialProcesses[0]);
    }

    void MoveToNext()
    {
        ChangeProcess(_tutorialProcesses.Count > 1 ? _tutorialProcesses[1] : null);
        _tutorialProcesses.RemoveAt(0);
    }
    
    void ChangeProcess(TutorialProcess nextProcess)
    {
        currentProcess?.ProcessEnd();
        currentProcess = nextProcess;
        currentProcess?.ProcessEnter();
    }
    
    // 所有的教程链都是以FrontPage为起点
    public void TutorialCheck()
    {
        // 在以下的分歧之前，账户信息必须是最新，否则反应不到账户真实进度。
        switch (PlayerAccountInfo.Me.tutorialProgress)
        {
            case "Started":
                Main.GenerateStep1Tutorial();
                Main.StartToMove();
                PlayFabReadClient.DontShowFrontFight = "false";
                break;
            case "SkillEditFinished": // 技能编辑教程结束 
                Main.GenerateStep2Tutorial();
                Main.StartToMove();
                break;
            case "StageOneFinished": // 第一关结束
                GenerateStep3Tutorial();
                Main.StartToMove();
                break;
            case "GotchaFinished":
                GenerateStep4Tutorial();
                Main.StartToMove();
                break;
            case "SkillEditFinished2":
                GenerateStep5Tutorial();
                Main.StartToMove();
                break;
            case "Finished":
                PlayFabReadClient.DontShowFrontFight = "true";
                break;
            default:
                break;
        }
    }
}
