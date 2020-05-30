using System.Collections.Generic;
using UnityEngine;

public class BasicPhysicSupport : MonoBehaviour
{
    public Data_Center _DATA_CENTER;
    public Animator animator;
    public Rigidbody Rigidbody;
    public HiddenMethods hiddenMethods;
    public Transform floorCheckersT;
    Transform[] floorCheckers;

    public class HiddenMethods
    {
        readonly BasicPhysicSupport _BasicPhysicSupport;
        public bool meTouchingEnemyBody;
        public bool onBattleGroundBundary;
        public Vector3 antiWallDirection;//往墙内走的方向，防止角色AI冲着墙走。我们的游戏里角色的走位基本是基于队友和敌人，通过地形判断走位只有这一条
        readonly List<int> TheCollidersITouched = new List<int>();
        
        public HiddenMethods(BasicPhysicSupport _BasicPhysicSupport)
        {
            this._BasicPhysicSupport = _BasicPhysicSupport;
        }
        public bool IfStepOnEnemy(Collider box)
        {
            if (box == null)
                return false;
            if (box.isTrigger)
                return false;
            if (_BasicPhysicSupport._DATA_CENTER == null)
                return false;
            if (_BasicPhysicSupport._DATA_CENTER._TeamConfig == null)
                return false;
                
            return _BasicPhysicSupport._DATA_CENTER._TeamConfig.enemyLayerMask == (_BasicPhysicSupport._DATA_CENTER._TeamConfig.enemyLayerMask | (1 << box.gameObject.layer))
                ||
                (_BasicPhysicSupport._DATA_CENTER._TeamConfig.enemyShieldLayerMask & (1 << box.gameObject.layer)) != 0;
        }
        
        public bool IfStepOnFriendCharacter(Collider box)
        {
            return _BasicPhysicSupport._DATA_CENTER == null || _BasicPhysicSupport._DATA_CENTER._TeamConfig != null
                && (_BasicPhysicSupport._DATA_CENTER._TeamConfig.mylayer == box.gameObject.layer) || _BasicPhysicSupport._DATA_CENTER._TeamConfig.myShieldLayer == box.gameObject.layer;
        }

        public void ITouchedThisCollider(int _dmg)
        {
            TheCollidersITouched.Add(_dmg);
        }
        
        public void ClearHitCountForAttackStepping()
        {
            TheCollidersITouched.Clear();
        }
        public bool ITouchedEnemyBody()
        {
            return TheCollidersITouched.Count > 0;
        }
        
        bool grounded;
        public bool Grounded
        {
            get => grounded;
            set {
                grounded = value;
                _BasicPhysicSupport.Rigidbody.useGravity = _BasicPhysicSupport.UsingGravity && !grounded;
            }
        }
        
        readonly float floorY = 0;
        public void GroundedCal()
        {
            foreach (Transform check in _BasicPhysicSupport.floorCheckers)
            {
                if (floorY >= check.transform.position.y)
                {
                    Grounded = true;
                    return;
                }
            }
            Grounded = false;
        }
        
        public void ResetAnimator()
        {
            if (_BasicPhysicSupport.animator)
            {
                _BasicPhysicSupport.animator.SetBool("Grounded", true);
                _BasicPhysicSupport.animator.SetFloat("groundedCount", 10);
            }
        }
    }

    void Awake()
    {
        hiddenMethods = new HiddenMethods(this);
        floorCheckers = new Transform[floorCheckersT.childCount];
        for (int i = 0; i < floorCheckers.Length; i++)
        {
            floorCheckers[i] = floorCheckersT.GetChild(i);
        }
    }

    bool UsingGravity = true;
    public void SetUsingGravity(bool _on)
    {
        UsingGravity = _on;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (_DATA_CENTER._MyBehaviorRunner.IfRunning())
        {
            if (hiddenMethods.IfStepOnEnemy(collision.collider))
            {
                hiddenMethods.ITouchedThisCollider(1);
                hiddenMethods.meTouchingEnemyBody = true;
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (_DATA_CENTER._MyBehaviorRunner.IfRunning())
        {
            if (hiddenMethods.IfStepOnEnemy(collision.collider))
            {
                hiddenMethods.ITouchedThisCollider(1);
                hiddenMethods.meTouchingEnemyBody = false;
            }
        }
    }
    
    Vector3 temp,temp2;
    float dis_from_center;
    float groundedCount;
    float airCount;
    void OnAnimatorMove()
    {
        if (FightGlobalSetting.scenestep == 1)
        {
            if (_DATA_CENTER._MyBehaviorRunner.IfRunning())
            {
                hiddenMethods.GroundedCal();
                animator.SetBool("Grounded", hiddenMethods.Grounded);
                animator.SetFloat("airCount", airCount);
                animator.SetFloat("groundedCount", groundedCount);
                groundedCount = (hiddenMethods.Grounded) ? groundedCount += Time.deltaTime : 0f;
                airCount = (!hiddenMethods.Grounded) ? airCount += Time.deltaTime : 0f;
            }
            
            temp2 = transform.position;
            temp2.y = 0;
            dis_from_center = temp2.magnitude;
            if (dis_from_center > BoundaryControllByGod._BattleRingRadius)
            {
                temp = temp2.normalized * BoundaryControllByGod._BattleRingRadius;
                temp.y = transform.position.y;
                transform.position = temp;
                hiddenMethods.onBattleGroundBundary = true;
            }
            else
            {
                hiddenMethods.onBattleGroundBundary = false;
            }
            
            temp = transform.position;
            if (temp.y < 0)
            {
                temp.y = 0f;
                transform.position = temp;
            }
        }
        if (animator.applyRootMotion)
        {
            animator.ApplyBuiltinRootMotion();
        }
    }
}

//private List<Vector3> houtui = new List<Vector3>();
//private bool canBodyPush = true;//这个值的存在并不是为了突出角色“体重”而是为了让一些冲击类技能的穿透性不会受“角色互斥系统”的影响
//public void setBodyPushFlag(bool flag)
//{
//    canBodyPush = flag;
//}
//if (canBodyPush)
//{
//    averageDirection = Vector3.zero;
//    if (houtui.Count > 0)
//    {
//        foreach (Vector3 t in houtui)
//        {
//            averageDirection += t;
//        }
//        averageDirection /= houtui.Count;
//        forceDirection = (gameObject.transform.position - averageDirection).normalized;
//        forceDirection.y = 0f;
//        //Rigidbody.velocity = forceDirection * 10f;
//        //Rigidbody.AddForce(forceDirection * 20f, ForceMode.VelocityChange);
//        //gameObject.transform.position += forceDirection;
//        gameObject.transform.position =
//                      Vector3.Lerp(gameObject.transform.position, gameObject.transform.position + forceDirection * 100, Time.deltaTime);//这个100是主观的。
//    }
//}
//houtui.Clear();
//public void addHoutuiForcePoint(Vector3 point)
//{
//    houtui.Add(point);
//}
//private Vector3 averageDirection;
//private Vector3 forceDirection;