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
        
        public bool onBattleGroundBundary;
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

        // 与敌人的接触摩操功能
        public bool EnemyTouchingDrag;
        public List<Collider> touchingEnemyCs = new List<Collider>();
        public bool ITouchedEnemyBody()
        {
            return touchingEnemyCs.Count > 0;
        }

        public int draglevel;
        public void AddTouchedEnemyBody(Collider C)
        {
            if (!EnemyTouchingDrag)
                return;
            if (!touchingEnemyCs.Contains(C))
                touchingEnemyCs.Add(C);
            switch (draglevel)
            {
                case 1:
                    _BasicPhysicSupport.Rigidbody.drag = 10f;
                    break;
                case 2:
                    _BasicPhysicSupport.Rigidbody.drag = 30f;
                    break;
                case 3:
                    _BasicPhysicSupport.Rigidbody.drag = 50f;
                    break;
            }
            
        }
        public void RemoveTouchedEnemyBody(Collider C)
        {
            if (!EnemyTouchingDrag)
                return;
            if (touchingEnemyCs.Contains(C))
                touchingEnemyCs.Remove(C);
            if (touchingEnemyCs.Count == 0)
            {
                _BasicPhysicSupport.Rigidbody.drag = 0f;
            }
        }
        public void ClearTouchedEnemyBody()
        {
            touchingEnemyCs.Clear();
            _BasicPhysicSupport.Rigidbody.drag = 0f;
        }
        
        bool grounded;
        public bool Grounded
        {
            get => grounded;
            set {
                grounded = value;
            }
        }
        
        readonly float floorY = 0f;
        public void GroundedCal()
        {
            foreach (var check in _BasicPhysicSupport.floorCheckers)
            {
                if (floorY >= check.transform.position.y)
                {
                    Grounded = true;
                    _BasicPhysicSupport.Rigidbody.useGravity = false;
                    return;
                }
            }
            _BasicPhysicSupport.Rigidbody.useGravity = _BasicPhysicSupport.usingGravity;
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
    
    Vector3 temp,temp2;
    float dis_from_center;
    float groundedCount;
    float airCount;
    void Update()
    {
        if (FightGlobalSetting.scenestep == 1)
        {
            if (_DATA_CENTER._MyBehaviorRunner.IfRunning())
            {
                hiddenMethods.GroundedCal();
                animator.SetBool("Grounded", hiddenMethods.Grounded);
                animator.SetFloat("airCount", airCount);
                animator.SetFloat("groundedCount", groundedCount);
                groundedCount = hiddenMethods.Grounded ? groundedCount += Time.deltaTime : 0f;
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
    }

    private bool usingGravity;
    public void SetUsingGravity(bool _on)
    {
        usingGravity = _on;
    }

    public void OpenEnemyTouchingDrag(int open)
    {
        hiddenMethods.draglevel = open;
        hiddenMethods.EnemyTouchingDrag = open != 0;
        if (hiddenMethods.EnemyTouchingDrag == false)
            hiddenMethods.ClearTouchedEnemyBody();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (_DATA_CENTER._MyBehaviorRunner.IfRunning())
        {
            if (hiddenMethods.IfStepOnEnemy(collision.collider))
            {
                hiddenMethods.AddTouchedEnemyBody(collision.collider);
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (_DATA_CENTER._MyBehaviorRunner.IfRunning())
        {
            if (hiddenMethods.IfStepOnEnemy(collision.collider))
            {
                hiddenMethods.RemoveTouchedEnemyBody(collision.collider);
            }
        }
    }
}