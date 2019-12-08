using System.Collections.Generic;
using UnityEngine;
using HittingDetection;
using Soul;
using DG.Tweening;

public class Hurt_State : AI_State {
    float used_dizzy_time;
    float time_counter;
    Vector3 fixDesPos;
    readonly Coroutine shaderChangeProcess;
    List<AnimationClip> hurtclips;
    Tween fixpostween;

    public override void Pre_process_before_enter()
	{
		base.Pre_process_before_enter ();
    }

	public override bool Force_enter_condition()
	{
        return _FightAttriCalReference.ReturnDamageList(DamageType.heavy_damage).Count > 0
            || _FightAttriCalReference.ReturnDamageList(DamageType.light_damage).Count > 0
            || _FightAttriCalReference.ReturnDamageList(DamageType.supper_damage).Count > 0
            || _FightAttriCalReference.ReturnEventDamageList().Count > 0;
    }

    bool touchingEnemyBody;
    public override void AI_State_enter(V_Damage newValue)
	{
		base.AI_State_enter();
        _Animator.SetFloat("speed", 0f);
        _FightAttriCalReference.SetGettingDamageState(true);
        _Weapon_Animation_Events.ClearMarkerManagers();
        _BO_Ani_E.hiddenMethods.CloseEffectsOnBodyParts();

        touchingEnemyBody = _BasicPhysicSupport.hiddenMethods.meTouchingEnemyBody;//这个奇葩设定的逻辑是，如果守击的瞬间我角色贴着敌人的肉，那么攻击给我的推力就包括一个敌人前方的力。没错这个是个简化逻辑，其他敌人摸到我的话我也受到攻击方正前推力。
        hurtclips = AnimationResourceLoader.SeriesAnimationClipsDic[_AIStateRunner.characterType + "/basic_hurts"];
        int ranDom = Random.Range(0, hurtclips.Count);
        Animation_Manger.AnimationTrigger(hurtclips[ranDom]);
        
        _FightAttriCalReference.plusCriticalGauge(1);
        switch(newValue.damage_type)
        {
                case DamageType.light_damage:
                used_dizzy_time = FightGlobalSetting._lighthit_lastingtime;
                _FightAttriCalReference.GetKnockOffCount().plusGauge(1f);
                _FightAttriCalReference.GetKnockOffCount().plusTimeCounter(0.2f);
                _FightAttriCalReference.EatDamage(DamageType.light_damage);
                break;
                case DamageType.heavy_damage:
                used_dizzy_time = FightGlobalSetting._heavyhit_lastingtime;
                _FightAttriCalReference.GetKnockOffCount().plusGauge(3f);
                _FightAttriCalReference.GetKnockOffCount().plusTimeCounter(0.2f); 
                _FightAttriCalReference.EatDamage(DamageType.heavy_damage);    
                break;
                case DamageType.supper_damage:
                used_dizzy_time = FightGlobalSetting._heavyhit_lastingtime;
                _FightAttriCalReference.GetKnockOffCount().plusGauge(4f);
                _FightAttriCalReference.GetKnockOffCount().plusTimeCounter(0.2f);
                _FightAttriCalReference.EatDamage(DamageType.supper_damage);
                break;        
        }
        
        fixDesPos = CalFixPosDestination(newValue.damageHappenPoint,
                                    newValue.AttackerT_foward,
                                        newValue.AttackerT_pos,
                                            gameObject.transform.position,
                                                newValue._WeaponPosAdjustMode,
                                                    touchingEnemyBody);
        fixpostween = _Rigidbody.DOMove(fixDesPos,1f);
        if (_FightAttriCalReference.GetKnockOffCount().getGauge() >= FightGlobalSetting._knockoffextent)//&& newValue.damage_type == DamageType.supper_damage
        {
            _FightAttriCalReference.ApplyDamage(new V_Damage(DamageType.knockOff_damage,
                                                                newValue._WeaponPosAdjustMode,   
                                                                    newValue.damageHappenPoint, 
                                                                        newValue.AttackerT_foward,
                                                                            newValue.AttackerT_pos, 
                                                                                newValue.fromWeapon));
            _FightAttriCalReference.GetKnockOffCount().setGauge(0f);
        }
        RotateToDirection(- fixDesPos, 2f, true);
        time_counter = 0f;
        personality_Events.CloseAllPersonalityEffects();
        Animation_Manger.Animator.SetTrigger("face_reset");
        Animation_Manger.Animator.SetTrigger("hurt");
    }
    
    public override void _State_FixedUpdate1()
    {
        time_counter += Time.fixedDeltaTime;
    }

	public override bool Naturally_exit_condition()
	{
        return time_counter > used_dizzy_time;
    }

	public override void AI_State_exit()
	{
        base.AI_State_exit();
        _FightAttriCalReference.SetGettingDamageState(false);
        time_counter = 0f;
        if (fixpostween != null)
        {
            fixpostween.Kill(false);
        }
        _Rigidbody.velocity = Vector3.zero;
        if (_AIStateRunner.GetTryState().StateType == stateType.AC || _AIStateRunner.GetTryState().StateType == stateType.GI ||
            _AIStateRunner.GetTryState().StateType == stateType.GM || _AIStateRunner.GetTryState().StateType == stateType.GR)
        {
            _FightAttriCalReference.BeHitCountInterrupt();
        }
    }
}

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
//BO_Health attackerHealth = BS_Main_Health.returnEventDamageList()[0].getAttackerHealthBody();
//            if (attackerHealth != null)
//            {
//  attackerHealth.eventAttackHitApprove(BS_Main_Health.returnEventDamageList()[0]);
//            }

//BS_Main_Health.returnDamageList(damageType.heavy_damage).Clear();
//BS_Main_Health.returnDamageList(damageType.light_damage).Clear();
//BS_Main_Health.returnDamageList(damageType.supper_damage).Clear();
//BS_Main_Health.returnDamageList(damageType.knockOff_damage).Clear();
//            this.time_counter = 0f;
//BS_Main_Health.returnEventDamageList().Clear();
//    }
//}
