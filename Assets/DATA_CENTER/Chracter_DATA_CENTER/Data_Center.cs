using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum zokusei : int
{
    redMagic = 1,
    blueMagic = 2,
    greenMagic = 3,
    darkMagic = 4,
    lightMagic = 6,
    Null = 7
}

//Basically, Data_Center is a place where all parameter need applying to a character are initiazlized,
//Those paremeters are sent to all parts of character from here.
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(BO_Weapon_Animation_Events))]
[RequireComponent(typeof(Animation_Manger))]
[RequireComponent(typeof(BO_Health))]
[RequireComponent(typeof(SkillCancelFlag))]
[RequireComponent(typeof(AIStateRunner))]
[RequireComponent(typeof(BO_Ani_E))]
public abstract partial class Data_Center : MonoBehaviour
{
    //下面这个三级初始化的问题，尤其要主意：那三部初始化并非在分别完成过一次后再运行就一定什么也不错，里面的工作是分部分的。
    protected bool phase1Initialized = false, phase2Initialized = false, phase3Initialized = false;
    public bool ifPreparedForBattle()
    {
        if (phase1Initialized && phase2Initialized && phase3Initialized)
        {
            return true;
        }else{
            return false;
        }
    }

    //以下的这三个信息各有所用。首先队伍适配信息不光是角色本身，而且全身各个武器和hitbox的初始化也和这个量息息相关，
    // 然后_CharacterDataInfo 事关GUI，角色信息啦浮动血跳什么的都和它相关，而_playerBattleInfo则是这场战斗为角色分配的攻击力和HP量
    // 因为我们这个游戏不是靠角色等级决定HP等属性，往往一场战斗所有角色会分配固定血量，因此事关总体血量和攻击力分配。
    public TeamConfig _TeamConfig;
    public CharacterDataInfo _CharacterDataInfo;
    public playerBattleInfo _playerBattleInfo;

    public zokusei Zokusei;

    public Transform geometryCenter;
    public Transform floorChecks;
    private Transform[] floorCheckers;
    float groundedCount = 0f;
    float airCount = 0f;
    bool grounded;

    //protected float BattleRingRadius = 30f;
    //public void passBattleGroundInfo(float BattleRingRadius,Vector3 battleRingCenter)
    //{
    //    this.battleRingCenter = battleRingCenter;
    //    this.BattleRingRadius = BattleRingRadius;
    //}
    //protected Vector3 battleRingCenter;
    //public Vector3 getBattleRingCenter()
    //{
    //    return battleRingCenter;
    //}

    public Animator animator;
    protected List<BO_Marker_Manager> myWeaponList = new List<BO_Marker_Manager>();

    public Sensor Sensor;
    //protected Collider _collider;
    protected SkillCancelFlag _SkillCancelFlag;
    protected BO_Ani_E _BO_Ani_E;
    protected Rigidbody Rigidbody;
    protected BO_Health BO_Health;
    protected BO_Weapon_Animation_Events bO_Weapon_Animation_Events;
    public BO_Health getBOHealth()
	{
		return BO_Health;
	}
    protected AIStateRunner AIStateRunner;
    public AIStateRunner getRunner()
    {
        return AIStateRunner;
    }
    public Animation_Manger Animation_Manger;

    public Transform right_hand_t, left_hand_t, right_foot_t, left_foot_t,tail_t, head_t;
    public Transform left_arm_hitbox_t, right_arm_hitbox_t, left_leg_hitbox_t, right_leg_hitbox_t, spine_hitbox_t;

    [Header("WeaponOnBody")]
    [Space(9)]
    [Tooltip("All the weapons we hold")]
    public BO_Marker_Manager right_hand, left_hand, right_foot, left_foot, right_hand_weapon, left_hand_weapon,head,tail;

    [Header("传统防御盾。可能真的用不到了")]
    [Space(1)]
    public BO_Shield Shield;

    //private PhysicMaterial moca, smooth;
    private List<GameObject> deActiveObjects_List;

