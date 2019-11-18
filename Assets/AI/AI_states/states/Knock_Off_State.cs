using System.Collections.Generic;
using UnityEngine;
using HittingDetection;
using Soul;

public class Knock_Off_State : AI_State
{
    readonly float knock_off_time;
    float time_counter;
    DecompositionerPool superHitPool;
    Vector3 _xz;    
    bool touchedBoundary;
    
    // 原先的MultiplyPoint3x4击飞曲线计划相关
    //Quaternion startquaternion;
    //Matrix4x4 Matrix;
    
    public Knock_Off_State(float knock_off_time)
    {
        this.knock_off_time = knock_off_time;
        StateType = stateType.KnockOff;
    }

    public override void Pre_process_before_enter()
    {
		base.Pre_process_before_enter ();
    }

    public override bool Enter_condition_priority2()
    {
        return false;
    }

    public override bool Force_enter_condition() => _FightAttriCalReference.ReturnDamageList(DamageType.knockOff_damage).Count > 0;

    Decompositioner processingBlood;
    string KnockOffSparkPersonalEffectPath;
    List<AnimationClip> knockoffAnimations;
    V_Damage processingD;
    public override void AI_State_enter()
    {
        base.AI_State_enter();
        time_counter = 0;
        alreadyFinishedZTranslation = 0;
        alreadyFinishedYTranslation = 0;
        touchedBoundary = false;
        _DATA_CENTER.SetUsingGravity(false);
        _FightAttriCalReference.SetGettingDamageState(true);
        _Animator.SetFloat("speed", 0f);
        _Weapon_Animation_Events.ClearMarkerManagers();
        _FightAttriCalReference.EnableAllHitBoxCollider(false);
        personality_Events.CloseAllPersonalityEffects();
        _FightAttriCalReference.plusCriticalGauge(2);
        _Rigidbody.velocity = Vector3.zero;
        AnimationResourceLoader.SeriesAnimationClipsDic.TryGetValue(this._AIStateRunner.characterType + "/basic_knockoffs", out knockoffAnimations);        
        int ranDom = (int)Random.Range(0,knockoffAnimations.Count);
        Animation_Manger.AnimationTrigger(knockoffAnimations[ranDom]);
		if (_FightAttriCalReference.ReturnDamageList(DamageType.knockOff_damage).Count > 0)
        {
            processingD = _FightAttriCalReference.ReturnDamageList(DamageType.knockOff_damage)[0];
            KnockOffSparkPersonalEffectPath = processingD.fromWeapon?.personalEffectPath;
            superHitPool = EffectAndHurtObjectLoading.Instance.IniEffectsPool("super_hit",KnockOffSparkPersonalEffectPath, 3);
            if (superHitPool != null)
            {
                processingBlood = superHitPool.Rent();
                processingBlood.transform.position = processingD.damageHappenPoint;
                processingBlood.transform.rotation = Quaternion.identity;
            }
            _xz = processingD.AttackerT.forward;
            
            _FightAttriCalReference.EatDamage(DamageType.knockOff_damage);
            StartPoint = gameObject.transform.position;
            
            // 原先的MultiplyPoint3x4击飞曲线计划相关
            //startquaternion =  Quaternion.LookRotation(processingD.AttackerT.forward, Vector3.up);
            //Matrix = new Matrix4x4();
            //Matrix = Matrix4x4.TRS(temp, startquaternion, Vector3.one * 1);
        }
        _FightAttriCalReference.ClearDamageLists();               
    }

    public override bool Capacity_exit_condition()
    {
        return (this.time_counter > this.knock_off_time);
    }

    public override void AI_State_exit()
    {
        base.AI_State_exit();
        _DATA_CENTER.SetUsingGravity(true);
        _FightAttriCalReference.SetGettingDamageState(false);
        _FightAttriCalReference.EnableAllHitBoxCollider(true);
        _Rigidbody.velocity = Vector3.zero;
    }

