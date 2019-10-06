using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using HittingDetection;

public class Decompositioner : MonoBehaviour {

    DecompositionerPool _DecompositionerPool;
        
    public BO_Marker_Manager _HitBox;
    
	public float DestructionDelay = 1.1f;//上面的值必须要大于下面的值
    public float stop_emission_delay = 0.9f;

    public List<ParticleSystem> to_be_stop_emissions;
    public List<MeshRenderer> to_be_faded_renderers;

    public AudioSource audioSource;
    private float counter = 0;
    private int phase = 0;

    public void setPool(DecompositionerPool _DecompositionerPool)
    {
        this._DecompositionerPool = _DecompositionerPool;
    }

    void OnEnable()
    {
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
                case WeaponMode.NormalWeapon:
                    break;
                case WeaponMode.EnergyFromBodyWeapon:
                    if (_HitBox.weaponHP > 0 && _HitBox.weaponHPCounter <= 0)
                    {
                        _HitBox.DisableMarkers();
                        disable(0.1f);
                    }
                    if (_HitBox.getWeaponOwnerHealth() != null)
                    {
                        if (_HitBox.getWeaponOwnerHealth().IFgettingDamage())
                        {
                            _HitBox.DisableMarkers();
                            disable(0.1f);
                        }
                    }
                    break;
                case WeaponMode.FlyerWeapon:
                    if (_HitBox.weaponHP > 0 && _HitBox.weaponHPCounter <= 0)
                    {
                        _HitBox.DisableMarkers();
                        disable(0.1f);
                    }
                    break;
            }
        }
    }

    void FixedUpdate()
    {
        counter += Time.fixedDeltaTime;
        magicEffectLifeCircle();
    }
    
    private void magicEffectLifeCircle()
    {
        switch (phase)
        {
            case 1:
                if (counter > stop_emission_delay)
                {
                    stopEmissions();
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
    
    public void stopEmissions()
    {
        if (to_be_stop_emissions.Count == 0)
            return;
        for (int i = 0; i < to_be_stop_emissions.Count; i++)
        {
            to_be_stop_emissions[i].Stop(true, ParticleSystemStopBehavior.StopEmitting);
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

    public void disable(float time)
    {
        if (this.gameObject.activeSelf)
            StartCoroutine(this.disableAfterTime(time));
    }
    
    // 这个函数一般情况下是纯粹关乎表现问题，所以我们认为游戏暂停的话这个不会造成太大影响。。但确实SetActive活动可能影响对象池
    private IEnumerator disableAfterTime(float time)
    {
        stopEmissions();
        yield return new WaitForSeconds(time);
        if (this._DecompositionerPool != null)
            this._DecompositionerPool.Return(this);
    }
}
