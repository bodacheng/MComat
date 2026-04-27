using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
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

        private EvolutionManager evolutionManager;
        public EvolutionManager EvolutionManager
        {
            get => evolutionManager ??= new EvolutionManager();
        }
        
        FightInfo _loadFight;

        public CompositeDisposable Disposables = new CompositeDisposable();

        public readonly SubUnitSupport SubUnits = new SubUnitSupport();
        
        void Awake()
        {
            Target = this;
            SubUnits.RefreshDefinitions();
        }

        public async UniTask LoadUnits(FightInfo info, Action<float> onProgress = null)
        {
            SubUnits.AddSubUnits(info);

            var totalUnits = info.FightMembers.HeroSets.GetValues().Count + info.FightMembers.EnemySets.GetValues().Count;
            if (totalUnits <= 0)
            {
                onProgress?.Invoke(1f);
                return;
            }

            float loadedUnitsProgress = 0f;
            void ReportUnitProgressDelta(float delta)
            {
                if (delta <= 0f)
                {
                    return;
                }

                loadedUnitsProgress = Mathf.Clamp01(loadedUnitsProgress + delta / totalUnits);
                onProgress?.Invoke(loadedUnitsProgress);
            }

            onProgress?.Invoke(0f);
            
            await UniTask.WhenAll(
                team1._UnitsLoad(info.FightMembers.HeroSets, UnitInfoRef, ReportUnitProgressDelta), 
                team2._UnitsLoad(info.FightMembers.EnemySets, UnitInfoRef, ReportUnitProgressDelta)
            );
            onProgress?.Invoke(1f);
        }
        
        public void SetGame(FightInfo stage)
        {
            _loadFight = stage;
            FightScene.target.LoadStageFinished.Value = true;
        }
        
        public void ModeStart()
        {
            switch (_loadFight.FightMode)
            {
                case FightMode.Multi:
                case FightMode.Group:
                    team1.AllUnitsStartOff();
                    team2.AllUnitsStartOff();
                    break;
                case FightMode.Rotate:
                case FightMode.Evolve:
                    team1.UnitStartOff();
                    team2.UnitStartOff();
                    break;
            }
        }
        
        // 战斗模式相机。根据选择队伍做相应调整。
        public void CameraAdjustment(Team myTeam, FightMode fightMode, Transform me = null)
        {
            C_Mode cMode;
            if (fightMode is FightMode.Rotate or FightMode.Evolve)
                cMode = C_Mode.CertainYAntiVibration;
            else
            {
                cMode = fightMode == FightMode.Group ? C_Mode.TopDown : C_Mode.WatchOver;
            }

            var myUnitsManager = myTeam == Team.player1 ? team1 : team2;
            var opponentManager = myTeam == Team.player1 ? team2 : team1;
            
            var ts = myUnitsManager.GetFightingUnitTs();
            var tsOpponents = GetOpponents();
            
            List<Transform> GetOpponents()
            {
                List<Transform> returnValue;
                if (fightMode is FightMode.Rotate or FightMode.Evolve)
                {
                    returnValue = new List<Transform> { opponentManager.GetRModeUnitT() };
                }
                else
                {
                    returnValue = opponentManager.GetFightingUnitTs();
                }
                return returnValue;
            }

            if (fightMode is FightMode.Rotate or FightMode.Evolve)
            {
                var myStandPoint = myUnitsManager.GetPrimaryStandPoint();
                var opponentStandPoint = opponentManager.GetPrimaryStandPoint();

                if (ts == null)
                {
                    ts = new List<Transform>();
                }
                if (!ts.Any(x => x != null) && myStandPoint != null)
                {
                    ts.Add(myStandPoint);
                }

                if (tsOpponents == null)
                {
                    tsOpponents = new List<Transform>();
                }
                if (!tsOpponents.Any(x => x != null) && opponentStandPoint != null)
                {
                    tsOpponents.Add(opponentStandPoint);
                }

                if (me == null)
                {
                    me = ts.FirstOrDefault(x => x != null) ?? myStandPoint;
                }
            }
            
            if (fightMode == FightMode.Group)
            {
                _CameraManager.Assign_Camera(cMode, null, null);
            }
            else
            {
                if (cMode == C_Mode.WatchOver)
                {
                    var center = me;
                    _CameraManager.Assign_Camera(cMode, center, tsOpponents, ts);
                }
                else
                {
                    var center = me ?? ts?.FirstOrDefault(x => x != null);
                    _CameraManager.Assign_Camera(
                        cMode,
                        center,
                        tsOpponents
                    );
                }
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
    }
}
