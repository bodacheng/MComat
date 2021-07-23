using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Api.Dto.Model;
using dataAccess;
using UniRx;
using FightScene;

// 这个就是FightingProcess的一个变种。
public class BasicTryProcess : FSceneProcess
{
    int step = 2;// 1. 移动 2. 技能  3.防御  4. 闪避 

    new int Step
    {
        set
        {
            step = value;
            switch (step)
            {
                case 1:
                
                break;
                case 2:
                break;
                case 3:
                break;
                case 4:
                break; 
            }
        }
        get
        {
            return step;
        }
    }
    Team loser = Team.none;
    Data_Center finalSurviver;
    IDictionary<Team, List<Data_Center>> TeamDeadMemberDictionary = new Dictionary<Team, List<Data_Center>>();
    List<Transform> watchetargets = new List<Transform>();

    CharDataInfo adamInfo;
    Data_Center Adam, Guard;
    readonly IDictionary<Team, List<Data_Center>> AllMembers = new Dictionary<Team, List<Data_Center>>();//双方队伍人员字典，和netfightscene模块里同名变量统一。

    public BasicTryProcess()
    {
        base.Step = SceneStep.BasicTryTutorial;
        nextProcessStep = SceneStep.FightOver;
    }
    
    public override bool CanEnterOtherProcess()
    {
        return Step == 4;
    }
        
    public IEnumerator EnterProcess()
    {
        AllMembers.Add(Team.player1,RealTimeGameProcessManager.target.team1.TeamMembers.GetValues());
        AllMembers.Add(Team.player2,RealTimeGameProcessManager.target.team2.TeamMembers.GetValues());
        TeamDeadMemberDictionary.Clear();
        
        foreach (KeyValuePair<Team,List<Data_Center>> keyValuePair in AllMembers)
        {
            if (keyValuePair.Key == Team.player1)
            {
                Adam = keyValuePair.Value[0];
            }
            if (keyValuePair.Key == Team.player2)
            {
                Guard = keyValuePair.Value[0];
            }
            
            TeamDeadMemberDictionary.Add(keyValuePair.Key,new List<Data_Center>());//这个什么意思呢，就是说把所有队伍的Team值加进TeamDeadMemberDictionary，value是个空列表，谁死了谁加进入
        }
        loser = Team.none;
        NetFightScene.target.PressedStartButton();
        NetFightScene.target.FightCanvas.gameObject.SetActive(true);
        FightOverControl.target.FightOverCanvas.gameObject.SetActive(false);
        NetFightScene.target.PreparingCanvas.gameObject.SetActive(false);
        
        watchetargets.Clear();
        if (RealTimeGameProcessManager.focusingChar.Sensor.GetEnemiesByDistance(true).Count > 0)
        {
            foreach (GameObject _G in RealTimeGameProcessManager.focusingChar.Sensor.GetEnemiesByDistance(false))
            {
                watchetargets.Add(_G.transform);
            }
        }

        UnitInfo before = MyMonsters.Get("1");
        CharDataInfo characterDataInfo = UnitInfo.GetCharDataInfo(before);
        adamInfo = characterDataInfo;
        yield break;
    }
    
    public override void ProcessEnter()
    {
        SingleThreadProcesser.backup.RunAsQueued(EnterProcess());
    }
    
    public override void ProcessEnd()
    {
        NetFightScene.target.FightCanvas.gameObject.SetActive(false);
        NetFightScene.target.PreparingCanvas.gameObject.SetActive(false);
        SingleThreadProcesser.backup.RunAsQueued(FinalMoment(finalSurviver, loser));
    }
    
    public override void LocalUpdate()
    {                
        switch (Step)
        {
            case 1:
            
            break;
            case 2:
            if (Adam._MyBehaviorRunner.GetNowState().StateKey == adamInfo.set.GetA3Config().REAL_NAME)
            {
                Step = 3;
                    Debug.Log("Success3");
            }
            break;
            case 3:
            if (Adam._MyBehaviorRunner.GetNowState().StateKey == "RushBack")
            {
                Step = 4;
                Debug.Log("Success4");
            }
            break;
            case 4:
            break; 
        }

        if (RealTimeGameProcessManager.focusingChar != null)
        {
            //FightScene._CameraManager.Assign_Camera(C_Mode.CertainYAntiVibration, watchetargets);
            //FightScene._CameraManager.CurrentMode.SetMeCenter(RealTimeGameProcessManager.focusingChar.WholeT);
        }
        
        TeamDeadMemberDictionary = TeamMemberDeathProcessing(AllMembers);
        loser = GetDeadTeamLocalGame(TeamDeadMemberDictionary);
    }
    
    IEnumerator FinalMoment(Data_Center _finalSurviver,Team _loser)
    {
        Time.timeScale = 0.4f;
        watchetargets.Clear();
        if (_finalSurviver != null)
        {
            watchetargets.Add(_finalSurviver.gameObject.transform);
            //FightScene._CameraManager.Assign_Camera(C_Mode.CertainYAntiVibration, watchetargets);
        }
        yield return new WaitForSeconds(2f);

        List<Data_Center> winners = new List<Data_Center>();
        if (_loser == Team.player1)
            winners = AllMembers[Team.player2];
        if (_loser == Team.player2)
            winners = AllMembers[Team.player1];
            
        foreach (Data_Center _one in winners)
        {
            if (_one.FightDataRef.CurrentHp.Value > 0)
            {
                _one._MyBehaviorRunner.ChangeState("Victory");
            }
        }
        Time.timeScale = 1f;
    }
    
    // 通用系函数
    public IDictionary<Team, List<Data_Center>> TeamMemberDeathProcessing(IDictionary<Team, List<Data_Center>> fighters)
    {
        if (fighters == null)
            return null;
            
        foreach (KeyValuePair<Team,List<Data_Center>> _KeyValuePair in fighters)
        {
            //foreach (Data_Center _char in _KeyValuePair.Value)
            //{
            //    if (_char.IsDead.Value)//字符串比较本身消耗比较大。。。这个环节如果我们愿意其实可以搞个death flag
            //    {
            //    }else{
            //        if (!TeamDeadMemberDictionary[_KeyValuePair.Key].Contains(_char))
            //        {
            //            _char.AIStateRunner.changeState("Death");
            //            TeamDeadMemberDictionary[_KeyValuePair.Key].Add(_char);
            //        }
            //    }
            //}
        }
        return TeamDeadMemberDictionary;
    }
    
    public Team GetDeadTeamLocalGame(IDictionary<Team, List<Data_Center>> deadMemberDic)
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
}
