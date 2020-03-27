using UnityEngine;
using System.Collections.Generic;
using HittingDetection;
using UnityEngine.Animations;

public enum HitBoxLifeEnding
{
    untouched = 1,
    touched = 2,
    successed = 3
}

public class Decompositioner : MonoBehaviour {
    
    public BO_Marker_Manager _HitBox;
    public TrackControl TrackControl;
    
	public float DestructionDelay = 1.1f;//上面的值必须要大于下面的值
    public float stop_emission_delay = 0.9f;
    public bool energyDissolveSlowly;
    
    public List<MeshRenderer> to_be_faded_renderers;
    public AudioSource audioSource;

    [Tooltip("附属物体。这个只能自己把握。")]
    public string[] Attachments;
    public string _HitTriggerEvent;

    #region realtime
    DecompositionerPool _DecompositionerPool;
    PositionConstraint positionConstraint;
    BO_Ani_E BO_Ani_E;
    ParticleSystem to_be_stop_emissions;
    int phase;
    float Counter;
    #endregion

    void Awake()
    {
        to_be_stop_emissions = gameObject.GetComponent<ParticleSystem>();
    }

    public void SetPool(DecompositionerPool _DecompositionerPool)
    {
        this._DecompositionerPool = _DecompositionerPool;
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
        if (to_be_stop_emissions != null)
        {
            to_be_stop_emissions.Play(true);
        }
        if (audioSource)
        {
            audioSource.volume = AudioManager.effectsVolumn;
        }
        SetMaterialsAlpha(1f);
        if (_HitBox != null)
        {
            _HitBox.CurrentHP = _HitBox.weaponHP;
        }
        phase = 1;
        if (DestructionDelay >= 0)
        {
            Counter = 0;
        }
    }

    void Update()
    {
        HitBoxesProcesser.AddToDecompositionerProcesserList(this);
    }

    void EnergyRessolve()
    {
        StopEmissions(true);
        _DecompositionerPool.Return(this);
    }

    public void CloseMarkers()
    {
        if (_HitBox != null)
        {
            _HitBox.Local_OnDisable();// 与Local_OnDisable()内的Local_OnDisable看起来重复，意思就是说
            _HitBox.SetOwnerFightAttriCalReference(null);
        }
    }
       
    public void Step1()
    {
        if (phase == 1 && _HitBox != null)
        {
            _HitBox.LocalUpdate();
        }
    }
    public void Step2()
    {
        if (phase == 1 && _HitBox != null)
        {
            _HitBox.LocalLateUpdate();
        }
    }
    
    public void Life()
    {
        if (phase == 1 && _HitBox != null)
        {
            switch (_HitBox._WeaponMode)
            {
                case WeaponMode.EnergyFromBodyWeapon:
                    if (_HitBox.weaponHP > 0 && _HitBox.CurrentHP <= 0)
                    {
                        CloseMarkers();
                        if (energyDissolveSlowly)
                        {
                            StopEmissions(false);
                            Invoke("EnergyRessolve", 0.7f);
                        }
                        else
                        {
                            EnergyRessolve();
                        }
                        Counter = stop_emission_delay;
                        phase = 2;
                        HitBoxFadedEvent();
                    }
                    if (_HitBox.GetOwnerFightAttriCalReference() != null)
                    {
                        if (_HitBox.GetOwnerFightAttriCalReference().IFgettingDamage())
                        {
                            CloseMarkers();
                            if (energyDissolveSlowly)
                            {
                                StopEmissions(false);
                                Invoke("EnergyRessolve", 0.7f);
                            }
                            else
                            {
                                EnergyRessolve();
                            }
                            Counter = stop_emission_delay;
                            phase = 2;
                        }
                    }
                    break;
                case WeaponMode.FlyerWeapon:
                    if (_HitBox.weaponHP > 0 && _HitBox.CurrentHP <= 0)
                    {
                        CloseMarkers();
                        if (energyDissolveSlowly)
                        {
                            StopEmissions(false);
                            Invoke("EnergyRessolve", 0.7f);
                        }
                        else
                        {
                            EnergyRessolve();
                        }
                        Counter = stop_emission_delay;
                        phase = 2;
                        HitBoxFadedEvent();
                    }
                    break;
            }
        }
        
        if (DestructionDelay > 0 && stop_emission_delay > 0)//如果能量自身有寿命
        {
            switch (phase)
            {
                case 1:
                    if (Counter > stop_emission_delay)
                    {
                        CloseMarkers();
                        StopEmissions(false);
                        phase = 2;
                    }
                break;
                case 2:
                    if (DestructionDelay > stop_emission_delay)
                    {
                        SetMaterialsAlpha((DestructionDelay - Counter) / (DestructionDelay - stop_emission_delay));
                    }
                    if (Counter > DestructionDelay)
                    {
                        phase = 0;
                        EnergyRessolve();
                    }
                break;
            }
        }
        if (gameObject.activeSelf)
        {
            Counter += Time.deltaTime;
        }
    }

