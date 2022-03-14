using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;

namespace FightScene
{
    public partial class UnitsManger : MonoBehaviour
    {
        public TeamMode TeamMode;
        public TeamConfig teamConfig;

        private bool auto;
        public bool Auto
        {
            set
            {
                auto = value;
                if (RMode_Unit != null)
                {
                    RMode_Unit.Value._MyBehaviorRunner.AI = auto;
                }
            }
            get => auto;
        }

        public ReactiveProperty<Data_Center> RMode_Unit;

        Data_Center waitingMember;
        
        [HideInInspector]
        public Transform[] TeamStandPoints;
        
        public void localUpdate(MultiDict<int, int, Data_Center> TeamMembers)
        {
            switch (TeamMode)
            {
                case TeamMode.multiRaid:
                    break;
                case TeamMode.rotation:
                    WaitToTriggerMemberChange(TeamMembers);
                    break;
            }
        }
        
        public void TeamsIni_Rotate(MultiDict<int, int, Data_Center> TeamMembers, float TeamHpRate, CriticalGaugeMode teamCGMode)
        {
            foreach (var center in TeamMembers.GetValues())
            {
                //  时间刷新整备
                if (!RTFightManager.RefreshTimeDic.ContainsKey(center))
                {
                    RTFightManager.RefreshTimeDic.Add(center, new ReactiveProperty<float>(0));
                }
                
                center.Step3Initialize(teamConfig, TeamHpRate * SkillSet.INI_Hp(RTFightManager.target.UnitInfoRef[center].set.SkillEntityList()), teamCGMode);
                
                center.IsDead = new ReactiveProperty<bool>(false);
                center.IsDead.Subscribe(x => {
                    if (x) 
                    {
                        Sensor.AddOrRemoveSharedUnits(center, teamConfig.myTeam, false);
                        var team = teamConfig.myTeam == Team.player1 ? RTFightManager.target.Team1Members : RTFightManager.target.Team2Members;
                        ToNewUnit(team);
                    }
                });
                
                center._ResistanceManager.Resistance = new ReactiveProperty<int>
                {
                    Value = 0
                };
                center._ResistanceManager.Resistance.Subscribe(x =>
                {
                    center._ResistanceManager.Resistance.Value = Mathf.Clamp(x, 0, 10);
                });
            }
        }
        
        async void ToNewUnit(MultiDict<int, int, Data_Center> team)
        {
            await UniTask.DelayFrame(100);
            RandomToAliveUnit(team);
        }
        
        public Data_Center ToStartPos_Rotate(MultiDict<int, int, Data_Center> TeamMembers)
        {
            Data_Center startUnit = null;
            foreach (var kv in TeamMembers.mDict)
            {
                if (kv.Value == null)
                {
                    continue;
                }
                
                if (startUnit == null)
                    startUnit = kv.Value;
                kv.Value.WholeT.parent = null;
                kv.Value.WholeT.gameObject.SetActive(true);
            }
            ChangeUnit_ReadyToGo(startUnit, TeamMembers, TeamStandPoints[0]);
            return startUnit;
        }
        
        // 最初切换队员
        bool ChangeUnit_ReadyToGo(Data_Center _changeTo, MultiDict<int, int, Data_Center> TeamMembers, Transform IniStandPoint)
        {
            var unitChanged = false;
            foreach (var data_Center in TeamMembers.GetValues())
            {
                if (_changeTo == data_Center)
                {
                    RMode_Unit.Value = _changeTo;
                    RMode_Unit.Value.WholeT.transform.position = IniStandPoint.position;
                    RMode_Unit.Value.WholeT.rotation = IniStandPoint.rotation;
                    EffectsManager.GenerateEffect("memberShift", null, RMode_Unit.Value.WholeT.transform.position, Quaternion.identity, RMode_Unit.Value.geometryCenter);
                    unitChanged = true;
                    RMode_Unit.Value.WholeT.gameObject.SetActive(true);
                }
                else
                {
                    data_Center._MyBehaviorRunner.ChangeState("Empty");
                    data_Center.WholeT.gameObject.SetActive(false);
                }
            }
            RTFightManager.target.CameraAdjustment(RTFightManager.playerTeam);
            return unitChanged;
        }
        
