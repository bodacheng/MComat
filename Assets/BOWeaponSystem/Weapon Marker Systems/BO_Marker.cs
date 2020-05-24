using UnityEngine;
using System.Collections.Generic;

namespace HittingDetection
{
    public class BO_Marker : Marker
    {
        public float radius;
        IDictionary<Collider, HitPointPara> BallDetectHitPool = new Dictionary<Collider, HitPointPara>();
                
        public IDictionary<Collider, HitPointPara> GetBallDetectHitPool()
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
                if (!BallDetectHitPool.Keys.Contains(other))
                {
                    HitPointPara hitPointPara = new HitPointPara()
                    {
                        pos = HitPointCal(other.transform.position),
                        qua = Quaternion.LookRotation(other.transform.position - HitPointCal(other.transform.position), Vector3.up)
                    };
                    BallDetectHitPool.Add(other,hitPointPara);
                }
            }
        }

        float temp;
        Vector3 _temp;
        public Vector3 HitPointCal(Vector3 colliderCenterPosition)
        {
            temp = Mathf.Clamp((colliderCenterPosition - myCollider.transform.position).magnitude,0,radius);
            _temp = myCollider.transform.position + (colliderCenterPosition - myCollider.transform.position).normalized * temp;
            _temp = new Vector3(Mathf.Round(_temp.x),Mathf.Round(_temp.y),Mathf.Round(_temp.z));
            return _temp;
        }
        
        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}

    //_StartPoint = BallDetectHitPool[hit_target_index].ClosestPointOnBounds(_markers[i].transform.position);
    //这个地方不能用ClosestPoint。这里存在unity官方bug。
    //_StartPoint = _StartPoint + (BallDetectHitPool[hit_target_index].transform.position - _StartPoint) * 0.3f;
    //为了打击特效看起来更接近肉体 向里切一下。
    // 以下是几种 _StartPoint的其他算法
    // 1.
    // Vector3 fromMarkerToHit = BallDetectHitPool[hit_target_index].transform.position - _markers[i].transform.position;
    // _StartPoint = _markers[i].transform.position + fromMarkerToHit / 2;//我是觉得离攻击体近更稳健些
    // _StartPoint = BallDetectHitPool[hit_target_index].transform.position;
    // 2.
    // _StartPoint = _Raw_Target_Instance.getHealthBodyCenterTransform().position;// TEST
    // 如果计算的某个点和collider的closetPoint，这个collider在场景里和其他collider有位置上的重合，那这个函数会出错