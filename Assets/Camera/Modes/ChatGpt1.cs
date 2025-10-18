using UnityEngine;
using DG.Tweening;

class ChatGptFix : CameraMode
{
    Vector3 cameraTargetPos;
    Vector3 enemiesCenter;
    Vector3 rotateToDirection;
    Vector2 meScreenPos;
    Vector2 enemyScreenPos;
    Vector3 xzOff;
    Vector3 lookPoint;
    Vector3 frontWPos, backWPos;
    Quaternion ToRotation;
    float autoChangeAngleLimit = 15f;
    float autoRotateSpeed = 160f;
    float _changeSpeed;
    readonly float _lookPointHeight = 1.5f;
    readonly float _minXZ;
    readonly float _minY;
    float fieldOfView;
    private float screenDifferForRotate = 100f;
    private float _transitionSpeedPara;
    private const float VerticalAlignmentAngleThreshold = 15f;
    private const float VerticalAlignmentMinYDiff = 0.03f;
    private const float VerticalAlignmentMaxXDiff = 0.25f;
    
    public bool AutoRotateCamera
    {
        get => PlayerPrefs.GetInt("AutoRotateCamera") == 1;
        set
        {
            PlayerPrefs.SetInt("AutoRotateCamera", value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }
    
    public ChatGptFix(float XZDis, float YDis, float fieldOfView)
    {
        _minXZ = XZDis;
        _minY = YDis;
        this.XZDis = XZDis;
        this.YDis = YDis;
        this.fieldOfView = fieldOfView;
    }

    private float XZDistance
    {
        get => XZDis;
        set => XZDis = Mathf.Clamp(value, _minXZ , _minXZ + 20f);
    }
    
    private float UsedHeight
    {
        get => YDis;
        set => YDis = Mathf.Clamp(value, _minY , _minY + 20f);
    }

    public override void Enter(Camera _camera)
    {
        CanSetH = true;
        _camera.fieldOfView = this.fieldOfView;
        CameraManager._subCamera.fieldOfView = this.fieldOfView;
        CameraManager._centerCamera.fieldOfView = this.fieldOfView;
        LocalUpdate(_camera);
        xzOff = _camera.transform.position - lookPoint;
        xzOff.y = 0;
        _transitionSpeedPara = 0f;
        DOTween.To(()=> _transitionSpeedPara, (x) => _transitionSpeedPara = x, CommonSetting.CameraSpeed, 1f);
    }

    float h;
    float ePosX;
    float ePosY;
    float mPosX;
    float mPosY;

    private bool _canSetH;
    public bool CanSetH
    {
        get => _canSetH;
        set
        {
            _canSetH = value;
            h = 0;
        }
    }

    private Vector3 mePos;
    
    public override void LocalUpdate(Camera camera)
    {
        if (meCenter != null)
        {
            mePos = meCenter.position;
        }

        _changeSpeed = _transitionSpeedPara * Time.deltaTime;
            //Time.deltaTime / (TransitionSpeedPara + Time.deltaTime); //分母里那个附加值越大，变得越慢。
        var hasTargets = targets != null && targets.Count > 0;
        if (hasTargets)
        {
            enemiesCenter = Vector3.zero;
            foreach (var o in targets)
            {
                if (o != null)
                {
                    enemiesCenter += o.transform.position;
                }
            }
            enemiesCenter /= targets.Count;
        }
        else
        {
            enemiesCenter = mePos + meCenter.forward * 10f;
        }
        
        enemyScreenPos = camera.WorldToScreenPoint(enemiesCenter);
        meScreenPos = camera.WorldToScreenPoint(mePos);
        float normalizedXDiff = Mathf.Abs(enemyScreenPos.x - meScreenPos.x) / Screen.width;
        float normalizedYDiff = Mathf.Abs(enemyScreenPos.y - meScreenPos.y) / Screen.height;
        Vector2 screenDiff = enemyScreenPos - meScreenPos;
        float screenDiffSqr = screenDiff.sqrMagnitude;
        float angleToHorizontal = Mathf.Abs(Vector2.SignedAngle(screenDiff, Vector2.right));
        if (angleToHorizontal > 90f)
        {
            angleToHorizontal = 180f - angleToHorizontal;
        }
        float verticalityRatio = Mathf.Sqrt(Mathf.Clamp01(angleToHorizontal / 90f));
        float dynamicAutoRotateSpeed = autoRotateSpeed * verticalityRatio;
        
        if (CanSetH)
        {
            h = UltimateJoystick.GetHorizontalAxis("RotateCamera");
        }
        
        if (h != 0)
        {
            xzOff = Quaternion.AngleAxis(h * 1.5f, Vector3.up) * xzOff;
            xzOff.y = 0;
        }
        else
        {
            if (AutoRotateCamera && hasTargets && meCenter != null && screenDiffSqr > screenDifferForRotate * screenDifferForRotate)
            {
                if (ShouldRotateForVerticalAlignment(normalizedXDiff, normalizedYDiff, angleToHorizontal))
                {
                    Vector3 planarDiff = enemiesCenter - mePos;
                    planarDiff.y = 0f;
                    if (planarDiff.sqrMagnitude > 0.0001f)
                    {
                        Vector3 currentForward = camera.transform.forward;
                        currentForward.y = 0f;
                        if (currentForward.sqrMagnitude < 0.0001f)
                        {
                            currentForward = -(xzOff.sqrMagnitude > 0.0001f ? xzOff : planarDiff.normalized);
                        }
                        currentForward.Normalize();

                        Vector3 desiredForward = Vector3.Cross(Vector3.up, planarDiff).normalized;
                        if (desiredForward.sqrMagnitude < 0.0001f)
                        {
                            desiredForward = currentForward;
                        }
                        else
                        {
                            var alternativeForward = -desiredForward;
                            if (Vector3.Angle(currentForward, alternativeForward) < Vector3.Angle(currentForward, desiredForward))
                            {
                                desiredForward = alternativeForward;
                            }
                        }

                        float angleToTarget = Vector3.SignedAngle(currentForward, desiredForward, Vector3.up);
                        float rotationStep = dynamicAutoRotateSpeed * Time.deltaTime;
                        if (!Mathf.Approximately(rotationStep, 0f))
                        {
                            if (Mathf.Abs(angleToTarget) <= rotationStep)
                            {
                                currentForward = desiredForward;
                            }
                            else
                            {
                                currentForward = Quaternion.AngleAxis(rotationStep * Mathf.Sign(angleToTarget), Vector3.up) * currentForward;
                            }
                        }
                        currentForward.y = 0f;
                        if (currentForward.sqrMagnitude > 0.0001f)
                        {
                            currentForward.Normalize();
                            var newXZ = -currentForward;
                            newXZ.y = 0f;
                            if (newXZ.sqrMagnitude > 0.0001f)
                            {
                                xzOff = newXZ.normalized;
                            }
                        }
                    }
                }
            }
        }
        
        ePosX = enemyScreenPos.x / Screen.width;
        ePosY = enemyScreenPos.y / Screen.height;
        mPosX = meScreenPos.x / Screen.width;
        mPosY = meScreenPos.y / Screen.height;
        
        if (ePosX >= 0.3 && ePosX <= 0.7 &&
            mPosX >= 0.3 && mPosX <= 0.7 &&
            ePosY >= 0.3 && ePosY <= 0.7 &&
            mPosY >= 0.3 && mPosY <= 0.7)
        {
            XZDistance -= _changeSpeed;
            UsedHeight -= _changeSpeed;
        }
        else if (ePosX <= 0.2 || ePosX >= 0.8 || 
                 mPosX <= 0.2 || mPosX >= 0.8 || 
                 ePosY <= 0.2 || ePosY >= 0.8 || 
                 mPosY <= 0.2 || mPosY >= 0.8)
        {
            XZDistance += _changeSpeed;
            UsedHeight += _changeSpeed;
        }
        
        // 判断我与敌人哪个更接近相机位置
        if (enemyScreenPos.y >= meScreenPos.y)
        {
            frontWPos = mePos;
            backWPos = enemiesCenter;
        }
        else
        {
            frontWPos = enemiesCenter;
            backWPos = mePos;
        }

        cameraTargetPos = lookPoint + xzOff.normalized * XZDistance;
        cameraTargetPos.y = UsedHeight;
        
        if (hasTargets || meCenter != null || h != 0)
        {
            camera.transform.position = Vector3.Lerp(camera.transform.position, cameraTargetPos, _changeSpeed);
            
            lookPoint = (backWPos - frontWPos) * 0.5f + frontWPos;
            lookPoint.y = _lookPointHeight;
            
            rotateToDirection = lookPoint - cameraTargetPos;
            ToRotation = Quaternion.LookRotation(rotateToDirection.normalized);
            camera.transform.rotation = Quaternion.Slerp(camera.transform.rotation, ToRotation, _changeSpeed);
        }
    }

    private bool ShouldRotateForVerticalAlignment(float normalizedXDiff, float normalizedYDiff, float angleToHorizontal)
    {
        if (normalizedYDiff < VerticalAlignmentMinYDiff)
        {
            return false;
        }

        if (normalizedXDiff > VerticalAlignmentMaxXDiff)
        {
            return false;
        }

        float threshold = Mathf.Min(autoChangeAngleLimit, VerticalAlignmentAngleThreshold);
        return angleToHorizontal >= threshold;
    }
}
