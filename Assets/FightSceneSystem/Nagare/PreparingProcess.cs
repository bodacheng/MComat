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
        
        Sensor.ClearFightingMember();
        yield return RTFightManager.target.LoadUnits(NetFightScene.Fight);
        
        RTFightManager.target.SetGame(NetFightScene.Fight);
        
        var TeamMembers = new Dictionary<TeamConfig, List<Data_Center>>();
        DicAdd<TeamConfig, List<Data_Center>>.Add(TeamMembers, RTFightManager.target.heroTeamConfig, RTFightManager.target.Team1Members.GetValues());
        DicAdd<TeamConfig, List<Data_Center>>.Add(TeamMembers, RTFightManager.target.EnemyTeamConfig, RTFightManager.target.Team2Members.GetValues());
        FightLogger.value.ReadyToLog(TeamMembers);
        
        EffectsManager.INIEffectsPool("hit_ground", null, 3);
        EffectsManager.INIEffectsPool("wallCrack", null, 3);
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
        
        if (NetFightScene.Fight.GetEventType() != FightEventType.Screensaver)
        {
            PopupLayer.LightUp(1f);
        }
    }
    
    public override bool CanEnterOtherProcess()
    {
        return NetFightScene.target.LoadStageFinished.Value
            && IfAllCharsPreparedForBattle(RTFightManager.target.Team1Members) 
            && IfAllCharsPreparedForBattle(RTFightManager.target.Team2Members);
    }
    
    bool IfAllCharsPreparedForBattle(MultiDict<int, int, Data_Center> TeamMembers)
    {
        foreach (var oneMember in TeamMembers.GetValues())
        {
            if (!oneMember.IfPreparedForBattle())
                return false;
        }
        return true;
    }
}
