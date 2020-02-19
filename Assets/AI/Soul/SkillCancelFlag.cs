using UnityEngine;

public class SkillCancelFlag : MonoBehaviour {

    public class HiddenMethods
    {
        readonly SkillCancelFlag skillCancelFlag;
        public HiddenMethods(SkillCancelFlag SkillCancelFlag)
        {
            skillCancelFlag = SkillCancelFlag;
        }
        
        public void SkillCancelFlagFixedUpdate()
        {
            if (skillCancelFlag.attackApproaching)
                skillCancelFlag.AttackApproachLoopCounter += Time.fixedDeltaTime;
            
            if (skillCancelFlag.attackApproaching)
            {
                if (skillCancelFlag.AttackApproachLoopCounter > 0.08f)
                    skillCancelFlag.attackApproaching = false;
            }
        }
        
        public void SetAttackApproachingFlag(bool startorend)
        {
            skillCancelFlag.attackApproaching = startorend;
        }
        public bool GetAttackApproachingFlag()
        {
            return skillCancelFlag.attackApproaching;
        }
    }

    public Data_Center _C;
    public HiddenMethods hiddenMethods;
    public bool Cancel_Flag;
    bool attackApproaching;
    float AttackApproachLoopCounter;

    void Awake()
    {
        hiddenMethods = new HiddenMethods(this);
    }
    
    public void ThisIsEndOfAnimation(AnimationEvent e)
    {
        // if (!_C.Animation_Manger.getOnAniTransitionFlag())//可能报错 //e.stringParameter == _C.Animation_Manger.currentAnimation.name
        // 这是目前这个系统最最最最头疼的一个环节了。。。下面这些是无奈之举.我们没能理解为什么上面那种情况也会出现bug
        // animationcounter那块就是说，，如果真是两个连续相同状态，如果迁移状态太短了代表那啥，肯定是迁移区间里ThisIsEndOfAnimation被激活了
        // 目前底下这套逻辑决定了，整个游戏不能同样的技能动画迁移至同样的技能动画。
        // 这样的话某一个横行就一个技能，以及闪避技能的自身迁移，都需要注意了。。。可能都要重新设计
        //if (_C.Animation_Manger._toUse == null ||  _C.Animation_Manger._toUse == e.animatorClipInfo.clip)
        //{
        //    _C.Animation_Manger.SetAnimationPlayingStep(AnimationPlaying_Step.over);
        //}
    }

    public void turn_on_flag()
    {
        _C.Sensor.ContinuousDetectionStart(-1);
        _C.bO_Weapon_Animation_Events.ClearMarkerManagers();//????????
        Cancel_Flag = true;
    }

    public void turn_off_flag()
    {
        _C.bO_Weapon_Animation_Events.ClearMarkerManagers();
        Cancel_Flag = false;
    }

    public void TurnRotationAdjustmentStartFlagWithoutstepfoward(int i = 1)
    {
        if (i == 1)
        {
            _C.Sensor.GetEnemiesByDistance(true);
            if (_C.Sensor.GetEnemiesByDistance(false).Count > 0)
            {
                if (_C.Sensor.GetEnemiesByDistance(false)[0] != null)
                {
                    _C._MyBehaviorRunner.GetNowState().RotateToTarget_Tween(_C.Sensor.GetEnemiesByDistance(false)[0].transform.position, 0.01f, true);
                }
            }
            AttackApproachLoopCounter = 0f;
            attackApproaching = false;//与校准方向一起 开始校准迈步
        }
        _C._BasicPhysicSupport.hiddenMethods.ClearHitCountForAttackStepping();//清理移动用攻击统计，从这个时候开始，一旦击中了敌人，脚步停止
    }

    public void TurnRotationAdjustmentStartFlag(int i = 1)
    {
        if (i == 1)
        {
            _C.Sensor.GetEnemiesByDistance(true);
            if (_C.Sensor.GetEnemiesByDistance(false).Count > 0)
            {
                if (_C.Sensor.GetEnemiesByDistance(false)[0] != null)
                {
                    _C._MyBehaviorRunner.GetNowState().RotateToTarget_Tween(_C.Sensor.GetEnemiesByDistance(false)[0].transform.position, 0.01f, true);
                }
            }
            AttackApproachLoopCounter = 0f;
            attackApproaching = true;//与校准方向一起 开始校准迈步
        }
        _C._BasicPhysicSupport.hiddenMethods.ClearHitCountForAttackStepping();//清理移动用攻击统计，从这个时候开始，一旦击中了敌人，脚步停止
    }
}
