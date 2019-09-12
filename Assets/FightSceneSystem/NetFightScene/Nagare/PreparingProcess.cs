using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class PreparingProcess : NagareProcess
{
    public PreparingProcess(NetFightScene _NetFightScene,FightSceneProcessesRunner fightSceneProcessesRunner)
    {
        this.thisProcessStep = SceneStep.Preparing;
        this.nextProcessStep = SceneStep.StoryBeforeFight;
        EelementsInherit(_NetFightScene,fightSceneProcessesRunner);
    }
    
    public IEnumerator enterProcess()
    {
        loadingCanvas.DarkOff(1f);
        FightLoadError.Instance.FightLoadErrors.Clear();
        switch (_NetFightScene._SceneMode)
        {
            case SceneMode.MyPetsFight:
                if (FightSceneNote.Instance.nextBattle != null)
                {
                    mainProcessRunner.triggerMainProcess(_NetFightScene.loadGame(FightSceneNote.Instance.nextBattle));//这个环节的完成flag就是ifLoadStageFinished()
                }
            break;
            case SceneMode.QuestFight:
                if (FightSceneNote.Instance.nextBattle != null)
                {
                    mainProcessRunner.triggerMainProcess(_NetFightScene.loadGame(FightSceneNote.Instance.nextBattle));//这个环节的完成flag就是ifLoadStageFinished()
                }
            break;
        }
        loadingCanvas.LightUp();
        yield break;
    }

    public override bool canEnterNextProcess()
    {
        return _NetFightScene.ifLoadStageFinished() && _RealTimeGameProcessManager.FightTeam1.ifAllCharsPreparedForBattle() && _RealTimeGameProcessManager.FightTeam2.ifAllCharsPreparedForBattle();
    }
    
    public override void ProcessEnter()
    {
        this.mainProcessRunner.triggerMainProcess(enterProcess());
    }
    
    public override void ProcessEnd()
    {
        _NetFightScene.resetLoadStageFinishedFlag();
        if (FightLoadError.Instance.FightLoadErrors.Count > 0)
        {
            foreach (string error in FightLoadError.Instance.FightLoadErrors)
                Debug.Log("双方队伍读取后问题： " + error);
            
            SceneManager.LoadScene(1);//也就是说这个地方是为了阻止进入下一步呗？
        }
        FightLoadError.Instance.FightLoadErrors.Clear();
    }
}
