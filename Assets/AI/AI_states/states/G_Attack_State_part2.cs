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
                        Sensor.OneRoundDetectionStart(5);
                        _BuffsRunner.EndSubCoroutineOfState(rushCoroutine);
                    }
                }
                else
                {
                    _Rigidbody.velocity = Vector3.zero;
                }
                rush_time_counter += Time.fixedDeltaTime;
                break;
            case Phase.reached:
                break;
            case Phase.reachedFromThebeginning:
                break;
        }
    }
}