                // 切换队员
        bool ChangeFightingUnit(Data_Center _changeTo, MultiDict<int, int, Data_Center> TeamMembers)
        {
            if (!(TeamMembers.GetValues().Count > 1) || RMode_Unit.Value == _changeTo)
            {
                return false;
            }
            if (_changeTo.IsDead.Value)
            {
                return false;
            }
            var unitChanged = false;
            var targetPos = Vector3.zero;
            if (RMode_Unit != null)
            {
                targetPos = RMode_Unit.Value.transform.position;
            }
            foreach (var data_Center in TeamMembers.GetValues())
            {
                if (_changeTo == data_Center)
                {
                    if (RMode_Unit != null && _changeTo != null) //继承hit数
                    {
                        _changeTo.FightDataRef._comboHitCount.HitCount.Value = RMode_Unit.Value.FightDataRef._comboHitCount.HitCount.Value;
                    }
                    Sensor.AddOrRemoveSharedUnits(RMode_Unit.Value, this.teamConfig.myTeam, false);
                    Sensor.AddOrRemoveSharedUnits(_changeTo, this.teamConfig.myTeam, true);
                    
                    RMode_Unit.Value = _changeTo;
                    RMode_Unit.Value.WholeT.gameObject.SetActive(true);
                    RMode_Unit.Value._MyBehaviorRunner.ChangeToWaitingState();
                    RMode_Unit.Value.WholeT.transform.position = targetPos;
                    EffectsManager.GenerateEffect("memberShift", null, RMode_Unit.Value.WholeT.transform.position, Quaternion.identity, RMode_Unit.Value.geometryCenter);
                    unitChanged = true;
                }
                else
                {
                    if (data_Center._MyBehaviorRunner.GetNowState().StateKey != "Empty")
                    {
                        data_Center._MyBehaviorRunner.ChangeState("Empty");
                        //data_Center.WholeT.transform.position = new Vector3(9999, 600, 9999);
                        data_Center.WholeT.gameObject.SetActive(false);
                    }
                }
            }
            if (teamConfig.myTeam == RTFightManager.playerTeam)
            {
                RTFightManager.target.SetFocusUnit(RMode_Unit.Value);
            }
            
            switch (_changeTo._TeamConfig.myTeam)
            {
                case Team.player1:
                    _changeTo._MyBehaviorRunner.AI = RTFightManager.target.team1.Auto;
                    break;
                case Team.player2:
                    _changeTo._MyBehaviorRunner.AI = RTFightManager.target.team2.Auto;
                    break;
            }
            RTFightManager.target.CameraAdjustment(RTFightManager.playerTeam);
            //Refresh(TeamMembers);
            return unitChanged;
        }
        
        // 计算时间统计可上场角色，更新上场冷却图标UI
        public void WaitToTriggerMemberChange(MultiDict<int, int, Data_Center> TeamMembers)
        {
            for (var i = 0; i < TeamMembers.GetValues().Count; i++)
            {
                if (RTFightManager.RefreshTimeDic[TeamMembers.GetValues()[i]].Value > 0)
                {
                    RTFightManager.RefreshTimeDic[TeamMembers.GetValues()[i]].Value -= Time.deltaTime; // 角色切换倒计时;
                }
            }
            
            if (waitingMember != null && CanChangeToThisMember(waitingMember))
            {
                RTFightManager.RefreshTimeDic[RMode_Unit.Value].Value = 10f;
                ChangeFightingUnit(waitingMember, TeamMembers);
                waitingMember = null;
            }
        }
        
        public List<Transform> GetFightingUnitTs(MultiDict<int, int, Data_Center> TeamMembers)
        {
            var transforms = new List<Transform>();
            switch (TeamMode)
            {
                case TeamMode.multiRaid:
                    foreach (var a_char in TeamMembers.GetValues())
                    {
                        if (a_char._MyBehaviorRunner.GetNowState().StateKey != "Death")
                        {
                            transforms.Add(a_char.geometryCenter);
                        }
                    }
                    return transforms;
                case TeamMode.rotation:
                    transforms = new List<Transform>
                    {
                        RMode_Unit.Value.geometryCenter
                    };
                    return transforms;
            }
            return null;
        }
        
        bool CanChangeToThisMember(Data_Center targetMember)
        {
            if (targetMember == RMode_Unit.Value)
            {
                return false;
            }
            if (targetMember.IsDead.Value)
            {
                return false;
            }
            if (RTFightManager.RefreshTimeDic[targetMember].Value > 0)
            {
                return false;
            }
            if (targetMember._MyBehaviorRunner.GetNowState().StateType == Skill.BehaviorType.Hit || targetMember._MyBehaviorRunner.GetNowState().StateType == Skill.BehaviorType.KnockOff)
            {
                return false;
            }
            if (targetMember._MyBehaviorRunner.GetNowState().StateType == Skill.BehaviorType.GI || targetMember._MyBehaviorRunner.GetNowState().StateType == Skill.BehaviorType.GM || targetMember._MyBehaviorRunner.GetNowState().StateType == Skill.BehaviorType.GR)
            {
                if (!targetMember._SkillCancelFlag.Cancel_Flag)
                    return true;
            }
            return true;
        }
        
        public void ReadyForNextMember(Data_Center nextOne)
        {
            if (waitingMember != nextOne)
            {
                waitingMember = nextOne;
            }
        }
        
        public bool RandomToAliveUnit(MultiDict<int, int, Data_Center> TeamMembers)
        {
            if (waitingMember != null && waitingMember.FightDataRef.CurrentHp.Value > 0)
            {
                if (!waitingMember.IsDead.Value)
                {
                    if (ChangeFightingUnit(waitingMember, TeamMembers))
                    {
                        return true;
                    }
                }
            }
            foreach (var data_Center in TeamMembers.GetValues())
            {
                if (!data_Center.IsDead.Value)
                {
                    if (ChangeFightingUnit(data_Center, TeamMembers))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}