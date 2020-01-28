using System.Collections.Generic;
using UnityEngine;
using Kalagaan.POFX;
using DG.Tweening;

public class ShaderManager : MonoBehaviour
{
    public List<POFX> pOFXes;

    void Awake()
    {
        for (int i = 0; i < pOFXes.Count; i++)
        {
            POFX_Rim RIMlayer = pOFXes[i].GetLayer(0) as POFX_Rim;
            if (RIMlayer == null)
            {
                pOFXes[i].AddLayer(pOFXes[i].gameObject.AddComponent<POFX_Rim>() as POFXLayer);
                RIMlayer = pOFXes[i].GetLayer(0) as POFX_Rim;
            }
            RIMlayer.m_cParams.intensity = 0f;
            POFX_RimBase pOFX_RimBase = pOFXes[i].GetComponent<POFX_RimBase>();
            if (pOFX_RimBase)
                pOFX_RimBase.m_params.rimpower = 0.9f;
            pOFXes[i].enabled = false;
        }
    }

    public void RimEffectsUp(Color color , float intensity,float time)
    {
        for (int i = 0; i < pOFXes.Count; i++)
        {
            pOFXes[i].enabled = true;
            POFX_Rim RIMlayer = pOFXes[i].GetLayer(0) as POFX_Rim;
            RIMlayer.m_cParams.color = color;
            DOTween.To(() => RIMlayer.m_cParams.intensity,x => RIMlayer.m_cParams.intensity = x, intensity, time);
        }
    }

    public void RimEffectsClear(float cleartime)
    {
        for (int i = 0; i < pOFXes.Count; i++)
        {
            POFX_Rim RIMlayer = pOFXes[i].GetLayer(0) as POFX_Rim;
            DOTween.To(() => RIMlayer.m_cParams.intensity,x => RIMlayer.m_cParams.intensity = x, 0, cleartime).OnComplete(() => { RIMlayer.enabled = false; });
        }
    }

    public void RimEffectsForAShortTime(float tartget_intensity, float time, Color targetColor)
    {
        for (int i = 0; i < pOFXes.Count; i++)
        {
            POFX_Rim RIMlayer = pOFXes[i].GetLayer(0) as POFX_Rim;
            RIMlayer.m_cParams.color = targetColor;
            DOTween.To(() => RIMlayer.m_cParams.intensity, x => RIMlayer.m_cParams.intensity = x, tartget_intensity, time).OnComplete(() => { DOTween.To(() => RIMlayer.m_cParams.intensity, x => RIMlayer.m_cParams.intensity = x, 0, time); });
        }
    }
}
