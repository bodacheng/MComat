using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kalagaan.POFX;

public class ShaderManager : MonoBehaviour
{

    public List<POFX> pOFXes;
    public List<IEnumerator> colorchangeprocesses = new List<IEnumerator>();

    void Start()
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

    public void RimEffectsUp(Color color , float intensity,float lerpspeed)
    {
        return;
        for (int i = 0; i < colorchangeprocesses.Count; i++)
        {
            StopCoroutine(colorchangeprocesses[i]);
        }
        colorchangeprocesses.Clear();
        for (int i = 0; i < pOFXes.Count; i++)
        {
            IEnumerator process = this.rimUpGradually(pOFXes[i], intensity, lerpspeed, color);
            StartCoroutine(process);
            colorchangeprocesses.Add(process);
        }
    }

    public void RimEffectsClear()
    {
        return;
        //RimEffectsClear()的话就不再把其他颜色改变进程清理了，因为这样一个颜色清理进程可能会把其他颜色变化进程给阻断掉，最典型的例子是角色抵抗消失时候会把应该有的受伤变色给搞的不再有效果
        for (int i = 0; i < colorchangeprocesses.Count; i++)
        {
            StopCoroutine(colorchangeprocesses[i]);
        }
        colorchangeprocesses.Clear();
        for (int i = 0; i < pOFXes.Count; i++)
        {
            IEnumerator process = this.rimClearGradually(pOFXes[i]);
            StartCoroutine(process);
            colorchangeprocesses.Add(process);
        }
    }

    public void RimEffectsForAShortTime(float tartget_intensity,float lerpspeed, float time, Color targetColor)
    {
        return;
        for (int i = 0; i < colorchangeprocesses.Count; i++)
        {
            StopCoroutine(colorchangeprocesses[i]);
        }
        colorchangeprocesses.Clear();
        for (int i = 0; i < pOFXes.Count; i++)
        {
            IEnumerator process = this.changeRimColorForAShortTime(pOFXes[i], tartget_intensity, lerpspeed, time, targetColor);
            StartCoroutine(process);
            colorchangeprocesses.Add(process);
        }
    }

    IEnumerator changeRimColorForAShortTime(POFX _POFX, float tartget_intensity, float lerpspeed, float time, Color targetColor)
    {
        yield return this.rimUpGradually(_POFX, tartget_intensity, lerpspeed, targetColor);
        yield return new WaitForSeconds(time);
        yield return this.rimClearGradually(_POFX);
    }

    IEnumerator rimUpGradually(POFX _POFX, float tartget_intensity,float lerpspeed,Color targetColor)
    {
        //_POFX.enabled = true;//一开一关在这个插件上会造成每帧重新编译问题 ——by 周宇
        POFX_Rim RIMlayer = _POFX.GetLayer(0) as POFX_Rim;
        RIMlayer.m_cParams.color = targetColor;
        while (tartget_intensity - RIMlayer.m_cParams.intensity > 0)
        {
            RIMlayer.m_cParams.intensity += lerpspeed;
            yield return null;
        }
        yield break;
    }
    IEnumerator rimClearGradually(POFX _POFX)
    {
        POFX_Rim RIMlayer = _POFX.GetLayer(0) as POFX_Rim;
        while (RIMlayer.m_cParams.intensity > 0)
        {
            RIMlayer.m_cParams.intensity -= 0.15f;
            yield return null;
        }
        RIMlayer.m_cParams.intensity = 0;
        //_POFX.enabled = false;
        yield break;
    }
}
