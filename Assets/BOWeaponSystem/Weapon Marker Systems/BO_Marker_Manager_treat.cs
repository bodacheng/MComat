using UnityEngine;

namespace HittingDetection
{
    public partial class BO_Marker_Manager : MonoBehaviour
    {
        // 下面这个结构目前为止事关三大重要的索引作用
        // 1. 武器在击中敌人时，作为攻击力参考
        // 2. 武器击中敌人时，特效种类参考
        // 3. 武器击中敌人时，对攻击方的hit combo进行加算
        // 由于BO_Marker_Manager现在全部都是对象池物件，如果我们认为一个instance返回对象池后就应该不再参与任何工作的话，
        // 原则上我们应该确保一切围绕BO_Marker_Manage的instance，最重要的是里面的myOwnerHealth进行的工作在instance返回对象池前结束
        
        Attack_on_shield_result collision;
        float _ContinuousDamage_Timer;
        //These DH and DS variables are Distances to the shield spots. Whie the shield is active, DH ("Distance to Health", distance to the back point of the shiled) has to be less than all the other shield edge spots (DS, "Distance to Shield")
        float dh;
        float ds1;
        float ds2;
        float ds3;
        float ds4;
        float ds5;
        float ds6;
        float ds7;
        float ds8;
        float ds9;
        