    public List<BO_Marker_Manager> getMyWeaponList()
    {
        return this.myWeaponList;
    }

    bool dead = false;
    public void setDeathState(bool dead)
    {
        this.dead = dead;
    }
    public bool getDeathState()
    {
        return dead;
    }

    public bool onBattleGroundBundary = false;
    public Vector3 antiWallDirection;//往墙内走的方向，防止角色AI冲着墙走。我们的游戏里角色的走位基本是基于队友和敌人，通过地形判断走位只有这一条

    public IEnumerator step1Initialize(string type, string basicPackName,string personalMagicPath)
    {
        if (!phase1Initialized)
        {
            Animation_Manger = gameObject.GetComponent<Animation_Manger>();
            AIStateRunner = gameObject.GetComponent<AIStateRunner>();
            setIfShowMode(true);
            _BO_Ani_E = gameObject.GetComponent<BO_Ani_E>();
            _SkillCancelFlag = gameObject.GetComponent<SkillCancelFlag>();
            bO_Weapon_Animation_Events = gameObject.GetComponent<BO_Weapon_Animation_Events>();
            BO_Health = gameObject.GetComponent<BO_Health>();//这个只在战斗模式需要
            BodyElementTagAndLayerSet(null);
            Rigidbody = GetComponent<Rigidbody>();//这个只在战斗模式需要
            if (!Rigidbody)
            {
                gameObject.AddComponent<Rigidbody>();
                Rigidbody = GetComponent<Rigidbody>();
            }
            Rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
            //计算量最小是Discrete，但实测设置成Continuous的话一定不会产生行走穿墙。但根据该功能注释看
            //设置成Discrete或Continuous对于角色间碰撞是一样的。（Continuous式计算只对无刚体的collider有效）这样的话考虑计算量时候还牵扯到个地面的问题。。。
            Rigidbody.useGravity = false;

            if (!animator)//这个只在战斗模式需要
            {
                animator = gameObject.GetComponent<Animator>();
                if (!animator)
                {
                    Debug.Log("Cant find animator,pls check");
                }
            }

            floorChecks = gameObject.transform.Find("FloorChecks");
            if (!floorChecks)
            {
                floorChecks = new GameObject().transform;
                floorChecks.name = "FloorChecks";
                floorChecks.parent = gameObject.transform;
                floorChecks.position = gameObject.transform.position;
                GameObject check = new GameObject();
                check.name = "Check1";
                check.transform.parent = floorChecks;
                Vector3 v = gameObject.transform.position;
                v.y = -1f;
                check.transform.position = v;
                Debug.LogWarning("No 'floorChecks' assigned to PlayerMove script, so a single floorcheck has been created", floorChecks);
            }
            floorCheckers = new Transform[floorChecks.childCount];
            for (int i = 0; i < floorCheckers.Length; i++)
            {
                floorCheckers[i] = floorChecks.GetChild(i);
            }

            bO_Weapon_Animation_Events.assignWeaponsFromDataCenter(right_hand, left_hand, right_foot, left_foot, right_hand_weapon, left_hand_weapon, head, tail);

            // 在第一级初始化中我们把两个角色武器先打开，又关闭，这起到了个非常邪门的效果：使得这两把武器的相关awake函数得以运行，在这里就是找到了相应武器的markers
            if (right_hand_weapon != null)
            {
                right_hand_weapon.gameObject.SetActive(false);
            }
            if (left_hand_weapon != null)
            {
                left_hand_weapon.gameObject.SetActive(false);
            }

            string personalEffectsPath;
            switch (this.Zokusei)
            {
                case zokusei.darkMagic:
                    personalEffectsPath = "darkMagic";
                    break;
                case zokusei.blueMagic:
                    personalEffectsPath = "blueMagic";
                    break;
                case zokusei.greenMagic:
                    personalEffectsPath = "greenMagic";
                    break;
                case zokusei.lightMagic:
                    personalEffectsPath = "lightMagic";
                    break;
                case zokusei.redMagic:
                    personalEffectsPath = "redMagic";
                    break;
                default:
                    personalEffectsPath = "defaultEffects";
                    break;
            }
            defaultPools.Instance.iniEffectsPool("short_effect", personalEffectsPath, 2);
            defaultPools.Instance.iniEffectsPool("normal_effect", personalEffectsPath, 2);
            defaultPools.Instance.iniEffectsPool("long_effect", personalEffectsPath, 2);

            switch (defaultPools.Instance.AnimationLoadingMode)
            {
                case ResourceLoadMode.CachAB:
                    yield return (Animation_Manger.preloadBasicPersonalAnims(type,basicPackName));
                    
                    break;
                case ResourceLoadMode.StreamingAssetAB:
                    yield return (Animation_Manger.preloadBasicPersonalAnimsStreamingAssetMode(type, basicPackName));
                    
                    break;
                case ResourceLoadMode.Resource:
                    yield return (Animation_Manger.preloadBasicPersonalAnimsResourceMode(type, basicPackName));
                    
                    break;
            }

            yield return _BO_Ani_E.basicMagicAndEffectsPathDefine(this.Zokusei, personalMagicPath);
            yield return _BO_Ani_E.setBodyPartsTransform(this, BO_Health);
            phase1Initialized = true;
        }
    }

