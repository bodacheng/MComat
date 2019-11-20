using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightGlobalSetting : MonoBehaviour
{
    public AnimationCurve knockOffyAnimationCurve;
    public AnimationCurve knockOffzAnimationCurve;
    
    public float lighthit_lastingtime = 0.4f, heavyhit_lastingtime = 0.6f;
    public float knockoffMaxtime = 2f;
    public int defendHP = 20;
    public float lightBlockLastingTime = 0.3f, heavyBlockLastingTime = 0.5f;
    
    public static int scenestep = 0;//0 :mainmenu 1: fightscene
    public static float _lighthit_lastingtime, _heavyhit_lastingtime;
    public static float _knockoffMaxtime;
    public static int _defendHP;
    public static float _lightBlockLastingTime, _heavyBlockLastingTime;
    public static AnimationCurve _knockOffyAnimationCurve,_knockOffzAnimationCurve;
    
    void Awake()
    {
        _lighthit_lastingtime = lighthit_lastingtime;
        _heavyhit_lastingtime = heavyhit_lastingtime;
        _knockoffMaxtime = knockoffMaxtime;
        
        _knockOffyAnimationCurve = knockOffyAnimationCurve;
        _knockOffzAnimationCurve = knockOffzAnimationCurve;

        _defendHP = defendHP;
        _lighthit_lastingtime = lighthit_lastingtime;
        _heavyhit_lastingtime = heavyhit_lastingtime;
    }
}
