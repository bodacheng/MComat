using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class AbstractShaderMesh : MonoBehaviour
{
    protected virtual Material GetMaterial() => null;
    protected Renderer mesh = default;
    protected Material currentMaterial = default;
        
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
        get => currentMaterial != null ? currentMaterial.GetColor(kPropertyBaseColor) : Color.white;
        set
        {
            if (currentMaterial != null) currentMaterial.SetColor(kPropertyBaseColor, value);
        }
    }

    public Color EmissionColor
    {
        get => currentMaterial != null ? currentMaterial.GetColor(kPropertyEmissionColor) : Color.black;
        set
        {
            if (currentMaterial != null)
            {
                currentMaterial.SetColor(kPropertyEmissionColor, value);
            }
        }
    }
}
