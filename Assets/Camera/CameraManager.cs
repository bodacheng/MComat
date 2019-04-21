using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Camera_Mode_Num
{
    GodMode = 2,
    GodPlayerMode = 3,
    CertainYAntiVibrationCamera = 12,
    LockCamera = 4,
    WatchOverCamera = 8,
    LerpToCertainPlace = 9,
    GodPlayerCertainYCamera = 10,
    GodWatchCamera = 11,
    keepTargetLeft = 13,
    approachToCertainDistance = 14,
}

public class CameraManager : MonoBehaviour
{
    public static Camera _camera;
    public Camera_Mode_Num current_Camera_Mode_Num;
    public camera_mode current_Camera_Mode;

    [Space(11)]
    [Header("GL Line materail")]
    public Material mat;

    private IDictionary<Camera_Mode_Num, camera_mode> camera_Mode_Dictionary;
    private List<Vector3[]> DrawLineMissions;

    void OnPostRender()
    {
        if (this.DrawLineMissions != null)
        {
            drawlines(this.DrawLineMissions);
        }
    }

    public void ClearDrawingLines()
    {
        this.DrawLineMissions = null;
    }

    public void drawlines(List<Vector3[]> drawLineMissions)//这是一个运行结构特殊的函数 仔细看就明白了 是靠外部激活
    {
        this.DrawLineMissions = drawLineMissions;
        if (mat == null)
            return;

        if (drawLineMissions == null)
            return;

        foreach (Vector3[] _SAF in drawLineMissions)
        {
            if (_SAF.Length != 2)
                continue;
            GL.PushMatrix();
            mat.SetPass(0);
            GL.LoadOrtho();
            GL.Begin(GL.LINES);
            GL.Color(Color.white);
            GL.Vertex(new Vector3(_SAF[0].x / Screen.width, _SAF[0].y / Screen.height, 0));
            GL.Vertex(new Vector3(_SAF[1].x / Screen.width, _SAF[1].y / Screen.height, 0));
            GL.End();
            GL.PopMatrix();
        }
    }

