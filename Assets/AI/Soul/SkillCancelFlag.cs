using UnityEngine;

public class SkillCancelFlag : MonoBehaviour {

    public class HiddenMethods
    {
        readonly SkillCancelFlag skillCancelFlag;
        public HiddenMethods(SkillCancelFlag SkillCancelFlag)
        {
            this.skillCancelFlag = SkillCancelFlag;
        }
        
        public void SkillCancelFlagFixedUpdate()
        {
            if (skillCancelFlag.rotationAdjustmentStartFlag || skillCancelFlag.attackApproaching)
                skillCancelFlag.rotateLoopCounter += Time.fixedDeltaTime;
            
            if (skillCancelFlag.rotationAdjustmentStartFlag)
            {
                if (skillCancelFlag.rotateLoopCounter > 0.1f)
                {
                    skillCancelFlag.rotationAdjustmentStartFlag = false;
                }
            }
            if (skillCancelFlag.attackApproaching)
            {
                if (skillCancelFlag.rotateLoopCounter > 0.08f)
                    skillCancelFlag.attackApproaching = false;
            }
        }
        
        public bool GetRotationAdjustmentStartFlag()
        {
            return skillCancelFlag.rotationAdjustmentStartFlag;
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
    bool rotationAdjustmentStartFlag = true;
    float rotateLoopCounter;

    void Awake()
    {
        hiddenMethods = new HiddenMethods(this);
    }
    
    public void ThisIsEndOfAnimation(AnimationEvent e)
    {
        // if (!_C.Animation_Manger.getOnAniTransitionFlag())//可能报错 //e.stringParameter == _C.Animation_Manger.currentAnimation.name
        // 这是目前这个系统最最最最头疼的一个环节了。。。下面这些是无奈之举.我们没能理解为什么上面那种情况也会出现bug
        // animationcounter那块就是说，，如果真是两个连续相同状态，如果迁移状态太短了代表那啥，肯定是迁移区间里ThisIsEndOfAnimation被激活了
        if (_C.Animation_Manger._toUse == null || (_C.Animation_Manger._toUse.name == e.stringParameter && _C.Animation_Manger.animationcounter > 0.08f))
        {
            _C.Animation_Manger.SetAnimationPlayingStep(AnimationPlaying_Step.over);
        }
    }

    public void turn_on_flag()
    {
        _C.Sensor.continuousDetectionStart(-1);
        _C.bO_Weapon_Animation_Events.ClearMarkerManagers();//????????
        this.Cancel_Flag = true;
    }

    public void turn_off_flag()
    {
        _C.bO_Weapon_Animation_Events.ClearMarkerManagers();
        this.Cancel_Flag = false;
    }

    public void TurnRotationAdjustmentStartFlagWithoutstepfoward(int i = 1)
    {
        if (i == 1)
        {
            _C.Sensor.OneRoundDetectionStart(10);
            rotateLoopCounter = 0f;
            rotationAdjustmentStartFlag = true;
            attackApproaching = false;//与校准方向一起 开始校准迈步
        }
        else rotationAdjustmentStartFlag &= i != 0;
        _C.pusher.hiddenMethods.ClearHitCountForAttackStepping();//清理移动用攻击统计，从这个时候开始，一旦击中了敌人，脚步停止
    }

    public void TurnRotationAdjustmentStartFlag(int i = 1)
    {
        if (i == 1)
        {
            this._C.Sensor.OneRoundDetectionStart(10);
            this.rotateLoopCounter = 0f;
            this.rotationAdjustmentStartFlag = true;
            this.attackApproaching = true;//与校准方向一起 开始校准迈步
        }
        else this.rotationAdjustmentStartFlag &= i != 0;
        _C.pusher.hiddenMethods.ClearHitCountForAttackStepping();//清理移动用攻击统计，从这个时候开始，一旦击中了敌人，脚步停止
    }
}
