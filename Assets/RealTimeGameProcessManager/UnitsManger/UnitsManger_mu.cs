using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace FightScene
{
    public partial class UnitsManger : MonoBehaviour
    {
        public void AllUnitsStartOff(bool TestMode = false)
        {
            foreach (var oneMember in TeamMembers.GetValues())
            {
                Sensor.AddOrRemoveSharedUnits(oneMember, teamConfig.myTeam, true);
                if (!TestMode)
                    oneMember._MyBehaviorRunner.ChangeToWaitingState();
                else
                {
                    oneMember._MyBehaviorRunner.ChangeToTestMode();
                }
            }
        }
        
        public Data_Center ToStartPos_Multi()
        {
            Data_Center startUnit = null;
            foreach (KeyValuePair<(int, int), Data_Center> kv in TeamMembers.mDict)
            {
                Data_Center _DataCenter = TeamMembers.Get(kv.Key.Item1, kv.Key.Item2);
                if (_DataCenter == null)
                {
                    continue;
                }
                if (startUnit == null)
                    startUnit = kv.Value;
                if (TeamStandPoints[kv.Key.Item2] != null)
                {
                    _DataCenter.WholeT.transform.position = TeamStandPoints[kv.Key.Item2].position;
                    _DataCenter.WholeT.transform.rotation = TeamStandPoints[kv.Key.Item2].rotation;
                    _DataCenter.WholeT.parent = null;
                    _DataCenter.WholeT.gameObject.SetActive(true);
                }
                else
                {
                    Debug.Log("站位逻辑错误。出现了系统未安排的站位点");
                }
            }

            return startUnit;
        }
        
        public void Initialize_Multi(float TeamHpRate, CriticalGaugeMode teamCGMode)
        {
            foreach (var center in TeamMembers.GetValues())
            {
                center.Step3Initialize(teamConfig, TeamHpRate * SkillSet.INI_Hp(RTFightManager.target.UnitInfoRef[center].set.SkillEntityList()), teamCGMode);
                
                center.FightDataRef.CriticalGauge = new ReactiveProperty<int>();
                
                center._ResistanceManager.Resistance = new ReactiveProperty<int>(0);
                center._ResistanceManager.Resistance.Subscribe(x =>
                {
                    center._ResistanceManager.Resistance.Value = Mathf.Clamp(x, 0, 10);
                }).AddTo(gameObject);
                
                center.FightDataRef._comboHitCount.HitCount.Value = 0;

                center.IsDead = new ReactiveProperty<bool>(false);
                center.IsDead.Subscribe(x => 
                {
                    if (x)
                    {
                        Sensor.AddOrRemoveSharedUnits(center, teamConfig.myTeam, false);
                    }
                }).AddTo(gameObject);
            }
        }
    }
}