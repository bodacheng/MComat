using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;

namespace FightScene
{
    public partial class UnitsManger : MonoBehaviour
    {
        public ReactiveProperty<Data_Center> RMode_Unit = new ReactiveProperty<Data_Center>();
        Data_Center waitingMember;
        
        public Data_Center ToStartPos_Rotate()
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
            ChangeFightingUnit(startUnit, true, TeamStandPoints[0]);
            return startUnit;
        }
        
        async void ToNewUnit()
        {
            await UniTask.DelayFrame(100);
            RandomToAliveUnit();
        }
        
        public void TeamsIni_Rotate(float TeamHpRate, CriticalGaugeMode teamCGMode)
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
                        ToNewUnit();
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
        
        // 切换队员
        public bool ChangeFightingUnit(Data_Center _changeTo, bool emptyState = false, Transform IniStandPoint = null)
        {
            if (!(TeamMembers.GetValues().Count > 1))
            {
                return false;
            }
            if (_changeTo.IsDead.Value)
            {
                return false;
            }
            var unitChanged = false;
            var targetPos = Vector3.zero;
            var targetRot = Quaternion.identity;
            if (IniStandPoint != null)
            {
                targetPos = IniStandPoint.position;
                targetRot = IniStandPoint.rotation;
            }
            else
            {
                if (RMode_Unit != null)
                {
                    targetPos = RMode_Unit.Value.transform.position;
                    targetRot = RMode_Unit.Value.transform.rotation;
                }
            }

            foreach (var data_Center in TeamMembers.GetValues())
            {
                if (_changeTo == data_Center)
                {
                    if (RMode_Unit.Value != null && _changeTo != null) //继承hit数
                    {
                        _changeTo.FightDataRef._comboHitCount.HitCount.Value = RMode_Unit.Value.FightDataRef._comboHitCount.HitCount.Value;
                    }
                    Sensor.AddOrRemoveSharedUnits(RMode_Unit.Value, this.teamConfig.myTeam, false);
                    Sensor.AddOrRemoveSharedUnits(_changeTo, this.teamConfig.myTeam, true);
                    
                    RMode_Unit.Value = _changeTo;
                    RMode_Unit.Value.WholeT.gameObject.SetActive(true);
                    Debug.Log(TeamMembers.GetValues().Count +":" +emptyState) ;
                    if (emptyState)
                    {
                        RMode_Unit.Value._MyBehaviorRunner.ChangeState("Empty");
                    }
                    else
                    {
                        Debug.Log(RMode_Unit.Value );
                        RMode_Unit.Value._MyBehaviorRunner.ChangeToWaitingState();
                    }
                    RMode_Unit.Value.WholeT.transform.position = targetPos;
                    RMode_Unit.Value.WholeT.transform.rotation = targetRot;
                    EffectsManager.GenerateEffect("memberShift", null, RMode_Unit.Value.WholeT.transform.position, Quaternion.identity, RMode_Unit.Value.geometryCenter);
                    unitChanged = true;
                }
                else
                {
                    if (data_Center._MyBehaviorRunner.GetNowState().StateKey != "Empty")
                    {
                        data_Center._MyBehaviorRunner.ChangeState("Empty");
                        //data_Center.WholeT.transform.position = new Vector3(9999, 600, 9999);
                    }
                    data_Center.WholeT.gameObject.SetActive(false);
                }
            }
            
            if (teamConfig.myTeam == RTFightManager.playerTeam)
            {
                InputsManager?.FocusUnit(RMode_Unit.Value);
            }
            
            //Refresh(TeamMembers);
            return unitChanged;
        }
        
        // 计算时间统计可上场角色，更新上场冷却图标UI
        void WaitToTriggerMemberChange()
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
                ChangeFightingUnit(waitingMember);
                waitingMember = null;
            }
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
        
        bool RandomToAliveUnit()
        {
            if (waitingMember != null && waitingMember.FightDataRef.CurrentHp.Value > 0)
            {
                if (!waitingMember.IsDead.Value)
                {
                    if (ChangeFightingUnit(waitingMember))
                    {
                        return true;
                    }
                }
            }
            foreach (var data_Center in TeamMembers.GetValues())
            {
                if (!data_Center.IsDead.Value)
                {
                    if (ChangeFightingUnit(data_Center))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}