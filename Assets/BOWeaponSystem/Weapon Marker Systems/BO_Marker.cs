using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace HittingDetection
{
    public class BO_Marker : Marker
    {
        public float radius;
        List<Collider> BallDetectHitPool = new List<Collider>();
        
        public List<Collider> GetBallDetectHitPool()
        {
            return BallDetectHitPool;
        }

        public override void LocalAwake()
        {
            base.LocalAwake();
            myCollider.radius = radius;
            myCollider.isTrigger = true;
        }

        public override bool HitCheck()
        {
            return BallDetectHitPool.Count > 0;
        }
        
        public override void EnableMarkerProcess(int weaponLayer)
        {
            base.EnableMarkerProcess(weaponLayer);
        }
        
        public override void DisableMarkerProcess()
        {
            base.DisableMarkerProcess();
        }
        
        public override void ClearMarkerProcess()
        {
            ClearDetection();
        }

        protected override void ClearDetection()
        {
            BallDetectHitPool.Clear();
        }

        protected override void OnTriggerEnter(Collider other)
        {
            BallDetectModeDetection(other);
        }

        protected override void OnTriggerStay(Collider other)
        {
            BallDetectModeDetection(other);
        }

        void BallDetectModeDetection(Collider other)
        {
            //_hits = Physics.SphereCastAll(_tempPos, radius, _dir, _dist, _layers, QueryTriggerInteraction.Collide);// 如果有能力把这个句子去掉最好。会极大幅度提高整个程序速度，但对于相应的代价得有替代方案
            //实际上吧，做到现在我们意识到一个问题：伤害判定系统这东西你不动用分层机制的话不可能保证程序效率。如果在上面这个地方引入层的话，起码我们可以用的了sphereCast而不是消耗巨大的SphereCastAll
            //BallDetectHitPool = Physics.OverlapSphere(this.transform.position, radius, _layers, QueryTriggerInteraction.Collide);
            if (_layers == (_layers | (1 << other.gameObject.layer)))
            {
                BallDetectHitPool.Add(other);
            }
        }

        float temp;
        public Vector3 HitPointCal(Vector3 colliderCenterPosition)
        {
            temp = Mathf.Clamp((colliderCenterPosition - transform.position).magnitude,0,radius);
            return transform.position + (colliderCenterPosition - transform.position).normalized * temp;
        }
        
        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}

