using UnityEngine;
using HittingDetection;
using Soul;
using Skill;

//死亡状态下关于怎么将死亡角色从战场正式排除需要重新研究。详见Data_Center.FindTargetsByDistance（直接从游戏物体获取tag意外的浪费时间）
public class Death_State : Behavior
{
    readonly string clip_name;
    readonly float stopRunningTime;
    readonly GameObject processingBlood;
    float time_count;
    Vector3 _xz;    
    bool touchedBoundary;
    bool dropped;
    
    public Death_State(float stopRunningTime, string clip_name)
    {
        this.stopRunningTime = stopRunningTime;
        this.clip_name = clip_name;
        StateType = BehaviorType.KnockOff;
    }
    
    public override void Pre_process_before_enter()
    {
        base.Pre_process_before_enter ();
    }
    
    public override bool Capacity_Exit_Condition()
    {
        return false;
    }
    
    public override bool Force_enter_condition()
    {
        return false;
    }

    public override void AI_State_enter(V_Damage newValue)
    {
        base.AI_State_enter();
        time_count = 0f;
        pEvents.CloseAllPersonalityEffects();
        _BasicPhysicSupport.SetUsingGravity(false);
        _DATA_CENTER.IsDead.Value = true;
        _DATA_CENTER.DeathInitialize();
        _Rigidbody.velocity = Vector3.zero;
        _Animator.SetFloat("speed", 0f);
        _Animator.applyRootMotion = false;
        //进入击飞状态后这个动画的播放应该是没有前提的。这一下和的机理比较绕，可以看一下BO_health那边eatdamage怎么写的。
        Animation_Manger.AnimationTrigger(clip_name,true,0.1f);
        EffectsManager.GenerateEffect("super_hit",
            FightGlobalSetting.EffectPathDefine(newValue.from_weapon.zokusei),
            newValue.damageHappenPoint, gameObject.transform.rotation,
            _FightAttriCalRef.transform);
        touchedBoundary = false;
        dropped = false;
        _xz = newValue.attacker._Center.WholeT.forward;
        pEvents.CloseAllPersonalityEffects();
    }

    public override void AI_State_exit()
    {
        base.AI_State_exit();
        _Rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        time_count = 0f;
        _BasicPhysicSupport.SetUsingGravity(true);
        _FightAttriCalRef.ChangeLayerForAllSelfColliders(_DATA_CENTER._TeamConfig.mylayer);
    }
    
    Vector3 effectP;
    public override void _State_FixedUpdate1()
    {
        if (!touchedBoundary)
        {
            if (_BasicPhysicSupport.hiddenMethods.onBattleGroundBundary)
            {
                touchedBoundary = true;
                _xz = Vector3.zero - gameObject.transform.position;
                _xz.y = 0;
                _xz = _xz.normalized;
                effectP = gameObject.transform.position.normalized * BoundaryControllByGod._BattleRingRadius;
                effectP.y = gameObject.transform.position.y;
                Vector3 quaV = Vector3.zero - gameObject.transform.position.normalized;
                quaV.y = 0;
                EffectsManager.GenerateEffect("wallCrack", null, effectP, Quaternion.LookRotation(quaV, Vector3.up), null);
            }
        }
        
        if (!dropped)
        {
            if (time_count > 0.1f && _BasicPhysicSupport.hiddenMethods.Grounded)
            {
                dropped = true;
                effectP = gameObject.transform.position;
                effectP.y = 0;
                EffectsManager.GenerateEffect("hit_ground", null, effectP, Quaternion.LookRotation(Vector3.right), null);
                _FightAttriCalRef.ChangeLayerForAllSelfColliders(0);
                _Rigidbody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
                time_count = 0;//开始针对躺地时间记时
            }else{
                gameObject.transform.position += 
                _xz * (FightGlobalSetting._knockOffzAnimationCurve.Evaluate(time_count + Time.fixedDeltaTime) - FightGlobalSetting._knockOffzAnimationCurve.Evaluate(time_count)) +
                Vector3.up * (FightGlobalSetting._knockOffyAnimationCurve.Evaluate(time_count + Time.fixedDeltaTime) - FightGlobalSetting._knockOffyAnimationCurve.Evaluate(time_count));
            }
        }
        time_count += Time.fixedDeltaTime;
    }
}