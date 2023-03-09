using System.Collections.Generic;
using UnityEngine;

namespace Soul
{
    public class Idle_State : Behavior
    {
        readonly string _clipName;
        private bool showVictoryMotion;
        private bool motionReset = false;

        private readonly List<string> hasVictoryAnimUnit = new List<string>()
        {
            "1","2","3","4","5","6","7"
        };
        
        public Idle_State(string clipName)
        {
            this._clipName = clipName;
        }
        
        public override void Pre_process_before_enter()
        {
            base.Pre_process_before_enter();
        }
        
        public override void AI_State_enter()
        {
            base.AI_State_enter();
            var r_id = this._DATA_CENTER.UnitInfo.r_id;
            showVictoryMotion = hasVictoryAnimUnit.Contains(r_id);
            motionReset = false;
            this._Animator.SetFloat("speed", 0f);
            if (showVictoryMotion)
                Animation_Manger.AnimationTrigger(_clipName, true, 0.1f);
            this._Rigidbody.velocity = Vector3.zero;
            this._Rigidbody.drag = FightGlobalSetting.OnTouchEnemyBodyRigidDrag;
            if (Sensor.GetEnemiesByDistance(false).Count > 0)
            {
                if (Sensor.GetEnemiesByDistance(false)[0] != null)
                {
                    RotateToTarget_Tween(Sensor.GetEnemiesByDistance(false)[0].transform.position, 0.01f);
                }
            }
        }

        public override bool Capacity_Exit_Condition()
        {
            return false;
        }
        
        public override void _State_Update()
        {
            if (showVictoryMotion && !motionReset && _clipName == "victory" && !Animation_Manger._toUse.isLooping && AnimationCasualFinishedFlag())
            {
                Animation_Manger.PlayLayerAnim(null, true, 0.05f);
                motionReset = true;
            }
        }
        
        public override void AI_State_exit()
        {
            base.AI_State_exit();
            this._Rigidbody.drag = 0;
        }
    }
}