using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knockOffCount
{
    float knock_off_time_counter;
    float knock_off_cooldown_infer;
    float knock_off_gauge;

    public knockOffCount()
    {
        knock_off_time_counter = 1f;
        knock_off_cooldown_infer = 1f;
        knock_off_gauge = 0;
    }

    public void setGauge(float amount)
    {
        this.knock_off_gauge = amount;
    }

    public void plusGauge(float amount)
    {
        knock_off_gauge += amount;
    }

    public void plusTimeCounter(float timeamout)
    {
        this.knock_off_time_counter += timeamout;
    }

    public float getGauge()
    {
        return knock_off_gauge;
    }

    public void update()
    {
        //底下这些就是说，我每隔一段时间就让KockoFF槽冷却一些(降低2)
        if (knock_off_gauge > 0)
        {
            if (knock_off_time_counter > 0)
            {
                knock_off_time_counter -= Time.fixedDeltaTime;
            }
            if (knock_off_time_counter <= 0)
            {
                knock_off_gauge = Mathf.Clamp(knock_off_gauge - 2f, 0, Mathf.Infinity);
                knock_off_time_counter = knock_off_cooldown_infer;
            }
        }
    }
}

public class BeHitCount
{
    int beHitCount;
    float HitConnectTolerate;
    float BeHItComboTimeCounter;

    public BeHitCount()
    {
        beHitCount = 0;
        HitConnectTolerate = 1.5f;
        BeHItComboTimeCounter = 0f;
    }

    public void BeHitCountPlus()
    {
        BeHItComboTimeCounter = HitConnectTolerate;
        beHitCount += 1;
    }

    public void BeHitCountInterrupt()
    {
        beHitCount = 0;
        BeHItComboTimeCounter = 0;
    }

    public int getBeHitCount()
    {
        return beHitCount;
    }

    public void update()
    {
        if (BeHItComboTimeCounter > 0f)
        {
            BeHItComboTimeCounter -= Time.fixedDeltaTime;
            if (BeHItComboTimeCounter <= 0f)
            {
                beHitCount = 0;
            }
        }
    }
}

public class ComboHitCount
{
    int hitCount;
    float HitConnectTolerate;
    float HItComboTimeCounter;

    public ComboHitCount()
    {
        hitCount = 0;
        HitConnectTolerate = 4f;
        HItComboTimeCounter = 0f;
    }

    public void HitCountPlus()
    {
        HItComboTimeCounter = HitConnectTolerate;
        hitCount += 1;
    }

    public void HitCountInterrupt()
    {
        hitCount = 0;
        HItComboTimeCounter = 0;
    }

    public int getHitCount()
    {
        return hitCount;
    }

    public void update()
    {
        if (HItComboTimeCounter > 0f)
        {
            HItComboTimeCounter -= Time.fixedDeltaTime;
            if (HItComboTimeCounter <= 0f)
            {
                hitCount = 0;
            }
        }
    }
}

public enum weight : int
{
    normal = 1,
    light = 2,
    heavy = 3
}
