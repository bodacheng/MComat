using UnityEngine;
using System.Collections.Generic;
using UniRx;
using HittingDetection;
using Log;

public partial class FightAttriCalReference
{
    public static List<Collider> AllMeatColliders = new List<Collider>();
    
    public Data_Center _Center;
    public float AT = 10;
    public CriticalGaugeMode criticalGaugeMode;
    
    public ReactiveProperty<float> CurrentHp { get; set; } = new ReactiveProperty<float>();
    public ComboHitCount _ComboHitCount = new ComboHitCount();
    readonly KnockOffCount _knockOffCount = new KnockOffCount();
    readonly BeHitCount _BeHitCount = new BeHitCount();
    readonly List<BO_Limb> myLimbs = new List<BO_Limb>();
    readonly List<E_Damage> Event_Damage_List = new List<E_Damage>();
    readonly List<V_Damage> ICauseDamages = new List<V_Damage>();
    readonly List<Collider> myColliders = new List<Collider>();
    readonly List<E_Damage> Event_Attack_Successed_List = new List<E_Damage>();
    
    // [Tooltip("与健康体同级的那个collider作不作为伤害判断?")]
    // public bool collider_on_health = false; //固定值 虽然这个值本身没有在本脚本中进行任何计算，但由于BO_Health会频繁访问BO_Health，所以如果需要这样一个参数，放在这里仍然合适
    
    public bool Invincible { get; set; }
    public bool Gettingdamage { get; set; }

    public void INI()
    {
        Clear();
        CurrentHp = new ReactiveProperty<float>();
        CriticalGauge = new ReactiveProperty<int>();
    }

    public void Clear()
    {
        CurrentHp.Dispose();
        CriticalGauge.Dispose();
    }
    
    public void HealthBodyFixedUpdate()
    {
        _knockOffCount.Update();
        _ComboHitCount.Update();
        _BeHitCount.Update();
    }

    public KnockOffCount GetKnockOffCount()
    {
        return _knockOffCount;
    }

    public void AddToBOHitBoxeComponent(BO_Limb bO_Hitbox)
    {
        if (!myLimbs.Contains(bO_Hitbox))
        {
            myLimbs.Add(bO_Hitbox);
        }
    }
    
    public void EnableAllLimbs(bool _bool)
    {
        foreach (BO_Limb hitbox in myLimbs)
        {
            if (hitbox.myColliderMustEquip != null)
                hitbox.myColliderMustEquip.isTrigger = !_bool;
        }
    }
        
    public void ChangeLayerForLimbs(int layer)
    {
        for (int i = 0; i < myLimbs.Count; i++)
        {
            BO_Limb _BO_Hitbox = myLimbs[i];
            _BO_Hitbox.gameObject.layer = layer;
        }
        _Center.geometryCenter.gameObject.layer = layer;
    }
    
    public bool IfMyBody(Collider collider) => myColliders.Contains(collider);
    
    public void FindAllSelfCollidersAndIgnoreCollision()
    {
        myColliders.Clear();
        if (myLimbs != null)
        {
            foreach (BO_Limb _BO_Hitbox in myLimbs)
            {
                _BO_Hitbox.INI();
                if (_BO_Hitbox.myColliderMustEquip != null && !myColliders.Contains(_BO_Hitbox.myColliderMustEquip))
                {
                    PhysicMaterial physicMaterial = Resources.Load<PhysicMaterial>("physics/temp");
                    _BO_Hitbox.myColliderMustEquip.material = physicMaterial;
                    myColliders.Add(_BO_Hitbox.myColliderMustEquip);
                }
            }
        }
        if (_shield != null)
        {
            if (_shield._shieldCollider != null)           
                myColliders.Add(_shield._shieldCollider);
        }
        for (int i = 0; i < myColliders.Count; i++)
        {
            for (int y = i + 1; y < myColliders.Count; y++)
            {
                Physics.IgnoreCollision(myColliders[i], myColliders[y]);
            }
            AllMeatColliders.Add(myColliders[i]);
        }
    }
    
    public void MyDamageCount(V_Damage _dmg)
    {
        if (ICauseDamages != null)
        {
            ICauseDamages.Add(_dmg);
        }
    }
    
    string temp;
    void HitEffect(V_Damage v_Damage)
    {
        if (_Center._ResistanceManager.Resistance.Value > 0)
        {
            EffectsManager.GenerateEffect("shield_hit",
            FightGlobalSetting.EffectPathDefine(v_Damage.from_weapon.zokusei),
            v_Damage.damageHappenPoint,
            v_Damage.CutRotation,
            null);
        }
        else
        {
            switch (v_Damage.from_weapon.damage_type)
            {
                case DamageType.heavy_damage_forward:
                    temp = "heavy_hit";
                    break;
                case DamageType.supper_damage_forward:
                    temp = "super_hit";
                    break;
                default:
                    temp = "light_hit";
                    break;
            }
            EffectsManager.GenerateEffect(temp, FightGlobalSetting.EffectPathDefine(v_Damage.from_weapon.zokusei),
            v_Damage.damageHappenPoint,
            v_Damage.CutRotation,
            v_Damage.from_weapon.effectSpreadOnBody ? _Center.geometryCenter : null);
        }
    }

