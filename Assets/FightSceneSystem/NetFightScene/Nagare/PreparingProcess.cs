using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        mainProcessRunner.Run(_NetFightScene.LoadGame(FightSceneNote.Instance.nextBattle));//这个环节的完成flag就是ifLoadStageFinished()
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
        _NetFightScene.LoadStageFinished.Value = false;
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
        AutoMoveToNext |= (_NetFightScene.LoadStageFinished.Value && _RealTimeGameProcessManager.FightTeam1.IfAllCharsPreparedForBattle() && _RealTimeGameProcessManager.FightTeam2.IfAllCharsPreparedForBattle());
    }
}
