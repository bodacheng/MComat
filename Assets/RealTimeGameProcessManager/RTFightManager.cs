using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using UniRx;

namespace FightScene
{
    //角色列表的职责现在不光是负责两侧菜单中角色的icon，也负责被控制角色又上角血条和ex条
    public class RTFightManager : MonoBehaviour
    {
        public UnitsManger team1, team2;
        
        [Header("Basic Element")]
        public CameraManager _CameraManager;
        
        public readonly TeamConfig heroTeamConfig = new TeamConfig("1", Team.player1, new List<Team>() { Team.player2 });
        public readonly TeamConfig EnemyTeamConfig = new TeamConfig("2", Team.player2, new List<Team>() { Team.player1 });
        
        public static RTFightManager Target;
        public static Team playerTeam = Team.player1;
        
        public readonly IDictionary<Data_Center, UnitInfo> UnitInfoRef = new Dictionary<Data_Center, UnitInfo>();
        public readonly IDictionary<Data_Center, ReactiveProperty<float>> RefreshTimeDic = new Dictionary<Data_Center, ReactiveProperty<float>>();
        
        FightInfo _loadFight;

        public readonly CompositeDisposable Disposables = new CompositeDisposable();
        
        void Awake()
        {
            Target = this;
        }
        
        public async UniTask LoadUnits(FightInfo info)
        {
            await UniTask.WhenAll(team1._UnitsLoad(info.FightMembers.HeroSets, UnitInfoRef), 
                team2._UnitsLoad(info.FightMembers.EnemySets, UnitInfoRef));
        }
        
        public void SetGame(FightInfo stage)
        {
            _loadFight = stage;
            FightScene.target.LoadStageFinished.Value = true;
        }
        
        public void ModeStart()
        {
            switch (_loadFight.team1Mode)
            {
                case TeamMode.MultiRaid:
                    team1.AllUnitsStartOff();
                    break;
                case TeamMode.Rotation:
                    team1.UnitStartOff();
                    break;
            }
            
            switch (_loadFight.team2Mode)
            {
                case TeamMode.MultiRaid:
                    team2.AllUnitsStartOff();
                    break;
                case TeamMode.Rotation:
                    team2.UnitStartOff();
                    break;
            }
        }
        
        // 战斗模式相机。根据选择队伍做相应调整。
        public void CameraAdjustment(Team myTeam, TeamMode teamMode)
        {
            var cMode = C_Mode.CertainYAntiVibration;
            if (teamMode == TeamMode.Rotation)
                cMode = C_Mode.CertainYAntiVibration;
            else
                cMode = C_Mode.TopDown;
            
            var ts = myTeam == Team.player1 ? team1.GetFightingUnitTs() : team2.GetFightingUnitTs();
            if (ts.Count > 0)
            {
                _CameraManager.Assign_Camera(
                    cMode, 
                    cMode != C_Mode.TopDown? ts[0] : null, 
                    myTeam == Team.player1  ? team2.GetFightingUnitTs() : team1.GetFightingUnitTs()
                );
            }
            else
            {
                _CameraManager.Assign_Camera(C_Mode.TopDown, null,null);
            }
        }
        
        public void ClearUnitData()
        {
            foreach (var one in team1.teamMembers.GetValues())
            {
                one.CleanClear();
            }
            foreach (var one in team2.teamMembers.GetValues())
            {
                one.CleanClear();
            }
        }
        
        public void ClearUnits()
        {
            foreach (var one in team1.teamMembers.GetValues())
            {
                Destroy(one.WholeT.gameObject);
            }
            foreach (var one in team2.teamMembers.GetValues())
            {
                Destroy(one.WholeT.gameObject);
            }
            team1.teamMembers.Clear();
            team2.teamMembers.Clear();
        }
    }
}