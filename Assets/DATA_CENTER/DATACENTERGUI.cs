#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Soul;
using UnityEngine.AddressableAssets;

[CustomEditor(typeof(Data_Center))]
public class DATACENTERGUI : Editor {

    GUIStyle _title;
    Data_Center _myScript;

    static readonly Vector3 LimbHitboxSize = new Vector3(0.3f, 0.15f, 0.15f);
    static readonly Vector3 SpineHitboxSize = new Vector3(1f, 1f, 1f);

    public override void OnInspectorGUI()
    {
        _myScript = (Data_Center)target;

        if (Application.isPlaying)
            return;
        
        _title = new GUIStyle(GUI.skin.box)
        {
            normal =
            {
                textColor = Color.blue
            },
            fontSize = 11
        };
        GUILayout.Space(5f);
        EditorGUILayout.LabelField("以下项目在新角色构成时请按顺序填写，填写完毕后点击Construct Unit按钮", _title);
        GUILayout.Space(5f);
        
        _myScript.element = (Element)EditorGUILayout.EnumPopup("zokusei", _myScript.element);
        GUILayout.Space(5f);
        EditorGUILayout.LabelField("WholeT", _title);
        _myScript.WholeT = EditorGUILayout.ObjectField("WholeT", _myScript.WholeT, typeof(Transform), true) as Transform;

        GUILayout.Space(5f);
        EditorGUILayout.LabelField("各武器transform", _title);
        _myScript.right_hand_t = EditorGUILayout.ObjectField("Right Hand", _myScript.right_hand_t, typeof(Transform), true) as Transform;
        _myScript.left_hand_t = EditorGUILayout.ObjectField("Left Hand", _myScript.left_hand_t, typeof(Transform), true) as Transform;
        _myScript.right_foot_t = EditorGUILayout.ObjectField("Right Foot", _myScript.right_foot_t, typeof(Transform), true) as Transform;
        _myScript.left_foot_t = EditorGUILayout.ObjectField("Left Foot", _myScript.left_foot_t, typeof(Transform), true) as Transform;
        _myScript.tail_t = EditorGUILayout.ObjectField("Tail", _myScript.tail_t, typeof(Transform), true) as Transform;
        _myScript.head_t = EditorGUILayout.ObjectField("Head", _myScript.head_t, typeof(Transform), true) as Transform;
        GUILayout.Space(5f);
        
        EditorGUILayout.LabelField("HitBox Transforms", _title);
        _myScript.spine_hitbox_t = EditorGUILayout.ObjectField("SpineHitBox Transform", _myScript.spine_hitbox_t, typeof(Transform), true) as Transform;
        _myScript.left_arm_hitbox_t = EditorGUILayout.ObjectField("LeftArmHitBox Transform", _myScript.left_arm_hitbox_t, typeof(Transform), true) as Transform;
        _myScript.right_arm_hitbox_t = EditorGUILayout.ObjectField("RightArmHitBox Transform", _myScript.right_arm_hitbox_t, typeof(Transform), true) as Transform;
        _myScript.left_leg_hitbox_t = EditorGUILayout.ObjectField("LeftLegHitBox Transform", _myScript.left_leg_hitbox_t, typeof(Transform), true) as Transform;
        _myScript.right_leg_hitbox_t = EditorGUILayout.ObjectField("RightLegHitBox Transform", _myScript.right_leg_hitbox_t, typeof(Transform), true) as Transform;
        GUILayout.Space(5f);
        
        //GUILayout.Space(5f);
        //EditorGUILayout.LabelField("盾牌设置（如果不设置，将生成默认盾牌）", title);
        //EditorGUILayout.BeginVertical();
        //myScript.Shield = EditorGUILayout.ObjectField("Shield", myScript.Shield, typeof(BO_Shield), true) as BO_Shield;
        //EditorGUILayout.EndVertical();

        if (GUILayout.Button("Construct Unit"))
        {
            _myScript.geometryCenter = _myScript.transform;
            if (_myScript.WholeT == null)
            {
                Debug.Log(" 没有适配wholeT，返回");
                return;
            }

            EnsureRuntimeReferences(_myScript);
            ConfigureRigidBodyDefaults(_myScript);
            EnsureHitboxColliders(_myScript);

            string bladeName;
            //string shieldName;
            switch (_myScript.element)
            {
                case Element.darkMagic:
                    bladeName = "D_enegryBlade.prefab";
                    //shieldName = "dark_Shield"; 
                    break;
                case Element.blueMagic:
                    bladeName = "B_enegryBlade.prefab";
                    //shieldName = "blue_Shield";
                    break;
                case Element.greenMagic:
                    bladeName = "G_enegryBlade.prefab";
                    //shieldName = "green_Shield";
                    break;
                case Element.lightMagic:
                    bladeName = "W_enegryBlade.prefab";
                    //shieldName = "light_Shield";
                    break;
                case Element.redMagic:
                    bladeName = "R_enegryBlade.prefab";
                    //shieldName = "red_Shield";
                    break;
                default:
                    bladeName = "D_enegryBlade.prefab";
                    //shieldName = "blue_Shield";
                    break;
            }
            
            if (_myScript.Personality_events != null)
            {
                EnsureEnergyBlade(ref _myScript.Personality_events.right_sword, _myScript.right_hand_t, bladeName, Quaternion.Euler(180, 0, 0));
                EnsureEnergyBlade(ref _myScript.Personality_events.left_sword, _myScript.left_hand_t, bladeName, Quaternion.identity);
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
        EditorGUILayout.LabelField("以下项目在完成construct后应该会自动出现。如果事前手动适配，则construct操作不会更改他们",_title);
        
        _title.normal.textColor = Color.red;
        GUILayout.Space(5f);
        EditorGUILayout.LabelField("所有角色在创建的时候要遵循这样重要的一点：作为角色在地面支撑用的collider，",_title);
        EditorGUILayout.LabelField(" 其下沿边必须低于gameobject。transform。position，并且高于floorcheckers中的marker。",_title);
        EditorGUILayout.LabelField("环境感知器的内环要超出所有身体collider，我们有一个攻击迈步系统，所以一个角色攻击另一个的时候略以远距离开始攻击没有关系", _title);

        if (GUILayout.Button("点一次这个。里面包括了清理多余Rigidbody和忽略自我碰撞两个方面"))
        {
            _allCollider.Clear();
            CleanAllChildrenFromRigidBody(_myScript.WholeT);
            for (int i = 0; i < _allCollider.Count; i++)
            {
                for (int y = i + 1; y < _allCollider.Count; y++)
                {
                    Physics.IgnoreCollision(_allCollider[i], _allCollider[y]);
                }
            }
            _allCollider.Clear();
        }
    }

    static void EnsureEnergyBlade(ref GameObject bladeHolder, Transform parent, string bladeName, Quaternion rotation)
    {
        if (bladeHolder != null || parent == null)
            return;

        var prefab = LoadBasicUnitComponent(bladeName);
        if (prefab == null)
            return;

        var enegryBlade = Object.Instantiate(prefab);
        enegryBlade.name = bladeName;
        enegryBlade.transform.SetParent(parent);
        enegryBlade.transform.localPosition = Vector3.zero;
        enegryBlade.transform.localRotation = rotation;
        bladeHolder = enegryBlade;
    }

    static GameObject LoadBasicUnitComponent(string bladeName)
    {
        var op = Addressables.LoadAssetAsync<GameObject>("BasicUnitComponent/" + bladeName);
        var prefab = op.WaitForCompletion();
        Addressables.Release(op);
        return prefab;
    }

    static void EnsureRuntimeReferences(Data_Center center)
    {
        if (center == null)
            return;

        center.geometryCenter = center.geometryCenter == null ? center.transform : center.geometryCenter;
        EnsureOutsideDataLink(center);

        center._MyBehaviorRunner = center.GetComponent<BehaviorRunner>();
        center._ShaderManager = center.GetComponent<ShaderManager>();
        center.blendShapeProxy = center.GetComponent<BlendShapeProxy>();
        center.FightDataRef.Center = center;

        if (center.WholeT == null)
            return;

        center._AudioSource = center.WholeT.GetComponent<AudioSource>();
        center._SkillCancelFlag = center.WholeT.GetComponent<SkillCancelFlag>();
        if (center._SkillCancelFlag != null)
        {
            center._SkillCancelFlag._C = center;
        }

        center._BO_Ani_E = center.WholeT.GetComponent<BO_Ani_E>();
        if (center._BO_Ani_E != null)
        {
            center._BO_Ani_E._DATA_CENTER = center;
        }

        center.bO_Weapon_Animation_Events = center.WholeT.GetComponent<BO_Weapon_Animation_Events>();
        center._BasicPhysicSupport = center.WholeT.GetComponent<BasicPhysicSupport>();
        if (center._BasicPhysicSupport != null)
        {
            center._BasicPhysicSupport._DATA_CENTER = center;
            center._BasicPhysicSupport.animator = center.WholeT.GetComponent<Animator>();
            if (center._BasicPhysicSupport.animator != null)
            {
                center._BasicPhysicSupport.animator.applyRootMotion = false;
                center._BasicPhysicSupport.animator.updateMode = AnimatorUpdateMode.Normal;
                center._BasicPhysicSupport.animator.cullingMode = AnimatorCullingMode.AlwaysAnimate;
            }
        }

        var resistanceManager = center.WholeT.GetComponent<ResistanceManager>();
        if (resistanceManager != null)
        {
            resistanceManager.data_Center = center;
        }
        center._ResistanceManager = resistanceManager;
        center.Personality_events = center.WholeT.GetComponent<Personality_events>();

        if (center._MyBehaviorRunner != null && center._SkillCancelFlag != null)
        {
            center._MyBehaviorRunner._SkillCancelFlag = center._SkillCancelFlag;
        }
    }

    static void EnsureOutsideDataLink(Data_Center center)
    {
        if (center == null || center.WholeT == null)
            return;

        var outsideDataLink = center.WholeT.GetComponent<OutsideDataLink>();
        if (outsideDataLink == null)
        {
            outsideDataLink = center.WholeT.gameObject.AddComponent<OutsideDataLink>();
        }
        outsideDataLink._C = center;
    }

    static void ConfigureRigidBodyDefaults(Data_Center center)
    {
        if (center == null)
            return;

        if (center._BasicPhysicSupport == null && center.WholeT != null)
        {
            center._BasicPhysicSupport = center.WholeT.GetComponent<BasicPhysicSupport>();
        }

        var basicPhysicSupport = center._BasicPhysicSupport;
        if (basicPhysicSupport == null)
            return;

        if (basicPhysicSupport.Rigidbody == null && center.WholeT != null)
        {
            basicPhysicSupport.Rigidbody = center.WholeT.GetComponent<Rigidbody>();
        }

        var rigidbody = basicPhysicSupport.Rigidbody;
        if (rigidbody == null)
            return;

        rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
        rigidbody.useGravity = false;
        rigidbody.mass = FightGlobalSetting.FighterRigidMass;
        rigidbody.linearDamping = 0f;
        rigidbody.angularDamping = 0.05f;
        rigidbody.isKinematic = false;
        rigidbody.interpolation = RigidbodyInterpolation.None;
        rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
    }

    static void EnsureHitboxColliders(Data_Center center)
    {
        if (center == null)
            return;

        EnsureHitbox(center, center.right_arm_hitbox_t, LimbHitboxSize);
        EnsureHitbox(center, center.left_arm_hitbox_t, LimbHitboxSize);
        EnsureHitbox(center, center.right_leg_hitbox_t, LimbHitboxSize);
        EnsureHitbox(center, center.left_leg_hitbox_t, LimbHitboxSize);
        EnsureHitbox(center, center.spine_hitbox_t, SpineHitboxSize);
    }

    static void EnsureHitbox(Data_Center center, Transform hitboxTransform, Vector3 size)
    {
        if (center == null || hitboxTransform == null)
            return;

        var boxCollider = hitboxTransform.GetComponent<BoxCollider>();
        if (boxCollider == null)
        {
            boxCollider = hitboxTransform.gameObject.AddComponent<BoxCollider>();
        }
        boxCollider.size = size;
        boxCollider.isTrigger = false;

        var limb = hitboxTransform.GetComponent<BO_Limb>();
        if (limb == null)
        {
            limb = hitboxTransform.gameObject.AddComponent<BO_Limb>();
        }
        limb.Center = center;
    }

    readonly List<Collider> _allCollider = new List<Collider>();
    void CleanAllChildrenFromRigidBody(Transform T)
    {
        foreach (Transform _t in T)
        {
            Collider C = _t.GetComponent<Collider>();
            if (C != null)
            {
                _allCollider.Add(C);
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

            //foreach (Transform _t in weaponPartsOnBody)
            //{
            //    if (_t.GetComponent<BO_Marker_Manager>() == null)
            //    {
            //        _t.gameObject.AddComponent<BO_Marker_Manager>();
            //        GameObject child_marker = new GameObject();
            //        child_marker.name = "WeaponMarker";
            //        child_marker.AddComponent<BO_Marker>();
            //        child_marker.GetComponent<BO_Marker>().radius = 0.5f;
            //        child_marker.transform.SetParent(_t);
            //        child_marker.transform.localPosition = Vector3.zero;
            //        _t.GetComponent<BO_Marker_Manager>().SetWeaponOwnerHealth(myScript.BO_Health);
            //        if (_t == myScript.left_foot_t || _t == myScript.right_foot_t)
            //        {
            //            GameObject child_marker2 = new GameObject();
            //            child_marker2.AddComponent<BO_Marker>();
            //            child_marker2.GetComponent<BO_Marker>().radius = 0.5f;
            //            child_marker2.transform.SetParent(_t);
            //            child_marker2.transform.localPosition = child_marker.transform.localPosition - new Vector3(0,0,0.5f);//脚踝
            //        }
            //    }else{
            //        BO_Marker[] markers = _t.GetComponentsInChildren<BO_Marker>();
            //        foreach(BO_Marker marker in markers)
            //        {
            //            marker.radius = 0.5f;
            //        }
            //    }
            //}
