using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Api.Dto.Model;
using dataAccess;
using UniRx;

// 这个就是FightingProcess的一个变种。
public class BasicTryProcess : NagareProcess
{
    int step = 2;// 1. 移动 2. 技能  3.防御  4. 闪避 
    int Step
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
                    AutoMoveToNext = true;
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

    CharacterDataInfo adamInfo;
    Data_Center Adam, Guard;
    
    IDictionary<Team, List<Data_Center>> AllMembers = new Dictionary<Team, List<Data_Center>>();//双方队伍人员字典，和netfightscene模块里同名变量统一。
    
    public BasicTryProcess(NetFightScene _NetFightScene,FightSceneProcessesRunner fightSceneProcessesRunner)
    {
        this.thisProcessStep = SceneStep.BasicTryTutorial;
        this.nextProcessStep = SceneStep.FightOver;
        EelementsInherit(_NetFightScene,fightSceneProcessesRunner);
    }
        
    public IEnumerator enterProcess()
    {
        this.BoundaryControllByGod.AllMembers.Clear();
        AllMembers.Add(Team.player1,_RealTimeGameProcessManager.FightTeam1.teamMembers.values);
        AllMembers.Add(Team.player2,_RealTimeGameProcessManager.FightTeam2.teamMembers.values);
        this.BoundaryControllByGod.AllMembers = AllMembers;
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
        _NetFightScene.pressedStartButton();
        _NetFightScene.FightCanvas.gameObject.SetActive(true);
        fightOverControl.FightOverCanvas.gameObject.SetActive(false);
        _NetFightScene.PreparingCanvas.gameObject.SetActive(false);
        
        watchetargets.Clear();
        if (RealTimeGameProcessManager.focusingChar.Sensor.getEnemiesByDistance(true).Count > 0)
        {
            foreach (GameObject _G in RealTimeGameProcessManager.focusingChar.Sensor.getEnemiesByDistance(false))
            {
                watchetargets.Add(_G.transform);
            }
        }
        
        IEnumerator getchar = AccountCharsSet.instance.getAccountCharacterInfo("1");
        yield return getchar;
        GetMonsterOfPlayerDetailModel before = (GetMonsterOfPlayerDetailModel)getchar.Current;
        CharacterDataInfo characterDataInfo = RemoteAccess.getCharacterDataInfo(before);
        adamInfo = characterDataInfo;
        yield break;
    }
    
    public override void ProcessEnter()
    {
        this.mainProcessRunner.triggerMainProcess(enterProcess());
    }
    
    public override void ProcessEnd()
    {
        _NetFightScene.FightCanvas.gameObject.SetActive(false);
        _NetFightScene.PreparingCanvas.gameObject.SetActive(false);
        mainProcessRunner.triggerMainProcess(finalMoment(finalSurviver, loser));
    }
    
    public override void LocalUpdate()
    {
        _RealTimeGameProcessManager.FightGUIProcess();

        if (Input.GetKey(KeyCode.Escape))
        {
            _NetFightScene.PauseScene();
        }

        switch (BoundaryControllByGod.boundaryMode)
        {
            case BoundaryMode.Round:
                //_BoundaryControllByGod.SUOQUANER(alivemembercount);
                //BoundaryControllByGod.RoundModeGodControll(BoundaryControllByGod.battleRingCenter, BoundaryControllByGod.BattleRingRadius);
                break;
            case BoundaryMode.None:
                //BoundaryControllByGod.RoundBattleFieldNormalControl(BoundaryControllByGod.battleRingCenter, 24);
                break;
        }
        
        switch (Step)
        {
            case 1:
            
            break;
            case 2:
            if (Adam.AIStateRunner.GetCurrentStateNum() == adamInfo._NineAndTwo.GetA3Config().REAL_NAME)
            {
                Step = 3;
                    Debug.Log("Success3");
            }
            break;
            case 3:
            if (Adam.AIStateRunner.GetCurrentStateNum() == "RushBack")
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
            _NetFightScene._CameraManager.Assign_Camera(Camera_Mode_Num.CertainYAntiVibrationCamera, watchetargets);
            _NetFightScene._CameraManager.current_Camera_Mode.SetMeCenter(RealTimeGameProcessManager.focusingChar.WholeT);            
        }
        
        this.TeamDeadMemberDictionary = TeamMemberDeathProcessing(AllMembers);
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
            winners = AllMembers[Team.player2];
        if (_loser == Team.player2)
            winners = AllMembers[Team.player1];
            
        foreach (Data_Center _one in winners)
        {
            if (_one._FightAttriCalReference.CurrentHp.Value > 0)
            {
                _one.AIStateRunner.ChangeState("Victory");
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
}
