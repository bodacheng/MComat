using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpToCertainDistance : CameraMode
{
    private float distancefromtarget;
    public LerpToCertainDistance(float distance,float speed)
    {
        this.distancefromtarget = distance;
        this.speed = speed;
    }

    private Vector3 targetcenter;
    public override void LocalLateUpdate(Camera _camera)
    {
        targetcenter = Vector3.zero;
        for (int i = 0; i < targets.Count;i++)
        {
            targetcenter += targets[i].position;
        }
        targetcenter = targetcenter / targets.Count;
        if (Vector3.Distance(targetcenter,_camera.transform.position) > distancefromtarget)
            _camera.transform.position = Vector3.Lerp(_camera.transform.position, targetcenter, speed * Time.deltaTime);
        else
            _camera.transform.position = Vector3.Lerp(_camera.transform.position, (_camera.transform.position - targetcenter) + _camera.transform.position, speed * Time.deltaTime);
    }
}
