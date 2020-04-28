using UnityEngine;
using System.Reflection;

namespace Soul
{
    public abstract partial class Behavior
    {
        Collider threat;
        Collider nearestEnemyMeat;
        
        public bool LosingDefendStrength() // Dash_Back_State G_Ani_MoveEscape_State 1
        {
            return _AIStateRunner.GetNowState().StateKey == "Defend" && _ResistanceManager.Resistance.Value < 2;
        }
        
        public bool DangerousNearby() // Dash_Back_State G_Ani_MoveEscape_State 2
        {
            return (_FightAttriCalReference.IFgettingDamage() || Sensor.GetSuddenThreatInRange(0,5) != null) && _ResistanceManager.Resistance.Value == 0;
        }
        
        public bool DangerousClose() //Counter_State 1 2 3
        {
            return Sensor.GetSuddenThreatInRange(0,5) != null;
        }
        
        public bool CounterComingEnergy()
        {
            nearestEnemyMeat = Sensor.GetTargetRangeEnemyCollider(0,5);
            threat = Sensor.GetSuddenThreatInRange(5, 15);
            return nearestEnemyMeat == null && (threat != null);
        }
        
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
            threat = Sensor.GetSuddenThreatInRange(0, 5);
            nearestEnemyMeat = Sensor.GetClosestEnemyColliderInSensorRange();

            if (nearestEnemyMeat != null && threat != null)
            {
                if (Vector3.Distance(nearestEnemyMeat.transform.position, _DATA_CENTER.geometryCenter.position) >  Vector3.Distance(threat.transform.position, _DATA_CENTER.geometryCenter.position))
                {
                    return true;
                }
            }
            else
            {
                if (threat != null)
                {
                    return true;
                }
            }
            return false;
        }

        Collider tar;
        public bool TimeToAttack()
        {
            if (Sensor.EnemyAndTeammateBetweenMeAndEnemy() != null)
            {
                return false;
            }
            
            // 从移动状态到攻击的话技能释放范围要求精准，但连招情况明明敌人在眼前但因为按技能最好范围而言“不够远”而不释放的话，会很奇怪
            if (_AIStateRunner.GetNowState() == _AIStateRunner.commandWaitingState)
                tar = Sensor.GetTargetRangeEnemyCollider(triggerAtttackRangeMin, triggerAtttackRangeMax);
            else
                tar = Sensor.GetTargetRangeEnemyCollider(Mathf.Clamp(triggerAtttackRangeMin - 3f,0, triggerAtttackRangeMin - 4), triggerAtttackRangeMax);
            return tar != null;
        }

        public bool TimeToRespond()
        {
            threat = Sensor.GetSuddenThreatInRange(0, 5);
            return threat == null;
        }
        
        public bool TimeToStopRunning()
        {
            nearestEnemyMeat = Sensor.GetClosestEnemyColliderInSensorRange();
            return (nearestEnemyMeat != null && Vector3.Distance(nearestEnemyMeat.transform.position, this._DATA_CENTER.WholeT.position) < 5f) || Sensor.GetSuddenThreatInRange(0,8) != null;
        }
        
        public bool CheckTriggerCondition(string conditionFunctionName)
        {
            System.Type T = typeof(Behavior);
            MethodInfo theMethod = T.GetMethod(conditionFunctionName); //激活同名函数
            return theMethod != null && (bool)theMethod.Invoke(this, null);
        }

        public bool CheckExitCondition(string stateKey)
        {
            _AIStateRunner.BehaviourAndStrategicExitCondition.TryGetValue(stateKey, out string exitCondition);
            switch (exitCondition)
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