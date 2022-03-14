using System;
using UnityEngine;
using DG.Tweening;

class New2023 : CameraMode
{
    Vector3 cameraTargetPos;
    Vector3 enemiesCenter; //敌人的位置中心
    Quaternion ToRotation; //目标相机旋角
    Vector3 rotateToDirection;
    Vector2 meScreenPos;
    Vector2 enemyScreenPos;
    Vector3 xzOff = - Vector3.forward; //相机从focuscenter出发的角度
    Vector3 lookPoint;
    float lookPointHeight = 2.3f;
    Vector3 frontWPos, backWPos;
    Vector2 frontScreenPos, backScreenPos;

    private float _changeSpeed = 0.7f;
    private readonly float minXZ;
    
    private float transitionSpeedPara = 10f;
    
    public New2023(float XZDis, float YDis)
    {
        minXZ = XZDis;
        this.XZDis = XZDis;
        this.YDis = YDis;
    }

    private float XZDistance
    {
        get => XZDis;
        set
        {
            XZDis = Mathf.Clamp(value, minXZ , minXZ + 20f);
        }
    }

    public override void Enter(Camera _camera)
    {
        LocalUpdate(_camera);
        xzOff = _camera.transform.position - lookPoint;
        xzOff.y = 0;
        transitionSpeedPara = 10f;
        DOTween.To(()=> transitionSpeedPara, (x) => transitionSpeedPara = x, 0.2f, 1f);
    }

    float fixY, h;
    private float c_offSet;//相机位置相对双方连线的偏移点，同时也是相机朝向的偏移变量，由rate计算而出，在rate为0和1时候，为0.5。

    private float ePosX;
    private float ePosY;
    private float mPosX;
    private float mPosY;
    public override void LocalUpdate(Camera _camera)
    {
        h = UltimateJoystick.GetHorizontalAxis("RotateCamera");
        if (h != 0)
        {
            xzOff = Quaternion.AngleAxis(h * 1.5f, Vector3.up) * xzOff;
            xzOff.y = 0;
        }
        
        _changeSpeed = Time.deltaTime / (transitionSpeedPara + Time.deltaTime);//分母里那个附加值越大，变得越慢。
        
        enemiesCenter = Vector3.zero;
        if (targets != null && targets.Count > 0)
        {
            foreach (var o in targets)
            {
                if (o != null)
                {
                    enemiesCenter += o.transform.position;
                }
            }
        }
        else
        {
            enemiesCenter = meCenter.position;
        }

        enemiesCenter /= targets.Count;

        enemyScreenPos = _camera.WorldToScreenPoint(enemiesCenter);
        meScreenPos = _camera.WorldToScreenPoint(meCenter.position);

        ePosX = (float)((decimal)enemyScreenPos.x / Screen.width);
        ePosY = (float)((decimal)enemyScreenPos.y / Screen.height);
        mPosX = (float)((decimal)meScreenPos.x / Screen.width);
        mPosY = (float)((decimal)meScreenPos.y / Screen.height);
        
        enemyScreenPos = new Vector2((enemyScreenPos.x /Screen.width),(enemyScreenPos.y /Screen.height));
        meScreenPos = new Vector2((meScreenPos.x /Screen.width), (meScreenPos.y /Screen.height));
        
        // 判断我与敌人哪个更接近相机位置
        if (enemyScreenPos.y >= meScreenPos.y)
        {
            frontWPos = meCenter.position;
            frontScreenPos = meScreenPos;
            backWPos = enemiesCenter;
            backScreenPos = enemyScreenPos;
        }
        else
        {
            frontWPos = enemiesCenter;
            frontScreenPos = enemyScreenPos;
            backWPos = meCenter.position;
            backScreenPos = meScreenPos;
        }
        
        if (ePosX >= 0.3 && ePosX <= 0.7 &&
            mPosX >= 0.3 && mPosX <= 0.7 &&
            ePosY >= 0.3 && ePosY <= 0.7 &&
            mPosY >= 0.3 && mPosY <= 0.7)
        {
            XZDistance -= _changeSpeed;
        }
        else if (ePosX <= 0.2 || ePosX >= 0.8 || 
                 mPosX <= 0.2 || mPosX >= 0.8 || 
                 ePosY <= 0.2 || ePosY >= 0.8 || 
                 mPosY <= 0.2 || mPosY >= 0.8)
        {
            XZDistance += _changeSpeed;
        }
        
        //Debug.Log(ePosX + ":"+ ePosY);
        //Debug.Log(mPosX + ":"+ mPosY);
        
        lookPoint = (backWPos - frontWPos) * 0.5f + frontWPos;
        cameraTargetPos = lookPoint + xzOff.normalized * XZDistance;
        cameraTargetPos.y = YDis;
        lookPoint.y = lookPointHeight;
        
        _camera.transform.position = Vector3.Lerp(_camera.transform.position, cameraTargetPos, _changeSpeed);
        rotateToDirection = lookPoint - cameraTargetPos;
        ToRotation = Quaternion.LookRotation(rotateToDirection.normalized);
        _camera.transform.rotation = Quaternion.Slerp(_camera.transform.rotation, ToRotation, _changeSpeed);
    }
}