using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using FightScene;
using System.Collections.Generic;

public class PreparingProcess : FSceneProcess
{
    public PreparingProcess()
    {
        Step = SceneStep.Preparing;
        nextProcessStep = SceneStep.StoryBeforeFight;
    }
    
    IEnumerator EnterProcess()
    {
        //RealTimeGameProcessManager.target._CameraManager.Assign_SToEMode(NetFightScene.target.WatchTeam2.position, NetFightScene.target.Team2StandPoints[0], 3f, 50f);
        CameraManager._camera.transform.position = CameraManager._StartPosRef.transform.position;
        CameraManager._camera.transform.rotation = CameraManager._StartPosRef.transform.rotation;
        RTFightManager.target._CameraManager.Assign_Camera(C_Mode.NULL, null);
        //RealTimeGameProcessManager.target.CameraParaAdjustment(RealTimeGameProcessManager.playerTeam);
        FightLoadError.Instance.FightLoadErrors.Clear();
        
        BoundaryControllByGod.target.ChangeBackGround(NetFightScene.Fight.BattleGroundID);
        
        FightingStepLayer FightingStepLayer = FightingStepLayer.Open();
        RTFightManager.target.team1 = FightingStepLayer.TeamUI1Manager;
        RTFightManager.target.team2 = FightingStepLayer.TeamUI2Manager;
        RTFightManager.target.team1.TeamStandPoints = NetFightScene.target.Team1StandPoints;
        RTFightManager.target.team2.TeamStandPoints = NetFightScene.target.Team2StandPoints;
        RTFightManager.target.team1.TeamMode = NetFightScene.Fight.Team1Mode;
        RTFightManager.target.team2.TeamMode = NetFightScene.Fight.Team2Mode;
        
        yield return RTFightManager.target.LoadUnits(NetFightScene.Fight);
        RTFightManager.target.SetGame(NetFightScene.Fight);
        
        var TeamMembers = new Dictionary<TeamConfig, List<Data_Center>>();
        DicAdd<TeamConfig, List<Data_Center>>.Add(TeamMembers, RTFightManager.target.heroTeamConfig, RTFightManager.target.Team1Members.GetValues());
        DicAdd<TeamConfig, List<Data_Center>>.Add(TeamMembers, RTFightManager.target.EnemyTeamConfig, RTFightManager.target.Team2Members.GetValues());
        FightOverControl.target.logger.ReadyToLog(TeamMembers);
        
        EffectsManager.INIEffectsPool("hit_ground", null, 3);
        EffectsManager.INIEffectsPool("wallCrack", null, 3);
    }
    
    public override void ProcessEnter()
    {
        SingleThreadProcesser.backup.RunFreely(EnterProcess());
    }
    
    public override void ProcessEnd()
    {
        NetFightScene.target.LoadStageFinished.Value = false;
        if (FightLoadError.Instance.FightLoadErrors.Count > 0)
        {
            foreach (string error in FightLoadError.Instance.FightLoadErrors)
            {
                Debug.Log("双方队伍读取后问题： " + error);
            }
            SceneManager.LoadScene(1);//也就是说这个地方是为了阻止进入下一步
        }
        FightLoadError.Instance.FightLoadErrors.Clear();
        
        if (NetFightScene.Fight.GetEventType() != FightEventType.Screensaver)
        {
            Debug.Log("jhslfs");
            PopupLayer.LightUp(1f);
        }
    }
    
    public override bool CanEnterOtherProcess()
    {
        return NetFightScene.target.LoadStageFinished.Value
            && RTFightManager.target.team1.IfAllCharsPreparedForBattle(RTFightManager.target.Team1Members) 
            && RTFightManager.target.team2.IfAllCharsPreparedForBattle(RTFightManager.target.Team2Members);
    }
}