    // 受攻击方运行
    float _d;
    public void ApplyDamage(V_Damage _dmg)
	{
        if (_Center._MyBehaviorRunner.GetNowState().StateType == Skill.BehaviorType.Def && _Center._ResistanceManager.Resistance.Value > 0)
        {
            _Center._MyBehaviorRunner.ChangeState("Defend", _dmg);
            HitEffect(_dmg);
            return;
        }
        
        HitEffect(_dmg);
        if (_Center._ResistanceManager.Resistance.Value > 0)
        {
            _Center._ResistanceManager.Resistance.Value -= 1;
            return;
        }
        
        _dmg.attacker.HitCountPlus(this);
        _ComboHitCount.HitCountInterrupt();
        _BeHitCount.BeHitCountPlus();
        
        _d = _dmg.from_weapon.GetDamageAmount();
        if (!Invincible)
            CurrentHp.Value -= _d;
        if (CurrentHp.Value <= 0)
        {
            _Center._MyBehaviorRunner.ChangeState("Death", _dmg);
            return;
        }
        
        _Center._MyBehaviorRunner.SingleFightLog.WriteLog(
            new Soul.SingleFightLog.NegativeRecord
            {
                //被打了，属于负面效益
            }
        );
        _Center._MyBehaviorRunner.SingleFightLog.AnalysisLog(_Center._MyBehaviorRunner.ConditionAndRespondPriority);
        _Center._MyBehaviorRunner.ChangeState("Hit", _dmg);
        _dmg.from_weapon.HitBoxLifeEnding = HitBoxLifeEnding.successed;
    }
    
    // 打别人计数
    public void HitCountPlus(FightAttriCalReference victim)
    {
        _ComboHitCount.HitCountPlus(_BeHitCount);
        _Center._MyBehaviorRunner.SingleFightLog.WriteLog(
            new Soul.SingleFightLog.PositiveRecord
            {
                //打了别人，属于正面效益
            }
        );
        _Center._MyBehaviorRunner.SingleFightLog.AnalysisLog(_Center._MyBehaviorRunner.ConditionAndRespondPriority);
        _Center._MyBehaviorRunner.GetNowState().EnergyAbsorb(criticalGaugeMode, victim);
    }
    
    public int GetBeHitCount() => _BeHitCount.GetBeHitCount(); //自己被揍计数

    E_Damage managingEDamage;
    // event 攻击系列。暂时不再使用
    public void SetManagingEventDamage(E_Damage e)
    {
        managingEDamage = e;
    }
    public E_Damage GetManagingEventDamage()
    {
        return managingEDamage;
    }
    public void EventAttackHitApprove(E_Damage e)
    {
        Event_Attack_Successed_List.Add(e);
    }
    public void AddEventDamageList(E_Damage e)
    {
        e.SetDamagedHealthBody(this);
        Event_Damage_List.Add(e);
    }
    public List<E_Damage> ReturnEventDamageList()
    {
        return Event_Damage_List;
    }
    public List<E_Damage> ReturnApprovedEventAttackAttempts()
    {
        return Event_Attack_Successed_List;
    }
    
    Color damagecolor;
    public void RunShaderChangeProcess(Zokusei zokusei, float time)
    {
        if (_Center._ShaderManager != null)
        {
            switch (zokusei)
            {
                case Zokusei.redMagic:
                    damagecolor = Color.red;
                    break;
                case Zokusei.blueMagic:
                    damagecolor = Color.blue;
                    break;
                case Zokusei.greenMagic:
                    damagecolor = Color.green;
                    break;
                case Zokusei.darkMagic:
                    damagecolor = new Color(1f, 0f, 1f);
                    break;
                case Zokusei.lightMagic:
                    damagecolor = new Color(0f, 1f, 1f);
                    break;
                default:
                    damagecolor = Color.white;
                    break;
            }
            _Center._ShaderManager.RimEffectsForAShortTime(1f, time, damagecolor);
        }
    }
    
    // 旧防御盾系列函数。已经基本不用
    BO_Shield _shield;
    public void SetShield(BO_Shield _shield)
    {
        FightAttriCalReference fightAttriCalReference = this;
        fightAttriCalReference._shield = _shield;
    }
    public BO_Shield GetShield()
    {
        return _shield;
    }
}