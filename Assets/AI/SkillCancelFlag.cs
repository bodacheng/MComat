using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCancelFlag : MonoBehaviour {

    public Data_Center _C;
    
    private bool attackApproaching = false;
    private bool rotationAdjustmentStartFlag = true;
    private float rotateLoopCounter = 0f;
    private bool Cancel_Flag = false;

    public void ThisIsEndOfAnimation(AnimationEvent e)
    {
        //if (!_C.Animation_Manger.getOnAniTransitionFlag())//可能报错 //e.stringParameter == _C.Animation_Manger.currentAnimation.name
        // 这是目前这个系统最最最最头疼的一个环节了。。。下面这些是无奈之举.我们没能理解为什么上面那种情况也会出现bug
        // animationcounter那块就是说，，如果真是两个连续相同状态，如果迁移状态太短了代表那啥，肯定是迁移区间里ThisIsEndOfAnimation被激活了
        if (_C.Animation_Manger._toUse == null || (_C.Animation_Manger._toUse.name == e.stringParameter && _C.Animation_Manger.animationcounter > 0.08f))
        {
            _C.Animation_Manger.setAnimationPlayingStep(AnimationPlaying_Step.over);
        }
    }

    public void turn_on_flag()
    {
        _C.Sensor.continuousDetectionStart(-1);
        _C.bO_Weapon_Animation_Events.clearMarkerManagers();//????????
        this.Cancel_Flag = true;
    }

    public void turn_off_flag()
    {
        _C.bO_Weapon_Animation_Events.clearMarkerManagers();
        this.Cancel_Flag = false;
    }

    public bool getFlag()
    {
        return this.Cancel_Flag;
    }

    public void SkillCancelFlagFixedUpdate()
    {
        if (this.rotationAdjustmentStartFlag || this.attackApproaching)
            rotateLoopCounter += Time.fixedDeltaTime;
        
        if (rotationAdjustmentStartFlag)
        {
            if (rotateLoopCounter > 0.1f)
            {
                this.rotationAdjustmentStartFlag = false;
            }
        }
        if (this.attackApproaching)
        {
            if (rotateLoopCounter > 0.08f)
                this.attackApproaching = false;
        }
    }

    public void turnRotationAdjustmentStartFlagWithoutstepfoward(int i = 1)
    {
        if (i == 1)
        {
            _C.Sensor.OneRoundDetectionStart(10);
            this.rotateLoopCounter = 0f;
            this.rotationAdjustmentStartFlag = true;
            this.attackApproaching = false;//与校准方向一起 开始校准迈步
        }
        else if (i == 0)
        {
            this.rotationAdjustmentStartFlag = false;
        }
        _C.pusher.clearHitCountForAttackStepping();//清理移动用攻击统计，从这个时候开始，一旦击中了敌人，脚步停止
    }

    public void turnRotationAdjustmentStartFlag(int i = 1)
    {
        if (i == 1)
        {
            this._C.Sensor.OneRoundDetectionStart(10);
            this.rotateLoopCounter = 0f;
            this.rotationAdjustmentStartFlag = true;
            this.attackApproaching = true;//与校准方向一起 开始校准迈步
        }
        else if (i == 0)
        {
            this.rotationAdjustmentStartFlag = false;
        }
        _C.pusher.clearHitCountForAttackStepping();//清理移动用攻击统计，从这个时候开始，一旦击中了敌人，脚步停止
    }
    
    public bool getRotationAdjustmentStartFlag()
    {
        return rotationAdjustmentStartFlag;
    }

    public void setAttackApproachingFlag(bool startorend)
    {
        this.attackApproaching = startorend;
    }
    public bool getAttackApproachingFlag()
    {
        return this.attackApproaching;
    }
}
