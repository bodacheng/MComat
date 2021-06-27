using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Cysharp.Threading.Tasks;

namespace FightScene
{
    public partial class FightTeam_RotationMode : FightTeam
    {
        Data_Center RotationMode_fightingMember;
        Data_Center waitingMember;
        IDictionary<Data_Center, float> RefreshTimeDic = new Dictionary<Data_Center, float>();
        Text rotationModeHitCombo;
        
        public override List<Transform> TeamMemberTransforms()
        {
            List<Transform> transforms = new List<Transform>
            {
                RotationMode_fightingMember.transform
            };
            return transforms;
        }
        
        public override void Clear()
        {
            foreach (Data_Center one in TeamMembers.values)
            {
                one.FightDataRef.Clear();
            }
            CharIconDic.Clear();
            rotationModeHitCombo.text = "";
        }
        
        public override void ArrangeAllTeamMembersToPosition(MultiDict<int, int, Data_Center> heromultiDictionary)
        {
            foreach (KeyValuePair<int, List<int>> keys in heromultiDictionary.GetAllUnNullKeys())
            {
                foreach (int key in keys.Value)
                {
                    Data_Center character_data_Center = heromultiDictionary.Get(keys.Key, key);
                    if (character_data_Center == null)
                    {
                        continue;
                    }
                    character_data_Center.WholeT.parent = null;
                    character_data_Center.WholeT.gameObject.SetActive(true);
                }
            }
            ChangeFightingMember_ReadyToGo(heromultiDictionary.values[0], TeamStandPoints[0]);
        }
        
        public void Rotation_LocalFightingUpdate()
        {
            WaitToTriggerMemberChange();
            if (RotationMode_fightingMember != null)
            {
                RefreshComboHitRotationMode(RotationMode_fightingMember);
            }
            if (teamConfig.myTeam != RealTimeGameProcessManager.playerTeam)
            {
                BarsPositionUpdate();
            }
        }
        
        // 最初切换队员
        public bool ChangeFightingMember_ReadyToGo(Data_Center _changeTo, Transform IniStandPoint)
        {
            bool memberchanged = false;
            foreach (Data_Center data_Center in TeamMembers.values)
            {
                if (_changeTo == data_Center)
                {
                    RotationMode_fightingMember = _changeTo;
                    RotationMode_fightingMember.WholeT.transform.position = IniStandPoint.position;
                    RotationMode_fightingMember.WholeT.rotation = IniStandPoint.rotation;
                    EffectsManager.GenerateEffect("membershift", null, RotationMode_fightingMember.WholeT.transform.position, Quaternion.identity, RotationMode_fightingMember.geometryCenter);
                    memberchanged = true;
                    RotationMode_fightingMember.WholeT.gameObject.SetActive(true);
                    if (teamConfig.myTeam != RealTimeGameProcessManager.playerTeam)
                        CharIconDic[RotationMode_fightingMember].gameObject.SetActive(true);
                }
                else
                {
                    data_Center._MyBehaviorRunner.ChangeState("Empty");
                    //data_Center.WholeT.transform.position = new Vector3(9999, 600, 9999);
                    data_Center.WholeT.gameObject.SetActive(false);
                    if (teamConfig.myTeam != RealTimeGameProcessManager.playerTeam)
                        CharIconDic[data_Center].gameObject.SetActive(false);
                }
            }
            return memberchanged;
        }
        