    // Use this for initialization
    private void Awake()
    {
        _camera = GetComponent<Camera>();
        _camera.depthTextureMode = DepthTextureMode.Depth;
        camera_Mode_Dictionary = new Dictionary<Camera_Mode_Num, camera_mode>();

        GodCamera _GodCamera = new GodCamera(null, 10f, 15f, 52f);
        GodplayerCamera _GodplayerCamera = new GodplayerCamera(12f, 5f);
        GodPlayerCertainYCamera GodPlayerCertainYCamera = new GodPlayerCertainYCamera(5f, 5f, 2f);
        CertainYAntiVibrationCamera _CertainYAntiVibrationCamera = new CertainYAntiVibrationCamera(10f, 6f);
        WatchOverCamera _WatchOverCamera = new WatchOverCamera(7f, 5f);
        LerpToCertainPlace _LerpToCertainPlace = new LerpToCertainPlace(Vector3.zero, new Quaternion(), 1f);
        LerpToCertainDistance _LerpToCertainDistance = new LerpToCertainDistance(5f,1f);
        GodWatchCamera _GodWatchCamera = new GodWatchCamera(10f, 10f);
        keepTargetLeftCamera _keepTargetLeftCamera = new keepTargetLeftCamera();
        LockCameraAsTheTransform _LockCameraAsTheTransform = new LockCameraAsTheTransform(null,5f);

        camera_Mode_Dictionary.Add(Camera_Mode_Num.GodPlayerCertainYCamera, GodPlayerCertainYCamera);
        camera_Mode_Dictionary.Add(Camera_Mode_Num.CertainYAntiVibrationCamera, _CertainYAntiVibrationCamera);
        camera_Mode_Dictionary.Add(Camera_Mode_Num.GodMode, _GodCamera);
        camera_Mode_Dictionary.Add(Camera_Mode_Num.GodPlayerMode, _GodplayerCamera);
        camera_Mode_Dictionary.Add(Camera_Mode_Num.LockCamera, _LockCameraAsTheTransform);
        camera_Mode_Dictionary.Add(Camera_Mode_Num.WatchOverCamera, _WatchOverCamera);
        camera_Mode_Dictionary.Add(Camera_Mode_Num.LerpToCertainPlace, _LerpToCertainPlace);
        camera_Mode_Dictionary.Add(Camera_Mode_Num.approachToCertainDistance, _LerpToCertainDistance);
        camera_Mode_Dictionary.Add(Camera_Mode_Num.GodWatchCamera, _GodWatchCamera);
        camera_Mode_Dictionary.Add(Camera_Mode_Num.keepTargetLeft, _keepTargetLeftCamera);

        DrawLineMissions = new List<Vector3[]>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void LateUpdate()
    {
        if (current_Camera_Mode != null)
        {
            current_Camera_Mode.LocalLateUpdate(_camera);
        }
    }

    public void Assign_Camera(Camera_Mode_Num num, List<Transform> targets)
    {
        ClearDrawingLines();
        this.current_Camera_Mode_Num = num;
        this.camera_Mode_Dictionary.TryGetValue(current_Camera_Mode_Num, out current_Camera_Mode);
        this.current_Camera_Mode.targets = targets;
    }

    public void Assign_LerpToCertainPlaceCamera(Vector3 p, Quaternion quaternion, float sp)
    {
        this.current_Camera_Mode_Num = Camera_Mode_Num.LerpToCertainPlace;
        this.camera_Mode_Dictionary.TryGetValue(current_Camera_Mode_Num, out current_Camera_Mode);
        this.current_Camera_Mode.setObjPosAndRotAndSpeed(p, quaternion, sp);
    }

    public void Assign_Camera(Camera_Mode_Num num)
    {
        ClearDrawingLines();
        this.current_Camera_Mode_Num = num;
        this.camera_Mode_Dictionary.TryGetValue(current_Camera_Mode_Num, out current_Camera_Mode);
    }

    public void Assign_Camera(Camera_Mode_Num num, string[] target_names)
    {
        ClearDrawingLines();
        this.current_Camera_Mode_Num = num;
        this.camera_Mode_Dictionary.TryGetValue(current_Camera_Mode_Num, out current_Camera_Mode);
        this.current_Camera_Mode.targets = this.lockTargets(target_names);
    }

    public List<Transform> lockTargets(string[] targets_names)
    {
        List<Transform> targets = new List<Transform>();
        foreach (string a_name in targets_names)
        {
            Transform target = GameObject.Find(a_name).transform;
            targets.Add(target);
        }
        return targets;
    }
}

public abstract class camera_mode
{
    protected Transform meCenter;
    public List<Transform> targets;

    // 整个这一套玩意混乱在，下面这些不同的变量，在不同的相机模式下可能完全是不同的作用。
    // 只是有节约变量数的意义。
    protected Vector3 obj_position;
    protected Quaternion obj_rotation;
    protected float speed;
    protected float XZDis, YDis;
    protected float XZrosOffset, YrosOffset;

    public void setMeCenter(Transform meCenter)
    {
        this.meCenter = meCenter;
    }

    public void setCertainYModeParameters(float XZDis, float YDis, Transform meCenter)
    {
        this.YDis = YDis;
        this.XZDis = XZDis;
        this.meCenter = meCenter;
    }

    public void setObjPosAndRotAndSpeed(Vector3 obj_position, Quaternion obj_rotation, float speed)
    {
        this.obj_position = obj_position;
        this.obj_rotation = obj_rotation;
        this.speed = speed;
    }

    public void setWatchOverModeParas(float XZdis,float Ydis, float XZrosOffset, float YrosOffset, float speed)
    {
        this.XZDis = XZdis;
        this.YDis = Ydis;
        this.XZrosOffset = XZrosOffset;
        this.YrosOffset = YrosOffset;
        this.speed = speed;
    }

