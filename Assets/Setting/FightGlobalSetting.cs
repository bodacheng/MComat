using UnityEngine;

public class FightGlobalSetting : MonoBehaviour
{
    public ProgramMode programMode = ProgramMode.normal;
    public bool hasDefend;
    public AnimationCurve knockOffyAnimationCurve;
    public AnimationCurve knockOffzAnimationCurve;

    public AnimationCurve HdamageYAnimationCurve;
    public AnimationCurve HdamageZAnimationCurve;

    public float AT_coefficient = 1;
    public float HP_coefficient = 1;
    public float GetExAfterDamageBy = 10f;
    public float ExGetAfterDamage = 10f;
    public float lighthit_lastingtime = 0.4f, heavyhit_lastingtime = 0.6f, highhit_lastingTime = 0.8f;
    public float normalattackpositionfixingtime = 0.1f;
    public float knockoffextent = 20f;
    public float MaxKnockoffLaidGroundTime = 2f;
    public float CanGetUpAfterKnockoffToGround = 0.5f;
    public float GetupTime = 1f;
    public int defendHP = 20;
    public float lightBlockLastingTime = 0.3f, heavyBlockLastingTime = 0.5f;
    public float attackDrawingDistance = 1f;

    public int HurtObjectPreLoadCount = 5;
    
    public static ProgramMode _programMode;
    public static int scenestep;//0 :mainmenu 1: fightscene
    public static bool _hasDefend;
    public static float _AT_coefficient = 1;
    public static float _HP_coefficient = 1;
    public static float _GetExAfterDamageBy;
    public static float _ExGetAfterDamage;
    public static float _lighthit_lastingtime, _heavyhit_lastingtime;
    public static float _knockoffextent;
    public static float _MaxKnockoffLaidGroundTime;
    public static float _CanGetUpAfterKnockoffToGround;
    public static float _GetupTime;
    public static int _defendHP;
    public static float _lightBlockLastingTime, _heavyBlockLastingTime, _highhit_lastingTime;
    public static float _normalattackpositionfixingtime;
    public static AnimationCurve _knockOffyAnimationCurve, _knockOffzAnimationCurve;
    public static AnimationCurve _HdamageYAnimationCurve;
    public static AnimationCurve _HdamageZAnimationCurve;
    public static float _attackDrawingDistance;
    public static bool HitBoxLogger = true;
    
    public static int _HurtObjectPreLoadCount;

    public static string EffectPathDefine(Zokusei zokusei)
    {
        string personalEffectPath;
        switch (zokusei)
        {
            case Zokusei.blueMagic:
                personalEffectPath = "bluemagic";
                break;
            case Zokusei.redMagic:
                personalEffectPath = "redmagic";
                break;
            case Zokusei.greenMagic:
                personalEffectPath = "greenmagic";
                break;
            case Zokusei.lightMagic:
                personalEffectPath = "lightmagic";
                break;
            case Zokusei.darkMagic:
                personalEffectPath = "darkmagic";
                break;
            case Zokusei.Null:
                personalEffectPath = "defaultmagic";
                break;
            default:
                personalEffectPath = "defaultmagic";
                break;
        }
        return personalEffectPath;
    }

    public enum ProgramMode
    {
        normal = 0,
        skillShow = 1
    }
    
    void Awake()
    {
        _programMode = programMode;

        _HurtObjectPreLoadCount = HurtObjectPreLoadCount;
        
        _hasDefend = hasDefend;
        
        _AT_coefficient = AT_coefficient;
        _HP_coefficient = HP_coefficient;

        _GetExAfterDamageBy = GetExAfterDamageBy;
        _ExGetAfterDamage = ExGetAfterDamage;
        
        _lighthit_lastingtime = lighthit_lastingtime;
        _heavyhit_lastingtime = heavyhit_lastingtime;
        _highhit_lastingTime = highhit_lastingTime;
        _normalattackpositionfixingtime = normalattackpositionfixingtime;
        
        _knockoffextent = knockoffextent;
        _CanGetUpAfterKnockoffToGround = CanGetUpAfterKnockoffToGround;
        _MaxKnockoffLaidGroundTime = MaxKnockoffLaidGroundTime;
        _GetupTime = GetupTime;
        
        _knockOffyAnimationCurve = knockOffyAnimationCurve;
        _knockOffzAnimationCurve = knockOffzAnimationCurve;

        _HdamageYAnimationCurve = HdamageYAnimationCurve;
        _HdamageZAnimationCurve = HdamageZAnimationCurve;

        _defendHP = defendHP;
        _lightBlockLastingTime = lightBlockLastingTime;
        _heavyBlockLastingTime = heavyBlockLastingTime;

        _attackDrawingDistance = attackDrawingDistance;
    }
}
