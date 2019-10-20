using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using System.Linq;
using HittingDetection;

//整个脚本中有若干个量并没有在本脚本内进行任何计算，就是起了个作为属性被参考的作用，原因在于为了保持BO武器脚本只参考BO系列文件的状态。
public partial class BO_Health : MonoBehaviour
{
	//[Tooltip("与健康体同级的那个collider作不作为伤害判断?")]
	//public bool collider_on_health = false; //固定值 虽然这个值本身没有在本脚本中进行任何计算，但由于BO_Health会频繁访问BO_Health，所以如果需要这样一个参数，放在这里仍然合适
	[Tooltip("角色重量级")]
	public weight weight = weight.normal;
    
    [Tooltip("数据中心")]
    public Data_Center _Center;

    [Tooltip("当前攻击力")]
    public float AT = 10;

    public ReactiveProperty<float> CurrentHp { get; set; } = new ReactiveProperty<float>();
    public ComboHitCount _ComboHitCount = new ComboHitCount();
    
    public static List<Collider> AllMeatColliders = new List<Collider>();    
    private knockOffCount _knockOffCount = new knockOffCount();    
    private BeHitCount _BeHitCount= new BeHitCount();        
    private bool gettingdamage = false;
    private List<BO_Hitbox> myBOHitBoxeComponent = new List<BO_Hitbox>();//713添加
    private List<e_Damage> Event_Damage_List = new List<e_Damage>();
    private List<v_Damage> ICauseDamages = new List<v_Damage>();
    private List<Collider> myColliders;
    private e_Damage managingEventDamage = null;
    private List<e_Damage> Event_Attack_Successed_List = new List<e_Damage>();
    private BO_Shield _shield;  //it's referenced in other scripts 本地初期化值，向datacenter看齐
    private IDictionary<damageType, List<v_Damage>> DamagesDIC = new Dictionary<damageType, List<v_Damage>>()
    {
        {damageType.slight_damage,new List<v_Damage>()},
        {damageType.light_damage,new List<v_Damage>()},
        {damageType.heavy_damage,new List<v_Damage>()},
        {damageType.supper_damage,new List<v_Damage>()},
        {damageType.heavy_block,new List<v_Damage>()},
        {damageType.stagger,new List<v_Damage>()},
        {damageType.light_block,new List<v_Damage>()},
        {damageType.knockOff_damage,new List<v_Damage>()},
        {damageType.deathknockoff,new List<v_Damage>()}
    };
    
    UnityEngine.Events.UnityAction gravityloststart;
    UnityEngine.Events.UnityAction gravitylostend;
    customCoroutine burstCoroutine;

    void Awake()
    {
        //registerMyDefaultMaterialsShaderDic();
    }
    
    public void clearDamageLists()
    {
        foreach (KeyValuePair<damageType, List<v_Damage>> _keyvaluepair in DamagesDIC)
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
        return this.gettingdamage;
    }

    public void HealthBodyFixedUpdate()
    {
        if (DamagesDIC[damageType.slight_damage].Count > 0)//这里有无法理解的报错
        {
            eatDamage(damageType.slight_damage);
        }
        _knockOffCount.update();
        _ComboHitCount.update();
        _BeHitCount.update();
    }

    public knockOffCount GetKnockOffCount()
    {
        return _knockOffCount;
    }

    public void addToBOHitBoxeComponent(BO_Hitbox bO_Hitbox)
    {
        if (!myBOHitBoxeComponent.Contains(bO_Hitbox))
        {
            myBOHitBoxeComponent.Add(bO_Hitbox);
        }
    }

    public void enableAllHitBoxCollider(bool _bool)
    {
        if (myBOHitBoxeComponent != null)
        {
            foreach (BO_Hitbox hitbox in myBOHitBoxeComponent)
            {
                if (hitbox.myColliderMustEquip != null)
                    hitbox.myColliderMustEquip.isTrigger = !_bool;
            }
        }
    }
    
    public bool ifMyBody(Collider collider)
    {
        if (myColliders.Contains(collider))
            return true;
        else
            return false;
    }

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

    public void changeLayerForAllSelfColliders(int layer)
    {
        if (myBOHitBoxeComponent != null)
        {
            foreach (BO_Hitbox _BO_Hitbox in myBOHitBoxeComponent)
            {
                _BO_Hitbox.gameObject.layer = layer;
            }
        }
        this.gameObject.layer = layer;
    }

    public void myDamageCount(v_Damage _dmg)
    {
        if (this.ICauseDamages != null)
        {
            this.ICauseDamages.Add(_dmg);
        }
    }

	public void setManagingEventDamage(e_Damage e)
	{
		this.managingEventDamage = e;
	}
	public e_Damage getManagingEventDamage()
	{
		return this.managingEventDamage;
	}
	public void eventAttackHitApprove(e_Damage e)
	{
		this.Event_Attack_Successed_List.Add(e);
	}
	public void addEventDamageList(e_Damage e)
	{
		e.setDamagedHealthBody(this);
		this.Event_Damage_List.Add(e);
	}
	public List<e_Damage> returnEventDamageList()
	{
		return this.Event_Damage_List;
	}
	public List<e_Damage> returnApprovedEventAttackAttempts()
	{
		return this.Event_Attack_Successed_List;
	}
	public List<v_Damage> returnDamageList(damageType type)
	{
        if (DamagesDIC.ContainsKey(type))
            return DamagesDIC[type];
        else
            return null;
	}

