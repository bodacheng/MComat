using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FightScene
{
    public partial class TeamUIManager : MonoBehaviour
    {
        Data_Center RMode_Unit;
        Data_Center waitingMember;
        readonly IDictionary<Data_Center, float> RefreshTimeDic = new Dictionary<Data_Center, float>();
        Text rotationModeHitCombo;
        
        void RotateClear()
        {
            UnitIconDic.Clear();
            rotationModeHitCombo.text = "";
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
        
        void Rotation_LocalUpdate(MultiDict<int, int, Data_Center> TeamMembers)
        {
            WaitToTriggerMemberChange(TeamMembers);
            if (RMode_Unit != null)
            {
                RefreshComboHitRotationMode(RMode_Unit);
            }
            if (teamConfig.myTeam != RTFightManager.playerTeam)
            {
                BarsPosUpdate(TeamMembers);
            }
        }
        
        // 最初切换队员
        bool ChangeUnit_ReadyToGo(Data_Center _changeTo, MultiDict<int, int, Data_Center> TeamMembers, Transform IniStandPoint)
        {
            var unitChanged = false;
            foreach (var data_Center in TeamMembers.GetValues())
            {
                if (_changeTo == data_Center)
                {
                    RMode_Unit = _changeTo;
                    RMode_Unit.WholeT.transform.position = IniStandPoint.position;
                    RMode_Unit.WholeT.rotation = IniStandPoint.rotation;
                    EffectsManager.GenerateEffect("memberShift", null, RMode_Unit.WholeT.transform.position, Quaternion.identity, RMode_Unit.geometryCenter);
                    unitChanged = true;
                    RMode_Unit.WholeT.gameObject.SetActive(true);
                    if (teamConfig.myTeam != RTFightManager.playerTeam)
                        UnitIconDic[RMode_Unit].gameObject.SetActive(true);
                }
                else
                {
                    data_Center._MyBehaviorRunner.ChangeState("Empty");
                    data_Center.WholeT.gameObject.SetActive(false);
                    if (teamConfig.myTeam != RTFightManager.playerTeam)
                        UnitIconDic[data_Center].gameObject.SetActive(false);
                }
            }
            return unitChanged;
        }
        
        // 切换队员
        bool ChangeFightingUnit(Data_Center _changeTo, MultiDict<int, int, Data_Center> TeamMembers)
        {
            if (!(TeamMembers.GetValues().Count > 1) || RMode_Unit == _changeTo)
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
                targetPos = RMode_Unit.transform.position;
            }
            foreach (var data_Center in TeamMembers.GetValues())
            {
                if (_changeTo == data_Center)
                {
                    if (RMode_Unit != null && _changeTo != null)//继承hit数
                    {
                        _changeTo.FightDataRef._ComboHitCount.HitCount.Value = RMode_Unit.FightDataRef._ComboHitCount.HitCount.Value;
                    }
                    Sensor.AddOrRemoveSharedUnits(RMode_Unit, this.teamConfig.myTeam, false);
                    Sensor.AddOrRemoveSharedUnits(_changeTo, this.teamConfig.myTeam, true);
                    
                    RMode_Unit = _changeTo;
                    RMode_Unit.WholeT.gameObject.SetActive(true);
                    if (teamConfig.myTeam != RTFightManager.playerTeam)
                        UnitIconDic[RMode_Unit].gameObject.SetActive(true);
                    RMode_Unit._MyBehaviorRunner.ChangeToWaitingState();
                    RMode_Unit.WholeT.transform.position = targetPos;
                    EffectsManager.GenerateEffect("memberShift", null, RMode_Unit.WholeT.transform.position, Quaternion.identity, RMode_Unit.geometryCenter);
                    unitChanged = true;
                }
                else
                {
                    if (data_Center._MyBehaviorRunner.GetNowState().StateKey != "Empty")
                    {
                        data_Center._MyBehaviorRunner.ChangeState("Empty");
                        //data_Center.WholeT.transform.position = new Vector3(9999, 600, 9999);
                        data_Center.WholeT.gameObject.SetActive(false);
                        if (teamConfig.myTeam != RTFightManager.playerTeam)
                            UnitIconDic[data_Center].gameObject.SetActive(false);
                    }
                }
            }
            if (teamConfig.myTeam == RTFightManager.playerTeam)
            {
                RTFightManager.target.SwitchToCMode(RMode_Unit, MobileInputsManager.playerMode);
            }
            RTFightManager.target.ParaAdjustment(RTFightManager.playerTeam);
            RTFightManager.target.Refresh();
            return unitChanged;
        }
        
        // 计算时间统计可上场角色，更新上场冷却图标UI
        void WaitToTriggerMemberChange(MultiDict<int, int, Data_Center> TeamMembers)
        {
            for (var i = 0; i < TeamMembers.GetValues().Count; i++)
            {
                if (RefreshTimeDic[TeamMembers.GetValues()[i]] > 0)
                {
                    RefreshTimeDic[TeamMembers.GetValues()[i]] -= Time.deltaTime; // 角色切换倒计时;
                    UnitIconDic[TeamMembers.GetValues()[i]].focusingCharIcon.CooldownCurtainUpdate(RefreshTimeDic[TeamMembers.GetValues()[i]] / 10);
                }
            }
            
            if (waitingMember != null && CanChangeToThisMember(waitingMember))
            {
                RefreshTimeDic[RMode_Unit] = 10f;
                ChangeFightingUnit(waitingMember, TeamMembers);
                waitingMember = null;
            }
        }
        
        bool CanChangeToThisMember(Data_Center targetMember)
        {
            if (targetMember == RMode_Unit)
            {
                return false;
            }
            if (targetMember.IsDead.Value)
            {
                return false;
            }
            if (RefreshTimeDic[targetMember] > 0)
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

        void ReadyForNextMember(Data_Center nextOne)
        {
            if (waitingMember != nextOne)
            {
                waitingMember = nextOne;
            }
        }

        /// <summary>
        /// 本质上这个函数是AI。。。而AI按理说应该和其他东西是分层的。。
        /// </summary>
        float time_counter;
        public void TurnModeEnemySideAutoMemberShaft(MultiDict<int, int, Data_Center> TeamMembers)
        {
            time_counter += Time.deltaTime;
            if (RMode_Unit != null && RMode_Unit.IsDead.Value)
            {
                if (TeamMembers.GetValues().Count > 0)
                {
                    for (int i = 0; i < TeamMembers.GetValues().Count; i++)
                    {
                        ReadyForNextMember(TeamMembers.GetValues()[i]);
                    }
                }
            }
            if (time_counter > 6f)
            {
                if (TeamMembers.GetValues().Count > 0)
                {
                    for (int i = 0; i < TeamMembers.GetValues().Count; i++)
                    {
                        if (RefreshTimeDic[TeamMembers.GetValues()[i]] <= 0)
                        {
                            ReadyForNextMember(TeamMembers.GetValues()[i]);
                        }
                        time_counter = 0f;
                    }
                }
                time_counter = 0f;
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