    public virtual void LocalLateUpdate(Camera _camera)
    {
    }

    //下面这个函数的意思是说，把这个original往水平，垂直方向分别扭它一个角度，返回新的方向。。。到底可不可靠最近没验证过
    public static Vector3 GetDirection(Vector3 original, float offsetAngle, float chuizhiangle)
    {
        //初始方向是物体正前方
        Vector3 targetDir = Vector3.zero;
        //求得物体绕其自身Y轴旋转指定角度后的四元数值
        Quaternion offsetRot = Quaternion.AngleAxis(offsetAngle, Vector3.up);
        //用求得的四元数值 * 初始方向 即为目标方向  （Mark: 必须是 Quaterion * Vector 不能写成 Vector * Quaterion） 
        Quaternion offsetRot2 = Quaternion.AngleAxis(chuizhiangle, Vector3.left);
        targetDir = offsetRot2 * offsetRot * original;
        return targetDir.normalized;
    }
}

class keepTargetLeftCamera : camera_mode
{
    Vector3 center = new Vector3(0, 0, 0);

    public override void LocalLateUpdate(Camera _camera)
    {
        if (this.targets == null)
        {
            return;
        }
        else
        {
            if (this.targets.Count == 0)
                return;
        }

        center = Vector3.zero;
        foreach (Transform o in this.targets)
        {
            if (o != null)
                center += o.transform.position;
        }
        center /= targets.Count;

        _camera.transform.position = Vector3.Lerp(_camera.transform.position, center + new Vector3(-7f,4f,7f), Time.deltaTime/(0.2f + Time.deltaTime));
        _camera.transform.LookAt(center - Vector3.right * 3 + Vector3.up  * 1f, Vector3.up);
        //_camera.transform.Translate(Vector3.right * -2, _camera.transform);
    }
}

class GodCamera : camera_mode
{
    Transform jizhun;
    float height_min, height_max;
    float y_angle;
    float height;
    //Gesture current;
    Vector3 cameraPosition;
    public float perspectiveZoomSpeed = 0.5f;        // The rate of change of the field of view in perspective mode.
    public float orthoZoomSpeed = 0.5f;        // The rate of change of the orthographic size in orthographic mode

    public GodCamera(Transform jizhun, float height_min, float height_max, float y_angle)
    {
        this.height_max = height_max;
        this.height_min = height_min;
        this.y_angle = y_angle;
        this.jizhun = jizhun;
    }

