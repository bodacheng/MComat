using UnityEngine;
using DG.Tweening;
public class GangV : CameraMode
{
    readonly Vector3 centerOnGround = Vector3.zero;
    readonly float distance = 25f; // Distance from the target
    readonly float height = 22f; // Height above the target
    readonly float rotationSpeed = 1f; // Speed at which the camera rotates
    Vector3 firstPoint;
    Vector3 secondPoint;
    bool isRotating = false;

    public GangV(float distance, float height)
    {
        this.distance = distance;
        this.height = height;
    }

    public override void LocalUpdate(Camera _camera)
    {
        var RotateCameraH = UltimateJoystick.GetHorizontalAxis("RotateCamera");
        var RotateCameraV = UltimateJoystick.GetHorizontalAxis("RotateCamera");

        bool OnPad()
        {
            return RotateCameraH != 0 || RotateCameraV != 0;
        }
        
        if (Input.touchCount >= 1)
        {
            Touch t1 = Input.GetTouch (0);
            if (t1.phase == TouchPhase.Began) // 触摸开始
            {
                firstPoint = t1.position;
                isRotating = true;
            }
            else if (t1.phase == TouchPhase.Moved && isRotating) // 触摸移动
            {
                if (OnPad())
                    CameraRotate(_camera, firstPoint, t1.position);
                firstPoint = t1.position;
            }
            else if (t1.phase == TouchPhase.Ended) // 触摸结束
            {
                isRotating = false;
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(1))
            {
                firstPoint = Input.mousePosition;
                isRotating = true;
            }
            else if (Input.GetMouseButton(1) && isRotating) // 触摸移动
            {
                if (OnPad())
                    CameraRotate(_camera, firstPoint, Input.mousePosition);
                firstPoint = Input.mousePosition;
            }
            else if (Input.GetMouseButtonUp(1))
            {
                isRotating = false;
            }
        }

    }
    
    public override void Enter(Camera _camera)
    {
        var tempF = _camera.transform.forward;
        tempF.y = 0;
        var startPos = Vector3.zero + tempF * distance + Vector3.up * height;
        _camera.transform.position = startPos;
        _camera.transform.LookAt(centerOnGround);
    }
    
    void CameraRotate(Camera camera, Vector3 _firstPoint, Vector3 _secondPoint)
    {
        float deltaX = _secondPoint.x - _firstPoint.x;
        float rotationAngle = deltaX * rotationSpeed;
        camera.transform.RotateAround(centerOnGround, Vector3.up, rotationAngle);
    }
}
