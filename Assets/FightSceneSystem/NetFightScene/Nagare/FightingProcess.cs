using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using System;

public class FightingProcess : NagareProcess
{
    private readonly IDictionary<Team, List<Data_Center>> AllMembers = new Dictionary<Team, List<Data_Center>>();//双方队伍人员字典，和netfightscene模块里同名变量统一。

    public FightingProcess(NetFightScene _NetFightScene, FightSceneProcessesRunner fightSceneProcessesRunner)
    {
        this.thisProcessStep = SceneStep.Fighting;
        this.nextProcessStep = SceneStep.FightOver;
        EelementsInherit(_NetFightScene, fightSceneProcessesRunner);
        fightLogger.gameOver.Subscribe(x => 
            {
                AutoMoveToNext |= x == true;
            }
        );
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
    }
    
    public override void ProcessEnd()
    {
        _NetFightScene.FightCanvas.gameObject.SetActive(false);
        _NetFightScene.PreparingCanvas.gameObject.SetActive(false);
        mainProcessRunner.triggerMainProcess(FinalMoment(this.fightLogger.getWinner()));
    }
    
    public override void LocalUpdate()
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
                        //BoundaryControllByGod.RoundBattleFieldNormalControl(Vector3.zero);
                        break;
                    case BoundaryMode.None:
                        break;
                }
                break;
        }
        this.mobileInputsManager.RefreshButtonPattern();
    }

    private IEnumerator FinalMoment(Team winner)
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
            if (!_one.IsDead.Value)
            {
                _one.AIStateRunner.ChangeState("Victory");
            }
        }
        Time.timeScale = 1f;
    }
}
