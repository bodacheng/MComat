using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pusher : MonoBehaviour
{
    public Data_Center _DATA_CENTER;
    private List<int> ICauseDamagersForAttackSteppingCommand = new List<int>();

    void OnCollisionEnter(Collision collision)
    {
        if (_DATA_CENTER.AIStateRunner.ifRunning())
        {
            if (ifStepOnEnemyCharacter(collision.collider))
            {
                if (_DATA_CENTER.Sensor != null)
                    _DATA_CENTER.Sensor.getInnerEnemiesColliders().Add(collision.collider);
                WhenIHitSomethingEnemy(1);
            }
        }
    }
        
    public bool ifStepOnEnemyCharacter(Collider box)
    {
        if (box == null)
            return false;
        if (box.isTrigger)
            return false;
        if (_DATA_CENTER == null)
            return false;
        if (_DATA_CENTER._TeamConfig == null)
            return false;

        //这个comment out 的写法貌似效果是一样的。
        //if ((AI_DATA_CENTER._TeamConfig.enemyLayerMask & (1 << box.gameObject.layer)) != 0
        //||
        //(AI_DATA_CENTER._TeamConfig.enemyShieldLayerMask & (1 << box.gameObject.layer)) != 0)
        if (_DATA_CENTER._TeamConfig.enemyLayerMask == (_DATA_CENTER._TeamConfig.enemyLayerMask | (1 << box.gameObject.layer))
            ||
            (_DATA_CENTER._TeamConfig.enemyShieldLayerMask & (1 << box.gameObject.layer)) != 0)
        {
            return true;
        }
        return false;
    }
    
    public bool ifStepOnFriendCharacter(Collider box)
    {
        if (_DATA_CENTER == null)
            return false;
        if (_DATA_CENTER._TeamConfig == null)
            return false;

        if ((_DATA_CENTER._TeamConfig.mylayer ==  box.gameObject.layer)
           ||
            _DATA_CENTER._TeamConfig.myShieldLayer ==  box.gameObject.layer)
        {
            return true;
        }
        return false;
    }
    
    public void WhenIHitSomethingEnemy(int _dmg)
    {
        if (ICauseDamagersForAttackSteppingCommand != null)
        {
            ICauseDamagersForAttackSteppingCommand.Add(_dmg);
        }
    }
    
    public void clearHitCountForAttackStepping()
    {
        ICauseDamagersForAttackSteppingCommand.Clear();
    }
    public bool IHitSomethingEnemy()
    {
        if (ICauseDamagersForAttackSteppingCommand.Count > 0)
        {
            return true;
        }else{
            return false;
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