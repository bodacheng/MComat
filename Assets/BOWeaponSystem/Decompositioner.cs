using UnityEngine;
using System.Collections.Generic;
using HittingDetection;
using UnityEngine.Animations;

public partial class Decompositioner : MonoBehaviour {

    public HitBoxManager _HitBox;
    public TrackControl TrackControl;
    
    public float DestructionDelay = 1.1f;//上面的值必须要大于下面的值
    public float stop_emission_delay = 0.9f;
    
    public List<MeshRenderer> to_be_faded_renderers;
    public AudioSource audioSource;
    
    [Tooltip("附属物体。这个只能自己把握。")]
    public string[] Attachments;

    #region realtime
    DecompositionerPool pool;
    PositionConstraint positionConstraint;
    BO_Ani_E BO_Ani_E;
    ParticleSystem to_be_stop_emissions;
    public float Counter;
    public int Phase { get; set; }
    public bool IsWeapon { get; set; }
    public bool hasParticle { get; set; }
    #endregion

    void Awake()
    {
        to_be_stop_emissions = gameObject.GetComponent<ParticleSystem>();
        hasParticle = to_be_stop_emissions != null;
    }

    public void SetPool(DecompositionerPool pool)
    {
        this.pool = pool;
    }

    public void SetPositionConstraint(PositionConstraint positionConstraint)
    {
        this.positionConstraint = positionConstraint;
    }
    
    public PositionConstraint GetPositionConstraint()
    {
        return positionConstraint;
    }
    
    public void SetBOAniE(BO_Ani_E _Ani_E)
    {
        BO_Ani_E = _Ani_E;
    }
    
    public BO_Ani_E GetBOAniE()
    {
        return BO_Ani_E;
    }
    
    // Local_OnEnable和Local_OnDisable，最大的一个区别是，
    // 前者没有打开marker的处理，marker的开启由各个与攻击相关的模块自行处理，因为在那之前涉及一些不太统一的参数设置
    // 而Local_OnDisable进行了关闭marker的处理（目前好像就干了这一件事）
    public void Local_OnEnable()
    {
        Phase = 1;
        if (DestructionDelay >= 0)
        {
            Counter = 0;
        }
        if (IsWeapon)
            _HitBox.CurrentHP = _HitBox.weaponHP;
        if (hasParticle)
            to_be_stop_emissions.Play(true);
        if (audioSource)
        {
            audioSource.volume = AppSetting.value.EffectsVolumn;
        }
        SetMaterialsAlpha(1f);
    }
    
    void Update()
    {
        HitBoxesProcesser.AddToDecompositionerProcesserList(this);
    }

    public void EnergyRessolve()
    {
        StopEmissions(true);
        if (pool == null)
        {
            Debug.Log("严重逻辑问题"+ gameObject.name);
            Destroy(gameObject);
            return;
        }
        pool.Return(this);
    }
    
    public void CloseMarkers()
    {
        if (IsWeapon)
        {
            _HitBox.Local_OnDisable();// 与Local_OnDisable()内的Local_OnDisable看起来重复，意思就是说
            _HitBox.SetOwnerFACR(null);
        }
    }
    
    public void Step1()
    {
        if (Phase == 1)
        {
            if (IsWeapon)
                _HitBox.LocalUpdate();
        }
    }
    public void Step2()
    {
        if (Phase == 1)
        {
            if (IsWeapon)
                _HitBox.LocalLateUpdate();
        }
    }

    public void StopEmissions(bool clearParticles)
    {
        if (hasParticle)
        {
            if (to_be_stop_emissions.isPlaying)
            {
                if (clearParticles)
                    to_be_stop_emissions.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                else
                    to_be_stop_emissions.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            }
        }
    }

    public void SetMaterialsAlpha(float a)
    {
        if (to_be_faded_renderers.Count == 0)
            return;
        
        for (int i = 0; i < to_be_faded_renderers.Count; i++)
        {
            for (int y = 0; y < to_be_faded_renderers[i].materials.Length; y++)
            {
                to_be_faded_renderers[i].materials[y].SetColor("_TintColor", new Color(to_be_faded_renderers[i].materials[y] .GetColor("_TintColor").r,to_be_faded_renderers[i].materials[y] .GetColor("_TintColor").g, to_be_faded_renderers[i].materials[y] .GetColor("_TintColor").b, a));
            }
        }
    }
        
    public void SpecialTriggerEvent(string defined_event_code, HitBoxSubEventManger hitBoxSubEventManger)//这个就只能在这自定义了
    {
        switch (defined_event_code)
        {
            case "expolosion":
                BO_Ani_E.hiddenMethods.BlastAttack_core(transform.position, transform.rotation, null, 2, _HitBox.GeneratedByStateKey);
                break;
            case "boltpForward":
                BO_Ani_E.hiddenMethods.MagicForward_core("boltp", hitBoxSubEventManger.transform.position, hitBoxSubEventManger.transform.rotation, 3, _HitBox.GeneratedByStateKey);
                break;
            case "c_l_bullet":
                BO_Ani_E.hiddenMethods.MagicForward_core("c_l_bullet", hitBoxSubEventManger.transform.position, hitBoxSubEventManger.transform.rotation, 3, _HitBox.GeneratedByStateKey);
                break;
            case "c_r_bullet":
                Debug.Log(BO_Ani_E.hiddenMethods + ":"+ _HitBox);
                BO_Ani_E.hiddenMethods.MagicForward_core("c_r_bullet", hitBoxSubEventManger.transform.position, hitBoxSubEventManger.transform.rotation, 3, _HitBox.GeneratedByStateKey);
                break;
            case "bulletForward":
                BO_Ani_E.hiddenMethods.Bullet_shoot_from_Core(hitBoxSubEventManger.transform.position,hitBoxSubEventManger.transform.rotation, 1, 10, _HitBox.GeneratedByStateKey);
                break;
            case "bulletForward3":
                BO_Ani_E.hiddenMethods.Bullet_shoot_from_Core(hitBoxSubEventManger.transform.position,hitBoxSubEventManger.transform.rotation, 3, 10, _HitBox.GeneratedByStateKey);
                break;
            case "groundroundblast":
                BO_Ani_E.hiddenMethods.MagicForward_core("groundroundblast", hitBoxSubEventManger.transform.position,hitBoxSubEventManger.transform.rotation,0,_HitBox.GeneratedByStateKey);
                break;
            case "explosionlighteningball_big":
                BO_Ani_E.hiddenMethods.MagicForward_core("explosionlighteningball_big", hitBoxSubEventManger.transform.position,hitBoxSubEventManger.transform.rotation, 2, _HitBox.GeneratedByStateKey);
                Phase = -1;
                break;
            case "s_pillarblast":
                BO_Ani_E.hiddenMethods.MagicForward_core("s_pillarblast", hitBoxSubEventManger.transform.position, hitBoxSubEventManger.transform.rotation, 0, _HitBox.GeneratedByStateKey);
                Phase = -1;
                break;
            case "lightningspray":
                BO_Ani_E.hiddenMethods.MagicForward_core("lightningspray", hitBoxSubEventManger.transform.position, hitBoxSubEventManger.transform.rotation, 0, _HitBox.GeneratedByStateKey);
                Phase = -1;
                break;
        }
    }
}
