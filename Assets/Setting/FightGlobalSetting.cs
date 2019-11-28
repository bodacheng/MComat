using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightGlobalSetting : MonoBehaviour
{
    public AnimationCurve knockOffyAnimationCurve;
    public AnimationCurve knockOffzAnimationCurve;
    
    public float lighthit_lastingtime = 0.4f, heavyhit_lastingtime = 0.6f;
    public float knockoffextent = 5f;
    public float MaxKnockoffLaidGroundTime = 2f;
    public float CanGetUpAfterKnockoffToGround = 0.5f;
    public float GetupTime = 1f;
    public float LeastCommandTimeAfterGetup = 0.3f;
    public int defendHP = 20;
    public float lightBlockLastingTime = 0.3f, heavyBlockLastingTime = 0.5f;
    public float attackDrawingDistance = 1f;
    
    public static int scenestep = 0;//0 :mainmenu 1: fightscene
    public static float _lighthit_lastingtime, _heavyhit_lastingtime;
    public static float _knockoffextent;
    public static float _MaxKnockoffLaidGroundTime;
    public static float _CanGetUpAfterKnockoffToGround;
    public static float _GetupTime;
    public static float _LeastCommandTimeAfterGetup;
    public static int _defendHP;
    public static float _lightBlockLastingTime, _heavyBlockLastingTime;
    public static AnimationCurve _knockOffyAnimationCurve,_knockOffzAnimationCurve;
    public static float _attackDrawingDistance;
    
    void Awake()
    {
        _lighthit_lastingtime = lighthit_lastingtime;
        _heavyhit_lastingtime = heavyhit_lastingtime;
        _knockoffextent = knockoffextent;
        _CanGetUpAfterKnockoffToGround = CanGetUpAfterKnockoffToGround;
        _MaxKnockoffLaidGroundTime = MaxKnockoffLaidGroundTime;
        _GetupTime = GetupTime;
        _LeastCommandTimeAfterGetup = LeastCommandTimeAfterGetup;
        
        _knockOffyAnimationCurve = knockOffyAnimationCurve;
        _knockOffzAnimationCurve = knockOffzAnimationCurve;

        _defendHP = defendHP;
        _lighthit_lastingtime = lighthit_lastingtime;
        _heavyhit_lastingtime = heavyhit_lastingtime;

        _attackDrawingDistance = attackDrawingDistance;
    }
}