    public void setShield(BO_Shield _shield)
	{
		this._shield = _shield;
	}
	public BO_Shield getShield()
	{
		return this._shield;
	}

    public void eatDamageProcess(List<v_Damage> v_Damages)
    {
        foreach(v_Damage _dmg in v_Damages.ToList())//这个ToList()不加的话可能报错：Collection was modified; enumeration operation may not execute
        {
            _BeHitCount.BeHitCountPlus();

            if (_dmg.fromWeapon != null)
            {
                if (_dmg.fromWeapon.GetWeaponOwnerHealth() != null)
                    _dmg.fromWeapon.GetWeaponOwnerHealth().HitCountPlus();
                CurrentHp.Value -= _dmg.AT;
                if (CurrentHp.Value <= 0)
                {
                    v_Damage deathknockOff;
                    deathknockOff = new v_Damage(
                                  damageType.deathknockoff,
                                  _dmg.force_direction,
                                  _dmg.damageHappenPoint,
                                  _dmg.toWho,
                                  _dmg.fromWeapon);
                    this.ApplyDamage(deathknockOff);
                    _Center.IsDead.Value = true;
                    this._Center.AIStateRunner.changeState("Death");
                    break;//重点在于，不再继续接下来的清空伤害列表操作。由死亡状态清空伤害列表，因为死亡状态需要这个列表来进行最后的击飞演出
                }
            }
            
            switch(_dmg.specialApply)
            {
                case specialApply.gravitylost:
                    gravityloststart = () =>
                    {
                        _Center.setGravitySwitch(false);
                    };
                    gravitylostend = () =>
                    {
                        _Center.setGravitySwitch(true);
                    };
                    burstCoroutine = new customCoroutine(gravityloststart, 0.2f, gravitylostend);
                    _Center.buffsRunner.runSubCoroutineOfState(burstCoroutine);
                    break;
                default:
                    break;
            }            
        }
        v_Damages.Clear();
    }

    // 消化伤害
    public void eatDamage(damageType damageType)
    {
        eatDamageProcess(DamagesDIC[damageType]);
        this._ComboHitCount.HitCountInterrupt();
    }

