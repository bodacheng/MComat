using UnityEngine;
using UniRx;

namespace FightScene
{
    public partial class UnitsManger : MonoBehaviour
    {
        public void AllUnitsStartOff(bool testMode = false)
        {
            foreach (var oneMember in teamMembers.GetValues())
            {
                Sensor.AddOrRemoveSharedUnits(oneMember, teamConfig.myTeam, true);
                if (!testMode)
                    oneMember._MyBehaviorRunner.ChangeToWaitingState();
                else
                {
                    oneMember._MyBehaviorRunner.ChangeToTestMode();
                }
            }
        }
        
        public void ToStartPos_Multi()
        {
            Data_Center unit = null;
            foreach (var kv in teamMembers.mDict)
            {
                var _DataCenter = teamMembers.Get(kv.Key.Item1, kv.Key.Item2);
                if (_DataCenter == null)
                {
                    continue;
                }
                if (unit == null)
                    unit = kv.Value;
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
            
            _startUnit = unit;
        }
        
        public void Initialize_Multi(float teamHpRate, CriticalGaugeMode teamCGMode, AIMode _aiMode, int aiDelayFrame)
        {
            foreach (var center in teamMembers.GetValues())
            {
                center.Step3Initialize(teamConfig, teamCGMode, _aiMode, aiDelayFrame, teamHpRate, RTFightManager.Target.UnitInfoRef[center]);
                center.FightDataRef.IsDead.Subscribe(x => 
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