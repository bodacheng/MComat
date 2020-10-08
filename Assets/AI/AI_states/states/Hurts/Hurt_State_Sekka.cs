using UnityEngine;
using Soul;

public partial class Hurt_State : Behavior
{
    Color stone = new Color(0.3f, 0.3f, 0.3f);
    Color freeze = new Color(0.1f, 0.1f, 0.8f);
    void SekkaStart(Zokusei zokusei)
    {
        _Animator.speed = 0;
        used_dizzy_time = FightGlobalSetting._superhit_lastingtime * 2;
        _Rigidbody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
        _FightAttriCalRef.GetKnockOffCount().PlusGauge(3f);
        _FightAttriCalRef.GetKnockOffCount().PlusTimeCounter(0.2f);
        
        switch(zokusei)
        {
            case Zokusei.blueMagic:
            case Zokusei.lightMagic:
                shaderManager.FlatColor(0.5f, freeze);
                break;
            default:
                shaderManager.FlatColor(0.8f, stone);
                break;
        }
    }
}