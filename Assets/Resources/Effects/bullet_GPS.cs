using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class bullet_GPS : MonoBehaviour {

    public BO_Marker_Manager myWeapon;
    public Rigidbody _Rigidbody;
    public float navigation_delay;
    public float navigation_time;
    private float time_counter;

    public float force;
    public int sensorRate = 0;
    public float sensorRange;
    public LayerMask layerMask;
    private List<Collider> _hits;

	void OnEnable()
	{
        time_counter = 0f;
        if (myWeapon)
        {
            if (myWeapon.getTeamConfig() != null)
                layerMask = myWeapon.getTeamConfig().enemyAndEnemyWeaponLayerMask;
        }
    }
    void OnDisable()
    {
        time_counter = 0f;
    }

    void Awake()
	{
        if (_Rigidbody != null)
        {
            _Rigidbody.mass = 1f;
            _Rigidbody.useGravity = false;
        }
    }

	void Start () {

	}

	void FixedUpdate()
	{
        time_counter += Time.fixedDeltaTime;
        if (time_counter < navigation_delay)
            return;
        if (time_counter < navigation_time)
        {
            if (this._hits != null)
            {
                if (this._hits.Count > 0)
                {
                    //_hits.Sort((Collider a, Collider b) => DistanceCompare(a, b));
                    if (this._hits[0] != null)
                    {
                        addForceToTargetDirection(this._hits[0].transform.position, force, true);
                    }
                }
            }
        }

        if (sensorRate >= 5)
        {
            _hits = Physics.OverlapSphere(transform.position, sensorRange, layerMask).ToList();//这个东西消耗太大，起码可以考虑减少运行次数
            sensorRate = 0;
        }
        sensorRate++;
	}

    float p1_to_me, p2_to_me;
    public int DistanceCompare(Collider p1, Collider p2)
    {
        if (p1 == null || p2 == null)
        {
            return 0;
        }
        p1_to_me = (p1.gameObject.transform.position - gameObject.transform.position).magnitude;
        p2_to_me = (p2.gameObject.transform.position - gameObject.transform.position).magnitude;

        if (p1_to_me > p2_to_me)
        {
            return 1;
        }
        if (p1_to_me < p2_to_me)
        {
            return -1;
        }
        return 0;
    }

	protected void addForceToTargetDirection(Vector3 target, float force, bool ignoreY)
    {
        Vector3 look_dir = target - gameObject.transform.position;
        if (ignoreY)
        {
            look_dir.y = 0;
        }
        if (_Rigidbody)
        {
            _Rigidbody.AddForce(look_dir * force);
        }
    }
}