    public override void LocalLateUpdate(Camera _camera)
    {
        //current = EasyTouch.current;
        //        if (current != null)
        //        {                    
        //			if (current.type == EasyTouch.EvtType.On_Drag)
        //			{              
        //				_camera.transform.Translate(Vector3.left * 30 * current.deltaPosition.x / Screen.width);
        //				float height = _camera.transform.position.y;
        //				_camera.transform.Translate(Vector3.back * 30 * current.deltaPosition.y / Screen.height);
        //				Vector3 cameraPosition = _camera.transform.position;
        //				cameraPosition.y = height;
        //				_camera.transform.position = cameraPosition;
        //			}

        //			if (Input.touchCount == 1)
        //			{
        //				if (Input.GetTouch(0).phase == TouchPhase.Moved)
        //				{
        //					_camera.transform.Translate(Vector3.left * Input.GetTouch(0).deltaPosition.x * 30f / Screen.width);
        //					height = _camera.transform.position.y;
        //					_camera.transform.Translate(Vector3.back * current.deltaPosition.y * 30f / Screen.height);
        //					cameraPosition = _camera.transform.position;
        //					cameraPosition.y = height;
        //					_camera.transform.position = cameraPosition;
        //				}
        //			}

        //			if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        //			{
        //				if (Input.touchCount == 2)
        //				{
        ////					if (current.type == EasyTouch.EvtType.On_Pinch)
        ////					{
        ////						_camera.fieldOfView += current.deltaPinch * Time.deltaTime;
        ////						_camera.fieldOfView = Mathf.Clamp(_camera.fieldOfView, 30f, 90f);
        ////					}

        //		// Store both touches.
        //		Touch touchZero = Input.GetTouch(0);
        //		Touch touchOne = Input.GetTouch(1);

        //		// Find the position in the previous frame of each touch.
        //		Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
        //		Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

        //		// Find the magnitude of the vector (the distance) between the touches in each frame.
        //		float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
        //		float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

        //		// Find the difference in the distances between each frame.
        //		float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

        //		// If the camera is orthographic...
        //		if (_camera.orthographic)
        //		{
        //			// ... change the orthographic size based on the change in distance between the touches.
        //			_camera.orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;

        //			// Make sure the orthographic size never drops below zero.
        //			_camera.orthographicSize = Mathf.Max(_camera.orthographicSize, 0.1f);
        //		}
        //		else
        //		{
        //			// Otherwise change the field of view based on the change in distance between the touches.
        //			_camera.fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;

        //			// Clamp the field of view to make sure it's between 0 and 180.
        //			_camera.fieldOfView = Mathf.Clamp(_camera.fieldOfView, 0.1f, 179.9f);
        //		}

        //		// Twist
        //		if (current.type == EasyTouch.EvtType.On_Twist)
        //		{
        //			_camera.transform.Rotate(new Vector3(0, 1, 0) * current.twistAngle * Time.deltaTime);
        //		}
        //	}
        //}
        //}

        if (jizhun != null)
            _camera.transform.rotation = jizhun.rotation * Quaternion.Euler(y_angle, 0, 0);
        Vector3 _camera_position = _camera.transform.position;
        if (_camera_position.y < height_min || _camera_position.y > height_max)
        {
            height = Mathf.Clamp(_camera_position.y, height_min, height_max);
            _camera_position.y = height;
            _camera.transform.position = _camera_position;
        }
    }
}

class GodPlayerCertainYCamera : camera_mode
{
    Vector3 Xi;
    Vector3 center;
    Quaternion ToRotation;

    public GodPlayerCertainYCamera(float XZDis, float YDis, float speed)
    {
        this.XZDis = XZDis;
        this.YDis = YDis;
        this.speed = speed;
    }

    public override void LocalLateUpdate(Camera _camera)
    {
        if (this.targets == null)
        {
            //Debug.Log ("GOdPlayer观看模式无目标");
            return;
        }
        else
        {
            if (this.targets.Count == 0)
            {
                //Debug.Log ("GOdPlayer观看模式无目标");
                return;
            }
        }

        center = Vector3.zero;
        foreach (Transform o in this.targets)
        {
            if (o != null)
                center += o.transform.position;
        }
        center /= targets.Count;

        Xi = center - Vector3.forward * XZDis;
        Xi.y = YDis;

        //_camera.transform.position = Vector3.Lerp(_camera.transform.position, Xi , Time.deltaTime/(1f + Time.deltaTime));
        //_camera.transform.position = Xi;
        //_camera.transform.LookAt(center, Vector3.up);
        ToRotation = Quaternion.LookRotation(center - Xi);
        _camera.transform.position = 
            Vector3.Lerp(_camera.transform.position, Xi, Time.deltaTime / (0.1f + Time.deltaTime));//上下这两部分都是分母里那个附加值越大，变得越慢。
        //如果你想研究战斗系统中角色本身的发抖问题，可以把上面这个附加值给调节的比较小一些
        _camera.transform.rotation = Quaternion.Slerp(_camera.transform.rotation, ToRotation, Time.deltaTime / (2f + Time.deltaTime));
    }
}

class CertainYAntiVibrationCamera : camera_mode
{
    Vector3 CameraTargetPos;
    Vector3 enemiescenter;
    Vector3 focuscenter;
    Quaternion ToRotation;

    public CertainYAntiVibrationCamera(float XZDis, float YDis)
    {
        this.XZDis = XZDis;
        this.YDis = YDis;
    }

