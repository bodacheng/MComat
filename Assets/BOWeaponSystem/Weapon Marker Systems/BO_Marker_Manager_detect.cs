using System.Collections.Generic;
using UnityEngine;

namespace HittingDetection
{
    public partial class BO_Marker_Manager : MonoBehaviour
    {
        FightAttriCalReference _Raw_Target_Instance;//A single target which was hit.
        FightAttriCalReference CalReference;
        BO_Limb _BO_Hitbox;
        Vector3 _StartPoint;
        List<Collider> BallDetectHitPool;
        BO_Weapon_Animation_Events bO_Weapon_Animation_Events;//20180208 重要改修：凡是与这个量建立连接的BO_Marker_Manager，都“一体化”

        void DetectProcess()
        {
            hitsOnHealthBody.Clear();
            for (int i = 0; i < _markers.Length; i++)
            {
                if (_markers[i].HitCheck())
                {
                    if (_markers[i] is Trail_Marker)
                    {
                        RaycastHit[] _hits = ((Trail_Marker)_markers[i])._hits;
                        if (TraditionalDefendMode)
                        {
                            for (int hit_target_index = 0; hit_target_index < _hits.Length; hit_target_index++)
                            {
                                if (_markers[i].enemyShieldLayer == (_markers[i].enemyShieldLayer | 1 << _hits[hit_target_index].collider.gameObject.layer) && !_Shields_Hit.Contains(_hits[hit_target_index].collider.transform))
                                {
                                    BO_Shield TheS = _hits[hit_target_index].collider.gameObject.GetComponent<BO_Shield>();
                                    if (TheS == null || TheS._ownerFightAttriCalReference == null)
                                    {
                                        Debug.Log("防御盾构造严重错误");
                                        break;
                                    }
                                    if (_Shields_Hit.Contains(TheS.transform) == false // 本帧之内只要有武器上的一个mark打中了盾牌，那不再考虑其他mark是否打中盾牌
                                        && _Used_Targets.Contains(TheS.transform) == false //used_target只在一轮攻击后才清空，所以这里的意思应该是：如果打中的这个盾牌物体在这一轮里已经起过一次作用，那就不再研究。
                                        && _Used_Targets.Contains(TheS._ownerFightAttriCalReference.transform) == false) //所打中的盾牌对应的肉体已经在本轮攻击起过一次作用，那也不再详细计算 一把武器一轮enablemarkers和disablemarkers之间只可能对一个敌人进行一次伤害或进行一次“被防御”，敌人不可能在一把武器的一轮攻击期间内既受伤一次又防御成功一次
                                    {
                                        if (TheS._AdvancedShieldDetection)
                                        {
                                            dh = Mathf.Pow(Vector3.Distance(_WeaponHolderCenter.position, TheS._ShieldBackSpot.transform.position), 2);//这第二个参数也就是被攻击方肉体的transform
                                            ds1 = Mathf.Pow(Vector3.Distance(_WeaponHolderCenter.position, TheS._ShieldCenterSpot.transform.position), 2);  //center
                                            ds2 = Mathf.Pow(Vector3.Distance(_WeaponHolderCenter.position, TheS._ShieldEdgeSpot1.transform.position), 2);  //Top
                                            ds3 = Mathf.Pow(Vector3.Distance(_WeaponHolderCenter.position, TheS._ShieldEdgeSpot2.transform.position), 2);  //Top Left
                                            ds4 = Mathf.Pow(Vector3.Distance(_WeaponHolderCenter.position, TheS._ShieldEdgeSpot3.transform.position), 2);  //Top Right
                                            ds5 = Mathf.Pow(Vector3.Distance(_WeaponHolderCenter.position, TheS._ShieldEdgeSpot4.transform.position), 2);  //Bottom
                                            ds6 = Mathf.Pow(Vector3.Distance(_WeaponHolderCenter.position, TheS._ShieldEdgeSpot5.transform.position), 2);  //Bottom Left
                                            ds7 = Mathf.Pow(Vector3.Distance(_WeaponHolderCenter.position, TheS._ShieldEdgeSpot6.transform.position), 2);  //Bottom Right
                                            ds8 = Mathf.Pow(Vector3.Distance(_WeaponHolderCenter.position, TheS._ShieldEdgeSpot7.transform.position), 2);  //Right
                                            ds9 = Mathf.Pow(Vector3.Distance(_WeaponHolderCenter.position, TheS._ShieldEdgeSpot8.transform.position), 2);  //Left
                                           //Debug.DrawRay(_WeaponHolderCenter.position, TheS._ShieldBackSpot.transform.position - _WeaponHolderCenter.position, Color.green, 5);
                                           //Debug.DrawRay(_WeaponHolderCenter.position, TheS._ShieldCenterSpot.transform.position - _WeaponHolderCenter.position, Color.red, 5);
                                           //Debug.DrawRay(_WeaponHolderCenter.position, TheS._ShieldEdgeSpot7.transform.position - _WeaponHolderCenter.position, Color.blue, 5);
                                           //Debug.DrawRay(_WeaponHolderCenter.position, TheS._ShieldEdgeSpot1.transform.position - _WeaponHolderCenter.position, Color.blue, 5);
                                           //Debug.DrawRay(_WeaponHolderCenter.position, TheS._ShieldEdgeSpot2.transform.position - _WeaponHolderCenter.position, Color.blue, 5);
                                           //Debug.DrawRay(_WeaponHolderCenter.position, TheS._ShieldEdgeSpot3.transform.position - _WeaponHolderCenter.position, Color.blue, 5);
                                           //Debug.DrawRay(_WeaponHolderCenter.position, TheS._ShieldEdgeSpot4.transform.position - _WeaponHolderCenter.position, Color.blue, 5);
                                           //Debug.DrawRay(_WeaponHolderCenter.position, TheS._ShieldEdgeSpot6.transform.position - _WeaponHolderCenter.position, Color.blue, 5);
                                           //Debug.DrawRay(_WeaponHolderCenter.position, TheS._ShieldEdgeSpot5.transform.position - _WeaponHolderCenter.position, Color.blue, 5);
                                           //Debug.DrawRay(_WeaponHolderCenter.position, TheS._ShieldEdgeSpot8.transform.position - _WeaponHolderCenter.position, Color.blue, 5);
                                        }
                                        if (((dh > ds1) || (dh > ds2) || (dh > ds3) || (dh > ds4) || (dh > ds5) || (dh > ds6) || (dh > ds7) || (dh > ds8) || (dh > ds9)) || (TheS._AdvancedShieldDetection == false))
                                        {
                                            //飞行道具一律不需要开启_AdvancedShieldDetection功能，因为这个功能本身针对的就是近距离一方对另一方发起大幅度动作的挥舞攻击时攻击区域太广阔所造成的防御失灵，
                                            //就比如说一个弧形攻击，打中盾牌的瞬间这个弧形攻击动作还是会继续，那攻击点由于动画问题穿透了对方的盾牌打到对方身体话，就和我们对攻击防御演出的认识相违背了。
                                            _Shields_Hit.Add(TheS.transform);
                                            HitShield = true;
                                            _Used_Targets.Add(TheS._ownerFightAttriCalReference.transform);//这一行与接下来含（* *）的两行紧密对应(不管打中盾牌的主人是谁，主人都因为受盾牌保护而不会收到攻击了)
                                            _ShiledHitPositions.Add(_hits[hit_target_index].point);
                                            if (_ShiledHitPositions.Count > 0)
                                            {
                                                TheS.PassHitPointsFromWeaponToShiled(_ShiledHitPositions);//hit points on the shiled
                                            }
                                            _ShiledHitPositions.Clear();
                                        }
                                    }
                                }
                            }
                        }
                        //以上全部内容都是针对射线检测的防御判断

                        for (int hit_target_index = 0; hit_target_index < _hits.Length; hit_target_index++)
                        {
                            if (SpecificTarget != SpecificTarget.flesh)
                            {
                                if ((teamConfig.enemyWeaponLayerMask == (teamConfig.enemyWeaponLayerMask | (1 << _hits[hit_target_index].collider.gameObject.layer))) && !_Used_Targets.Contains(_hits[hit_target_index].collider.transform))
                                {
                                    HitBoxesProcesser.ColliderHitBox.TryGetValue(_hits[hit_target_index].collider, out BO_Marker_Manager hit_hitbox);
                                    if (hit_hitbox != null && hit_hitbox.Enabled)
                                    {
                                        _Used_Targets.Add(_hits[hit_target_index].collider.transform);
                                        WeaponEnergyExaust(_hits[hit_target_index].point, _hits[hit_target_index].collider.transform.rotation);
                                        HitBoxLifeEnding = HitBoxLifeEnding.touched;
                                        if (weaponHP > 0 && CurrentHP <= 0)
                                        {
                                            break;
                                        }
                                        continue;//这里不退出循环的话就可能造成一个HP只有1的能量球既打碎了敌人的一个同血量能量球，又对敌人产生一点伤害。
                                    }
                                }
                            }

                            if (SpecificTarget == SpecificTarget.energy)
                            {
                                continue;
                            }
 
                            //_Raw_Target_Instance这个里面全是mainhealth，就是mainhealth，不是含着mainhealth的transform
                            //_Targets_Raw_Hit里面加入的全是_Raw_Target_Instance的transform，也就是mainhealth的transform
                            CalReference = _hits[hit_target_index].collider.GetComponent<FightAttriCalReference>();
                            _BO_Hitbox = _hits[hit_target_index].collider.GetComponent<BO_Limb>();
                            if (!_Targets_Raw_Hit.Contains(_hits[hit_target_index].collider.transform) && !_Used_Targets.Contains(_hits[hit_target_index].collider.transform))
                            {
                                //方式1：mainhealth所在层级有collider //注意看这行条件，主要就是考虑到防御问题  （* *）
                                //if (_BO_Health != null && _Used_Targets.Contains(_markers[i]._hits[hit_target_index].collider.transform) == false)
                                //{
                                //    if (_BO_Health.collider_on_health)
                                //    {
                                //        HitFlesh = true;
                                //        _Raw_Target_Instance = _BO_Health;
                                //    }
                                //}
                                
                                //方式2：hitbox模式
                                if (_BO_Hitbox != null)
                                {
                                    if (!_Used_Targets.Contains(_BO_Hitbox.MainHealth.transform)) //注意看这行条件，主要就是考虑到防御问题 （* *）
                                    {
                                        HitFlesh = true;
                                        _Raw_Target_Instance = _BO_Hitbox.MainHealth;//从上往下看，其实这一段表达的意思是一轮攻击只对一个main——health造成伤害
                                        _Used_Targets.Add(_BO_Hitbox.transform);
                                    }
                                }
                                
                                if (_Raw_Target_Instance != null)
                                {
                                    _Targets_Raw_Hit.Add(_Raw_Target_Instance.transform);
                                    _StartPoint = _hits[hit_target_index].point;
                                    _StartPoint = _StartPoint + (_hits[hit_target_index].transform.position - _StartPoint) * 0.3f;
                                    hitsOnHealthBody.Add(new V_Damage(this, _markers[i],_Raw_Target_Instance, _MyOwnerCalReference,_StartPoint, Quaternion.LookRotation(_Raw_Target_Instance.transform.position-_StartPoint,Vector3.up)));
                                    WeaponEnergyExaust(_hits[hit_target_index].point, _hits[hit_target_index].collider.transform.rotation);
                                    HitBoxLifeEnding = HitBoxLifeEnding.touched;
                                }
                                if (HitFlesh && _Raw_Target_Instance != null)
                                {
                                    if (_Raw_Target_Instance.GetShield() != null)
                                    {
                                        _Used_Targets.Add(_Raw_Target_Instance.GetShield().transform);
                                        //一把武器一轮enablemarkers和disablemarkers之间只可能对一个敌人进行一次伤害或进行一次“被防御”，敌人不可能在一把武器的一轮攻击期间内既受伤一次又防御成功一次
                                        //因此如果一轮攻击内敌人受伤了，也就再不用研究他能不能防御住所受攻击了。
                                    }
                                }
                            }
                            if (weaponHP > 0 && CurrentHP <= 0)
                            {
                                break;
                            }
                        }
                    }

                    if (_markers[i] is BO_Marker) //其实是针对球形检测的特殊形式把下面那个大for循环按照marker里的BallDetectHitPool重新循环跑了一次
                    {
                        BallDetectHitPool = ((BO_Marker)_markers[i]).GetBallDetectHitPool();
                        if (BallDetectHitPool != null)
                        {
                            if (TraditionalDefendMode)
                            {
                                for (int hit_target_index = 0; hit_target_index < BallDetectHitPool.Count; hit_target_index++)
                                {
                                    if (_markers[i].enemyShieldLayer == (_markers[i].enemyShieldLayer | 1 << BallDetectHitPool[hit_target_index].gameObject.layer)　&&　!_Shields_Hit.Contains(BallDetectHitPool[hit_target_index].transform))
                                    {
                                        BO_Shield TheS = BallDetectHitPool[hit_target_index].gameObject.GetComponent<BO_Shield>();
                                        if (TheS == null)
                                        {
                                            Debug.Log("防御盾构造严重错误");
                                            break;
                                        }

                                        if (TheS._ownerFightAttriCalReference == null)
                                        {
                                            break;
                                        }

                                        if (!_Shields_Hit.Contains(TheS.transform) // 本帧之内只要有武器上的一个mark打中了盾牌，那不再考虑其他mark是否打中盾牌
                                            && !_Used_Targets.Contains(TheS.transform) //used_target只在一轮攻击后才清空，所以这里的意思应该是：如果打中的这个盾牌物体在这一轮里已经起过一次作用，那就不再研究。
                                            && !_Used_Targets.Contains(TheS._ownerFightAttriCalReference.transform)) //所打中的盾牌对应的肉体已经在本轮攻击起过一次作用，那也不再详细计算
                                        {
                                            if (TheS._AdvancedShieldDetection)
                                            {
                                                dh = Mathf.Pow(Vector3.Distance(_WeaponHolderCenter.position, TheS._ShieldBackSpot.transform.position), 2);//这第二个参数也就是被攻击方肉体的transform
                                                ds1 = Mathf.Pow(Vector3.Distance(_WeaponHolderCenter.position, TheS._ShieldCenterSpot.transform.position), 2);  //center
                                                ds2 = Mathf.Pow(Vector3.Distance(_WeaponHolderCenter.position, TheS._ShieldEdgeSpot1.transform.position), 2);  //Top
                                                ds3 = Mathf.Pow(Vector3.Distance(_WeaponHolderCenter.position, TheS._ShieldEdgeSpot2.transform.position), 2);  //Top Left
                                                ds4 = Mathf.Pow(Vector3.Distance(_WeaponHolderCenter.position, TheS._ShieldEdgeSpot3.transform.position), 2);  //Top Right
                                                ds5 = Mathf.Pow(Vector3.Distance(_WeaponHolderCenter.position, TheS._ShieldEdgeSpot4.transform.position), 2);  //Bottom
                                                ds6 = Mathf.Pow(Vector3.Distance(_WeaponHolderCenter.position, TheS._ShieldEdgeSpot5.transform.position), 2);  //Bottom Left
                                                ds7 = Mathf.Pow(Vector3.Distance(_WeaponHolderCenter.position, TheS._ShieldEdgeSpot6.transform.position), 2);  //Bottom Right
                                                ds8 = Mathf.Pow(Vector3.Distance(_WeaponHolderCenter.position, TheS._ShieldEdgeSpot7.transform.position), 2);  //Right
                                                ds9 = Mathf.Pow(Vector3.Distance(_WeaponHolderCenter.position, TheS._ShieldEdgeSpot8.transform.position), 2);  //Left
                                            }
                                            if (((dh > ds1) || (dh > ds2) || (dh > ds3) || (dh > ds4) || (dh > ds5) || (dh > ds6) || (dh > ds7) || (dh > ds8) || (dh > ds9)) || (TheS._AdvancedShieldDetection == false))
                                            {
                                                //飞行道具一律不需要开启_AdvancedShieldDetection功能，因为这个功能本身针对的就是近距离一方对另一方发起大幅度动作的挥舞攻击时攻击区域太广阔所造成的防御失灵，
                                                //就比如说一个弧形攻击，打中盾牌的瞬间这个弧形攻击动作还是会继续，那攻击点由于动画问题穿透了对方的盾牌打到对方身体话，就和我们对攻击防御演出的认识相违背了。
                                                _Shields_Hit.Add(TheS.transform);
                                                HitShield = true;
                                                _Used_Targets.Add(TheS._ownerFightAttriCalReference.transform);//这一行与接下来含（* *）的两行紧密对应(不管打中盾牌的主人是谁，主人都因为受盾牌保护而不会收到攻击了)

                                                _ShiledHitPositions.Add(BallDetectHitPool[hit_target_index].ClosestPoint(_markers[i].transform.position));//ClosestPointOnBounds
                                                if (_ShiledHitPositions.Count > 0)
                                                    TheS.PassHitPointsFromWeaponToShiled(_ShiledHitPositions);//hit points on the shiled
                                                _ShiledHitPositions.Clear();
                                            }
                                        }
                                    }
                                }
                            }

                            for (int hit_target_index = 0; hit_target_index < BallDetectHitPool.Count; hit_target_index++)
                            {
                                if (SpecificTarget != SpecificTarget.flesh)
                                {
                                    if ((teamConfig.enemyWeaponLayerMask == (teamConfig.enemyWeaponLayerMask | (1 << BallDetectHitPool[hit_target_index].gameObject.layer)))
                                        && !_Used_Targets.Contains(BallDetectHitPool[hit_target_index].transform))
                                    {
                                        HitBoxesProcesser.ColliderHitBox.TryGetValue(BallDetectHitPool[hit_target_index], out BO_Marker_Manager hit_hitbox);
                                        if (hit_hitbox != null && hit_hitbox.Enabled)
                                        {
                                            _Used_Targets.Add(BallDetectHitPool[hit_target_index].transform);
                                            WeaponEnergyExaust(BallDetectHitPool[hit_target_index].transform.position, BallDetectHitPool[hit_target_index].transform.rotation);
                                            HitBoxLifeEnding = HitBoxLifeEnding.touched;
                                            if (weaponHP > 0 && CurrentHP <= 0)
                                            {
                                                break;
                                            }
                                            continue;
                                        }
                                    }
                                }
                                
                                if (SpecificTarget == SpecificTarget.energy)
                                {
                                    continue;
                                }

                                CalReference = BallDetectHitPool[hit_target_index].GetComponent<FightAttriCalReference>();
                                _BO_Hitbox = BallDetectHitPool[hit_target_index].GetComponent<BO_Limb>();
                                if (!_Targets_Raw_Hit.Contains(BallDetectHitPool[hit_target_index].transform) && !_Used_Targets.Contains(BallDetectHitPool[hit_target_index].transform))
                                {
                                    //方式1：mainhealth所在层级有collider //注意看这行条件，主要就是考虑到防御问题  （* *）
                                    //if (_BO_Health != null && _Used_Targets.Contains(BallDetectHitPool[hit_target_index].transform) == false)
                                    //{
                                    //    if (_BO_Health.collider_on_health)
                                    //    {
                                    //        HitFlesh = true;
                                    //        _Raw_Target_Instance = _BO_Health;
                                    //    }
                                    //}
                                    //方式2：hitbox模式
                                    if (_BO_Hitbox != null)
                                    {
                                        if (!_Used_Targets.Contains(_BO_Hitbox.MainHealth.transform)) //注意看这行条件，主要就是考虑到防御问题 （* *）
                                        {
                                            HitFlesh = true;
                                            _Raw_Target_Instance = _BO_Hitbox.MainHealth;//从上往下看，其实这一段表达的意思是一轮攻击只对一个main——health造成伤害
                                            _Used_Targets.Add(_BO_Hitbox.transform);
                                        }
                                    }

                                    if (_Raw_Target_Instance != null)
                                    {
                                        _Targets_Raw_Hit.Add(_Raw_Target_Instance.transform);
                                        _StartPoint = ((BO_Marker)_markers[i]).HitPointCal(BallDetectHitPool[hit_target_index].transform.position);
                                        //_StartPoint = BallDetectHitPool[hit_target_index].ClosestPointOnBounds(_markers[i].transform.position);
                                        //这个地方不能用ClosestPoint。这里存在unity官方bug。
                                        //_StartPoint = _StartPoint + (BallDetectHitPool[hit_target_index].transform.position - _StartPoint) * 0.3f;
                                        //为了打击特效看起来更接近肉体 向里切一下。
                                        // 以下是几种 _StartPoint的其他算法
                                        // 1.
                                        // Vector3 fromMarkerToHit = BallDetectHitPool[hit_target_index].transform.position - _markers[i].transform.position;
                                        // _StartPoint = _markers[i].transform.position + fromMarkerToHit / 2;//我是觉得离攻击体近更稳健些
                                        // _StartPoint = BallDetectHitPool[hit_target_index].transform.position;
                                        // 2.
                                        // _StartPoint = _Raw_Target_Instance.getHealthBodyCenterTransform().position;// TEST
                                        // 如果计算的某个点和collider的closetPoint，这个collider在场景里和其他collider有位置上的重合，那这个函数会出错
                                        hitsOnHealthBody.Add(new V_Damage(this, _markers[i],_Raw_Target_Instance, _MyOwnerCalReference,_StartPoint, Quaternion.LookRotation(_Raw_Target_Instance.transform.position - _StartPoint,Vector3.up)));
                                        WeaponEnergyExaust(BallDetectHitPool[hit_target_index].transform.position, BallDetectHitPool[hit_target_index].transform.rotation);
                                        HitBoxLifeEnding = HitBoxLifeEnding.touched;
                                    }
                                    if (HitFlesh && _Raw_Target_Instance != null)
                                    {
                                        if (_Raw_Target_Instance.GetShield() != null)
                                        {
                                            _Used_Targets.Add(_Raw_Target_Instance.GetShield().transform);
                                            //一把武器一轮enablemarkers和disablemarkers之间只可能对一个敌人进行一次伤害或进行一次“被防御”，敌人不可能在一把武器的一轮攻击期间内既受伤一次又防御成功一次
                                            //因此如果一轮攻击内敌人受伤了，也就再不用研究他能不能防御住所受攻击了。
                                        }
                                    }
                                }
                                if (weaponHP > 0 && CurrentHP <= 0)
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
                _Raw_Target_Instance = null;
            }
            _Targets_Raw_Hit.Clear();//这个列表的各种contains判断是为了防止一个武器单位的多个markers重复打中健康体。一个marker一帧内不会判断击中同一个健康体两次
        }

        // 3月24日新企划，围绕ContinuousDamage以及能量类攻击的新改动
        // 我们把原先的isHitDestroyEnergy参数给去掉，取而代之换成一个weaponhealth的int参数，如果这个参数大于0，系统会考虑其撞击毁灭的问题。
        // 而这个参数将和ContinuousDamage形成一个相互权衡的关系。如果武器不是ContinuousDamage，则一个能量系武器在打击到对象后应该立刻hp-1，并且直接cleartargets。
        // 直到hp为0时自身消灭。这样比如一个hp为2的波动技能就形成了一个类似kof99中boss那样的2连击飞行道具，这个道具打到人身上基本是形成一个很快的2连击。
        // 而如果这个武器是ContinuousDamage，事情将另当别论。ContinuousDamage类武器的cleartargets周期应该符合ContinuousDamageInterval。
        // 它在攻击到一个对象后不会立刻随着自身hp的减少而cleartargets，但如果它有着大于0的hp，它依然会随着打击到对象而掉血，并随着寿命结束而消失
        // 设想有一个地上火焰技能是ContinuousDamage，它可能有两种消失方式，一种是打击了不少对象hp为0了，一种是随着自身BO_destroyer的设置而时间已经尽。        
        // WeaponEnergyExaust 这个函数在“与敌人武器发生接触”和“与敌人肉体产生接触”的时候是不同的处理逻辑，这个可以更细致分析来探讨有没有其他方式处理
        void WeaponEnergyExaust(Vector3 Pos, Quaternion Qua)
        {
            if (weaponHP > 0)
            {
                CurrentHP -= 1;
                EffectAndHurtObjectLoading.Instance.GenerateEffect(ExplosionEffect, FightGlobalSetting.EffectPathDefine(zokusei), Pos, Qua, null);
            }else{
                if (_WeaponMode == WeaponMode.EnergyFromBodyWeapon)
                {
                    _MyOwnerCalReference._Center.Animation_Manger.FrameFreeze();
                }
            }
        }
    }
}