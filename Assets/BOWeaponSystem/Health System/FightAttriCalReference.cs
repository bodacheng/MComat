using UnityEngine;
using System.Collections.Generic;
using UniRx;
using HittingDetection;

//整个脚本中有若干个量并没有在本脚本内进行任何计算，就是起了个作为属性被参考的作用，原因在于为了保持BO武器脚本只参考BO系列文件的状态。
public partial class FightAttriCalReference : MonoBehaviour
{
    public static List<Collider> AllMeatColliders = new List<Collider>();
    
    [Tooltip("数据中心")]
    public Data_Center _Center;
        
    [Tooltip("当前攻击力")]
    public float AT = 10;

    public ReactiveProperty<float> CurrentHp { get; set; } = new ReactiveProperty<float>();
    
    public ComboHitCount _ComboHitCount = new ComboHitCount();
    KnockOffCount _knockOffCount = new KnockOffCount();
    BeHitCount _BeHitCount = new BeHitCount();    
    
    V_Damage deathknockOff;
    bool gettingdamage;
    List<BO_Limb> myBOHitBoxeComponent = new List<BO_Limb>();//713添加
    List<E_Damage> Event_Damage_List = new List<E_Damage>();
    List<V_Damage> ICauseDamages = new List<V_Damage>();
    List<Collider> myColliders;
    E_Damage managingEventDamage;
    List<E_Damage> Event_Attack_Successed_List = new List<E_Damage>();
    UnityEngine.Events.UnityAction gravityloststart;
    UnityEngine.Events.UnityAction gravitylostend;
    CustomCoroutine burstCoroutine;

    // [Tooltip("与健康体同级的那个collider作不作为伤害判断?")]
    // public bool collider_on_health = false; //固定值 虽然这个值本身没有在本脚本中进行任何计算，但由于BO_Health会频繁访问BO_Health，所以如果需要这样一个参数，放在这里仍然合适
    
    public void SetGettingDamageState(bool _state)
    {
        gettingdamage = _state;
    }
    public bool IFgettingDamage()
    {
        return gettingdamage;
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
        if (!myBOHitBoxeComponent.Contains(bO_Hitbox))
        {
            myBOHitBoxeComponent.Add(bO_Hitbox);
        }
    }
    
    public void EnableAllHitBoxCollider(bool _bool)
    {
        if (myBOHitBoxeComponent != null)
        foreach (BO_Limb hitbox in myBOHitBoxeComponent)
        {
            if (hitbox.myColliderMustEquip != null)
                hitbox.myColliderMustEquip.isTrigger = !_bool;
        }
    }
    
    public void ChangeLayerForAllSelfColliders(int layer)
    {
        if (myBOHitBoxeComponent != null)
        {
            for (int i = 0; i < myBOHitBoxeComponent.Count; i++)
            {
                BO_Limb _BO_Hitbox = myBOHitBoxeComponent[i];
                _BO_Hitbox.gameObject.layer = layer;
            }
        }
        gameObject.layer = layer;
    }

    public bool IfMyBody(Collider collider) => myColliders.Contains(collider);

