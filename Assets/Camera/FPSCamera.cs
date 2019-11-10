using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//该模式就是去激活easytouch插件所提供的那组双手柄相机。
class FPSCamera : CameraMode
{
    public override void LocalLateUpdate(Camera _camera)
    {
    }
    
    public override void Enter(Camera _camera)
    {
        CameraManager.FPSControllersCanvas_static.gameObject.SetActive(true);
    }
}