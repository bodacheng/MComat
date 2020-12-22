using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static Camera _camera;
    public CameraMode CurrentMode;

    public readonly IDictionary<C_Mode, CameraMode> CModeDic = new Dictionary<C_Mode, CameraMode>()
    {
        {C_Mode.GodPlayerCertainYCamera,new GodPlayerCertainY(5f, 5f)},
        {C_Mode.CertainYAntiVibration, new CertainYAntiVabration(8.8f, 5f)},
        {C_Mode.OneVOne, new OneVOneMode(16f)},
        {C_Mode.ApproachToCertainDis,  new LerpToCertainDistance(5f, 1f)},
        {C_Mode.keepTargetLeft, new keepTargetLeftCamera()},
        {C_Mode.WatchOver, new WatchOverCamera(7f, 5f)},
        {C_Mode.StartAndEnd, new StartToEndMode()},
        {C_Mode.RoundBoundary, new CenterSurroundCamera(25f, 10f)},
        {C_Mode.TopDown, new TouchTopDownCamera(12f, 20f)},
        {C_Mode.ScreenSaver, new ScreenSaverC(8.8f, 8.8f)}
    };
    
    void Awake()
    {
        _camera = gameObject.GetComponent<Camera>();
        _camera.depthTextureMode = DepthTextureMode.Depth;
    }

    void Start()
    {
        //Screen.SetResolution(1080, 720, true, 60);
    }
    
    void Update()
    {
        if (CurrentMode != null)
        {
            CurrentMode.LocalUpdate(_camera);
        }
    }

    public void Assign_Camera(C_Mode num, List<Transform> targets)
    {
        CModeDic.TryGetValue(num, out CurrentMode);
        if (CurrentMode != null)
        {
            //有些相机模式的enter函数内处理需要根据targets来
            CurrentMode.targets = targets;
            CurrentMode.Enter(_camera);
        }
    }
    
    public void Assign_SToEMode(Vector3 obj_p, Transform _target ,float duration, float sizeoffield)
    {
        CModeDic.TryGetValue(C_Mode.StartAndEnd, out CurrentMode);
        StartToEndMode _LerpToCertainPlace = (StartToEndMode)CurrentMode;
        _LerpToCertainPlace.SetObjPosAndRotAndSpeed(obj_p, duration, sizeoffield);
        CurrentMode.target = _target;
        CurrentMode.Enter(_camera);
    }
}