    public void FindAllSelfCollidersAndIgnoreCollision()
    {
        myColliders = new List<Collider>();
        if (myBOHitBoxeComponent != null)
        {
            foreach (BO_Limb _BO_Hitbox in myBOHitBoxeComponent)
            {
                _BO_Hitbox.INI();
                if (_BO_Hitbox.myColliderMustEquip != null && !myColliders.Contains(_BO_Hitbox.myColliderMustEquip))
                {
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
    
    Decompositioner processingBlood;
    void HitEffect(V_Damage v_Damage)
    {
        if (_Center._ResistanceManager.Resistance.Value > 0)
        {
            processingBlood = EffectAndHurtObjectLoading.Instance.GenerateEffect("shield_hit",
                                                                   v_Damage.effectPath,
                                                                   v_Damage.damageHappenPoint,
                                                                   v_Damage.CutRotation,
                                                                   null);
        }
        else
        {
            switch (v_Damage.damage_type)
            {
                case DamageType.slight_damage:
                    processingBlood = EffectAndHurtObjectLoading.Instance.GenerateEffect("light_hit",
                                                                   v_Damage.effectPath,
                                                                   v_Damage.damageHappenPoint,
                                                                   v_Damage.CutRotation,
                                                                   v_Damage.effectSpreadOnBody ? transform : null);
                    break;
                case DamageType.light_damage:
                    processingBlood = EffectAndHurtObjectLoading.Instance.GenerateEffect("light_hit",
                                                                   v_Damage.effectPath,
                                                                   v_Damage.damageHappenPoint,
                                                                   v_Damage.CutRotation,
                                                                   v_Damage.effectSpreadOnBody ? transform : null);
                    break;
                case DamageType.heavy_damage:
                    processingBlood = EffectAndHurtObjectLoading.Instance.GenerateEffect("heavy_hit",
                                                                   v_Damage.effectPath,
                                                                   v_Damage.damageHappenPoint,
                                                                   v_Damage.CutRotation,
                                                                   v_Damage.effectSpreadOnBody ? transform : null);
                    break;
                case DamageType.supper_damage:
                    processingBlood = EffectAndHurtObjectLoading.Instance.GenerateEffect("super_hit",
                                                                   v_Damage.effectPath,
                                                                   v_Damage.damageHappenPoint,
                                                                   v_Damage.CutRotation,
                                                                   v_Damage.effectSpreadOnBody ? transform : null);
                    break;
                case DamageType.light_block:
                    processingBlood = EffectAndHurtObjectLoading.Instance.GenerateEffect("shield_hit",
                                                   v_Damage.effectPath,
                                                   v_Damage.damageHappenPoint,
                                                   v_Damage.CutRotation,
                                                   v_Damage.effectSpreadOnBody ? transform : null);
                break;
                case DamageType.heavy_block:
                    processingBlood = EffectAndHurtObjectLoading.Instance.GenerateEffect("shield_hit",
                                                   v_Damage.effectPath,
                                                   v_Damage.damageHappenPoint,
                                                   v_Damage.CutRotation,
                                                   v_Damage.effectSpreadOnBody ? transform : null);
                break;
                default:
                    processingBlood = EffectAndHurtObjectLoading.Instance.GenerateEffect("light_hit",
                                                                   v_Damage.effectPath,
                                                                   v_Damage.damageHappenPoint,
                                                                   v_Damage.CutRotation,
                                                                   v_Damage.effectSpreadOnBody ? transform : null);
                    break;
            }
        }
    }
    
    // 受攻击方运行
    public void ApplyDamage(V_Damage _dmg)
	{
        if (_Center._MyBehaviorRunner.GetNowState().StateKey == "Defend" && _Center._ResistanceManager.Resistance.Value > 0)
        {
            _dmg.damage_type = DamageType.heavy_block;
            HitEffect(_dmg);
            _dmg.AT = 0;
            _Center._MyBehaviorRunner.ChangeState("Defend",_dmg);
            return;
        }
        
        if (_dmg._weaponMode == WeaponMode.EnergyFromBodyWeapon)
        {
            _dmg.attacker._Center.Animation_Manger.FrameFreeze();
        }

        HitEffect(_dmg);
        if (_Center._ResistanceManager.Resistance.Value > 0)
        {
            _Center._ResistanceManager.Resistance.Value -= 1;
            return;
        }
        
        _dmg.attacker.HitCountPlus();
        _ComboHitCount.HitCountInterrupt();
        _BeHitCount.BeHitCountPlus();
        
        CurrentHp.Value -= _dmg.AT;
        
        switch (_dmg.specialApply)
        {
            case SpecialApply.gravitylost:
                gravityloststart = () =>
                {
                    _Center._BasicPhysicSupport.SetUsingGravity(false);
                };
                gravitylostend = () =>
                {
                    _Center._BasicPhysicSupport.SetUsingGravity(true);
                };
                burstCoroutine = new CustomCoroutine(gravityloststart, 0.2f, gravitylostend);
                _Center.buffsRunner.RunSubCoroutineOfState(burstCoroutine);
                break;
        }

        if (_dmg.AT >= CurrentHp.Value)
        {
            _dmg.damage_type = DamageType.DeathKnockoff;
        }
        switch(_dmg.damage_type)
        {
            case DamageType.slight_damage:
            break;
            case DamageType.light_damage:
            _Center._MyBehaviorRunner.ChangeState("Hit",_dmg);
            break;
            case DamageType.heavy_damage:
            _Center._MyBehaviorRunner.ChangeState("Hit",_dmg);
            break;
            case DamageType.supper_damage:
            _Center._MyBehaviorRunner.ChangeState("Hit",_dmg);
            break;
            case DamageType.knockOff_damage:
            _Center._MyBehaviorRunner.ChangeState("KnockOff",_dmg);
            break;
            case DamageType.light_block:
            _Center._MyBehaviorRunner.ChangeState("Defend",_dmg);
            break;
            case DamageType.heavy_block:
            _Center._MyBehaviorRunner.ChangeState("Defend",_dmg);
            break;
            case DamageType.DeathKnockoff:
            _Center._MyBehaviorRunner.ChangeState("Death",_dmg);
            break;
        }        
	}
    
    public void HitCountPlus() => _ComboHitCount.HitCountPlus(_BeHitCount);//打别人计数
    public int GetBeHitCount() => _BeHitCount.GetBeHitCount(); //自己被揍计数
    
    // event 攻击系列。暂时不再使用
    public void SetManagingEventDamage(E_Damage e)
    {
        managingEventDamage = e;
    }
    public E_Damage GetManagingEventDamage()
    {
        return managingEventDamage;
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

    IDictionary<int, Shader> myDefaultMaterialsShaderDic;
    void RegisterMyDefaultMaterialsShaderDic()
    {
        myDefaultMaterialsShaderDic = new Dictionary<int, Shader>();
        foreach (Renderer singleRenderer in GetComponentsInChildren<Renderer>())
        {
            if (singleRenderer is ParticleSystemRenderer)
            {
                continue;
            }
            foreach (Material singleMaterial in singleRenderer.materials)
                if (!myDefaultMaterialsShaderDic.ContainsKey(singleMaterial.GetHashCode()))
                {
                    myDefaultMaterialsShaderDic.Add(new KeyValuePair<int, Shader>(singleMaterial.GetHashCode(), singleMaterial.shader));
                }
        }
    }
    
    Color damagecolor;
    public void RunShaderChangeProcess(string magicKind, float time)
    {
        if (_Center._ShaderManager != null)
        {
            switch (magicKind)
            {
                case "redMagic":
                    damagecolor = Color.red;
                    break;
                case "blueMagic":
                    damagecolor = Color.blue;
                    break;
                case "greenMagic":
                    damagecolor = Color.green;
                    break;
                case "darkMagic":
                    damagecolor = new Color(1f, 0f, 1f);
                    break;
                case "lightMagic":
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