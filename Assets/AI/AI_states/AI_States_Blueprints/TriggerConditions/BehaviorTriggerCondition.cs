using System.Collections.Generic;
using UnityEngine;

namespace Soul
{
    public abstract partial class Behavior
    {
        //public List<string> priority1 = new List<string>();
        //public List<string> priority2 = new List<string>();
        //public List<string> priority3 = new List<string>();
        public string strategic_exit_condition_code;
        
        public bool LosingDefendStrength() // Dash_Back_State G_Ani_MoveEscape_State 1
        {
            return _AIStateRunner.GetNowState().StateKey == "Defend" && _ResistanceManager.Resistance.Value < 2;
        }
        
        public bool DangerousNearby() // Dash_Back_State G_Ani_MoveEscape_State 2
        {
            return (_FightAttriCalReference.IFgettingDamage() || Sensor.GetNearbyDamagingWeaponColliders().Count > 0) && _ResistanceManager.Resistance.Value == 0;
        }
        
        public bool DangerousClose() //Counter_State 1 2 3
        {
            return Sensor.GetNearbyDamagingWeaponColliders().Count > 0 && CheckToEnemyDisEnterCondition(behaviorEnterRanges);
        }
        
        List<Collider> damagingweaponList;
        List<Collider> nearbyenemymeat;
        public bool DangerousVeryClose() //Defend_State 1 
        {
            if (_ResistanceManager.Resistance.Value > 0)
            {
                return false;
            }
            if (_FightAttriCalReference.IFgettingDamage())
            {
                return true;
            }
            damagingweaponList = Sensor.GetNearbyDamagingWeaponColliders();
            nearbyenemymeat = Sensor.GetInnerEnemiesColliders();
            if (nearbyenemymeat.Count == 0)
            {
                if (damagingweaponList.Count > 0)
                {
                    return true;
                }
            }
            else{
                if (damagingweaponList.Count > 0)
                {
                    if (Vector3.Distance(nearbyenemymeat[0].transform.position, _DATA_CENTER.geometryCenter.position) >
                        Vector3.Distance(damagingweaponList[0].transform.position, _DATA_CENTER.geometryCenter.position))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        
        public bool MayBeDefend() //Defend_State 3
        {
            return (Sensor.EnemyAndTeammateBetweenMeAndEnemy() == null && Sensor.GetInnerEnemiesColliders().Count > 0) && _ResistanceManager.Resistance.Value == 0;
        }
        
        public bool TimeToAttack() //G_Attack_State 。。原本有个skillEmergentLevel的机制。。一般是2
        {
            if (Sensor.EnemyAndTeammateBetweenMeAndEnemy() != null)
                return false;
            if (_AIStateRunner.GetNowState() != null)
            {
                if (_AIStateRunner.GetNowState().nextAttackStateCanRushFirst == true)
                    return CheckToEnemyDisEnterCondition(this.InnerAndMidAndFarRanges);
    
                //if (this._AIStateRunner.getNowState().StateType == stateType.GR ||
                    //this._AIStateRunner.getNowState().StateType == stateType.GM ||
                    //this._AIStateRunner.getNowState().StateType == stateType.GI)
                    //return this.checkToEnemyDisEnterCondition(RangePlusOne(this.behaviorEnterRanges));
            }
            return CheckToEnemyDisEnterCondition(this.behaviorEnterRanges);
        }
        
        public bool TimeToDashAttack()
        {
            return _AIStateRunner.GetNowState() != null &&
            (_AIStateRunner.GetNowState().StateType == BehaviorType.GI ||
            _AIStateRunner.GetNowState().StateType == BehaviorType.GR ||
            _AIStateRunner.GetNowState().StateType == BehaviorType.GM ||
            _AIStateRunner.GetNowState().StateType == BehaviorType.AC) 
            && Sensor.EnemyAndTeammateBetweenMeAndEnemy() == null
            ? CheckToEnemyDisEnterCondition(RangePlusOne(behaviorEnterRanges))
            : Sensor.EnemyAndTeammateBetweenMeAndEnemy() == null && CheckToEnemyDisEnterCondition(behaviorEnterRanges);
        }
        
        public bool TimeToRespond()
        {
            damagingweaponList = Sensor.GetNearbyDamagingWeaponColliders();
            return damagingweaponList.Count == 0;
        }
        
        public bool TimeToStopRunning()
        {
            return Sensor.GetInnerEnemiesColliders().Count > 0 || Sensor.GetNearbyDamagingWeaponColliders().Count > 0 || Sensor.GetOutterDamagingWeaponColliders().Count > 0;
        }
        
        public bool CheckTriggerCondition(string conditionFunctionName)
        {
            switch(conditionFunctionName)
            {
                case "LosingDefendStrength":
                    return LosingDefendStrength();
                case "DangerousNearby":
                    return DangerousNearby();
                case "TimeToAttack":
                    return TimeToAttack();
                case "TimeToDashAttack":
                    return TimeToDashAttack();
                case "DangerousClose":
                    return DangerousClose();
                case "DangerousVeryClose":
                    return DangerousVeryClose();
                case "MayBeDefend":
                    return MayBeDefend();
                default:
                    return false;
            }
        }
        
        public bool CheckExitCondition(string exitFunctionName)
        {
            switch(exitFunctionName)
            {
                case "TimeToRespond":
                    return TimeToRespond();
                case "TimeToStopRunning":
                    return TimeToStopRunning();
                default:
                    return true;
            }
        }
    }
}