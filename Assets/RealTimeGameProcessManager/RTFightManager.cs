using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UniRx;

namespace FightScene
{
    //角色列表的职责现在不光是负责两侧菜单中角色的icon，也负责被控制角色又上角血条和ex条
    public class RTFightManager : MonoBehaviour
    {
        public UnitsManger team1, team2;
        
        [Header("Basic Element")]
        public CameraManager _CameraManager;
        
        [Header("Messages")]
        public Text Messages;
        
        public readonly TeamConfig heroTeamConfig = new TeamConfig("1", Team.player1, new List<Team>() { Team.player2 });
        public readonly TeamConfig EnemyTeamConfig = new TeamConfig("2", Team.player2, new List<Team>() { Team.player1 });
        
        public static RTFightManager target;
        
        public static Data_Center focusingUnit;
        public static Team playerTeam = Team.player1;
        
        public MultiDict<int, int, Data_Center> Team1Members;
        public MultiDict<int, int, Data_Center> Team2Members;
        
        public readonly IDictionary<Data_Center, UnitInfo> UnitInfoRef = new Dictionary<Data_Center, UnitInfo>();
        public static readonly IDictionary<Data_Center, ReactiveProperty<float>> RefreshTimeDic = new Dictionary<Data_Center, ReactiveProperty<float>>();

        FightInfo loadFight;
        
        void Awake()
        {
            target = this;
        }
        
        public void SwitchToWatchMode() // button behaviour
        {
            team1.Auto = false;
            team2.Auto = false;
            CameraAdjustment(playerTeam);
        }

        public void SwitchAuto(Team team, bool ai)
        {
            switch (team)
            {
                case Team.player1:
                    team1.Auto = ai;
                    break;
                case Team.player2:
                    team2.Auto = ai;
                    break;
            }
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
        
        public Data_Center team1StartUnit = null, team2StartUnit = null;
        
        public void SetGame(FightInfo stage)
        {
            loadFight = stage;
            NetFightScene.target.LoadStageFinished.Value = true;
        }
        
        public void SetFocusUnit(Data_Center unit)
        {
            focusingUnit = unit;
            CameraAdjustment(playerTeam);
        }
        
        void AllUnitsStartOff(MultiDict<int, int, Data_Center> TeamMembers, Team myTeam, bool TestMode = false)
        {
            foreach (var oneMember in TeamMembers.GetValues())
            {
                Sensor.AddOrRemoveSharedUnits(oneMember, myTeam, true);
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
            Sensor.AddOrRemoveSharedUnits(dc, myTeam, true);
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
        
        // 全队无敌
        public void TurnAllUnitsInvincible(bool _Invincible, MultiDict<int, int, Data_Center> TeamMembers)
        {
            foreach (var center in TeamMembers.GetValues())
            {
                center.FightDataRef.Invincible = _Invincible;
            }
        }
        
        // 战斗模式相机。根据选择队伍做相应调整。
        public void CameraAdjustment(Team myTeam)
        {
            var c_Mode = C_Mode.CertainYAntiVibration;
            if (focusingUnit != null)
            {
                _CameraManager.Assign_Camera(c_Mode, focusingUnit.geometryCenter, myTeam == Team.player1  ? team2.GetFightingUnitTs(Team2Members) : team1.GetFightingUnitTs(Team1Members));
            }
            else
            {
                _CameraManager.Assign_Camera(C_Mode.TopDown, null,null);
            }
        }
        
        public void ClearUIAndData()
        {
            foreach (var one in Team1Members.GetValues())
            {
                one.CleanClear();
            }
            foreach (var one in Team2Members.GetValues())
            {
                one.CleanClear();
            }
            FightingStepLayer.target.team1UI.Clear();
            FightingStepLayer.target.team2UI.Clear();
            MobileInputsManager.target.Clear();
        }
        
        public void ClearUnits()
        {
            foreach (var one in Team1Members.GetValues())
            {
                Destroy(one.WholeT.gameObject);
            }
            foreach (var one in Team2Members.GetValues())
            {
                Destroy(one.WholeT.gameObject);
            }
            Team1Members.Clear();
            Team2Members.Clear();
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