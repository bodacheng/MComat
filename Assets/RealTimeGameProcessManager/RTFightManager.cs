using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace FightScene
{
    //角色列表的职责现在不光是负责两侧菜单中角色的icon，也负责被控制角色又上角血条和ex条
    public class RTFightManager : MonoBehaviour
    {
        [Header("Basic Element")]
        public CameraManager _CameraManager;
        
        [Header("Messages")]
        [Space(6)]
        public Text Messages;
        
        public TeamUIManager team1;
        public TeamUIManager team2;
        
        public readonly TeamConfig heroTeamConfig = new TeamConfig("1", Team.player1, new List<Team>() { Team.player2 });
        public readonly TeamConfig EnemyTeamConfig = new TeamConfig("2", Team.player2, new List<Team>() { Team.player1 });
        
        public static RTFightManager target;
        
        public static bool Auto;
        public static Data_Center focusingUnit;
        public static Team playerTeam = Team.player1;
        
        public MultiDict<int, int, Data_Center> Team1Members;
        public MultiDict<int, int, Data_Center> Team2Members;
        
        public readonly IDictionary<Data_Center, UnitInfo> UnitInfoRef = new Dictionary<Data_Center, UnitInfo>();
        //public readonly IDictionary<Team, List<Data_Center>> AllMembers = new Dictionary<Team, List<Data_Center>>();
        public static readonly IDictionary<Team, List<Data_Center>> FightingMembers = new Dictionary<Team, List<Data_Center>>();
        
        FightInfo loadFight;
        
        void Awake()
        {
            target = this;
        }
        
        public static void AddOrRemoveFightingMember(Data_Center member, Team team, bool add) // add:true remove: false
        {
            if (!FightingMembers.ContainsKey(team))
                FightingMembers.Add(team, new List<Data_Center>());
            var fightingUnits = FightingMembers[team];
            if (add)
            {
                if (!fightingUnits.Contains(member))
                {
                    fightingUnits.Add(member);
                }
            }
            else
            {
                if (fightingUnits.Contains(member))
                {
                    fightingUnits.Remove(member);
                }
            }
            FightingMembers[team] = fightingUnits;
        }
        
        public void SwitchToWatchMode() // button behaviour
        {
            SwitchToCMode(null, false);
            ParaAdjustment(playerTeam);
        }
        
        public void SwitchAutoMode()
        {
            Auto = !Auto;
            SwitchToCMode(focusingUnit, Auto);
            
            if (!Auto && focusingUnit != null)
            {
                FightingStepLayer.target.RefreshAIFlag(false);
            }
            else
            {
                FightingStepLayer.target.RefreshAIFlag(true);
            }
        }

        public void Refresh()//这个刷新是倾向于画面制御
        {
            // if (!Auto && focusingChar != null)
            // {
            //     _C_button.gameObject.SetActive(true);
            //     _AI_button.gameObject.SetActive(false);
            // }
            // else
            // {
            //     _C_button.gameObject.SetActive(false);
            //     _AI_button.gameObject.SetActive(true);
            // }

            //autoBUtton.onClick.RemoveAllListeners();
            //autoBUtton.onClick.AddListener(SwitchAutoMode);
            
            team1.Refresh(Team1Members);
            team2.Refresh(Team2Members);
            
            if (focusingUnit == null)
            {
                MobileInputsManager.target.TurnOffButtons();
            }
            else
            {
                MobileInputsManager.target.FocusCharInputs(focusingUnit._MyBehaviorRunner, focusingUnit.zokusei);
            }
        }
        
        public void SwitchToCMode(Data_Center _char, bool playerControl) //要转成控制模式的是哪个角色，如果括号里是null，意味着走向AI模式    
        {
            if (_char != null)
            {
                MobileInputsManager.SetPlayerMode(playerControl);
            }
            else
            {
                MobileInputsManager.SetPlayerMode(false);
            }
            focusingUnit = _char;
            Refresh();
        }
        
        IEnumerator _UnitsLoad(MultiDict<int, int, UnitInfo> MembersSets, MultiDict<int, int, Data_Center> TeamMembers)
        {
            foreach (var kv in MembersSets.mDict)
            {
                var _one = kv.Value;
                var center = TeamMembers.Get(kv.Key.Item1, kv.Key.Item2);
                if (center == null)
                {
                    var returnValue = UnitCreator.CreateUnit(_one);
                    yield return returnValue;
                    center = (Data_Center)returnValue.Current;
                }
                
                TeamMembers.Set(kv.Key.Item1, kv.Key.Item2, center);
                DicAdd<Data_Center, UnitInfo>.Add(UnitInfoRef, center, _one);
            }
        }

        public IEnumerator LoadUnits(FightInfo info)
        {
            yield return _UnitsLoad(info.fightMembers.HeroSets, Team1Members);
            yield return _UnitsLoad(info.fightMembers.EnemySets, Team2Members);
        }
        
        Data_Center team1StartUnit = null, team2StartUnit = null;
        
        public void SetGame(FightInfo stage)
        {
            loadFight = stage;
            
            team1.teamConfig = heroTeamConfig;
            team2.teamConfig = EnemyTeamConfig;
            team1.teamConfig.playID = loadFight.team1ID;
            team2.teamConfig.playID = loadFight.team2ID;
            
            // 角色第二次初始化在这之前已经结束
            
            team1.InsTeamUI(Team1Members);
            team2.InsTeamUI(Team2Members);
            
            team1.TeamsInit(Team1Members, stage.Team1HpRate ,stage.team1CGMode);
            team2.TeamsInit(Team2Members, stage.Team2HpRate ,stage.team2CGMode);
            
            if (stage.GetEventType() == FightEventType.Screensaver)
            {
                team1.TurnAllMembersInvincible(true, Team1Members);
                team2.TurnAllMembersInvincible(true, Team2Members);
            }else{
                team1.TurnAllMembersInvincible(false, Team1Members);
                team2.TurnAllMembersInvincible(false, Team2Members);
            }
            
            switch (team1.TeamMode)
            {
                case TeamMode.multiRaid:
                    team1StartUnit = team1.ToStartPos_Multi(Team1Members);
                    break;
                case TeamMode.rotation:
                    team1StartUnit = team1.ToStartPos_Rotate(Team1Members);
                    break;
            }
            
            switch (team2.TeamMode)
            {
                case TeamMode.multiRaid:
                    team2StartUnit = team2.ToStartPos_Multi(Team2Members);
                    break;
                case TeamMode.rotation:
                    team2StartUnit = team2.ToStartPos_Rotate(Team2Members);
                    break;
            }
            
            switch (playerTeam)
            {
                case Team.player1:
                    SwitchToCMode(team1StartUnit, false);
                    break;
                case Team.player2:
                    SwitchToCMode(team2StartUnit, false);
                    break;
            }
            NetFightScene.target.LoadStageFinished.Value = true;
        }
        
        void AllUnitsStartOff(MultiDict<int, int, Data_Center> TeamMembers, Team myTeam, bool TestMode = false)
        {
            foreach (var oneMember in TeamMembers.GetValues())
            {
                oneMember._MyBehaviorRunner.controller.TestMode = TestMode;
                AddOrRemoveFightingMember(oneMember, myTeam, true);
                if (!TestMode)
                    oneMember._MyBehaviorRunner.ChangeToWaitingState();
                else
                {
                    oneMember._MyBehaviorRunner.ChangeToTestMode();
                }
            }
        }
        
        void OneUnitStartOff(Data_Center dc, Team myTeam)
        {
            AddOrRemoveFightingMember(dc, myTeam, true);
            dc._MyBehaviorRunner.ChangeToWaitingState();
        }
        
        public void ModeStart()
        {
            switch (loadFight.Team1Mode)
            {
                case TeamMode.multiRaid:
                    AllUnitsStartOff(Team1Members, heroTeamConfig.myTeam);
                    break;
                case TeamMode.rotation:
                    OneUnitStartOff(team1StartUnit, heroTeamConfig.myTeam);
                    break;
            }
            
            if (loadFight.GetEventType() != FightEventType.Test)
            {
                switch (loadFight.Team2Mode)
                {
                    case TeamMode.multiRaid:
                        AllUnitsStartOff(Team2Members, EnemyTeamConfig.myTeam);
                        break;
                    case TeamMode.rotation:
                        OneUnitStartOff(team2StartUnit, EnemyTeamConfig.myTeam);
                        break;
                }
            }
            else
            {
                AllUnitsStartOff(Team2Members, heroTeamConfig.myTeam, true);
            }
        }
        
        // 战斗模式相机。根据选择队伍做相应调整。
        public void ParaAdjustment(Team myTeam)
        {
            C_Mode c_Mode;
            if (loadFight.Team1Mode == TeamMode.multiRaid)
            {
                c_Mode = C_Mode.CertainYAntiVibration;
            }
            else
            {
                c_Mode = C_Mode.CertainYAntiVibration;
            }
            if (focusingUnit != null)
            {
                if (myTeam == Team.player1)
                {
                    _CameraManager.Assign_Camera(c_Mode, focusingUnit.WholeT, team2.GetFightingUnitTs(Team1Members));
                }
                else
                {
                    _CameraManager.Assign_Camera(c_Mode, focusingUnit.WholeT, team1.GetFightingUnitTs(Team2Members));
                }
            }
            else
            {
                _CameraManager.Assign_Camera(C_Mode.TopDown, null);
            }
        }
        
        // 屏保模式相机。
        public void ScreenSaverC(Team myTeam)
        {
            if (focusingUnit != null)
            {
                if (myTeam == Team.player1)
                {
                    _CameraManager.Assign_Camera(C_Mode.ScreenSaver, team2.GetFightingUnitTs(Team1Members));
                }
                else
                {
                    _CameraManager.Assign_Camera(C_Mode.ScreenSaver, team1.GetFightingUnitTs(Team2Members));
                }
                _CameraManager.CurrentMode.SetMeCenter(focusingUnit.WholeT);
            }
        }

        public void Clear()// 这个我们还没有添加在合理的地方。
        {
            foreach (Data_Center one in Team1Members.GetValues())
            {
                one.FightDataRef.Clear();
            }
            foreach (Data_Center one in Team2Members.GetValues())
            {
                one.FightDataRef.Clear();
            }
            team1.Clear();
            team2.Clear();
            Team1Members.Clear();
            Team2Members.Clear();
            FightingMembers.Clear();
            MobileInputsManager.target.Clear();
        }

        //void OnGUI()
        //{
        //    if (GUI.Button(new Rect(40, 40, 60, 30), "切换队伍"))
        //    {
        //        switch (playerTeam)
        //        {
        //            case Team.player1:
        //                playerTeam = Team.player2;
        //                SwitchToCMode(null, Auto);

        //                break;
        //            case Team.player2:
        //                playerTeam = Team.player1;
        //                SwitchToCMode(null, Auto);
        //                break;
        //        }
        //        CameraParaAdjustment(playerTeam);
        //    }
        //}
    }
}