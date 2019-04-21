using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//这其中的唯一一个变量Cancel_Flag也归根结底是用于状态机参考。如果在客服务端不准备运行状态机那这个量也无所谓，不需要同步
public class SkillCancelFlag : MonoBehaviour {
    BO_Weapon_Animation_Events BO_Weapon_Animation_Events;
    Sensor _Sensor;
    BO_Health BS_Main_Health;

    private bool attackApproaching = false;
    private bool rotationAdjustmentStartFlag = true;
    private float rotateLoopCounter = 0f;

    bool Cancel_Flag = false;

    public void turn_on_flag()
    {
        if (_Sensor)
            _Sensor.continuousDetectionStart(-1);

        BO_Weapon_Animation_Events.clearMarkerManagers();//????????
        this.Cancel_Flag = true;
    }

    public void turn_off_flag()
    {
        BO_Weapon_Animation_Events.clearMarkerManagers();
        this.Cancel_Flag = false;
    }

    public bool getFlag()
    {
        return this.Cancel_Flag;
    }

    void Update()
    {
        if (this.rotationAdjustmentStartFlag || this.attackApproaching)
            rotateLoopCounter += Time.deltaTime;
        
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
            if (_Sensor != null)
            {
                _Sensor.OneRoundDetectionStart(10);
            }
            this.rotateLoopCounter = 0f;
            this.rotationAdjustmentStartFlag = true;
            this.attackApproaching = false;//与校准方向一起 开始校准迈步
        }
        else if (i == 0)
        {
            this.rotationAdjustmentStartFlag = false;
        }
        this.BS_Main_Health.clearHitCountForAttackStepping();//清理移动用攻击统计，从这个时候开始，一旦击中了敌人，脚步停止
    }

    public void turnRotationAdjustmentStartFlag(int i = 1)
    {
        if (i == 1)
        {
            if (_Sensor != null)
            {
                _Sensor.OneRoundDetectionStart(10);
            }
            this.rotateLoopCounter = 0f;
            this.rotationAdjustmentStartFlag = true;
            this.attackApproaching = true;//与校准方向一起 开始校准迈步
        }
        else if (i == 0)
        {
            this.rotationAdjustmentStartFlag = false;
        }
        this.BS_Main_Health.clearHitCountForAttackStepping();//清理移动用攻击统计，从这个时候开始，一旦击中了敌人，脚步停止
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

    public void setSensor(Sensor _Sensor)
    {
        this._Sensor = _Sensor;
    }

    // Use this for initialization
    void Awake() {
        BO_Weapon_Animation_Events = gameObject.GetComponent<BO_Weapon_Animation_Events>();
        BS_Main_Health = gameObject.GetComponent<BO_Health>();
        Cancel_Flag = false;
    }
}
