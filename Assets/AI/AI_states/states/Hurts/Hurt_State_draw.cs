using UnityEngine;
using HittingDetection;
using Soul;
using UniRx;
using DG.Tweening;

public partial class Hurt_State : Behavior
{
    void DrawDamgeStart(V_Damage newValue)
    {
        Animation_Manger.AnimationTrigger(Animation_Manger.GetRandomHurtAnim(),true,0.05f);
        used_dizzy_time = FightGlobalSetting._heavyhit_lastingtime;
        Vector3 vector3 = newValue.from_weapon_marker.transform.position;
        vector3.y = gameObject.transform.position.y;
        gameObject.transform.DOMove(vector3,0.1f).
        OnComplete(() =>{_Rigidbody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;});
        _FightAttriCalRef.GetKnockOffCount().PlusGauge(3f);
        _FightAttriCalRef.GetKnockOffCount().PlusTimeCounter(0.2f);
    }
}
