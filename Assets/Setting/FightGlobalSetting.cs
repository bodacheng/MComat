using UnityEngine;

[CreateAssetMenu(fileName = "FightGlobalSetting", menuName = "ScriptableObjects/FightGlobalSetting", order = 1)]
public class FightGlobalSetting : ScriptableObject
{
    [SerializeField] bool hasDefend;
    [SerializeField] bool skillStoneHasExp = false;
    [SerializeField] AnimationCurve knockOffYAnimationCurve;
    [SerializeField] AnimationCurve knockOffZAnimationCurve;
    [SerializeField] AnimationCurve hDamageYAnimationCurve;
    [SerializeField] AnimationCurve hDamageZAnimationCurve;
    [SerializeField] float AT_coefficient = 1;
    [SerializeField] float HP_coefficient = 1;
    [SerializeField] bool Team1Invincible = false;
    [SerializeField] int NormalSkillExGet = 20;
    [SerializeField] int Sp1SkillExGet = 15;
    [SerializeField] int Sp2SkillExGet = 10;
    [SerializeField] int Sp3SkillExGet = 5;
    [SerializeField] float slightHitLastingTime = 0.2f, lightHitLastingTime = 0.3f, heavyHitLastingTime = 0.6f, superHitLastingTime = 1f, highHitLastingTime = 0.8f;
    [SerializeField] float normalAttackPosFixingTime = 0.1f;
    [SerializeField] float knockOffExtent = 20f;
    [SerializeField] float MaxKnockoffLaidGroundTime = 2f;
    [SerializeField] float CanGetUpAfterKnockoffToGround = 0.5f;
    [SerializeField] float GetupTime = 1f;
    [SerializeField] float SureToPushForwardDis = 5f;
    [SerializeField] int defendHP = 20;
    [SerializeField] float lightBlockLastingTime = 0.3f, heavyBlockLastingTime = 0.5f;
    [SerializeField] float attackDrawingDistance = 1f;
    [SerializeField] int resistanceMax = 10;
    [SerializeField] int eXMax = 120;
    [SerializeField] int hurtObjectPreLoadCount = 5;
    [SerializeField] Material shadowMaterial;
    
    public static int _sceneStep;//0 :mainmenu 1: fightscene
    public static bool _hasDefend;
    public static bool _skillStoneHasExp;
    public static float _AT_coefficient = 1;
    public static float _HP_coefficient = 1;
    public static bool _Team1Invincible;
    public static int _NormalSkillExGet;
    public static int _Sp1SkillExGet;
    public static int _Sp2SkillExGet;
    public static int _Sp3SkillExGet;
    public static float SlightHitLastingTime, LightHitLastingTime, HeavyHitLastingTime, SuperHitLastingTime;
    public static float KnockOffExtent;
    public static float _MaxKnockoffLaidGroundTime;
    public static float _CanGetUpAfterKnockoffToGround;
    public static float _GetupTime;
    public static float _SureToPushForwardDis = 5f;
    public static float LightBlockLastingTime, HeavyBlockLastingTime, HighHitLastingTime;
    public static float NormalAttackPosFixingTime;
    public static AnimationCurve KnockOffYAnimationCurve, KnockOffZAnimationCurve;
    public static AnimationCurve HDamageYAnimationCurve;
    public static AnimationCurve HDamageZAnimationCurve;
    public static float _attackDrawingDistance;
    public static int _ResistanceMax = 120;
    public static int _EXMax;
    public static bool HitBoxLogger = true;
    public static int _HurtObjectPreLoadCount;
    public static Material _shadowMaterial;
    public static int _defendHP;
    
    public static string EffectPathDefine(Element element = Element.Null)
    {
        string personalEffectPath;
        switch (element)
        {
            case Element.blueMagic:
                personalEffectPath = "bluemagic";
                break;
            case Element.redMagic:
                personalEffectPath = "redmagic";
                break;
            case Element.greenMagic:
                personalEffectPath = "greenmagic";
                break;
            case Element.lightMagic:
                personalEffectPath = "lightmagic";
                break;
            case Element.darkMagic:
                personalEffectPath = "darkmagic";
                break;
            case Element.Null:
                personalEffectPath = "defaultmagic";
                break;
            default:
                personalEffectPath = "defaultmagic";
                break;
        }
        return personalEffectPath;
    }
    
    public void Initialise()
    {
        _HurtObjectPreLoadCount = hurtObjectPreLoadCount;
        
        _hasDefend = hasDefend;
        _skillStoneHasExp = skillStoneHasExp;

        _AT_coefficient = AT_coefficient;
        _HP_coefficient = HP_coefficient;
        _Team1Invincible = Team1Invincible;
        
        _NormalSkillExGet = NormalSkillExGet;
        _Sp1SkillExGet = Sp1SkillExGet;
        _Sp2SkillExGet = Sp2SkillExGet;
        _Sp3SkillExGet = Sp3SkillExGet;
        
        SlightHitLastingTime = slightHitLastingTime;
        LightHitLastingTime = lightHitLastingTime;
        HeavyHitLastingTime = heavyHitLastingTime;
        SuperHitLastingTime = superHitLastingTime;
        HighHitLastingTime = highHitLastingTime;
        NormalAttackPosFixingTime = normalAttackPosFixingTime;
        
        KnockOffExtent = knockOffExtent;
        _CanGetUpAfterKnockoffToGround = CanGetUpAfterKnockoffToGround;
        _MaxKnockoffLaidGroundTime = MaxKnockoffLaidGroundTime;
        _GetupTime = GetupTime;
        
        KnockOffYAnimationCurve = knockOffYAnimationCurve;
        KnockOffZAnimationCurve = knockOffZAnimationCurve;

        HDamageYAnimationCurve = hDamageYAnimationCurve;
        HDamageZAnimationCurve = hDamageZAnimationCurve;

        _SureToPushForwardDis = SureToPushForwardDis;

        _defendHP = defendHP;
        LightBlockLastingTime = lightBlockLastingTime;
        HeavyBlockLastingTime = heavyBlockLastingTime;

        _attackDrawingDistance = attackDrawingDistance;

        _ResistanceMax = resistanceMax;
        _EXMax = eXMax;
        
        _shadowMaterial = shadowMaterial;
    }
}