    Vector2 screenpos;
    Vector3 xzOff;
    public override void LocalLateUpdate(Camera _camera)
    {
        if (this.targets == null)
            return;
        if (this.targets.Count > 0)
        {
            enemiescenter = Vector3.zero;
            foreach (Transform o in this.targets)
            {
                if (o != null)
                    enemiescenter += o.transform.position;
                screenpos = _camera.WorldToViewportPoint(o.position);
                if (screenpos.x < 0.2 || screenpos.x > 0.8)
                {
                    XZDis += 0.5f;
                    YDis += 0.5f;
                }
                if ((screenpos.x > 0.4 && screenpos.x < 0.6 && screenpos.y > 0.4 && screenpos.x < 0.6) && XZDis > 10f)
                {
                    XZDis -= 0.4f;
                    YDis -= 0.4f;
                }
            }
            enemiescenter /= targets.Count;
            focuscenter = enemiescenter + (meCenter.position - enemiescenter) * 3 / 4;
        }else{
            focuscenter = meCenter.position;
        }

        if (this.meCenter == null)
            return;
        screenpos = _camera.WorldToViewportPoint(meCenter.position);
        if ((screenpos.x < 0.3 || screenpos.x > 0.7) && YDis < 10)
        {
            XZDis += 1f;
            YDis += 1f;
        }
        if ((screenpos.x > 0.4 && screenpos.x < 0.6 && screenpos.y > 0.4 && screenpos.x < 0.6) && XZDis > 16f)
        {
            XZDis -= 1f;
            YDis -= 1f;
        }

        xzOff = _camera.transform.position - focuscenter;
        xzOff.y = 0;
        CameraTargetPos = focuscenter + xzOff.normalized * XZDis;
        CameraTargetPos.y = YDis;

        //Vector3 targetRotateDirection = enemiescenter - CameraTargetPos;
        //Vector3 targetRotateDirectiony0 = targetRotateDirection; targetRotateDirectiony0.y = 0;
        //if (Mathf.Abs(targetRotateDirection.y / targetRotateDirectiony0.magnitude) > YDis / XZDis)
        //{
        //    return;
        //    targetRotateDirection.y = -targetRotateDirectiony0.magnitude;
        //}

        _camera.transform.position =
                   Vector3.Lerp(_camera.transform.position, CameraTargetPos, Time.deltaTime / (0.2f + Time.deltaTime));//上下这两部分都是分母里那个附加值越大，变得越慢。

        ToRotation = Quaternion.LookRotation(focuscenter - CameraTargetPos);
        _camera.transform.rotation = Quaternion.Slerp(_camera.transform.rotation, ToRotation, Time.deltaTime / (0.2f + Time.deltaTime));
    }
}

class GodplayerCamera : camera_mode
{
    float distance_use = 1f, distance, zoom_range;
    float x, y;
    public float perspectiveZoomSpeed = 0.5f;        // The rate of change of the field of view in perspective mode.
    public float orthoZoomSpeed = 0.5f;        // The rate of change of the orthographic size in orthographic mode
    Vector3 direction = Vector3.forward;

    public GodplayerCamera(float distance, float zoom_range)
    {
        targets = new List<Transform>();
        this.distance = distance;
        this.distance_use = distance;
        this.zoom_range = zoom_range;
    }

    public override void LocalLateUpdate(Camera _camera)
    {
        if (this.targets == null)
        {
            //Debug.Log ("GOdPlayer观看模式无目标");
            return;
        }
        if (this.targets.Count == 0)
        {
            //Debug.Log ("GOdPlayer观看模式无目标");
            return;
        }

        Vector3 center = new Vector3(0, 0, 0);

        foreach (Transform o in this.targets)
        {
            if (o != null)
                center += o.transform.position;
        }
        center /= targets.Count;

        speed = 1f;
        if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor ||
            Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.OSXPlayer)
        {
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                if (distance_use > distance - zoom_range)
                    distance_use -= 0.1f;
            }
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                if (distance_use < distance + zoom_range)
                    distance_use += 0.1f;
            }

