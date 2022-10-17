using System.Collections.Generic;
using PlayFab.ClientModels;
using Newtonsoft.Json;
using FightScene;

/// <summary>
/// 
/// </summary>
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
    
    public void GenerateTutorial()
    {
        var goToUnitList = new GoToUnitList();
        var openSkillEdit = new OpenSkillEdit();
        var SkillEditTry = new SkillEditTry();
        var aLineConfirm = new ALineConfirm();
        var goToStages = new GoToStages();
        var goToStageOne = new GoToStageOne();
        var goToTeamEdit = new GoToTeamEdit();
        var clickTeamEditSlotOne = new ClickTeamEditSlotOne();
        var ChooseAdamToSlot1 = new ChooseAdamToSlot1();
        var confirmQuest1 = new ConfirmQuest1();
        
        bool StartedFighting()
        {
            return FSceneProcessesRunner.Main.currentProcess.Step == SceneStep.Fighting; 
        }
        var waitForStage1Loaded = new WaitProcess(StartedFighting);
        
        TutorialProcesses.Add(goToUnitList);
        TutorialProcesses.Add(openSkillEdit);
        TutorialProcesses.Add(SkillEditTry);
        //TutorialProcesses.Add(aLineConfirm);
        TutorialProcesses.Add(goToStages);
        TutorialProcesses.Add(goToStageOne);
        //TutorialProcesses.Add(goToTeamEdit);
        // TutorialProcesses.Add(clickTeamEditSlotOne);
        // TutorialProcesses.Add(ChooseAdamToSlot1);
        // TutorialProcesses.Add(confirmQuest1);
        // TutorialProcesses.Add(waitForStage1Loaded);
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
        if (currentProcess is SkillEditTry)
        {
            PlayFabReadClient.UpdateUserData(
                new UpdateUserDataRequest()
                {
                    Data = new Dictionary<string, string>()
                    {
                        { "TutorialProgress", "SkillEditFinished" }
                    }
                },
                (x) =>
                { }
            );
        }
        else if (currentProcess is GoToStageOne)
        {
            
        }
        
        currentProcess = nextProcess;
        currentProcess?.ProcessEnter();
    }
}
