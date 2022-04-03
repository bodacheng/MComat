using UnityEngine;

namespace Soul
{
    public partial class Hurt_State : Behavior
    {
        Color stone = new Color(0.3f, 0.3f, 0.3f);
        Color freeze = new Color(0.1f, 0.1f, 0.8f);
        void SekkaStart(Element element)
        {
            pasuestart = () =>
            {
                _BuffsRunner.Freesing = true;
                Animation_Manger.Speed = 0;
                _Rigidbody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
            };
            pasueend = () =>
            {
                Animation_Manger.Speed = 1;
                shaderManager.FlatColor(Color.white, 0);
                _Rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
                _BuffsRunner.Freesing = false;
            };
            pasueCoroutine = new CustomCoroutine(
                pasuestart,
                FightGlobalSetting._superhit_lastingtime * 2,
                () =>
                {
                // 被其他种类攻击打一下接着石化就中止
                return this.target.from_weapon.damage_type != HittingDetection.DamageType.sekka;
                },
                pasueend);
            _BuffsRunner.RunSubCoroutineOfState(pasueCoroutine);

            switch (element)
            {
                case Element.blueMagic:
                case Element.lightMagic:
                    shaderManager.FlatColor(freeze, 0.5f);
                    break;
                default:
                    shaderManager.FlatColor(stone, 0.5f);
                    break;
            }
        }
    }
}