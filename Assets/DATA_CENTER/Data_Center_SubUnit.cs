using System;
using HittingDetection;
using MCombat.Shared.Combat;

public partial class Data_Center : ISubUnitSwitchHandler<V_Damage>
{
    bool _changedToSubUnit;
    Func<string, V_Damage, bool> _changeToSub;

    public bool IsSubUnit => SubUnitUtility.IsSubUnitId(UnitInfo?.id);
    public bool HasChangedToSubUnit => _changedToSubUnit;
    public bool CanUseMainUnitDeathFlow => !IsSubUnit && !_changedToSubUnit;

    public void AssignSubUnitSwitcher(Func<string, V_Damage, bool> switcher)
    {
        _changeToSub = switcher;
    }

    public void MarkChangedToSubUnit()
    {
        _changedToSubUnit = true;
    }

    public void ResetSubUnitState()
    {
        _changedToSubUnit = false;
        _changeToSub = null;
    }

    public bool TryChangeToSub(string stateKey, V_Damage damage)
    {
        if (FightDataRef.CurrentHp.Value >= FightDataRef.MaxHp * CommonSetting.ChangeToSubHpPercent)
        {
            return false;
        }

        return _changeToSub != null
               && !_changedToSubUnit
               && _changeToSub.Invoke(stateKey, damage);
    }
}