    public void BodyElementTagAndLayerSet(TeamConfig _TeamConfig)
    {
        this._TeamConfig = _TeamConfig;
        if (this._TeamConfig != null)
        {
            gameObject.layer = this._TeamConfig.mylayer;
            gameObject.tag = this._TeamConfig.my_tag;
        }else{
            gameObject.layer = 0;
            gameObject.tag = "Untagged";
        }

        myWeaponList = new List<BO_Marker_Manager>();
        deActiveObjects_List = new List<GameObject>();

        if (right_hand != null)
        {
            myWeaponList.Add(right_hand);
        }
        if (left_hand != null)
        {
            myWeaponList.Add(left_hand);
        }
        if (right_foot != null)
        {
            myWeaponList.Add(right_foot);
        }
        if (left_foot != null)
        {
            myWeaponList.Add(left_foot);
        }
        if (head != null)
        {
            myWeaponList.Add(head);
        }
        if (tail != null)
        {
            myWeaponList.Add(tail);
        }
        if (right_hand_weapon != null)
        {
            right_hand_weapon.gameObject.SetActive(true);
            myWeaponList.Add(right_hand_weapon);
            addToDeActiveObjects(right_hand_weapon.gameObject);
        }
        if (left_hand_weapon != null)
        {
            left_hand_weapon.gameObject.SetActive(true);
            myWeaponList.Add(left_hand_weapon);
            addToDeActiveObjects(left_hand_weapon.gameObject);
        }

        if (Sensor == null)
        {
            Debug.Log("请手动适配sensor");
        }
        else
        {
            _SkillCancelFlag.setSensor(Sensor);
            Sensor.setDectectLayerAndFrontDirection(_TeamConfig,this);
        }

        string effectPath;
        switch (Zokusei)
        {
            case zokusei.darkMagic:
                effectPath = "darkMagic";
                break;
            case zokusei.blueMagic:
                effectPath = "blueMagic";
                break;
            case zokusei.greenMagic:
                effectPath = "greenMagic";
                break;
            case zokusei.lightMagic:
                effectPath = "lightMagic";
                break;
            case zokusei.redMagic:
                effectPath = "redMagic";
                break;
            default:
                effectPath = "defaultEffects";
                break;
        }

        defaultPools.Instance.iniEffectsPool("Sparks", effectPath, 2);
        defaultPools.Instance.iniEffectsPool("light_hit", effectPath, 2);
        defaultPools.Instance.iniEffectsPool("light_hit", effectPath, 2);
        defaultPools.Instance.iniEffectsPool("heavy_hit", effectPath, 2);
        defaultPools.Instance.iniEffectsPool("super_hit", effectPath, 2);
        defaultPools.Instance.iniEffectsPool("resistanceUp", effectPath, 2);
        defaultPools.Instance.iniEffectsPool("on_enable_effect", effectPath, 1);

        foreach (BO_Marker_Manager weapon in myWeaponList)
        {
            weapon.setTeamConfig(_TeamConfig);
            weapon.setAT(this._playerBattleInfo.AT);
            weapon.setHolderCenter(this.geometryCenter);
            weapon.setWeaponOwnerHealth(BO_Health);
            weapon.personalEffectPath = effectPath;
            weapon.DisableMarkers();
        }
        
        if (Shield != null)
        {
            Shield.gameObject.SetActive(false);
            if (BO_Health != null)
            {
                Shield.iniShield(_TeamConfig, BO_Health);
                Shield._ShieldBackSpot = this.geometryCenter;
            }
        }
    }
    
