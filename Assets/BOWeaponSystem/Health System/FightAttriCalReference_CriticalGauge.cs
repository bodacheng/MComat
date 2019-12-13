using UnityEngine;

public partial class FightAttriCalReference : MonoBehaviour
{
    int critical_gauge = 100;//和skillcancelflag一样与角色的技能发动条件息息相关，所以放在这里
    bool gauge_collecting = true;
    public int CriticalGauge
    {
        get => critical_gauge;
        set => critical_gauge = Mathf.Clamp(value, 0, 100);//就是说角色最大ex槽最大100呗。
    }
    public void PlusCriticalGauge(int Gauge)
    {
        if (Gauge > 0 && !gauge_collecting)
            return;
        CriticalGauge = CriticalGauge + Gauge;
    }
    public void CostCriticalGaugeBySPlevel(int level)
    {
        switch (level)
        {
            case 0:
                SetGaugeCollecting(true);
                break;
            case 1:
                PlusCriticalGauge(-10);
                SetGaugeCollecting(false);
                break;
            case 2:
                PlusCriticalGauge(-20);
                SetGaugeCollecting(false);
                break;
            case 3:
                PlusCriticalGauge(-50);
                SetGaugeCollecting(false);
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
                if (CriticalGauge >= 10)
                    return true;
                break;
            case 2:
                if (CriticalGauge >= 20)
                    return true;
                break;
            case 3:
                if (CriticalGauge >= 50)
                    return true;
                break;
            case -1:
                return true;
        }
        return false;
    }

    public void SetGaugeCollecting(bool a)
    {
        gauge_collecting = a;
    }
    public bool IfGaugeCollecting()
    {
        return gauge_collecting;
    }
}
