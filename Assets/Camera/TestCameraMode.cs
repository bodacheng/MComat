using UnityEngine;

class TestCameraMode : CameraMode
{
    Vector3 CameraTargetPos;
    Vector3 enemiescenter;//敌人的位置中心
    Quaternion ToRotation;//目标相机旋角
    Vector3 rotateToDirection;
    Vector2 enemyscreenpos;
    Vector3 xzOff = Vector3.forward;//相机从focuscenter出发的角度，最大的难点。
    float h;
    bool auto = true;
    
    public TestCameraMode(float XZDis, float YDis)
    {
        this.XZDis = XZDis;
        this.YDis = YDis;
    }
    
    public override void LocalUpdate(Camera _camera)
    {
        if (targets.Count > 0)
        {
            enemiescenter = Vector3.zero;
            foreach (Transform o in targets)
            {
                if (o != null)
                {
                    enemiescenter += o.transform.position;
                }
            }
            enemiescenter /= targets.Count;
        }
        enemiescenter.y = 0;
        
        if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.OSXPlayer)
        {
            h =  ETCInput.GetAxis("HorizontalCR");
            xzOff = Quaternion.AngleAxis(h * 1.5f, Vector3.up) * xzOff;
        }
        else if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            h =  ETCInput.GetAxis("HorizontalCR");
            xzOff = Quaternion.AngleAxis(h * 1.5f, Vector3.up) * xzOff;
        }
        xzOff.y = 0;
        enemyscreenpos = _camera.WorldToViewportPoint(enemiescenter);
        
        if (auto)
        {
            if (enemyscreenpos.x < 0.2 || enemyscreenpos.x > 0.8 ||  enemyscreenpos.y < 0.2)
            {
                xzOff = Vector3.Lerp(xzOff, (meCenter.position - enemiescenter), Time.deltaTime);
            }
        }
        
        //下面的那个(meCenter.position + enemiescenter)，其实是说从0，0，0到他们
        CameraTargetPos = meCenter.position + xzOff.normalized * XZDis;//focuscenter + xzOff.normalized * XZDis;
        CameraTargetPos += Vector3.up * YDis;
        //CameraTargetPos.y = Mathf.Clamp(YDis - angele / 180 * 10f, 6f,7f);//夹角越大，相机越低。夹角小说明两个角色在画面里一上一下，更俯视一些会看的更方便。
        _camera.transform.position = Vector3.Lerp(_camera.transform.position, CameraTargetPos, Time.deltaTime / (0.2f + Time.deltaTime));//上下这两部分都是分母里那个附加值越大，变得越慢。

        rotateToDirection = (meCenter.position + Vector3.up * 2f) - CameraTargetPos;
        ToRotation = Quaternion.LookRotation(rotateToDirection.normalized);
        _camera.transform.rotation = Quaternion.Slerp(_camera.transform.rotation, ToRotation, Time.deltaTime / (0.2f + Time.deltaTime));
    }
}