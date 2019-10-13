using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personality_events : MonoBehaviour
{
    [Header("剑")]
    [Space(1)]
    public ParticleSystem right_sword,left_sword;

    void Start()
    {
        // 在第一级初始化中我们把两个角色武器先打开，又关闭，这起到了个非常邪门的效果：使得这两把武器的相关awake函数得以运行，在这里就是找到了相应武器的markers
        if (right_sword != null)
        {
            right_sword.Stop(true);
        }
        if (left_sword != null)
        {
            left_sword.Stop(true);
        }
    }
    
    public void CloseAllPersonalityEffects()
    {
        turn_off_Right_energy_blade();
        turn_off_Left_energy_blade();
    }

    public void turn_on_Right_energy_blade()
    {
        turnRightEnergyBlade(true);
    }
    public void turn_off_Right_energy_blade()
    {
        turnRightEnergyBlade(false);
    }
    private void turnRightEnergyBlade(bool _on)
    {
        if (right_sword != null)
        {
            if (_on)
                right_sword.Play(true);
            else
                right_sword.Stop(true);
        }
    }
    public void turn_on_Left_energy_blade()
    {
        turnLeftEnergyBlade(true);
    }
    public void turn_off_Left_energy_blade()
    {
        turnLeftEnergyBlade(false);
    }
    private void turnLeftEnergyBlade(bool _on)
    {
        if (left_sword != null)
        {
            if (_on)
                left_sword.Play(true);
            else
                left_sword.Stop(true);
        }
    }
}