            if (Input.GetAxis("Mouse X") != 0f)
            {
                x = Input.GetAxis("Mouse X") * speed * Time.deltaTime;
                direction = GetDirection(direction, x, 0);
            }
            if (Input.GetAxis("Mouse Y") != 0f)
            {
                y = Input.GetAxis("Mouse Y") * speed * 30 * Time.deltaTime;
                direction = GetDirection(direction, 0, -y);
            }
        }
        else if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            if (Input.touchCount == 2)
            {
                // Store both touches.
                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);

                // Find the position in the previous frame of each touch.
                Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                // Find the magnitude of the vector (the distance) between the touches in each frame.
                float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

                // Find the difference in the distances between each frame.
                float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

                // If the camera is orthographic...
                if (_camera.orthographic)
                {
                    // ... change the orthographic size based on the change in distance between the touches.
                    _camera.orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;

                    // Make sure the orthographic size never drops below zero.
                    _camera.orthographicSize = Mathf.Max(_camera.orthographicSize, 0.1f);
                }
                else
                {
                    // Otherwise change the field of view based on the change in distance between the touches.
                    _camera.fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;

                    // Clamp the field of view to make sure it's between 0 and 180.
                    _camera.fieldOfView = Mathf.Clamp(_camera.fieldOfView, 5f, 90.9f);
                }
            }
            if (Input.touchCount == 1)
            {
                //_camera.transform.rotation = Quaternion.Slerp(_camera.transform.rotation,Quaternion.LookRotation(center - _camera.transform.position,Vector3.up),Time.deltaTime);
                if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    direction = GetDirection(direction, Input.GetTouch(0).deltaPosition.x * 60f / Screen.width, Input.GetTouch(0).deltaPosition.y * 50f / Screen.height);
                }
            }
        }

        if (_camera.transform.position != center - direction * distance_use)
        {
            Vector3 to = center - direction * distance_use;
            _camera.transform.position = Vector3.Lerp(_camera.transform.position, to, Vector3.Distance(_camera.transform.position, to) * speed * Time.deltaTime);
            _camera.transform.LookAt(center, Vector3.up);
        }
    }
}

class TeamEditCamera : camera_mode
{
    public float distance, height;
    Vector3 direction = Vector3.zero;

    public TeamEditCamera(float distance, float height)
    {
        targets = new List<Transform>();
        this.distance = distance;
        this.height = height;
    }

    public override void LocalLateUpdate(Camera _camera)
    {
        if (this.targets == null)
        {
            //Debug.Log ("观看模式无目标");
            return;
        }
        if (this.targets.Count == 0)
        {
            //Debug.Log ("观看模式无目标");
            return;
        }

        Vector3 center = new Vector3(0, 0, 0);
        //Quaternion average = new Quaternion(0, 0, 0, 0);
        foreach (Transform o in this.targets)
        {
            //amount++;
            if (o != null)
            {
                center += o.transform.position;
                direction += o.transform.forward;
            }
            //average = Quaternion.Slerp(average, o.rotation, 1 / amount); //得到所有对象的平均旋转值。这个我们并没有详细认证过到底对不对。其实如果我们真动用这个模式的话肯定不会指定多个聚焦对象
        }
        center /= targets.Count;
        float speed = 1f;
        if (_camera.transform.position != center - direction * distance + new Vector3(0, height, 0))
        {
            Vector3 to = center - direction.normalized * distance + new Vector3(0, height, 0);
            _camera.transform.position = Vector3.Lerp(_camera.transform.position, to, speed * Time.deltaTime);
        }
        Vector3 directionLook = center - _camera.transform.position;
        Quaternion toRotation = Quaternion.FromToRotation(_camera.transform.forward, directionLook);
        _camera.transform.rotation = Quaternion.Lerp(_camera.transform.rotation, toRotation, speed * Time.deltaTime);
    }
}

