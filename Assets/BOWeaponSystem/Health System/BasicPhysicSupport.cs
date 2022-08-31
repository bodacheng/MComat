using System.Collections.Generic;
using UnityEngine;

public class BasicPhysicSupport : MonoBehaviour
{
    public Data_Center _DATA_CENTER;
    public Animator animator;
    public Rigidbody Rigidbody;
    public HiddenMethods hiddenMethods;
    
    public bool AtRing
    {
        get;
        set;
    }

    public class HiddenMethods
    {
        readonly BasicPhysicSupport _BasicPhysicSupport;
        public bool EnemyTouchingDrag;
        
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
        readonly List<Collider> _touchingEnemyCs = new List<Collider>();
        
        public bool TouchingEnemy()
        {
            return _touchingEnemyCs.Count > 0;
        }
        
        //弃用
        private Vector3 keptEnemyPoint;
        private Vector3 keptMePoint;
        public Vector3 ClampPosBetweenMeAndE(Vector3 pos)
        {
            pos.y = 0;
            float temp = Vector3.Dot(pos - keptMePoint, keptEnemyPoint - keptMePoint);
            temp = Mathf.Clamp( temp, temp,0 );
            pos = keptMePoint + ( keptEnemyPoint- keptMePoint).normalized * temp;
            return pos;
        }
        
        public void AddTouchedEnemyBody(Collider C)
        {
            if (!_touchingEnemyCs.Contains(C))
                _touchingEnemyCs.Add(C);
            
            // if (!lockedKept && touchingEnemyCs.Count > 0)
            // {
            //     lockedKept = true;
            //     BO_Limb l = C.transform.GetComponent<BO_Limb>();
            //     if (l != null)
            //     {
            //         keptEnemyPoint = l.Center.WholeT.position;
            //         keptMePoint = _BasicPhysicSupport._DATA_CENTER.WholeT.position;
            //         keptMePoint.y = 0;
            //         keptEnemyPoint.y = 0;
            //     }
            // }
        }
        public void RemoveTouchedEnemyBody(Collider C)
        {
            if (_touchingEnemyCs.Contains(C))
                _touchingEnemyCs.Remove(C);
        }
        
        public void ClearTouchedEnemyBody()
        {
            _touchingEnemyCs.Clear();
            _BasicPhysicSupport.Rigidbody.drag = 0f;
        }

        public bool Grounded { get; set; }

        readonly float floorY = 0f;
        public void GroundedCal()
        {
            // foreach (var check in _BasicPhysicSupport.floorCheckers)
            // {
            //     if (floorY >= check.transform.position.y)
            //     {
            //         Grounded = true;
            //         _BasicPhysicSupport.Rigidbody.useGravity = false;
            //         return;
            //     }
            // }

            if (_BasicPhysicSupport._DATA_CENTER.WholeT.position.y <= floorY)
            {
                Grounded = true;
                _BasicPhysicSupport.Rigidbody.useGravity = false;
                return;
            }
            _BasicPhysicSupport.Rigidbody.useGravity = _BasicPhysicSupport.usingGravity;
            Grounded = false;
        }
        
        public void RecoverRootPosChange( )
        {
            if (!TouchingEnemy() && _BasicPhysicSupport.Rigidbody.velocity == Vector3.zero)
                _BasicPhysicSupport._DATA_CENTER.WholeT.transform.position += _BasicPhysicSupport._DATA_CENTER.Animation_Manger.AnimatorRef.deltaPosition;
        }

        public void LockPos()
        {
            _BasicPhysicSupport.hiddenMethods.Grounded = true;
            _BasicPhysicSupport.SetUsingGravity(false);
            _BasicPhysicSupport.Rigidbody.constraints = RigidbodyConstraints.FreezeAll;
            _BasicPhysicSupport.Rigidbody.velocity = Vector3.zero;
        }
    }
    
    void Awake()
    {
        hiddenMethods = new HiddenMethods(this);
        Rigidbody.drag = 0;
    }
    
    void Update()
    {
        if (FightGlobalSetting.scenestep == 1)
        {
            hiddenMethods.GroundedCal();
            BoundaryControlByGod.LimitTargetToRange(_DATA_CENTER);
        }
    }

    private bool usingGravity;
    public void SetUsingGravity(bool _on)
    {
        usingGravity = _on;
    }

    public void OpenEnemyTouchingDrag(int open)
    {
        hiddenMethods.EnemyTouchingDrag = open != 0;
        if (!hiddenMethods.EnemyTouchingDrag)
            hiddenMethods.ClearTouchedEnemyBody();
    }

    
    void OnCollisionEnter(Collision collision)
    {
        if (!hiddenMethods.EnemyTouchingDrag) return;
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
        if (!hiddenMethods.EnemyTouchingDrag) return;
        if (_DATA_CENTER._MyBehaviorRunner.IfRunning())
        {
            if (hiddenMethods.IfStepOnEnemy(collision.collider))
            {
                hiddenMethods.RemoveTouchedEnemyBody(collision.collider);
            }
        }
    }
}