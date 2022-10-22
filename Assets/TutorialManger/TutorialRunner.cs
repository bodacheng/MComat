using System.Collections.Generic;

public class TutorialRunner
{
    static TutorialRunner instance;
    public static TutorialRunner Main
    {
        get
        {
            if (instance == null)
            {
                instance = new TutorialRunner();
            }
            return instance;
        }
    }
    
    TutorialProcess currentProcess;

    // 这个结构代表了教程的顺序, 很大的特点在于可加入重复元素。典型的如后退菜单
    readonly List<TutorialProcess> TutorialProcesses = new ();
    
    void GenerateStep1Tutorial()
    {
        var goToUnitList = new GoToUnitList();
        var openSkillEdit = new OpenSkillEdit("1");
        var skillEditTry = new SkillEditTry();
        
        TutorialProcesses.Clear();
        TutorialProcesses.Add(goToUnitList);
        TutorialProcesses.Add(openSkillEdit);
        TutorialProcesses.Add(skillEditTry);
    }

    void GenerateStep2Tutorial()
    {
        var goToStages = new GoToStages();
        var goToStageOne = new GoToStageOne();
        
        bool StageOneFinished()
        {
            return PlayerAccountInfo.Me.TutorialProgress == "StageOneFinished";
        }
        
        var waitFighting = new WaitProcess(StageOneFinished);
        
        TutorialProcesses.Clear();
        TutorialProcesses.Add(goToStages);
        TutorialProcesses.Add(goToStageOne);
        TutorialProcesses.Add(waitFighting);
    }

    void GenerateStep3Tutorial()
    {
        // gacha
        // new character skill edit
        // team edit
        // arcade stage 2

        var tryGotcha = new TryGotcha();
        
        TutorialProcesses.Clear();
        TutorialProcesses.Add(tryGotcha);
    }

    void GenerateStep4Tutorial()
    {
        var goToUnitList = new GoToUnitList();
        var openSkillEdit = new OpenSkillEdit("2");
        var skillEditTry = new SkillEditTry();
        
        TutorialProcesses.Clear();
        TutorialProcesses.Add(goToUnitList);
        TutorialProcesses.Add(openSkillEdit);
        TutorialProcesses.Add(skillEditTry);
    }

    void GenerateStep5Tutorial()
    {
        var teamEdit = new TeamEdit();
        var goToStageOne = new GoToStageOne();
        
        TutorialProcesses.Clear();
        TutorialProcesses.Add(teamEdit);
        TutorialProcesses.Add(goToStageOne);// 这个环节已经可有可无。如果玩家在队伍编辑后直接退出游戏重开，将获得自由
    }
    
    public void ProcessNagare()
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

    public void StartToMove()
    {
        ChangeProcess(TutorialProcesses[0]);
    }

    void MoveToNext()
    {
        ChangeProcess(TutorialProcesses.Count > 1 ? TutorialProcesses[1] : null);
        TutorialProcesses.RemoveAt(0);
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
        switch (PlayerAccountInfo.Me.TutorialProgress)
        {
            case "Started":
                Main.GenerateStep1Tutorial();
                Main.StartToMove();
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
            case "TeamEditFinished":// 
                break;
            default:
                break;
        }
    }
}
