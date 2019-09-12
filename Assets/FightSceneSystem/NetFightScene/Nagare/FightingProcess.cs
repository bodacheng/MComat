using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using System;

public class FightingProcess : NagareProcess
{
    List<Transform> outter_watchetargets = new List<Transform>();
    List<Transform> inner_watchetargets = new List<Transform>();
    IDictionary<Team, List<Data_Center>> AllMembers = new Dictionary<Team, List<Data_Center>>();//双方队伍人员字典，和netfightscene模块里同名变量统一。
    
    public FightingProcess(NetFightScene _NetFightScene,FightSceneProcessesRunner fightSceneProcessesRunner)
    {
        this.thisProcessStep = SceneStep.Fighting;
        this.nextProcessStep = SceneStep.FightOver;
        EelementsInherit(_NetFightScene,fightSceneProcessesRunner);
    }
    
    public override bool canEnterNextProcess()
    {
        if (this.fightLogger.ifGameOver())
            return true;
        else return false;
    }
    
    public override void ProcessEnter()
    {
        AllMembers.Add(Team.player1,_RealTimeGameProcessManager.FightTeam1.teamMembers.values);
        AllMembers.Add(Team.player2,_RealTimeGameProcessManager.FightTeam2.teamMembers.values);

        this.BoundaryControllByGod.AllMembers = AllMembers;
        this.fightLogger.ReadyToLog(AllMembers);
        
        foreach (KeyValuePair<Team,List<Data_Center>> _set in AllMembers)
        {
            foreach (Data_Center _char in _set.Value)
            {
                _char.Sensor.TeamMembers = AllMembers;
            }
        }

        _NetFightScene.pressedStartButton();
        _NetFightScene.FightCanvas.gameObject.SetActive(true);
        fightOverControl.FightOverCanvas.gameObject.SetActive(false);
        _NetFightScene.PreparingCanvas.gameObject.SetActive(false);
        
        if (RealTimeGameProcessManager.focusingChar != null)
        {
            cameraManager.Assign_Camera(Camera_Mode_Num.CertainYAntiVibrationCamera, new List<Transform>() { RealTimeGameProcessManager.focusingChar.WholeT });
            cameraManager.current_Camera_Mode.setMeCenter(RealTimeGameProcessManager.focusingChar.WholeT);
        }
    }
    
    public override void ProcessEnd()
    {
        _NetFightScene.FightCanvas.gameObject.SetActive(false);
        _NetFightScene.PreparingCanvas.gameObject.SetActive(false);
        mainProcessRunner.triggerMainProcess(finalMoment(this.fightLogger.getWinner()));
    }
    
    public override void localUpdate()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            _NetFightScene.PauseScene();
        }
        
        switch (FightSceneNote.Instance.nextBattle.fightModeType)
        {
            case fightModeType.combat:
                _RealTimeGameProcessManager.FightingStepProcess();
                switch (BoundaryControllByGod.boundaryMode)
                {
                    case BoundaryMode.Round:
                        //_BoundaryControllByGod.SUOQUANER(alivemembercount);
                        BoundaryControllByGod.RoundModeGodControll(BoundaryControllByGod.battleRingCenter, BoundaryControllByGod.BattleRingRadius);
                        break;
                    case BoundaryMode.None:
                        BoundaryControllByGod.RoundBattleFieldNormalControl(BoundaryControllByGod.battleRingCenter, 24);
                        break;
                }
                break;
        }
        this.fightLogger.TeamMemberDeathProcessing(AllMembers);
    }

    IEnumerator finalMoment(Team winner)
    {
        Time.timeScale = 0.4f;
        yield return new WaitForSeconds(2f);
        
        List<Data_Center> winners = new List<Data_Center>();
        if (winner == Team.player1)
            winners = AllMembers[Team.player1];
        if (winner == Team.player2)
            winners = AllMembers[Team.player2];
            
        foreach (Data_Center _one in winners)
        {
            if (_one.BO_Health._health > 0)
            {
                _one.AIStateRunner.changeState("Victory");
            }
        }
        Time.timeScale = 1f;
    }
}
