using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering;

public class ShaderManager : MonoBehaviour
{
    [SerializeField] List<DummyMesh> meshes;
    
    void Start()
    {
        #region outdated

        foreach (var mesh in meshes)
        {
            mesh.gameObject.layer = 3;
            var shadowMesh = Instantiate(mesh);
            shadowMesh.transform.parent = mesh.transform;
            shadowMesh.transform.localPosition = Vector3.zero;
            shadowMesh.transform.localScale = Vector3.one * 0.9f;
            shadowMesh.gameObject.name = "shadow_" + shadowMesh.gameObject.name;
            shadowMesh.gameObject.layer = 0;
            shadowMesh.Mesh.material = FightGlobalSetting._shadowMaterial;
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

    #region Rim
    public void RimEffectsUp(Color color, float duration)
    {
        meshes.ForEach(x =>
        {
            if (x.CurrentMaterials != null)
                DOTween.To(() => x.EmissionColor, c => x.EmissionColor = c, color, duration);
        });
    }

    public void RimEffectsClear(float duration)
    {
        meshes.ForEach(x =>
        {
            if (x.CurrentMaterials != null)
                DOTween.To(() => x.EmissionColor, c => x.EmissionColor = c, Color.clear, duration);
        });
    }

    public void RimEffectsForAShortTime(Color targetColor, float duration)
    {
        meshes.ForEach(x =>
        {
            if (x.CurrentMaterials != null)
            DOTween.To(() => x.EmissionColor, c => x.EmissionColor = c, targetColor, duration).OnComplete(
                () =>
                {
                    DOTween.To(() => x.EmissionColor, c => x.EmissionColor = c, Color.clear, duration);
                }
            );
        });
    }
    #endregion

    #region 纯色
    public void FlatColor(Color targetColor, float duration)
    {
        meshes.ForEach(x =>
        {
            if (x.CurrentMaterials != null)
            DOTween.To(() => x.BaseColor, c => x.BaseColor = c, targetColor, duration);
        });
    }
    
    public void FlatColorForAShortTime(Color targetColor, float addTime, float fadeTime)
    {
        meshes.ForEach(x =>
        {
            if (x.CurrentMaterials != null)
            DOTween.To(() => x.EmissionColor, c => x.EmissionColor = c, targetColor, addTime).OnComplete(
                () =>
                {
                    DOTween.To(() => x.EmissionColor, c => x.EmissionColor = c, Color.clear, fadeTime);
                }
            );
        });
    }
    #endregion
}
