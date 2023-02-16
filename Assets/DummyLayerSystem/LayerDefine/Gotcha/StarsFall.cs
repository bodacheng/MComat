using DG.Tweening;
using UnityEngine;

public class StarsFall : MonoBehaviour
{
    public enum GachaType {
        normal,
        super
    }
    
    [SerializeField] Camera _camera;
    [SerializeField] Transform center;
    [SerializeField] GameObject lookTarget;
    [SerializeField] float skySphereRadius = 650;

    [SerializeField] ParticleSystem normalGachaEffect;
    [SerializeField] ParticleSystem superGachaEffect;
    
    public static StarsFall target;

    public Camera Camera => _camera;

    public Vector3 GetEffectCenter()
    {
        return normalGachaEffect.transform.position;
    }

    void Awake()
    {
        target = this;
        target.gameObject.SetActive(false);
    }

    public void LookReset()
    {
        _camera.transform.DOLookAt(lookTarget.transform.position, 1f);
    }

    public void TriggerHoleEffect(GachaType type)
    {
        switch (type)
        {
            case GachaType.normal:
                superGachaEffect.Stop();
                normalGachaEffect.Play();
                break;
            case GachaType.super:
                normalGachaEffect.Stop();
                superGachaEffect.Play();
                break;
        }
    }
    
    public Vector3 GetRandomStarPos()
    {
        var xzDisFromCenter = Random.Range(0, skySphereRadius * 2 / 3);
        var temp = center.transform.position + (Vector3.forward * Random.Range(-100, 100) + Vector3.right * Random.Range(-100, 100)).normalized * xzDisFromCenter;
        var height = Mathf.Sqrt(Mathf.Pow(skySphereRadius, 2) - Mathf.Pow(xzDisFromCenter, 2));
        var finalPos = temp + (int)(height - 10) * Vector3.up;
        return finalPos;
    }
}
