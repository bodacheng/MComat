using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LerpToCertainPlace : CameraMode
{
    protected Vector3 obj_position;
    protected Quaternion obj_rotation;
    
    public void setObjPosAndRotAndSpeed(Vector3 obj_position, Quaternion obj_rotation, float duration,float fieldOfView)
    {
        this.obj_position = obj_position;
        this.obj_rotation = obj_rotation;
        this.duration = duration;
        this.fieldOfView = fieldOfView;
    }
    
    public void setObjPosAndRotAndSpeed(Vector3 obj_position, Quaternion obj_rotation)
    {
        this.obj_position = obj_position;
        this.obj_rotation = obj_rotation;
    }
    
    public override void Enter(Camera _camera)
    {
        _camera.DOFieldOfView(this.fieldOfView,this.duration);
        _camera.transform.DOMoveX(this.obj_position.x,this.duration);
        _camera.transform.DOMoveY(this.obj_position.y,this.duration);
        _camera.transform.DOMoveZ(this.obj_position.z,this.duration);
        _camera.transform.DORotateQuaternion(this.obj_rotation,this.duration);
    }
}