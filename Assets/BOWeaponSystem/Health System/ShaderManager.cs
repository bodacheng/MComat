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
    private List<Sequence> _sequences = new List<Sequence>();
    private bool _destroyed;
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
        _destroyed = true;
        ClearDoing();
    }

    #region Rim
    public void RimEffectsUp(Color color, float duration)
    {
        ClearDoing();
        if (meshes == null)
        {
            return;
        }

        meshes.ForEach(x =>
        {
            if (CanTween(x))
            {
                TweenerCore<Color, Color, ColorOptions> tweener = null;
                tweener = TweenEmission(x, color, duration).OnComplete(
                    () =>
                    {
                        if (tweener != null)
                        {
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
        if (meshes == null)
        {
            return;
        }

        meshes.ForEach(x =>
        {
            TweenerCore<Color, Color, ColorOptions> tweener = null;
            if (CanTween(x))
            {
                tweener = TweenEmission(x, Color.clear, duration).OnComplete(
                    () =>
                    {
                        if (tweener != null)
                        {
                            _tweenerCores.Remove(tweener);
                        }
                    });
                _tweenerCores.Add(tweener);
            }
        });
    }

    public bool HasDoing()
    {
        return _tweenerCores.Count > 0 || _sequences.Count > 0;
    }
    
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

        if (_sequences.Count > 0)
        {
            foreach (var sequence in _sequences)
            {
                sequence?.Kill();
            }
            _sequences.Clear();
        }
    }

    public void RimEffectsForAShortTime(Color targetColor, float duration)
    {
        ClearDoing();
        if (meshes == null)
        {
            return;
        }

        foreach (var mesh in meshes)
        {
            if (CanTween(mesh))
            {
                // 创建一个新的 Sequence
                var sequence = DOTween.Sequence().SetLink(gameObject);

                // 第一步：将 EmissionColor 从当前值渐变到 targetColor
                sequence.Append(
                    TweenEmission(mesh, targetColor, duration)
                );

                // 第二步：在第一步完成后，将 EmissionColor 从 targetColor 渐变回 Color.clear
                sequence.Append(
                    TweenEmission(mesh, Color.clear, duration)
                );
                
                sequence.OnComplete(() =>
                {
                    _sequences.Remove(sequence);
                });
                
                _sequences.Add(sequence);
            }
        }
    }

    #endregion

    #region 纯色
    public void FlatColor(Color targetColor, float duration)
    {
        ClearDoing();
        if (meshes == null)
        {
            return;
        }

        meshes.ForEach(x =>
        {
            if (CanTween(x))
            {
                TweenerCore<Color, Color, ColorOptions> tweener = null;
                tweener = TweenBaseColor(x, targetColor, duration).OnComplete(
                    () =>
                    {
                        if (tweener != null)
                        {
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
        if (meshes == null)
        {
            return;
        }

        foreach (var mesh in meshes)
        {
            if (CanTween(mesh))
            {
                // 创建一个新的 Sequence
                var sequence = DOTween.Sequence().SetLink(gameObject);

                // 第一步：将 EmissionColor 从当前值渐变到 targetColor，持续 addTime
                sequence.Append(
                    TweenEmission(mesh, targetColor, addTime)
                );

                // 第二步：将 EmissionColor 从 targetColor 渐变回 Color.clear，持续 fadeTime
                sequence.Append(
                    TweenEmission(mesh, Color.clear, fadeTime)
                );

                sequence.OnComplete(() =>
                {
                    _sequences.Remove(sequence);
                });
                
                _sequences.Add(sequence);
            }
        }
    }

    #endregion

    bool CanTween(DummyMesh mesh)
    {
        return !_destroyed && mesh != null && mesh.CurrentMaterials != null;
    }

    TweenerCore<Color, Color, ColorOptions> TweenEmission(DummyMesh mesh, Color color, float duration)
    {
        return DOTween.To(
            () => CanTween(mesh) ? mesh.EmissionColor : Color.clear,
            c =>
            {
                if (CanTween(mesh))
                {
                    mesh.EmissionColor = c;
                }
            },
            color,
            duration
        ).SetLink(gameObject);
    }

    TweenerCore<Color, Color, ColorOptions> TweenBaseColor(DummyMesh mesh, Color color, float duration)
    {
        return DOTween.To(
            () => CanTween(mesh) ? mesh.BaseColor : Color.clear,
            c =>
            {
                if (CanTween(mesh))
                {
                    mesh.BaseColor = c;
                }
            },
            color,
            duration
        ).SetLink(gameObject);
    }
}
