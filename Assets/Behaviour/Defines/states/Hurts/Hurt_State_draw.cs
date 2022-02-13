using UnityEngine;
using HittingDetection;
using DG.Tweening;

namespace Soul
{
    public partial class Hurt_State : Behavior
    {
        void DrawDamgeStart(V_Damage newValue)
        {
            used_dizzy_time = FightGlobalSetting._heavyhit_lastingtime;
            Vector3 vector3 = newValue.from_weapon_marker.transform.position;
            vector3.y = gameObject.transform.position.y;
            gameObject.transform.DOMove(vector3, 0.1f).OnComplete(() =>
                 {
                     _Rigidbody.velocity = Vector3.zero;
                 });
        }
    }
}