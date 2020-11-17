using UnityEngine;
using Soul;

public partial class Hurt_State : Behavior
{
    Color stone = new Color(0.3f, 0.3f, 0.3f);
    Color freeze = new Color(0.1f, 0.1f, 0.8f);
    void SekkaStart(Zokusei zokusei)
    {
        pasuestart = () =>
        {
            _BuffsRunner.Freesing = true;
            Animation_Manger.Speed = 0;
            _Rigidbody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
            shaderManager.FlatColor(0.5f, gold);
            _FightAttriCalRef.GetKnockOffCount().PlusGauge(3f);
            _FightAttriCalRef.GetKnockOffCount().PlusTimeCounter(0.2f);
        };
        pasueend = () =>
        {
            Animation_Manger.Speed = 1;
            shaderManager.FlatColor(0, Color.white);
            _Rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
            _BuffsRunner.Freesing = false;
        };
        pasueCoroutine = new CustomCoroutine(pasuestart, FightGlobalSetting._superhit_lastingtime * 2, pasueend);
        _BuffsRunner.RunSubCoroutineOfState(pasueCoroutine);
        
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