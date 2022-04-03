using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class AbstractShaderMesh : MonoBehaviour
{
    protected virtual Material GetMaterial() => null;
    protected Renderer mesh = default;
    private Material currentMaterial = default;
    
    private static readonly int kPropertyBaseColor = Shader.PropertyToID("_BaseColor");
    private static readonly int kPropertyEmissionColor = Shader.PropertyToID("_EmissionColor");

    public Material CurrentMaterial => currentMaterial;
        
    protected virtual void Awake()
    {
        mesh = GetComponent<Renderer>();
        if (mesh != null)
        {
            var mat = GetMaterial();
            if (mat != null) mesh.sharedMaterial = mat;
            currentMaterial = mesh.material;
        }
    }

    public Color BaseColor
    {
        get => currentMaterial.GetColor(kPropertyBaseColor);
        set => currentMaterial.SetColor(kPropertyBaseColor, value);
    }

    public Color EmissionColor
    {
        get => currentMaterial.GetColor(kPropertyEmissionColor);
        set => currentMaterial.SetColor(kPropertyEmissionColor, value);
    }
}