        // 切换队员
        bool ChangeFightingMember(Data_Center _changeTo)
        {
            if (!(TeamMembers.values.Count > 1) || RotationMode_fightingMember == _changeTo)
            {
                return false;
            }
            if (_changeTo.IsDead.Value)
            {
                return false;
            }
            bool memberchanged = false;
            Vector3 targetposition = Vector3.zero;
            if (RotationMode_fightingMember != null)
            {
                targetposition = RotationMode_fightingMember.transform.position;
            }
            foreach (Data_Center data_Center in TeamMembers.values)
            {
                if (_changeTo == data_Center)
                {
                    if (RotationMode_fightingMember != null && _changeTo != null)//继承hit数
                    {
                        _changeTo.FightDataRef._ComboHitCount.HitCount.Value = RotationMode_fightingMember.FightDataRef._ComboHitCount.HitCount.Value;
                    }
                    RealTimeGameProcessManager.AddOrRemoveFightingMember(RotationMode_fightingMember, this.teamConfig.myTeam, false);
                    RealTimeGameProcessManager.AddOrRemoveFightingMember(_changeTo, this.teamConfig.myTeam, true);

                    RotationMode_fightingMember = _changeTo;
                    RotationMode_fightingMember.WholeT.gameObject.SetActive(true);
                    if (teamConfig.myTeam != RealTimeGameProcessManager.playerTeam)
                        CharIconDic[RotationMode_fightingMember].gameObject.SetActive(true);
                    RotationMode_fightingMember._MyBehaviorRunner.ChangeToWaitingState();
                    RotationMode_fightingMember.WholeT.transform.position = targetposition;
                    EffectsManager.GenerateEffect("membershift", null, RotationMode_fightingMember.WholeT.transform.position, Quaternion.identity, RotationMode_fightingMember.geometryCenter);
                    memberchanged = true;
                }
                else
                {
                    if (data_Center._MyBehaviorRunner.GetNowState().StateKey != "Empty")
                    {
                        data_Center._MyBehaviorRunner.ChangeState("Empty");
                        //data_Center.WholeT.transform.position = new Vector3(9999, 600, 9999);
                        data_Center.WholeT.gameObject.SetActive(false);
                        if (teamConfig.myTeam != RealTimeGameProcessManager.playerTeam)
                            CharIconDic[data_Center].gameObject.SetActive(false);
                    }
                }
            }
            if (teamConfig.myTeam == RealTimeGameProcessManager.playerTeam)
            {
                RealTimeGameProcessManager.target.SwitchToCMode(RotationMode_fightingMember, MobileInputsManager.playerMode);
            }
            RealTimeGameProcessManager.target.CameraParaAdjustment(RealTimeGameProcessManager.playerTeam);
            RealTimeGameProcessManager.target.Refresh();
            return memberchanged;
        }

        public override void ModeStart()
        {
            RealTimeGameProcessManager.AddOrRemoveFightingMember(RotationMode_fightingMember, this.teamConfig.myTeam, true);
            RotationMode_fightingMember._MyBehaviorRunner.ChangeToWaitingState();
        }

        // 计算时间统计可上场角色，更新上场冷却图标UI
        void WaitToTriggerMemberChange()
        {
            for (int i = 0; i < TeamMembers.values.Count; i++)
            {
                if (RefreshTimeDic[TeamMembers.values[i]] > 0)
                {
                    RefreshTimeDic[TeamMembers.values[i]] -= Time.deltaTime; // 角色切换倒计时;
                    CharIconDic[TeamMembers.values[i]].focusingCharIcon.CooldownCurtainUpdate(RefreshTimeDic[TeamMembers.values[i]] / 10);
                }
            }
            
            if (waitingMember != null && CanChangeToThisMember(waitingMember))
            {
                RefreshTimeDic[RotationMode_fightingMember] = 10f;
                ChangeFightingMember(waitingMember);
                waitingMember = null;
            }
        }
        
        bool CanChangeToThisMember(Data_Center targetMember)
        {
            if (targetMember == RotationMode_fightingMember)
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
        public void TurnModeEnemySideAutoMemberShaft()
        {
            time_counter += Time.deltaTime;
            if (RotationMode_fightingMember != null && RotationMode_fightingMember.IsDead.Value)
            {
                if (TeamMembers.values.Count > 0)
                {
                    for (int i = 0; i < TeamMembers.values.Count; i++)
                    {
                        ReadyForNextMember(TeamMembers.values[i]);
                    }
                }
            }
            if (time_counter > 6f)
            {
                if (TeamMembers.values.Count > 0)
                {
                    for (int i = 0; i < TeamMembers.values.Count; i++)
                    {
                        if (RefreshTimeDic[TeamMembers.values[i]] <= 0)
                        {
                            ReadyForNextMember(TeamMembers.values[i]);
                        }
                        time_counter = 0f;
                    }
                }
                time_counter = 0f;
            }
        }
        
        public bool RandomChangeAliveFightingMember()
        {
            if (waitingMember != null && waitingMember.FightDataRef.CurrentHp.Value > 0)
            {
                if (!waitingMember.IsDead.Value)
                {
                    if (ChangeFightingMember(waitingMember))
                    {
                        return true;
                    }
                }
            }
            foreach (Data_Center data_Center in TeamMembers.values)
            {
                if (!data_Center.IsDead.Value)
                {
                    if (ChangeFightingMember(data_Center))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}