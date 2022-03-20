using UnityEngine;
using UniRx;

public partial class FightParamsReference
{
    public ReactiveProperty<int> CriticalGauge { get; set; } = new ReactiveProperty<int>();
        
    public void PlusEx(int add)
    {
        CriticalGauge.Value = Mathf.Clamp(CriticalGauge.Value + add, 0, FightGlobalSetting._EXMax);
    }
    
    public void CostCriticalGaugeBySPLevel(int level)
    {
        if (CriticalGaugeMode == CriticalGaugeMode.Unlimited)
            return;
        switch (level)
        {
            case 0:
            break;
            case 1:
                PlusEx(-30);
            break;
            case 2:
                PlusEx(-60);
            break;
            case 3:
                PlusEx(-90);
            break;
        }
    }
    
    public bool HasPlentyGauge(int spLevel)
    {
        switch (spLevel)
        {
            case 0:
                return true;
            case 1:
                if (CriticalGauge.Value >= 30)
                    return true;
                break;
            case 2:
                if (CriticalGauge.Value >= 60)
                    return true;
                break;
            case 3:
                if (CriticalGauge.Value >= 90)
                    return true;
                break;
            case -1:
                return true;
        }
        return false;
    }
}
