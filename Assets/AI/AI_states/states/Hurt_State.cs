using System.Collections.Generic;
using UnityEngine;
using HittingDetection;
using Soul;

public class Hurt_State : AI_State {
    private float used_dizzy_time;
    private float time_counter;
    private Vector3 fixDesPos;
	private readonly Coroutine shaderChangeProcess;
    private List<AnimationClip> hurtclips;

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

    V_Damage processingD;
    public override void AI_State_enter()
	{
		base.AI_State_enter();
        this._Rigidbody.mass = 50;
        this._Animator.SetFloat("speed", 0f);
        this._FightAttriCalReference.SetGettingDamageState(true);
        this._Weapon_Animation_Events.ClearMarkerManagers();
        this._BO_Ani_E.hiddenMethods.CloseEffectsOnBodyParts();
        this.hurtclips = AnimationResourceLoader.SeriesAnimationClipsDic[_AIStateRunner.characterType + "/basic_hurts"];
        
        int ranDom = (int)Random.Range(0,hurtclips.Count);
        Animation_Manger.AnimationTrigger(hurtclips[ranDom]);
		if (_FightAttriCalReference.ReturnDamageList(DamageType.heavy_damage).Count > 0)
		{
            processingD = _FightAttriCalReference.ReturnDamageList(DamageType.heavy_damage)[0];
            used_dizzy_time = FightGlobalSetting._heavyhit_lastingtime;
            fixDesPos = CalFixPosDestination(processingD.damageHappenPoint,processingD.AttackerT,this.gameObject.transform,processingD._WeaponPosAdjustMode);
            this._Rigidbody.velocity = fixDesPos - gameObject.transform.position;
            _FightAttriCalReference.GetKnockOffCount().plusGauge(3f);
			_FightAttriCalReference.GetKnockOffCount().plusTimeCounter(0.2f);            
            _FightAttriCalReference.EatDamage(DamageType.heavy_damage);
            _FightAttriCalReference.plusCriticalGauge(1);
		}

		if (_FightAttriCalReference.ReturnDamageList(DamageType.light_damage).Count > 0) 
        {
            processingD = _FightAttriCalReference.ReturnDamageList(DamageType.light_damage)[0];
            used_dizzy_time = FightGlobalSetting._lighthit_lastingtime;
            fixDesPos = CalFixPosDestination(processingD.damageHappenPoint,processingD.AttackerT,this.gameObject.transform,processingD._WeaponPosAdjustMode);
            this._Rigidbody.velocity = fixDesPos - gameObject.transform.position;
            _FightAttriCalReference.plusCriticalGauge(1);
			_FightAttriCalReference.GetKnockOffCount().plusGauge(1f);
            _FightAttriCalReference.GetKnockOffCount().plusTimeCounter(0.2f);
            _FightAttriCalReference.EatDamage(DamageType.light_damage);
			//为了让轻攻击带来的连击能保证持续，不在轻攻击处进行击飞积累
        }

		if (_FightAttriCalReference.ReturnDamageList(DamageType.supper_damage).Count > 0)
        {
            processingD = _FightAttriCalReference.ReturnDamageList(DamageType.supper_damage)[0];
            used_dizzy_time = FightGlobalSetting._heavyhit_lastingtime;
            fixDesPos = CalFixPosDestination(processingD.damageHappenPoint,processingD.AttackerT,this.gameObject.transform,processingD._WeaponPosAdjustMode);
            this._Rigidbody.velocity = fixDesPos - gameObject.transform.position;
            _FightAttriCalReference.plusCriticalGauge(1);
			_FightAttriCalReference.GetKnockOffCount().plusGauge(4f);
            _FightAttriCalReference.GetKnockOffCount().plusTimeCounter(0.2f);
            _FightAttriCalReference.EatDamage(DamageType.supper_damage);
        }
        if (_FightAttriCalReference.GetKnockOffCount().getGauge() >= 5f)
        {
            _FightAttriCalReference.ApplyDamage(new V_Damage(DamageType.knockOff_damage,processingD._WeaponPosAdjustMode,processingD.damageHappenPoint, processingD.AttackerT, processingD.fromWeapon));
            _FightAttriCalReference.GetKnockOffCount().setGauge(0f);
        }
        this.RotateToDirection(-fixDesPos,0.5f,true);
        this.time_counter = 0f;
        this.personality_Events.CloseAllPersonalityEffects();
        this.Animation_Manger.Animator.SetTrigger("face_reset");
        this.Animation_Manger.Animator.SetTrigger("hurt");
    }
    
    public override void _State_FixedUpdate1()
    {
        this.time_counter += Time.fixedDeltaTime;
    }

	public override bool Capacity_exit_condition()
	{
        return this.time_counter > used_dizzy_time;
    }

	public override void AI_State_exit()
	{
        base.AI_State_exit();
        this._Rigidbody.mass = 100;
        _FightAttriCalReference.SetGettingDamageState(false);
        time_counter = 0f;
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
