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

    public bool atRing
    {
        get;
        set;
    }

    public class HiddenMethods
    {
        readonly BasicPhysicSupport _BasicPhysicSupport;
        
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

        //弃用
        private Vector3 keptEnemyPoint;
        private Vector3 keptMePoint;
        public bool lockedKept;
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
            if (!EnemyTouchingDrag)
                return;
            if (!touchingEnemyCs.Contains(C))
                touchingEnemyCs.Add(C);
            switch (draglevel)
            {
                case 1:
                    _BasicPhysicSupport.Rigidbody.drag = 100f;
                    break;
                case 2:
                    _BasicPhysicSupport.Rigidbody.drag = 200f;
                    break;
                case 3:
                    _BasicPhysicSupport.Rigidbody.drag = 300f;
                    break;
            }

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
            if (!EnemyTouchingDrag)
            {
                touchingEnemyCs.Clear();
                return;
            }
            if (touchingEnemyCs.Contains(C))
                touchingEnemyCs.Remove(C);
        }
        
        public void ClearTouchedEnemyBody()
        {
            touchingEnemyCs.Clear();
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
    void Update()
    {
        if (FightGlobalSetting.scenestep == 1)
        {
            hiddenMethods.GroundedCal();
            BoundaryControllByGod.LimitTargetToRange(_DATA_CENTER);
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
        if (open == 0)
        {
            Rigidbody.drag = 0;
        }
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