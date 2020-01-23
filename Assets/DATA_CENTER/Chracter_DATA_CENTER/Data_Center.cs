using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Soul;

//Basically, Data_Center is a place where all parameter need applying to a character are initiazlized,
//Those paremeters are sent to all parts of character from here.
[RequireComponent(typeof(BehaviorRunner))]
[RequireComponent(typeof(Animation_Manger))]
[RequireComponent(typeof(Controller))]
[RequireComponent(typeof(FightAttriCalReference))]
[RequireComponent(typeof(Sensor))]
[RequireComponent(typeof(ShaderManager))]
[RequireComponent(typeof(BuffsRunner))]
[RequireComponent(typeof(BlendShapeProxy))]
public partial class Data_Center : MonoBehaviour
{
    public TeamConfig _TeamConfig = TeamConfig.defaultSet;

    public Zokusei Zokusei;
    public Transform geometryCenter;
    public Transform WholeT;
    public AudioSource _AudioSource;
    public Sensor Sensor;
    public Animation_Manger Animation_Manger;
    public SkillCancelFlag _SkillCancelFlag;
    public BO_Ani_E _BO_Ani_E;
    public FightAttriCalReference _FightAttriCalReference;
    public BO_Weapon_Animation_Events bO_Weapon_Animation_Events;
    public BasicPhysicSupport _BasicPhysicSupport;
    public BehaviorRunner _MyBehaviorRunner;
    public Controller controller;
    public BuffsRunner buffsRunner;
    public ResistanceManager _ResistanceManager;
    public ShaderManager _ShaderManager;
    public BlendShapeProxy blendShapeProxy;
    public Personality_events Personality_events;
    
    public Transform right_hand_t, left_hand_t, right_foot_t, left_foot_t,tail_t, head_t;
    public Transform left_arm_hitbox_t, right_arm_hitbox_t, left_leg_hitbox_t, right_leg_hitbox_t, spine_hitbox_t;

    protected bool phase1Initialized, phase2Initialized;
    
    [Header("传统防御盾。可能真的用不到了")]
    [Space(1)]
    public BO_Shield Shield;
    
    public ReactiveProperty<bool> IsDead { get; set; } = new ReactiveProperty<bool>(false);

    public bool IfPreparedForBattle()
    {
        return phase1Initialized && phase2Initialized;
    }

    void Awake()
    {
        if (geometryCenter == null)
            geometryCenter = gameObject.transform; 
    }

