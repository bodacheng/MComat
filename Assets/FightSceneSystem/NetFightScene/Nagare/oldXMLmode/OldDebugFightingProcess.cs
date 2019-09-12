using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class OldDebugFightingProcess : NagareProcess
{
    IDictionary<Team, List<Data_Center>> AllMembers = new Dictionary<Team, List<Data_Center>>();//双方队伍人员字典，和netfightscene模块里同名变量统一。
    IDictionary<Team, List<Data_Center>> TeamDeadMemberDictionary = new Dictionary<Team, List<Data_Center>>();
    Data_Center finalSurviver;
    
    public OldDebugFightingProcess(NetFightScene _NetFightScene,DebugManager debugManager)
    {
        this.thisProcessStep = SceneStep.Fighting;
        this.nextProcessStep = SceneStep.FightOver;
        this._NetFightScene = _NetFightScene;
        this.debugManager = debugManager;
    }

    public override bool canEnterNextProcess()
    {
        return _NetFightScene.ifLoadStageFinished() && _RealTimeGameProcessManager.FightTeam1.ifAllCharsPreparedForBattle() && _RealTimeGameProcessManager.FightTeam2.ifAllCharsPreparedForBattle();
    }
    
    public override void ProcessEnter()
    {
        this.BoundaryControllByGod.AllMembers.Clear();
        AllMembers.Add(Team.player1,_RealTimeGameProcessManager.FightTeam1.teamMembers.values);
        AllMembers.Add(Team.player2,_RealTimeGameProcessManager.FightTeam2.teamMembers.values);
        
        if (debugManager.debugMode == DebugMode.ab_mode)
        {
            debugManager.pretabName.gameObject.SetActive(true);
            debugManager.charsOfType.gameObject.SetActive(false);
            debugManager.AIScriptName.gameObject.SetActive(true);
            debugManager.AIScriptsOfType.gameObject.SetActive(false);
        }else{
            debugManager.pretabName.gameObject.SetActive(false);
            debugManager.charsOfType.gameObject.SetActive(true);
            debugManager.AIScriptName.gameObject.SetActive(false);
            debugManager.AIScriptsOfType.gameObject.SetActive(true);
        }
        //_NetFightScene.gameStartButton.gameObject.SetActive(true);
    }
    
    public override void ProcessEnd()
    {
        _NetFightScene.resetLoadStageFinishedFlag();
        _NetFightScene.PreparingCanvas.gameObject.SetActive(false);
        _NetFightScene.FightCanvas.gameObject.SetActive(false);
        mainProcessRunner.triggerMainProcess(_NetFightScene._FightOverControl.WINProcess());//这里是要根据情况的。。
    }

    public override void localUpdate()
    {
        switch (debugManager._BoundaryControllByGod.boundaryMode)
        {
            case BoundaryMode.Round:
                //_BoundaryControllByGod.SUOQUANER(_NetFightScene.alivemembercount);
                debugManager._BoundaryControllByGod.RoundModeGodControll(debugManager._BoundaryControllByGod.battleRingCenter, debugManager._BoundaryControllByGod.BattleRingRadius);
                break;
            case BoundaryMode.None:
                debugManager._BoundaryControllByGod.RoundBattleFieldNormalControl(debugManager._BoundaryControllByGod.battleRingCenter, 24);
                break;
        }

        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            if (RealTimeGameProcessManager.focusingChar != null)
            {
                //if (AIStateRunner.playerMode)
                //{
                //    //mobile_input.SetActive(true);
                //}
                //else
                //{
                //    //mobile_input.SetActive(false);
                //}
            }
        }
        else
        {
            //mobile_input.SetActive(false);
        }
        //getWinnerTagLocalGame()这个东西在消耗很大的计算量..我感觉其实如果你的AI们在找不到目标时候能进入个待机动作，这个胜负判断的函数没有必要每帧都进行
        TeamDeadMemberDictionary = TeamMemberDeathProcessing(AllMembers);
        Team loser = getDeadTeamLocalGame(TeamDeadMemberDictionary);
        Team winner = Team.none;
        if (loser == Team.player1)
            winner = Team.player2;
        if (loser == Team.player2)
            winner = Team.player1;
        if (winner != Team.none)
        {
            mainProcessRunner.triggerMainProcess(finalMoment(finalSurviver, winner)) ;
        }

        if (RealTimeGameProcessManager.focusingChar != null)
        {
            if (RealTimeGameProcessManager.focusingChar.Sensor.getInnerEnemiesColliders().Count > 0)
            {
                _NetFightScene._CameraManager.Assign_Camera(Camera_Mode_Num.GodPlayerCertainYCamera, new List<Transform>() { RealTimeGameProcessManager.focusingChar.transform });
                //_CameraManager.current_Camera_Mode.setCertainYModeParameters(13f, _NetFightScene.Icons.GetFocusingChar().getFloorY() + 10f);
                return;
            }
            if (RealTimeGameProcessManager.focusingChar.Sensor.getMidEnemiesColliders().Count > 0)
            {
                _NetFightScene._CameraManager.Assign_Camera(Camera_Mode_Num.GodPlayerCertainYCamera, new List<Transform>() { RealTimeGameProcessManager.focusingChar.transform });
                //_CameraManager.current_Camera_Mode.setCertainYModeParameters(16f, _NetFightScene.Icons.GetFocusingChar().getFloorY() + 10f);
                return;
            }
            if (RealTimeGameProcessManager.focusingChar.Sensor.getfarEnemiesColliders().Count > 0)
            {
                _NetFightScene._CameraManager.Assign_Camera(Camera_Mode_Num.GodPlayerCertainYCamera, new List<Transform>() { RealTimeGameProcessManager.focusingChar.transform });
                //_CameraManager.current_Camera_Mode.setCertainYModeParameters(16f, _NetFightScene.Icons.GetFocusingChar().getFloorY() + 10f);
                return;
            }
            else
            {
                _NetFightScene._CameraManager.Assign_Camera(Camera_Mode_Num.GodPlayerCertainYCamera, new List<Transform>() { RealTimeGameProcessManager.focusingChar.transform });
                //_CameraManager.current_Camera_Mode.setCertainYModeParameters(18f, _NetFightScene.Icons.GetFocusingChar().getFloorY() + 10f);
                return;
            }
        }
        else
        {
        }
    }
    
        // 通用系函数
    public IDictionary<Team, List<Data_Center>> TeamMemberDeathProcessing(IDictionary<Team, List<Data_Center>> fighters)
    {
        if (fighters == null)
            return null;
            
        foreach (KeyValuePair<Team,List<Data_Center>> _KeyValuePair in fighters)
        {
            foreach (Data_Center _char in _KeyValuePair.Value)
            {
                if (_char.BO_Health._health > 0)//字符串比较本身消耗比较大。。。这个环节如果我们愿意其实可以搞个death flag
                {
                }else{
                    if (!TeamDeadMemberDictionary[_KeyValuePair.Key].Contains(_char))
                    {
                        _char.AIStateRunner.changeState("Death");
                        TeamDeadMemberDictionary[_KeyValuePair.Key].Add(_char);
                    }
                }
            }
        }
        return TeamDeadMemberDictionary;
    }
    
    public Team getDeadTeamLocalGame(IDictionary<Team, List<Data_Center>> deadMemberDic)
    {
        List<Team> AllDieList = new List<Team>();

        foreach (KeyValuePair<Team, List<Data_Center>> _keyvalue in deadMemberDic)
        {
            if (AllMembers.ContainsKey(_keyvalue.Key))
            {
                if (AllMembers[_keyvalue.Key].Count == _keyvalue.Value.Count)
                {
                    AllDieList.Add(_keyvalue.Key);
                }
                //下面这部分逻辑是这样：如果只有两队处于有人活着状态，并且其中一个队伍的人数为1，那么就把这个角色设定为finalSurviver，相机可以围绕这个最终生存者展开一些演出。
                if (AllMembers[_keyvalue.Key].Count == _keyvalue.Value.Count + 1)
                {
                    finalSurviver = AllMembers[_keyvalue.Key].Except<Data_Center>(_keyvalue.Value).ToList()[0];
                }
            }
        }

        if (AllDieList.Count > 0)
            return AllDieList[0];
        return Team.none;
    }
    
    IEnumerator finalMoment(Data_Center _finalSurviver,Team loser)
    {
        Time.timeScale = 0.4f;
        List<Transform> watch = new List<Transform>();
        if (_finalSurviver != null)
        {
            watch.Add(_finalSurviver.gameObject.transform);
            _NetFightScene._CameraManager.Assign_Camera(Camera_Mode_Num.CertainYAntiVibrationCamera, watch);
        }
        yield return new WaitForSeconds(2f);

        List<Data_Center> winners = new List<Data_Center>();
        if (loser == Team.player1)
            winners = AllMembers[Team.player2];
        if (loser == Team.player2)
            winners = AllMembers[Team.player1];
            
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
