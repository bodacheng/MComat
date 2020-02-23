using UnityEngine;
using System.Collections.Generic;
// 18 年年初
//该类不进行网络值同步，但个别值向其他组件看齐
//武器是用来伤害敌人的，而在我们的计划是把自身受伤害这个判断放在自身这一边的客户端上决定。

namespace HittingDetection
{   
    public partial class BO_Marker_Manager : MonoBehaviour
    {
        bool Enabled;

        [Tooltip("Should the Markers be active upon the Start of this weapon?")]
        [SerializeField]
        float ActivateAfterTime;

        [Tooltip("特定针对")]
        public SpecificTarget SpecificTarget = SpecificTarget.both;
        [Tooltip("damageTypeOfTheWeapon")]
        public DamageType damage_type = DamageType.light_damage;
        [Tooltip("damageTypeOfTheWeapon")]
        public WeaponMode _WeaponMode;
        [Tooltip("击中时候靠受力调整敌人位置")]
        public WeaponPosAdjustMode _WeaponPosAdjustMode = WeaponPosAdjustMode.pushToMidForward;
        [Tooltip("特殊施予")]
        public SpecialApply _specialApply = SpecialApply.none;

        [Tooltip("weaponHP, when below 0, is not an energy")]
        public int weaponHP = -1;
        
        [Tooltip("如果是特效类攻击，是否为贴地魔法")]
        public bool onGroundMagic;//这个是和其他模块联动的。确实不得不放这儿。
        [Tooltip("特效是否有粘身视效")]
        public bool effectSpreadOnBody;
        [Tooltip("魔法特效路径(blueMagic redMagic)")]
        public Zokusei zokusei;
        [Tooltip("启动特效")]
        public string muzzle;
        [Tooltip("特殊爆炸特效")]
        [SerializeField]
        string ExplosionEffect = "energy_resolve";

        [Tooltip("A weapon can work in two ways: Manual and Continuous. Manual bases on an idea that each attack (like a sword swing) can hit each target only once - it then needs to be manualy reloaded through a function call or an Animation Event (for example by ClearTargets() - explained in the Info Bool, at the bottom of this Inspector window). This method is very precise, as you can be certain that each Target will get damage and  Hurt animation (on BS_Main_Health script) triggered only once with each swing. Manual should be used in most situations, mostly for animation-driven combat (like Dark Souls or God of War). The Continuous mode deals damage constantly, in a given time interval, giving you a constant damage-dealing weapon. It's better for VR and games with free-moving weapons (like when the Player can swing his sword by moving his mouse in any desired direction) - the downside of Continuous damage is that it takes away the precise control of the damage dealt (as each Target may be hit more than once depending on how fast the weapon is being driven around). Continuous should be used with blades which can move freely, independent from animations. [INFO:] Continuous damage is not fit for working with shields (BS_Shield script objects).")]
        [SerializeField]
        bool ContinuousDamage;
        [Tooltip("If you choose the Continuous damage, what should the interval of damage dealing be? (In seconds)")]
        [SerializeField]
        float ContinuousDamageInterval = 0.2f;

        public int CurrentHP { get; set; }

        FightAttriCalReference _MyOwnerCalReference;
        TeamConfig teamConfig = TeamConfig.defaultSet;
        Transform _WeaponHolderCenter;//角色几何中心，如果是能量道具则为能量道具的几何中心，用于防御判断。
        bool HitFlesh;
        bool HitShield;
        Marker[] _markers;
        List<Transform> _Targets_Raw_Hit = new List<Transform>(); //Targets initialy hit by the blade (pre-check 这个是以一帧为单位处理，为了避免多个marker重复处理击中的bodyhealth。
        List<Transform> _Used_Targets = new List<Transform>(); // 就是每一帧所碰撞到的所有collider的母体。所有collider。不论是否包含mainhealth什么的。是以武器启动周期为处理单位。处理过的单位才会加入至其中
        List<Transform> _Shields_Hit = new List<Transform>();
        List<Vector3> _ShiledHitPositions = new List<Vector3>();
        List<V_Damage> hitsOnHealthBody = new List<V_Damage>();
        bool TraditionalDefendMode;
        
