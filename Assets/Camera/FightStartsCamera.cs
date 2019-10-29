using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightStartsCamera : CameraMode
{
    Vector3 Xi;
    Vector3 center;
    Quaternion ToRotation;

    Animation cameraPathAnim;

    public FightStartsCamera(float speed)
    {
        this.speed = speed;
    }

    public override void LocalLateUpdate(Camera _camera)
    {
    }
    
    public override void Enter(Camera _camera)
    {
        cameraPathAnim = _camera.gameObject.GetComponent<Animation>();
        cameraPathAnim.Play();
    }
}

