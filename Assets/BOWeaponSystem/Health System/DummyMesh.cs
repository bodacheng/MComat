using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyMesh : AbstractShaderMesh
{
    [SerializeField] private Material dummyMaterial = default;
    protected override Material GetMaterial() => dummyMaterial;

    //property
    private static readonly int kPropertyAlpha = Shader.PropertyToID("_Alpha");

    public void SetAlpha(float alpha)
    {
        mesh.material.SetFloat(kPropertyAlpha, alpha);
    }
        
#if UNITY_EDITOR
    public void SetMaterial(Material material)
    {
        dummyMaterial = material;
        GetComponent<Renderer>().sharedMaterial = material;
    }

    public void UpdateMaterial()
    {
        GetComponent<Renderer>().sharedMaterial = dummyMaterial;
    }
#endif
        
    protected override void Awake()
    {
        base.Awake();
        SetAlpha(0f); // reset to clear
    }
}