        void TreatProcess()
        {
            if (HitShield && traditionalDefendMode)//其实由上面的分析可以知道，对于来自一把武器的攻击，hitshield和hitflesh是不会同时为true的。但如果多把武器同时来攻击，如果被攻击方同时有被击中以及防御住的情况发生，肯定要先处理所受伤害，立刻转入受伤状态才对
            {
                for (int i1 = 0; i1 < _Shields_Hit.Count; i1++)
                {
                    if (!_Used_Targets.Contains(_Shields_Hit[i1])) //无论对墙壁，盾牌，还是伤害对象，每一轮攻击只会造成一次影响
                    {
                        BO_Shield TheS = _Shields_Hit[i1].GetComponent<BO_Shield>();
                        collision = Attack_And_Shield_Specification.Instance.Attack_On_Shield_Cal(damage_type, TheS.damage_type);
                        _myOwnerCalReference._Center._BasicPhysicSupport.hiddenMethods.ITouchedThisCollider(1);
                        switch (collision.on_weapon_holder)
                        {
                            case DamageType.stagger:
                                    V_Damage new_damage = new V_Damage(0,
                                        _myOwnerCalReference,TheS._ownerFightAttriCalReference,
                                        DamageType.stagger, WeaponPosAdjustMode.explosion, this._WeaponMode,SpecialApply.none,
                                        _Shields_Hit[i1].position, Quaternion.LookRotation(_Shields_Hit[i1].position - TheS._ShieldBackSpot.transform.position), 
                                        TheS._ownerFightAttriCalReference._Center.WholeT.forward,TheS._ownerFightAttriCalReference._Center.WholeT.position,
                                        personalEffectPath, this.effectSpreadOnBody);　//盾牌主人的wholeT在当前系统下获得不了，攻击的施加方是那个盾牌，不需要写fromweapon了
                                    _myOwnerCalReference.ApplyDamage(new_damage);
                                break;
                            case DamageType.none:
                                break;
                        }
                        
                        //在此向防御方发送防御信号
                        if (TheS._ownerFightAttriCalReference != null)
                        {
                            switch (collision.on_shield_holder)
                            {
                                case DamageType.light_block:
                                    V_Damage new_damage = new V_Damage(0f,
                                        TheS._ownerFightAttriCalReference, _myOwnerCalReference,
                                        DamageType.light_block, WeaponPosAdjustMode.pushToMidForward, this._WeaponMode,SpecialApply.none,
                                        _WeaponHolderCenter.position, Quaternion.LookRotation(_Shields_Hit[i1].position - TheS._ShieldBackSpot.transform.position), 
                                        attackerWholeTransform.forward, attackerWholeTransform.position,
                                        personalEffectPath, effectSpreadOnBody);
                                    TheS.PlusHP(-1);
                                    TheS._ownerFightAttriCalReference.ApplyDamage(new_damage);
                                    break;
                                case DamageType.heavy_block:
                                        new_damage = new V_Damage(0f,
                                        TheS._ownerFightAttriCalReference, _myOwnerCalReference,
                                        DamageType.heavy_block, WeaponPosAdjustMode.pushToMidForward, this._WeaponMode,SpecialApply.none,
                                        _WeaponHolderCenter.position, Quaternion.LookRotation(_Shields_Hit[i1].position - TheS._ShieldBackSpot.transform.position), 
                                        attackerWholeTransform.forward, attackerWholeTransform.position,
                                        personalEffectPath, effectSpreadOnBody);
                                    TheS.PlusHP(-2);
                                    TheS._ownerFightAttriCalReference.ApplyDamage(new_damage);
                                    break;
                                case DamageType.supper_damage:
                                        new_damage = new V_Damage(0f,
                                        TheS._ownerFightAttriCalReference, _myOwnerCalReference,
                                        DamageType.supper_damage, WeaponPosAdjustMode.pushToMidForward, this._WeaponMode,SpecialApply.none,
                                        _WeaponHolderCenter.position, Quaternion.LookRotation(_Shields_Hit[i1].position - TheS._ShieldBackSpot.transform.position), 
                                        attackerWholeTransform.forward, attackerWholeTransform.position,
                                        personalEffectPath,effectSpreadOnBody);
                                    TheS._ownerFightAttriCalReference.ApplyDamage(new_damage);
                                    break;
                                case DamageType.none:
                                    break;
                            }
                        }
                        _Used_Targets.Add(_Shields_Hit[i1]);
                    }
                }
            }

            if (HitFlesh)
            {
                foreach (V_Damage _hitOnHealthBody in hitsOnHealthBody)
                {
                    if (_hitOnHealthBody.victim != null && _Used_Targets.Contains(_hitOnHealthBody.victim.transform) == false)
                    {
                        _hitOnHealthBody.victim.ApplyDamage(_hitOnHealthBody);
                        _hitOnHealthBody.attacker.MyDamageCount(_hitOnHealthBody);
                        _hitOnHealthBody.attacker._Center._BasicPhysicSupport.hiddenMethods.ITouchedThisCollider(1);
                        _hitOnHealthBody.attacker.PlusCriticalGauge(1);
                        //if (is_E_weapon)
                        //{
                        //    if (e_Damage != null)
                        //    {
                        //        if (!(e_Damage.Position_set.Parent == null && e_Damage.Position_set.Child == null) && !(e_Damage.Position_set.Parent != null && e_Damage.Position_set.Child != null))
                        //        {
                        //            _hitOnHealthBody._BO_Health.AddEventDamageList(e_Damage);
                        //        }
                        //    }
                        //}
                        if (_Used_Targets != null)
                        {
                            _Used_Targets.Add(_hitOnHealthBody.victim.transform);
                        }else{
                            Debug.Log("邪门了："+gameObject);
                        }
                    }
                }
            }

            // 下面的环节对特效攻击的“能量消解”机制极端关键。如果非ContinuousDamage那么每帧一个能量特效只会被一个撞击对象消耗一格寿命
            // 可以结合WeaponEnergyExaust的作用位置来理解
            if (!ContinuousDamage && weaponHP > 0)
            {
                ClearTargets();
            }
            if (ContinuousDamage)
            {
                _ContinuousDamage_Timer += Time.fixedDeltaTime;
                if (_ContinuousDamage_Timer >= ContinuousDamageInterval)
                {
                    ClearTargets();
                    _ContinuousDamage_Timer = 0;
                }
            }
        }

        Vector3 CalFixPosDestination(Vector3 damageHappenPoint, Transform attackerTransform)
        {
            damageHappenPoint.y = 0;
            return (Vector3.Dot(damageHappenPoint - attackerTransform.position,attackerTransform.forward) * attackerTransform.forward + attackerTransform.position);
        }
        
        // 点积的计算方式为:  a·b=|a|·|b|cos<a,b>  其中|a|和|b|表示向量的模，<a,b>表示两个向量的夹角。另外在 点积 中，<a,b>和<b,a> 夹角是不分顺序的。 
        // 所以通过点积，我们其实是可以计算两个向量的夹角的。 
        // 另外通过点积的计算我们可以简单粗略的判断当前物体是否朝向另外一个物体: 只需要计算当前物体的transform.forward向量与 (otherObj.transform.position – transform.position)的点积即可， 大于0则面对，否则则背对着。当然这个计算也会有一点误差，但大致够用。 
    }
}