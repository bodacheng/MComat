using UnityEngine;
using HittingDetection;
using Soul;

public partial class Hurt_State : Behavior
{
    bool dropped;
    Vector3 _xz;
    
    void HighDamgeStart(V_Damage newValue)
    {
        dropped = false;
        _Animator.SetFloat("speed", 0f);
        _Animator.applyRootMotion = false;
        _Rigidbody.velocity = Vector3.zero;
        _BasicPhysicSupport.SetUsingGravity(false);
        used_dizzy_time = FightGlobalSetting._highhit_lastingTime;
        _xz = newValue.attacker._Center.WholeT.forward;
        _FightAttriCalRef.GetKnockOffCount().PlusTimeCounter(0.2f);
        Animation_Manger.AnimationTrigger(Animation_Manger.GetRandomKnockOffAnim(),true,0.05f);
    }
    
    void HighDamageUpdate()
    {
        if (!dropped)
        {
            if (TimeCounter > 0.1f && _BasicPhysicSupport.hiddenMethods.Grounded)
            {
                dropped = true;
                _Rigidbody.velocity = Vector3.zero;
            }else{
                gameObject.transform.position += 
                _xz * (FightGlobalSetting._HdamageZAnimationCurve.Evaluate(TimeCounter + Time.fixedDeltaTime) - FightGlobalSetting._HdamageZAnimationCurve.Evaluate(TimeCounter)) +
                Vector3.up * (FightGlobalSetting._HdamageYAnimationCurve.Evaluate(TimeCounter + Time.fixedDeltaTime) - FightGlobalSetting._HdamageYAnimationCurve.Evaluate(TimeCounter));
            }
        }
    }
}