        void Awake()//按理说所有现在Awake里的东西都应该能静态化。。或者最起码的。。。可以换个更好的时机
        {
            Transform _MarkersParent = transform;
            Transform[] children = new Transform[_MarkersParent.childCount];
            List<BO_Marker> bms = new List<BO_Marker>();
            for (int i = 0; i < children.Length; i++)
            {
                if (_MarkersParent.GetChild(i).gameObject.GetComponent<BO_Marker>() != null)
                {
                    _MarkersParent.GetChild(i).gameObject.GetComponent<BO_Marker>().LocalAwake();
                    bms.Add(_MarkersParent.GetChild(i).gameObject.GetComponent<BO_Marker>());
                    Collider colliderofmarker = _MarkersParent.GetChild(i).gameObject.GetComponent<Collider>();
                    HitBoxesProcesser.AddToColliderHitBoxDic(colliderofmarker,this);
                }
            }
            _markers = bms.ToArray();
        }
                
        public void MarkersEnablingStarts()
        {
            if (ActivateAfterTime == 0)
                EnableMarkers();
            else
                Invoke("EnableMarkers", ActivateAfterTime);
        }

        public void Local_OnDisable()
        {
            DisableMarkers();
            SetTeamConfig(TeamConfig.defaultSet);
        }

        public void SetReferenceTransformInfo(Transform centerT)
        {
            _WeaponHolderCenter = centerT;
        }

        public void SetOwnerFightAttriCalReference(FightAttriCalReference myOwnerCalReference)
        {
            _MyOwnerCalReference = myOwnerCalReference;
        }
        public FightAttriCalReference GetOwnerFightAttriCalReference()
        {
            return _MyOwnerCalReference;
        }
        public void SetDectionTargetsUnion(List<Transform> Used_Targets)
        {
            _Used_Targets = Used_Targets;
        }

        public void SetTeamConfig(TeamConfig teamConfig)
        {
            this.teamConfig = teamConfig;
            for (int i = 0; i < _markers.Length; i++)
            {
                Marker _Marker = _markers[i];
                _Marker._layers = teamConfig.mySensorAndWeaponTargetLayerMask;
                _Marker.enemyShieldLayer = teamConfig.enemyShieldLayerMask;
                _Marker.gameObject.layer = teamConfig.myWeaponLayer;
            }
        }

        // EnableMarkers的运行归根结底也都是建立在动画事件上，而动画片段的播放在双方客户端上是平等的，所以理论上说也没有必须针对主客客户端区别执行的操作
        public void EnableMarkers()
        {
            if (_Used_Targets != null)// 20200119 :我们认为这个null判断理论上不需要，但的确这里不加的话会有bug。以后的测试可以考虑把它去掉试试
                _Used_Targets.Clear();
            _Shields_Hit.Clear();
            for (int i = 0; i < _markers.Length; i++)
            {
                _markers[i].EnableMarkerProcess(teamConfig.myWeaponLayer);
            }
            Enabled = true;
        }

        public void DisableMarkers()
        {
            Enabled = false;
            if (_Used_Targets != null)
                _Used_Targets.Clear();
            _Shields_Hit.Clear();
            if (_markers != null)
            {
                for (int i = 0; i < _markers.Length; i++)
                {
                    _markers[i].DisableMarkerProcess();
                }
            }
        }

        public void ClearMarkersDectections()
        {
            for (int i = 0; i < _markers.Length; i++)
            {
                _markers[i].ClearMarkerProcess();
            }
        }

        public void ClearTargets()
        {
            if (_Used_Targets != null)
                _Used_Targets.Clear();
            _Shields_Hit.Clear();
            for (int i = 0; i < _markers.Length; i++)
            {
                _markers[i].ClearMarkerProcess();
            }
        }

        public void SetDamageType(DamageType damageType)
        {
            damage_type = damageType;
        }

