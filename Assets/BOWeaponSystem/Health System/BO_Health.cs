using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using EZObjectPools;
using System.Linq;
using System;

//整个脚本中有若干个量并没有在本脚本内进行任何计算，就是起了个作为属性被参考的作用，原因在于为了保持BO武器脚本只参考BO系列文件的状态。
public partial class BO_Health : MonoBehaviour
{
	[Tooltip("与健康体同级的那个collider作不作为伤害判断?")]
	public bool collider_on_health = false; //固定值 虽然这个值本身没有在本脚本中进行任何计算，但由于BO_Health会频繁访问BO_Health，所以如果需要这样一个参数，放在这里仍然合适
	[Tooltip("角色重量级")]
	public weight weight = weight.normal;
    public int _health;

    [Tooltip("变色器")]
    public ShaderManager _ShaderManager;

    private knockOffCount _knockOffCount;
    private ComboHitCount _ComboHitCount;
    private BeHitCount _BeHitCount;
    
    private bool canBodyPush = true;//这个值的存在并不是为了突出角色“体重”而是为了让一些冲击类技能的穿透性不会受“角色互斥系统”的影响
    private bool gettingdamage = false;
    private List<BO_Hitbox> myBOHitBoxeComponent = new List<BO_Hitbox>();//713添加
    private Collider ColliderOnWhole;
    private Transform _healthCenterTransform; //本地初始化值，向datacenter看齐
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

    private List<e_Damage> Event_Damage_List;
    private List<v_Damage> ICauseDamages;
    private List<int> ICauseDamagersForAttackSteppingCommand;
    private List<Collider> myColliders;

    //研究了半天的结论是。。。只要这个健康体不是本客户端的，上面这些列表按时候清空一下就可以。甚至不清空也没事，
    //因为对面的stateRunner不会在本客户端上跑，那意味着访问这些东西的hurtstate的进入条件判读什么不会执行，就压根不会读这些信息，
    //所以这些信息怎么也没事。从而，我们把握本地客户端不跑对面staterunner这件事到底能决定哪些值可以不理会。
    private e_Damage managingEventDamage = null;
    private List<e_Damage> Event_Attack_Successed_List;
    private BO_Shield _shield;  //it's referenced in other scripts 本地初期化值，向datacenter看齐
    private AI_DATA_CENTER AI_DATA_CENTER;
    private List<Vector3> houtui;
    private Vector3 averageDirection;
    private Vector3 forceDirection;

    void Awake()
    {
        canBodyPush = true;
        AI_DATA_CENTER = gameObject.GetComponent<AI_DATA_CENTER>();
        houtui = new List<Vector3>();
        ColliderOnWhole = gameObject.GetComponent<Collider>();

        Event_Damage_List = new List<e_Damage>();
        Event_Attack_Successed_List = new List<e_Damage>();

        ICauseDamages = new List<v_Damage>();
        ICauseDamagersForAttackSteppingCommand = new List<int>();

        if (AI_DATA_CENTER != null)
        {
            if (AI_DATA_CENTER.geometryCenter != null)
            {
                this._healthCenterTransform = AI_DATA_CENTER.geometryCenter;//固定的，所以不需要作为网络角色初始化工作进行 
            }
        }

        _knockOffCount = new knockOffCount();
        _ComboHitCount = new ComboHitCount();
        _BeHitCount = new BeHitCount();

        registerMyDefaultMaterialsShaderDic();
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

    void Start()
    {
        if (this._healthCenterTransform == null)
        {
            this._healthCenterTransform = transform;
        }
    }

    void Update()
    {
        if (DamagesDIC[damageType.slight_damage].Count > 0)//这里有无法理解的报错
        {
            eatDamage(damageType.slight_damage);
        }
        _knockOffCount.update();
        _ComboHitCount.update();
        _BeHitCount.update();

        if (canBodyPush)
        {
            if (houtui != null)
            {
                averageDirection = Vector3.zero;
                if (houtui.Count > 0)
                {
                    foreach (Vector3 t in houtui)
                    {
                        averageDirection += t;
                    }
                    averageDirection /= houtui.Count;
                    forceDirection = (gameObject.transform.position - averageDirection).normalized;
                    forceDirection.y = 0f;
                    //Rigidbody.velocity = forceDirection * 10f;
                    //Rigidbody.AddForce(forceDirection * 20f, ForceMode.VelocityChange);
                    //gameObject.transform.position += forceDirection;
                    gameObject.transform.position =
                                  Vector3.Lerp(gameObject.transform.position, gameObject.transform.position + forceDirection * 100, Time.deltaTime);//这个100是主观的。
                }
                houtui.Clear();
            }
        }
        else
        {
            houtui.Clear();
        }
    }

    void FixedUpdate()
    {
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!AI_DATA_CENTER.getRunner().showMode)
        {
            if (!AI_DATA_CENTER.IsGrounded())
                addHoutuiForcePoint(collision.contacts[0].otherCollider.transform.position);//这个可能选择“接触点”比较合适。

            if (ifStepOnEnemyCharacter(collision.collider))
            {
                if (AI_DATA_CENTER.Sensor != null)
                    AI_DATA_CENTER.Sensor.getInnerEnemiesColliders().Add(collision.collider);
                WhenIHitSomethingEnemy(1);
            }
        }
    }

