using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HittingDetection
{
    public partial class BO_Marker_Manager : MonoBehaviour
    {
        private Vector3 force_direction;
        private V_Damage new_damage;
        
        // 下面这个结构目前为止事关三大重要的索引作用
        // 1. 武器在击中敌人时，作为攻击力参考
        // 2. 武器击中敌人时，特效种类参考
        // 3. 武器击中敌人时，对攻击方的hit combo进行加算
        // 由于BO_Marker_Manager现在全部都是对象池物件，如果我们认为一个instance返回对象池后就应该不再参与任何工作的话，
        // 原则上我们应该确保一切围绕BO_Marker_Manage的instance，最重要的是里面的myOwnerHealth进行的工作在instance返回对象池前结束
        private FightAttriCalReference _FightAttriCalReference;
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
                        //在此向攻击方发送趔趄信号。这个地方是客户端对战时候逻辑困难的关键。
                        // 武器脚本虽然处理内容非常繁冗，但归结起来其实逻辑只有那几条，就是通过武器与hitbox以及盾牌的接触碰撞，来决定向健康体发送哪些信息。这些计算，原则上其实只需要一个客户端的逻辑去计算
                        // 现在我们在讨论的其实是关于同步问题的一个核心的事情。。。什么时候两边都需要执行，什么时候只需要一个客户端执行。我们现在不熟悉处理这类问题的逻辑方式。
                        if (_FightAttriCalReference != null)
                        {
                            this._FightAttriCalReference._Center.pusher.WhenIHitSomethingEnemy(1);
                            switch (collision.on_weapon_holder)
                            {
                                case DamageType.stagger:
                                    force_direction = _WeaponHolderCenter.position - TheS._ParentHealth.transform.position;
                                    new_damage = new V_Damage(DamageType.stagger, force_direction, Vector3.zero, _FightAttriCalReference, this);
                                    _FightAttriCalReference.ApplyDamage(new_damage);
                                    break;
                                case DamageType.none:
                                    break;
                            }
                        }

                        //在此向防御方发送防御信号
                        if (TheS._ParentHealth != null)
                        {
                            switch (collision.on_shield_holder)
                            {
                                case DamageType.light_block:
                                    //Vector3 jiuzhengweizhi;
                                    if (_FightAttriCalReference != null)
                                    {
                                        force_direction = TheS._ParentHealth.transform.position - _WeaponHolderCenter.position;
                                        //jiuzhengweizhi = myOwnerHealth.transform.position + myOwnerHealth.transform.forward * 3f;
                                    }
                                    else
                                    {
                                        //jiuzhengweizhi = this.transform.position;
                                        force_direction = TheS._ParentHealth.transform.position - _WeaponHolderCenter.position;
                                    }
                                    new_damage = new V_Damage(DamageType.light_block, force_direction, Vector3.zero, TheS._ParentHealth, this); //这地方是因为我们不了解往同一个列表里加入相同变量两次到底是啥结果。。。所以保险起见
                                    TheS.plusHP(-1);
                                    TheS._ParentHealth.ApplyDamage(new_damage);
                                    break;
                                case DamageType.heavy_block:
                                    if (_FightAttriCalReference != null)
                                    {
                                        //jiuzhengweizhi = myOwnerHealth.transform.position + myOwnerHealth.transform.forward * 3.5f;
                                        force_direction = TheS._ParentHealth.transform.position - _WeaponHolderCenter.position;
                                    }
                                    else
                                    {
                                        //jiuzhengweizhi = this.transform.position;
                                        force_direction = TheS._ParentHealth.transform.position;
                                    }
                                    new_damage = new V_Damage(DamageType.heavy_block, force_direction, Vector3.zero, TheS._ParentHealth, this); //这地方是因为我们不了解往同一个列表里加入相同变量两次到底是啥结果。。。所以保险起见
                                    TheS.plusHP(-2);
                                    TheS._ParentHealth.ApplyDamage(new_damage);
                                    break;
                                case DamageType.supper_damage:
                                    force_direction = TheS._ParentHealth.transform.position - _WeaponHolderCenter.position;
                                    new_damage = new V_Damage(DamageType.supper_damage, force_direction, Vector3.zero, TheS._ParentHealth, this); //这地方是因为我们不了解往同一个列表里加入相同变量两次到底是啥结果。。。所以保险起见
                                    TheS._ParentHealth.ApplyDamage(new_damage);
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
                    if (_hitOnHealthBody._BO_Health != null && _Used_Targets.Contains(_hitOnHealthBody._BO_Health.transform) == false)
                    {
                        WeaponEnergyExaust(_hitOnHealthBody._Startpoint, Quaternion.identity);
                        switch (_WeaponPosAdjustMode)
                        {
                            case WeaponPosAdjustMode.pushToMidForward:
                                force_direction = _hitOnHealthBody._BO_Health.transform.position - _WeaponHolderCenter.position;
                                //底下这个平行四边形学受力校准处理，效果比较微妙。待定吧。
                                if (_FightAttriCalReference != null)
                                {
                                    //平行四边形。用以攻击校准。
                                    force_direction = _FightAttriCalReference._Center.WholeT.transform.forward * 2f - force_direction.normalized;
                                    //上面这个myOwnerHealth.transform.forward乘以的参数，其实就是平行四边形中间连线和一个边的比例，如果是正方形的话那不就是pai2？这个值其实越小的话，这种校准力越大
                                    //但如果小于1的话逻辑就会发生些问题。
                                }
                                break;
                            case WeaponPosAdjustMode.draw:
                                force_direction = _hitOnHealthBody.marker_point - _hitOnHealthBody._BO_Health.transform.position;//transform.position
                                force_direction = force_direction.normalized;
                                break;
                        }

                        //Vector3 jiuzhengweizhi;
                        //if (myOwnerHealth != null && _hitOnHealthBody._BO_Health.canBeDistanceManage)
                        //{
                        //jiuzhengweizhi = sensorPoint + myOwnerHealth.transform.forward * onHitRangeAdjustDis;//暂时这么处理
                        //jiuzhengweizhi.y = 0;
                        //}else{
                        //jiuzhengweizhi = this.transform.position;
                        //}
                        new_damage = new V_Damage(damage_type, force_direction, _hitOnHealthBody._Startpoint, _hitOnHealthBody._BO_Health, this,_specialApply);
                        if (effectSpreadOnBody && _hitOnHealthBody._BO_Health._Center._ResistanceManager.Resistance.Value == 0)
                            _hitOnHealthBody._BO_Health.RunShaderChangeProcess(personalEffectPath, 0.3f, 0.4f);
                        _hitOnHealthBody._BO_Health.ApplyDamage(new_damage);

                        if (this._FightAttriCalReference != null)
                        {
                            this._FightAttriCalReference.MyDamageCount(new_damage);
                            this._FightAttriCalReference._Center.pusher.WhenIHitSomethingEnemy(1);
                        }

                        if (IfVectorClean(_hitOnHealthBody._Startpoint))
                        {
                            if (_hitOnHealthBody._BO_Health._Center._ResistanceManager.Resistance.Value > 0)
                            {
                                processingBlood = EffectAndHurtObjectLoading.Instance.GenerateEffect("Sparks",
                                                                                       this.personalEffectPath,
                                                                                       _hitOnHealthBody._Startpoint,
                                                                                       Quaternion.LookRotation(_hitOnHealthBody._Direction, Vector3.up),
                                                                                       effectSpreadOnBody ? _hitOnHealthBody._BO_Health.transform : null);
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
                                                                                       effectSpreadOnBody ? _hitOnHealthBody._BO_Health.transform : null);
                                        //PlayTargetHitSound("light_hit");
                                        break;
                                    case DamageType.light_damage:
                                        processingBlood = EffectAndHurtObjectLoading.Instance.GenerateEffect("light_hit",
                                                                                       this.personalEffectPath,
                                                                                       _hitOnHealthBody._Startpoint,
                                                                                       Quaternion.LookRotation(_hitOnHealthBody._Direction, Vector3.up),
                                                                                       effectSpreadOnBody ? _hitOnHealthBody._BO_Health.transform : null);
                                        //PlayTargetHitSound("light_hit");
                                        break;
                                    case DamageType.heavy_damage:
                                        processingBlood = EffectAndHurtObjectLoading.Instance.GenerateEffect("heavy_hit",
                                                                                       this.personalEffectPath,
                                                                                       _hitOnHealthBody._Startpoint,
                                                                                       Quaternion.LookRotation(_hitOnHealthBody._Direction, Vector3.up),
                                                                                       effectSpreadOnBody ? _hitOnHealthBody._BO_Health.transform : null);
                                        //PlayTargetHitSound("heavy_hit");
                                        break;
                                    case DamageType.supper_damage:
                                        processingBlood = EffectAndHurtObjectLoading.Instance.GenerateEffect("super_hit",
                                                                                       this.personalEffectPath,
                                                                                       _hitOnHealthBody._Startpoint,
                                                                                       Quaternion.LookRotation(_hitOnHealthBody._Direction, Vector3.up),
                                                                                       effectSpreadOnBody ? _hitOnHealthBody._BO_Health.transform : null);
                                        //PlayTargetHitSound("super_hit");
                                        break;
                                    default:
                                        processingBlood = EffectAndHurtObjectLoading.Instance.GenerateEffect("light_hit",
                                                                                       this.personalEffectPath,
                                                                                       _hitOnHealthBody._Startpoint,
                                                                                       Quaternion.LookRotation(_hitOnHealthBody._Direction, Vector3.up),
                                                                                       effectSpreadOnBody ? _hitOnHealthBody._BO_Health.transform : null);
                                        //PlayTargetHitSound("light_hit");
                                        break;
                                }
                            }
                        }
                        else
                        {
                            Debug.Log("火花位置产生不干净值");
                        }

                        if (is_E_weapon)
                        {
                            if (e_Damage != null)
                            {
                                if (!(e_Damage.Position_set.Parent == null && e_Damage.Position_set.Child == null) && !(e_Damage.Position_set.Parent != null && e_Damage.Position_set.Child != null))
                                {
                                    _hitOnHealthBody._BO_Health.AddEventDamageList(e_Damage);
                                }
                            }
                        }
                        _Used_Targets.Add(_hitOnHealthBody._BO_Health.transform);
                    }
                }
                if (_FightAttriCalReference != null)
                {
                    _FightAttriCalReference.plusCriticalGauge(3);
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
    }
}