using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightGlobalSetting : MonoBehaviour
{
    public AnimationCurve knockOffyAnimationCurve;
    public AnimationCurve knockOffzAnimationCurve;
    public float lighthit_lastingtime = 0.4f, heavyhit_lastingtime = 0.6f;
    public float light_damage_force = 2f, heavy_damage_force = 8f;
    public float knockoffMaxtime = 2f;
    
    public static AnimationCurve _knockOffyAnimationCurve,_knockOffzAnimationCurve;
    public static float _lighthit_lastingtime, _heavyhit_lastingtime;
    public static float _light_damage_force, _heavy_damage_force;
    public static float _knockoffMaxtime;

    void Awake()
    {
        _knockOffyAnimationCurve = knockOffyAnimationCurve;
        _knockOffzAnimationCurve = knockOffzAnimationCurve;
        _lighthit_lastingtime = lighthit_lastingtime;
        _heavyhit_lastingtime = heavyhit_lastingtime;
        _knockoffMaxtime = knockoffMaxtime;
        
        _light_damage_force = light_damage_force;
        _heavy_damage_force = heavy_damage_force;        
    }
}
