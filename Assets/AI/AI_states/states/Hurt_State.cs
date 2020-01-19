using System.Collections.Generic;
using UnityEngine;
using HittingDetection;
using Soul;

public class Hurt_State : AI_State {
    float used_dizzy_time;
    float time_counter;
    Vector3 fixDesPos;
    readonly Coroutine shaderChangeProcess;
    List<AnimationClip> hurtclips;

    public override void Pre_process_before_enter()
	{
		base.Pre_process_before_enter ();
    }

    bool touchingEnemyBody;
    public override void AI_State_enter(V_Damage newValue)
	{
		base.AI_State_enter();
        _Animator.SetFloat("speed", 0f);
        _Animator.applyRootMotion = false;
        _FightAttriCalReference.SetGettingDamageState(true);
        _Weapon_Animation_Events.ClearMarkerManagers();
        _BO_Ani_E.hiddenMethods.CloseEffectsOnBodyParts(true);
        touchingEnemyBody = _BasicPhysicSupport.hiddenMethods.meTouchingEnemyBody;//这个奇葩设定的逻辑是，如果守击的瞬间我角色贴着敌人的肉，那么攻击给我的推力就包括一个敌人前方的力。没错这个是个简化逻辑，其他敌人摸到我的话我也受到攻击方正前推力。
        hurtclips = AnimationResourceLoader.SeriesAnimationClipsDic[_AIStateRunner.characterType + "/basic_hurts"];
        int ranDom = Random.Range(0, hurtclips.Count);
        Animation_Manger.AnimationTrigger(hurtclips[ranDom]);
        
        _FightAttriCalReference.PlusCriticalGauge(1);
        switch(newValue.damage_type)
        {
                case DamageType.light_damage:
                used_dizzy_time = FightGlobalSetting._lighthit_lastingtime;
                _FightAttriCalReference.GetKnockOffCount().PlusGauge(1f);
                _FightAttriCalReference.GetKnockOffCount().PlusTimeCounter(0.2f);
                break;
                case DamageType.heavy_damage:
                used_dizzy_time = FightGlobalSetting._heavyhit_lastingtime;
                _FightAttriCalReference.GetKnockOffCount().PlusGauge(3f);
                _FightAttriCalReference.GetKnockOffCount().PlusTimeCounter(0.2f);    
                break;
                case DamageType.supper_damage:
                used_dizzy_time = FightGlobalSetting._heavyhit_lastingtime;
                _FightAttriCalReference.GetKnockOffCount().PlusGauge(4f);
                _FightAttriCalReference.GetKnockOffCount().PlusTimeCounter(0.2f);
                break;        
        }
        
        fixDesPos = CalFixPosDestination(newValue.damageHappenPoint,
                                    newValue.AttackerT_foward,
                                        newValue.AttackerT_pos,
                                            gameObject.transform.position,
                                                newValue._WeaponPosAdjustMode);
        _Rigidbody.velocity = fixDesPos - gameObject.transform.position;
        //fixpostween = _Rigidbody.DOMove(fixDesPos,Vector3.Distance(gameObject.transform.position,fixDesPos)/0.25f);// 第二个参数是距离除以期望速度
        if (_FightAttriCalReference.GetKnockOffCount().GetGauge() >= FightGlobalSetting._knockoffextent && newValue.damage_type == DamageType.supper_damage)//&& newValue.damage_type == DamageType.supper_damage
        {
            _FightAttriCalReference.ApplyDamage(new V_Damage(0,
                                                            _FightAttriCalReference, newValue.attacker,
                                                            DamageType.knockOff_damage, newValue._WeaponPosAdjustMode, newValue._weaponMode,SpecialApply.none,
                                                            newValue.damageHappenPoint, newValue.CutRotation,
                                                            newValue.AttackerT_foward,newValue.AttackerT_pos,
                                                            newValue.effectPath,newValue.effectSpreadOnBody));
            _FightAttriCalReference.GetKnockOffCount().SetGauge(0f);
        }
        RotateToDirection(- fixDesPos, 2f, true);
        time_counter = 0f;
        personality_Events.CloseAllPersonalityEffects();
        Animation_Manger.Animator.SetTrigger("face_reset");
        Animation_Manger.Animator.SetTrigger("hurt");
        if (newValue.effectSpreadOnBody)
        {
            _FightAttriCalReference.RunShaderChangeProcess(newValue.effectPath, 0.1f);
        }
    }
    
    public override void _State_FixedUpdate1()
    {
        time_counter += Time.fixedDeltaTime;
        if (time_counter > used_dizzy_time/2)
            _Rigidbody.velocity = Vector3.zero;
    }

	public override bool Naturally_exit_condition()
	{
        return time_counter > used_dizzy_time;
    }

	public override void AI_State_exit()
	{
        base.AI_State_exit();
        _FightAttriCalReference.SetGettingDamageState(false);
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