    public IEnumerator step2Initialize(string type, NineAndTwo _NineAndTwo, passiveSkillConfigs passiveSkillConfigs, int AI_level,zokusei _zokusei, string personalMagic)
    {
        if (!phase2Initialized)
        {
            AIStateRunner.characterType = type;
            phase2Initialized = true;
        }
        
        if (AIStateRunner.getReadingNineAndTwo() != _NineAndTwo || AIStateRunner.usingScriptLevel != AI_level)
        {
            AIStateRunner.FormFightingSetsByNineAndTwo(type, _NineAndTwo, passiveSkillConfigs, AI_level);
            AIStateRunner.iniStates();
            //这个环节之后我应该有一份列表来展示到底我一个角色一场战斗都能用上什么招
            // 上面这个环节结束后，有这样几个重要情况1. state_Transition_Dictionary的内容就正确了 2.AIStateRunner内的States_Dictionary实例内将有一份正确的skill类key的列表
            List<string> toLoadSkillAnimsNames = AIStateRunner.passSkillTypeKeys();
            switch (defaultPools.Instance.AnimationLoadingMode)
            {
                case ResourceLoadMode.CachAB:
                    yield return (
                        Animation_Manger.preloadPersonalAnims(AssetBundleLoader.BundleURL + "/animClips",type, toLoadSkillAnimsNames, personalMagic, _zokusei));
                break;
                case ResourceLoadMode.Resource:
                    yield return (
                        Animation_Manger.preloadPersonalAnimsResourceMode(type, toLoadSkillAnimsNames, personalMagic, _zokusei));
                break;
                case ResourceLoadMode.StreamingAssetAB:
                    yield return (
                        Animation_Manger.preloadPersonalAnimsStreamingAssetMode(type, toLoadSkillAnimsNames, personalMagic, _zokusei));
                break;
            }
        }
    }

    public void step3Initialize(TeamConfig _TeamConfig, playerBattleInfo _playerBattleInfo)//战斗必备
    {
        this._playerBattleInfo = _playerBattleInfo;
        BodyElementTagAndLayerSet(_TeamConfig);//这一步和下面的changeLayerForAllSelfColliders为什么分开？没什么为什么。就是给写开了。
        BO_Health._health = (this._playerBattleInfo.MaxHP);
        BO_Health.FindAllSelfCollidersAndIgnoreCollision();//上面那个防御盾设置保证了这一步也能把防御盾碰撞体处理。
        BO_Health.changeLayerForAllSelfColliders(_TeamConfig.mylayer);
        BO_Health.enableAllHitBoxCollider(true);

        if (right_hand_weapon != null)
        {
            right_hand_weapon.gameObject.SetActive(false);
        }
        if (left_hand_weapon != null)
        {
            left_hand_weapon.gameObject.SetActive(false);
        }

        if (phase3Initialized)
        {
            return;
        }
        phase3Initialized = true;
    }

    //为什么需要一个这样的函数呢，最主要原因是DATA系感知函数和Sensor系列感知函数都是靠一些层和标签来为AI模块提供判断依据，如果角色战败，他们还挂着原来的信息则会对仍战斗中的AI判断进行干扰
    public void deathInitialize()
    {
        gameObject.layer = this._TeamConfig.deadLayer;
        if (myWeaponList != null)
        {
            foreach (BO_Marker_Manager weapon in myWeaponList)
            {
                weapon.gameObject.layer = this._TeamConfig.deadLayer;
            }
        }
        this.BO_Health.changeLayerForAllSelfColliders(_TeamConfig.deadLayer);
    }

