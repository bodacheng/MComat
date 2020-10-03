using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using FightScene;
using System.Collections.Generic;

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
        LoadingCanvas.target.DarkOffDirectly(1f);
        RealTimeGameProcessManager.target._CameraManager.Assign_SToEMode(FightScene.WatchTeam2.position, FightScene.Team2StandPoints[0], 3f, 50f);
        FightLoadError.Instance.FightLoadErrors.Clear();
        if (FightSceneNote.nextBattle != null)
        {
            yield return RealTimeGameProcessManager.target.LoadGame(FightSceneNote.nextBattle);
        }
        RealTimeGameProcessManager.target.AllMembers.Clear();
        DicAdd<Team, List<Data_Center>>.Add(RealTimeGameProcessManager.target.AllMembers, Team.player1, RealTimeGameProcessManager.target.FightTeam1.TeamMembers.values);
        DicAdd<Team, List<Data_Center>>.Add(RealTimeGameProcessManager.target.AllMembers, Team.player2, RealTimeGameProcessManager.target.FightTeam2.TeamMembers.values);
        fightLogger.ReadyToLog(RealTimeGameProcessManager.target.AllMembers);
        foreach (KeyValuePair<Team, List<Data_Center>> _set in RealTimeGameProcessManager.target.AllMembers)
        {
            foreach (Data_Center _char in _set.Value)
            {
                _char.Sensor.TeamMembers = RealTimeGameProcessManager.target.AllMembers;
                _char.IsDead.Value = false;
            }
        }
        EffectsManager.INIEffectsPool("hit_ground", null, 3);
        EffectsManager.INIEffectsPool("wallCrack", null, 3);
        LoadingCanvas.target.LightUp();
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
