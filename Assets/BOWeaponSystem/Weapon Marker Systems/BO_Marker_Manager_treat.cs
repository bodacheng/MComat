using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HittingDetection
{
    public partial class BO_Marker_Manager : MonoBehaviour
    {
        private V_Damage new_damage;
        
        // 下面这个结构目前为止事关三大重要的索引作用
        // 1. 武器在击中敌人时，作为攻击力参考
        // 2. 武器击中敌人时，特效种类参考
        // 3. 武器击中敌人时，对攻击方的hit combo进行加算
        // 由于BO_Marker_Manager现在全部都是对象池物件，如果我们认为一个instance返回对象池后就应该不再参与任何工作的话，
        // 原则上我们应该确保一切围绕BO_Marker_Manage的instance，最重要的是里面的myOwnerHealth进行的工作在instance返回对象池前结束
        private Attack_on_shield_result collision;
        private float _ContinuousDamage_Timer;
        //These DH and DS variables are Distances to the shield spots. Whie the shield is active, DH ("Distance to Health", distance to the back point of the shiled) has to be less than all the other shield edge spots (DS, "Distance to Shield")
        private float dh;
        private float ds1;
        private float ds2;
        private float ds3;
        private float ds4;
        private float ds5;
        private float ds6;
        private float ds7;
        private float ds8;
        private float ds9;
        
        private void TreatProcess()
        {
            if (HitShield && traditionalDefendMode)//其实由上面的分析可以知道，对于来自一把武器的攻击，hitshield和hitflesh是不会同时为true的。但如果多把武器同时来攻击，如果被攻击方同时有被击中以及防御住的情况发生，肯定要先处理所受伤害，立刻转入受伤状态才对
            {
                for (int i1 = 0; i1 < _Shields_Hit.Count; i1++)
                {
                    if (!_Used_Targets.Contains(_Shields_Hit[i1])) //无论对墙壁，盾牌，还是伤害对象，每一轮攻击只会造成一次影响
                    {
                        WeaponEnergyExaust(_Shields_Hit[i1].position, _Shields_Hit[i1].rotation);
                        TheS = _Shields_Hit[i1].GetComponent<BO_Shield>();
                        collision = Attack_And_Shield_Specification.Instance.Attack_On_Shield_Cal(damage_type, TheS.damage_type);
                        // 在此向攻击方发送趔趄信号。这个地方是客户端对战时候逻辑困难的关键。
                        // 武器脚本虽然处理内容非常繁冗，但归结起来其实逻辑只有那几条，就是通过武器与hitbox以及盾牌的接触碰撞，来决定向健康体发送哪些信息。这些计算，原则上其实只需要一个客户端的逻辑去计算
                        // 现在我们在讨论的其实是关于同步问题的一个核心的事情。。。什么时候两边都需要执行，什么时候只需要一个客户端执行。我们现在不熟悉处理这类问题的逻辑方式。
                        if (_myOwnerCalReference != null)
                        {
                            this._myOwnerCalReference._Center.pusher.hiddenMethods.ITouchedThisCollider(1);
                            switch (collision.on_weapon_holder)
                            {
                                case DamageType.stagger:
                                    new_damage = new V_Damage(DamageType.stagger, WeaponPosAdjustMode.explosion, _Shields_Hit[i1].position, Vector3.zero,TheS.transform.position, null);//盾牌主人的wholeT在当前系统下获得不了，攻击的施加方是那个盾牌，不需要写fromweapon了
                                    _myOwnerCalReference.ApplyDamage(new_damage);
                                    break;
                                case DamageType.none:
                                    break;
                            }
                        }

                        //在此向防御方发送防御信号
                        if (TheS._ownerFightAttriCalReference != null)
                        {
                            switch (collision.on_shield_holder)
                            {
                                case DamageType.light_block:
                                    //Vector3 jiuzhengweizhi;
                                    new_damage = new V_Damage(DamageType.light_block, WeaponPosAdjustMode.pushToMidForward ,_WeaponHolderCenter.position, attackerWholeTransform.forward,attackerWholeTransform.position,this); //这地方是因为我们不了解往同一个列表里加入相同变量两次到底是啥结果。。。所以保险起见
                                    TheS.PlusHP(-1);
                                    TheS._ownerFightAttriCalReference.ApplyDamage(new_damage);
                                    break;
                                case DamageType.heavy_block:
                                    new_damage = new V_Damage(DamageType.heavy_block, WeaponPosAdjustMode.pushToMidForward ,_WeaponHolderCenter.position, attackerWholeTransform.forward,attackerWholeTransform.position,this); //这地方是因为我们不了解往同一个列表里加入相同变量两次到底是啥结果。。。所以保险起见
                                    TheS.PlusHP(-2);
                                    TheS._ownerFightAttriCalReference.ApplyDamage(new_damage);
                                    break;
                                case DamageType.supper_damage:
                                    new_damage = new V_Damage(DamageType.supper_damage, WeaponPosAdjustMode.pushToMidForward, _WeaponHolderCenter.position, attackerWholeTransform.forward, attackerWholeTransform.position,this); //这地方是因为我们不了解往同一个列表里加入相同变量两次到底是啥结果。。。所以保险起见
                                    TheS._ownerFightAttriCalReference.ApplyDamage(new_damage);
                                    break;
                                case DamageType.none:
                                    break;
                            }
                        }
                        _Used_Targets.Add(_Shields_Hit[i1]);
                    }
                }

                if (_wallHitPositions.Count > 0)
                {
                    Vector3 point;
                    for (int temp = 0; temp < _wallHitPositions.Count; temp++)
                    {
                        point = _wallHitPositions[temp];
                        if (IfVectorClean(point))
                        {
                            processingBlood = EffectAndHurtObjectLoading.Instance.GenerateEffect("Sparks", this.personalEffectPath,point, Quaternion.LookRotation(_MarkersParent.transform.position - point, Vector3.up),null);
                        }
                    }
                }
            }

            if (HitFlesh)
            {
                foreach (HitOnHealthBody _hitOnHealthBody in hitsOnHealthBody)
                {
                    if (_hitOnHealthBody._victimFightAttriCalReference != null && _Used_Targets.Contains(_hitOnHealthBody._victimFightAttriCalReference.transform) == false)
                    {
                        WeaponEnergyExaust(_hitOnHealthBody._Startpoint, Quaternion.identity);
                        new_damage = new V_Damage(damage_type, _WeaponPosAdjustMode, _hitOnHealthBody._Startpoint, attackerWholeTransform.forward,attackerWholeTransform.position,this,_specialApply);
                        if (effectSpreadOnBody && _hitOnHealthBody._victimFightAttriCalReference._Center._ResistanceManager.Resistance.Value == 0)
                            _hitOnHealthBody._victimFightAttriCalReference.RunShaderChangeProcess(personalEffectPath, 0.3f, 0.4f);
                        _hitOnHealthBody._victimFightAttriCalReference.ApplyDamage(new_damage);

                        if (this._myOwnerCalReference != null)
                        {
                            this._myOwnerCalReference.MyDamageCount(new_damage);
                            this._myOwnerCalReference._Center.pusher.hiddenMethods.ITouchedThisCollider(1);
                        }

                        if (IfVectorClean(_hitOnHealthBody._Startpoint))
                        {
                            if (_hitOnHealthBody._victimFightAttriCalReference._Center._ResistanceManager.Resistance.Value > 0)
                            {
                                processingBlood = EffectAndHurtObjectLoading.Instance.GenerateEffect("Sparks",
                                                                                       this.personalEffectPath,
                                                                                       _hitOnHealthBody._Startpoint,
                                                                                       Quaternion.LookRotation(_hitOnHealthBody._Direction, Vector3.up),
                                                                                       effectSpreadOnBody ? _hitOnHealthBody._victimFightAttriCalReference.transform : null);
                            }
                            else
                            {
                                switch (damage_type)
                                {
                                    case DamageType.slight_damage:
                                        processingBlood = EffectAndHurtObjectLoading.Instance.GenerateEffect("light_hit",
                                                                                       this.personalEffectPath,
                                                                                       _hitOnHealthBody._Startpoint,
                                                                                       Quaternion.LookRotation(_hitOnHealthBody._Direction, Vector3.up),
                                                                                       effectSpreadOnBody ? _hitOnHealthBody._victimFightAttriCalReference.transform : null);
                                        //PlayTargetHitSound("light_hit");
                                        break;
                                    case DamageType.light_damage:
                                        processingBlood = EffectAndHurtObjectLoading.Instance.GenerateEffect("light_hit",
                                                                                       this.personalEffectPath,
                                                                                       _hitOnHealthBody._Startpoint,
                                                                                       Quaternion.LookRotation(_hitOnHealthBody._Direction, Vector3.up),
                                                                                       effectSpreadOnBody ? _hitOnHealthBody._victimFightAttriCalReference.transform : null);
                                        //PlayTargetHitSound("light_hit");
                                        break;
                                    case DamageType.heavy_damage:
                                        processingBlood = EffectAndHurtObjectLoading.Instance.GenerateEffect("heavy_hit",
                                                                                       this.personalEffectPath,
                                                                                       _hitOnHealthBody._Startpoint,
                                                                                       Quaternion.LookRotation(_hitOnHealthBody._Direction, Vector3.up),
                                                                                       effectSpreadOnBody ? _hitOnHealthBody._victimFightAttriCalReference.transform : null);
                                        //PlayTargetHitSound("heavy_hit");
                                        break;
                                    case DamageType.supper_damage:
                                        processingBlood = EffectAndHurtObjectLoading.Instance.GenerateEffect("super_hit",
                                                                                       this.personalEffectPath,
                                                                                       _hitOnHealthBody._Startpoint,
                                                                                       Quaternion.LookRotation(_hitOnHealthBody._Direction, Vector3.up),
                                                                                       effectSpreadOnBody ? _hitOnHealthBody._victimFightAttriCalReference.transform : null);
                                        //PlayTargetHitSound("super_hit");
                                        break;
                                    default:
                                        processingBlood = EffectAndHurtObjectLoading.Instance.GenerateEffect("light_hit",
                                                                                       this.personalEffectPath,
                                                                                       _hitOnHealthBody._Startpoint,
                                                                                       Quaternion.LookRotation(_hitOnHealthBody._Direction, Vector3.up),
                                                                                       effectSpreadOnBody ? _hitOnHealthBody._victimFightAttriCalReference.transform : null);
                                        //PlayTargetHitSound("light_hit");
                                        break;
                                }
                            }
                        }
                        else
                        {
                            Debug.Log("火花位置产生不干净值");
                        }

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
                        _Used_Targets.Add(_hitOnHealthBody._victimFightAttriCalReference.transform);
                    }
                }
                if (_myOwnerCalReference != null)
                {
                    _myOwnerCalReference.plusCriticalGauge(3);
                }
            }

            if (!ContinuousDamage && weaponHP > 0)
                ClearTargets();

            _wallHitPositions.Clear();

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