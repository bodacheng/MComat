using UnityEngine;
using HittingDetection;

public partial class Decompositioner : MonoBehaviour
{    
    public void Life()
    {
        if (Phase == 1 && _HitBox != null)
        {
            switch (_HitBox._WeaponMode)
            {
                case WeaponMode.EnergyFromBodyWeapon:
                    if (_HitBox.weaponHP > 0 && _HitBox.CurrentHP <= 0)
                    {
                        CloseMarkers();
                        StopEmissions(false);
                        Counter = stop_emission_delay;
                        Phase = 2;
                    }
                    if (_HitBox.GetOwnerFightAttriCalReference() != null)
                    {
                        if (_HitBox.GetOwnerFightAttriCalReference().IFgettingDamage())
                        {
                            CloseMarkers();
                            StopEmissions(false);
                            Counter = stop_emission_delay;
                            Phase = 2;
                        }
                    }
                break;
                case WeaponMode.FlyerWeapon:
                    if (_HitBox.weaponHP > 0 && _HitBox.CurrentHP <= 0)
                    {
                        CloseMarkers();
                        StopEmissions(false);
                        Counter = stop_emission_delay;
                        Phase = 2;
                    }
                break;
            }
        }
        
        switch (Phase)
        {
            case 1:
                if (DestructionDelay > 0 && stop_emission_delay > 0)//如果能量自身有寿命
                {
                    if (Counter > stop_emission_delay)
                    {
                        CloseMarkers();
                        StopEmissions(false);
                        Phase = 2;
                    }
                }
            break;
            case 2:
                if (DestructionDelay > 0 && stop_emission_delay > 0)//如果能量自身有寿命
                {
                    if (DestructionDelay > stop_emission_delay)
                    {
                        SetMaterialsAlpha((DestructionDelay - Counter) / (DestructionDelay - stop_emission_delay));
                    }
                }
                if (Counter > DestructionDelay || DestructionDelay <= 0)//DestructionDelay <= 0 代表这个物件没有生存寿命
                {
                    Phase = -1;
                }
            break;
            case -1: // -1是立刻归还对象池的flag。这个逻辑是让所有hitbox按序运行的重要一环。
                EnergyRessolve();
                break;
        }
        
        if (gameObject.activeSelf)
        {
            Counter += Time.deltaTime;
        }
    }
}