    public void addToDeActiveObjects(GameObject gameObject)
    {
        if (!deActiveObjects_List.Contains(gameObject))
        {
            deActiveObjects_List.Add(gameObject);
        }
    }

    public void deActiveObjects()
    {
        if (deActiveObjects_List.Count > 0)
        {
            foreach (GameObject o in deActiveObjects_List)
            {
                o.SetActive(false);
            }
        }
    }

    //我们希望datacenter是整个角色初始化的出发点，那么这个地方应该也可以做到根据情况决定一些组件加载还是不加载。
    //关于角色初始化，我们需要把所有进行初始化的内容都拿出来进行一个归类工作。其实当下我们的系统里最不稳的一个事情在于
    //很多靠字符串表达的地址。。这种东西我们在读取角色和读取脚本的地方已经修改了，但在读取动画文件和读取脚本的地方仍然存在。
    //这些地方有没有必要给想办法去掉呢。。
    private void Awake()
    {
        if (geometryCenter == null)
        {
            Debug.Log("没有在DATA中心里适配角色几何中心");
        }
    }

    //public void switchToSmoothPhysicMaterial()
    //{
    //    if (smooth)
    //        _collider.material = smooth;
    //}
    //public void switchToMocaPhysicMaterial()
    //{
    //    if (moca)
    //        _collider.material = moca;
    //}

    public virtual void AI_Update()
    {
    }
    public virtual void AI_LateUpdate()
    {
    }

    public void turnShield(bool isShieldActive)
    {
        //if (PhotonNetwork.offlineMode || !PhotonNetwork.connected) {
        //} else
        //    photonView.RPC("turnShieldRPC", PhotonTargets.All, isShieldActive);
        if (Shield != null)
            Shield.gameObject.SetActive(isShieldActive);
            //Shield.EnableShieldCollider();   
    }

    Vector3 temp;
    void Update ()
    {
        if (showMode)
        {
            this.grounded = true;
            if (animator)
            {
                animator.SetBool("Grounded", true);
                animator.SetFloat("groundedCount", 10);
            }
            return;
        }
        
        IfGrounded();
        if (animator)
        {
            animator.SetBool("Grounded", grounded);
            animator.SetFloat("airCount", airCount);
            animator.SetFloat("groundedCount", groundedCount);
        }

        groundedCount = (grounded) ? groundedCount += Time.deltaTime : 0f;
        airCount = (!grounded) ? airCount += Time.deltaTime : 0f;
        if (AIStateRunner != null)
            AI_Update();

        if (gameObject.transform.position.y <= floorY) 
        {
            temp = gameObject.transform.position;
            temp.y = floorY;
            if (ifVectorClean(temp))
                gameObject.transform.position = temp;
        }

        if (System.Math.Abs(gameObject.transform.rotation.x) > 0.05f || System.Math.Abs(gameObject.transform.rotation.z) > 0.05f)
        {
            Quaternion q = gameObject.transform.rotation;
            q.x = 0f;
            q.z = 0f;
            gameObject.transform.rotation = q;
        }
    }
    
    private bool showMode = true;
    public void setIfShowMode(bool showMOde)
    {
        this.showMode = showMOde;
        this.AIStateRunner.showMode = showMOde;
    }

    private float p1_to_me,p2_to_me;
    public int horizontalDistanceCompare(Vector3 p1, Vector3 p2)
    {
        p1.y = gameObject.transform.position.y;
        p1_to_me = (p1 - gameObject.transform.position).magnitude;

        p2.y = gameObject.transform.position.y;
        p2_to_me = (p2 - gameObject.transform.position).magnitude;

        if (p1_to_me > p2_to_me)
        {
            return 1;
        }
        if (p1_to_me < p2_to_me)
        {
            return -1;
        }
        return 0;
    }