class WatchOverCamera : camera_mode
{
    private Vector3 direction = Vector3.zero;
    private Quaternion ToRotation;

    public WatchOverCamera(float distance, float height)
    {
        targets = new List<Transform>();
        this.XZDis = distance;
        this.YDis = height;
    }

    public override void LocalLateUpdate(Camera _camera)
    {
        if (this.targets == null || this.targets.Count == 0)
            return;

        Vector3 center = Vector3.zero;
        //Quaternion average = new Quaternion(0, 0, 0, 0);
        direction = Vector3.zero;
        foreach (Transform o in this.targets)
        {
            //amount++;
            if (o != null)
            {
                center += o.transform.position;
                direction += o.transform.forward;
            }
            //average = Quaternion.Slerp(average, o.rotation, 1 / amount); //得到所有对象的平均旋转值。这个我们并没有详细认证过到底对不对。其实如果我们真动用这个模式的话肯定不会指定多个聚焦对象
        }
        center /= targets.Count;
        direction =  Quaternion.AngleAxis(XZrosOffset, Vector3.up) * direction;
        Vector3 to = center + direction.normalized * XZDis + new Vector3(0, YDis, 0);   
        _camera.transform.position = Vector3.Lerp(_camera.transform.position, to, speed * Time.deltaTime);
        ToRotation = Quaternion.LookRotation(center - to);
        _camera.transform.rotation = Quaternion.Slerp(_camera.transform.rotation, ToRotation, Time.deltaTime / (0.2f + Time.deltaTime));
    }
}

public class LockCameraAsTheTransform : camera_mode
{
    public Transform _AS;

    public LockCameraAsTheTransform(Transform As, float speed)
    {
        this._AS = As;
        this.speed = speed;
    }

    public override void LocalLateUpdate(Camera _camera)
    {
        if (targets != null && targets.Count > 0)
        {
            if (_AS != targets[0])
                _AS = targets[0];
        }
        if (_AS != null)
        {
            _camera.transform.position = Vector3.Lerp(_camera.transform.position, _AS.position, speed * Time.deltaTime);
            _camera.transform.rotation = Quaternion.Lerp(_camera.transform.rotation, _AS.rotation, speed * Time.deltaTime);
        }
    }
}

public class LerpToCertainPlace : camera_mode
{
    public LerpToCertainPlace(Vector3 obj_position, Quaternion obj_rotation, float speed)
    {
        this.obj_position = obj_position;
        this.obj_rotation = obj_rotation;
        this.speed = speed;
    }

    public override void LocalLateUpdate(Camera _camera)
    {
        _camera.transform.position = Vector3.Lerp(_camera.transform.position, obj_position, speed * Time.deltaTime);
        _camera.transform.rotation = Quaternion.Lerp(_camera.transform.rotation, obj_rotation, speed * Time.deltaTime);
    }
}

public class LerpToCertainDistance : camera_mode
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

// 1.固定高度
// 2. 固定45度角俯视地面
// 3. 可控制前后移动，可控制水平旋转
// 思路是，控制相机位置，并且有一个由右遥杆决定的方向量是这个instance里的某固定量，代表相对相机看向的一个相对坐标值
class GodWatchCamera : camera_mode
{
    public float distance, height;

    #region Inspector Variables

    [Header("-- Camera Controller Properties --")]
    [Tooltip("How fast camera follows player")]
    public float followSpeed = 9;
    [Tooltip("How fast camera moves with mouse")]
    public float mouseSpeed = 2;
    [Tooltip("How fast camera moves with controller")]
    public float controllerSpeed = 7;
    [Tooltip("Minimum Rotation along X")]
    public float minRotation = -35f;
    [Tooltip("Maximum Rotation along X")]
    public float maxRotation = 35f;

    #endregion

    //smoothly rotating camera around player
    float turnSmoothing = 0.1f;
    //smoothness along X
    float smoothX= 0.1f;
    //smoothness along Y
    float smoothY= 0.1f;
    //smoothness velocity along X
    float smoothXVelocity= 0.1f;
    //smoothness velocity along Y
    float smoothYVelocity= 0.1f;

