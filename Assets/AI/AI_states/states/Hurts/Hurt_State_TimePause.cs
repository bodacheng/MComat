using UnityEngine;
using Soul;

public partial class Hurt_State : Behavior
{
    Color gold = new Color(1f,1,0.2f);
    void TimePauseStart()
    {
        Animation_Manger.Speed = 0;
        used_dizzy_time = FightGlobalSetting._superhit_lastingtime * 2;
        _Rigidbody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
        _FightAttriCalRef.GetKnockOffCount().PlusGauge(3f);
        _FightAttriCalRef.GetKnockOffCount().PlusTimeCounter(0.2f);
        shaderManager.FlatColor(0.5f, gold);
    }
}