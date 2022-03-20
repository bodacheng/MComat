using UnityEngine;
using dataAccess;

[CreateAssetMenu(fileName = "FightGlobalSetting", menuName = "ScriptableObjects/FightGlobalSetting", order = 1)]
public class FightGlobalSetting : ScriptableObject
{
    public ProgramMode programMode = ProgramMode.normal;
    public bool hasDefend;
    public bool skillStoneHasExp = false;
    public AnimationCurve knockOffyAnimationCurve;
    public AnimationCurve knockOffzAnimationCurve;

    public AnimationCurve HdamageYAnimationCurve;
    public AnimationCurve HdamageZAnimationCurve;

    public float AT_coefficient = 1;
    public float HP_coefficient = 1;
    public int NormalSkillExGet = 20;
    public int Sp1SkillExGet = 15;
    public int Sp2SkillExGet = 10;
    public int Sp3SkillExGet = 5;
    public float GetExAfterDamageBy = 10f;// 每受到GetExAfterDamageBy的伤害，获得ExGetAfterDamage的EX槽。用以平衡弱势
    public int ExGetAfterDamage = 10;
    public float slighthitLastingtime = 0.2f,lighthitLastingtime = 0.3f, heavyhitLastingtime = 0.6f, superhitLastingtime = 1f, highhitLastingTime = 0.8f;
    public float normalattackpositionfixingtime = 0.1f;
    public float knockoffextent = 20f;
    public float MaxKnockoffLaidGroundTime = 2f;
    public float CanGetUpAfterKnockoffToGround = 0.5f;
    public float GetupTime = 1f;
    public int defendHP = 20;
    public float lightBlockLastingTime = 0.3f, heavyBlockLastingTime = 0.5f;
    public float attackDrawingDistance = 1f;
    public int ResistanceMax = 10;
    public int EXMax = 120;

    public int HurtObjectPreLoadCount = 5;
    
    #region 技能石经验相关
    public float goldToExp = 1;
    public int SkillStoneRankToExp_rank1 = 100;
    public int SkillStoneRankToExp_rank2 = 200;
    public int SkillStoneRankToExp_rank3 = 300;
    public int SkillStoneRankToExp_rank4 = 400;
    public int SkillStoneRankToExp_rank5 = 500;
    #endregion
    
    public static ProgramMode _programMode;
    public static int scenestep;//0 :mainmenu 1: fightscene
    public static bool _hasDefend;
    public static bool _skillStoneHasExp;
    public static float _AT_coefficient = 1;
    public static float _HP_coefficient = 1;
    public static int _NormalSkillExGet;
    public static int _Sp1SkillExGet;
    public static int _Sp2SkillExGet;
    public static int _Sp3SkillExGet;
    public static float _slighthit_lastingtime, _lighthit_lastingtime, _heavyhit_lastingtime, _superhit_lastingtime;
    public static float _hitImpactLastingTime = 0.1f;
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
    public static int _ResistanceMax = 120;
    public static int _EXMax;
    public static bool HitBoxLogger = true;
    public static int _HurtObjectPreLoadCount;
    

    public static string EffectPathDefine(Element element)
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

    public enum ProgramMode
    {
        normal = 0,
        skillShow = 1
    }
    
    public void Initialise()
    {
        _programMode = programMode;
        
        _HurtObjectPreLoadCount = HurtObjectPreLoadCount;
        
        _hasDefend = hasDefend;
        _skillStoneHasExp = skillStoneHasExp;

        _AT_coefficient = AT_coefficient;
        _HP_coefficient = HP_coefficient;
        
        _NormalSkillExGet = NormalSkillExGet;
        _Sp1SkillExGet = Sp1SkillExGet;
        _Sp2SkillExGet = Sp2SkillExGet;
        _Sp3SkillExGet = Sp3SkillExGet;
        
        _slighthit_lastingtime = slighthitLastingtime;
        _lighthit_lastingtime = lighthitLastingtime;
        _heavyhit_lastingtime = heavyhitLastingtime;
        _superhit_lastingtime = superhitLastingtime;
        _highhit_lastingTime = highhitLastingTime;
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

        _ResistanceMax = ResistanceMax;
        _EXMax = EXMax;

        StoneExpManager.goldToExp = goldToExp;
        StoneExpManager.SkillStoneRankToExp_rank1 = SkillStoneRankToExp_rank1;
        StoneExpManager.SkillStoneRankToExp_rank2 = SkillStoneRankToExp_rank2;
        StoneExpManager.SkillStoneRankToExp_rank3 = SkillStoneRankToExp_rank3;
        StoneExpManager.SkillStoneRankToExp_rank4 = SkillStoneRankToExp_rank4;
        StoneExpManager.SkillStoneRankToExp_rank5 = SkillStoneRankToExp_rank5;
    }
}
