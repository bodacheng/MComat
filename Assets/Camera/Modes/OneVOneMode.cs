using UnityEngine;

class OneVOneMode : CameraMode
{
    Vector3 CameraTargetPos;
    Vector3 enemiesCenter;//敌人的位置中心
    Quaternion ToRotation;//目标相机旋角
    Vector3 rotateToDirection;// 这个值在整个计算里用于两次内容完全不同的计算。一个是计算xzOff的中间变量，一个是用于计算相机看向角度
    Vector2 mescreenpos;
    Vector2 enemyscreenpos;
    Vector3 xzOff = -Vector3.forward;//相机从focuscenter出发的角度，最大的难点。

    float startAutoRotateRange = 9; 
    // 如果敌我屏幕坐标的x值差在此之内，则自动调整角度让两者处于画面左右两侧。
    // 超过此值，则画面转向敌人
    // 过快的摆动xzOff会造成不适。因而靠autoRotateXZOffRangeMaxSpeed限制摇摆xzOff的上限
    float autoRotateXZOffRangeMaxSpeed = 1f;

    readonly float xzMax = 18f;// 相机距离焦点的xz方向最远距离
    float lookdownDegree = 0.5f; //相机向下方看的角度，以横向为单位1
    float zoomAcc;
    float ZoomAcc // 相机调整焦距的加速度。以恒定速度拉远或拉近相机会造成抖动
    {
        get { return zoomAcc; }
        set
        {
            zoomAcc = Mathf.Clamp(value, -1f, 1f);// 上下限两个值过大会导致自动zoom不灵敏
        }
    }

    float zoomcounter;
    float zoomChangeInter = 3f;// zoom in or out 切换所必须达到的时间间隔

    float heightOfXZRate = 0.7f;//高度恒定为XZ_distance的百分之多少
    float xzd;
    float XZ_distance
    {
        get { return xzd; }
        set
        {
            xzd = Mathf.Clamp(value, 8.5f, xzMax);
            YDis = this.xzd * heightOfXZRate; // 相机恒定高度。但如果有角色位置高出次值，则相机高度会超出此位置
        }
    }

    public OneVOneMode(float XZDis)
    {
        this.XZ_distance = XZDis;
        YDis = this.XZ_distance * heightOfXZRate;
    }

    bool justEnterdThisMode = true;
    public override void Enter(Camera _camera)
    {
        if (meCenter == null)
            return;
        if (justEnterdThisMode)
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
            }
            enemyscreenpos = _camera.WorldToViewportPoint(enemiesCenter);
            mescreenpos = _camera.WorldToViewportPoint(meCenter.position);
            temp = Mathf.Abs(mescreenpos.x - enemyscreenpos.x);
            temp = Mathf.Sqrt(temp);

            rotateToDirection = mescreenpos.x > enemyscreenpos.x ? GetVerticalDir(meCenter.position - enemiesCenter) : GetVerticalDir(enemiesCenter - meCenter.position);
            rotateToDirection = rotateToDirection * (1 - temp);
            rotateToDirection += (meCenter.position - enemiesCenter).normalized * temp;
            xzOff = rotateToDirection;

            justEnterdThisMode = false;
        }
        this.LocalUpdate(_camera);
    }

    public override void Exit(Camera _camera)
    {
        justEnterdThisMode = true;
    }

    float h, maxheight;
    float temp;
    bool zoomDirection = false;// false :拉近 true：拉远
    public override void LocalUpdate(Camera _camera)
    {
        if (meCenter == null)
            return;

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

            zoomcounter += Time.deltaTime;

            void ZoomOut()
            {
                if (!zoomDirection && zoomcounter > zoomChangeInter)
                {
                    zoomDirection = true;
                    ZoomAcc = 0;
                    zoomcounter = 0;
                }
                ZoomAcc += Time.deltaTime;
            }

            void ZoomIn()
            {
                if (zoomDirection && zoomcounter > zoomChangeInter)
                {
                    zoomDirection = false;
                    ZoomAcc = 0;
                    zoomcounter = 0;
                }
                // zoomIn的速度比zoomOut慢一些
                ZoomAcc -= Time.deltaTime;
            }

            if (mescreenpos.y < 0.2f)
            {
                ZoomIn();
            }
            else
            {
                if (enemyscreenpos.x < 0.1 || enemyscreenpos.x > 0.9 || enemyscreenpos.y < 0.2 || mescreenpos.x < 0.1 || mescreenpos.x > 0.9)
                {
                    ZoomOut();
                }
                else
                {
                    ZoomIn();
                }
            }

            XZ_distance += ZoomAcc;
        }

        if (auto)
        {
            if (targets != null && targets.Count > 0)
            {
                temp = Vector3.Distance(meCenter.position, enemiesCenter);
                if (temp < startAutoRotateRange)
                {
                    rotateToDirection = mescreenpos.x > enemyscreenpos.x ? GetVerticalDir(meCenter.position - enemiesCenter) : GetVerticalDir(enemiesCenter - meCenter.position);
                    // Mathf.Pow(mescreenpos.x - enemyscreenpos.x, 2f) 如果二者横向非常近，那么至今距离内会产生不可调整的剧烈抖动
                    // 4f 是因为Mathf.Pow2使得0开始的相距范围内值过于低，而增大一些。 
                    speed = Mathf.Clamp(Mathf.Pow(mescreenpos.x - enemyscreenpos.x, 2f), 0, autoRotateXZOffRangeMaxSpeed); //mescreenpos.x - enemyscreenpos.x;
                    xzOff = Vector3.RotateTowards(xzOff, rotateToDirection, speed * Time.deltaTime / (0.2f + Time.deltaTime), 0.0f);
                }
            }
        }

        h = Input.GetAxis("Horizontal") + UltimateJoystick.GetHorizontalAxis("RotateCamera");
        xzOff = Quaternion.AngleAxis(h * 1.5f, Vector3.up) * xzOff;
        maxheight = Mathf.Max(meCenter.position.y, enemiesCenter.y);

        // 相机所在位置计算
        CameraTargetPos = meCenter.position + xzOff.normalized * XZ_distance;
        CameraTargetPos.y = Mathf.Max(maxheight, YDis);
        _camera.transform.position = Vector3.Lerp(_camera.transform.position, CameraTargetPos, Time.deltaTime / (0.1f + Time.deltaTime));//分母里那个附加值越大，变得越慢。

        // 计算相机“看向的位置”
        rotateToDirection = meCenter.position - _camera.transform.position;
        rotateToDirection.y = 0;
        rotateToDirection = rotateToDirection.normalized;
        rotateToDirection.y = -lookdownDegree * 1;
        ToRotation = Quaternion.LookRotation(rotateToDirection.normalized);
        _camera.transform.rotation = Quaternion.Slerp(_camera.transform.rotation, ToRotation, Time.deltaTime / (0.1f + Time.deltaTime));
    }
}