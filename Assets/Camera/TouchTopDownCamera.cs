using UnityEngine;
using DG.Tweening;

//本相机控制模式也是基于圆形战场，并且相机的朝向固定为Vector3.foward，偏朝下俯视。
//这里的相机实际的活动范围是一个半径小于战场的圆内，并且由于相机在观察战场的时候的角度关系，
//这个更小的圆形区域是向屏幕方向正外偏离战场的圆心。
//进一步的改修可以考虑以相机的活动范围为参考制定手指划过屏幕时的相机移动范围

public class TouchTopDownCamera : CameraMode
{
    readonly float height;
    readonly float battlefieldRadius;
    Vector3 FirstPoint;
    Vector3 SecondPoint;
    Sequence mainSequence;
    bool canTouch;

    public TouchTopDownCamera(float height,float battlefieldRadius)
    {
        this.height = height;
        this.battlefieldRadius = battlefieldRadius;
    }
    
    Vector3 temp;
    public override void Enter(Camera _camera)
    {
        mainSequence = DOTween.Sequence().OnStart(() =>
        {
            canTouch = false;
            sameHeightCenter = new Vector3(0,height,0);
            temp = _camera.transform.forward;
            temp.y = 0;
        }).Append(_camera.transform.DOMoveY(height, 0.2f)).
        Join(_camera.transform.DOLookAt(temp  - Vector3.up, 0.5f, AxisConstraint.None,Vector3.up)).AppendCallback(() => { canTouch = true; });
        mainSequence.Play();        
    }
    
    public override void LocalUpdate(Camera _camera)
    {
        if (!canTouch)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            FirstPoint = Input.mousePosition;
        }
        if(Input.GetMouseButton(0))
        {
            SecondPoint = Input.mousePosition;
            CameraDrag(_camera,FirstPoint,SecondPoint);
        }
    }

    Vector3 c_foward;
    Vector3 sameHeightCenter;
    void CameraDrag(Camera _camera, Vector3 _FirstPoint, Vector3 _SecondPoint)
    {
        _camera.transform.position += ((_FirstPoint.x - _SecondPoint.x) / Screen.width)  * _camera.transform.right;
        c_foward = _camera.transform.forward;
        c_foward.y = 0;
        _camera.transform.position += ((_FirstPoint.y - _SecondPoint.y) / Screen.height) * c_foward;
        if (Vector3.Distance(_camera.transform.position, (sameHeightCenter - c_foward * 11f)) > battlefieldRadius)
        {
            _camera.transform.position = (_camera.transform.position - (sameHeightCenter - c_foward * 11f)).normalized * battlefieldRadius + (sameHeightCenter - c_foward * 11f);
        }
    }
}
