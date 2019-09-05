using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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