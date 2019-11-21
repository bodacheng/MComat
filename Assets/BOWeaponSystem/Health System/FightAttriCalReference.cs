using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using System.Linq;
using HittingDetection;

//整个脚本中有若干个量并没有在本脚本内进行任何计算，就是起了个作为属性被参考的作用，原因在于为了保持BO武器脚本只参考BO系列文件的状态。
public partial class FightAttriCalReference : MonoBehaviour
{
    [Tooltip("数据中心")]
    public Data_Center _Center;
    
	[Tooltip("角色重量级")]
	public weight weight = weight.normal;
    
    [Tooltip("当前攻击力")]
    public float AT = 10;

    public ReactiveProperty<float> CurrentHp { get; set; } = new ReactiveProperty<float>();
    public ComboHitCount _ComboHitCount = new ComboHitCount();
    
    public static List<Collider> AllMeatColliders = new List<Collider>();
    
    private knockOffCount _knockOffCount = new knockOffCount();    
    private BeHitCount _BeHitCount= new BeHitCount();        
    private bool gettingdamage;
    private List<BO_Hitbox> myBOHitBoxeComponent = new List<BO_Hitbox>();//713添加
    private List<E_Damage> Event_Damage_List = new List<E_Damage>();
    private List<V_Damage> ICauseDamages = new List<V_Damage>();
    private List<Collider> myColliders;
    private E_Damage managingEventDamage;
    private List<E_Damage> Event_Attack_Successed_List = new List<E_Damage>();
    private IDictionary<DamageType, List<V_Damage>> DamagesDIC = new Dictionary<DamageType, List<V_Damage>>()
    {
        {DamageType.slight_damage,new List<V_Damage>()},
        {DamageType.light_damage,new List<V_Damage>()},
        {DamageType.heavy_damage,new List<V_Damage>()},
        {DamageType.supper_damage,new List<V_Damage>()},
        {DamageType.heavy_block,new List<V_Damage>()},
        {DamageType.stagger,new List<V_Damage>()},
        {DamageType.light_block,new List<V_Damage>()},
        {DamageType.knockOff_damage,new List<V_Damage>()},
        {DamageType.deathknockoff,new List<V_Damage>()}
    };
    
    UnityEngine.Events.UnityAction gravityloststart;
    UnityEngine.Events.UnityAction gravitylostend;
    customCoroutine burstCoroutine;

    BO_Shield _shield; //已几乎不再用

    // [Tooltip("与健康体同级的那个collider作不作为伤害判断?")]
    // public bool collider_on_health = false; //固定值 虽然这个值本身没有在本脚本中进行任何计算，但由于BO_Health会频繁访问BO_Health，所以如果需要这样一个参数，放在这里仍然合适

    void Awake()
    {
        //registerMyDefaultMaterialsShaderDic();
    }
    
    public void ClearDamageLists()
    {
        foreach (KeyValuePair<DamageType, List<V_Damage>> _keyvaluepair in DamagesDIC)
        {
            _keyvaluepair.Value.Clear();
        }
    }

    public void SetGettingDamageState(bool _state)
    {
        this.gettingdamage = _state;
    }
    public bool IFgettingDamage()
    {
        return gettingdamage;
    }

    public void HealthBodyFixedUpdate()
    {
        if (DamagesDIC[DamageType.slight_damage].Count > 0)//这里有无法理解的报错
        {
            EatDamage(DamageType.slight_damage);
        }
        _knockOffCount.update();
        _ComboHitCount.update();
        _BeHitCount.update();
    }

