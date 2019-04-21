using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using System;

public class FightingProcess : NagareProcess
{
    Team loser = Team.none;
    jueSeLiebiao Icons;
    BoundaryControllByGod _BoundaryControllByGod;
    Data_Center finalSurviver;
    IDictionary<Team, List<Data_Center>> TeamDeadMemberDictionary = new Dictionary<Team, List<Data_Center>>();
    List<Transform> watchetargets = new List<Transform>();
    
    public FightingProcess(NetFightScene _NetFightScene)
    {
        this.thisProcessStep = SceneStep.Fighting;
        this.nextProcessStep = SceneStep.FightOver;
        this._NetFightScene = _NetFightScene;
        
        this.Icons = this._NetFightScene.Icons;
        this._BoundaryControllByGod = this._NetFightScene._BoundaryControllByGod;
    }
    
    public override bool canEnterNextProcess()
    {
        if (loser != Team.none)
            return true;
        else return false;
    }
    
    public override void ProcessEnter()
    {
        this._BoundaryControllByGod.AllMembers = _NetFightScene._CharSetManager.TeamMembers;
        TeamDeadMemberDictionary.Clear();
        foreach (KeyValuePair<Team,List<Data_Center>> keyValuePair in _NetFightScene._CharSetManager.TeamMembers)
        {
            TeamDeadMemberDictionary.Add(keyValuePair.Key,new List<Data_Center>());//这个什么意思呢，就是说把所有队伍的Team值加进TeamDeadMemberDictionary，value是个空列表，谁死了谁加进入
        }
        loser = Team.none;
        Debug.Log("这里3");
        _NetFightScene.pressedStartButton();
        _NetFightScene.FightCanvas.gameObject.SetActive(true);
        _NetFightScene._FightOverControl.FightOverCanvas.gameObject.SetActive(false);
        _NetFightScene.PreparingCanvas.gameObject.SetActive(false);
    }
    
    public override void ProcessEnd()
    {
        _NetFightScene.FightCanvas.gameObject.SetActive(false);
        _NetFightScene.PreparingCanvas.gameObject.SetActive(false);
        _NetFightScene.RunFightSceneProcess(finalMoment(finalSurviver, loser));
    }
    
    public override void localUpdate()
    {
        Icons.fightGUIProcess();

        if (Input.GetKey(KeyCode.Escape))
        {
            _NetFightScene.PauseScene();
        }

        switch (_BoundaryControllByGod.boundaryMode)
        {
            case BoundaryMode.Round:
                //_BoundaryControllByGod.SUOQUANER(alivemembercount);
                _BoundaryControllByGod.RoundModeGodControll(_BoundaryControllByGod.battleRingCenter, _BoundaryControllByGod.BattleRingRadius);
                break;
            case BoundaryMode.None:
                _BoundaryControllByGod.RoundBattleFieldNormalControl(_BoundaryControllByGod.battleRingCenter, 24);
                break;
        }

        if (Icons.GetFocusingChar() != null)
        {
            watchetargets.Clear();
            if (Icons.GetFocusingChar().Sensor.getEnemiesByDistance(false).Count > 0)
            {
                foreach (GameObject _G in Icons.GetFocusingChar().Sensor.getEnemiesByDistance(false))
                {
                    if (Vector3.Distance(Icons.GetFocusingChar().transform.position, _G.transform.position) <= 20)
                        watchetargets.Add(_G.transform);
                }
            }
            _NetFightScene._CameraManager.Assign_Camera(Camera_Mode_Num.CertainYAntiVibrationCamera, watchetargets);
            _NetFightScene._CameraManager.current_Camera_Mode.setMeCenter(Icons.GetFocusingChar().transform);
            
            //if (Icons.GetFocusingChar().Sensor.getOutterEnemiesColliders().Count == 0 && Icons.GetFocusingChar().Sensor.getInnerEnemiesColliders().Count == 0)
            //    _CameraManager.Assign_Camera(Camera_Mode_Num.CertainYAntiVibrationCamera, targets);
            //if (Icons.GetFocusingChar().Sensor.getInnerEnemiesColliders().Count == 0 && Icons.GetFocusingChar().Sensor.getOutterEnemiesColliders().Count > 0)
            //    _CameraManager.Assign_Camera(Camera_Mode_Num.CertainYAntiVibrationCamera, targets);
            //if (Icons.GetFocusingChar().Sensor.getInnerEnemiesColliders().Count > 0)
                //_CameraManager.Assign_Camera(Camera_Mode_Num.CertainYAntiVibrationCamera, targets);
        }
        else
        {
            if (_NetFightScene._CameraManager.current_Camera_Mode_Num != Camera_Mode_Num.GodMode)
            {
                //_CameraManager.Assign_Camera (Camera_Mode_Num.GodMode);
            }
        }

        //getWinnerTagLocalGame()这个东西在消耗很大的计算量..我感觉其实如果你的AI们在找不到目标时候能进入个待机动作，这个胜负判断的函数没有必要每帧都进行

        this.TeamDeadMemberDictionary = TeamMemberDeathProcessing(_NetFightScene._CharSetManager.TeamMembers);
        loser = getDeadTeamLocalGame(TeamDeadMemberDictionary);
    }

    IEnumerator finalMoment(Data_Center _finalSurviver,Team _loser)
    {
        Time.timeScale = 0.4f;
        watchetargets.Clear();
        if (_finalSurviver != null)
        {
            watchetargets.Add(_finalSurviver.gameObject.transform);
            _NetFightScene._CameraManager.Assign_Camera(Camera_Mode_Num.CertainYAntiVibrationCamera, watchetargets);
        }
        yield return new WaitForSeconds(2f);

        List<Data_Center> winners = new List<Data_Center>();
        if (_loser == Team.player1)
            winners = _NetFightScene._CharSetManager.getTeamMembers(Team.player2);
        if (_loser == Team.player2)
            winners = _NetFightScene._CharSetManager.getTeamMembers(Team.player1);
            
        foreach (Data_Center _one in winners)
        {
            if (_one.getBOHealth()._health > 0)
            {
                _one.getRunner().changeState("Victory");
            }
        }
        Time.timeScale = 1f;
        _NetFightScene.changeProcess(SceneStep.FightOver);
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
                if (_char.getBOHealth()._health > 0)//字符串比较本身消耗比较大。。。这个环节如果我们愿意其实可以搞个death flag
                {
                }else{
                    if (!TeamDeadMemberDictionary[_KeyValuePair.Key].Contains(_char))
                    {
                        _char.getRunner().changeState("Death");
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
            if (_NetFightScene._CharSetManager.TeamMembers.ContainsKey(_keyvalue.Key))
            {
                if (_NetFightScene._CharSetManager.TeamMembers[_keyvalue.Key].Count == _keyvalue.Value.Count)
                {
                    AllDieList.Add(_keyvalue.Key);
                }
                //下面这部分逻辑是这样：如果只有两队处于有人活着状态，并且其中一个队伍的人数为1，那么就把这个角色设定为finalSurviver，相机可以围绕这个最终生存者展开一些演出。
                if (_NetFightScene._CharSetManager.TeamMembers[_keyvalue.Key].Count == _keyvalue.Value.Count + 1)
                {
                    finalSurviver = _NetFightScene._CharSetManager.TeamMembers[_keyvalue.Key].Except<Data_Center>(_keyvalue.Value).ToList()[0];
                }
            }
        }

        if (AllDieList.Count > 0)
            return AllDieList[0];
        return Team.none;
    }
}