    public void setBodyPushFlag(bool flag)
    {
        canBodyPush = flag;
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
        if (ColliderOnWhole != null)
            ColliderOnWhole.isTrigger = !_bool;
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

        ColliderOnWhole = gameObject.GetComponent<Collider>();
        if (ColliderOnWhole != null)
        {
            myColliders.Add(ColliderOnWhole);
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

    public void WhenIHitSomethingEnemy(int _dmg)
    {
        if (ICauseDamagersForAttackSteppingCommand != null)
        {
            ICauseDamagersForAttackSteppingCommand.Add(_dmg);
        }
    }
    public void clearHitCountForAttackStepping()
    {
        ICauseDamagersForAttackSteppingCommand.Clear();
    }
    public bool IHitSomethingEnemy()
    {
        if (ICauseDamagersForAttackSteppingCommand.Count > 0)
        {
            return true;
        }else{
            return false;
        }
    }

	public Transform getHealthBodyCenterTransform()
	{
		return _healthCenterTransform;
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
            if (_dmg == null)
                continue;

            _BeHitCount.BeHitCountPlus();
            _health -= _dmg._damage;

            if (_dmg.fromWeapon != null)
            {
                if (_dmg.fromWeapon.getWeaponOwnerHealth() != null)
                    _dmg.fromWeapon.getWeaponOwnerHealth().HitCountPlus();
            }

            if (_health <= 0)
            {
                v_Damage deathknockOff = new v_Damage(0,
                  damageType.deathknockoff,
                  _dmg.force_direction,
                  _dmg.damageHappenPoint,
                  _dmg.toWho,
                  _dmg.fromWeapon);
                this.ApplyDamage(deathknockOff);
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

    private int resistance = 0;
    public int Resistance
    {
        get
        {
            return resistance;
        }
        set
        {
            resistance = Mathf.Clamp(value, 0, 10);//就是说角色最大ex槽最大100呗。
        }
    }

    //那么这个函数现在有一个作用在于为反击触发事件做准备。
    private string nextcounterevent;
    private int nextcountereventneeddamagecount;
    public string getNextCounterEventName()
    {
        return nextcounterevent;
    }
    public void setNextCounterEventName(string name)
    {
        nextcounterevent = name;
    }
    public int getNextCounterEventDamageTriggerAmount()
    {
        return nextcountereventneeddamagecount;
    }
    public void resistanceUp(AnimationEvent R)
    {
        //defaultPools.Instance.GenerateEffect("resistanceUp", null, _healthCenterTransform.position, _healthCenterTransform.rotation, null);
        Resistance += R.intParameter;
        if (resistance > 0)
        {
            if (_ShaderManager != null)
                _ShaderManager.RimEffectsUp(new Color(1f, 1f, 0.8f), (float)Resistance / 5, 0.05f);
        }
        else
        {
            if (_ShaderManager != null)
                _ShaderManager.RimEffectsClear();
        }
        nextcountereventneeddamagecount = R.intParameter - 1;
        nextcounterevent = R.stringParameter;
    }
    
    public void resistanceClear()
    {
        Resistance = 0;
        if (_ShaderManager != null)
            _ShaderManager.RimEffectsClear();
    }

    public void ApplyDamage(v_Damage _dmg)
	{
		//if ((PhotonNetwork.connected && !photonView.isMine))
		//{
		//	return;//同步系统中伤害判定最关键的一条逻辑
		//}

		//colorTransition =
		//          ColorTransition.DoTransition(gameObject, new GradientColorKey[] { new GradientColorKey(Color.red, 0.2f), new GradientColorKey(Color.white, 0.2f) }, true, 0f);

		//UnityEngine.Events.UnityAction resetColor = () => { colorTransition.ResetColors(); };
		//      colorTransition.OnEnd.AddListener(resetColor);
		////colorTransition.AfterTransition = AfterTransitionActions.DisableComponent;
		//colorTransition.Duration = 0.2f;
		//colorTransition.LoopMode = TransitionLoopModes.PlayOnce;

        Resistance -= 1;

		switch (_dmg.damage_type)
		{
			case damageType.slight_damage:
                if (Resistance == 0)
                    DamagesDIC[damageType.slight_damage].Add(_dmg);
                break;
			case damageType.light_damage:
                switch (this.weight)
				{
					case weight.light:
                        if (Resistance == 0)
                        {
                            _dmg.damage_type = damageType.heavy_damage;
                            DamagesDIC[damageType.heavy_damage].Add(_dmg);
                        }else{
                            DamagesDIC[damageType.heavy_block].Add(_dmg);
                        }
                        break;
					case weight.normal:
                        if (Resistance == 0)
                        {
                            DamagesDIC[damageType.light_damage].Add(_dmg);
                        }else{
                            DamagesDIC[damageType.light_block].Add(_dmg);
                        }
                        break;
					case weight.heavy:
                        if (Resistance == 0)
                        {
                            _dmg.damage_type = damageType.light_damage;
                            DamagesDIC[damageType.light_damage].Add(_dmg);
                        }else
                        {
                            DamagesDIC[damageType.light_block].Add(_dmg);
                        }
                        break;
					default:
                        if (Resistance == 0)
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
                        if (Resistance == 0)
                        {
                            _dmg.damage_type = damageType.supper_damage;
                            DamagesDIC[damageType.supper_damage].Add(_dmg);
                        }else{
                            DamagesDIC[damageType.heavy_block].Add(_dmg);
                        }
                        break;
					case weight.normal:
                        if (Resistance == 0)
                        {
                            DamagesDIC[damageType.heavy_damage].Add(_dmg);
                        }else{
                            DamagesDIC[damageType.heavy_block].Add(_dmg);
                        }
                        break;
					case weight.heavy:
                        if (Resistance == 0)
                        {
                            _dmg.damage_type = damageType.light_damage;
                            DamagesDIC[damageType.light_damage].Add(_dmg);
                        }else{
                            DamagesDIC[damageType.light_block].Add(_dmg);
                        }
                        break;
					default:
                        if (Resistance == 0)
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
                    if (Resistance == 0)
                    {
                        _dmg.damage_type = damageType.knockOff_damage;
                        DamagesDIC[damageType.knockOff_damage].Add(_dmg);
                    }else{
                        DamagesDIC[damageType.heavy_block].Add(_dmg);
                    }
                        break;
					case weight.normal:
                    if (Resistance == 0)
                    {
                        DamagesDIC[damageType.supper_damage].Add(_dmg);
                    }else{
                        DamagesDIC[damageType.heavy_block].Add(_dmg);
                    }
                        break;
					case weight.heavy:
                    if (Resistance == 0)
                    {
                        _dmg.damage_type = damageType.heavy_damage;
                        DamagesDIC[damageType.heavy_damage].Add(_dmg);
                    }else{
                        DamagesDIC[damageType.heavy_block].Add(_dmg);
                    }
                        break;
					default:
                    if (Resistance == 0)
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
        if (_ShaderManager != null)
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
            _ShaderManager.RimEffectsForAShortTime(0.8f, speed, time, damagecolor);
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

	public AI_DATA_CENTER getMyDATACENTER()
	{
		return AI_DATA_CENTER;
	}

	public void addHoutuiForcePoint(Vector3 point)
	{
		if (!houtui.Contains(point))
			houtui.Add(point);
	}

	public bool ifStepOnEnemyCharacter(Collider box)
	{
        if (box == null)
            return false;
		if (box.isTrigger)
			return false;
		if (AI_DATA_CENTER == null)
			return false;
		if (AI_DATA_CENTER._TeamConfig == null)
			return false;

        //这个comment out 的写法貌似效果是一样的。
        //if ((AI_DATA_CENTER._TeamConfig.enemyLayerMask & (1 << box.gameObject.layer)) != 0
        //||
        //(AI_DATA_CENTER._TeamConfig.enemyShieldLayerMask & (1 << box.gameObject.layer)) != 0)
        if (AI_DATA_CENTER._TeamConfig.enemyLayerMask == (AI_DATA_CENTER._TeamConfig.enemyLayerMask | (1 << box.gameObject.layer))
            ||
            (AI_DATA_CENTER._TeamConfig.enemyShieldLayerMask & (1 << box.gameObject.layer)) != 0)
        {
			return true;
		}
		return false;
	}

    public bool ifStepOnFriendCharacter(Collider box)
    {
        if (AI_DATA_CENTER == null)
            return false;
        if (AI_DATA_CENTER._TeamConfig == null)
            return false;

        if ((AI_DATA_CENTER._TeamConfig.mylayer ==  box.gameObject.layer)
           ||
            AI_DATA_CENTER._TeamConfig.myShieldLayer ==  box.gameObject.layer)
        {
            return true;
        }
        return false;
    }

    public void HitCountPlus()//打别人计数
    {
        this._ComboHitCount.HitCountPlus();
    }

    public int getHitCount()//打别人计数
    {
        return this._ComboHitCount.getHitCount();
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

public class ComboHitCount
{
    int hitCount;
    float HitConnectTolerate;
    float HItComboTimeCounter;

    public ComboHitCount()
    {
        hitCount = 0;
        HitConnectTolerate = 4f;
        HItComboTimeCounter = 0f;
    }

    public void HitCountPlus()
    {
        HItComboTimeCounter = HitConnectTolerate;
        hitCount += 1;
    }

    public void HitCountInterrupt()
    {
        hitCount = 0;
        HItComboTimeCounter = 0;
    }

    public int getHitCount()
    {
        return hitCount;
    }

    public void update()
    {
        if (HItComboTimeCounter > 0f)
        {
            HItComboTimeCounter -= Time.deltaTime;
            if (HItComboTimeCounter <= 0f)
            {
                hitCount = 0;
            }
        }
    }
}

public class BeHitCount
{
    int beHitCount;
    float HitConnectTolerate;
    float BeHItComboTimeCounter;

    public BeHitCount()
    {
        beHitCount = 0;
        HitConnectTolerate = 1.5f;
        BeHItComboTimeCounter = 0f;
    }

    public void BeHitCountPlus()
    {
        BeHItComboTimeCounter = HitConnectTolerate;
        beHitCount += 1;
    }

    public void BeHitCountInterrupt()
    {
        beHitCount = 0;
        BeHItComboTimeCounter = 0;
    }

    public int getBeHitCount()
    {
        return beHitCount;
    }

    public void update()
    {
        if (BeHItComboTimeCounter > 0f)
        {
            BeHItComboTimeCounter -= Time.deltaTime;
            if (BeHItComboTimeCounter <= 0f)
            {
                beHitCount = 0;
            }
        }
    }
}

public class knockOffCount
{
    float knock_off_time_counter;
	float knock_off_cooldown_infer;
	float knock_off_gauge;

	public knockOffCount()
	{
		knock_off_time_counter = 1f;
		knock_off_cooldown_infer = 1f;
		knock_off_gauge = 0;
	}

	public void setGauge(float amount)
	{
		this.knock_off_gauge = amount;
	}

	public void plusGauge(float amount)
	{
		knock_off_gauge += amount;
	}

	public void plusTimeCounter(float timeamout)
	{
		this.knock_off_time_counter += timeamout;
	}

	public float getGauge()
	{
		return knock_off_gauge;
	}

	public void update()
	{
        //底下这些就是说，我每隔一段时间就让KockoFF槽冷却一些(降低2)
		if (knock_off_gauge > 0)
		{
			if (knock_off_time_counter > 0)
            {
                knock_off_time_counter -= Time.deltaTime;
            }
			if (knock_off_time_counter <= 0)
			{
				knock_off_gauge = Mathf.Clamp(knock_off_gauge - 2f, 0, Mathf.Infinity);
				knock_off_time_counter = knock_off_cooldown_infer;
			}
		}
	}
}

public enum weight : int
{
	normal = 1,
	light = 2,
	heavy = 3
}