        public void LocalUpdate()
        {
            if (Enabled)
            {
                HitFlesh = false;
                HitShield = false;
                DetectProcess();
            }
        }

        public void LocalLateUpdate()
        {
            if (Enabled)
            {
                TreatProcess();
                ClearMarkersDectections();
            }
        }

        // 3月24日新企划，围绕ContinuousDamage以及能量类攻击的新改动
        // 我们把原先的isHitDestroyEnergy参数给去掉，取而代之换成一个weaponhealth的int参数，如果这个参数大于0，系统会考虑其撞击毁灭的问题。
        // 而这个参数将和ContinuousDamage形成一个相互权衡的关系。如果武器不是ContinuousDamage，则一个能量系武器在打击到对象后应该立刻hp-1，并且直接cleartargets。
        // 直到hp为0时自身消灭。这样比如一个hp为2的波动技能就形成了一个类似kof99中boss那样的2连击飞行道具，这个道具打到人身上基本是形成一个很快的2连击。
        // 而如果这个武器是ContinuousDamage，事情将另当别论。ContinuousDamage类武器的cleartargets周期应该符合ContinuousDamageInterval。
        // 它在攻击到一个对象后不会立刻随着自身hp的减少而cleartargets，但如果它有着大于0的hp，它依然会随着打击到对象而掉血，并随着寿命结束而消失
        // 设想有一个地上火焰技能是ContinuousDamage，它可能有两种消失方式，一种是打击了不少对象hp为0了，一种是随着自身BO_destroyer的设置而时间已经尽。        
        // WeaponEnergyExaust 这个函数在“与敌人武器发生接触”和“与敌人肉体产生接触”的时候是不同的处理逻辑，这个可以更细致分析来探讨有没有其他方式处理
        void WeaponEnergyExaust(Vector3 Pos, Quaternion Qua)
        {
            if (weaponHP > 0)
            {
                CurrentHP -= 1;
                EffectAndHurtObjectLoading.Instance.GenerateEffect(ExplosionEffect, FightGlobalSetting.EffectPathDefine(zokusei), Pos, Qua, null);
            }
        }

        public bool IfVectorClean(Vector3 rot)
        {
            return rot == Vector3.zero
            || float.IsNaN(rot.x) || float.IsNaN(rot.y) || float.IsNaN(rot.z)
            || !float.IsInfinity(rot.x) && !float.IsInfinity(rot.y) && !float.IsInfinity(rot.z);
        }
    }
}
//AudioClip processingClip;
//void PlayTargetHitSound(string soundName)
//{
//    if (SoundSource == null)
//    {
//        Debug.Log("武器音效无法找到音源");
//        return;
//    }
//    soundEffectDic.TryGetValue(soundName.GetHashCode(),out processingClip);
//    if (processingClip != null)
//    {
//        SoundSource.PlayOneShot(processingClip);
//    }else{
//        Debug.Log("找不到武器音效");
//    }
//}

//void iniSoundDic(string resource_name)
//{
//    if (personalSoundResourcePath != null)
//    {
//        if ((Resources.Load(defaultSoundResourcePath + "/" + personalSoundResourcePath + "/" + resource_name, typeof(AudioClip)) as AudioClip) != null)
//        {
//            soundEffectDic.Add(new KeyValuePair<int, AudioClip>(resource_name.GetHashCode(),Resources.Load(defaultSoundResourcePath + "/" + personalSoundResourcePath + "/" + resource_name, typeof(AudioClip)) as AudioClip));
//            return;
//        }
//    }
//    if (Resources.Load(defaultSoundResourcePath + "/" + resource_name, typeof(AudioClip)) as AudioClip != null)
//    {
//        soundEffectDic.Add(new KeyValuePair<int,AudioClip>(resource_name.GetHashCode(),Resources.Load(defaultSoundResourcePath + "/" + resource_name, typeof(AudioClip)) as AudioClip));
//        return;
//    }
//    Debug.Log("无法找到音源：" + resource_name);
//}