    Vector3 StartPoint;
    float alreadyFinishedZTranslation,alreadyFinishedYTranslation;
    public override void _State_FixedUpdate1()
    {
        time_counter += Time.fixedDeltaTime;
        if (!touchedBoundary)
        {
            if (_DATA_CENTER.onBattleGroundBundary)
            {
                touchedBoundary = true;
                StartPoint = gameObject.transform.position;
                _xz = Vector3.zero - StartPoint;_xz.y = 0;_xz = _xz.normalized;
                alreadyFinishedZTranslation = FightGlobalSetting._knockOffzAnimationCurve.Evaluate(time_counter);
                alreadyFinishedYTranslation = FightGlobalSetting._knockOffyAnimationCurve.Evaluate(time_counter);
            }
        }else{
            touchedBoundary = _DATA_CENTER.onBattleGroundBundary;
        }

        gameObject.transform.position = StartPoint + 
        _xz * (FightGlobalSetting._knockOffzAnimationCurve.Evaluate(time_counter) - alreadyFinishedZTranslation) +  
        Vector3.up * (FightGlobalSetting._knockOffyAnimationCurve.Evaluate(time_counter) - alreadyFinishedYTranslation);
        
        // 原先的MultiplyPoint3x4击飞曲线计划相关
        //if (!touchedBoundary)
        //{
        //    if (_DATA_CENTER.onBattleGroundBundary)
        //    {
        //        touchedBoundary = true;
        //        temp = gameObject.transform.position;
        //        temp.y = 0;
        //        startquaternion = Quaternion.LookRotation(Vector3.zero - temp, Vector3.up);
        //        temp = gameObject.transform.position;
        //        Matrix = Matrix4x4.TRS(temp, startquaternion, Vector3.one * 1);
        //        alreadyFinishedZTranslation = FightGlobalSetting._knockOffzAnimationCurve.Evaluate(time_counter);
        //        alreadyFinishedYTranslation = gameObject.transform.position.y;
        //    }
        //}else{
        //    touchedBoundary = _DATA_CENTER.onBattleGroundBundary;
        //}
        //gameObject.transform.position = Matrix.MultiplyPoint3x4(new Vector3(0, 
        //FightGlobalSetting._knockOffyAnimationCurve.Evaluate( time_counter ) * 1f - alreadyFinishedYTranslation, 
        //FightGlobalSetting._knockOffzAnimationCurve.Evaluate( time_counter ) * 1f - alreadyFinishedZTranslation));
        
        if (!_DATA_CENTER.IsGrounded())
            RotateToVelocityNegative(3f, true);           
    }
}

// Knock_off_state should not be super_canceled to revive state,here is the reason:
// While in knock off state, the character may also be transited into knock off state again,
// during the transition of knock off animation to self,the turn on flag you put there is triggerd,
// The AIStateRunner will recognise it as a permision to enter the revive state,then what you will see
// is that revive state is somehow triggerd immmediately. 1107 -- haku

    //if (BS_Main_Health.returnEventDamageList() != null)
   //     {
            //if (BS_Main_Health.returnEventDamageList().Count > 0)
    //        {
                //if (BS_Main_Health.returnEventDamageList()[0].Position_set.Child == null)
    //            {
                //  BS_Main_Health.returnEventDamageList()[0].Position_set.Child = this.gameObject;
    //            }
                //if (BS_Main_Health.returnEventDamageList()[0].Position_set.Parent == null)
    //            {
                //  BS_Main_Health.returnEventDamageList()[0].Position_set.Parent = this.gameObject;
    //            }
                //BS_Main_Health.returnEventDamageList()[0].getAttackerHealthBody().eventAttackHitApprove(BS_Main_Health.returnEventDamageList()[0]);
                //BS_Main_Health.returnEventDamageList().Clear();
                //BS_Main_Health.returnDamageList(damageType.supper_damage).Clear();
                //BS_Main_Health.returnDamageList(damageType.heavy_damage).Clear();
                //BS_Main_Health.returnDamageList(damageType.light_damage).Clear();
                //BS_Main_Health.returnDamageList(damageType.knockOff_damage).Clear();
        //    }
        //}
        //if (BS_Main_Health.returnApprovedEventAttackAttempts() != null)
   //     {
            //BS_Main_Health.returnApprovedEventAttackAttempts().Clear();
        //}