    public IEnumerator Step1Initialize(string type, string basicPackName,string personalMagicPath)
    {
        if (!phase1Initialized)
        {
            _BasicPhysicSupport.Rigidbody.useGravity = false;
            BodyElementTagAndLayerSet(TeamConfig.defaultSet);
            bO_Weapon_Animation_Events.hiddenMethods.AssignWeaponsFromDataCenter(_FightAttriCalReference,geometryCenter, right_hand_t, left_hand_t, right_foot_t, left_foot_t, head_t, tail_t);
            string personalEffectsPath;
            switch (Zokusei)
            {
                case Zokusei.darkMagic:
                    personalEffectsPath = "darkMagic";
                    break;
                case Zokusei.blueMagic:
                    personalEffectsPath = "blueMagic";
                    break;
                case Zokusei.greenMagic:
                    personalEffectsPath = "greenMagic";
                    break;
                case Zokusei.lightMagic:
                    personalEffectsPath = "lightMagic";
                    break;
                case Zokusei.redMagic:
                    personalEffectsPath = "redMagic";
                    break;
                default:
                    personalEffectsPath = "defaultmagic";
                    break;
            }
            EffectAndHurtObjectLoading.Instance.IniEffectsPool("short_effect", personalEffectsPath, 3);
            EffectAndHurtObjectLoading.Instance.IniEffectsPool("normal_effect", personalEffectsPath, 3);
            EffectAndHurtObjectLoading.Instance.IniEffectsPool("long_effect", personalEffectsPath, 3);
            switch (ResourceLoadingSetting.Instance.AnimationLoadingMode)
            {
                case ResourceLoadMode.CachAB:
                    yield return (Animation_Manger.PreloadBasicPersonalAnims(type,basicPackName));
                    break;
                case ResourceLoadMode.StreamingAssetAB:
                    yield return (Animation_Manger.preloadBasicPersonalAnimsStreamingAssetMode(type, basicPackName));
                    break;
                case ResourceLoadMode.Resource:
                    yield return (Animation_Manger.preloadBasicPersonalAnimsResourceMode(type, basicPackName));
                    break;
            }
            yield return _BO_Ani_E.BasicMagicAndEffectsPathDefine(this.Zokusei, personalMagicPath);
            //if (this.blendShapeProxy != null && this.blendShapeProxy.VRMBlendShapeProxy != null)
            //    this.blendShapeProxy.VRMBlendShapeProxy.AvaterRemerge(this.WholeT);
            //else
            //{
            //    this.blendShapeProxy = this.gameObject.GetComponent<BlendShapeProxy>();
            //    this.blendShapeProxy.VRMBlendShapeProxy.AvaterRemerge(this.WholeT);
            //}                
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

        Sensor.SetDectectLayer(_TeamConfig,this);
        bO_Weapon_Animation_Events.hiddenMethods.AssignTeamFlag(_TeamConfig);

        string effectPath;
        switch (Zokusei)
        {
            case Zokusei.darkMagic:
                effectPath = "darkMagic";
                break;
            case Zokusei.blueMagic:
                effectPath = "blueMagic";
                break;
            case Zokusei.greenMagic:
                effectPath = "greenMagic";
                break;
            case Zokusei.lightMagic:
                effectPath = "lightMagic";
                break;
            case Zokusei.redMagic:
                effectPath = "redMagic";
                break;
            default:
                effectPath = "defaultmagic";
                break;
        }

        EffectAndHurtObjectLoading.Instance.IniEffectsPool("Sparks", effectPath, 3);
        EffectAndHurtObjectLoading.Instance.IniEffectsPool("light_hit", effectPath, 3);
        EffectAndHurtObjectLoading.Instance.IniEffectsPool("light_hit", effectPath, 3);
        EffectAndHurtObjectLoading.Instance.IniEffectsPool("heavy_hit", effectPath, 3);
        EffectAndHurtObjectLoading.Instance.IniEffectsPool("super_hit", effectPath, 3);
        EffectAndHurtObjectLoading.Instance.IniEffectsPool("resistanceUp", effectPath, 3);
        EffectAndHurtObjectLoading.Instance.IniEffectsPool("on_enable_effect", effectPath, 3);
                   
        if (Shield != null)
        {
            Shield.gameObject.SetActive(false);
            if (_FightAttriCalReference != null)
            {
                Shield.IniShield(_TeamConfig, _FightAttriCalReference);
                Shield._ShieldBackSpot = this.geometryCenter;
            }
        }
    }
    
    public IEnumerator Step2Initialize(string type, NineAndTwo _NineAndTwo, Zokusei _zokusei, string personalMagic)
    {
        if (!phase2Initialized)
        {
            _MyBehaviorRunner.characterType = type;
            phase2Initialized = true;
        }
        
        _MyBehaviorRunner.FormFightingSetsByNineAndTwo(type, _NineAndTwo);
        _MyBehaviorRunner.INIStates(this);
        //这个环节之后我应该有一份列表来展示到底我一个角色一场战斗都能用上什么招
        // 上面这个环节结束后，有这样几个重要情况1. state_Transition_Dictionary的内容就正确了 2.AIStateRunner内的States_Dictionary实例内将有一份正确的skill类key的列表
        List<string> toLoadSkillAnimsNames = _MyBehaviorRunner.PassSkillTypeKeys();
        switch (ResourceLoadingSetting.Instance.AnimationLoadingMode)
        {
            case ResourceLoadMode.CachAB:
                yield return (Animation_Manger.PreloadPersonalAnims(ResourceLordSceneStarter.BundleURL + "/animClips",type, toLoadSkillAnimsNames, personalMagic, _zokusei));
            break;
            case ResourceLoadMode.Resource:
                yield return (Animation_Manger.preloadPersonalAnimsResourceMode(type, toLoadSkillAnimsNames, personalMagic, _zokusei));
            break;
            case ResourceLoadMode.StreamingAssetAB:
                yield return (Animation_Manger.preloadPersonalAnimsStreamingAssetMode(type, toLoadSkillAnimsNames, personalMagic, _zokusei));
            break;
        }
    }

    public void Step3Initialize(TeamConfig _TeamConfig)//战斗必备
    {
        BodyElementTagAndLayerSet(_TeamConfig);//这一步和下面的changeLayerForAllSelfColliders为什么分开？没什么为什么。就是给写开了。
        _FightAttriCalReference.FindAllSelfCollidersAndIgnoreCollision();//上面那个防御盾设置保证了这一步也能把防御盾碰撞体处理。
        _FightAttriCalReference.ChangeLayerForAllSelfColliders(_TeamConfig.mylayer);
        _FightAttriCalReference.EnableAllHitBoxCollider(true);
    }

    //为什么需要一个这样的函数呢，最主要原因是DATA系感知函数和Sensor系列感知函数都是靠一些层和标签来为AI模块提供判断依据，如果角色战败，他们还挂着原来的信息则会对仍战斗中的AI判断进行干扰
    public void DeathInitialize()
    {
        gameObject.layer = _TeamConfig.deadLayer;
        _FightAttriCalReference.ChangeLayerForAllSelfColliders(_TeamConfig.deadLayer);
    }

    //我们希望datacenter是整个角色初始化的出发点，那么这个地方应该也可以做到根据情况决定一些组件加载还是不加载。
    //关于角色初始化，我们需要把所有进行初始化的内容都拿出来进行一个归类工作。其实当下我们的系统里最不稳的一个事情在于
    //很多靠字符串表达的地址。。这种东西我们在读取角色和读取脚本的地方已经修改了，但在读取动画文件和读取脚本的地方仍然存在。
    //这些地方有没有必要给想办法去掉呢。。
    public void TurnShield(bool isShieldActive)
    {
        //if (PhotonNetwork.offlineMode || !PhotonNetwork.connected) {
        //} else
        //    photonView.RPC("turnShieldRPC", PhotonTargets.All, isShieldActive);
        if (Shield != null)
            Shield.gameObject.SetActive(isShieldActive);
            //Shield.EnableShieldCollider();   
    }

    void FixedUpdate()
    {
        if (_MyBehaviorRunner.IfRunning())
        {
            Sensor.SensorFixedUpdate();
            buffsRunner.BuffsRunnerFixedUpdate();
            _FightAttriCalReference.HealthBodyFixedUpdate();
            _SkillCancelFlag.hiddenMethods.SkillCancelFlagFixedUpdate();
        }
    }
   
    public void CleanClear()
    {
        _BasicPhysicSupport.hiddenMethods.Grounded = true;
        Sensor.Stop();
        bO_Weapon_Animation_Events.ClearMarkerManagers();
        buffsRunner.EndAllCoroutines();
        _BasicPhysicSupport.hiddenMethods.ClearHitCountForAttackStepping();
        _BasicPhysicSupport.hiddenMethods.ResetAnimator();
    }

    float p1_to_me, p2_to_me;
    public int HorizontalDistanceCompare(Vector3 p1, Vector3 p2)
    {
        p1.y = gameObject.transform.position.y;
        p1_to_me = (p1 - gameObject.transform.position).magnitude;

        p2.y = gameObject.transform.position.y;
        p2_to_me = (p2 - gameObject.transform.position).magnitude;

        return p1_to_me > p2_to_me ? 1 : p1_to_me < p2_to_me ? -1 : 0;
    }

    public GameObject[] MergerGameObjectArray(GameObject[] First, GameObject[] Second) //合并两个数组。不光是gameobjects，任何类型变量都可以通用。
    {
        GameObject[] result = new GameObject[First.Length + Second.Length];
        First.CopyTo(result, 0);
        Second.CopyTo(result, First.Length);
        return result;
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