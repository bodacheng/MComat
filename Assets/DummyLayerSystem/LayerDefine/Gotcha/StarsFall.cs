using DG.Tweening;
using UnityEngine;

public class StarsFall : MonoBehaviour
{
    public Camera _camera;
    [SerializeField] Transform center;
    [SerializeField] private GameObject lookTarget;
    [SerializeField] float skySphereRadius = 650;
    
    public static StarsFall target;

    void Awake()
    {
        target = this;
        target.gameObject.SetActive(false);
    }

    public void LookReset()
    {
        _camera.transform.DOLookAt(lookTarget.transform.position, 1f);
    }
    
    public Vector3 GetRandomStarPos()
    {
        var xzDisFromCenter = Random.Range(0, skySphereRadius * 2 / 3);
        var temp = center.transform.position + (Vector3.forward * Random.Range(0, 100) + Vector3.right * Random.Range(0, 100)).normalized * xzDisFromCenter;
        var height = Mathf.Sqrt(Mathf.Pow(skySphereRadius, 2) - Mathf.Pow(xzDisFromCenter, 2));
        var finalPos = temp + (int)(height - 10) * Vector3.up;
        return finalPos;
    }
}
