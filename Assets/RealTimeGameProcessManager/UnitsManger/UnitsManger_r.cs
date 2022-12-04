using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;

namespace FightScene
{
    public partial class UnitsManger : MonoBehaviour
    {
        public ReactiveProperty<Data_Center> RMode_Unit = new ();
        Data_Center waitingMember;
        
        public void ToStartPos_Rotate()
        {
            Data_Center unit = null;
            foreach (var kv in teamMembers.mDict)
            {
                if (kv.Value == null)
                {
                    continue;
                }
                
                if (unit == null)
                    unit = kv.Value;
                kv.Value.WholeT.parent = null;
                kv.Value.WholeT.gameObject.SetActive(true);
            }

            StartUnit = unit;
            ChangeFightingUnit(unit, true, TeamStandPoints[0]);
        }
        
        async void ToNewUnit()
        {
            await UniTask.DelayFrame(100);
            RandomToAliveUnit();
        }
        
        public void TeamsIni_Rotate(float teamHpRate, CriticalGaugeMode teamCGMode, AIMode _aiMode, int aiDelayFrame)
        {
            foreach (var center in teamMembers.GetValues())
            {
                //  时间刷新整备
                RTFightManager.Target.RefreshTimeDic.Add(center, new ReactiveProperty<float>(0));
                center.Step3Initialize(teamConfig, teamCGMode, _aiMode, aiDelayFrame, teamHpRate, RTFightManager.Target.UnitInfoRef[center]);
                center.FightDataRef.IsDead.Subscribe(x => {
                    if (x) 
                    {
                        Sensor.AddOrRemoveSharedUnits(center, teamConfig.myTeam, false);
                        ToNewUnit();
                    }
                }).AddTo(gameObject);
            }
        }
        
        // 切换队员
        public bool ChangeFightingUnit(Data_Center changeTo = null, bool emptyState = false, Transform iniStandPoint = null)
        {
            if (changeTo == null)
            {
                changeTo = StartUnit;
                if (changeTo == null)
                    return false;
            }
            
            if (changeTo.FightDataRef.IsDead.Value)
            {
                return false;
            }
            var unitChanged = false;
            var targetPos = Vector3.zero;
            var targetRot = Quaternion.identity;
            if (iniStandPoint != null)
            {
                targetPos = iniStandPoint.position;
                targetRot = iniStandPoint.rotation;
            }
            else
            {
                if (RMode_Unit != null)
                {
                    targetPos = RMode_Unit.Value.transform.position;
                    targetRot = Quaternion.Euler(new Vector3(1,0,1));
                }
            }
            
            foreach (var dataCenter in teamMembers.GetValues())
            {
                if (changeTo == dataCenter)
                {
                    if (RMode_Unit.Value != null && changeTo != null) //继承hit数
                    {
                        changeTo.FightDataRef._comboHitCount.HitCount.Value = RMode_Unit.Value.FightDataRef._comboHitCount.HitCount.Value;
                    }
                    Sensor.AddOrRemoveSharedUnits(RMode_Unit.Value, teamConfig.myTeam, false);
                    Sensor.AddOrRemoveSharedUnits(changeTo, teamConfig.myTeam, true);
                    RMode_Unit.Value = changeTo;
                    RMode_Unit.Value.WholeT.gameObject.SetActive(true);
                    
                    if (emptyState)
                    {
                        RMode_Unit.Value._MyBehaviorRunner.ChangeState("Empty");
                    }
                    else
                    {
                        RMode_Unit.Value._MyBehaviorRunner.ChangeToWaitingState();
                    }
                    RMode_Unit.Value.WholeT.transform.position = targetPos;
                    RMode_Unit.Value.WholeT.transform.rotation = targetRot;
                    EffectsManager.GenerateEffect("memberShift", null, RMode_Unit.Value.WholeT.transform.position, Quaternion.identity, RMode_Unit.Value.geometryCenter).Forget();
                    unitChanged = true;
                }
                else
                {
                    if (dataCenter._MyBehaviorRunner.GetNowState().StateKey != "Empty")
                    {
                        dataCenter._MyBehaviorRunner.ChangeState("Empty");
                    }
                    dataCenter.WholeT.gameObject.SetActive(false);
                }
            }
            
            if (teamConfig.myTeam == RTFightManager.playerTeam && InputsManager != null)
            {
                InputsManager.FocusUnit(RMode_Unit.Value);
            }
            
            //Refresh(TeamMembers);
            return unitChanged;
        }
        
        // 计算时间统计可上场角色，更新上场冷却图标UI
        void WaitUnitChange()
        {
            for (var i = 0; i < teamMembers.GetValues().Count; i++)
            {
                if (RTFightManager.Target.RefreshTimeDic[teamMembers.GetValues()[i]].Value > 0)
                {
                    RTFightManager.Target.RefreshTimeDic[teamMembers.GetValues()[i]].Value -= Time.deltaTime; // 角色切换倒计时;
                }
            }
            
            if (waitingMember != null && CanChangeToThisMember(waitingMember))
            {
                RTFightManager.Target.RefreshTimeDic[RMode_Unit.Value].Value = 10f;
                ChangeFightingUnit(waitingMember);
                waitingMember = null;
            }
        }
        
        bool CanChangeToThisMember(Data_Center target)
        {
            if (target == RMode_Unit.Value)
            {
                return false;
            }
            if (target.FightDataRef.IsDead.Value)
            {
                return false;
            }
            if (RTFightManager.Target.RefreshTimeDic[target].Value > 0)
            {
                return false;
            }
            if (target._MyBehaviorRunner.GetNowState().StateType == Skill.BehaviorType.Hit || target._MyBehaviorRunner.GetNowState().StateType == Skill.BehaviorType.KnockOff)
            {
                return false;
            }
            if (target._MyBehaviorRunner.GetNowState().StateType == Skill.BehaviorType.GI || target._MyBehaviorRunner.GetNowState().StateType == Skill.BehaviorType.GM || target._MyBehaviorRunner.GetNowState().StateType == Skill.BehaviorType.GR)
            {
                if (!target._SkillCancelFlag.Cancel_Flag)
                    return true;
            }
            return true;
        }
        
        public void ReadyForNextMember(Data_Center next)
        {
            if (waitingMember != next)
            {
                waitingMember = next;
            }
        }
        
        bool RandomToAliveUnit()
        {
            if (waitingMember != null && waitingMember.FightDataRef.CurrentHp.Value > 0)
            {
                if (!waitingMember.FightDataRef.IsDead.Value)
                {
                    if (ChangeFightingUnit(waitingMember))
                    {
                        return true;
                    }
                }
            }
            foreach (var dataCenter in teamMembers.GetValues())
            {
                if (!dataCenter.FightDataRef.IsDead.Value)
                {
                    if (ChangeFightingUnit(dataCenter))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}