    public knockOffCount GetKnockOffCount()
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
        this.gameObject.layer = layer;
    }

    public void MyDamageCount(V_Damage _dmg)
    {
        if (this.ICauseDamages != null)
        {
            this.ICauseDamages.Add(_dmg);
        }
    }

    List<V_Damage> processingDlist;
    V_Damage processingD;
    void EatDamageProcess(List<V_Damage> v_Damages)
    {
        processingDlist = v_Damages.ToList();
        for (int i = 0; i < processingDlist.Count; i++)//这个ToList()不加的话可能报错：Collection was modified; enumeration operation may not execute
        {
            processingD = processingDlist[i];
            _BeHitCount.BeHitCountPlus();
            if (processingD.fromWeapon != null)
            {
                if (processingD.fromWeapon.GetOwnerFightAttriCalReference() != null)
                    processingD.fromWeapon.GetOwnerFightAttriCalReference().HitCountPlus();
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
                    break; //重点在于，不再继续接下来的清空伤害列表操作。由死亡状态清空伤害列表，因为死亡状态需要这个列表来进行最后的击飞演出
                }
            }

            switch (processingD.specialApply)
            {
                case SpecialApply.gravitylost:
                    gravityloststart = () =>
                    {
                        _Center.SetUsingGravity(false);
                    };
                    gravitylostend = () =>
                    {
                        _Center.SetUsingGravity(true);
                    };
                    burstCoroutine = new customCoroutine(gravityloststart, 0.2f, gravitylostend);
                    _Center.buffsRunner.runSubCoroutineOfState(burstCoroutine);
                    break;
            }
        }
        v_Damages.Clear();
    }

    // 消化伤害
    public void EatDamage(DamageType damageType)
    {
        EatDamageProcess(DamagesDIC[damageType]);
        _ComboHitCount.HitCountInterrupt();
    }

    // 这个函数，是写的很丑陋，但如果纯粹是为了解决个人强迫症的话干脆别改了。其实这个产生的根本原因就是你非要把不同重量级的攻击给用枚举类型区分开
    public void ApplyDamage(V_Damage _dmg)
	{
        _Center._ResistanceManager.Resistance.Value -= 1;
		switch (_dmg.damage_type)
		{
			case DamageType.slight_damage:
                if (_Center._ResistanceManager.Resistance.Value == 0)
                    DamagesDIC[DamageType.slight_damage].Add(_dmg);
                break;
			case DamageType.light_damage:
                switch (this.weight)
				{
					case weight.light:
                        if (_Center._ResistanceManager.Resistance.Value == 0)
                        {
                            _dmg.damage_type = DamageType.heavy_damage;
                            DamagesDIC[DamageType.heavy_damage].Add(_dmg);
                            _Center.AIStateRunner.ChangeState("Hit");
                        }else{
                            DamagesDIC[DamageType.heavy_block].Add(_dmg);
                        }
                        break;
					case weight.normal:
                        if (_Center._ResistanceManager.Resistance.Value == 0)
                        {
                            DamagesDIC[DamageType.light_damage].Add(_dmg);
                            _Center.AIStateRunner.ChangeState("Hit");
                        }else{
                            DamagesDIC[DamageType.light_block].Add(_dmg);
                        }
                        break;
					case weight.heavy:
                        if (_Center._ResistanceManager.Resistance.Value == 0)
                        {
                            _dmg.damage_type = DamageType.light_damage;
                            DamagesDIC[DamageType.light_damage].Add(_dmg);
                            _Center.AIStateRunner.ChangeState("Hit");
                        }else
                        {
                            DamagesDIC[DamageType.light_block].Add(_dmg);
                        }
                        break;
					default:
                        if (_Center._ResistanceManager.Resistance.Value == 0)
                        {
                            DamagesDIC[DamageType.light_damage].Add(_dmg);
                            _Center.AIStateRunner.ChangeState("Hit");
                        }else{
                            DamagesDIC[DamageType.light_block].Add(_dmg);
                        }
                        break;
				}
				break;
			case DamageType.heavy_damage:
                switch (this.weight)
				{
					case weight.light:
                        if (_Center._ResistanceManager.Resistance.Value == 0)
                        {
                            _dmg.damage_type = DamageType.supper_damage;
                            DamagesDIC[DamageType.supper_damage].Add(_dmg);
                            _Center.AIStateRunner.ChangeState("Hit");
                        }else{
                            DamagesDIC[DamageType.heavy_block].Add(_dmg);
                        }
                        break;
					case weight.normal:
                        if (_Center._ResistanceManager.Resistance.Value == 0)
                        {
                            DamagesDIC[DamageType.heavy_damage].Add(_dmg);
                            _Center.AIStateRunner.ChangeState("Hit");
                        }else{
                            DamagesDIC[DamageType.heavy_block].Add(_dmg);
                        }
                        break;
					case weight.heavy:
                        if (_Center._ResistanceManager.Resistance.Value == 0)
                        {
                            _dmg.damage_type = DamageType.light_damage;
                            DamagesDIC[DamageType.light_damage].Add(_dmg);
                            _Center.AIStateRunner.ChangeState("Hit");
                        }else{
                            DamagesDIC[DamageType.light_block].Add(_dmg);
                        }
                        break;
					default:
                        if (_Center._ResistanceManager.Resistance.Value == 0)
                        {
                            DamagesDIC[DamageType.heavy_damage].Add(_dmg);
                            _Center.AIStateRunner.ChangeState("Hit");
                        }else{
                            DamagesDIC[DamageType.heavy_block].Add(_dmg);
                        }
                        break;
				}
				break;
			case DamageType.supper_damage:
                switch (this.weight)
				{
					case weight.light:
                    if (_Center._ResistanceManager.Resistance.Value == 0)
                    {
                        _dmg.damage_type = DamageType.knockOff_damage;
                        DamagesDIC[DamageType.knockOff_damage].Add(_dmg);
                        _Center.AIStateRunner.ChangeState("KnockOff");
                    }else{
                        DamagesDIC[DamageType.heavy_block].Add(_dmg);
                    }
                        break;
					case weight.normal:
                    if (_Center._ResistanceManager.Resistance.Value == 0)
                    {
                        DamagesDIC[DamageType.supper_damage].Add(_dmg);
                        _Center.AIStateRunner.ChangeState("Hit");
                    }else{
                        DamagesDIC[DamageType.heavy_block].Add(_dmg);
                    }
                        break;
					case weight.heavy:
                    if (_Center._ResistanceManager.Resistance.Value == 0)
                    {
                        _dmg.damage_type = DamageType.heavy_damage;
                        DamagesDIC[DamageType.heavy_damage].Add(_dmg);
                        _Center.AIStateRunner.ChangeState("Hit");
                    }else{
                        DamagesDIC[DamageType.heavy_block].Add(_dmg);
                    }
                        break;
					default:
                    if (_Center._ResistanceManager.Resistance.Value == 0)
                    {
                        DamagesDIC[DamageType.supper_damage].Add(_dmg);
                        _Center.AIStateRunner.ChangeState("Hit");
                    }else{
                        DamagesDIC[DamageType.heavy_block].Add(_dmg);
                    }
                    break;
				}
				break;
			case DamageType.knockOff_damage:
                DamagesDIC[DamageType.knockOff_damage].Add(_dmg);
                _Center.AIStateRunner.ChangeState("KnockOff");
                break;
            case DamageType.deathknockoff:
                DamagesDIC[DamageType.deathknockoff].Add(_dmg);
                break;
            case DamageType.light_block:
                DamagesDIC[DamageType.light_block].Add(_dmg);
                break;
			case DamageType.heavy_block:
                DamagesDIC[DamageType.heavy_block].Add(_dmg);
                break;
			case DamageType.stagger:
                DamagesDIC[DamageType.stagger].Add(_dmg);
                break;
        }
	}
    
    public void HitCountPlus() => this._ComboHitCount.HitCountPlus();//打别人计数
    public int GetBeHitCount() => this._BeHitCount.getBeHitCount(); //自己被揍计数
    public void BeHitCountInterrupt() => this._BeHitCount.BeHitCountInterrupt();

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
    public List<V_Damage> ReturnDamageList(DamageType type)
    {
        return DamagesDIC.ContainsKey(type) ? DamagesDIC[type] : null;
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