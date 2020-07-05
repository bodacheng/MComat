using UnityEngine;
using UniRx;

public partial class FightAttriCalReference : MonoBehaviour
{
    public ReactiveProperty<float> CriticalGauge { get; set; } = new ReactiveProperty<float>();
    //{
    //    get => critical_gauge;
    //    set => critical_gauge = Mathf.Clamp(value, 0, 100);
    //}
    
    public void PlusEx(float add)
    {
        CriticalGauge.Value = Mathf.Clamp(CriticalGauge.Value + add, 0,100);
    }
    
    public void CostCriticalGaugeBySPlevel(int level)
    {
        switch (level)
        {
            case 0:
            break;
            case 1:
                PlusEx(-10);
            break;
            case 2:
                PlusEx(-30);
            break;
            case 3:
                PlusEx(-50);
            break;
        }
    }

    public bool HasPlentyGauge(int splevel)
    {
        switch (splevel)
        {
            case 0:
                return true;
            case 1:
                if (CriticalGauge.Value >= 10)
                    return true;
                break;
            case 2:
                if (CriticalGauge.Value >= 30)
                    return true;
                break;
            case 3:
                if (CriticalGauge.Value >= 50)
                    return true;
                break;
            case -1:
                return true;
        }
        return false;
    }
}
