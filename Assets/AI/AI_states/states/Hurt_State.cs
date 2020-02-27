using UnityEngine;
using HittingDetection;
using Soul;
using DG.Tweening;

public class Hurt_State : Behavior {
    float used_dizzy_time;
    float time_counter;
    Vector3 fixDesPos;
    bool freezed;
    float TimeCounter
    {
        set {
            time_counter = value;
            if (!freezed)
            {
                if (time_counter > 2 * used_dizzy_time / 3)
                {
                    _Rigidbody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
                    freezed = true;
                }
            }
        }
        get {
            return time_counter;
        }
    }
    
    public override void Pre_process_before_enter()
	{
		base.Pre_process_before_enter ();
    }

    bool touchingEnemyBody;
    public override void AI_State_enter(V_Damage newValue)
	{
		base.AI_State_enter();
        _Animator.applyRootMotion = false;
        _FightAttriCalReference.SetGettingDamageState(true);
        _Weapon_Animation_Events.ClearMarkerManagers();
        _BO_Ani_E.hiddenMethods.CloseEffectsOnBodyParts(true);
        touchingEnemyBody = _BasicPhysicSupport.hiddenMethods.meTouchingEnemyBody;//这个奇葩设定的逻辑是，如果守击的瞬间我角色贴着敌人的肉，那么攻击给我的推力就包括一个敌人前方的力。没错这个是个简化逻辑，其他敌人摸到我的话我也受到攻击方正前推力。
        Animation_Manger.AnimationTrigger(Animation_Manger.GetRandomHurtAnim(),true,0.05f);      
        _FightAttriCalReference.PlusCriticalGauge(1);
        switch (newValue.from_weapon.damage_type)
        {
            case DamageType.light_damage_forward:
                freezed = false;
                used_dizzy_time = FightGlobalSetting._lighthit_lastingtime;
                fixDesPos = CalFixPosDestination(newValue.damageHappenPoint, newValue.attacker._Center.WholeT.forward, newValue.attacker._Center.WholeT.position, gameObject.transform.position,newValue.from_weapon.damage_type);
                _Rigidbody.velocity = fixDesPos - gameObject.transform.position;
                _FightAttriCalReference.GetKnockOffCount().PlusGauge(1f);
                _FightAttriCalReference.GetKnockOffCount().PlusTimeCounter(0.2f);
                break;
            case DamageType.heavy_damage_forward:
                freezed = false;
                used_dizzy_time = FightGlobalSetting._heavyhit_lastingtime;
                fixDesPos = CalFixPosDestination(newValue.damageHappenPoint, newValue.attacker._Center.WholeT.forward, newValue.attacker._Center.WholeT.position, gameObject.transform.position, newValue.from_weapon.damage_type);
                _Rigidbody.velocity = fixDesPos - gameObject.transform.position;
                _FightAttriCalReference.GetKnockOffCount().PlusGauge(3f);
                _FightAttriCalReference.GetKnockOffCount().PlusTimeCounter(0.2f);
                break;
            case DamageType.supper_damage_forward:
                freezed = false;
                used_dizzy_time = FightGlobalSetting._heavyhit_lastingtime;
                fixDesPos = CalFixPosDestination(newValue.damageHappenPoint, newValue.attacker._Center.WholeT.forward, newValue.attacker._Center.WholeT.position, gameObject.transform.position, newValue.from_weapon.damage_type);
                _Rigidbody.velocity = fixDesPos - gameObject.transform.position;
                _FightAttriCalReference.GetKnockOffCount().PlusGauge(4f);
                _FightAttriCalReference.GetKnockOffCount().PlusTimeCounter(0.2f);
                break;
            case DamageType.slight_damage_forward:
                freezed = false;
                used_dizzy_time = FightGlobalSetting._heavyhit_lastingtime;
                fixDesPos = CalFixPosDestination(newValue.damageHappenPoint, newValue.attacker._Center.WholeT.forward, newValue.attacker._Center.WholeT.position, gameObject.transform.position, newValue.from_weapon.damage_type);
                _Rigidbody.velocity = fixDesPos - gameObject.transform.position;
                _FightAttriCalReference.GetKnockOffCount().PlusGauge(3f);
                _FightAttriCalReference.GetKnockOffCount().PlusTimeCounter(0.2f);
                break;
            case DamageType.draw:
                used_dizzy_time = FightGlobalSetting._heavyhit_lastingtime;
                Vector3 vector3 = newValue.from_weapon_marker.transform.position;
                vector3.y = gameObject.transform.position.y;
                gameObject.transform.DOMove(vector3,0.1f).
                OnComplete(() =>{_Rigidbody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;freezed = true; });
                _FightAttriCalReference.GetKnockOffCount().PlusGauge(3f);
                _FightAttriCalReference.GetKnockOffCount().PlusTimeCounter(0.2f);
                break;
            case DamageType.explosion:
                freezed = false;
                used_dizzy_time = FightGlobalSetting._heavyhit_lastingtime;
                fixDesPos = CalFixPosDestination(newValue.damageHappenPoint, newValue.attacker._Center.WholeT.forward, newValue.attacker._Center.WholeT.position, gameObject.transform.position, newValue.from_weapon.damage_type);
                _Rigidbody.velocity = fixDesPos - gameObject.transform.position;
                _FightAttriCalReference.GetKnockOffCount().PlusGauge(3f);
                _FightAttriCalReference.GetKnockOffCount().PlusTimeCounter(0.2f);
                break;
        }
        if (newValue.from_weapon.effectSpreadOnBody)
        {
            _FightAttriCalReference.RunShaderChangeProcess(FightGlobalSetting.EffectPathDefine(newValue.from_weapon.zokusei), 0.1f);
        }
        if (_FightAttriCalReference.GetKnockOffCount().GetGauge() >= FightGlobalSetting._knockoffextent && newValue.from_weapon.damage_type == DamageType.supper_damage_forward)//&& newValue.damage_type == DamageType.supper_damage
        {
            _FightAttriCalReference.GetKnockOffCount().SetGauge(0f);
            _AIStateRunner.ChangeState("KnockOff", newValue);
            return;
        }
        RotateToTarget_Tween(newValue.damageHappenPoint, 0.1f, true);
        TimeCounter = 0f;
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
