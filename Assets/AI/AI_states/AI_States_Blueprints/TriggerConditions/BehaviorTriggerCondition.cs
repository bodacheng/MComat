using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

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
            nearbyenemymeat = Sensor.GetInnerEnemiesColliders();
            farenemymeat = Sensor.GetFarEnemiesColliders();
            midenemymeat = Sensor.GetMidEnemiesColliders();
            damagingweaponList = Sensor.GetOutterDamagingWeaponColliders();
            return nearbyenemymeat.Count == 0 && damagingweaponList.Count > 0 && (farenemymeat.Count > 0 && midenemymeat.Count > 0);
        }
        
        List<Collider> damagingweaponList;
        List<Collider> nearbyenemymeat;
        List<Collider> farenemymeat;
        List<Collider> midenemymeat;
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
        
        public bool TimeToAttack_Close()
        {
            if (Sensor.EnemyAndTeammateBetweenMeAndEnemy() != null)
            {
                return false;
            }
            return CheckToEnemyDisEnterCondition(new BehaviorEnterRange[1]{ BehaviorEnterRange.inner_range });
        }
        
        public bool TimeToAttack_Near()
        {
            if (Sensor.EnemyAndTeammateBetweenMeAndEnemy() != null)
            {
                return false;
            }
            return CheckToEnemyDisEnterCondition(new BehaviorEnterRange[1]{ BehaviorEnterRange.mid_range });
        }
        
        public bool TimeToAttack_Far()
        {
            if (Sensor.EnemyAndTeammateBetweenMeAndEnemy() != null)
            {
                return false;
            }
            return CheckToEnemyDisEnterCondition(new BehaviorEnterRange[1]{ BehaviorEnterRange.far_range });
        }
        
        public bool TimeToAttack_OutterRange()
        {
            if (Sensor.EnemyAndTeammateBetweenMeAndEnemy() != null)
            {
                return false;
            }
            return CheckToEnemyDisEnterCondition(new BehaviorEnterRange[1]{ BehaviorEnterRange.out_of_range });
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