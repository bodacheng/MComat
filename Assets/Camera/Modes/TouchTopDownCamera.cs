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
    Vector3 firstPoint;
    Vector3 secondPoint;
    Sequence mainSequence;
    bool canTouch;

    public TouchTopDownCamera(float height,float battlefieldRadius)
    {
        this.height = height;
        this.battlefieldRadius = battlefieldRadius;
    }
    
    Vector3 temp;
    public override void Enter(Camera camera)
    {
        mainSequence = DOTween.Sequence().OnStart(() =>
        {
            canTouch = false;
            sameHeightCenter = new Vector3(0,height,0);
            temp = camera.transform.forward;
            temp.y = 0;
        }).Append(camera.transform.DOMoveY(height, 0.2f)).
        Join(camera.transform.DOLookAt(temp  - Vector3.up, 0.5f, AxisConstraint.None,Vector3.up)).AppendCallback(() => { canTouch = true; });
        mainSequence.Play();        
    }
    
    public override void LocalUpdate(Camera camera)
    {
        if (!canTouch)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            firstPoint = Input.mousePosition;
        }
        if(Input.GetMouseButton(0))
        {
            secondPoint = Input.mousePosition;
            CameraDrag(camera,firstPoint, secondPoint);
        }
    }

    Vector3 cForward;
    Vector3 sameHeightCenter;
    void CameraDrag(Camera camera, Vector3 _firstPoint, Vector3 _secondPoint)
    {
        var transform = camera.transform;
        var position = transform.position;
        position += ((_firstPoint.x - _secondPoint.x) / Screen.width)  * transform.right;
        cForward = transform.forward;
        cForward.y = 0;
        position += ((_firstPoint.y - _secondPoint.y) / Screen.height) * cForward;
        transform.position = position;
        if (Vector3.Distance(camera.transform.position, (sameHeightCenter - cForward * 11f)) > battlefieldRadius)
        {
            camera.transform.position = (camera.transform.position - (sameHeightCenter - cForward * 11f)).normalized * battlefieldRadius + (sameHeightCenter - cForward * 11f);
        }
    }
}