    public void ApplyDamage(v_Damage _dmg)
	{
		//if ((PhotonNetwork.connected && !photonView.isMine))
		//	return;//同步系统中伤害判定最关键的一条逻辑

		//colorTransition =
		//          ColorTransition.DoTransition(gameObject, new GradientColorKey[] { new GradientColorKey(Color.red, 0.2f), new GradientColorKey(Color.white, 0.2f) }, true, 0f);

		//UnityEngine.Events.UnityAction resetColor = () => { colorTransition.ResetColors(); };
		//      colorTransition.OnEnd.AddListener(resetColor);
		////colorTransition.AfterTransition = AfterTransitionActions.DisableComponent;
		//colorTransition.Duration = 0.2f;
		//colorTransition.LoopMode = TransitionLoopModes.PlayOnce;

        _Center._ResistanceManager.Resistance.Value -= 1;
		switch (_dmg.damage_type)
		{
			case damageType.slight_damage:
                if (_Center._ResistanceManager.Resistance.Value == 0)
                    DamagesDIC[damageType.slight_damage].Add(_dmg);
                break;
			case damageType.light_damage:
                switch (this.weight)
				{
					case weight.light:
                        if (_Center._ResistanceManager.Resistance.Value == 0)
                        {
                            _dmg.damage_type = damageType.heavy_damage;
                            DamagesDIC[damageType.heavy_damage].Add(_dmg);
                        }else{
                            DamagesDIC[damageType.heavy_block].Add(_dmg);
                        }
                        break;
					case weight.normal:
                        if (_Center._ResistanceManager.Resistance.Value == 0)
                        {
                            DamagesDIC[damageType.light_damage].Add(_dmg);
                        }else{
                            DamagesDIC[damageType.light_block].Add(_dmg);
                        }
                        break;
					case weight.heavy:
                        if (_Center._ResistanceManager.Resistance.Value == 0)
                        {
                            _dmg.damage_type = damageType.light_damage;
                            DamagesDIC[damageType.light_damage].Add(_dmg);
                        }else
                        {
                            DamagesDIC[damageType.light_block].Add(_dmg);
                        }
                        break;
					default:
                        if (_Center._ResistanceManager.Resistance.Value == 0)
                        {
                            DamagesDIC[damageType.light_damage].Add(_dmg);
                        }else{
                            DamagesDIC[damageType.light_block].Add(_dmg);
                        }
                        break;
				}
				break;
			case damageType.heavy_damage:
                switch (this.weight)
				{
					case weight.light:
                        if (_Center._ResistanceManager.Resistance.Value == 0)
                        {
                            _dmg.damage_type = damageType.supper_damage;
                            DamagesDIC[damageType.supper_damage].Add(_dmg);
                        }else{
                            DamagesDIC[damageType.heavy_block].Add(_dmg);
                        }
                        break;
					case weight.normal:
                        if (_Center._ResistanceManager.Resistance.Value == 0)
                        {
                            DamagesDIC[damageType.heavy_damage].Add(_dmg);
                        }else{
                            DamagesDIC[damageType.heavy_block].Add(_dmg);
                        }
                        break;
					case weight.heavy:
                        if (_Center._ResistanceManager.Resistance.Value == 0)
                        {
                            _dmg.damage_type = damageType.light_damage;
                            DamagesDIC[damageType.light_damage].Add(_dmg);
                        }else{
                            DamagesDIC[damageType.light_block].Add(_dmg);
                        }
                        break;
					default:
                        if (_Center._ResistanceManager.Resistance.Value == 0)
                        {
                            DamagesDIC[damageType.heavy_damage].Add(_dmg);
                        }else{
                            DamagesDIC[damageType.heavy_block].Add(_dmg);
                        }
                        break;
				}
				break;
			case damageType.supper_damage:
                switch (this.weight)
				{
					case weight.light:
                    if (_Center._ResistanceManager.Resistance.Value == 0)
                    {
                        _dmg.damage_type = damageType.knockOff_damage;
                        DamagesDIC[damageType.knockOff_damage].Add(_dmg);
                    }else{
                        DamagesDIC[damageType.heavy_block].Add(_dmg);
                    }
                        break;
					case weight.normal:
                    if (_Center._ResistanceManager.Resistance.Value == 0)
                    {
                        DamagesDIC[damageType.supper_damage].Add(_dmg);
                    }else{
                        DamagesDIC[damageType.heavy_block].Add(_dmg);
                    }
                        break;
					case weight.heavy:
                    if (_Center._ResistanceManager.Resistance.Value == 0)
                    {
                        _dmg.damage_type = damageType.heavy_damage;
                        DamagesDIC[damageType.heavy_damage].Add(_dmg);
                    }else{
                        DamagesDIC[damageType.heavy_block].Add(_dmg);
                    }
                        break;
					default:
                    if (_Center._ResistanceManager.Resistance.Value == 0)
                    {
                        DamagesDIC[damageType.supper_damage].Add(_dmg);
                    }else{
                        DamagesDIC[damageType.heavy_block].Add(_dmg);
                    }
                    break;
				}
				break;
			case damageType.knockOff_damage:
                DamagesDIC[damageType.knockOff_damage].Add(_dmg);
                break;
            case damageType.deathknockoff:
                DamagesDIC[damageType.deathknockoff].Add(_dmg);
                break;
            case damageType.light_block:
                DamagesDIC[damageType.light_block].Add(_dmg);
                break;
			case damageType.heavy_block:
                DamagesDIC[damageType.heavy_block].Add(_dmg);
                break;
			case damageType.stagger:
                DamagesDIC[damageType.stagger].Add(_dmg);
                break;
			default:
				break;
		}
	}

	private Shader One_Shader = null; //"ShurikenMagic/TransparentRim"   //
    public void swapShader(string shaderName)
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
    void registerMyDefaultMaterialsShaderDic()
    {
        myDefaultMaterialsShaderDic = new Dictionary<int, Shader>();
        foreach (Renderer singleRenderer in GetComponentsInChildren<Renderer>())
        {
            if (singleRenderer is ParticleSystemRenderer)
            {
                continue;
            }
            foreach (Material singleMaterial in singleRenderer.materials)
            {
                if (!myDefaultMaterialsShaderDic.ContainsKey(singleMaterial.GetHashCode()))
                {
                    myDefaultMaterialsShaderDic.Add(new KeyValuePair<int, Shader>(singleMaterial.GetHashCode(), singleMaterial.shader));
                }
            }
        }
    }

    public void restoreMyDefaultMaterialsShaders()
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

	public Coroutine process;
    private Color damagecolor;
    public void runShaderChangeProcess(string magicKind,float speed,float time)
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

    public IEnumerator shaderChange(string magicKind,float time)
    {
		switch(magicKind)
		{
			case "redMagic":
				swapShader("Hurt/Red");
				break;
			case "blueMagic":
				swapShader("Hurt/Red");
				break;
			case "greenMagic":
				swapShader("Hurt/Red");
				break;
			default:
				swapShader("Hurt/Red");
				break;
		}
		yield return new WaitForSeconds(time);
        restoreMyDefaultMaterialsShaders();
    }

    public void HitCountPlus()//打别人计数
    {
        this._ComboHitCount.HitCountPlus();
    }

    public int getBeHitCount()//自己被揍计数
    {
        return this._BeHitCount.getBeHitCount();
    }

    public void BeHitCountInterrupt()//自己被揍计数
    {
        this._BeHitCount.BeHitCountInterrupt();
    }
}