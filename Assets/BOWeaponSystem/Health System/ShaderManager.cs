using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine.Rendering;

public class ShaderManager : MonoBehaviour
{
    [SerializeField] List<DummyMesh> meshes;
    private List<TweenerCore<Color, Color, ColorOptions>> _tweenerCores = new List<TweenerCore<Color, Color, ColorOptions>>();
    
    void Start()
    {
        #region outdated

        foreach (var mesh in meshes)
        {
            mesh.gameObject.layer = 3;
            var shadowMesh = Instantiate(mesh, mesh.transform, true);
            var transform1 = shadowMesh.transform;
            transform1.localPosition = Vector3.zero;
            transform1.localScale = Vector3.one * 0.9f;
            var o = shadowMesh.gameObject;
            o.name = "shadow_" + o.name;
            o.layer = 0;
            shadowMesh.Mesh.material = CommonSetting.ShadowMaterial;
            shadowMesh.Mesh.shadowCastingMode = ShadowCastingMode.On;
        }
        
        // for (int i = 0; i < pOFXes.Count; i++)
        // {
        //     // Rim
        //     RIMlayer = pOFXes[i].GetLayer(0) as POFX_Rim;
        //     if (RIMlayer == null)
        //     {
        //         pOFXes[i].AddLayer(pOFXes[i].gameObject.AddComponent<POFX_Rim>() as POFXLayer);
        //         RIMlayer = pOFXes[i].GetLayer(0) as POFX_Rim;
        //     }
        //     RIMlayer.m_cParams.intensity = 0f;
        //     POFX_RimBase pOFX_RimBase = pOFXes[i].GetComponent<POFX_RimBase>();
        //     if (pOFX_RimBase)
        //     {
        //         pOFX_RimBase.m_params.rimpower = 0.9f;
        //     }
        //     
        //     // flatColor
        //     POFX_FlatColor flatColorLayer = pOFXes[i].GetLayer(1) as POFX_FlatColor;
        //     if (flatColorLayer == null)
        //     {
        //         pOFXes[i].AddLayer(pOFXes[i].gameObject.AddComponent<POFX_FlatColor>() as POFXLayer);
        //         flatColorLayer = pOFXes[i].GetLayer(1) as POFX_FlatColor;
        //     }
        //     flatColorLayer.m_cParams.intensity = 0f;
        //     POFX_FlatColorBase flatColorLayerBase = pOFXes[i].GetComponent<POFX_FlatColorBase>();
        //     
        //     pOFXes[i].enabled = false;
        // }

        #endregion
    }

    void OnDestroy()
    {
        ClearDoing();
    }

    #region Rim
    public void RimEffectsUp(Color color, float duration)
    {
        ClearDoing();
        TweenerCore<Color, Color, ColorOptions> tweener = null;
        meshes.ForEach(x =>
        {
            if (x.CurrentMaterials != null)
            {
                tweener = DOTween.To(() => x.EmissionColor, c => x.EmissionColor = c, color, duration).OnComplete(
                    () =>
                    {
                        if (tweener != null)
                        {
                            tweener.Kill();
                            _tweenerCores.Remove(tweener);
                        }
                    });
                _tweenerCores.Add(tweener);
            }
        });
    }

    public void RimEffectsClear(float duration)
    {
        ClearDoing();
        meshes.ForEach(x =>
        {
            TweenerCore<Color, Color, ColorOptions> tweener = null;
            if (x.CurrentMaterials != null)
            {
                tweener = DOTween.To(() => x.EmissionColor, c => x.EmissionColor = c, Color.clear, duration).OnComplete(
                    () =>
                    {
                        if (tweener != null)
                        {
                            tweener.Kill();
                            _tweenerCores.Remove(tweener);
                        }
                    });
                _tweenerCores.Add(tweener);
            }
        });
    }

    public bool HasDoing()
    {
        return _tweenerCores.Count > 0;
    }

    private Sequence _sequence;
    void ClearDoing()
    {
        if (_tweenerCores.Count > 0)
        {
            foreach (var tweener in _tweenerCores)
            {
                tweener?.Kill();
            }
            _tweenerCores.Clear();
        }

        if (_sequence != null)
        {
            _sequence.Kill();
            _sequence = null;
        }
    }

    public void RimEffectsForAShortTime(Color targetColor, float duration)
    {
        ClearDoing();
        foreach (var mesh in meshes)
        {
            if (mesh.CurrentMaterials != null)
            {
                // 创建一个新的 Sequence
                _sequence = DOTween.Sequence();

                // 第一步：将 EmissionColor 从当前值渐变到 targetColor
                _sequence.Append(
                    DOTween.To(() => mesh.EmissionColor, c => mesh.EmissionColor = c, targetColor, duration)
                );

                // 第二步：在第一步完成后，将 EmissionColor 从 targetColor 渐变回 Color.clear
                _sequence.Append(
                    DOTween.To(() => mesh.EmissionColor, c => mesh.EmissionColor = c, Color.clear, duration)
                );
                
                _sequence.OnComplete(() =>
                {
                    _sequence.Kill();
                    _sequence = null;
                });
            }
        }
    }

    #endregion

    #region 纯色
    public void FlatColor(Color targetColor, float duration)
    {
        ClearDoing();
        meshes.ForEach(x =>
        {
            if (x.CurrentMaterials != null)
            {
                TweenerCore<Color, Color, ColorOptions> tweener = null;
                tweener = DOTween.To(() => x.BaseColor, c => x.BaseColor = c, targetColor, duration).OnComplete(
                    () =>
                    {
                        if (tweener != null)
                        {
                            tweener.Kill();
                            _tweenerCores.Remove(tweener);
                        }
                    });
                _tweenerCores.Add(tweener);
            }
        });
    }
    
    public void FlatColorForAShortTime(Color targetColor, float addTime, float fadeTime)
    {
        ClearDoing();
        foreach (var mesh in meshes)
        {
            if (mesh.CurrentMaterials != null)
            {
                // 创建一个新的 Sequence
                _sequence = DOTween.Sequence();

                // 第一步：将 EmissionColor 从当前值渐变到 targetColor，持续 addTime
                _sequence.Append(
                    DOTween.To(() => mesh.EmissionColor, c => mesh.EmissionColor = c, targetColor, addTime)
                );

                // 第二步：将 EmissionColor 从 targetColor 渐变回 Color.clear，持续 fadeTime
                _sequence.Append(
                    DOTween.To(() => mesh.EmissionColor, c => mesh.EmissionColor = c, Color.clear, fadeTime)
                );

                _sequence.OnComplete(() =>
                {
                    _sequence.Kill();
                    _sequence = null;
                });
            }
        }
    }

    #endregion
}
