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
    public void costCriticalGaugeBySPlevel(int level)
    {
        switch (level)
        {
            case 0:
                setGaugeCollecting(true);
                break;
            case 1:
                plusCriticalGauge(-10);
                setGaugeCollecting(false);
                break;
            case 2:
                plusCriticalGauge(-20);
                setGaugeCollecting(false);
                break;
            case 3:
                plusCriticalGauge(-50);
                setGaugeCollecting(false);
                break;
        }
    }

    public bool hasPlentyGauge(int splevel)
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

    public void setGaugeCollecting(bool a)
    {
        gauge_collecting = a;
    }
    public bool ifGaugeCollecting()
    {
        return gauge_collecting;
    }
}
