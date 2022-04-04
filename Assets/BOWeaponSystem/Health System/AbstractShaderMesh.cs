using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class AbstractShaderMesh : MonoBehaviour
{
    protected virtual Material GetMaterial() => null;
    protected Renderer mesh = default;

    private static readonly int kPropertyBaseColor = Shader.PropertyToID("_BaseColor");
    private static readonly int kPropertyEmissionColor = Shader.PropertyToID("_EmissionColor");

    public Renderer Mesh => mesh;
    public Material CurrentMaterial { get; set; } = default;

    protected virtual void Awake()
    {
        mesh = GetComponent<Renderer>();
        if (mesh != null)
        {
            var mat = GetMaterial();
            if (mat != null) mesh.sharedMaterial = mat;
            CurrentMaterial = mesh.material;
        }
    }

    public Color BaseColor
    {
        get => CurrentMaterial.GetColor(kPropertyBaseColor);
        set => CurrentMaterial.SetColor(kPropertyBaseColor, value);
    }

    public Color EmissionColor
    {
        get => CurrentMaterial.GetColor(kPropertyEmissionColor);
        set => CurrentMaterial.SetColor(kPropertyEmissionColor, value);
    }
}
