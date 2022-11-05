using UnityEngine;
using HittingDetection;
using UniRx;
using DG.Tweening;

namespace Soul
{
    public partial class Hurt_State : Behavior
    {
        void PushToMidStart(V_Damage newValue, float dis, bool Grounded)
        {
            used_dizzy_time = FightGlobalSetting.HeavyHitLastingTime;
            Vector3 MidDistanceFromMe = newValue.attacker.Center.geometryCenter.transform.position + newValue.attacker.Center.WholeT.transform.forward * dis;
            if (Grounded)
            {
                PlayHurtAnim(newValue);
                MidDistanceFromMe.y = 0;
            }
            else
            {
                Animation_Manger.AnimationTrigger(Animation_Manger.GetRandomKnockOffAnim(), true, 0.1f);
            }
            tween = gameObject.transform.DOMove(MidDistanceFromMe, 0.3f);
            physicMissionDisposable = new SingleAssignmentDisposable();
            physicMissionDisposable.Disposable = Observable.EveryUpdate().Subscribe(_ =>
                {
                    if (gameObject == null)
                    {
                        physicMissionDisposable.Dispose();
                        return;
                    }
                    
                    if (Vector3.Distance(MidDistanceFromMe, gameObject.transform.position) < 0.3f || _BasicPhysicSupport.AtRing)
                    {
                        tween.Kill(false);
                        _Rigidbody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
                        physicMissionDisposable.Dispose();
                    }
                }
            ).AddTo(gameObject);
        }
    }
}