    [HideInInspector]
    //Child transform which is parent of main camera
    public Transform pivot;
    [HideInInspector]
    //the camera transform
    public Transform camTrans;
    [HideInInspector]
    //The look rotation of camera to player
    public float lookRotation;
    [HideInInspector]
    //The tilt rotation of camera to player
    public float tiltRotation;

    public GodWatchCamera(float distance, float height)
    {
        targets = new List<Transform>();
        this.distance = distance;
        this.height = height;
    }

    Vector3 pos;
    public override void LocalLateUpdate(Camera _camera)
    {
        pos = new Vector3(_camera.transform.position.x, height, _camera.transform.position.z);

        Quaternion screenMovementSpace;
        Vector3 screenMovementForward, screenMovementRight;
        Vector3 use_direction = Vector3.zero;

        float k = 0f;
        if (_camera != null)
        {
            //get movement axis relative to camera
            screenMovementSpace = Quaternion.Euler(0, _camera.transform.eulerAngles.y, 0);
            screenMovementForward = screenMovementSpace * Vector3.forward;
            screenMovementRight = screenMovementSpace * Vector3.right;
            //get movement input, set direction to move in
            float h = 0f;
            float v = 0f;
            if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.OSXPlayer)
            {
                h = Input.GetAxis("Horizontal");
                v = Input.GetAxis("Vertical");

            }
            else if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
            {
                h = ETCInput.GetAxis("Horizontal");
                v = ETCInput.GetAxis("Vertical");
            }
            use_direction = (screenMovementForward * v) + (screenMovementRight * h);


            if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.WindowsEditor 
                || Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.OSXPlayer)
            {
                k += Input.GetAxis("HorizontalCR") * speed * Time.deltaTime / (Time.deltaTime + 0.2f);
            }
            else if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
            {
                k += ETCInput.GetAxis("HorizontalCR") * speed * Time.deltaTime / (Time.deltaTime + 0.2f);
            }



        }


        Vector3 to = pos + use_direction.normalized * speed;
        if (_camera.transform.position != to)
        {
            _camera.transform.position = Vector3.Lerp(_camera.transform.position, to, speed * Time.deltaTime / (Time.deltaTime + 0.2f));
        }

        //directionLook = Quaternion.AngleAxis(45, Vector3.up) * directionLook;
        //getting axis from Mouse
        float horizontal = Input.GetAxis("Mouse X");
        float vertical = Input.GetAxis("Mouse Y");
        RotateCamera(Time.deltaTime, vertical, horizontal, 2f, _camera);
    }

    //rotating AROUND player
    void RotateCamera(float d, float vert, float horz, float camTargetSpeed, Camera _camera)
    {
        //if we have any smoothing
        if (turnSmoothing > 0)
        {

            //rotate smoothly
            smoothX = Mathf.SmoothDamp(smoothX, horz, ref smoothXVelocity, turnSmoothing);
            smoothY = Mathf.SmoothDamp(smoothY, vert, ref smoothYVelocity, turnSmoothing);

        }
        else
        {
            //else just apply raw rotation
            smoothX = horz;
            smoothY = vert;

        }

        //now changing the tilt rotation
        tiltRotation -= smoothY * camTargetSpeed;
        //clamping it with our minRotation and maxRotation
        tiltRotation = Mathf.Clamp(tiltRotation, minRotation, maxRotation);
        //setting our pivot's LOCAL rotation NOT THE WORLDSPACE 
        //_camera.transform.rotation = Quaternion.Euler(tiltRotation, 0, 0);

        //incrementing the look rotation with smoothness and camTargetSpeed
        lookRotation += smoothX * camTargetSpeed;

        //Finally applying it to our camera controller rotation
        _camera.transform.rotation = Quaternion.Euler(tiltRotation, lookRotation, 0);

    }
}