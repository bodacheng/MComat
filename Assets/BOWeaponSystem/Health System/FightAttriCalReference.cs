using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using HittingDetection;

//整个脚本中有若干个量并没有在本脚本内进行任何计算，就是起了个作为属性被参考的作用，原因在于为了保持BO武器脚本只参考BO系列文件的状态。
public partial class FightAttriCalReference : MonoBehaviour
{
    [Tooltip("数据中心")]
    public Data_Center _Center;
        
    [Tooltip("当前攻击力")]
    public float AT = 10;

    public ReactiveProperty<float> CurrentHp { get; set; } = new ReactiveProperty<float>();
    public ComboHitCount _ComboHitCount = new ComboHitCount();
    
    public static List<Collider> AllMeatColliders = new List<Collider>();

    KnockOffCount _knockOffCount = new KnockOffCount();
    BeHitCount _BeHitCount = new BeHitCount();
    bool gettingdamage;
    List<BO_Hitbox> myBOHitBoxeComponent = new List<BO_Hitbox>();//713添加
    List<E_Damage> Event_Damage_List = new List<E_Damage>();
    List<V_Damage> ICauseDamages = new List<V_Damage>();
    List<Collider> myColliders;
    E_Damage managingEventDamage;
    List<E_Damage> Event_Attack_Successed_List = new List<E_Damage>();
    UnityEngine.Events.UnityAction gravityloststart;
    UnityEngine.Events.UnityAction gravitylostend;
    customCoroutine burstCoroutine;

    // [Tooltip("与健康体同级的那个collider作不作为伤害判断?")]
    // public bool collider_on_health = false; //固定值 虽然这个值本身没有在本脚本中进行任何计算，但由于BO_Health会频繁访问BO_Health，所以如果需要这样一个参数，放在这里仍然合适

    void Awake()
    {
        //registerMyDefaultMaterialsShaderDic();
    }
    
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

    public void AddToBOHitBoxeComponent(BO_Hitbox bO_Hitbox)
    {
        if (!myBOHitBoxeComponent.Contains(bO_Hitbox))
        {
            myBOHitBoxeComponent.Add(bO_Hitbox);
        }
    }
    
    public void EnableAllHitBoxCollider(bool _bool)
    {
        if (myBOHitBoxeComponent != null)
        foreach (BO_Hitbox hitbox in myBOHitBoxeComponent)
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
                BO_Hitbox _BO_Hitbox = myBOHitBoxeComponent[i];
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
            foreach (BO_Hitbox _BO_Hitbox in myBOHitBoxeComponent)
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

    void EatDamageProcess(V_Damage processingD)
    {
        _BeHitCount.BeHitCountPlus();
        if (processingD.fromWeapon != null)
        {
            if (processingD.fromWeapon.GetOwnerFightAttriCalReference() != null)
            {
                processingD.fromWeapon.GetOwnerFightAttriCalReference().HitCountPlus();
                if (processingD.fromWeapon._WeaponMode == WeaponMode.EnergyFromBodyWeapon)
                    processingD.fromWeapon.GetOwnerFightAttriCalReference()._Center.Animation_Manger.FrameFreeze();
            }
            CurrentHp.Value -= processingD.AT;
            if (CurrentHp.Value <= 0)
            {
                V_Damage deathknockOff;
                deathknockOff = new V_Damage(
                              DamageType.deathknockoff,
                              processingD._WeaponPosAdjustMode,
                              processingD.damageHappenPoint,
                              processingD.AttackerT_foward,
                              processingD.AttackerT_pos,
                              processingD.fromWeapon);
                ApplyDamage(deathknockOff);
                _Center.IsDead.Value = true;
                _Center.AIStateRunner.ChangeState("Death");
                return; //重点在于，不再继续接下来的清空伤害列表操作。由死亡状态清空伤害列表，因为死亡状态需要这个列表来进行最后的击飞演出
            }
        }
        
        switch (processingD.specialApply)
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
                burstCoroutine = new customCoroutine(gravityloststart, 0.2f, gravitylostend);
                _Center.buffsRunner.runSubCoroutineOfState(burstCoroutine);
                break;
        }
    }