    public void StopEmissions(bool clearParticles)
    {
        if (to_be_stop_emissions != null && to_be_stop_emissions.isPlaying)
        {
            if (clearParticles)
                to_be_stop_emissions.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            else
                to_be_stop_emissions.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }
    }

    public void SetMaterialsAlpha(float a)
    {
        if (to_be_faded_renderers.Count == 0)
            return;

        for (int i = 0; i < to_be_faded_renderers.Count; i++)
        {
            if (to_be_faded_renderers[i] != null)
            {
                for (int y = 0; y < to_be_faded_renderers[i].materials.Length; y++)
                {
                    if (to_be_faded_renderers[i].materials[y] != null)
                        to_be_faded_renderers[i].materials[y].SetColor("_TintColor", new Color(to_be_faded_renderers[i].materials[y] .GetColor("_TintColor").r,to_be_faded_renderers[i].materials[y] .GetColor("_TintColor").g, to_be_faded_renderers[i].materials[y] .GetColor("_TintColor").b, a));
                }
            }
        }
    }
    
    void HitBoxFadedEvent()//这个就只能在这自定义了
    {
        switch (_HitTriggerEvent)
        {
            case "expolosion":
                BO_Ani_E.hiddenMethods.BlastAttack_core(transform.position,transform.rotation,null,2, _HitBox.GeneratedByStateKey);
                break;
            case "explosionlighteningball_big":
                BO_Ani_E.hiddenMethods.MagicForward_core("explosionlighteningball_big", 
                    transform.position,transform.rotation,0,_HitBox.GeneratedByStateKey);
                break;
        }
    }
    
    public void SpecialTriggerEvent(string defined_event_code, HitBoxSubEventManger hitBoxSubEventManger)//这个就只能在这自定义了
    {
        switch (defined_event_code)
        {
            case "expolosion":
                BO_Ani_E.hiddenMethods.BlastAttack_core(transform.position,transform.rotation,null,2,_HitBox.GeneratedByStateKey);
                break;
            case "bulletForward":
                BO_Ani_E.hiddenMethods.Bullet_shoot_from_Core(hitBoxSubEventManger.transform.position,hitBoxSubEventManger.transform.rotation, 1,10,_HitBox.GeneratedByStateKey);
                break;
            case "groundroundblast":
                BO_Ani_E.hiddenMethods.MagicForward_core("groundroundblast", 
                hitBoxSubEventManger.transform.position,hitBoxSubEventManger.transform.rotation,0,_HitBox.GeneratedByStateKey);
                break;
            case "explosionlighteningball_big":
                BO_Ani_E.hiddenMethods.MagicForward_core("explosionlighteningball_big",
                hitBoxSubEventManger.transform.position,hitBoxSubEventManger.transform.rotation,0,_HitBox.GeneratedByStateKey);
                break;
        }
    }
}
