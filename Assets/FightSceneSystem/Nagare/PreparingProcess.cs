using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using FightScene;

public class PreparingProcess : FSceneProcess
{
    public PreparingProcess(NetFightScene _NetFightScene)
    {
        Step = SceneStep.Preparing;
        nextProcessStep = SceneStep.StoryBeforeFight;
        EelementsInherit(_NetFightScene);
    }
    
    public IEnumerator EnterProcess()
    {
        LoadingCanvas.target.DarkOff(1f);
        FightLoadError.Instance.FightLoadErrors.Clear();
        if (FightSceneNote.nextBattle != null)
        {
            mainProcessRunner.Run(RealTimeGameProcessManager.target.LoadGame(FightSceneNote.nextBattle));
        }
        EffectsManager.INIEffectsPool("hit_ground", null, 3);
        EffectsManager.INIEffectsPool("wallCrack", null, 3);
        LoadingCanvas.target.LightUp();
        yield break;
    }
    
    public override void ProcessEnter()
    {
        FightScene.PreparingCanvas.gameObject.SetActive(true);
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
            SceneManager.LoadScene(1);//也就是说这个地方是为了阻止进入下一步
        }
        FightLoadError.Instance.FightLoadErrors.Clear();
    }
    
    public override bool CanEnterOtherProcess()
    {
        return FightScene.LoadStageFinished.Value
            && RealTimeGameProcessManager.target.FightTeam1.IfAllCharsPreparedForBattle() 
            && RealTimeGameProcessManager.target.FightTeam2.IfAllCharsPreparedForBattle();
    }
}
