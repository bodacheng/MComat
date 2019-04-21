using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EZObjectPools;

public class BO_DestroyAfterSeconds : MonoBehaviour {

	public float DestructionDelay = 7;
    public float stop_emission_delay = 2;

    public List<ParticleSystem> to_be_stop_emissions;
    public List<MeshRenderer> to_be_faded_renderers;

    private float counter = 0;
    private float A;
    private Coroutine process;

    void OnEnable()
    {
        A = 1;
        SetMaterialsAlpha(A);
        //开启协程方法
        process = StartCoroutine(DelayDisable(stop_emission_delay,DestructionDelay));
    }

    void OnDisable()
    {
        StopCoroutine(process);
    }

    public void stopEmissions()
    {
        if (to_be_stop_emissions.Count == 0)
            return;
        foreach (ParticleSystem p in to_be_stop_emissions)
        {
            p.Stop();
        }
    }

    public void SetMaterialsAlpha(float a)
    {
        if (to_be_faded_renderers.Count > 0)
        {
            foreach (MeshRenderer meshrender in to_be_faded_renderers)
            {
                for (int i = 0; i < meshrender.materials.Length; i++)
                {
                    if (meshrender.materials[0] != null)
                        meshrender.materials[0].SetColor("_TintColor", new Color(
                            meshrender.materials[0].GetColor("_TintColor").r,
                            meshrender.materials[0].GetColor("_TintColor").g, 
                            meshrender.materials[0].GetColor("_TintColor").b, a));
                }
            }
        }
    }

    IEnumerator DelayDisable(float _stop_emission_delay,float _DestructionDelay)
    {
        for (counter = stop_emission_delay; counter > 0; counter -= Time.deltaTime)
        {
            A = counter / stop_emission_delay;
            SetMaterialsAlpha(A);
            yield return null;
        }

        //yield return new WaitForSeconds(_stop_emission_delay);
        stopEmissions();
        if (_DestructionDelay - _stop_emission_delay > 0)
            yield return new WaitForSeconds(_DestructionDelay - _stop_emission_delay);
        else
            yield return new WaitForSeconds(0f);

        //调用单例中向对象池里面存对象的方法
        gameObject.SetActive(false);
        yield break;
    }
}
