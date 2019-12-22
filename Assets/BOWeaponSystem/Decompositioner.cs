using UnityEngine;
using System.Collections.Generic;
using HittingDetection;
using UnityEngine.Animations;

public class Decompositioner : MonoBehaviour {

    DecompositionerPool _DecompositionerPool;

    public BO_Marker_Manager _HitBox;
    public TrackControl TrackControl;
    
	public float DestructionDelay = 1.1f;//上面的值必须要大于下面的值
    public float stop_emission_delay = 0.9f;

    public List<MeshRenderer> to_be_faded_renderers;
    public AudioSource audioSource;
    PositionConstraint positionConstraint;
    ParticleSystem to_be_stop_emissions;
    float counter;
    int phase;
    
    public void SetPositionConstraint(PositionConstraint positionConstraint)
    {
        this.positionConstraint = positionConstraint;
    }
    
    public PositionConstraint GetPositionConstraint()
    {
        return this.positionConstraint;
    }

    void Awake()
    {
        to_be_stop_emissions = gameObject.GetComponent<ParticleSystem>();
    }

    public void SetPool(DecompositionerPool _DecompositionerPool)
    {
        this._DecompositionerPool = _DecompositionerPool;
    }

    public void Local_OnEnable()
    {
        if (to_be_stop_emissions != null)
            to_be_stop_emissions.Play(true);
        phase = 1;
        if (audioSource)
            audioSource.volume = AudioManager.effectsVolumn;
        counter = 0;
        SetMaterialsAlpha(1f);
    }

    void Update()
    {
        if (_HitBox != null)
        {
            switch (_HitBox._WeaponMode)
            {
                case WeaponMode.EnergyFromBodyWeapon:
                    if (_HitBox.weaponHP > 0 && _HitBox.CurrentHP <= 0)
                    {
                        _HitBox.DisableMarkers();
                        StopEmissions();
                    }
                    if (_HitBox.GetOwnerFightAttriCalReference() != null)
                    {
                        if (_HitBox.GetOwnerFightAttriCalReference().IFgettingDamage())
                        {
                            _HitBox.DisableMarkers();
                            StopEmissions();
                        }
                    }
                    break;
                case WeaponMode.FlyerWeapon:
                    if (_HitBox.weaponHP > 0 && _HitBox.CurrentHP <= 0)
                    {
                        _HitBox.DisableMarkers();
                        StopEmissions();
                    }
                    break;
            }
        }
    }

    void FixedUpdate()
    {
        if (DestructionDelay > 0)
        {
            counter += Time.fixedDeltaTime;
            MagicEffectLifeCircle();
        }
    }

    void MagicEffectLifeCircle()
    {
        switch (phase)
        {
            case 1:
                if (counter > stop_emission_delay)
                {
                    StopEmissions();
                    phase = 2;
                }
                break;
            case 2:
                if (DestructionDelay > stop_emission_delay)
                {
                    SetMaterialsAlpha((float)(DestructionDelay - counter) / (DestructionDelay - stop_emission_delay));
                }
                if (counter > DestructionDelay)
                {
                    phase = 0;
                    _DecompositionerPool.Return(this);
                }
                break;
        }
    }

    public void StopEmissions()
    {
        if (to_be_stop_emissions != null)
            to_be_stop_emissions.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
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
}
