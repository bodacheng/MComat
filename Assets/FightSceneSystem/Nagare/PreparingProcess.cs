using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using FightScene;

public class PreparingProcess : NagareProcess
{
    public PreparingProcess(NetFightScene _NetFightScene,FightSceneProcessesRunner fightSceneProcessesRunner)
    {
        thisProcessStep = SceneStep.Preparing;
        nextProcessStep = SceneStep.StoryBeforeFight;
        EelementsInherit(_NetFightScene,fightSceneProcessesRunner);
    }
    
    public IEnumerator EnterProcess()
    {
        LoadingCanvas.target.DarkOff(1f);
        FightLoadError.Instance.FightLoadErrors.Clear();
        if (FightSceneNote.nextBattle != null)
        {
            mainProcessRunner.Run(RealTimeGameProcessManager.target.LoadGame(FightSceneNote.nextBattle));
        }
        EffectsManager.IniEffectsPool("wallCrack", null, 3);
        LoadingCanvas.target.LightUp();
        yield break;
    }
    
    public override void ProcessEnter()
    {
        mainProcessRunner.Run(EnterProcess());
    }
    
    public override void ProcessEnd()
    {
        FightScene.LoadStageFinished.Value = false;
        if (FightLoadError.Instance.FightLoadErrors.Count > 0)
        {
            foreach (string error in FightLoadError.Instance.FightLoadErrors)
            {
                Debug.Log("双方队伍读取后问题： " + error);
            }
            SceneManager.LoadScene(1);//也就是说这个地方是为了阻止进入下一步呗？
        }
        FightLoadError.Instance.FightLoadErrors.Clear();
    }
    
    public override void LocalUpdate()
    {
        AutoMoveToNext |= (FightScene.LoadStageFinished.Value && RealTimeGameProcessManager.target.FightTeam1.IfAllCharsPreparedForBattle() && RealTimeGameProcessManager.target.FightTeam2.IfAllCharsPreparedForBattle());
    }
}
