using System.Collections.Generic;
using UnityEngine;

public abstract class CameraMode
{
    protected Transform meCenter;
    public Transform target;
    public List<Transform> targets;

    // 整个这一套玩意混乱在，下面这些不同的变量，在不同的相机模式下可能完全是不同的作用。
    // 只是有节约变量数的意义。
    public bool auto = true;
    protected float speed;
    protected float XZDis, YDis;
    protected float XZrosOffset, YrosOffset;
    public float duration,fieldOfView;
    
    public void SetMeCenter(Transform meCenter)
    {
        this.meCenter = meCenter;
    }
    
    public virtual void Enter(Camera _camera)
    {
    }

    public virtual void LocalUpdate(Camera _camera)
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

public enum C_Mode
{
    NULL = -1,
    RoundBoundary = 0,
    StartAndEnd = 1,
    CertainYAntiVibration = 12,
    WatchOver = 8,
    TopDown = 2,
    LerpToCertainPlace = 9,
    GodPlayerCertainYCamera = 10,
    keepTargetLeft = 13,
    ApproachToCertainDis = 14,
}