using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static Camera _camera;
    public static Camera _subCamera;
    [SerializeField] Camera mainCamera;
    [SerializeField] Camera subCamera; // unit camera
    [SerializeField] Transform StartPosRef;
    [SerializeField] Transform topDownModeEndRef;
    CameraMode CurrentMode;
    public Transform TopDownModeEndRef => topDownModeEndRef;

    readonly IDictionary<C_Mode, CameraMode> CModeDic = new Dictionary<C_Mode, CameraMode>()
    {
        {C_Mode.CertainYAntiVibration, new ChatGptFix(15f, 5.5f, 25f)},
        //{C_Mode.CertainYAntiVibration, new New2023(8.8f, 5f)},
        {C_Mode.ApproachToCertainDis,  new LerpToCertainDistance(5f, 1f)},
        {C_Mode.keepTargetLeft, new keepTargetLeftCamera()},
        {C_Mode.WatchOver, new WatchOverCamera(7f, 5f)},
        {C_Mode.StartAndEnd, new StartToEndMode()},
        {C_Mode.RoundBoundary, new CenterSurroundCamera(25f, 10f)},
        {C_Mode.TopDown, new TouchTopDownCamera(12f, 20f, 25)},
        {C_Mode.ScreenSaver, new New2023(8.8f, 5f)}//new ScreenSaverC(8.8f, 8.8f)}
    };

    public void SetPosToStart()
    {
        _camera.transform.position = StartPosRef.position;
        _camera.transform.rotation = StartPosRef.rotation;
    }
    
    void Awake()
    {
        _camera = mainCamera;
        _subCamera = subCamera;
        _camera.depthTextureMode = DepthTextureMode.Depth;

        foreach (var kv in CModeDic)
        {
            kv.Value.cameraManager = this;
        }
    }
    
    void Update()
    {
        if (CurrentMode != null)
        {
            CurrentMode.LocalUpdate(_camera);
        }
    }
    
    public void SetCurrentCameraParams(Transform me, List<Transform> targets)
    {
        if (CurrentMode != null)
        {
            //有些相机模式的enter函数内处理需要根据targets来
            CurrentMode.SetMeCenter(me);
            CurrentMode.targets = targets;
        }
    }
    
    public void Assign_Camera(C_Mode num, Transform me, List<Transform> targets)
    {
        if (CurrentMode != null)
            CurrentMode.Exit(_camera);
        CModeDic.TryGetValue(num, out CurrentMode);
        if (CurrentMode != null)
        {
            //有些相机模式的enter函数内处理需要根据targets来
            CurrentMode.SetMeCenter(me);
            CurrentMode.targets = targets;
            CurrentMode.Enter(_camera);
        }
    }
}