    // 这个函数，是写的很丑陋，但如果纯粹是为了解决个人强迫症的话干脆别改了。其实这个产生的根本原因就是你非要把不同重量级的攻击给用枚举类型区分开
    public void ApplyDamage(V_Damage _dmg)
	{
        _Center._ResistanceManager.Resistance.Value -= 1;
        if (_Center.AIStateRunner.GetCurrentStateNum() == "Defend")
        {
            _dmg.damage_type = DamageType.heavy_block;
            _dmg.AT = 0;
            _Center.AIStateRunner.ChangeState("Defend",_dmg);
        }
        EatDamageProcess(_dmg);
        if (_Center._ResistanceManager.Resistance.Value > 0)
        {
            return;
        }        
        _ComboHitCount.HitCountInterrupt();
        switch(_dmg.damage_type)
        {
            case DamageType.slight_damage:
            break;
            case DamageType.light_damage:
            _Center.AIStateRunner.ChangeState("Hit",_dmg);
            break;
            case DamageType.heavy_damage:
            _Center.AIStateRunner.ChangeState("Hit",_dmg);
            break;
            case DamageType.supper_damage:
            _Center.AIStateRunner.ChangeState("Hit",_dmg);
            break;
            case DamageType.knockOff_damage:
            _Center.AIStateRunner.ChangeState("KnockOff",_dmg);
            break;
            case DamageType.light_block:
            _Center.AIStateRunner.ChangeState("Defend",_dmg);
            break;
            case DamageType.heavy_block:
            _Center.AIStateRunner.ChangeState("Defend",_dmg);
            break;
            case DamageType.deathknockoff:
            _Center.AIStateRunner.ChangeState("Death",_dmg);
            break;
        }        
	}
    
    public void HitCountPlus() => _ComboHitCount.HitCountPlus();//打别人计数
    public int GetBeHitCount() => _BeHitCount.GetBeHitCount(); //自己被揍计数
    public void BeHitCountInterrupt() => _BeHitCount.BeHitCountInterrupt();

    // event 攻击系列。暂时不再使用
    public void SetManagingEventDamage(E_Damage e)
    {
        this.managingEventDamage = e;
    }
    public E_Damage GetManagingEventDamage()
    {
        return managingEventDamage;
    }
    public void EventAttackHitApprove(E_Damage e)
    {
        this.Event_Attack_Successed_List.Add(e);
    }
    public void AddEventDamageList(E_Damage e)
    {
        e.SetDamagedHealthBody(this);
        this.Event_Damage_List.Add(e);
    }
    public List<E_Damage> ReturnEventDamageList()
    {
        return this.Event_Damage_List;
    }
    public List<E_Damage> ReturnApprovedEventAttackAttempts()
    {
        return this.Event_Attack_Successed_List;
    }

    Shader One_Shader;
    public void SwapShader(string shaderName)
    {
        One_Shader = Shader.Find(shaderName);
        foreach (Renderer singleRenderer in GetComponentsInChildren<Renderer>())
        {
            if (singleRenderer is ParticleSystemRenderer)
            {
                continue;
            }
            foreach (Material singleMaterial in singleRenderer.materials)
            {
				singleMaterial.shader = One_Shader;
                //SetAllMaterialProperties(singleMaterial);
            }
        }
    }

    IDictionary<int, Shader> myDefaultMaterialsShaderDic;
    private void RegisterMyDefaultMaterialsShaderDic()
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

    public void RestoreMyDefaultMaterialsShaders()
    {
        if (myDefaultMaterialsShaderDic == null)
            return;

        foreach (Renderer singleRenderer in GetComponentsInChildren<Renderer>())
        {
            if (singleRenderer is ParticleSystemRenderer)
            {
                continue;
            }
            foreach (Material singleMaterial in singleRenderer.materials)
            {
                myDefaultMaterialsShaderDic.TryGetValue(singleMaterial.GetHashCode(), out One_Shader);
                if (One_Shader)
                    singleMaterial.shader = One_Shader;
            }
        }
    }
        
    public IEnumerator ShaderChange(string magicKind,float time)
    {
        switch(magicKind)
        {
            case "redMagic":
                SwapShader("Hurt/Red");
                break;
            case "blueMagic":
                SwapShader("Hurt/Red");
                break;
            case "greenMagic":
                SwapShader("Hurt/Red");
                break;
            default:
                SwapShader("Hurt/Red");
                break;
        }
        yield return new WaitForSeconds(time);
        RestoreMyDefaultMaterialsShaders();
    }
    
    Color damagecolor;
    public void RunShaderChangeProcess(string magicKind,float speed,float time)
    {
        //process = StartCoroutine(shaderChange(magicKind,time));
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
            _Center._ShaderManager.RimEffectsForAShortTime(0.8f, speed, time, damagecolor);
        }
    }
    
    BO_Shield _shield; //已几乎不再用
    // 旧防御盾系列函数。已经基本不用
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

// 下面这些原本在applydamage开头
    //if ((PhotonNetwork.connected && !photonView.isMine))
    //  return;//同步系统中伤害判定最关键的一条逻辑

    //colorTransition = ColorTransition.DoTransition(gameObject, new GradientColorKey[] { new GradientColorKey(Color.red, 0.2f), new GradientColorKey(Color.white, 0.2f) }, true, 0f);

    //UnityEngine.Events.UnityAction resetColor = () => { colorTransition.ResetColors(); };
    //      colorTransition.OnEnd.AddListener(resetColor);
    ////colorTransition.AfterTransition = AfterTransitionActions.DisableComponent;
    //colorTransition.Duration = 0.2f;
    //colorTransition.LoopMode = TransitionLoopModes.PlayOnce;