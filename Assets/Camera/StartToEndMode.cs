using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StartToEndMode : CameraMode
{
    protected Vector3 obj_position;
    protected Quaternion torotation;
    float yOffset;
    
    public void SetObjPosAndRotAndSpeed(Vector3 obj_position, float duration,float fieldOfView)
    {
        this.obj_position = obj_position;
        this.duration = duration;
        this.fieldOfView = fieldOfView;
    }
    
    public void SetObjPosAndRotAndSpeed(Vector3 obj_position, float duration)
    {
        this.obj_position = obj_position;
        this.duration = duration;
    }
        
    public override void Enter(Camera _camera)
    {
        _camera.DOFieldOfView(this.fieldOfView,this.duration);
    }
    
    Vector3 FirstPoint;
    Vector3 SecondPoint;
    float touchZeroscreenposx;
    float touchZeroscreenposy;
    public override void LocalLateUpdate(Camera _camera)
    {
        if (target == null)
            return;

        if (UnityEngine.Input.GetMouseButtonDown(0))//Input.GetTouch(0).phase == TouchPhase.Began
        {
            FirstPoint = UnityEngine.Input.mousePosition;
        }
        else if (UnityEngine.Input.GetMouseButton(0))
        {
            SecondPoint = UnityEngine.Input.mousePosition;
            touchZeroscreenposx = FirstPoint.x / Screen.width;
            touchZeroscreenposy = FirstPoint.y / Screen.height;
            if (touchZeroscreenposx < 0.1f || touchZeroscreenposx > 0.5f || touchZeroscreenposy < 0.1f || touchZeroscreenposy > 0.8f)
            {
                // 点击位置太靠近屏幕边缘。只有在画面左边的手指操作才能zoom相机。
            }else{
                yOffset +=  (FirstPoint.y - SecondPoint.y) / Screen.height * 5;
                yOffset = Mathf.Clamp(yOffset, 0,5f);
            }
        }
        _camera.transform.position = Vector3.Lerp(_camera.transform.position, obj_position + new Vector3(0,yOffset,0), 2 * Time.deltaTime/(0.01f + Time.deltaTime));
        torotation = Quaternion.LookRotation(target.position -  (obj_position + new Vector3(0, yOffset, 0)) , Vector3.up);
        _camera.transform.rotation = Quaternion.Slerp(_camera.transform.rotation, torotation, 10 * Time.deltaTime / (0.01f + Time.deltaTime));       
    }
}