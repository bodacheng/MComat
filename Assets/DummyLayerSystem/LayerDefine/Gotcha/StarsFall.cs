using UnityEngine;
using UnityEngine.Rendering.Universal;
using mainMenu;

public class StarsFall : MonoBehaviour
{
    public Camera _camera;
    public Camera _UICamera;
    [SerializeField] Transform center;
    [SerializeField] float SkySphereRadius = 650;
    
    public static StarsFall target;

    void Awake()
    {
        target = this;
        target.gameObject.SetActive(false);
    }

    public void Enter()
    {
        var baseC =  PreScene.target.FxCamera.GetUniversalAdditionalCameraData();
        
        var cameraData = _camera.GetUniversalAdditionalCameraData();
        cameraData.renderType = CameraRenderType.Overlay;
        
    }
    
    public Vector3 GetRandomStarPos()
    {
        var xzDisFromCenter = Random.Range(0, SkySphereRadius * 2 / 3);
        var temp = center.transform.position + (Vector3.forward * Random.Range(0, 100) + Vector3.right * Random.Range(0, 100)).normalized * xzDisFromCenter;
        var height = Mathf.Sqrt(Mathf.Pow(SkySphereRadius, 2) - Mathf.Pow(xzDisFromCenter, 2));
        var finalPos = temp + (int)(height - 10) * Vector3.up;
        return finalPos;
    }
}
