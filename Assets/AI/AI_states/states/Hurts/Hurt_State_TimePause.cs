using UnityEngine;

namespace Soul
{
    public partial class Hurt_State : Behavior
    {
        UnityEngine.Events.UnityAction pasuestart;
        UnityEngine.Events.UnityAction pasueend;
        CustomCoroutine pasueCoroutine;
        Color gold = new Color(1f, 1, 0.2f);

        void TimePauseStart()
        {
            pasuestart = () =>
            {
                _BuffsRunner.Freesing = true;
                Animation_Manger.Speed = 0;
                _Rigidbody.mass = 1000f;
                _Rigidbody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
                shaderManager.FlatColor(0.5f, gold);
            };
            pasueend = () =>
            {
                Animation_Manger.Speed = 1;
                shaderManager.FlatColor(0, Color.white);
                _Rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
                _BuffsRunner.Freesing = false;
            };
            pasueCoroutine = new CustomCoroutine(pasuestart, FightGlobalSetting._superhit_lastingtime * 3, pasueend);
            _BuffsRunner.RunSubCoroutineOfState(pasueCoroutine);
        }
    }
}