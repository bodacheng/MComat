using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

[AddComponentMenu("Camera/3RDPerson Camera")]
public class CameraCtr : MonoBehaviour
{
    //private static CameraCtr Camera_Ctr;

    //public static CameraCtr instance
    //{
    //    get
    //    {
    //        if (!Camera_Ctr)
    //        {
    //            Camera_Ctr = FindObjectOfType(typeof(CameraCtr)) as CameraCtr;

    //            if (!Camera_Ctr)
    //            {
    //                Debug.LogError("There needs to be one active cameraControl script on a GameObject in your scene.");
    //            }
    //            else
    //            {
    //                Camera_Ctr.Init();
    //            }
    //        }
    //        return Camera_Ctr;
    //    }
    //}

    //void Init()
    //{
    //}
    // 整个上面这些东西就是说这是个单例模式，没有什么别的目的


    public float distance = 15;
    public float zoom_range = 0;
    public float height = 5;

	public float extra_height;

    public float direction_pingmian, direction_chuizhi;

    public float heightDamping = 3;

    GameObject[] targets;
    public string[] targetNames = null;
    public int Camera_mode = 0;

    float x, y;

    string direction_parameter;

    Transform[] t_targets;

    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("a"))
        {
            direction_pingmian += 1.0f;
            if (direction_pingmian >= 360f)
                direction_pingmian = 0f;
        }
        if (Input.GetKey("d"))
        {
            direction_pingmian -= 1.0f;
            if (direction_pingmian < 0f)
                direction_pingmian = 359f;
        }

		switch (Camera_mode) {
            case 8: CameraMode8(targetNames, distance, height, direction_pingmian, direction_chuizhi); //可定角度，高度，距离，相机移动平滑
                    break;
			case 9: CameraMode9(targetNames, distance, height, direction_pingmian, direction_chuizhi);//可定角度，高度，距离，相机移动不平滑。和模式8只一行代码不同
                    break;
			case 1: CameraMode1(distance, zoom_range , extra_height); //鼠标水平摇晃。可定相机高度，和相机上下朝向
                    break;
			case 3: CameraMode3(targetNames); //将物体的朝向和位置完全复制到相机
					break;
			case 2: CameraMode2(targetNames,height); ////正面固定视角，最没用的一个模式。里面用到了height     
					break;
			case 6: CameraMode6(targetNames,direction_parameter,distance,height);//观察对象的正面，正后，和正侧面.除了四个正方向外，只能调整远近和高低.相机位置移动平滑
					break;
			case 5: CameraMode5(targetNames,direction_parameter,distance,height);//观察对象的正面，正后，和正侧面.除了四个正方向外，只能调整远近和高低.相机位置移动瞬间
					break;
            case 33:CameraMode33(t_targets);//观察对象的正面，正后，和正侧面.除了四个正方向外，只能调整远近和高低.相机位置移动瞬间
                    break;
			case 7: CameraMode7 (targetNames);
					break;
            case 0:
					break;

        } 
    }

    public void findTartgetsByTags(string[] cameraTargetNames)
    {
        targetNames = cameraTargetNames;
        List<GameObject> Objects = new List<GameObject>();
        foreach (string name in targetNames)
        {
            GameObject target = GameObject.Find(name);
            Objects.Add(target);
        }
        targets = Objects.ToArray();
    }

	public void stop(){
		this.Camera_mode = 0;
	}

    public void Assignment(int mode,string[] cameraTargetNames, float distance, float height, float direction_pingmian, float direction_chuizhi) //模式8，9专用
    {
		this.Camera_mode = mode;
        this.targetNames = cameraTargetNames;
        this.distance = distance;
        this.height = height;
        this.direction_pingmian = direction_pingmian;
        this.direction_chuizhi = direction_chuizhi;
    }
		
	public void Assignment_mode1(string[] cameraTargetNames, float distance ,float zoom_range, float extra_height)  // 相机模式1专用assignment
	{
		this.Camera_mode = 1;
		this.targetNames = cameraTargetNames;

        findTartgetsByTags(targetNames);

        this.distance = distance;
        this.distance_use = distance;
        this.zoom_range = zoom_range;
		this.extra_height = extra_height;
	}

	public void Assignment(int mode, string[] cameraTargetNames, string direction, float distance, float height)  // 相机模式6和模式5专用assignment
	{
		this.Camera_mode = mode;
		this.targetNames = cameraTargetNames;
		this.direction_parameter = direction;
		this.distance = distance;
		this.height = height;
	}

	public void Assignment(int mode, string[] cameraTargetNames) // 模式三可用
	{
		this.Camera_mode = mode;
		this.targetNames = cameraTargetNames;
	}

    public void Assignment(int mode, Transform[] cameraTargets)
    {
        this.Camera_mode = mode;
        this.t_targets = cameraTargets;
    }

    float distance_use;
    public void CameraMode1(float distance,float zoom_range ,float extra_height) // 随鼠标轻微旋转
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

        Vector3 center = new Vector3(0, 0, 0);
        foreach (GameObject o in targets) {
          center += o.transform.position;
        }
        center /= targets.Length;

        float speed = 33f;

        x += Input.GetAxis("Mouse X") * speed * Time.deltaTime;
        y += Input.GetAxis("Mouse Y") * speed * Time.deltaTime;

        y = Mathf.Clamp(y,-60.0f,-0f);

        Quaternion q = Quaternion.Euler(-y, x, 0);
		Vector3 direction = q * Vector3.forward * distance_use;

        if (transform.position != center - direction * distance_use)
        {
            transform.position = Vector3.MoveTowards(transform.position, center - direction * distance_use, speed * Time.deltaTime);
        }

		center.y += extra_height;
        transform.LookAt(center);
    }

	public void CameraMode2(string[] cameraTargetNames,float camera_height) //正面固定视角，最没用的一个模式。里面用到了height                                    
    {
        targetNames = cameraTargetNames;
        List<GameObject> Objects = new List<GameObject>();
        foreach (string name in targetNames)
        {
            GameObject target = GameObject.Find(name);
            Objects.Add(target);
        }
        targets = Objects.ToArray();

        Vector3 center = new Vector3(0, 0, 0);
        foreach (GameObject o in targets)
        {
            center += o.transform.position;
        }
        center /= targets.Length;


		float wantedHeight = center.y + camera_height;
        float currentHeight = transform.position.y;

        // Damp the height
        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

        Vector3 pos = center;
        pos -= Vector3.forward * distance;
        pos.y = currentHeight;
        transform.position = pos;

        // Always look at the target
        transform.LookAt(center);
    }

    public void CameraMode3(string[] cameraTargetNames) // 固定视角 这个模式恐怕我们不会怎么用但意义很大，这个是直接让相机的位置和朝向与某个物体一致。
    {
        targetNames = cameraTargetNames;
        List<GameObject> Objects = new List<GameObject>();
        foreach (string name in targetNames)
        {
            GameObject target = GameObject.Find(name);
            Objects.Add(target);
        }
        targets = Objects.ToArray();

        Vector3 center = new Vector3(0, 0, 0);
        foreach (GameObject o in targets)
        {
            center += o.transform.position;
        }
        center /= targets.Length;// 得到所有对象的平均位置

        Quaternion average = new Quaternion(0, 0, 0, 0);
        var amount = 0;
        foreach (var target in targets)
        {
            amount++;
            average = Quaternion.Slerp(average, target.transform.rotation, 1 / amount);
        }

        transform.position = center;
        transform.rotation = average;
    }

    public void CameraMode33(Transform[] cameraTargets) // 模式三的直接靠transform做参数版本
    {
        Vector3 center = new Vector3(0, 0, 0);
        foreach (Transform o in cameraTargets)
        {
            center += o.position;
        }
        center /= cameraTargets.Length;// 得到所有对象的平均位置

        Quaternion average = new Quaternion(0, 0, 0, 0);
        var amount = 0;
        foreach (var target in cameraTargets)
        {
            amount++;
            average = Quaternion.Slerp(average, target.rotation, 1 / amount);
        }

        transform.position = center;
        transform.rotation = average;
    }

    public void CameraMode4(string[] cameraTargetNames) // 模式3的位置滑行版本
    {
        targetNames = cameraTargetNames;
        List<GameObject> Objects = new List<GameObject>();
        foreach (string name in targetNames)
        {
            GameObject target = GameObject.Find(name);
            Objects.Add(target);
        }
        targets = Objects.ToArray();

        Vector3 center = new Vector3(0, 0, 0);
        foreach (GameObject o in targets)
        {
            center += o.transform.position;
        }
        center /= targets.Length;// 得到所有对象的平均位置

        Quaternion average = new Quaternion(0, 0, 0, 0);
        var amount = 0;
        foreach (var target in targets)
        {
            amount++;
            average = Quaternion.Slerp(average, target.transform.rotation, 1 / amount); //得到所有对象的平均旋转值。这个我们并没有详细认证过到底对不对。其实如果我们真动用这个模式的话肯定不会指定多个聚焦对象
        }

        transform.position = Vector3.Slerp(transform.position, center, Time.time * 0.1f);
        transform.rotation = average;
    }

	public void CameraMode6(string[] cameraTargetNames, string direction, float distance, float height) //观察对象的正面，正后，和正侧面.除了四个正方向外，只能调整远近和高低
    {
        targetNames = cameraTargetNames;
        List<GameObject> Objects = new List<GameObject>();
        foreach (string name in targetNames)
        {
            GameObject target = GameObject.Find(name);
            Objects.Add(target);
        }
        targets = Objects.ToArray();
        Vector3 center = new Vector3(0, 0, 0);
        foreach (GameObject o in targets)
        {
            center += o.transform.position;
        }
        center /= targets.Length;// 得到所有对象的平均位置
		center += Vector3.up * height;
		center -= transform.forward * distance;

        Quaternion average = new Quaternion(0, 0, 0, 0);
        var amount = 0;
        foreach (var target in targets)
        {
            amount++;
            average = Quaternion.Slerp(average, target.transform.rotation, 1 / amount); //归根结底所谓的平均旋转值为什么频繁出现在我们的相机代码里？因为我们经常希望相机可以对准多个目标，而这原本对大部分情况下就是不需要的。
			                                                                            //如果你一看到在计算平均旋转值，你就要想这是在谈一个物体，而我们是在试图得到物体的朝向，这样就容易理解了。
        }

        transform.position = Vector3.Slerp(transform.position, center, Time.time * 0.1f);
        if (direction == "back")
            transform.rotation = average * Quaternion.Euler(0, 0, 0);
        else if (direction == "front")
            transform.rotation = average * Quaternion.Euler(0, 180, 0);
        else if (direction == "left")
            transform.rotation = average * Quaternion.Euler(0, 90, 0);
        else if (direction == "right")
            transform.rotation = average * Quaternion.Euler(0, 270, 0);
    }

	public void CameraMode5(string[] cameraTargetNames, string direction, float distance, float height) // 模式6的瞬时版本
    {
        targetNames = cameraTargetNames;
        List<GameObject> Objects = new List<GameObject>();
        foreach (string name in targetNames)
        {
            GameObject target = GameObject.Find(name);
            Objects.Add(target);
        }
        targets = Objects.ToArray();
        Vector3 center = new Vector3(0, 0, 0);
        foreach (GameObject o in targets)
        {
            center += o.transform.position;
        }
        center /= targets.Length;// 得到所有对象的平均位置
		center += Vector3.up * height;
		center -= transform.forward * distance;

        Quaternion average = new Quaternion(0, 0, 0, 0);
        var amount = 0;
        foreach (var target in targets)
        {
            amount++;
            average = Quaternion.Slerp(average, target.transform.rotation, 1 / amount); 
        }

        transform.position = center;
        if (direction == "back")
            transform.rotation = average * Quaternion.Euler(0, 0, 0);
        else if (direction == "front")
            transform.rotation = average * Quaternion.Euler(0, 180, 0);
        else if (direction == "left")
            transform.rotation = average * Quaternion.Euler(0, 90, 0);
        else if (direction == "right")
            transform.rotation = average * Quaternion.Euler(0, 270, 0);
    }

    public void CameraMode7(string[] cameraTargetNames) //貌似是一个不停转圈的相机模式
    {
        targetNames = cameraTargetNames;
        List<GameObject> Objects = new List<GameObject>();
        foreach (string name in targetNames)
        {
            GameObject target = GameObject.Find(name);
            Objects.Add(target);
        }
        targets = Objects.ToArray();

        Vector3 center = new Vector3(0, 0, 0);
        Vector3 temp = new Vector3(0, 0, 0);
        foreach (GameObject o in targets)
        {
            center += o.transform.localPosition;
        }
        center /= targets.Length;// 得到所有对象的平均位置
        temp = center;
        temp += Vector3.up * 0.1F;
        temp -= transform.forward * 0.8F;

        transform.position = Vector3.Slerp(transform.position, temp, Time.time * 0.02f);

        var n = center - transform.position;
        var newRotation = Quaternion.LookRotation(n) * Quaternion.Euler(0, 90, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 0.1f);
    }

    public void CameraMode8(string[] cameraTargetNames, float distance, float height, float direction_pingmian,float direction_chuizhi)
    {
        targetNames = cameraTargetNames;
        List<GameObject> Objects = new List<GameObject>();
        foreach (string name in targetNames)
        {
            GameObject target = GameObject.Find(name);
            Objects.Add(target);
        }
        targets = Objects.ToArray();
        Vector3 center = new Vector3(0, 0, 0);
        foreach (GameObject o in targets)
        {
            center += o.transform.position;
        }
        center /= targets.Length;// 得到所有对象的平均位置
        center += Vector3.up * height;
        center -= transform.forward * distance;

        Quaternion average = new Quaternion(0, 0, 0, 0);
        var amount = 0;
        foreach (var target in targets)
        {
            amount++;
            average = Quaternion.Slerp(average, target.transform.rotation, 1 / amount); //得到所有对象的平均旋转值。这个我们并没有详细认证过到底对不对。其实如果我们真动用这个模式的话肯定不会指定多个聚焦对象
        }

        transform.position = Vector3.Slerp(transform.position, center, Time.time * 0.01f);
        transform.rotation = average * Quaternion.Euler(direction_chuizhi, direction_pingmian, 0);
    }

    public void CameraMode9(string[] cameraTargetNames, float distance, float height, float direction_pingmian, float direction_chuizhi) //模式8的瞬间版本
    {
        targetNames = cameraTargetNames;
        List<GameObject> Objects = new List<GameObject>();
        foreach (string name in targetNames)
        {
            GameObject target = GameObject.Find(name);
            Objects.Add(target);
        }
        targets = Objects.ToArray();
        Vector3 center = new Vector3(0, 0, 0);
        foreach (GameObject o in targets)
        {
            center += o.transform.position;
        }
        center /= targets.Length;// 得到所有对象的平均位置
        center += Vector3.up * height;
        center -= transform.forward * distance;

        Quaternion average = new Quaternion(0, 0, 0, 0);
        var amount = 0;
        foreach (var target in targets)
        {
            amount++;
            average = Quaternion.Slerp(average, target.transform.rotation, 1 / amount); //得到所有对象的平均旋转值。这个我们并没有详细认证过到底对不对。其实如果我们真动用这个模式的话肯定不会指定多个聚焦对象
        }

        transform.position = center;
        transform.rotation = average * Quaternion.Euler(direction_chuizhi, direction_pingmian, 0);
    }
}