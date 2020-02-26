using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using Skill;

namespace Soul
{
    public abstract partial class Behavior
    {
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
            return Sensor.GetNearbyDamagingWeaponColliders().Count > 0;
        }

        public bool DangerousNearButEnemyFar()
        {
            nearestEnemyMeat = Sensor.GetClosestEnemyColliderInSensorRange();
            damagingweaponList = Sensor.GetOutterDamagingWeaponColliders();
            return (nearestEnemyMeat != null && Vector3.Distance(nearestEnemyMeat.transform.position,this._DATA_CENTER.WholeT.position) > 10f) && damagingweaponList.Count > 0;
        }
        
        List<Collider> damagingweaponList;
        Collider nearestEnemyMeat;
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
            nearestEnemyMeat = Sensor.GetClosestEnemyColliderInSensorRange();

            if (nearestEnemyMeat != null)
            {
                if (damagingweaponList.Count > 0)
                {
                    if (Vector3.Distance(nearestEnemyMeat.transform.position, _DATA_CENTER.geometryCenter.position) >
                        Vector3.Distance(damagingweaponList[0].transform.position, _DATA_CENTER.geometryCenter.position))
                    {
                        return true;
                    }
                }
            }
            else
            {
                if (damagingweaponList.Count > 0)
                    return Vector3.Distance(damagingweaponList[0].transform.position, _DATA_CENTER.geometryCenter.position) < 5f;
            }
            return false;
        }
        
        public bool TimeToAttack()
        {
            if (Sensor.EnemyAndTeammateBetweenMeAndEnemy() != null)
            {
                return false;
            }
            Collider tar = this.Sensor.GetTargetRangeEnemyCollider(this.triggerAtttackRangeMin,this.triggerAtttackRangeMax);
            if (tar == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool TimeToRespond()
        {
            damagingweaponList = Sensor.GetNearbyDamagingWeaponColliders();
            return damagingweaponList.Count == 0;
        }
        
        public bool TimeToStopRunning()
        {
            return (nearestEnemyMeat != null && Vector3.Distance(nearestEnemyMeat.transform.position, this._DATA_CENTER.WholeT.position) < 5f) || Sensor.GetNearbyDamagingWeaponColliders().Count > 0 || Sensor.GetOutterDamagingWeaponColliders().Count > 0;
        }
        
        public bool CheckTriggerCondition(string conditionFunctionName)
        {
            System.Type T = typeof(Behavior);
            MethodInfo theMethod = T.GetMethod(conditionFunctionName); //激活同名函数
            if (theMethod != null)
            {   
                return (bool)theMethod.Invoke(this, null);
            }
            return false;
        }
        
        public bool CheckExitCondition(string stateKey)
        {
            string exitCondition;
            _AIStateRunner.BehaviourAndStrategicExitCondition.TryGetValue(stateKey,out exitCondition);
            switch(exitCondition)
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