using System.Collections.Generic;
using UnityEngine;

public class Pusher : MonoBehaviour
{
    public class HiddenMethods
    {
        readonly Pusher pusher;
        public HiddenMethods(Pusher Pusher)
        {
            this.pusher = Pusher;
        }
        public bool IfStepOnEnemyCharacter(Collider box)
        {
            if (box == null)
                return false;
            if (box.isTrigger)
                return false;
            if (pusher._DATA_CENTER == null)
                return false;
            if (pusher._DATA_CENTER._TeamConfig == null)
                return false;

            //这个comment out 的写法貌似效果是一样的。
            //if ((AI_DATA_CENTER._TeamConfig.enemyLayerMask & (1 << box.gameObject.layer)) != 0
            //||
            //(AI_DATA_CENTER._TeamConfig.enemyShieldLayerMask & (1 << box.gameObject.layer)) != 0)
            return pusher._DATA_CENTER._TeamConfig.enemyLayerMask == (pusher._DATA_CENTER._TeamConfig.enemyLayerMask | (1 << box.gameObject.layer))
                ||
                (pusher._DATA_CENTER._TeamConfig.enemyShieldLayerMask & (1 << box.gameObject.layer)) != 0
                ? true
                : false;
        }

        public bool IfStepOnFriendCharacter(Collider box)
        {
            return pusher._DATA_CENTER == null
                || pusher._DATA_CENTER._TeamConfig != null
                && (pusher._DATA_CENTER._TeamConfig.mylayer == box.gameObject.layer)
               ||
                pusher._DATA_CENTER._TeamConfig.myShieldLayer == box.gameObject.layer
                ? true
                : false;
        }

        public void WhenIHitSomethingEnemy(int _dmg)
        {
            if (pusher.ICauseDamagersForAttackSteppingCommand != null)
            {
                pusher.ICauseDamagersForAttackSteppingCommand.Add(_dmg);
            }
        }
        
        public void ClearHitCountForAttackStepping()
        {
            pusher.ICauseDamagersForAttackSteppingCommand.Clear();
        }
        public bool IHitSomethingEnemy()
        {
            return pusher.ICauseDamagersForAttackSteppingCommand.Count > 0 ? true : false;
        }
    }

    public Data_Center _DATA_CENTER;
    public HiddenMethods hiddenMethods;
    readonly List<int> ICauseDamagersForAttackSteppingCommand = new List<int>();

    void Awake()
    {
        hiddenMethods = new HiddenMethods(this);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (_DATA_CENTER.AIStateRunner.IfRunning())
        {
            if (hiddenMethods.IfStepOnEnemyCharacter(collision.collider))
            {
                if (_DATA_CENTER.Sensor != null)
                    _DATA_CENTER.Sensor.getInnerEnemiesColliders().Add(collision.collider);
                hiddenMethods.WhenIHitSomethingEnemy(1);
            }
        }
    }

    Vector3 temp;
    float dis_from_center;
    void OnAnimatorMove()
    {
        _DATA_CENTER.animator.ApplyBuiltinRootMotion();
        temp = transform.position;
        temp.y = 0;
        dis_from_center = transform.position.magnitude;
        if (transform.position.magnitude > BoundaryControllByGod._BattleRingRadius)
        {
            transform.position = 20 * temp / dis_from_center;
            _DATA_CENTER.onBattleGroundBundary = true;
        }else
        {
            _DATA_CENTER.onBattleGroundBundary = System.Math.Abs(transform.position.magnitude - BoundaryControllByGod._BattleRingRadius) < 0.01f;
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