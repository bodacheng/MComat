#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AI_DATA_CENTER))]
public class DATACENTERGUI : Editor {

    GUIStyle title;

    AI_DATA_CENTER myScript;
    public override void OnInspectorGUI()
    {
        myScript = (AI_DATA_CENTER)target;

        title = new GUIStyle(GUI.skin.box);
        title.normal.textColor = Color.blue;
        title.fontSize = 11;
        //title.fixedWidth = 100f;
        GUILayout.Space(5f);
        EditorGUILayout.LabelField("以下项目在新角色构成时请按顺序填写，填写完毕后点击Construct Chracter按钮",title);

        GUILayout.Space(5f);
        myScript.Zokusei = (zokusei)EditorGUILayout.EnumPopup("zokusei", myScript.Zokusei);

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
            myScript.animator = myScript.gameObject.GetComponent<Animator>();
            if (myScript.animator != null)
            {
                //myScript.animator.runtimeAnimatorController = Resources.Load(characterType+"/"+characterType) as RuntimeAnimatorController;
                myScript.animator.applyRootMotion = false;
                myScript.animator.updateMode = AnimatorUpdateMode.Normal;
                myScript.animator.cullingMode = AnimatorCullingMode.AlwaysAnimate;
            }

            myScript.Animation_Manger = myScript.gameObject.GetComponent<Animation_Manger>();
            myScript.Animation_Manger.Animator = myScript.animator;

            BO_Health myHealth = myScript.gameObject.GetComponent<BO_Health>();

            BO_Hitbox focusingHitBox = null;
            if (myScript.right_arm_hitbox_t != null)
            {
                //if (!myScript.right_arm_hitbox_t.GetComponent<BoxCollider>())
                //{
                //    myScript.right_arm_hitbox_t.gameObject.AddComponent<BoxCollider>();
                //}
                //myScript.right_arm_hitbox_t.GetComponent<BoxCollider>().isTrigger = false;
                //myScript.right_arm_hitbox_t.GetComponent<BoxCollider>().size = new Vector3(0.4f, 0.2f, 0.2f);

                //focusingHitBox = myScript.right_arm_hitbox_t.GetComponent<BO_Hitbox>();
                //if (focusingHitBox == null)
                //    myScript.right_arm_hitbox_t.gameObject.AddComponent<BO_Hitbox>();
                //focusingHitBox = myScript.right_arm_hitbox_t.GetComponent<BO_Hitbox>();
                //focusingHitBox.MainHealth = myHealth;
                //focusingHitBox.DisableColliderOnDeath = true;               
            }
            if (myScript.left_arm_hitbox_t != null)
            {
                //if (!myScript.left_arm_hitbox_t.GetComponent<BoxCollider>())
                //{
                //    myScript.left_arm_hitbox_t.gameObject.AddComponent<BoxCollider>();
                //}
                //myScript.left_arm_hitbox_t.GetComponent<BoxCollider>().isTrigger = false;
                //myScript.left_arm_hitbox_t.GetComponent<BoxCollider>().size = new Vector3(0.4f, 0.2f, 0.2f);

                //focusingHitBox = myScript.left_arm_hitbox_t.GetComponent<BO_Hitbox>();
                //if (focusingHitBox == null)
                //    myScript.left_arm_hitbox_t.gameObject.AddComponent<BO_Hitbox>();
                //focusingHitBox = myScript.left_arm_hitbox_t.GetComponent<BO_Hitbox>();
                //focusingHitBox.MainHealth = myHealth;
                //focusingHitBox.DisableColliderOnDeath = true;
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
                focusingHitBox.MainHealth = myHealth;
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
                focusingHitBox.MainHealth = myHealth;
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
                focusingHitBox.MainHealth = myHealth;
            }

            if (myScript.geometryCenter)
            {
                Sensor sensor = myScript.geometryCenter.GetComponent<Sensor>();
                if (sensor == null)
                {
                    sensor = myScript.geometryCenter.gameObject.AddComponent<Sensor>();
                    myScript.Sensor = sensor;
                }
                sensor.sensor_radius = 15f;
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
                    child_marker.GetComponent<BO_Marker>().radius = 0.6f;
                    child_marker.transform.SetParent(_t);
                    child_marker.transform.localPosition = Vector3.zero;
                    _t.GetComponent<BO_Marker_Manager>().setWeaponOwnerHealth(myHealth);

                    if (_t == myScript.left_foot_t || _t == myScript.right_foot_t)
                    {
                        GameObject child_marker2 = new GameObject();
                        child_marker2.AddComponent<BO_Marker>();
                        child_marker2.GetComponent<BO_Marker>().radius = 0.6f;
                        child_marker2.transform.SetParent(_t);

                        child_marker2.transform.localPosition = child_marker.transform.localPosition - new Vector3(0,0,0.5f);//脚踝
                    }
                }else{
                    BO_Marker[] markers = _t.GetComponentsInChildren<BO_Marker>();
                    foreach(BO_Marker marker in markers)
                    {
                        marker.radius = 0.6f;
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
                    floorChecker.transform.SetParent(myScript.gameObject.transform);
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
            //        shield.transform.SetParent(myScript.gameObject.transform);
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

            Rigidbody R = myScript.gameObject.GetComponent<Rigidbody>();
            if (R == null)
            {
                myScript.gameObject.AddComponent<Rigidbody>();
                R = myScript.gameObject.GetComponent<Rigidbody>();
            }
            if (R != null)
            {
                R.mass = 100f;
                R.drag = 0f;
                R.angularDrag = 0.05f;
                R.useGravity = true;
                R.isKinematic = false;
                R.interpolation = RigidbodyInterpolation.None;
                R.collisionDetectionMode = CollisionDetectionMode.Continuous;
                R.constraints = RigidbodyConstraints.None;
                R.constraints = RigidbodyConstraints.FreezeRotation;
            }
            //SphereCollider _collider = myScript.gameObject.GetComponent<SphereCollider>();
            //if (_collider == null)
            //{
            //    myScript.gameObject.AddComponent<SphereCollider>();
            //    _collider = myScript.gameObject.GetComponent<SphereCollider>();
            //}
            //PhysicMaterial moca = (PhysicMaterial)Resources.Load("PhysicMaterials/moca");
            //_collider.isTrigger = false;
            //_collider.material = moca;
            //_collider.center = new Vector3(0,0.19f,0);
            //_collider.radius = 0.2f;
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

        title = new GUIStyle(GUI.skin.label);
        title.normal.textColor = Color.red;
        GUILayout.Space(5f);
        EditorGUILayout.LabelField("所有角色在创建的时候要遵循这样重要的一点：作为角色在地面支撑用的collider，",title);
        EditorGUILayout.LabelField(" 其下沿边必须低于gameobject。transform。position，并且高于floorcheckers中的marker。",title);
        EditorGUILayout.LabelField("环境感知器的内环要超出所有身体collider，我们有一个攻击迈步系统，所以一个角色攻击另一个的时候略以远距离开始攻击没有关系", title);

        if (GUILayout.Button("clean children RigidBody"))
        {
            allCollider.Clear();
            CleanAllChildrenFromRigidBody(myScript.transform);
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