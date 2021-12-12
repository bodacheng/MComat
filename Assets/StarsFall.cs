using UnityEngine;

public class StarsFall : MonoBehaviour
{
    [SerializeField] public Camera _camera;
    [SerializeField] Transform center;
    [SerializeField] float SkySphereRadius = 650;
    
    public static StarsFall target;

    void Awake()
    {
        target = this;
        target.gameObject.SetActive(false);
    }
    
    public Vector3 GetRandomStarPos()
    {
        float xzDisFromCenter = Random.Range(0, SkySphereRadius * 2 / 3);
        Vector3 temp = center.transform.position + (Vector3.forward * Random.Range(0, 100) + Vector3.right * Random.Range(0, 100)).normalized * xzDisFromCenter;
        float height = Mathf.Sqrt(Mathf.Pow(SkySphereRadius, 2) - Mathf.Pow(xzDisFromCenter, 2));
        Vector3 finalPos = temp + (int)(height - 10) * Vector3.up;
        return finalPos;
    }
}
