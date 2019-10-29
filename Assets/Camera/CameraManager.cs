using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Camera_Mode_Num
{
    startAndEnd = 1,
    GodPlayerMode = 3,
    CertainYAntiVibrationCamera = 12,
    WatchOver = 8,
    LerpToCertainPlace = 9,
    GodPlayerCertainYCamera = 10,
    GodWatchCamera = 11,
    keepTargetLeft = 13,
    approachToCertainDistance = 14,
}

public class CameraManager : MonoBehaviour
{
    public static Camera _camera;
    public CameraMode current_Camera_Mode;
    
    private readonly IDictionary<Camera_Mode_Num, CameraMode> camera_Mode_Dictionary = new Dictionary<Camera_Mode_Num, CameraMode>()
    {
        {Camera_Mode_Num.GodPlayerCertainYCamera,new GodPlayerCertainYCamera(5f, 5f, 2f)},
        {Camera_Mode_Num.CertainYAntiVibrationCamera, new CertainYAntiVibrationCamera(7f, 4f)},
        {Camera_Mode_Num.GodPlayerMode, new GodplayerCamera(12f, 5f)},
        {Camera_Mode_Num.LerpToCertainPlace, new LerpToCertainPlace()},
        {Camera_Mode_Num.approachToCertainDistance,  new LerpToCertainDistance(5f,1f)},
        {Camera_Mode_Num.GodWatchCamera, new GodWatchCamera(10f, 10f)},
        {Camera_Mode_Num.keepTargetLeft, new keepTargetLeftCamera()},
        {Camera_Mode_Num.WatchOver, new WatchOverCamera(7f,5f)},
        {Camera_Mode_Num.startAndEnd, new StartToEndMode()},             
    };

    void Awake()
    {
        _camera = this.gameObject.GetComponent<Camera>();
        _camera.depthTextureMode = DepthTextureMode.Depth;
    }

    void Start()
    {
        Screen.SetResolution(1080, 720, true, 60);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void LateUpdate()
    {
        if (current_Camera_Mode != null)
        {
            current_Camera_Mode.LocalLateUpdate(_camera);
        }
    }

    public void Assign_Camera(Camera_Mode_Num num, List<Transform> targets)
    {
        this.camera_Mode_Dictionary.TryGetValue(num, out current_Camera_Mode);
        this.current_Camera_Mode.targets = targets;//有些相机模式的enter函数内处理需要根据targets来
        this.current_Camera_Mode.Enter(_camera);
    }
    
    public void Assign_StartToEndModeCamera(Vector3 p, float duration,float sizeoffield)
    {
        this.camera_Mode_Dictionary.TryGetValue(Camera_Mode_Num.startAndEnd, out current_Camera_Mode);
        StartToEndMode _LerpToCertainPlace = (StartToEndMode)current_Camera_Mode;
        _LerpToCertainPlace.setObjPosAndRotAndSpeed(p, duration, sizeoffield);
        this.current_Camera_Mode.Enter(_camera);
    }
    
    public void Assign_StartToEndModeCamera(Vector3 p,float duration)
    {
        this.camera_Mode_Dictionary.TryGetValue(Camera_Mode_Num.startAndEnd, out current_Camera_Mode);
        StartToEndMode _LerpToCertainPlace = (StartToEndMode)current_Camera_Mode;
        _LerpToCertainPlace.setObjPosAndRotAndSpeed(p, duration);
        this.current_Camera_Mode.Enter(_camera);
    }
}

public abstract class CameraMode
{
    protected Transform meCenter;
    public Transform target;
    public List<Transform> targets;

    // 整个这一套玩意混乱在，下面这些不同的变量，在不同的相机模式下可能完全是不同的作用。
    // 只是有节约变量数的意义。
    protected float speed;
    protected float XZDis, YDis;
    protected float XZrosOffset, YrosOffset;
    public float duration,fieldOfView;
    
    public void SetMeCenter(Transform meCenter)
    {
        this.meCenter = meCenter;
    }

    public void SetCertainYModeParameters(float XZDis, float YDis, Transform meCenter)
    {
        this.YDis = YDis;
        this.XZDis = XZDis;
        this.meCenter = meCenter;
    }

    public void SetWatchOverModeParas(float XZdis,float Ydis, float XZrosOffset, float YrosOffset, float speed)
    {
        this.XZDis = XZdis;
        this.YDis = Ydis;
        this.XZrosOffset = XZrosOffset;
        this.YrosOffset = YrosOffset;
        this.speed = speed;
    }
    
    public virtual void Enter(Camera _camera)
    {
    }

    public virtual void LocalLateUpdate(Camera _camera)
    {
    }

    //下面这个函数的意思是说，把这个original往水平，垂直方向分别扭它一个角度，返回新的方向。。。到底可不可靠最近没验证过
    public static Vector3 GetDirection(Vector3 original, float offsetAngle, float chuizhiangle)
    {
        //初始方向是物体正前方
        Vector3 targetDir = Vector3.zero;
        //求得物体绕其自身Y轴旋转指定角度后的四元数值
        Quaternion offsetRot = Quaternion.AngleAxis(offsetAngle, Vector3.up);
        //用求得的四元数值 * 初始方向 即为目标方向  （Mark: 必须是 Quaterion * Vector 不能写成 Vector * Quaterion） 
        Quaternion offsetRot2 = Quaternion.AngleAxis(chuizhiangle, Vector3.left);
        targetDir = offsetRot2 * offsetRot * original;
        return targetDir.normalized;
    }
}
