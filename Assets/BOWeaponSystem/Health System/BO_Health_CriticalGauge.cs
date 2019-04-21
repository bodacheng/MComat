using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class BO_Health : MonoBehaviour
{
    private int critical_gauge = 100;//和skillcancelflag一样与角色的技能发动条件息息相关，所以放在这里
    private bool gauge_collecting = true;
    public int CriticalGauge
    {
        get
        {
            //Some other code
            return critical_gauge;
        }
        set
        {
            //Some other code
            critical_gauge = Mathf.Clamp(value, 0, 100);//就是说角色最大ex槽最大100呗。
        }
    }
    public void plusCriticalGauge(int Gauge)
    {
        if (Gauge > 0 && !gauge_collecting)
            return;
        CriticalGauge = CriticalGauge + Gauge;
    }
    public void costCriticalGaugeBySPlevel(EX level)
    {
        switch (level)
        {
            case EX.normal:
                setGaugeCollecting(true);
                break;
            case EX.EX1:
                plusCriticalGauge(-10);
                setGaugeCollecting(false);
                break;
            case EX.EX2:
                plusCriticalGauge(-20);
                setGaugeCollecting(false);
                break;
            case EX.EX3:
                plusCriticalGauge(-30);
                setGaugeCollecting(false);
                break;
        }
    }

    public bool hasPlentyGauge(EX splevel)
    {
        switch (splevel)
        {
            case EX.normal:
                return true;
            case EX.EX1:
                if (CriticalGauge >= 10)
                    return true;
                break;
            case EX.EX2:
                if (CriticalGauge >= 20)
                    return true;
                break;
            case EX.EX3:
                if (CriticalGauge >= 30)
                    return true;
                break;
            case EX.NULL:
                return true;
        }
        return false;
    }

    public void setGaugeCollecting(bool a)
    {
        gauge_collecting = a;
    }
    public bool ifGaugeCollecting()
    {
        return gauge_collecting;
    }
}
