using UnityEngine;

class CertainYAntiVabration : CameraMode
{
    Vector3 CameraTargetPos;
    Vector3 enemiescenter;//敌人的位置中心
    Quaternion ToRotation;//目标相机旋角
    Vector3 rotateToDirection;
    
    Vector2 mescreenpos;
    Vector2 enemyscreenpos;
    Vector3 xzOff = Vector3.forward;//相机从focuscenter出发的角度，最大的难点。
    float h;
   
    public CertainYAntiVabration(float XZDis, float YDis)
    {
        this.XZDis = XZDis;
        this.YDis = YDis;
    }
    
    /// <summary>
    /// 获取某向量的垂直向量（方向是左手边?）
    /// </summary>
    Vector3 GetVerticalDir(Vector3 _dir)
    {
        //（_dir.x,_dir.z）与（？，1）垂直，则_dir.x * ？ + _dir.z * 1 = 0
        return Mathf.Approximately(_dir.z, 0) ? new Vector3(0, 0, -1) : new Vector3(-_dir.z / _dir.x, 0, 1).normalized;
    }
    
    public override void LocalUpdate(Camera _camera)
    {
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
        
        if (auto)
        {
            if (targets != null && targets.Count > 0)
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
                enemiescenter.y = 0;
                enemyscreenpos = _camera.WorldToViewportPoint(enemiescenter);
                mescreenpos = _camera.WorldToViewportPoint(meCenter.position);
                if (enemyscreenpos.x < 0.08 || enemyscreenpos.x > 0.92)
                {
                    xzOff = Vector3.RotateTowards(xzOff, meCenter.position - enemiescenter, 3 * Time.deltaTime, 0.0f);
                }else{
                    // special version
                    //Vector3 midpoint = (enemiescenter + meCenter.position) / 2;
                    //float dis1 = Vector3.Distance(_camera.transform.position, midpoint + GetVerticalDir(meCenter.position - enemiescenter) * 5f);    // 试验点1
                    //float dis2 = Vector3.Distance(_camera.transform.position, midpoint + GetVerticalDir(enemiescenter - meCenter.position) * 5f);    // 试验点1
                    //xzOff = dis1 < dis2 ? 
                    //Vector3.RotateTowards(xzOff, GetVerticalDir(meCenter.position - enemiescenter), Time.deltaTime, 0.0f)
                    //:
                    //Vector3.RotateTowards(xzOff, GetVerticalDir(enemiescenter - meCenter.position), Time.deltaTime, 0.0f);
                    
                    // old version
                    if (Vector3.Distance(enemiescenter, meCenter.position) < 10f)
                    {
                        xzOff = Vector3.RotateTowards(xzOff, GetVerticalDir(meCenter.position - enemiescenter), Time.deltaTime, 0.0f);
                    }
                }
            }
        }
        
        //下面的那个(meCenter.position + enemiescenter)，其实是说从0，0，0到他们
        CameraTargetPos = meCenter.position + xzOff.normalized * XZDis;//focuscenter + xzOff.normalized * XZDis;
        CameraTargetPos += Vector3.up * YDis;
        float fixy = Mathf.Clamp(CameraTargetPos.y, 3f,CameraTargetPos.y);
        CameraTargetPos.y = fixy;
        //CameraTargetPos.y = Mathf.Clamp(YDis - angele / 180 * 10f, 6f,7f);//夹角越大，相机越低。夹角小说明两个角色在画面里一上一下，更俯视一些会看的更方便。
        _camera.transform.position = Vector3.Lerp(_camera.transform.position, CameraTargetPos, Time.deltaTime / (0.2f + Time.deltaTime));//上下这两部分都是分母里那个附加值越大，变得越慢。

        rotateToDirection = (meCenter.position + Vector3.up * 2f) - CameraTargetPos;
        ToRotation = Quaternion.LookRotation(rotateToDirection.normalized);
        _camera.transform.rotation = Quaternion.Slerp(_camera.transform.rotation, ToRotation, Time.deltaTime / (0.3f + Time.deltaTime));
    }
}