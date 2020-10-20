using UnityEngine;
using HittingDetection;
using Soul;
using DG.Tweening;

public partial class Hurt_State : Behavior
{
    void DrawDamgeStart(V_Damage newValue)
    {
        PlayHurtAnim(newValue);
        used_dizzy_time = FightGlobalSetting._heavyhit_lastingtime;
        Vector3 vector3 = newValue.from_weapon_marker.transform.position;
        vector3.y = gameObject.transform.position.y;
        gameObject.transform.DOMove(vector3,0.1f).OnComplete(() => 
            {
                _Rigidbody.velocity = Vector3.zero;
            });
        _FightAttriCalRef.GetKnockOffCount().PlusGauge(3f);
        _FightAttriCalRef.GetKnockOffCount().PlusTimeCounter(0.2f);
    }
}
