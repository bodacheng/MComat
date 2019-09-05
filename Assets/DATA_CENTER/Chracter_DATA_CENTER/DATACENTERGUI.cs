#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using HittingDetection;
using Soul;

[CustomEditor(typeof(Data_Center))]
public class DATACENTERGUI : Editor {

    GUIStyle title;
    Data_Center myScript;

    public override void OnInspectorGUI()
    {
        myScript = (Data_Center)target;
        title = new GUIStyle(GUI.skin.box);
        title.normal.textColor = Color.blue;
        title.fontSize = 11;
        //title.fixedWidth = 100f;
        GUILayout.Space(5f);
        EditorGUILayout.LabelField("以下项目在新角色构成时请按顺序填写，填写完毕后点击Construct Chracter按钮",title);

        GUILayout.Space(5f);
        myScript.Zokusei = (zokusei)EditorGUILayout.EnumPopup("zokusei", myScript.Zokusei);

        GUILayout.Space(5f);
        EditorGUILayout.LabelField("WholeT", title);
        EditorGUILayout.BeginVertical();
        myScript.WholeT = EditorGUILayout.ObjectField("WholeT", myScript.WholeT, typeof(Transform), true) as Transform;
        EditorGUILayout.EndVertical();

        GUILayout.Space(5f);
        EditorGUILayout.LabelField("Center", title);
        EditorGUILayout.BeginVertical();
        myScript.geometryCenter = EditorGUILayout.ObjectField("GeometryCenter", myScript.geometryCenter, typeof(Transform), true) as Transform;
        EditorGUILayout.EndVertical();

        GUILayout.Space(5f);
        EditorGUILayout.LabelField("各武器transform", title);

        EditorGUILayout.BeginVertical();
        myScript.right_hand_t = EditorGUILayout.ObjectField("Right Hand", myScript.right_hand_t, typeof(Transform), true) as Transform;
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical();
        myScript.left_hand_t = EditorGUILayout.ObjectField("Left Hand", myScript.left_hand_t, typeof(Transform), true) as Transform;
        EditorGUILayout.EndVertical();

        GUILayout.Space(5f);
        EditorGUILayout.LabelField("左右两脚的位置具体是脚尖，系统会自动给脚踝附近也适配marker。", title);

        EditorGUILayout.BeginVertical();
        myScript.right_foot_t = EditorGUILayout.ObjectField("Right Foot", myScript.right_foot_t, typeof(Transform), true) as Transform;
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical();
        myScript.left_foot_t = EditorGUILayout.ObjectField("Left Foot", myScript.left_foot_t, typeof(Transform), true) as Transform;
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical();
        myScript.tail_t = EditorGUILayout.ObjectField("Tail", myScript.tail_t, typeof(Transform), true) as Transform;
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical();
        myScript.head_t = EditorGUILayout.ObjectField("Head", myScript.head_t, typeof(Transform), true) as Transform;
        EditorGUILayout.EndVertical();

        //双手武器你不得不亲自设置，因为这两个东西在手上的位置实在是主观的。在这里设置后接下来的construct会直接把他们加入到其他一些模块的相应参数上
        GUILayout.Space(5f);
        EditorGUILayout.BeginVertical();//floor checker按道理讲也是个自动去适配的东西，只要我们把默认物体放在默认位置
        myScript.left_hand_weapon = EditorGUILayout.ObjectField("Left Weapon", myScript.left_hand_weapon, typeof(BO_Marker_Manager), true) as BO_Marker_Manager;
        EditorGUILayout.EndVertical();
        GUILayout.Space(5f);
        EditorGUILayout.BeginVertical();//floor checker按道理讲也是个自动去适配的东西，只要我们把默认物体放在默认位置
        myScript.right_hand_weapon = EditorGUILayout.ObjectField("Right Weapon", myScript.right_hand_weapon, typeof(BO_Marker_Manager), true) as BO_Marker_Manager;
        EditorGUILayout.EndVertical();

        GUILayout.Space(5f);
        EditorGUILayout.LabelField("HitBox Transforms", title);
        EditorGUILayout.BeginVertical();
        myScript.spine_hitbox_t = EditorGUILayout.ObjectField("SpineHitBox Transform", myScript.spine_hitbox_t, typeof(Transform), true) as Transform;
        EditorGUILayout.EndVertical();
        EditorGUILayout.BeginVertical();
        myScript.left_arm_hitbox_t = EditorGUILayout.ObjectField("LeftArmHitBox Transform", myScript.left_arm_hitbox_t, typeof(Transform), true) as Transform;
        EditorGUILayout.EndVertical();
        EditorGUILayout.BeginVertical();
        myScript.right_arm_hitbox_t = EditorGUILayout.ObjectField("RightArmHitBox Transform", myScript.right_arm_hitbox_t, typeof(Transform), true) as Transform;
        EditorGUILayout.EndVertical();
        EditorGUILayout.BeginVertical();
        myScript.left_leg_hitbox_t = EditorGUILayout.ObjectField("LeftLegHitBox Transform", myScript.left_leg_hitbox_t, typeof(Transform), true) as Transform;
        EditorGUILayout.EndVertical();
        EditorGUILayout.BeginVertical();
        myScript.right_leg_hitbox_t = EditorGUILayout.ObjectField("RightLegHitBox Transform", myScript.right_leg_hitbox_t, typeof(Transform), true) as Transform;
        EditorGUILayout.EndVertical();

        //GUILayout.Space(5f);
        //EditorGUILayout.LabelField("盾牌设置（如果不设置，将生成默认盾牌）", title);
        //EditorGUILayout.BeginVertical();
        //myScript.Shield = EditorGUILayout.ObjectField("Shield", myScript.Shield, typeof(BO_Shield), true) as BO_Shield;
        //EditorGUILayout.EndVertical();

        GUILayout.Space(5f);
        if (GUILayout.Button("Construct Chracter"))
        {
            if (myScript.WholeT)
            {
                //　WholeT上应该有以下组件：BO_Ani_E，BO_Weapon_Animation_Events，ResistanceManager，SkillCancelFlag，Animator，Rigidbody,AudioSource
                // 以上这些我们靠SKillCancelFlag来加载
                if (myScript.WholeT.GetComponent<OutsideDataLink>() == null)
                    myScript.WholeT.gameObject.AddComponent<OutsideDataLink>();
                myScript.WholeT.GetComponent<OutsideDataLink>()._C = myScript;
            }else{
                Debug.Log(" 没有适配wholeT，返回");
                return;
            }

            myScript._SkillCancelFlag = myScript.WholeT.GetComponent<SkillCancelFlag>();
            myScript._SkillCancelFlag._C = myScript;
            myScript.Sensor = myScript.gameObject.GetComponent<Sensor>();
            myScript.Sensor.sensor_radius = 15f;
            myScript.animator = myScript.WholeT.GetComponent<Animator>();
            myScript.animator.applyRootMotion = false;
            myScript.animator.updateMode = AnimatorUpdateMode.Normal;
            myScript.animator.cullingMode = AnimatorCullingMode.AlwaysAnimate;
            myScript.Animation_Manger = myScript.gameObject.GetComponent<Animation_Manger>();
            myScript.Animation_Manger.Animator = myScript.animator;
            myScript.BO_Health = myScript.gameObject.GetComponent<BO_Health>();
            myScript.BO_Health._Center = myScript;
            myScript.AIStateRunner = myScript.gameObject.GetComponent<AIStateRunner>();            
            myScript.AIStateRunner._BO_Health = myScript.BO_Health;
            myScript.AIStateRunner._SkillCancelFlag = myScript._SkillCancelFlag;
            myScript.buffsRunner = myScript.gameObject.GetComponent<BuffsRunner>();
            myScript.blendShapeProxy = myScript.gameObject.GetComponent<BlendShapeProxy>();
            myScript.Rigidbody = myScript.WholeT.GetComponent<Rigidbody>();//这个只在战斗模式需要
            myScript.Rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;//计算量最小是Discrete，但实测设置成Continuous的话一定不会产生行走穿墙。但根据该功能注释看
            //设置成Discrete或Continuous对于角色间碰撞是一样的。（Continuous式计算只对无刚体的collider有效）这样的话考虑计算量时候还牵扯到个地面的问题。。。
            myScript.Rigidbody.useGravity = false;
            myScript.Rigidbody.mass = 100f;
            myScript.Rigidbody.drag = 0f;
            myScript.Rigidbody.angularDrag = 0.05f;
            myScript.Rigidbody.isKinematic = false;
            myScript.Rigidbody.interpolation = RigidbodyInterpolation.None;
            myScript.Rigidbody.constraints = RigidbodyConstraints.None;
            myScript.Rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
            
            AudioSource audioSource = myScript.WholeT.GetComponent<AudioSource>();
            myScript._AudioSource = audioSource;
            
            BO_Ani_E bO_Ani_E = myScript.WholeT.GetComponent<BO_Ani_E>();
            myScript._BO_Ani_E = bO_Ani_E;
            bO_Ani_E._DATA_CENTER = myScript;
            
            Pusher pusher =  myScript.WholeT.GetComponent<Pusher>();
            myScript.pusher = pusher;
            pusher._DATA_CENTER = myScript;
            
            BO_Weapon_Animation_Events bO_Weapon_Animation_Events = myScript.WholeT.GetComponent<BO_Weapon_Animation_Events>();
            myScript.bO_Weapon_Animation_Events = bO_Weapon_Animation_Events;
            
            ResistanceManager resistanceManager = myScript.WholeT.GetComponent<ResistanceManager>();
            ShaderManager shaderManager = myScript.transform.GetComponent<ShaderManager>();
            resistanceManager._ShaderManager = shaderManager;
            myScript._ResistanceManager = resistanceManager;
            myScript._ShaderManager = shaderManager;
            
            BO_Hitbox focusingHitBox = null;
            if (myScript.right_arm_hitbox_t != null)
            {
                if (!myScript.right_arm_hitbox_t.GetComponent<BoxCollider>())
                {
                    myScript.right_arm_hitbox_t.gameObject.AddComponent<BoxCollider>();
                }
                myScript.right_arm_hitbox_t.GetComponent<BoxCollider>().isTrigger = false;
                myScript.right_arm_hitbox_t.GetComponent<BoxCollider>().size = new Vector3(0.3f, 0.15f, 0.15f);
                focusingHitBox = myScript.right_arm_hitbox_t.GetComponent<BO_Hitbox>();
                if (focusingHitBox == null)
                    myScript.right_arm_hitbox_t.gameObject.AddComponent<BO_Hitbox>();
                focusingHitBox = myScript.right_arm_hitbox_t.GetComponent<BO_Hitbox>();
                focusingHitBox.MainHealth = myScript.BO_Health;
            }
            if (myScript.left_arm_hitbox_t != null)
            {
                if (!myScript.left_arm_hitbox_t.GetComponent<BoxCollider>())
                {
                    myScript.left_arm_hitbox_t.gameObject.AddComponent<BoxCollider>();
                }
                myScript.left_arm_hitbox_t.GetComponent<BoxCollider>().isTrigger = false;
                myScript.left_arm_hitbox_t.GetComponent<BoxCollider>().size = new Vector3(0.3f, 0.15f, 0.15f);
                focusingHitBox = myScript.left_arm_hitbox_t.GetComponent<BO_Hitbox>();
                if (focusingHitBox == null)
                    myScript.left_arm_hitbox_t.gameObject.AddComponent<BO_Hitbox>();
                focusingHitBox = myScript.left_arm_hitbox_t.GetComponent<BO_Hitbox>();
                focusingHitBox.MainHealth = myScript.BO_Health;
            }
            if (myScript.right_leg_hitbox_t != null)
            {
                if (!myScript.right_leg_hitbox_t.GetComponent<BoxCollider>())
                {
                    myScript.right_leg_hitbox_t.gameObject.AddComponent<BoxCollider>();
                }
                myScript.right_leg_hitbox_t.GetComponent<BoxCollider>().isTrigger = false;
                myScript.right_leg_hitbox_t.GetComponent<BoxCollider>().size = new Vector3(0.3f, 0.15f, 0.15f);
                focusingHitBox = myScript.right_leg_hitbox_t.GetComponent<BO_Hitbox>();
                if (focusingHitBox == null)
                    myScript.right_leg_hitbox_t.gameObject.AddComponent<BO_Hitbox>();
                focusingHitBox = myScript.right_leg_hitbox_t.GetComponent<BO_Hitbox>();
                focusingHitBox.MainHealth = myScript.BO_Health;
            }
            if (myScript.left_leg_hitbox_t != null)
            {
                if (!myScript.left_leg_hitbox_t.GetComponent<BoxCollider>())
                {
                    myScript.left_leg_hitbox_t.gameObject.AddComponent<BoxCollider>();
                }
                myScript.left_leg_hitbox_t.GetComponent<BoxCollider>().isTrigger = false;
                myScript.left_leg_hitbox_t.GetComponent<BoxCollider>().size = new Vector3(0.3f, 0.15f, 0.15f);
                focusingHitBox = myScript.left_leg_hitbox_t.GetComponent<BO_Hitbox>();
                if (focusingHitBox == null)
                    myScript.left_leg_hitbox_t.gameObject.AddComponent<BO_Hitbox>();
                focusingHitBox = myScript.left_leg_hitbox_t.GetComponent<BO_Hitbox>();
                focusingHitBox.MainHealth = myScript.BO_Health;
            }
            if (myScript.spine_hitbox_t != null)
            {
                if (!myScript.spine_hitbox_t.GetComponent<BoxCollider>())
                {
                    myScript.spine_hitbox_t.gameObject.AddComponent<BoxCollider>();
                }
                myScript.spine_hitbox_t.GetComponent<BoxCollider>().isTrigger = false;
                myScript.spine_hitbox_t.GetComponent<BoxCollider>().size = new Vector3(1f, 1f, 1f);

                focusingHitBox = myScript.spine_hitbox_t.GetComponent<BO_Hitbox>();
                if (focusingHitBox == null)
                    myScript.spine_hitbox_t.gameObject.AddComponent<BO_Hitbox>();
                focusingHitBox = myScript.spine_hitbox_t.GetComponent<BO_Hitbox>();
                focusingHitBox.MainHealth = myScript.BO_Health;
            }

            List<Transform> weaponPartsOnBody = new List<Transform>();
            if (myScript.right_hand_t != null)
                weaponPartsOnBody.Add(myScript.right_hand_t);
            if (myScript.left_hand_t != null)
                weaponPartsOnBody.Add(myScript.left_hand_t);
            if (myScript.right_foot_t != null)
                weaponPartsOnBody.Add(myScript.right_foot_t);
            if (myScript.left_foot_t != null)
                weaponPartsOnBody.Add(myScript.left_foot_t);
            if (myScript.head_t != null)
                weaponPartsOnBody.Add(myScript.head_t);
            if (myScript.tail_t != null)
                weaponPartsOnBody.Add(myScript.tail_t);

            foreach (Transform _t in weaponPartsOnBody)
            {
                if (_t.GetComponent<BO_Marker_Manager>() == null)
                {
                    _t.gameObject.AddComponent<BO_Marker_Manager>();
                    GameObject child_marker = new GameObject();
                    child_marker.name = "WeaponMarker";
                    child_marker.AddComponent<BO_Marker>();
                    child_marker.GetComponent<BO_Marker>().radius = 0.5f;
                    child_marker.transform.SetParent(_t);
                    child_marker.transform.localPosition = Vector3.zero;
                    _t.GetComponent<BO_Marker_Manager>().setWeaponOwnerHealth(myScript.BO_Health);

                    if (_t == myScript.left_foot_t || _t == myScript.right_foot_t)
                    {
                        GameObject child_marker2 = new GameObject();
                        child_marker2.AddComponent<BO_Marker>();
                        child_marker2.GetComponent<BO_Marker>().radius = 0.5f;
                        child_marker2.transform.SetParent(_t);

                        child_marker2.transform.localPosition = child_marker.transform.localPosition - new Vector3(0,0,0.5f);//脚踝
                    }
                }else{
                    BO_Marker[] markers = _t.GetComponentsInChildren<BO_Marker>();
                    foreach(BO_Marker marker in markers)
                    {
                        marker.radius = 0.5f;
                    }
                }
            }
            if (myScript.right_hand_t != null)
            {
                if (myScript.right_hand_t.GetComponent<BO_Marker_Manager>())
                    myScript.right_hand = myScript.right_hand_t.GetComponent<BO_Marker_Manager>();
            }

            if (myScript.left_hand_t != null)
            {
                if (myScript.left_hand_t.GetComponent<BO_Marker_Manager>())
                    myScript.left_hand = myScript.left_hand_t.GetComponent<BO_Marker_Manager>();
            }

            if (myScript.right_foot_t != null)
            {
                if (myScript.right_foot_t.GetComponent<BO_Marker_Manager>())
                    myScript.right_foot = myScript.right_foot_t.GetComponent<BO_Marker_Manager>();
            }

            if (myScript.left_foot_t != null)
            {
                if (myScript.left_foot_t.GetComponent<BO_Marker_Manager>())
                    myScript.left_foot = myScript.left_foot_t.GetComponent<BO_Marker_Manager>();
            }

            if (myScript.head_t != null)
            {
                if (myScript.head_t.GetComponent<BO_Marker_Manager>())
                    myScript.head = myScript.head_t.GetComponent<BO_Marker_Manager>();
            }

            if (myScript.tail_t != null)
            {
                if (myScript.tail_t.GetComponent<BO_Marker_Manager>())
                    myScript.tail = myScript.tail_t.GetComponent<BO_Marker_Manager>();
            }

            string bladeName;
            string shieldName;
            switch(myScript.Zokusei)
            {
                case zokusei.darkMagic:
                    bladeName = "D_enegryBlade";
                    shieldName = "dark_Shield"; 
                    break;
                case zokusei.blueMagic:
                    bladeName = "B_enegryBlade";
                    shieldName = "blue_Shield";
                    break;
                case zokusei.greenMagic:
                    bladeName = "G_enegryBlade";
                    shieldName = "green_Shield";
                    break;
                case zokusei.lightMagic:
                    bladeName = "W_enegryBlade";
                    shieldName = "light_Shield";
                    break;
                case zokusei.redMagic:
                    bladeName = "R_enegryBlade";
                    shieldName = "red_Shield";
                    break;
                default:
                    bladeName = "D_enegryBlade";
                    shieldName = "blue_Shield";
                    break;
            }

            if (myScript.right_hand_weapon == null)
            {
                GameObject enegryBlade = GameObject.Instantiate(Resources.Load("BasicCharComponent" + "/" + bladeName) as GameObject);
                enegryBlade.name = bladeName;
                enegryBlade.transform.SetParent(myScript.right_hand_t);
				enegryBlade.transform.localPosition = Vector3.zero;
                enegryBlade.transform.localRotation = Quaternion.Euler(180, 0, 0);//这个事情非常不一定
                myScript.right_hand_weapon = enegryBlade.GetComponent<BO_Marker_Manager>();
            }
            if (myScript.left_hand_weapon == null)
            {
                GameObject enegryBlade = GameObject.Instantiate(Resources.Load("BasicCharComponent" + "/" + bladeName) as GameObject);
                enegryBlade.name = bladeName;
                enegryBlade.transform.SetParent(myScript.left_hand_t);
				enegryBlade.transform.localPosition = Vector3.zero;
				enegryBlade.transform.localRotation = Quaternion.identity;
                myScript.left_hand_weapon = enegryBlade.GetComponent<BO_Marker_Manager>();
            }

			if (myScript.right_hand_weapon)
			{
				myScript.right_hand_weapon.setOnEnableEffectT(myScript.right_hand_t);
			}                    
			if (myScript.left_hand_weapon)
			{
				myScript.left_hand_weapon.setOnEnableEffectT(myScript.left_hand_t);
			}

            if (myScript.floorChecks == null)
            {
                GameObject floorChecker = GameObject.Instantiate(Resources.Load("BasicCharComponent" + "/" + "FloorChecks") as GameObject);
                if (floorChecker)
                {
                    floorChecker.name = "FloorChecks";
                    floorChecker.transform.parent = null;
                    floorChecker.transform.SetParent(myScript.WholeT.transform);
                    floorChecker.transform.localPosition = Vector3.zero;
                    floorChecker.transform.rotation = Quaternion.identity;
                }
                myScript.floorChecks = floorChecker.transform;
            }

            //2019.3.29 我们基本放弃了传统防御盾逻辑。这让我们无比纠结但相关防御检测代码还在系统里只是没打开。
            //if (myScript.Shield == null)
            //{
            //    GameObject shield = GameObject.Instantiate(Resources.Load("BasicCharComponent" + "/Shield/" + shieldName) as GameObject);
            //    if (shield != null)
            //    {
            //        shield.transform.SetParent(myScript.WholeT);
            //        shield.transform.localRotation = Quaternion.Euler(0, 180, 0);//这个事情非常不一定
            //        shield.transform.position = myScript.geometryCenter.transform.position +
            //            myScript.gameObject.transform.forward * (myScript.Sensor.innerSensorRadius - 2.3f)
            //            + new Vector3(0,0.2f,0);
            //            ;
            //        myScript.Shield = shield.GetComponent<BO_Shield>();
            //        myScript.Shield._ShieldBackSpot = myScript.geometryCenter.transform;
            //        myScript.Shield._ParentHealth = myHealth;
            //    }
            //}else{
            //    GameObject shield_new = GameObject.Instantiate(Resources.Load("BasicCharComponent" + "/Shield/" + shieldName) as GameObject);
            //    if (shield_new != null)
            //    {
            //        shield_new.transform.SetParent(myScript.gameObject.transform);
            //        shield_new.transform.localRotation = myScript.Shield.gameObject.transform.localRotation;
            //        shield_new.transform.localPosition = myScript.Shield.gameObject.transform.localPosition;

            //        DestroyImmediate(myScript.Shield.gameObject);

            //        myScript.Shield = shield_new.GetComponent<BO_Shield>();
            //        myScript.Shield._ShieldBackSpot = myScript.geometryCenter.transform;
            //        myScript.Shield._ParentHealth = myHealth;
            //    }
            //}
        }

        GUILayout.Space(5f);
        EditorGUILayout.LabelField("以下项目在完成construct后应该会自动出现。如果事前手动适配，则construct操作不会更改他们",title);

        GUILayout.Space(5f);
        EditorGUILayout.BeginVertical();//floor checker按道理讲也是个自动去适配的东西，只要我们把默认物体放在默认位置
        myScript.floorChecks = EditorGUILayout.ObjectField("Floor Checker", myScript.floorChecks, typeof(Transform), true) as Transform;
        EditorGUILayout.EndVertical();

        GUILayout.Space(5f);
        EditorGUILayout.BeginVertical();//floor checker按道理讲也是个自动去适配的东西，只要我们把默认物体放在默认位置
        myScript.Sensor = EditorGUILayout.ObjectField("Sensor", myScript.Sensor, typeof(Sensor), true) as Sensor;
        EditorGUILayout.EndVertical();

        title.normal.textColor = Color.red;
        GUILayout.Space(5f);
        EditorGUILayout.LabelField("所有角色在创建的时候要遵循这样重要的一点：作为角色在地面支撑用的collider，",title);
        EditorGUILayout.LabelField(" 其下沿边必须低于gameobject。transform。position，并且高于floorcheckers中的marker。",title);
        EditorGUILayout.LabelField("环境感知器的内环要超出所有身体collider，我们有一个攻击迈步系统，所以一个角色攻击另一个的时候略以远距离开始攻击没有关系", title);

        if (GUILayout.Button("点一次这个。里面包括了清理多余Rigidbody和忽略自我碰撞两个方面"))
        {
            allCollider.Clear();
            CleanAllChildrenFromRigidBody(myScript.WholeT);
            for (int i = 0; i < allCollider.Count; i++)
            {
                for (int y = i + 1; y < allCollider.Count; y++)
                {
                    Physics.IgnoreCollision(allCollider[i], allCollider[y]);
                }
            }
            allCollider.Clear();
        }
    }

    List<Collider> allCollider = new List<Collider>();
    public void CleanAllChildrenFromRigidBody(Transform T)
    {
        foreach (Transform _t in T)
        {
            Collider C = _t.GetComponent<Collider>();
            if (C != null)
            {
                allCollider.Add(C);
            }
            if (_t.GetComponent<Rigidbody>())
            {
                DestroyImmediate(_t.GetComponent<Rigidbody>());
                Debug.Log("清理了"+_t+"节点的刚体");
            }
            CleanAllChildrenFromRigidBody(_t);
        }
        return;
    }
}
#endif