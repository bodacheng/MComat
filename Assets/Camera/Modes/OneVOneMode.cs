using UnityEngine;

class OneVOneMode : CameraMode
{
    Vector3 CameraTargetPos;
    Vector3 enemiesCenter;//敌人的位置中心
    Quaternion ToRotation;//目标相机旋角
    Vector3 rotateToDirection;
    Vector3 LooKToPoint;
    Vector2 mescreenpos;
    Vector2 enemyscreenpos;
    Vector3 xzOff = Vector3.forward;//相机从focuscenter出发的角度，最大的难点。
    float yaoHeight = 0.95f;
    public float xzMax = 100f;

    float xzd;
    float XZ_distance
    {
        get { return xzd; }
        set
        {
            xzd = Mathf.Clamp(value, 8.5f, xzMax);
            YDis = xzd * 0.5f;
        }
    }

    public OneVOneMode(float XZDis)
    {
        this.XZ_distance = XZDis;
    }

    float h;
    public override void LocalUpdate(Camera _camera)
    {
        h = Input.GetAxis("Horizontal") + UltimateJoystick.GetHorizontalAxis("RotateCamera");
        xzOff = Quaternion.AngleAxis(h * 1.5f, Vector3.up) * xzOff;
        if (auto)
        {
            if (targets != null && targets.Count > 0)
            {
                enemiesCenter = Vector3.zero;
                foreach (Transform o in targets)
                {
                    if (o != null)
                    {
                        enemiesCenter += o.transform.position;
                    }
                }
                enemiesCenter /= targets.Count;

                enemyscreenpos = _camera.WorldToViewportPoint(enemiesCenter);
                mescreenpos = _camera.WorldToViewportPoint(meCenter.position);

                if (enemyscreenpos.x < 0.08 || enemyscreenpos.x > 0.92 || enemyscreenpos.y < 0.1 ||
                    mescreenpos.x < 0.08 || mescreenpos.x > 0.92 || mescreenpos.y < 0.1)
                {
                    XZ_distance += 5f * Time.fixedDeltaTime;
                }
                else
                {
                    XZ_distance -= 5f * Time.fixedDeltaTime;
                }
                xzOff = Vector3.RotateTowards(xzOff, GetVerticalDir(meCenter.position - enemiesCenter), Time.deltaTime, 0.0f);
            }
        }

        // 相机所在位置计算
        CameraTargetPos = (meCenter.position + enemiesCenter) / 2 + xzOff.normalized * XZ_distance;
        CameraTargetPos.y += YDis + yaoHeight;
        h = Mathf.Clamp(CameraTargetPos.y, YDis + yaoHeight, YDis + yaoHeight + 2f);
        CameraTargetPos.y = h;
        _camera.transform.position = Vector3.Lerp(_camera.transform.position, CameraTargetPos, Time.deltaTime / (0.2f + Time.deltaTime));//分母里那个附加值越大，变得越慢。

        // 计算相机“看向的位置”
        LooKToPoint = (meCenter.position + enemiesCenter) / 2;
        LooKToPoint.y =(meCenter.position.y > enemiesCenter.y ? meCenter.position.y : enemiesCenter.y)+ yaoHeight;

        rotateToDirection = LooKToPoint - CameraTargetPos;
        ToRotation = Quaternion.LookRotation(rotateToDirection.normalized);
        _camera.transform.rotation = Quaternion.Slerp(_camera.transform.rotation, ToRotation, (Time.deltaTime) / (0.2f + Time.deltaTime));
    }
}