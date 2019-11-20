using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Soul;

public partial class G_Attack_State : AI_State
{
    float ji;Vector3 use_direction;
    void SingleDirectionRotateProcess(Vector3 P,float speed)
    {
        //底下这个是说，攻击状态里角色在一个1f周期里有0.3f时长会调整方向，但是在这0.3f时间段里，如果产生了旋转不定向(比如已经转到目标)，那么转向就会提前结束。
        if (_SkillCancelFlag.hiddenMethods.GetRotationAdjustmentStartFlag())
        {
            thisFrameRotateAngle = this.RotateToTarget(P, 0.5f, true);
            ji = thisFrameRotateAngle * lastFrameRotateAngle;
            if (ji > 0)//同向
            {
                lastFrameRotateAngle = thisFrameRotateAngle;
            }
            else if (ji < 0)//反向
            {
                _SkillCancelFlag.TurnRotationAdjustmentStartFlag(0);
            }
            else //刚开始计
            {   
                lastFrameRotateAngle = thisFrameRotateAngle;
            }
        }
        else{
            lastFrameRotateAngle = 0;
            thisFrameRotateAngle = 0;
        }

        if (_SkillCancelFlag.hiddenMethods.GetAttackApproachingFlag())
        {
            use_direction = P - gameObject.transform.position;
            use_direction.y = 0;
            Move(use_direction, speed, true);
            if (_Pusher.hiddenMethods.ITouchedEnemyBody())
            {
                _SkillCancelFlag.hiddenMethods.SetAttackApproachingFlag(false);
            }
        }
    }

    public override void _State_FixedUpdate2()
    {
        switch (_phase)
        {
            case Phase.noRushState://其实现在压根不会进入。
                if (Sensor.getEnemiesByDistance(false).Count > 0)
                {
                    if (Sensor.getEnemiesByDistance(false)[0] != null)
                    {
                        SingleDirectionRotateProcess(Sensor.getEnemiesByDistance(false)[0].transform.position,approcahingSpeed);
                    }
                }
                break;
            case Phase.farFromReach:
                if (Sensor.getEnemiesByDistance(false).Count > 0)
                {
                    if (Sensor.getEnemiesByDistance(false)[0] != null)
                    {
                        SingleDirectionRotateProcess(Sensor.getEnemiesByDistance(false)[0].transform.position,approcahingSpeed);
                    }
                }
                //_Rigidbody.velocity = Vector3.zero;
                break;
            case Phase.needToRush:
                if (rushingToTarget != null)
                {
                    if (Vector3.Distance(gameObject.transform.position,rushingToTarget.position) < 2f)
                        _phase = Phase.reached;
                    thisFrameRotateAngle = this.RotateToTarget(rushingToTarget.position, 1f, true);

                    if (thisFrameRotateAngle > 60)//就是说 ，转角太大代表出问题了，很可能在自转
                    { _phase = Phase.reached;}
                    ji = thisFrameRotateAngle * lastFrameRotateAngle;
                    if (ji > 0)//同向
                    {
                        lastFrameRotateAngle = thisFrameRotateAngle;
                    }
                    else if (ji < 0)//反向
                    {
                        this._Rigidbody.velocity = Vector3.zero;
                        _phase = Phase.reached;
                    }
                    else
                    {   //刚开始计
                        lastFrameRotateAngle = thisFrameRotateAngle;
                    }
                    this.Move(rushingToTarget.position - gameObject.transform.position, rushSpeed, true);
                    if (_phase == Phase.reached)
                    {
                        Animation_Manger.AnimationTrigger(clip_name);
                        _SkillCancelFlag.TurnRotationAdjustmentStartFlag(1);
                        lastFrameRotateAngle = 0;
                        thisFrameRotateAngle = 0;
                        this._Rigidbody.velocity = Vector3.zero;
                        this.Sensor.OneRoundDetectionStart(5);
                        this._BuffsRunner.endSubCoroutineOfState(rushCoroutine);
                    }
                }
                else
                {
                    this._Rigidbody.velocity = Vector3.zero;
                }
                rush_time_counter += Time.fixedDeltaTime;
                break;
            case Phase.reached:
                if (Sensor.getInnerEnemiesColliders().Count > 0)
                {
                    if (Sensor.getInnerEnemiesColliders()[0] == null)
                        break;
                    //如果角色是在扭转自身方向至一个敌人的hitbox，
                    //而武器校准是在把对方推向自身的transform.forward线上，那这样下来如果对方的hitbox t.p和整体t.p相差比较远，则并不是一种把两个角色并到一条线的趋势。
                    //其实，如果一个敌人可以被位置校准，那其实是如上所述要同时转向这个敌人的TP并靠攻击将对方的TP连到自己的前方；
                    //而如果一个敌人不可以被位置校准，也是两方面，一来武器不需要对对方进行校准处理，另一方面转向不是完全转至敌人的TP而是根据情况转向敌人的HItbox TP。
                    SingleDirectionRotateProcess(Sensor.getInnerEnemiesColliders()[0].transform.position,approcahingSpeed);
                }
                break;
            case Phase.reachedFromThebeginning:
                if (Sensor.getInnerEnemiesColliders().Count > 0)
                {
                    if (Sensor.getInnerEnemiesColliders()[0] == null)
                        break;
                    SingleDirectionRotateProcess(Sensor.getInnerEnemiesColliders()[0].transform.position,approcahingSpeed);
                }
                else
                {
                    if (Sensor.getClosestColliderInSensorRange(false,true,true)!=null)//也就是说，在你本状态内不冲刺但是外环有敌人的情况下，如果发动了这个攻击状态，还是会有朝向和迈步。
                    {
                        SingleDirectionRotateProcess(Sensor.getClosestColliderInSensorRange(false,true,true).transform.position,approcahingSpeed);
                    }
                }
                break;
            default:
                break;
        }
    }
}
