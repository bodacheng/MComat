using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using FightScene;
using System.Collections.Generic;
using UniRx;

public class PreparingProcess : FSceneProcess
{
    public PreparingProcess()
    {
        Step = SceneStep.Preparing;
        nextProcessStep = SceneStep.StoryBeforeFight;
    }
    
    IEnumerator EnterProcess()
    {
        AddressablesLogic.Essentials();
        
        RTFightManager.target._CameraManager.Assign_Camera(C_Mode.NULL, null,null);
        CameraManager._camera.transform.position = CameraManager._StartPosRef.transform.position;
        CameraManager._camera.transform.rotation = CameraManager._StartPosRef.transform.rotation;
        
        FightLoadError.Instance.FightLoadErrors.Clear();
        BoundaryControllByGod.target.ChangeBackGround(NetFightScene.Fight.BattleGroundID);
        Sensor.ClearFightingMember();
        yield return RTFightManager.target.LoadUnits(NetFightScene.Fight);
        
        var teamMembers = new Dictionary<TeamConfig, List<Data_Center>>();
        DicAdd<TeamConfig, List<Data_Center>>.Add(teamMembers, RTFightManager.target.heroTeamConfig, RTFightManager.target.team1.TeamMembers.GetValues());
        DicAdd<TeamConfig, List<Data_Center>>.Add(teamMembers, RTFightManager.target.EnemyTeamConfig, RTFightManager.target.team2.TeamMembers.GetValues());
        FightLogger.value.ReadyToLog(teamMembers);
        
        EffectsManager.INIEffectsPool("hit_ground", null, 3);
        EffectsManager.INIEffectsPool("wallCrack", null, 3);
        
        RTFightManager.target.team1.Auto = NetFightScene.Fight.team1Auto;
        RTFightManager.target.team2.Auto = NetFightScene.Fight.team2Auto;
        RTFightManager.target.team1.TeamMode = NetFightScene.Fight.Team1Mode;
        RTFightManager.target.team2.TeamMode = NetFightScene.Fight.Team2Mode;
        RTFightManager.target.team1.teamConfig = RTFightManager.target.heroTeamConfig;
        RTFightManager.target.team2.teamConfig = RTFightManager.target.EnemyTeamConfig;
        
        RTFightManager.target.team1.TeamStandPoints = NetFightScene.target.Team1StandPoints;
        RTFightManager.target.team2.TeamStandPoints = NetFightScene.target.Team2StandPoints;
        
        if (NetFightScene.Fight.EventType == FightEventType.Screensaver)
        {
            RTFightManager.target.team1.TurnAllUnitsInvincible(true);
            RTFightManager.target.team2.TurnAllUnitsInvincible(true);
        }else{
            RTFightManager.target.team1.TurnAllUnitsInvincible(false);
            RTFightManager.target.team2.TurnAllUnitsInvincible(false);
        }
        
        switch (RTFightManager.target.team1.TeamMode)
        {
            case TeamMode.multiRaid:
                RTFightManager.target.team1.Initialize_Multi(NetFightScene.Fight.Team1HpRate, NetFightScene.Fight.team1CGMode);
                RTFightManager.target.team1StartUnit = RTFightManager.target.team1.ToStartPos_Multi();
                break;
            case TeamMode.rotation:
                RTFightManager.target.team1.TeamsIni_Rotate(NetFightScene.Fight.Team1HpRate, NetFightScene.Fight.team1CGMode);
                RTFightManager.target.team1StartUnit = RTFightManager.target.team1.ToStartPos_Rotate();
                break;
        }
        
        switch (RTFightManager.target.team2.TeamMode)
        {
            case TeamMode.multiRaid:
                RTFightManager.target.team2.Initialize_Multi(NetFightScene.Fight.Team2HpRate, NetFightScene.Fight.team2CGMode);
                RTFightManager.target.team2StartUnit = RTFightManager.target.team2.ToStartPos_Multi();
                break;
            case TeamMode.rotation:
                RTFightManager.target.team2.TeamsIni_Rotate(NetFightScene.Fight.Team2HpRate, NetFightScene.Fight.team2CGMode);
                RTFightManager.target.team2StartUnit = RTFightManager.target.team2.ToStartPos_Rotate();
                break;
        }
        
        RTFightManager.target.team1.RMode_Unit.Subscribe(x =>
            {
                RTFightManager.target.CameraAdjustment(RTFightManager.playerTeam);
            }
        ).AddTo(RTFightManager.target.disposables);
        
        RTFightManager.target.team2.RMode_Unit.Subscribe(x =>
            {
                RTFightManager.target.CameraAdjustment(RTFightManager.playerTeam);
            }
        ).AddTo(RTFightManager.target.disposables);
        
        RTFightManager.target.SetGame(NetFightScene.Fight);
        FightingStepLayer.Open(false);
    }
    
    public override void ProcessEnter()
    {
        PopupLayer.DarkOff(NetFightScene.target.T.gameObject, 1, 0);
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
        PopupLayer.LightUp(1f);
    }
    
    public override bool CanEnterOtherProcess()
    {
        return NetFightScene.target.LoadStageFinished.Value
               && RTFightManager.target.team1.IfAllUnitsPreparedForBattle()
               && RTFightManager.target.team2.IfAllUnitsPreparedForBattle();
    }
}
