using UnityEngine;
using HittingDetection;
using Soul;
using DG.Tweening;
using UniRx;

public class Hurt_State : Behavior {
    float used_dizzy_time;
    Vector3 fixDesPos;
    float TimeCounter { set; get; }

    public override void Pre_process_before_enter()
	{
		base.Pre_process_before_enter ();
    }
    
    public override void AI_State_enter(V_Damage newValue)
	{
        base.AI_State_enter();
        _Animator.applyRootMotion = false;
        _FightAttriCalReference.SetGettingDamageState(true);
        _Weapon_Animation_Events.ClearMarkerManagers();
        _BO_Ani_E.hiddenMethods.CloseEffectsOnBodyParts(true);
        Animation_Manger.AnimationTrigger(Animation_Manger.GetRandomHurtAnim(),true,0.05f);      
        _FightAttriCalReference.PlusCriticalGauge(1);
        TimeCounter = 0f;
        
        switch (newValue.from_weapon.damage_type)
        {
            case DamageType.slight_damage_forward:
                used_dizzy_time = FightGlobalSetting._heavyhit_lastingtime;
                var disposable1 = new SingleAssignmentDisposable();
                disposable1.Disposable = Observable.EveryUpdate().Subscribe(_ =>
                    {
                        if (TimeCounter > 2 * used_dizzy_time / 3)
                        {
                            _Rigidbody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
                            disposable1.Dispose();
                        }
                    }
                );
                fixDesPos = CalFixPosDestination(newValue.damageHappenPoint, newValue.attacker._Center.WholeT.forward, newValue.attacker._Center.WholeT.position, gameObject.transform.position, newValue.from_weapon.damage_type);
                _Rigidbody.velocity = fixDesPos - gameObject.transform.position;
                _FightAttriCalReference.GetKnockOffCount().PlusGauge(3f);
                _FightAttriCalReference.GetKnockOffCount().PlusTimeCounter(0.2f);
            break;
            case DamageType.light_damage_forward:
                used_dizzy_time = FightGlobalSetting._lighthit_lastingtime;
                var disposable2 = new SingleAssignmentDisposable();
                disposable2.Disposable = Observable.EveryUpdate().Subscribe(_ =>
                    {
                        if (TimeCounter > 2 * used_dizzy_time / 3)
                        {
                            _Rigidbody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
                            disposable2.Dispose();
                        }
                    }
                );
                fixDesPos = CalFixPosDestination(newValue.damageHappenPoint, newValue.attacker._Center.WholeT.forward, newValue.attacker._Center.WholeT.position, gameObject.transform.position,newValue.from_weapon.damage_type);
                _Rigidbody.velocity = fixDesPos - gameObject.transform.position;
                _FightAttriCalReference.GetKnockOffCount().PlusGauge(1f);
                _FightAttriCalReference.GetKnockOffCount().PlusTimeCounter(0.2f);
            break;
            case DamageType.heavy_damage_forward:
                used_dizzy_time = FightGlobalSetting._heavyhit_lastingtime;
                var disposable3 = new SingleAssignmentDisposable();
                disposable3.Disposable = Observable.EveryUpdate().Subscribe(_ =>
                    {
                        if (TimeCounter > 2 * used_dizzy_time / 3)
                        {
                            _Rigidbody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
                            disposable3.Dispose();
                        }
                    }
                );
                fixDesPos = CalFixPosDestination(newValue.damageHappenPoint, newValue.attacker._Center.WholeT.forward, newValue.attacker._Center.WholeT.position, gameObject.transform.position, newValue.from_weapon.damage_type);
                _Rigidbody.velocity = fixDesPos - gameObject.transform.position;
                _FightAttriCalReference.GetKnockOffCount().PlusGauge(3f);
                _FightAttriCalReference.GetKnockOffCount().PlusTimeCounter(0.2f);
            break;
            case DamageType.supper_damage_forward:
                used_dizzy_time = FightGlobalSetting._heavyhit_lastingtime;
                var disposable4 = new SingleAssignmentDisposable();
                disposable4.Disposable = Observable.EveryUpdate().Subscribe(_ =>
                    {
                        if (TimeCounter > 2 * used_dizzy_time / 3)
                        {
                            _Rigidbody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
                            disposable4.Dispose();
                        }
                    }
                );
                fixDesPos = CalFixPosDestination(newValue.damageHappenPoint, newValue.attacker._Center.WholeT.forward, newValue.attacker._Center.WholeT.position, gameObject.transform.position, newValue.from_weapon.damage_type);
                _Rigidbody.velocity = fixDesPos - gameObject.transform.position;
                _FightAttriCalReference.GetKnockOffCount().PlusGauge(6f);
                _FightAttriCalReference.GetKnockOffCount().PlusTimeCounter(0.2f);
            break;
            case DamageType.draw:
                used_dizzy_time = FightGlobalSetting._heavyhit_lastingtime;
                Vector3 vector3 = newValue.from_weapon_marker.transform.position;
                vector3.y = gameObject.transform.position.y;
                gameObject.transform.DOMove(vector3,0.1f).
                OnComplete(() =>{_Rigidbody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;});
                _FightAttriCalReference.GetKnockOffCount().PlusGauge(3f);
                _FightAttriCalReference.GetKnockOffCount().PlusTimeCounter(0.2f);
            break;
            case DamageType.explosion:
                used_dizzy_time = FightGlobalSetting._heavyhit_lastingtime;
                var disposable5 = new SingleAssignmentDisposable();
                disposable5.Disposable = Observable.EveryUpdate().Subscribe(_ =>
                    {
                        if (TimeCounter > 2 * used_dizzy_time / 3)
                        {
                            _Rigidbody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
                            disposable5.Dispose();
                        }
                    }
                );
                fixDesPos = CalFixPosDestination(newValue.damageHappenPoint, newValue.attacker._Center.WholeT.forward, newValue.attacker._Center.WholeT.position, gameObject.transform.position, newValue.from_weapon.damage_type);
                _Rigidbody.velocity = fixDesPos - gameObject.transform.position;
                _FightAttriCalReference.GetKnockOffCount().PlusGauge(3f);
                _FightAttriCalReference.GetKnockOffCount().PlusTimeCounter(0.2f);
            break;
            case DamageType.push_to_mid:
                used_dizzy_time = FightGlobalSetting._heavyhit_lastingtime;
                Vector3 MidDistanceFromMe = newValue.attacker._Center.WholeT.transform.position + newValue.attacker._Center.WholeT.transform.forward * 10f;
                MidDistanceFromMe.y = 0;
                var disposable6 = new SingleAssignmentDisposable();
                disposable6.Disposable = Observable.EveryUpdate().Subscribe(_ =>
                    {
                        if (Vector3.Distance(MidDistanceFromMe,gameObject.transform.position) < 0.5f || _BasicPhysicSupport.hiddenMethods.onBattleGroundBundary)
                        {
                            _Rigidbody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
                            disposable6.Dispose();
                        }
                    }
                );
                gameObject.transform.DOMove(MidDistanceFromMe,0.3f).OnComplete(() =>{_Rigidbody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;});
                _FightAttriCalReference.GetKnockOffCount().PlusGauge(3f);
                _FightAttriCalReference.GetKnockOffCount().PlusTimeCounter(0.2f);
            break;
        }
        
        if (newValue.from_weapon.effectSpreadOnBody)
        {
            _FightAttriCalReference.RunShaderChangeProcess(FightGlobalSetting.EffectPathDefine(newValue.from_weapon.zokusei), 0.1f);
        }

        if (_FightAttriCalReference.GetKnockOffCount().GetGauge() >= FightGlobalSetting._knockoffextent)
        {
            _FightAttriCalReference.GetKnockOffCount().SetGauge(0f);
            _AIStateRunner.ChangeState("KnockOff", newValue);
            return;
        }
        RotateToTarget_Tween(newValue.damageHappenPoint, 0.1f, true);
        personality_Events.CloseAllPersonalityEffects();
        Animation_Manger.Animator.SetTrigger("face_reset");
        Animation_Manger.Animator.SetTrigger("hurt");
    }
        
    public override void _State_FixedUpdate1()
    {
        TimeCounter += Time.fixedDeltaTime;
    }
    
	public override bool Capacity_Exit_Condition()
	{
        return TimeCounter > used_dizzy_time;
    }
    
	public override void AI_State_exit()
	{
        base.AI_State_exit();
        _Rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        _FightAttriCalReference.SetGettingDamageState(false);
    }
}
