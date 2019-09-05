using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class BO_Hitbox : MonoBehaviour {

	[Tooltip("What is this Limb attached to? Select the desired BS Health script which stores the Health of this Object (for example in one of the limb's parents.")]
    public BO_Health MainHealth;
    [Tooltip("ColliderOfThisHitBox.MUST HAVE")]
    public Collider myColliderMustEquip;

    void Awake()
	{
        if (MainHealth != null)       
            MainHealth.addToBOHitBoxeComponent(this);
	}

    public void INI()
    {
        if (GetComponent<Collider>())
        {
            myColliderMustEquip = GetComponent<Collider>();
        }
        else
        {
            Debug.Log("hitbox" + transform + "没有配置collider，并不会自动创建，请检查");
            this.enabled = false;
            return;
        }

        if (MainHealth == null)//就是说，在awake阶段角色必须被适配mainhealth
        {
            Debug.Log("hitbox" + this.gameObject.name + "由于没有适配health体而将尝试关闭");
            this.enabled = false;
            return;
        }
    }

    //void OnCollisionEnter(Collision collision)
    //{
        //if (enable)
        //{
        //    if (AI_DATA_CENTER == null)
        //        return;

        //    if (AI_DATA_CENTER._loadMode == loadMode.fightModel)
        //    {
        //        if (MainHealth.ifStepOnEnemyCharacter(collision.collider) )
        //        {
        //            if (AI_DATA_CENTER.Sensor != null)
        //                AI_DATA_CENTER.Sensor.innerEnemies.Add(collision.collider);

        //            this.MainHealth.WhenIHitSomethingEnemy(1);
        //            if (!AI_DATA_CENTER.IsGrounded())
        //                MainHealth.addHoutuiForcePoint(collision.collider.transform.position);
        //        }
        //        if (MainHealth.ifStepOnFriendCharacter(collision.collider))
        //        {
        //            if (!AI_DATA_CENTER.IsGrounded())
        //                MainHealth.addHoutuiForcePoint(collision.collider.transform.position);
        //        }
        //    }
        //}
    //}

    //void OnTriggerExit(Collider other)
    //{
        //if (other.gameObject.layer == 19)
        //{
        //    GetComponent<Collider>().isTrigger = false;
        //    Debug.Log("出地");
        //}
    //}

    //void OnTriggerEnter(Collider collision)//这个是针对角色倒地
    //{
        //if (enable)
        //{
        //    if (AI_DATA_CENTER == null)
        //        return;

        //    if (AI_DATA_CENTER._loadMode == loadMode.fightModel)
        //    {
        //        if (MainHealth.ifStepOnEnemyCharacter(collision.GetComponent<Collider>()))
        //        {
        //            this.MainHealth.WhenIHitSomethingEnemy(1);
        //        }
        //    }
        //}
    //}

    //void OnCollisionStay(Collision collision)
    //{
        //if (enable)
        //{
        //    if (AI_DATA_CENTER == null)
        //        return;
        //    if (collision.collider.gameObject.layer == 13)
        //    {
        //        if (AI_DATA_CENTER)
        //            AI_DATA_CENTER.animator.applyRootMotion = false;

        //        Vector3 to = collision.collider.transform.right * 3f;
        //        to.y = 0;
        //        myRigidbody.AddForce(to * 1f, ForceMode.VelocityChange);
        //    }
        //}
    //}
}