    public GameObject[] MergerGameObjectArray(GameObject[] First, GameObject[] Second) //合并两个数组。不光是gameobjects，任何类型变量都可以通用。
    {
        GameObject[] result = new GameObject[First.Length + Second.Length];
        First.CopyTo(result, 0);
        Second.CopyTo(result, First.Length);
        return result;
    }

    RaycastHit hit;

    private float floorY = 0;

    public float getFloorY()
    {
        return floorY;
    }

    public void IfGrounded()
    {
        if (floorCheckers == null)
        {
            this.grounded = false;
            return;
        }
        foreach (Transform check in floorCheckers)
        {
            //if (floorGot)
            //{
                if (this.floorY > check.transform.position.y)//Mathf.Abs(check.transform.position.y - this.floorY) < grounded_judgement_RayUpDis &&
                {
                    if (Rigidbody != null)
                        Rigidbody.useGravity = false;
                    this.grounded = true;
                    return;
                }
            //}else{
            //    RaycastHit[] floorCheckersHits = Physics.RaycastAll(check.position, Vector3.up, Mathf.Infinity);
            //    foreach(RaycastHit _hit in floorCheckersHits)
            //    {
            //        if (_hit.collider.gameObject.layer == 19)
            //        {
            //            floorGot = true;
            //            BO_Health.makeHitBoxesDontCollideWithGround(_hit.collider);//我们姑且认为这一步时候所有的hitbox collider都已经加载好。因为这个加载工作是包括在第三级初始化里。
            //            this.floorY = _hit.collider.transform.position.y;
            //            this.grounded = true;
            //            return;
            //        }
            //    }
            //}
        }

        if (Rigidbody != null)
            Rigidbody.useGravity = true;
        this.grounded = false;
    }

    public bool IsGrounded()
    {
        return this.grounded;
    }

    public bool ifVectorClean(Vector3 rot)
    {
        if (rot == Vector3.zero)
            return false;

        if (float.IsNaN(rot.x) || float.IsNaN(rot.y) || float.IsNaN(rot.z))
        {
            return false;
        }
        if (float.IsInfinity(rot.x) || float.IsInfinity(rot.y) || float.IsInfinity(rot.z))
        {
            return false;
        }
        return true;
    }
}

//4.25日的重要修改

 //现在，所有角色在创建的时候要遵循这样重要的一点：作为角色在地面支撑用的collider，其下沿边必须低于gameobject。transform。position，
 //并且高于floorcheckers中的marker。在DATACENTER中仍然会在角色陷入地面的情况下强行将角色的坐标回拉到地面高度，
 //但这样之后，应该是在战斗的进行过程中，撑地collider在各种抖动后会再次起到支撑地面的作用，这时候其与地面的摩擦力就会起到真正的作用。
 //如此一来原先的武器攻击位置校准，角色互推，以及防御状态震动调位的功能都已经重新打开。
 //GM攻击状态下，原本有一个把collider给trigger化的机制，这个我们给删掉了。因为这不利于生动的让角色的冲击被阻挡（这个事情有些微妙）

 //另外还有了个比较大的点：我们尽量在创建角色时候让角色手脚的攻击球附近也有角色自身的collider，这是因为在一些类似电钻一样的武功发动时候能尽可能的给对方一些推动作用，
 //否则角色总是会和对方互相重合，非常违和。

 //另外，我们决定Shield全部都是非trigger，一来因为这个shield也应该起一个和角色的隔绝作用，尤其是GM攻击，
 //再一个，这个盾牌既然看的见，就应该和角色互推。而我们的互推机制靠的是onentertrigger，并没有写ontriggerenter。

 //GM攻击下触碰Shield仍然会停止。因为root animation实在是很难靠collider接触给拦下来。一不小心就会穿越盾牌，攻击球在不接触盾牌情况下打到敌人（再来个cleartarget，敌人就伤着了）
 //于是此企划保留。