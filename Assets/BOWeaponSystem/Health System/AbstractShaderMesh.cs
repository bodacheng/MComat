using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class AbstractShaderMesh : MonoBehaviour
{
    protected Material[] GetMaterials() => Mesh != null ? Mesh.sharedMaterials : null;
    protected Renderer mesh = default;

    private static readonly int kPropertyBaseColor = Shader.PropertyToID("_BaseColor");
    private static readonly int kPropertyEmissionColor = Shader.PropertyToID("_EmissionColor");

    public Renderer Mesh => mesh;
    public Material[] CurrentMaterials { get; set; } = default;

    protected virtual void Awake()
    {
        mesh = GetComponent<Renderer>();
        if (mesh != null)
        {
            CurrentMaterials = GetMaterials();
        }
    }

    public Color BaseColor
    {
        get
        {
            foreach (var m in CurrentMaterials)
            {
                return m.GetColor(kPropertyBaseColor);
            }
            return Color.clear;
        }
        set
        {
            foreach (var m in CurrentMaterials)
            {
                m.SetColor(kPropertyBaseColor, value);
            }
        }
    }

    public Color EmissionColor
    {
        get
        {
            foreach (var m in CurrentMaterials)
            {
                return m.GetColor(kPropertyEmissionColor);
            }
            return Color.clear;
        }
        set
        {
            foreach (var m in CurrentMaterials)
            {
                m.SetColor(kPropertyEmissionColor, value);
            }
        }
    }
}
