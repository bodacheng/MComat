using UnityEngine;
using System.Reflection;

namespace Soul
{
    public abstract partial class Behavior
    {
        Collider tempCollider1, tempCollider2;

        public bool SpareOption()
        {
            return true;
        }

        public bool LosingDefendStrength() // Dash_Back_State G_Ani_MoveEscape_State 1
        {
            return _AIStateRunner.GetNowState().StateKey == "Defend" && _ResistanceManager.Resistance.Value < 2;
        }
        
        public bool DangerousNearby() // Dash_Back_State G_Ani_MoveEscape_State 2
        {
            return Sensor.GetSuddenThreatInRange(0 , 5) != null && _ResistanceManager.Resistance.Value == 0;
        }
        
        public bool DangerousClose() //Counter_State 1 2 3
        {
            return Sensor.GetSuddenThreatInRange(0, 3) != null;
        }
        
        public bool CounterComingEnergy()
        {
            Collider nearestEnemyMeat = Sensor.GetTargetRangeEnemyCollider(0, 5);
            Collider threat = Sensor.GetSuddenThreatInRange(5, 15);
            return nearestEnemyMeat == null && (threat != null);
        }
        
        public bool CT()
        {
            return !OnBuff() && DangerousVeryClose();
        }
        
        public bool OnBuff()
        {
            return _DATA_CENTER.buffsRunner.mysubmissions.Count > 0;
        }


        public bool DangerousVeryClose() //CT
        {
            if (_ResistanceManager.Resistance.Value > 0)
            {
                return false;
            }
            tempCollider1 = Sensor.GetSuddenThreatInRange(0, 3);
            tempCollider2 = Sensor.GetClosestEnemyColliderInSensorRange();
            
            if (tempCollider2 != null && tempCollider1 != null)
            {
                if (Vector3.Distance(tempCollider2.transform.position, _DATA_CENTER.geometryCenter.position) >  Vector3.Distance(tempCollider1.transform.position, _DATA_CENTER.geometryCenter.position))
                {
                    return true;
                }
            }
            else
            {
                if (tempCollider1 != null)
                {
                    return true;
                }
            }
            return false;
        }

        public bool EnemyClose()
        {
            tempCollider1 = Sensor.GetTargetRangeEnemyCollider(0, 5);
            return tempCollider1 != null;
        }

        public bool TimeToAttack()
        {
            if (Sensor.EnemyAndTeammateBetweenMeAndEnemy() != null)
            {
                return false;
            }
            
            // 从移动状态到攻击的话技能释放范围要求精准，但连招情况明明敌人在眼前但因为按技能最好范围而言“不够远”而不释放的话，会很奇怪
            //if (_AIStateRunner.GetNowState() == _AIStateRunner.commandWaitingState)
                tempCollider1 = Sensor.GetTargetRangeEnemyCollider(triggerAtttackRangeMin, triggerAtttackRangeMax);
            //else
                //tar = Sensor.GetTargetRangeEnemyCollider(Mathf.Clamp(triggerAtttackRangeMin - 3f, 0, triggerAtttackRangeMin - 3f), triggerAtttackRangeMax);
            switch (triggerAtttackHeight)
            {
                case -1:// 只适合砸地
                    return (tempCollider1 != null) && tempCollider1.transform.position.y < 0.5f;
                case 0:// 只适合中段
                    return (tempCollider1 != null) && tempCollider1.transform.position.y >= 0.8f;
                case 1:// 只适合对空和打脑袋
                    return (tempCollider1 != null) && tempCollider1.transform.position.y >= 1f;
                case 2:// 全高度适合
                    break;
            }
            return tempCollider1 != null;
        }
        
        public bool TimeToAttack_Reluctant()
        {
            if (Sensor.EnemyAndTeammateBetweenMeAndEnemy() != null)
            {
                return false;
            }
            
            // 从移动状态到攻击的话技能释放范围要求精准，但连招情况明明敌人在眼前但因为按技能最好范围而言“不够远”而不释放的话，会很奇怪
            //if (_AIStateRunner.GetNowState() == _AIStateRunner.commandWaitingState)
                tempCollider1 = Sensor.GetTargetRangeEnemyCollider(0, triggerAtttackRangeMax);
            //else
                //tar = Sensor.GetTargetRangeEnemyCollider(Mathf.Clamp(triggerAtttackRangeMin - 3f, 0, triggerAtttackRangeMin - 3f), triggerAtttackRangeMax);
            
            switch (triggerAtttackHeight)
            {
                case -1:// 只适合砸地
                    return (tempCollider1 != null) && tempCollider1.transform.position.y < 0.5f;
                case 0:// 只适合中段
                    return (tempCollider1 != null) && tempCollider1.transform.position.y >= 0.8f;
                case 1:// 只适合对空和打脑袋
                    return (tempCollider1 != null) && tempCollider1.transform.position.y >= 1f;
                case 2:// 全高度适合
                    break;
            }
            return tempCollider1 != null;
        }

        public bool TimeToRespond()
        {
            Collider threat = Sensor.GetSuddenThreatInRange(0, 5);
            return threat == null;
        }
        
        public bool TimeToStopRunning() //没有意义的条件。 
        {
            Collider nearestEnemyMeat = Sensor.GetClosestEnemyColliderInSensorRange();
            return (nearestEnemyMeat != null && Vector3.Distance(nearestEnemyMeat.transform.position, this._DATA_CENTER.WholeT.position) < 5f) || Sensor.GetSuddenThreatInRange(0,8) != null;
        }
        
        public bool CheckTriggerCondition(string conditionFunctionName)
        {
            var T = typeof(Behavior);
            var theMethod = T.GetMethod(conditionFunctionName); //激活同名函数
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