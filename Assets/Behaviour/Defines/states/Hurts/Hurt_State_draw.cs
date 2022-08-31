using UnityEngine;
using HittingDetection;
using DG.Tweening;

namespace Soul
{
    public partial class Hurt_State : Behavior
    {
        void DrawDamageStart(V_Damage newValue)
        {
            used_dizzy_time = FightGlobalSetting._heavyhit_lastingtime;
            var vector3 = newValue.from_weapon_marker.transform.position;
            vector3.y = gameObject.transform.position.y;
            gameObject.transform.DOMove(vector3, FightGlobalSetting._normalattackpositionfixingtime).OnComplete(() =>
            {
                if (!_BasicPhysicSupport.hiddenMethods.Grounded)
                    _Rigidbody.velocity = Vector3.zero;
                else
                    _Rigidbody.constraints = RigidbodyConstraints.FreezeAll;
            });
        }
    }
}