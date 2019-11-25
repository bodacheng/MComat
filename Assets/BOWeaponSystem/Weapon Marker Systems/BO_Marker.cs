//The Marker Script
//All markers must be a child of Marker Menager

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

namespace HittingDetection
{
    public class BO_Marker : MonoBehaviour
    {
        [HideInInspector]
        public Vector3 _tempPos; //Temporary position of the marker from the last frame
        float _dist; //distance between temp and actual marker position
        Vector3 _dir; //Direction of the above.

        [HideInInspector]
        public RaycastHit[] _hits = new RaycastHit[0]; //What was hit in this frame?
        [Tooltip("Choose which Layers should be affected by this marker's hit check.")]
        public LayerMask _layers;
        public LayerMask enemyShieldLayer;
        public float radius;
        public Hit_detection_mode mode = Hit_detection_mode.ball_detect;
        SphereCollider myCollider;
        List<Collider> BallDetectHitPool = new List<Collider>();
        public List<Collider> GetBallDetectHitPool()
        {
            return BallDetectHitPool;
        }

        void Awake()
        {
            _tempPos = transform.position;
            myCollider = this.gameObject.GetComponent<SphereCollider>();
            if (myCollider == null)
            {
                myCollider = this.gameObject.AddComponent<SphereCollider>();
            }
            if (mode == Hit_detection_mode.ball_detect)
            {
                myCollider.radius = radius;
                myCollider.isTrigger = true;
            }
            else
            {
                myCollider.radius = 0.1f;//这个是为了什么呢。。比如剑，它如果没有一个小collider的话那不是不好被其他角色检测来躲闪吗？
                myCollider.isTrigger = true;
            }
        }

        public bool HitCheck()
        {
            if (mode == Hit_detection_mode.trail_detect)
            {
                _dir = transform.position - _tempPos;
                _dist = Vector3.Distance(transform.position, _tempPos);
                //Debug.DrawRay(_tempPos, _dir, Color.white, 0.3f);
                //为什么要raycastALl？说到底也是因为我们想让系统简单化，不给所有物体分层，从而这个轨道可能会停于自身武器上的collider
                _hits = Physics.RaycastAll(_tempPos, _dir, _dist, _layers, QueryTriggerInteraction.Ignore);
                if (_hits.Length == 0)
                {
                    _tempPos = transform.position;
                }
            }
            return BallDetectHitPool.Count > 0 || _hits.Length > 0;
        }

        public void ClearDetection()
        {
            BallDetectHitPool.Clear();
            _hits = new RaycastHit[0];
        }

        void OnTriggerEnter(Collider other)
        {
            BallDetectModeDetection(other);
        }

        void OnTriggerStay(Collider other)
        {
            BallDetectModeDetection(other);
        }

        void BallDetectModeDetection(Collider other)
        {
            if (mode == Hit_detection_mode.ball_detect)
            {
                //_hits = Physics.SphereCastAll(_tempPos, radius, _dir, _dist, _layers, QueryTriggerInteraction.Collide);// 如果有能力把这个句子去掉最好。会极大幅度提高整个程序速度，但对于相应的代价得有替代方案
                //实际上吧，做到现在我们意识到一个问题：伤害判定系统这东西你不动用分层机制的话不可能保证程序效率。如果在上面这个地方引入层的话，起码我们可以用的了sphereCast而不是消耗巨大的SphereCastAll
                //BallDetectHitPool = Physics.OverlapSphere(this.transform.position, radius, _layers, QueryTriggerInteraction.Collide);

                if (_layers == (_layers | (1 << other.gameObject.layer)))
                {
                    BallDetectHitPool.Add(other);
                }
                if (BallDetectHitPool.Count == 0)
                {
                    _tempPos = transform.position;
                }
            }
        }

        void OnDrawGizmosSelected()
        {
            if (mode == Hit_detection_mode.ball_detect)
            {
                Gizmos.color = Color.white;
                Gizmos.DrawWireSphere(transform.position, radius);
            }
        }
    }
}

public enum Hit_detection_mode
{
    trail_detect = 1,
    ball_detect = 2
}