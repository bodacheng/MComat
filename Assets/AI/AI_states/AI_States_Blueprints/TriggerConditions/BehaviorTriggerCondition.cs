using System.Collections.Generic;

namespace Soul
{
    public abstract partial class Behavior
    {
        public List<string> priority1 = new List<string>();
        public List<string> priority2 = new List<string>();
        public List<string> priority3 = new List<string>();
        
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
                default:
                    return false;
            }
        }
    }
}