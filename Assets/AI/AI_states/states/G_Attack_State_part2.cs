using UnityEngine;
using Soul;

public partial class G_Attack_State : Behavior
{
    public override void _State_FixedUpdate2()
    {
        switch (_phase)
        {
            case Phase.noRushState://其实现在压根不会进入。
                break;
            case Phase.farFromReach:
                //_Rigidbody.velocity = Vector3.zero;
                break;
            case Phase.needToRush:
                if (rushingToTarget != null)
                {
                    if (Vector3.Distance(gameObject.transform.position,rushingToTarget.position) < 2f)
                        _phase = Phase.reached;
                    Move(rushingToTarget.position - gameObject.transform.position, rushSpeed, true);
                    if (_phase == Phase.reached)
                    {
                        Animation_Manger.AnimationTrigger(clip_name,true,0.05f);
                        _SkillCancelFlag.TurnRotationAdjustmentStartFlag(1);
                        _Rigidbody.velocity = Vector3.zero;
                        Sensor.GetEnemiesByDistance(true);
                        _BuffsRunner.EndSubCoroutineOfState(rushCoroutine);
                        if (Sensor.GetEnemiesByDistance(false).Count > 0)
                        {
                            if (Sensor.GetEnemiesByDistance(false)[0] != null)
                            {
                                RotateToTarget_Tween(Sensor.GetEnemiesByDistance(false)[0].transform.position, 0.01f, true);
                            }
                        }
                    }
                }
                else
                {
                    _Rigidbody.velocity = Vector3.zero;
                }
                rush_time_counter += Time.fixedDeltaTime;
                break;
            case Phase.reached:
                if (Sensor.GetInnerEnemiesColliders().Count > 0)
                {
                    if (Sensor.GetInnerEnemiesColliders()[0] != null)
                        AttackApprocach(Sensor.GetInnerEnemiesColliders()[0].transform.position,approcahingSpeed);
                }
                break;
            case Phase.reachedFromThebeginning:
                if (Sensor.GetInnerEnemiesColliders().Count > 0)
                {
                    if (Sensor.GetInnerEnemiesColliders()[0] != null)
                        AttackApprocach(Sensor.GetInnerEnemiesColliders()[0].transform.position, approcahingSpeed);
                }
                break;
        }
    }
}
